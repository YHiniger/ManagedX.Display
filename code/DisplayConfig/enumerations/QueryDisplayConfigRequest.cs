namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff569215%28v=vs.85%29.aspx
	// WinUser.h

	// FIXME - it is not clear whether QueryDisplayConfigRequest values can be combined.


	/// <summary>Enumerates requests for the QueryDisplayConfig function.</summary>
	[System.Flags]
	public enum QueryDisplayConfigRequest : int
	{

		/// <summary>Invalid.</summary>
		None = 0x00000000,

		/// <summary>Requests the table sizes to hold all the possible path combinations.
		/// <para>In the case of any temporary modes, this value means the mode data returned may not be the same as that which is stored in the persistence database.</para>
		/// </summary>
		AllPaths = 0x00000001,

		/// <summary>Requests the table sizes to hold only active paths.
		/// <para>In the case of any temporary modes, this value setting means the mode data returned may not be the same as that which is stored in the persistence database.</para>
		/// </summary>
		OnlyActivePaths = 0x00000002,

		/// <summary>Requests the table sizes to hold the active paths as defined in the persistence database for the currently connected monitors.</summary>
		DatabaseCurrent = 0x00000004
	
	}

}