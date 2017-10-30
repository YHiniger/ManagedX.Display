using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary></summary>
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_COLOR_ENCODING" )]
	public enum ColorEncoding : int
	{

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGB" )]
		[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_COLOR_ENCODING_RGB" )]
		RGB = 0,

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "YCBCR" )]
		[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_COLOR_ENCODING_YCBCR444" )]
		YCBCR444 = 1,

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "YCBCR" )]
		[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_COLOR_ENCODING_YCBCR422" )]
		YCBCR422 = 2,

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "YCBCR" )]
		[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_COLOR_ENCODING_YCBCR420" )]
		YCBCR420 = 3,

		/// <summary></summary>
		[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_COLOR_ENCODING_INTENSITY" )]
		Intensity = 4,

	}

}