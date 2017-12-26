using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>Contains information about a display monitor.
	/// <para>This structure is equivalent to the native <code>MONITORINFOEX</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd145066%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinUser.h", "MONITORINFOEX" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 104 )]
	internal struct MonitorInfoEx : IEquatable<MonitorInfoEx>
	{

		/// <summary>Enumerates flags used in the MonitorInfo and <see cref="MonitorInfoEx"/> structures.
		/// <para>This enumeration is equivalent to the native <code>MONITORINFOF_*</code> constants (defined in WinUser.h).</para>
		/// </summary>
		[Flags]
		private enum StateIndicators : int
		{

			/// <summary>No states specified.</summary>
			None = 0x00000000,

			/// <summary>This is the primary display monitor.</summary>
			[Win32.Source( "WinUser.h", "MONITORINFOF_PRIMARY" )]
			Primary = 0x00000001,

		}



		// MONITORINFO struct
		private readonly int structSize;

		/// <summary>The display monitor rectangle, expressed in virtual-screen coordinates.
		/// <para>Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.</para>
		/// </summary>
		public readonly Rect Monitor;

		/// <summary>The work area rectangle of the display monitor which can be used by applications, expressed in virtual-screen coordinates.
		/// <para>Windows uses this rectangle to maximize an application on the monitor.</para>
		/// The rest of the area in <see cref="Monitor"/> contains system windows such as the task bar and side bars.
		/// <para>Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.</para>
		/// </summary>
		public readonly Rect Workspace;

		private readonly StateIndicators flags;
		// End of MONITORINFO struct

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = DisplayDevice.MaxDeviceNameChars )]
		private string deviceName;



		private MonitorInfoEx( int structureSize )
		{
			structSize = structureSize;
			Monitor = Workspace = Rect.Zero;
			flags = StateIndicators.None;
			deviceName = string.Empty;
		}



		/// <summary>Gets a value indicating whether the monitor is the primary monitor.</summary>
		public bool IsPrimary => flags.HasFlag( StateIndicators.Primary );


		/// <summary>Gets the adapter device name of the monitor.
		/// <para>Most applications have no use for a display monitor name, and so can save some bytes by using a MonitorInfo structure.</para>
		/// </summary>
		public string AdapterDeviceName => string.Copy( deviceName ?? string.Empty );



		/// <summary>Returns a hash code for this <see cref="MonitorInfoEx"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MonitorInfoEx"/> structure.</returns>
		public override int GetHashCode()
		{
			return structSize ^ Monitor.GetHashCode() ^ Workspace.GetHashCode() ^ (int)flags ^ this.AdapterDeviceName.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="MonitorInfoEx"/> structure equals another <see cref="MonitorInfoEx"/> structure.</summary>
		/// <param name="other">A <see cref="MonitorInfoEx"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( MonitorInfoEx other )
		{
			return
				( structSize == other.structSize ) &&
				Monitor.Equals( other.Monitor ) &&
				Workspace.Equals( other.Workspace ) &&
				( flags == other.flags ) && 
				this.AdapterDeviceName.Equals( other.AdapterDeviceName, StringComparison.Ordinal );
		}


		/// <summary>Returns a value indicating whether this <see cref="MonitorInfoEx"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MonitorInfoEx"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is MonitorInfoEx mie ) && this.Equals( mie );
		}


		/// <summary>Returns the <see cref="AdapterDeviceName"/> of this <see cref="MonitorInfoEx"/> structure.</summary>
		/// <returns>Returns the <see cref="AdapterDeviceName"/> of this <see cref="MonitorInfoEx"/> structure.</returns>
		public override string ToString()
		{
			return this.AdapterDeviceName;
		}


		/// <summary>The empty (and invalid) <see cref="MonitorInfoEx"/> structure.</summary>
		public static readonly MonitorInfoEx Empty;

		/// <summary>The default <see cref="MonitorInfoEx"/> structure.
		/// <para>Use this value to initialize a valid structure.</para>
		/// </summary>
		public static readonly MonitorInfoEx Default = new MonitorInfoEx( Marshal.SizeOf( typeof( MonitorInfoEx ) ) );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="monitorInfo">A <see cref="MonitorInfoEx"/> structure.</param>
		/// <param name="other">A <see cref="MonitorInfoEx"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( MonitorInfoEx monitorInfo, MonitorInfoEx other )
		{
			return monitorInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="monitorInfo">A <see cref="MonitorInfoEx"/> structure.</param>
		/// <param name="other">A <see cref="MonitorInfoEx"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( MonitorInfoEx monitorInfo, MonitorInfoEx other )
		{
			return !monitorInfo.Equals( other );
		}

		#endregion Operators

	}

}