namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Enumerates requests for the QueryDisplayConfig function (as defined in WinUser.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff569215%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1027:MarkEnumsWithFlags" )]
	[Design.Native( "WinUser.h" )]
	public enum QueryDisplayConfigRequest : int
	{

		/// <summary>Invalid.</summary>
		None = 0,

		/// <summary>Requests the table sizes to hold all the possible path combinations.
		/// <para>In the case of any temporary modes, this value means the mode data returned may not be the same as that which is stored in the persistence database.</para>
		/// </summary>
		AllPaths = 1,

		/// <summary>Requests the table sizes to hold only active paths.
		/// <para>In the case of any temporary modes, this value setting means the mode data returned may not be the same as that which is stored in the persistence database.</para>
		/// </summary>
		OnlyActivePaths = 2,

		/// <summary>Requests the table sizes to hold the active paths as defined in the persistence database for the currently connected monitors.</summary>
		DatabaseCurrent = 4
	
	}

}