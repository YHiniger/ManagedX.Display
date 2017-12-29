using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Enumerates video output technologies, as defined in WinGDI.h.
	/// <para>Values with "Embedded" in their names indicate that the graphics adapter's video output device connects internally to the display device.
	/// In those cases, the <see cref="Internal"/> value is redundant.
	/// The caller should ignore <see cref="Internal"/> and just process the embedded values,
	/// <see cref="DisplayPortEmbedded"/> and <see cref="UDIEmbedded"/>.</para>
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
		HD15,

		/// <summary>Indicates an S-video connector.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO" )]
		SVideo,

		/// <summary>Indicates a composite video connector group.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO" )]
		CompositeVideo,

		/// <summary>Indicates a component video connector group.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO" )]
		ComponentVideo,

		/// <summary>Indicates a Digital Video Interface (DVI) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DVI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI" )]
		DVI,

		/// <summary>Indicates a High-Definition Multimedia Interface (HDMI) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HDMI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI" )]
		HDMI,

		/// <summary>Indicates a Low Voltage Differential Swing (LVDS) connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LVDS" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS" )]
		LVDS,

        /// <summary>Indicates a Japanese D connector.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jpn")]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN" )]
		DJpn = 8,

		/// <summary>Indicates an SDI connector.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI" )]
		SDI,

		/// <summary>Indicates an external display port, which is a display port that connects externally to a display device.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL" )]
		DisplayPortExternal,

		/// <summary>Indicates an embedded display port that connects internally to a display device.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED" )]
		DisplayPortEmbedded,

		/// <summary>Indicates an external Unified Display Interface (UDI), which is a UDI that connects externally to a display device.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL" )]
		UDIExternal,

		/// <summary>Indicates an embedded Unified Display Interface (UDI) which connects internally to a display device.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED" )]
		UDIEmbedded,

		/// <summary>Indicates a dongle cable that supports Standard Definition TeleVision (SDTV).</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SDTV" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE" )]
		SDTVDongle,

        /// <summary>Indicates that the VidPN target is a Miracast wireless display device. Supported starting in Windows 8.1.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Miracast")]
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST" )]
		Miracast,


		/// <summary></summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INDIRECT_WIRED" )]
		IndirectWired,

	}

}