namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Specifies whether the information contained within the <see cref="ModeInfo"/> structure is source or target mode.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_MODE_INFO_TYPE</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553942%28v=vs.85%29.aspx</remarks>
	[Design.Native( "WinGDI.h", "DISPLAYCONFIG_MODE_INFO_TYPE" )]
	public enum ModeInfoType : int
	{

		/// <summary>Indicates that the <see cref="ModeInfo"/> structure is not initialized.</summary>
		Undefined = 0,

		/// <summary>Indicates that the <see cref="ModeInfo"/> structure contains source mode information.</summary>
		Source = 1,

		/// <summary>Indicates that the <see cref="ModeInfo"/> structure contains target mode information.</summary>
		Target = 2

	}

}