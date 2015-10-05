namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553954%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Enumerates a <see cref="PathTargetInfo"/>'s status flags.</summary>
	[System.Flags]
	public enum PathTargetInfoStatus : int
	{

		/// <summary>Invalid.</summary>
		None = 0x00000000,

		/// <summary>Target is in use on an active path.</summary>
		InUse = 0x00000001,

		/// <summary>The output can be forced on this target even if a monitor is not detected.</summary>
		Forcible = 0x00000002,

		/// <summary>Output is currently being forced in a boot-persistent manner.</summary>
		ForcedAvailabilityBoot = 0x00000004,

		/// <summary>Output is currently being forced in a path-persistent manner.</summary>
		ForcedAvailabilityPath = 0x00000008,

		/// <summary>Output is currently being forced in a nonpersistent manner.</summary>
		ForcedAvailabilitySystem = 0x00000010

	}

}
