namespace ManagedX.Display.DisplayConfig
{
	using Win32;


	/// <summary>Enumerates requests for the QueryDisplayConfig function (as defined in WinUser.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff569215%28v=vs.85%29.aspx</remarks>
	[System.Flags]
	public enum QueryDisplayConfigRequest : int
	{

		/// <summary>Invalid.</summary>
		None = 0x00000000,

		/// <summary>Requests the table sizes to hold all the possible path combinations.
		/// <para>In the case of any temporary modes, this value means the mode data returned may not be the same as that which is stored in the persistence database.</para>
		/// </summary>
		[Native( "WinUser.h", "QDC_ALL_PATHS" )]
		AllPaths = 0x00000001,

		/// <summary>Requests the table sizes to hold only active paths.
		/// <para>In the case of any temporary modes, this value setting means the mode data returned may not be the same as that which is stored in the persistence database.</para>
		/// </summary>
		[Native( "WinUser.h", "QDC_ONLY_ACTIVE_PATHS" )]
		OnlyActivePaths = 0x00000002,

		/// <summary>Requests the table sizes to hold the active paths as defined in the persistence database for the currently connected monitors.</summary>
		[Native( "WinUser.h", "QDC_DATABASE_CURRENT" )]
		DatabaseCurrent = 0x00000004,


		/// <summary>
		/// <para>Requires Windows 10 or newer.</para>
		/// </summary>
		[Native( "WinUser.h", "QDC_VIRTUAL_MODE_AWARE" )]
		VirtualModeAware = 0x00000010,
	
	}

}