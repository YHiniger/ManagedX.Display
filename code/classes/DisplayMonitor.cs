using System;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Display
{

	/// <summary>Represents a display monitor.</summary>
	public sealed class DisplayMonitor : DisplayDeviceBase
	{

		/// <summary>Enumerates options for use with the <see cref="GetMonitorHandleFromWindow"/> method.</summary>
		internal enum MonitorFromWindowOption : int
		{

			/// <summary>Causes the method to return <see cref="IntPtr.Zero"/>.</summary>
			DefaultToNull,

			/// <summary>Causes the method to return a handle to the primary display monitor.</summary>
			DefaultToPrimary,
			
			/// <summary>Causes the method to return a handle to the display monitor that is nearest to the window.</summary>
			DefaultToNearest

		}


		#region Static methods

		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h

			
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


		/// <summary>Returns a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.</summary>
		/// <param name="windowHandle">A handle to the window of interest.</param>
		/// <returns>Returns an HMONITOR handle to the display monitor that has the largest area of intersection with the window, or to the nearest display monitor.</returns>
		public static IntPtr GetMonitorHandleFromWindow( IntPtr windowHandle )
		{
			return SafeNativeMethods.MonitorFromWindow( windowHandle, MonitorFromWindowOption.DefaultToNearest );
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

		#endregion Static methods



		private IntPtr handle;



		/// <summary>Initializes a new <see cref="DisplayMonitor"/> instance.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure.</param>
		/// <param name="monitorHandle">A handle (HMONITOR) to the monitor.</param>
		internal DisplayMonitor( DisplayDevice displayDevice, IntPtr monitorHandle )
			: base( displayDevice )
		{
			handle = monitorHandle;
		}



		/// <summary>Gets a value indicating the state of this <see cref="DisplayMonitor"/>.</summary>
		public MonitorStateIndicators State { get { return (MonitorStateIndicators)base.RawState; } }


		internal void Reset( DisplayDevice displayDevice, IntPtr monitorHandle )
		{
			handle = monitorHandle;
			base.Reset( displayDevice );
		}


		/// <summary>Gets the device path associated with this <see cref="DisplayMonitor"/>.
		/// <para>This string can be used with DisplayConfig and SetupAPI (not implemented).</para>
		/// </summary>
		public string DevicePath { get { return base.DeviceId; } }


		/// <summary>Gets the handle (HMONITOR) associated with this <see cref="DisplayMonitor"/>.</summary>
		public IntPtr Handle { get { return handle; } }


		/// <summary>Gets information about this <see cref="DisplayMonitor"/>.</summary>
		public MonitorInfoEx Info { get { return GetMonitorInfo( handle ); } }

	}

}