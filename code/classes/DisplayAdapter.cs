using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Display
{

	/// <summary>Encapsulates a <see cref="DisplayDevice"/> structure related to a display adapter.</summary>
	public sealed class DisplayAdapter : DisplayDeviceBase
	{

		/// <summary>Defines the maximum number of display adapters supported by the system: 16.</summary>
		public const int MaxAdapterCount = 16;


		#region Static


		/// <summary>Enumerates flags used by EnumDisplaySettingsEx.</summary>
		[Flags]
		private enum EnumDisplaySettingsExOptions : int
		{

			/// <summary>Undefined.</summary>
			None = 0x00000000,

			/// <summary>If set, the EnumDisplaySettingsEx function will return all graphics modes reported by the adapter driver, regardless of monitor capabilities.
			/// Otherwise, it will only return modes that are compatible with current monitors.
			/// </summary>
			RawMode = 0x00000001,

			/// <summary>If set, the EnumDisplaySettingsEx function will return graphics modes in all orientations.
			/// Otherwise, it will only return modes that have the same orientation as the one currently set for the requested display.
			/// </summary>
			RotatedMode = 0x00000002

		}


		/// <summary>Provides access to native functions (located in user32.dll, defined in WinUser.h).
		/// <para>Requires Windows Vista or newer.</para>
		/// </summary>
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{

			private const string LibraryName = "User32.dll";


			#region EnumDisplayDevices

			/// <summary>Enumerates flags used by EnumDisplayDevices.</summary>
			[Flags]
			private enum EnumDisplayDevicesOptions : int
			{
				None = 0x00000000,
				GetDeviceInterfaceName = 0x00000001
			}


			/// <summary>Retrieves information about the display devices in the current session.</summary>
			/// <param name="deviceName">The device name. If null, the function returns information for the display adapter(s) on the machine, based on <paramref name="deviceIndex"/>.
			/// </param>
			/// <param name="deviceIndex">An index value that specifies the display device of interest.
			/// The operating system identifies each display device in the current session with an index value.
			/// The index values are consecutive integers, starting at 0. If the current session has three display devices, for example, they are specified by the index values 0, 1, and 2.
			/// </param>
			/// <param name="displayDevice">A pointer to a <see cref="DisplayDevice"/> structure that receives information about the display device specified by <paramref name="deviceIndex"/>.
			/// Before calling this function, you must initialize the <code>cb</code> member of <paramref name="displayDevice"/> to the size, in bytes, of <see cref="DisplayDevice"/>.
			/// </param>
			/// <param name="options">Set this flag to <see cref="EnumDisplayDevicesOptions.GetDeviceInterfaceName"/> to retrieve the device interface name for GUID_DEVINTERFACE_MONITOR, which is registered by the operating system on a per monitor basis.
			/// The value is placed in the <see cref="DisplayDevice.DeviceId"/> member of the returned <paramref name="displayDevice"/> structure.
			/// The resulting device interface name can be used with SetupAPI functions and serves as a link between GDI monitor devices and SetupAPI monitor devices.
			/// </param>
			/// <returns>Returns true on success, otherwise false (<paramref name="deviceIndex"/> is out-of-range).</returns>
			/// <remarks>
			/// To query all display devices in the current session, call this function in a loop, starting with <paramref name="deviceIndex"/> set to 0, and incrementing <paramref name="deviceIndex"/> until the function fails. To select all display devices in the desktop, use only the display devices that have the DISPLAY_DEVICE_ATTACHED_TO_DESKTOP flag in the <see cref="DisplayDevice"/> structure.
			/// To get information on the display adapter, call EnumDisplayDevices with <paramref name="deviceName"/> set to null. For example, <see cref="DisplayDevice"/>.DeviceString contains the adapter name.
			/// To obtain information on a display monitor, first call EnumDisplayDevices with <paramref name="deviceName"/> set to null. Then call EnumDisplayDevices with <paramref name="deviceName"/> set to <see cref="DisplayDevice"/>.DeviceName from the first call to EnumDisplayDevices and with <paramref name="deviceIndex"/> set to zero. Then <see cref="DisplayDevice"/>.DeviceString is the monitor name.
			/// To query all monitor devices associated with an adapter, call EnumDisplayDevices in a loop with <paramref name="deviceName"/> set to the adapter name, <paramref name="deviceIndex"/> set to start at 0, and <paramref name="deviceIndex"/> set to increment until the function fails. Note that <see cref="DisplayDevice"/>.DeviceName changes with each call for monitor information, so you must save the adapter name.
			/// The function fails when there are no more monitors for the adapter.
			/// </remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			private static extern bool EnumDisplayDevicesW(
				[In, Optional, MarshalAs( UnmanagedType.LPWStr )] string deviceName,
				[In] int deviceIndex,
				[In, Out] ref DisplayDevice displayDevice,
				[In] EnumDisplayDevicesOptions options
			);
			// https://msdn.microsoft.com/en-us/library/dd162609%28v=vs.85%29.aspx
			//BOOL EnumDisplayDevices(
			//	_In_opt_ LPCWSTR lpDevice,
			//	_In_ DWORD iDevNum,
			//	_Inout_ PDISPLAY_DEVICEW lpDisplayDevice,
			//	_In_ DWORD dwFlags);


			/// <summary>Returns a read-only collection of <see cref="DisplayDevice"/> structures containing information about the display devices in the current session.</summary>
			/// <param name="deviceName">The device name; if null, the function returns information for the display adapters on the machine.</param>
			/// <param name="getMonitorDeviceInterfaceName">
			/// true to retrieve the device interface name for GUID_DEVINTERFACE_MONITOR, which is registered by the operating system on a per monitor basis.
			/// The value is placed in the <code>DeviceID</code> member of the returned <see cref="DisplayDevice"/> structure.
			/// The resulting device interface name can be used with SetupAPI functions and serves as a link between GDI monitor devices and SetupAPI monitor devices.
			/// </param>
			/// <returns>Returns a read-only collection of <see cref="DisplayDevice"/> structures containing information about the display devices in the current session.</returns>
			internal static ReadOnlyCollection<DisplayDevice> EnumDisplayDevices( string deviceName, bool getMonitorDeviceInterfaceName )
			{
				if( string.IsNullOrWhiteSpace( deviceName ) )
					deviceName = null;

				var list = new List<DisplayDevice>( MaxAdapterCount );

				var options = EnumDisplayDevicesOptions.None;
				if( getMonitorDeviceInterfaceName )
					options = EnumDisplayDevicesOptions.GetDeviceInterfaceName;

				int deviceIndex = 0;
				var device = DisplayDevice.Default;

				while( EnumDisplayDevicesW( deviceName, deviceIndex++, ref device, options ) )
					list.Add( device );

				return new ReadOnlyCollection<DisplayDevice>( list );
			}

			#endregion


			#region EnumDisplaySettingsEx

			/// <summary>Retrieves information about one of the graphics modes for a display device.
			/// To retrieve information for all the graphics modes for a display device, make a series of calls to this function.</summary>
			/// <param name="deviceName">
			/// A string that specifies the display device about which graphics mode the function will obtain information.
			/// This parameter is either null or a <code><see cref="DisplayDevice"/>.Identifier</code> returned from <see cref="EnumDisplayDevicesW"/>.
			/// A null value specifies the current display device on the computer that the calling thread is running on.
			/// </param>
			/// <param name="modeIndex">
			/// Indicates the type of information to be retrieved.
			/// This value can be a graphics mode index or one of the following values: ENUM_CURRENT_SETTINGS(-1) or ENUM_REGISTRY_SETTINGS(-2).
			/// Graphics mode indexes start at zero.
			/// To obtain information for all of a display device's graphics modes, make a series of calls to this function, as follows:
			/// <para>
			/// Set <paramref name="modeIndex"/> to zero for the first call, and increment <paramref name="modeIndex"/> by one for each subsequent call.
			/// Continue calling the function until the return value is zero.
			/// </para>
			/// When <paramref name="modeIndex"/> is set to zero, the operating system initializes and caches information about the display device.
			/// When <paramref name="modeIndex"/> is set to a nonzero value, the function returns the information that was cached the last time the function was called with <paramref name="modeIndex"/> set to zero.
			/// </param>
			/// <param name="devMode">
			/// A <see cref="DisplayDeviceMode"/> structure into which the function stores information about the specified graphics mode.
			/// Before calling EnumDisplaySettingsEx, set the size member to sizeof(DeviceMode), and set the driverExtra member to indicate the size, in bytes, of the additional space available to receive private driver data.
			/// This function will populate the fields member of the <paramref name="devMode"/> and one or more other members of the DeviceMode structure.
			/// To determine which members were set by the call to EnumDisplaySettingsEx, inspect the fields bitmask. Some of the fields typically populated by this function include:
			/// bitsPerPel, pelsWidth, pelsHeight, displayFlags, displayFrequency, position, displayOrientation.
			/// </param>
			/// <param name="options">See <see cref="EnumDisplaySettingsExOptions"/>.</param>
			/// <returns></returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			private static extern bool EnumDisplaySettingsExW(
				[In, Optional, MarshalAs( UnmanagedType.LPWStr )] string deviceName,
				[In] int modeIndex,
				[In, Out] ref DisplayDeviceMode devMode,
				[In] EnumDisplaySettingsExOptions options
			);
			// https://msdn.microsoft.com/en-us/library/dd162612(v=vs.85).aspx
			//BOOL EnumDisplaySettingsEx(
			//	_In_opt_ LPCWSTR lpszDeviceName,
			//	_In_ DWORD iModeNum,
			//	_Inout_ DEVMODEW* lpDevMode,
			//	_In_ DWORD dwFlags);


			/// <summary>Returns a read-only collection containing information about all the graphics modes for a display device.</summary>
			/// <param name="deviceName">A string that specifies the display device about which graphics mode the function will obtain information.
			/// <para>This parameter is either null or a <see cref="DisplayDevice.DeviceName"/> returned from <see cref="EnumDisplayDevicesW"/>.</para>
			/// A null value specifies the current display device on the computer that the calling thread is running on.</param>
			/// <param name="options">See <see cref="EnumDisplaySettingsExOptions"/>.</param>
			/// <returns>Returns a read-only collection containing information about all the graphics modes for a display device.</returns>
			internal static ReadOnlyDisplayDeviceModeCollection EnumDisplaySettingsEx( string deviceName, EnumDisplaySettingsExOptions options )
			{
				var modes = new List<DisplayDeviceMode>();
				var devMode = DisplayDeviceMode.Default;
				int modeIndex = 0;

				while( EnumDisplaySettingsExW( deviceName, modeIndex++, ref devMode, options ) )
				{
					if( !devMode.PelsWidth.HasValue || !devMode.PelsHeight.HasValue || !devMode.DisplayFrequency.HasValue )
						continue;
					// basic filtering: we don't need display modes whose width, height or frequency is missing.

					if( !devMode.BitsPerPel.HasValue || devMode.BitsPerPel.Value != 32 )
						continue;
					// 8, 16, and even 24 bpp modes are no more supported, since Windows 7 (to be confirmed).

					if( devMode.IsGrayscale.HasValue && devMode.IsGrayscale.Value )
						continue;
					// Only 32 bpp modes are supported, so grayscale modes should be safely ignored.

					modes.Add( devMode );
				}

				if( modes.Count > 1 )
					modes.Sort( ( DisplayDeviceMode mode, DisplayDeviceMode other ) => { return -mode.CompareTo( other ); } );

				return new ReadOnlyDisplayDeviceModeCollection( modes );
			}

			internal static DisplayDeviceMode GetCurrentDisplaySettingsEx( string deviceName, EnumDisplaySettingsExOptions options )
			{
				const int CurrentModeIndex = -1;
				var output = DisplayDeviceMode.Default;
				var success = EnumDisplaySettingsExW( deviceName, CurrentModeIndex, ref output, options );
				if( success )
					throw new InvalidOperationException();
				return output;
			}

			internal static DisplayDeviceMode GetRegistryDisplaySettingsEx( string deviceName, EnumDisplaySettingsExOptions options )
			{
				const int RegistryModeIndex = -2;
				var output = DisplayDeviceMode.Default;
				var success = EnumDisplaySettingsExW( deviceName, RegistryModeIndex, ref output, options );
				if( success )
					throw new InvalidOperationException();
				return output;
			}

			#endregion


			#region EnumDisplayMonitors

			/// <summary>Application-defined callback function for use with <see cref="EnumDisplayMonitors"/>.</summary>
			/// <param name="monitorHandle">A handle to the display monitor. This value will always be non-NULL.</param>
			/// <param name="deviceContextHandle">A handle to a device context.
			/// <para>The device context has color attributes that are appropriate for the display monitor identified by <paramref name="monitorHandle"/>.
			/// The clipping area of the device context is set to the intersection of the visible region of the device context identified by the deviceContextHandle parameter of <see cref="EnumDisplayMonitors"/>, the rectangle pointed to by the clipRect parameter of <see cref="EnumDisplayMonitors"/>, and the display monitor rectangle.
			/// </para>
			/// <para>This value is NULL if the deviceContextHandle parameter of <see cref="EnumDisplayMonitors"/> was NULL.</para>
			/// </param>
			/// <param name="monitor">A pointer to a <see cref="Rect"/> structure.
			/// <para>If <paramref name="deviceContextHandle"/> is non-NULL, this rectangle is the intersection of the clipping area of the device context identified by <paramref name="deviceContextHandle"/> and the display monitor rectangle. The rectangle coordinates are device-context coordinates.</para>
			/// <para>If <paramref name="deviceContextHandle"/> is NULL, this rectangle is the display monitor rectangle. The rectangle coordinates are virtual-screen coordinates.</para>
			/// </param>
			/// <param name="param">Application-defined data that <see cref="EnumDisplayMonitors"/> passes directly to the enumeration function.</param>
			/// <returns>To continue the enumeration, returns true. Otherwise returns false, which causes <see cref="EnumDisplayMonitors"/> to return false.</returns>
			[return: MarshalAs( UnmanagedType.Bool )]
			unsafe internal delegate bool MonitorEnumProc(
				[In] IntPtr monitorHandle,
				[In] IntPtr deviceContextHandle,
				[In] Rect* monitor,
				[In] IntPtr param
			);
			// https://msdn.microsoft.com/en-us/library/dd145061%28v=vs.85%29.aspx


			/// <summary>Enumerates display monitors (including invisible pseudo-monitors associated with the mirroring drivers) that intersect a region formed by the intersection of a specified clipping rectangle and the visible region of a device context.
			/// <para>EnumDisplayMonitors calls an application-defined <see cref="MonitorEnumProc"/> callback function once for each monitor that is enumerated.</para>
			/// Note that GetSystemMetrics (SM_CMONITORS) counts only the display monitors.
			/// </summary>
			/// <param name="deviceContextHandle">A handle to a display device context that defines the visible region of interest.
			/// <para>If this parameter is NULL, the deviceContextHandle parameter passed to the callback function will be NULL, and the visible region of interest is the virtual screen that encompasses all the displays on the desktop.</para>
			/// </param>
			/// <param name="clipRect">A pointer to a <see cref="Rect"/> structure that specifies a clipping rectangle.
			/// <para>The region of interest is the intersection of the clipping rectangle with the visible region specified by <paramref name="deviceContextHandle"/>.</para>
			/// If <paramref name="deviceContextHandle"/> is non-NULL, the coordinates of the clipping rectangle are relative to the origin of the <paramref name="deviceContextHandle"/>.
			/// If <paramref name="deviceContextHandle"/> is NULL, the coordinates are virtual-screen coordinates.
			/// <para>This parameter can be NULL if you don't want to clip the region specified by <paramref name="deviceContextHandle"/>.</para>
			/// </param>
			/// <param name="callback">An application-defined <see cref="MonitorEnumProc"/> callback function.</param>
			/// <param name="data">Application-defined data that EnumDisplayMonitors passes directly to the <see cref="MonitorEnumProc"/> function.</param>
			/// <returns>Returns true on success, otherwise returns false (ie: the callback function interrupted the enumeration).</returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			unsafe internal static extern bool EnumDisplayMonitors(
				[In] IntPtr deviceContextHandle,
				[In] Rect* clipRect,
				[In] MonitorEnumProc callback,
				[In] IntPtr data
			);
			// https://msdn.microsoft.com/en-us/library/dd162610%28v=vs.85%29.aspx

			#endregion

		}

		

		private static readonly List<DisplayAdapter> all = new List<DisplayAdapter>( MaxAdapterCount );


		private static void RefreshAdaptersCache()
		{
			DisplayAdapter primary = null;
			if( all.Count > 0 )
				primary = all[ 0 ];
			all.Clear();

			foreach( var adapter in NativeMethods.EnumDisplayDevices( null, false ) )
			{
				if( ( (AdapterStates)adapter.State ).HasFlag( AdapterStates.PrimaryDevice ) )
				{
					if( primary == null )
					{
						primary = new DisplayAdapter( adapter );
						all.Insert( 0, primary );
					}
					else
					{
						all.Insert( 0, primary );
						primary.Reset( adapter );
					}
				}
				else
					all.Add( new DisplayAdapter( adapter ) );
			}
		}


		/// <summary>Gets a read-only collection containing all available display adapters.</summary>
		public static ReadOnlyDisplayAdapterCollection All
		{
			get
			{
				RefreshAdaptersCache();
				return new ReadOnlyDisplayAdapterCollection( all );
			}
		}


		/// <summary>Returns a <see cref="DisplayAdapter"/> given its device name.</summary>
		/// <param name="deviceName">The name of the display adapter device.</param>
		/// <returns>Returns the <see cref="DisplayAdapter"/> whose device name matches the specified <paramref name="deviceName"/>, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static DisplayAdapter FindByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			foreach( var adapter in All )
				if( deviceName.Equals( adapter.DeviceName, StringComparison.Ordinal ) )
					return adapter;

			return null;
		}


		/// <summary>Returns a read-only collection containing the handle (HMONITOR) of all connected monitors.</summary>
		/// <returns>Returns a read-only collection containing the handle (HMONITOR) of all connected monitors.</returns>
		unsafe private static ReadOnlyCollection<IntPtr> GetMonitorHandles()
		{
			var handles = new List<IntPtr>( 1 );
			
			var callback = new NativeMethods.MonitorEnumProc(
				( IntPtr hMonitor, IntPtr hDeviceContext, Rect* monitor, IntPtr param ) =>
				{
					handles.Add( hMonitor );
					return true;
				}
			);

			if( !NativeMethods.EnumDisplayMonitors( IntPtr.Zero, null, callback, IntPtr.Zero ) )
				handles.Clear();

			return new ReadOnlyCollection<IntPtr>( handles );
		}


		#endregion


		/// <summary>Private constructor.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure containing information about the display adapter.</param>
		private DisplayAdapter( DisplayDevice displayDevice )
			: base( displayDevice )
		{
		}



		/// <summary>Gets the device id associated with this adapter.</summary>
		new public string DeviceId { get { return base.DeviceId; } }


		/// <summary>Gets the device name of this adapter.</summary>
		public sealed override string DeviceName { get { return base.DeviceName; } }


		/// <summary>Gets a description (friendly name) of this adapter.</summary>
		public sealed override string Description { get { return base.Description; } }


		/// <summary>Gets a value indicating the state of the display adapter.</summary>
		new public AdapterStates State { get { return (AdapterStates)base.State; } }


		/// <summary>Gets a read-only collection containing all (32 bpp) display modes supported by both this <see cref="DisplayAdapter"/> and the <see cref="Monitors"/>.</summary>
		public ReadOnlyDisplayDeviceModeCollection DisplayModes { get { return NativeMethods.EnumDisplaySettingsEx( this.DeviceName, EnumDisplaySettingsExOptions.None ); } }


		/// <summary>Gets a read-only collection containing all monitors currently connected to this <see cref="DisplayAdapter"/>.</summary>
		public ReadOnlyDisplayMonitorCollection Monitors
		{
			get
			{
				var handles = new List<IntPtr>( GetMonitorHandles() );
				var invalidHandles = new List<IntPtr>();
				var infos = new List<MonitorInfoEx>( 1 );

				foreach( var handle in handles )
				{
					var monitorInfo = DisplayMonitor.GetMonitorInfo( handle );
					if( !monitorInfo.DeviceName.Equals( this.DeviceName, StringComparison.Ordinal ) )
						invalidHandles.Add( handle );
					else
						infos.Add( monitorInfo );
				}

				foreach( var invalidHandle in invalidHandles )
					handles.Remove( invalidHandle );
				invalidHandles.Clear();


				var monitors = new List<DisplayMonitor>( Math.Max( 1, handles.Count ) );
				int monitorIndex = 0;

				DisplayMonitor displayMonitor;
				foreach( var monitorDevice in NativeMethods.EnumDisplayDevices( this.DeviceName, true ) )
				{
					if( monitorIndex < handles.Count )
						displayMonitor = new DisplayMonitor( monitorDevice, handles[ monitorIndex ], infos[ monitorIndex ] );
					else
						displayMonitor = new DisplayMonitor( monitorDevice, IntPtr.Zero );
					monitors.Add( displayMonitor );
					++monitorIndex;
				}
				
				return new ReadOnlyDisplayMonitorCollection( monitors );
			}
		}


		/// <summary>Returns a hash code for this <see cref="DisplayAdapter"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="DisplayAdapter"/>.</returns>
		public sealed override int GetHashCode()
		{
			return base.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayAdapter"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DisplayAdapter"/> which equals this <see cref="DisplayAdapter"/>, otherwise returns false.</returns>
		public sealed override bool Equals( object obj )
		{
			return base.Equals( obj as DisplayAdapter );
		}

	}

}