using System;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Display
{

	/// <summary>A display monitor.</summary>
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
			/// <remarks>https://msdn.microsoft.com/en-us/library/dd144901%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetMonitorInfoW(
				[In] IntPtr monitorHandle,
				[In, Out] ref MonitorInfoEx info
			);

		}


		/// <summary>Retrieves information about a display monitor.</summary>
		/// <param name="monitorHandle">A handle (HMONITOR) to the display monitor of interest.</param>
		/// <returns>Returns a <see cref="MonitorInfoEx"/> structure containing information about the display monitor associated with the specified <paramref name="monitorHandle"/>.</returns>
		internal static MonitorInfoEx GetMonitorInfo( IntPtr monitorHandle )
		{
			var info = MonitorInfoEx.Default;
			if( monitorHandle == IntPtr.Zero || !SafeNativeMethods.GetMonitorInfoW( monitorHandle, ref info ) )
				info = MonitorInfoEx.Empty;
			return info;
		}

		#endregion Static methods



		private IntPtr handle;
		private MonitorInfoEx info;
		private string moreFriendlyName;	// comes from DisplayConfig, if supported (and used, hence the need of a DisplayDeviceManager)



		/// <summary>Initializes a new <see cref="DisplayMonitor"/> instance.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure.</param>
		/// <param name="monitorHandle">A handle (HMONITOR) to the monitor.</param>
		internal DisplayMonitor( DisplayDevice displayDevice, IntPtr monitorHandle )
			: base( displayDevice )
		{
			info = GetMonitorInfo( handle = monitorHandle );
		}



		internal sealed override void Refresh( DisplayDevice displayDevice )
		{
			base.Refresh( displayDevice );
			info = GetMonitorInfo( handle );
		}


		/// <summary>Raised when this <see cref="DisplayMonitor"/> is disconnected.</summary>
		public event EventHandler Disconnected;

		/// <summary>Raises the <see cref="Disconnected"/> event.</summary>
		internal void OnDisconnected()
		{
			var disconnectedEvent = this.Disconnected;
			if( disconnectedEvent != null )
				disconnectedEvent.Invoke( this, EventArgs.Empty );
		}


		/// <summary>Gets the friendly name of this <see cref="DisplayMonitor"/>.
		/// <para>On Windows Vista, this is always "Generic PnP monitor".</para>
		/// </summary>
		public sealed override string DisplayName
		{
			get
			{
				if( moreFriendlyName != null )
					return string.Copy( moreFriendlyName );
				return base.DisplayName;
			}
			internal set { moreFriendlyName = value; }
		}


		/// <summary>Gets a value indicating the state of this <see cref="DisplayMonitor"/>.</summary>
		public MonitorStateIndicators State { get { return (MonitorStateIndicators)base.RawState; } }


		/// <summary>Gets the device path associated with this <see cref="DisplayMonitor"/>.
		/// <para>This string can be used with DisplayConfig and SetupAPI (not implemented).</para>
		/// </summary>
		public string DevicePath { get { return base.DeviceId; } }


		/// <summary>Gets the handle (HMONITOR) associated with this <see cref="DisplayMonitor"/>.</summary>
		public IntPtr Handle { get { return handle; } }


		#region MonitorInfoEx properties

		/// <summary>Gets a value indicating whether this <see cref="DisplayMonitor"/> is the primary monitor.</summary>
		public bool IsPrimary { get { return info.IsPrimary; } }

		
		/// <summary>Gets a <see cref="Rect"/> representing the monitor screen, expressed in virtual screen coordinates.</summary>
		public Rect Screen { get { return info.Monitor; } }


		/// <summary>Gets a <see cref="Rect"/> representing the monitor's workspace, expressed in virtual screen coordinates.</summary>
		public Rect Workspace { get { return info.Workspace; } }

		
		/// <summary>Gets the device name of the display adapter this <see cref="DisplayMonitor"/> is connected to.</summary>
		public string AdapterDeviceName { get { return info.AdapterDeviceName; } }

		#endregion MonitorInfoEx properties
	
	}

}