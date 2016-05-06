namespace ManagedX.Display
{

	/// <summary>Enumerates flags used in the MonitorInfo and <see cref="MonitorInfoEx"/> structures.
	/// <para>This enumeration is equivalent to the native <code>MONITORINFOF_*</code> constants (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/dd145065%28v=vs.85%29.aspx (MONITORINFO)
	/// https://msdn.microsoft.com/en-us/library/dd145066%28v=vs.85%29.aspx (MONITORINFOEX)
	/// </remarks>
	[System.Flags]
	internal enum MonitorInfoStateIndicators : int
	{
		
		/// <summary>No states specified.</summary>
		None = 0x00000000,

		/// <summary>This is the primary display monitor.</summary>
		[Win32.Native( "WinUser.h", "MONITORINFOF_PRIMARY" )]
		Primary = 0x00000001,

	}

}