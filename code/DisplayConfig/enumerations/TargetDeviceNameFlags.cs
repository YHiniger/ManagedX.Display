using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553990%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Enumerates flags used in the <see cref="TargetDeviceName"/> structure.</summary>
	[SuppressMessage( "Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags" )]
	[System.Flags]
	public enum TargetDeviceNameFlags : int
	{

		/// <summary>No flag specified.</summary>
		None = 0x00000000,

		/// <summary>Indicates that the string in the monitorFriendlyDeviceName member of the <see cref="TargetDeviceName"/> structure was constructed from the manufacture identification string in the extended display identification data (EDID).</summary>
		FriendlyNameFromExtendedDisplayInformationData = 0x00000001,

		/// <summary>Indicates that the target is forced with no detectable monitor attached and the monitorFriendlyDeviceName member of the <see cref="TargetDeviceName"/> structure is a null-terminated empty string.</summary>
		FriendlyNameForced = 0x00000002,

		/// <summary>Indicates that the edidManufactureId and edidProductCodeId members of the <see cref="TargetDeviceName"/> structure are valid and were obtained from the extended display information data (EDID).</summary>
		ExtendedDisplayInformationDataIdsValid = 0x00000004

	}

}