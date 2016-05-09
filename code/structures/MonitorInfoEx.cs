using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>Contains information about a display monitor.
	/// <para>This structure is equivalent to the native <code>MONITORINFOEX</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd145066%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Native( "WinUser.h", "MONITORINFOEX" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 104 )]
	internal struct MonitorInfoEx : IEquatable<MonitorInfoEx>
	{

		private int structSize;
		private Rect monitor;
		private Rect work;
		private MonitorInfoStateIndicators flags;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 32 )]
		private string deviceName;



		private MonitorInfoEx( int structureSize )
		{
			structSize = structureSize;
			monitor = work = Rect.Zero;
			flags = MonitorInfoStateIndicators.None;
			deviceName = string.Empty;
		}



		/// <summary>Gets the display monitor rectangle, expressed in virtual-screen coordinates.
		/// <para>Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.</para>
		/// </summary>
		public Rect Monitor { get { return monitor; } }


		/// <summary>Gets the work area rectangle of the display monitor which can be used by applications, expressed in virtual-screen coordinates.
		/// <para>Windows uses this rectangle to maximize an application on the monitor.</para>
		/// The rest of the area in <see cref="Monitor"/> contains system windows such as the task bar and side bars.
		/// <para>Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.</para>
		/// </summary>
		public Rect Workspace { get { return work; } }
		

		/// <summary>Gets a value indicating whether the monitor is the primary monitor.</summary>
		public bool IsPrimary { get { return flags.HasFlag( MonitorInfoStateIndicators.Primary ); } }


		/// <summary>Gets the adapter device name of the monitor.
		/// <para>Most applications have no use for a display monitor name, and so can save some bytes by using a MonitorInfo structure.</para>
		/// </summary>
		public string AdapterDeviceName { get { return string.Copy( deviceName ?? string.Empty ); } }



		/// <summary>Returns a hash code for this <see cref="MonitorInfoEx"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MonitorInfoEx"/> structure.</returns>
		public override int GetHashCode()
		{
			return structSize ^ monitor.GetHashCode() ^ work.GetHashCode() ^ (int)flags ^ this.AdapterDeviceName.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="MonitorInfoEx"/> structure equals another <see cref="MonitorInfoEx"/> structure.</summary>
		/// <param name="other">A <see cref="MonitorInfoEx"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( MonitorInfoEx other )
		{
			return
				( structSize == other.structSize ) &&
				( monitor == other.monitor ) &&
				( work == other.work ) &&
				( flags == other.flags ) && 
				this.AdapterDeviceName.Equals( other.AdapterDeviceName, StringComparison.Ordinal );
		}


		/// <summary>Returns a value indicating whether this <see cref="MonitorInfoEx"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MonitorInfoEx"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is MonitorInfoEx ) && this.Equals( (MonitorInfoEx)obj );
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