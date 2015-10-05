namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff554001%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Specifies the type of display topology.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Unspecified is the zero value." )]
	[System.Flags]
	public enum TopologyId : int
	{
		
		/// <summary>Unspecified topology.</summary>
		Unspecified = 0x00000000,

		/// <summary>Indicates that the display topology is an internal configuration.</summary>
		Internal = 0x00000001,

		/// <summary>Indicates that the display topology is clone-view configuration.</summary>
		Clone = 0x00000002,

		/// <summary>Indicates that the display topology is an extended configuration.</summary>
		Extend = 0x00000004,

		/// <summary>Indicates that the display topology is an external configuration.</summary>
		External = 0x00000008

	}

}