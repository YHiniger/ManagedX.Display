namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553942%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Specifies whether the information contained within the <see cref="ModeInfo"/> structure is source or target mode.</summary>
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
