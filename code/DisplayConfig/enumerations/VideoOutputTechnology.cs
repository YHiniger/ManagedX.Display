using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff554003%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Enumerates video output technologies.
	/// <para>Values with "Embedded" in their names indicate that the graphics adapter's video output device connects internally to the display device.
	/// In those cases, the <see cref="VideoOutputTechnology.Internal"/> value is redundant.
	/// The caller should ignore <see cref="VideoOutputTechnology.Internal"/> and just process the embedded values, <see cref="VideoOutputTechnology.DisplayPortEmbedded"/> and <see cref="VideoOutputTechnology.UDIEmbedded"/>.</para>
	/// <para>An embedded display port or UDI is also known as an integrated display port or UDI.</para>
	/// </summary>
	public enum VideoOutputTechnology : int
	{

		/// <summary>Indicates that the video output device connects internally to a display device (for example, the internal connection in a laptop computer).</summary>
		Internal = -2147483648,

		/// <summary>Indicates a connector that is not one of the types that is indicated by the following enumerators in this enumeration.</summary>
		Other = -1,

		/// <summary>Indicates an HD15 (VGA) connector.</summary>
		HD15 = 0,

		/// <summary>Indicates an S-video connector.</summary>
		SVideo = 1,

		/// <summary>Indicates a composite video connector group.</summary>
		CompositeVideo = 2,

		/// <summary>Indicates a component video connector group.</summary>
		ComponentVideo = 3,

		/// <summary>Indicates a Digital Video Interface (DVI) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DVI" )]
		DVI = 4,

		/// <summary>Indicates a High-Definition Multimedia Interface (HDMI) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HDMI" )]
		HDMI = 5,

		/// <summary>Indicates a Low Voltage Differential Swing (LVDS) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LVDS" )]
		LVDS = 6,

		/// <summary>Indicates a Japanese D connector.</summary>
		DJpn = 8,

		/// <summary>Indicates an SDI connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SDI" )]
		SDI = 9,

		/// <summary>Indicates an external display port, which is a display port that connects externally to a display device.</summary>
		DisplayPortExternal = 10,

		/// <summary>Indicates an embedded display port that connects internally to a display device.</summary>
		DisplayPortEmbedded = 11,

		/// <summary>Indicates an external Unified Display Interface (UDI), which is a UDI that connects externally to a display device.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UDI" )]
		UDIExternal = 12,

		/// <summary>Indicates an embedded UDI that connects internally to a display device.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UDI" )]
		UDIEmbedded = 13,

		/// <summary>Indicates a dongle cable that supports Standard Definition TeleVision (SDTV).</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SDTV" )]
		SDTVDongle = 14,

		/// <summary>Indicates that the VidPN target is a Miracast wireless display device. Supported starting in Windows 8.1.</summary>
		Miracast = 15

	}

}