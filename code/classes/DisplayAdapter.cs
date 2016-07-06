using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Graphics
{

	/// <summary>A display adapter.</summary>
	public sealed class DisplayAdapter : DisplayDeviceBase
	{

		/// <summary>Defines the maximum number of display adapters supported by the system: 16.</summary>
		public const int MaxAdapterCount = 16;


		#region Native

		/// <summary>Enumerates flags used by EnumDisplayDevices.</summary>
		[Flags]
		private enum EnumDisplayDevicesOptions : int
		{
			None = 0x00000000,
			GetDeviceInterfaceName = 0x00000001
		}


		/// <summary>Enumerates flags used by EnumDisplaySettingsEx.</summary>
		[Flags]
		private enum EnumDisplaySettingsExOptions : int
		{

			/// <summary>The EnumDisplaySettingsEx function will return all graphics modes supported by the adapter driver and the monitor, that have the same orientation as the one currently set for the requested display.</summary>
			None = 0x00000000,

			/// <summary>If set, the EnumDisplaySettingsEx function will return all graphics modes reported by the adapter driver, regardless of monitor capabilities.
			/// <para>Otherwise, it will only return modes that are compatible with current monitors.</para>
			/// </summary>
			RawMode = 0x00000001,

			/// <summary>If set, the EnumDisplaySettingsEx function will return graphics modes in all orientations.
			/// <para>Otherwise, it will only return modes that have the same orientation as the one currently set for the requested display.</para>
			/// </summary>
			RotatedMode = 0x00000002

		}


		private enum MonitorFromWindowOption : int
		{

			/// <summary>Causes the method to return <see cref="IntPtr.Zero"/>.</summary>
			DefaultToNull,

			/// <summary>Causes the method to return a handle to the primary display monitor.</summary>
			DefaultToPrimary,

			/// <summary>Causes the method to return a handle to the display monitor that is nearest to the window.</summary>
			DefaultToNearest

		}


		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{

			private const string LibraryName = "User32.dll";


			/// <summary>Retrieves information about the display devices in the current session.
			/// <para>To query all display devices in the current session, call this function in a loop, starting with <paramref name="deviceIndex"/> set to 0, and incrementing <paramref name="deviceIndex"/> until the function fails.
			/// To select all display devices in the desktop, use only the display devices that have the <see cref="AdapterStateIndicators.AttachedToDesktop"/> flag in the <see cref="DisplayDevice"/> structure.
			/// To get information on the display adapter, call this function with <paramref name="deviceName"/> set to null.
			/// </para>
			/// <para>To obtain information on a display monitor, first call this function with <paramref name="deviceName"/> set to null.
			/// Then call this function with <paramref name="deviceName"/> set to <see cref="DisplayDevice.DeviceName"/> from the first call to EnumDisplayDevices and with <paramref name="deviceIndex"/> set to zero.
			/// </para>
			/// <para>To query all monitor devices associated with an adapter, call EnumDisplayDevices in a loop with <paramref name="deviceName"/> set to the adapter name, <paramref name="deviceIndex"/> set to start at 0, and <paramref name="deviceIndex"/> set to increment until the function fails.
			/// Note that <see cref="DisplayDevice.DeviceName"/> changes with each call for monitor information, so you must save the adapter name.
			/// The function fails when there are no more monitors for the adapter.
			/// </para>
			/// </summary>
			/// <param name="deviceName">The device name.
			/// <para>If null, the function returns information for the display adapter(s) on the machine, based on <paramref name="deviceIndex"/>.</para>
			/// </param>
			/// <param name="deviceIndex">An index value that specifies the display device of interest.
			/// <para>The operating system identifies each display device in the current session with an index value.</para>
			/// The index values are consecutive integers, starting at 0. If the current session has three display devices, for example, they are specified by the index values 0, 1, and 2.
			/// </param>
			/// <param name="displayDevice">A pointer to a <see cref="DisplayDevice"/> structure that receives information about the display device specified by <paramref name="deviceIndex"/>.
			/// <para>Before calling this function, you must initialize the <code>cb</code> member of <paramref name="displayDevice"/> to the size, in bytes, of <see cref="DisplayDevice"/>.</para>
			/// </param>
			/// <param name="options">Set this flag to <see cref="EnumDisplayDevicesOptions.GetDeviceInterfaceName"/> to retrieve the device interface name for GUID_DEVINTERFACE_MONITOR, which is registered by the operating system on a per monitor basis.
			/// <para>The value is placed in the <see cref="DisplayDevice.DeviceId"/> member of the returned <paramref name="displayDevice"/> structure.</para>
			/// The resulting device interface name can be used with SetupAPI functions and serves as a link between GDI monitor devices and SetupAPI monitor devices.
			/// </param>
			/// <returns>Returns true on success, otherwise false (<paramref name="deviceIndex"/> is out-of-range).</returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/dd162609%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool EnumDisplayDevicesW(
				[In, MarshalAs( UnmanagedType.LPWStr )] string deviceName,
				[In] int deviceIndex,
				[In, Out] ref DisplayDevice displayDevice,
				[In] EnumDisplayDevicesOptions options
			);


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
			/// <remarks>https://msdn.microsoft.com/en-us/library/dd162612(v=vs.85).aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			private static extern bool EnumDisplaySettingsExW(
				[In, MarshalAs( UnmanagedType.LPWStr )] string deviceName,
				[In] int modeIndex,
				[In, Out] ref DisplayDeviceMode devMode,
				[In] EnumDisplaySettingsExOptions options
			);
			//BOOL EnumDisplaySettingsEx(
			//	_In_opt_ LPCWSTR lpszDeviceName,
			//	_In_ DWORD iModeNum,
			//	_Inout_ DEVMODEW* lpDevMode,
			//	_In_ DWORD dwFlags);


			/// <summary>Returns a read-only collection containing information about all the graphics modes for a display device.</summary>
			/// <param name="deviceName">A string that specifies the display device about which graphics modes the function will obtain information.
			/// <para>This parameter is either null or a <see cref="DisplayDevice.DeviceName"/> returned from <see cref="EnumDisplayDevicesW"/>.</para>
			/// A null value specifies the current display device on the computer that the calling thread is running on.</param>
			/// <param name="options">One or more <see cref="EnumDisplaySettingsExOptions"/>.</param>
			/// <returns>Returns a read-only collection containing information about all the graphics modes for a display device.</returns>
			internal static ReadOnlyDisplayDeviceModeCollection EnumDisplaySettingsEx( string deviceName, EnumDisplaySettingsExOptions options )
			{
				var modes = new List<DisplayDeviceMode>();
				var devMode = DisplayDeviceMode.Default;
				var modeIndex = 0;

				while( EnumDisplaySettingsExW( deviceName, modeIndex++, ref devMode, options ) )
				{
					if( !devMode.Width.HasValue || !devMode.Height.HasValue || !devMode.DisplayFrequency.HasValue )
						continue;
					// basic filtering: we don't need display modes whose width, height or frequency is missing.

					if( !devMode.BitsPerPixel.HasValue || devMode.BitsPerPixel.Value != 32 )
						continue;
					// 8, 16 and 24 bpp modes are no more supported, since Windows 7.

					if( devMode.IsGrayscale.HasValue && devMode.IsGrayscale.Value )
						continue;
					// Only 32 bpp modes are supported, so grayscale modes should be safely ignored.

					modes.Add( devMode );
				}

				if( modes.Count > 1 )
					modes.Sort( ( DisplayDeviceMode mode, DisplayDeviceMode other ) => { return -mode.CompareTo( other ); } );

				return new ReadOnlyDisplayDeviceModeCollection( modes );
			}

			
			// TODO - do they work with monitor device names ?

			/// <summary>Returns information about the current display mode of a display adapter.
			/// <para>If the adapter is not attached to the desktop, this method returns <see cref="DisplayDeviceMode.Default"/>.</para>
			/// </summary>
			/// <param name="deviceName">A string that specifies the display device about which graphics mode the function will obtain information.
			/// <para>This parameter is either null or a <see cref="DisplayDevice.DeviceName"/> returned from <see cref="EnumDisplayDevicesW"/>.</para>
			/// A null value specifies the current display device on the computer that the calling thread is running on.</param>
			/// <param name="options">One or more <see cref="EnumDisplaySettingsExOptions"/>.</param>
			/// <returns>Returns information about the current display mode of a display adapter.</returns>
			internal static DisplayDeviceMode GetCurrentDisplaySettingsEx( string deviceName, EnumDisplaySettingsExOptions options )
			{
				var output = DisplayDeviceMode.Default;
				if( !EnumDisplaySettingsExW( deviceName, -1, ref output, options ) )
					output = DisplayDeviceMode.Default;
				return output;
			}

			/// <summary>Returns information about the display mode of a display adapter, as stored in the registry.</summary>
			/// <param name="deviceName">A string that specifies the display device about which graphics mode the function will obtain information.
			/// <para>This parameter is either null or a <see cref="DisplayDevice.DeviceName"/> returned from <see cref="EnumDisplayDevicesW"/>.</para>
			/// A null value specifies the current display device on the computer that the calling thread is running on.</param>
			/// <param name="options">One or more <see cref="EnumDisplaySettingsExOptions"/>.</param>
			/// <returns>Returns information about the display mode of a display adapter, as stored in the registry.</returns>
			internal static DisplayDeviceMode GetRegistryDisplaySettingsEx( string deviceName, EnumDisplaySettingsExOptions options )
			{
				var output = DisplayDeviceMode.Default;
				if( !EnumDisplaySettingsExW( deviceName, -2, ref output, options ) )
					output = DisplayDeviceMode.Default;
				return output;
			}

			#endregion EnumDisplaySettingsEx


			#region EnumDisplayMonitors (unsafe)

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
			internal unsafe delegate bool MonitorEnumProc(
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
			/// <remarks>https://msdn.microsoft.com/en-us/library/dd162610%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal unsafe static extern bool EnumDisplayMonitors(
				[In] IntPtr deviceContextHandle,
				[In] Rect* clipRect,
				[In] MonitorEnumProc callback,
				[In] IntPtr data
			);

			#endregion EnumDisplayMonitors (unsafe)


			/// <summary>Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.</summary>
			/// <param name="windowHandle">A handle to the window of interest.</param>
			/// <param name="option">Determines the function's return value if the window does not intersect any display monitor.</param>
			/// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.
			/// <para>If the window does not intersect a display monitor, the return value depends on the value of <paramref name="option"/>.</para>
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd145064%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern IntPtr MonitorFromWindow(
				[In] IntPtr windowHandle,
				[In] MonitorFromWindowOption option
			);

		}

		#endregion Native


		#region Static

		private static DisplayMonitor primaryDisplayMonitor;


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

			var deviceIndex = 0;
			var device = DisplayDevice.Default;
			var options = getMonitorDeviceInterfaceName ? EnumDisplayDevicesOptions.GetDeviceInterfaceName : EnumDisplayDevicesOptions.None;

			while( NativeMethods.EnumDisplayDevicesW( deviceName, deviceIndex++, ref device, options ) )
				list.Add( device );

			return new ReadOnlyCollection<DisplayDevice>( list );
		}


		/// <summary>Returns a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.</summary>
		/// <param name="windowHandle">A handle to the window of interest.</param>
		/// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.
		/// <para>If the window does not intersect a display monitor, the return value is the nearest monitor.</para>
		/// </returns>
		/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd145064%28v=vs.85%29.aspx</remarks>
		internal static IntPtr GetMonitorHandleFromWindow( IntPtr windowHandle )
		{
			return NativeMethods.MonitorFromWindow( windowHandle, MonitorFromWindowOption.DefaultToNearest );
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


		/// <summary>Gets the primary <see cref="DisplayMonitor"/>.</summary>
		internal static DisplayMonitor PrimaryMonitor { get { return primaryDisplayMonitor; } }


		/// <summary>Raised when the <see cref="PrimaryMonitor"/> changed.</summary>
		internal static event EventHandler PrimaryMonitorChanged;

		#endregion Static


		
		private Dictionary<string, DisplayMonitor> monitorsByDeviceName;
		private DisplayDeviceMode currentMode;



		internal DisplayAdapter( DisplayDevice displayDevice )
			: base( displayDevice )
		{
			monitorsByDeviceName = new Dictionary<string, DisplayMonitor>();
			currentMode = NativeMethods.GetCurrentDisplaySettingsEx( base.DeviceIdentifier, EnumDisplaySettingsExOptions.None );
		}



		internal sealed override void Refresh( DisplayDevice displayDevice )
		{
			base.Refresh( displayDevice );
			this.RefreshMonitors();

			var mode = NativeMethods.GetCurrentDisplaySettingsEx( base.DeviceIdentifier, EnumDisplaySettingsExOptions.None );
			if( !mode.Equals( currentMode ) )
			{
				currentMode = mode;
				
				var currentModeChangedEvent = this.CurrentModeChanged;
				if( currentModeChangedEvent != null )
					currentModeChangedEvent.Invoke( this, EventArgs.Empty );
			}
		}


		/// <summary>Gets the device id of this <see cref="DisplayAdapter"/>.</summary>
		new public string DeviceId { get { return base.DeviceId; } }


		/// <summary>Gets a value indicating the state of this <see cref="DisplayAdapter"/>.</summary>
		public AdapterStateIndicators State { get { return (AdapterStateIndicators)base.RawState; } }


		/// <summary>Gets a read-only collection containing all (32 bpp) display modes supported by both this <see cref="DisplayAdapter"/> and its <see cref="Monitors"/>.</summary>
		public ReadOnlyDisplayDeviceModeCollection DisplayModes { get { return NativeMethods.EnumDisplaySettingsEx( base.DeviceIdentifier, EnumDisplaySettingsExOptions.None ); } }


		private void RefreshMonitors()
		{
			var deviceName = base.DeviceIdentifier;
			
			var allHandles = GetMonitorHandles();
			var handles = new List<IntPtr>();
			int h;
			for( h = 0; h < allHandles.Count; ++h )
			{
				var info = DisplayMonitor.GetMonitorInfo( allHandles[ h ] );
				if( deviceName.Equals( info.AdapterDeviceName, StringComparison.Ordinal ) )
					handles.Add( allHandles[ h ] );
			}

			var removedMonitors = new List<string>( monitorsByDeviceName.Keys );
			var addedMonitors = new List<string>();
			var primaryMonitorChanged = false;

			DisplayMonitor displayMonitor;
			var monitors = EnumDisplayDevices( deviceName, true );

			h = 0;
			for( var m = 0; m < monitors.Count; ++m )
			{
				var monitor = monitors[ m ];

				IntPtr handle;
				if( ( monitor.State & (int)MonitorStateIndicators.Active ) == (int)MonitorStateIndicators.Active && ( h < handles.Count ) )
				{
					handle = handles[ h ];	// FIXME - this is not the right handle when topology is Clone !
					++h;
				}
				else
					handle = IntPtr.Zero;

				if( monitorsByDeviceName.TryGetValue( monitor.DeviceName, out displayMonitor ) )
				{
					displayMonitor.Refresh( monitor );
					displayMonitor.Handle = handle;
					removedMonitors.Remove( monitor.DeviceName );
				}
				else
				{
					displayMonitor = new DisplayMonitor( monitor, handle );
					monitorsByDeviceName.Add( monitor.DeviceName, displayMonitor );
					addedMonitors.Add( monitor.DeviceName );
				}

				if( displayMonitor.IsPrimary && ( primaryDisplayMonitor != displayMonitor ) )
				{
					primaryMonitorChanged = ( primaryDisplayMonitor != null );
					primaryDisplayMonitor = displayMonitor;
				}
			}


			while( addedMonitors.Count > 0 )
			{
				this.OnMonitorConnectedOrDisconnected( addedMonitors[ 0 ], true );
				addedMonitors.RemoveAt( 0 );
			}


			while( removedMonitors.Count > 0 )
			{
				deviceName = removedMonitors[ 0 ];
				removedMonitors.RemoveAt( 0 );

				displayMonitor = monitorsByDeviceName[ deviceName ];
				monitorsByDeviceName.Remove( deviceName );

				displayMonitor.OnDisconnected();
				this.OnMonitorConnectedOrDisconnected( deviceName, false );
			}


			if( primaryMonitorChanged )
			{
				var primaryMonitorChangedEvent = PrimaryMonitorChanged;
				if( primaryMonitorChangedEvent != null )
					primaryMonitorChangedEvent.Invoke( this, EventArgs.Empty );
			}
		}


		/// <summary>Gets a read-only collection containing all monitors currently connected to this <see cref="DisplayAdapter"/>.</summary>
		public ReadOnlyDisplayMonitorCollection Monitors
		{
			get
			{
				this.RefreshMonitors();

				var list = new List<DisplayMonitor>( monitorsByDeviceName.Values );
				return new ReadOnlyDisplayMonitorCollection( list );
			}
		}


		/// <summary>Gets the current display mode of this <see cref="DisplayAdapter"/>.
		/// <para>Requires the adapter to be attached to the desktop.</para>
		/// </summary>
		public DisplayDeviceMode CurrentMode { get { return currentMode; } }


		/// <summary>Gets the display mode associated with this <see cref="DisplayAdapter"/>, as stored in the Windows registry.</summary>
		public DisplayDeviceMode RegistryMode { get { return NativeMethods.GetRegistryDisplaySettingsEx( base.DeviceIdentifier, EnumDisplaySettingsExOptions.None ); } }


		#region Events

		/// <summary>Raised when this <see cref="DisplayAdapter"/> is removed from the system.</summary>
		public event EventHandler Removed;

		/// <summary>Raises the <see cref="Removed"/> event.</summary>
		internal void OnRemoved()
		{
			foreach( var monitor in monitorsByDeviceName.Values )
				monitor.OnDisconnected();
			
			monitorsByDeviceName.Clear();
			
			var removedEvent = this.Removed;
			if( removedEvent != null )
				removedEvent.Invoke( this, EventArgs.Empty );
		}


		/// <summary>Raised when the <see cref="CurrentMode"/> of this <see cref="DisplayAdapter"/> changed.</summary>
		public event EventHandler CurrentModeChanged;

		
		/// <summary>Raised when a display monitor is connected to this <see cref="DisplayAdapter"/>.</summary>
		public event EventHandler<DisplayDeviceEventArgs> MonitorConnected;

		/// <summary>Raised when a display monitor is disconnected from this <see cref="DisplayAdapter"/>.</summary>
		public event EventHandler<DisplayDeviceEventArgs> MonitorDisconnected;

		/// <summary>Raises the <see cref="MonitorConnected"/> or <see cref="MonitorDisconnected"/> event.</summary>
		/// <param name="deviceIdentifier">The device name of the display monitor of interest.</param>
		/// <param name="connected">True to raise the <see cref="MonitorConnected"/> event, false to raise the <see cref="MonitorDisconnected"/> event.</param>
		private void OnMonitorConnectedOrDisconnected( string deviceIdentifier, bool connected )
		{
			var eventHandler = ( connected ? this.MonitorConnected : this.MonitorDisconnected );
			if( eventHandler != null )
				eventHandler.Invoke( this, new DisplayDeviceEventArgs( deviceIdentifier ) );
		}

		#endregion Events

	}

}