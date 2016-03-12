namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Enumerates the types of display topology (as defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff554001%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Unspecified is the zero value." )]
	[Design.Native( "WinGDI.h", "DISPLAYCONFIG_TOPOLOGY_ID" )]
	[System.Flags]
	public enum TopologyId : int
	{
		
		/// <summary>Unspecified topology.</summary>
		Unspecified = 0x00000000,

		/// <summary>The display topology is an internal configuration.</summary>
		Internal = 0x00000001,

		/// <summary>The display topology is a clone-view configuration.</summary>
		Clone = 0x00000002,

		/// <summary>The display topology is an extended configuration.</summary>
		Extend = 0x00000004,

		/// <summary>The display topology is an external configuration.</summary>
		External = 0x00000008

	}

}