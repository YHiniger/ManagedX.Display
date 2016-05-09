namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Enumerates flags of a <see cref="PathTargetInfo"/>'s status.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553954%28v=vs.85%29.aspx</remarks>
	[System.Flags]
	public enum PathTargetInfoStatus : int // TODO - rename to PathTargetInfoStateIndicators
	{

		/// <summary>Invalid.</summary>
		None = 0x00000000,

		/// <summary>Target is in use on an active path.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_IN_USE" )]
		InUse = 0x00000001,

		/// <summary>The output can be forced on this target even if a monitor is not detected.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_FORCIBLE" )]
		Forcible = 0x00000002,

		/// <summary>Output is currently being forced in a boot-persistent manner.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_BOOT" )]
		ForcedAvailabilityBoot = 0x00000004,

		/// <summary>Output is currently being forced in a path-persistent manner.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_PATH" )]
		ForcedAvailabilityPath = 0x00000008,

		/// <summary>Output is currently being forced in a nonpersistent manner.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_FORCED_AVAILABILITY_SYSTEM" )]
		ForcedAvailabilitySystem = 0x00000010,

	}

}