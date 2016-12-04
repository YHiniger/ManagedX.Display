namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Enumerates the types of display topology (as defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff554001%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Unspecified is the zero value." )]
	[Source( "WinGDI.h", "DISPLAYCONFIG_TOPOLOGY_ID" )]
	[System.Flags]
	public enum TopologyIndicators : int
	{
		
		/// <summary>Unspecified topology.</summary>
		Unspecified = 0x00000000,

		/// <summary>The display topology is an internal configuration.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_TOPOLOGY_INTERNAL" )]
		Internal = 0x00000001,

		/// <summary>The display topology is a clone-view configuration.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_TOPOLOGY_CLONE" )]
		Clone = 0x00000002,

		/// <summary>The display topology is an extended configuration.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_TOPOLOGY_EXTEND" )]
		Extend = 0x00000004,

		/// <summary>The display topology is an external configuration.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_TOPOLOGY_EXTERNAL" )]
		External = 0x00000008,

	}

}