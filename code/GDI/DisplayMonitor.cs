using System;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Graphics
{

	/// <summary>Represents a GDI (Graphics Device Interface) display monitor.</summary>
	public sealed class DisplayMonitor : DisplayDeviceBase
	{

		#region Native

		private enum MonitorFromWindowOption : int
		{

			/// <summary>Causes the method to return <see cref="IntPtr.Zero"/>.</summary>
			DefaultToNull,

			/// <summary>Causes the method to return a handle to the primary display monitor.</summary>
			DefaultToPrimary,

			/// <summary>Causes the method to return a handle to the display monitor that is nearest to the window.</summary>
			DefaultToNearest

		}


		[Win32.Source( "WinUser.h" )]
		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";

			
			/// <summary>Retrieves information about a display monitor.</summary>
			/// <param name="monitorHandle">A handle to the display monitor of interest.</param>
			/// <param name="info">A (pointer to a) MonitorInfo or a <see cref="MonitorInfoEx"/> structure that receives information about the specified display monitor.
			/// <para>You must set the cbSize member of the structure to sizeof(MONITORINFO) or sizeof(<see cref="MonitorInfoEx"/>) before calling this function. Doing so lets the function determine the type of structure you are passing to it.</para>
			/// The <see cref="MonitorInfoEx"/> structure is a superset of the MONITORINFO structure. It has one additional member: a string that contains a name for the display monitor. Most applications have no use for a display monitor name, and so can save some bytes by using a MONITORINFO structure.
			/// </param>
			/// <returns>Returns false on failure, otherwise returns true.</returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/dd144901%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetMonitorInfoW(
				[In] IntPtr monitorHandle,
				[In, Out] ref MonitorInfoEx info
			);


			/// <summary>Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.</summary>
			/// <param name="windowHandle">A handle to the window of interest.</param>
			/// <param name="option">Determines the function's return value if the window does not intersect any display monitor.</param>
			/// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.
			/// <para>If the window does not intersect a display monitor, the return value depends on the value of <paramref name="option"/>.</para>
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd145064%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true )]
			internal static extern IntPtr MonitorFromWindow(
				[In] IntPtr windowHandle,
				[In] MonitorFromWindowOption option
			);

		}

		#endregion Native


		#region Static

		/// <summary>Retrieves information about a display monitor.</summary>
		/// <param name="monitorHandle">A handle (HMONITOR) to the display monitor of interest.</param>
		/// <returns>Returns a <see cref="MonitorInfoEx"/> structure containing information about the display monitor associated with the specified <paramref name="monitorHandle"/>.</returns>
		internal static MonitorInfoEx GetInfo( IntPtr monitorHandle )
		{
			var info = MonitorInfoEx.Default;
			if( monitorHandle == IntPtr.Zero || !SafeNativeMethods.GetMonitorInfoW( monitorHandle, ref info ) )
				info = MonitorInfoEx.Empty;
			return info;
		}


		/// <summary>Returns a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.</summary>
		/// <param name="windowHandle">A handle to the window of interest.</param>
		/// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.
		/// <para>If the window does not intersect a display monitor, the return value is the nearest monitor.</para>
		/// </returns>
		/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd145064%28v=vs.85%29.aspx</remarks>
		internal static IntPtr GetMonitorHandleFromWindow( IntPtr windowHandle )
		{
			return SafeNativeMethods.MonitorFromWindow( windowHandle, MonitorFromWindowOption.DefaultToNearest );
		}

		#endregion Static



		private readonly DisplayAdapter adapter;
		private IntPtr handle;
		private MonitorInfoEx info;
		// Provided by DisplayConfig:
		internal VideoOutputTechnology videoOutputTechnology;
		private DisplayRotation orientation;
		internal Scaling scaling;
		internal Rational refreshRate;
		internal ScanlineOrdering scanlineOrdering;
		private string friendlyName;
		internal VideoSignalInfo videoSignalInfo;
		internal int connectorInstance;



		/// <summary>Initializes a new <see cref="DisplayMonitor"/> instance.</summary>
		/// <param name="displayAdapter">The display adapter this monitor is attached to; must not be null.</param>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure.</param>
		/// <param name="monitorHandle">A handle (HMONITOR) to the monitor.</param>
		internal DisplayMonitor( DisplayAdapter displayAdapter, DisplayDevice displayDevice, IntPtr monitorHandle )
			: base( ref displayDevice )
		{
			adapter = displayAdapter ?? throw new ArgumentNullException( "displayAdapter" );
			info = GetInfo( handle = monitorHandle );
		}



		/// <summary>Gets the <see cref="DisplayAdapter"/> this <see cref="DisplayMonitor"/> is attached to.</summary>
		public DisplayAdapter Adapter => adapter;


		/// <summary>Raised when this <see cref="DisplayMonitor"/> is disconnected.</summary>
		public event EventHandler Disconnected;


		internal void OnDisconnected()
		{
			this.Disconnected?.Invoke( this, EventArgs.Empty );
		}


		/// <summary>Gets the friendly name of this <see cref="DisplayMonitor"/>.</summary>
		public sealed override string DisplayName
		{
			get
			{
				if( !string.IsNullOrWhiteSpace( friendlyName ) )
					return string.Copy( friendlyName );
				return base.DisplayName;
			}
			internal set => friendlyName = value;
		}


		/// <summary>Gets the device path associated with this <see cref="DisplayMonitor"/>.
		/// <para>This string can be used with DisplayConfig and SetupAPI (not implemented).</para>
		/// </summary>
		public string DevicePath => base.DeviceId;


		/// <summary>Gets a value indicating the state of this <see cref="DisplayMonitor"/>.</summary>
		/// <seealso cref="DisplayMonitorStateIndicators"/>
		new public DisplayMonitorStateIndicators State => (DisplayMonitorStateIndicators)base.State;


		/// <summary>When the monitor is active (see <see cref="State"/>), gets the handle (HMONITOR) associated with this <see cref="DisplayMonitor"/>.</summary>
		public IntPtr Handle
		{
			get => handle;
			internal set => info = GetInfo( handle = value );
		}


		#region MonitorInfoEx

		/// <summary>When the monitor is active (see <see cref="State"/>), gets a value indicating whether this <see cref="DisplayMonitor"/> is the primary monitor.</summary>
		public bool IsPrimary => info.IsPrimary;


		/// <summary>When the monitor is active (see <see cref="State"/>), gets a <see cref="Rect"/> representing the monitor screen, expressed in virtual screen coordinates.</summary>
		public Rect Screen => info.Monitor;


		/// <summary>When the monitor is active (see <see cref="State"/>), gets a <see cref="Rect"/> representing the monitor's workspace, expressed in virtual screen coordinates.</summary>
		public Rect Workspace => info.Workspace;

		#endregion MonitorInfoEx


		#region DisplayConfig

		/// <summary>Gets the connector type of this <see cref="DisplayMonitor"/>.</summary>
		public VideoOutputTechnology Technology => videoOutputTechnology;


		/// <summary>Raised when the <see cref="Orientation"/> of this <see cref="DisplayMonitor"/> changed.</summary>
		public event EventHandler OrientationChanged;


		/// <summary>Gets the orientation of this <see cref="DisplayMonitor"/>.</summary>
		public DisplayRotation Orientation
		{
			get => orientation;
			internal set
			{
				if( value != orientation )
				{
					orientation = value;
					this.OrientationChanged?.Invoke( this, EventArgs.Empty );
				}
			}
		}


		/// <summary>Gets a value indicating how the source image is scaled to this <see cref="DisplayMonitor"/>.</summary>
		public Scaling Scaling => scaling;


		/// <summary>Gets the refresh rate of this <see cref="DisplayMonitor"/>.
		/// <para>
		/// If the caller specifies target mode information, the operating system will instead use the refresh rate that is stored in the VSyncFrequency member of the <see cref="VideoSignalInfo"/> structure.
		/// In this case, the caller specifies this value in the TargetVideoSignalInfo member of the <see cref="DisplayConfig.TargetMode"/> structure.
		/// </para>
		/// A refresh rate with both the numerator and denominator set to zero (<see cref="Rational.Empty"/>) indicates that the caller does not specify a refresh rate and the operating system should use the most optimal refresh rate available.
		/// </summary>
		public Rational RefreshRate => refreshRate;


		/// <summary>Specifies the scan-line ordering of the output on this <see cref="DisplayMonitor"/>.
		/// <para>If the caller specifies target mode information, the operating system will instead use the scan-line ordering that is stored in the ScanlineOrdering member of the <see cref="VideoSignalInfo"/> structure.</para>
		/// In this case, the caller specifies this value in the TargetVideoSignalInfo member of the <see cref="DisplayConfig.TargetMode"/> structure.
		/// </summary>
		public ScanlineOrdering ScanlineOrdering => scanlineOrdering;


		/// <summary></summary>
		public VideoSignalInfo VideoSignal => videoSignalInfo;


		/// <summary></summary>
		public int ConnectorInstance => connectorInstance;

		#endregion DisplayConfig

	}

}