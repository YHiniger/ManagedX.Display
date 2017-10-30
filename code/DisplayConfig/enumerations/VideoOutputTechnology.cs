using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Enumerates video output technologies, as defined in WinGDI.h.
	/// <para>Values with "Embedded" in their names indicate that the graphics adapter's video output device connects internally to the display device.
	/// In those cases, the <see cref="VideoOutputTechnology.Internal"/> value is redundant.
	/// The caller should ignore <see cref="VideoOutputTechnology.Internal"/> and just process the embedded values,
	/// <see cref="VideoOutputTechnology.DisplayPortEmbedded"/> and <see cref="VideoOutputTechnology.UDIEmbedded"/>.</para>
	/// <para>An embedded display port or UDI is also known as an integrated display port or UDI.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff554003%28v=vs.85%29.aspx</remarks>
	[Source( "WinGDI.h", "DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY" )]
	public enum VideoOutputTechnology : int
	{

		/// <summary>Indicates that the video output device connects internally to a display device (for example, the internal connection in a laptop computer).</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY" )]
		Internal = -2147483648,

		/// <summary>Indicates a connector that is not one of the types that is indicated by the following enumerators in this enumeration.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER" )]
		Other = -1,

		/// <summary>Indicates an HD15 (VGA) connector.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15" )]
		HD15 = 0,

		/// <summary>Indicates an S-video connector.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO" )]
		SVideo = 1,

		/// <summary>Indicates a composite video connector group.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO" )]
		CompositeVideo = 2,

		/// <summary>Indicates a component video connector group.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO" )]
		ComponentVideo = 3,

		/// <summary>Indicates a Digital Video Interface (DVI) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DVI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI" )]
		DVI = 4,

		/// <summary>Indicates a High-Definition Multimedia Interface (HDMI) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HDMI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI" )]
		HDMI = 5,

		/// <summary>Indicates a Low Voltage Differential Swing (LVDS) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LVDS" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS" )]
		LVDS = 6,

        /// <summary>Indicates a Japanese D connector.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jpn")]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN" )]
		DJpn = 8,

		/// <summary>Indicates an SDI connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI" )]
		SDI = 9,

		/// <summary>Indicates an external display port, which is a display port that connects externally to a display device.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL" )]
		DisplayPortExternal = 10,

		/// <summary>Indicates an embedded display port that connects internally to a display device.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED" )]
		DisplayPortEmbedded = 11,

		/// <summary>Indicates an external Unified Display Interface (UDI), which is a UDI that connects externally to a display device.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL" )]
		UDIExternal = 12,

		/// <summary>Indicates an embedded UDI that connects internally to a display device.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED" )]
		UDIEmbedded = 13,

		/// <summary>Indicates a dongle cable that supports Standard Definition TeleVision (SDTV).</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SDTV" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE" )]
		SDTVDongle = 14,

        /// <summary>Indicates that the VidPN target is a Miracast wireless display device. Supported starting in Windows 8.1.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Miracast")]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST" )]
		Miracast = 15,

		/// <summary></summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INDIRECT_WIRED" )]
		IndirectWired = 16,

	}

}