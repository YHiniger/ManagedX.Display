using System;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Display
{

	// NOTE - since DisplayAdapter uses this class, DisplayMonitor must not refer to DisplayAdapter.


	/// <summary>Contains information about a display monitor device.</summary>
	public sealed class DisplayMonitor : DisplayDeviceBase
	{

		#region Static methods

		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Retrieves information about a display monitor.</summary>
			/// <param name="monitorHandle">A handle to the display monitor of interest.</param>
			/// <param name="info">A (pointer to a) MonitorInfo or a <see cref="MonitorInfoEx"/> structure that receives information about the specified display monitor.
			/// <para>You must set the cbSize member of the structure to sizeof(MONITORINFO) or sizeof(<see cref="MonitorInfoEx"/>) before calling this function. Doing so lets the function determine the type of structure you are passing to it.</para>
			/// The <see cref="MonitorInfoEx"/> structure is a superset of the MONITORINFO structure. It has one additional member: a string that contains a name for the display monitor. Most applications have no use for a display monitor name, and so can save some bytes by using a MONITORINFO structure.
			/// </param>
			/// <returns>Returns false on failure, otherwise returns true.</returns>
			[DllImport( LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetMonitorInfoW(
				[In] IntPtr monitorHandle,
				[In, Out] ref MonitorInfoEx info
			);
			// https://msdn.microsoft.com/en-us/library/dd144901%28v=vs.85%29.aspx
			//BOOL WINAPI GetMonitorInfoW(
			//	_In_	HMONITOR hMonitor,
			//	_Inout_ LPMONITORINFO lpmi
			//);

		}


		/// <summary>Retrieves information about a display monitor.</summary>
		/// <param name="monitorHandle">A handle (HMONITOR) to the display monitor of interest.</param>
		/// <returns>Returns a <see cref="MonitorInfoEx"/> structure containing information about the display monitor associated with the specified <paramref name="monitorHandle"/>.</returns>
		public static MonitorInfoEx GetMonitorInfo( IntPtr monitorHandle )
		{
			var info = MonitorInfoEx.Default;
			if( monitorHandle == IntPtr.Zero || !SafeNativeMethods.GetMonitorInfoW( monitorHandle, ref info ) )
				info = MonitorInfoEx.Empty;
			return info;
		}

		#endregion


		private IntPtr handle;
		private MonitorInfoEx info;


		#region Constructors

		// The constructors are internal since they're only used by DisplayAdapter:

		/// <summary>Initializes a new <see cref="DisplayMonitor"/> object.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure.</param>
		/// <param name="monitorHandle">A handle (HMONITOR) to the monitor.</param>
		/// <param name="monitorInfo">A <see cref="MonitorInfoEx"/> structure containing information about the monitor; can be obtained with <see cref="GetMonitorInfo"/>.</param>
		/// <exception cref="ArgumentException"/>
		internal DisplayMonitor( DisplayDevice displayDevice, IntPtr monitorHandle, MonitorInfoEx monitorInfo )
			: base( displayDevice )
		{
			handle = monitorHandle;
			info = monitorInfo;
		}


		/// <summary>Initializes a new <see cref="DisplayMonitor"/> object.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure.</param>
		/// <param name="monitorHandle">A handle (HMONITOR) to the monitor.</param>
		/// <exception cref="ArgumentException"/>
		internal DisplayMonitor( DisplayDevice displayDevice, IntPtr monitorHandle )
			: this( displayDevice, monitorHandle, GetMonitorInfo( monitorHandle ) )
		{
		}

		#endregion


		/// <summary>Gets the device name of this monitor.</summary>
		public sealed override string DeviceName { get { return base.DeviceName; } }

		
		/// <summary>Gets a description (friendly name) of this monitor.</summary>
		public sealed override string Description { get { return base.Description; } }


		/// <summary>Gets the device path associated with this monitor.
		/// <para>This string can be used with DisplayConfig and SetupAPI (not implemented).</para>
		/// </summary>
		public string DevicePath { get { return base.DeviceId; } }


		/// <summary>Gets the handle (HMONITOR) associated with this monitor.</summary>
		public IntPtr Handle { get { return handle; } }


		/// <summary>Gets information about this monitor.</summary>
		public MonitorInfoEx Info { get { return info; } }


		/// <summary>Gets a value indicating the monitor state.</summary>
		new public MonitorStates State { get { return (MonitorStates)base.State; } }


		/// <summary>Returns the hash code of the underlying <see cref="DisplayDevice"/> structure.</summary>
		/// <returns>Returns the hash code of the underlying <see cref="DisplayDevice"/> structure.</returns>
		public sealed override int GetHashCode()
		{
			return base.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayMonitor"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object represents a <see cref="DisplayMonitor"/> equivalent to this <see cref="DisplayMonitor"/>, otherwise returns false.</returns>
		public sealed override bool Equals( object obj )
		{
			if( obj is DisplayDevice )
				return base.Equals( (DisplayDevice)obj );
			
			return base.Equals( obj as DisplayMonitor );
		}

	}

}