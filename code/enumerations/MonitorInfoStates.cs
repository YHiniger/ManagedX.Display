namespace ManagedX.Display
{

	// https://msdn.microsoft.com/en-us/library/dd145065%28v=vs.85%29.aspx
	// https://msdn.microsoft.com/en-us/library/dd145066%28v=vs.85%29.aspx
	// WinUser.h

	
	/// <summary>Enumerates flags used in the MonitorInfo and <see cref="MonitorInfoEx"/> structures.</summary>
	[System.Flags]
	internal enum MonitorInfoStates : int
	{
		
		/// <summary>No state specified.</summary>
		None = 0x00000000,

		/// <summary>This is the primary display monitor.</summary>
		Primary = 0x00000001

	}

}