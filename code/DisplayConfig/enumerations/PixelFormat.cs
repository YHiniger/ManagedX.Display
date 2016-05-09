namespace ManagedX.Display.DisplayConfig
{
	using Win32;


	/// <summary>Enumerates pixel format in various bits per pixel (BPP) values.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_PIXELFORMAT</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553963%28v=vs.85%29.aspx</remarks>
	[Native( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT" )]
	public enum PixelFormat : int
	{

		/// <summary>Undefined.</summary>
		None = 0,

        /// <summary>8 bits per pixel format.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bpp")]
		[Native( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_8BPP" )]
		EightBpp = 1,

        /// <summary>16 bits per pixel format.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bpp")]
		[Native( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_16BPP" )]
		SixteenBpp = 2,

        /// <summary>24 bits per pixel format.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bpp")]
		[Native( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_24BPP" )]
		TwentyFourBpp = 3,

        /// <summary>32 bits per pixel format. </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bpp")]
		[Native( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_32BPP" )]
		ThirtyTwoBpp = 4,

		/// <summary>The current display is not an 8, 16, 24, or 32 BPP GDI desktop mode.
		/// <para>For example, a call to the QueryDisplayConfig function returns NonGDI if a DirectX application previously set the desktop to A2R10G10B10 format.</para>
		/// A call to the SetDisplayConfig function fails if any pixel formats for active paths are set to NonGDI.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GDI" )]
		[Native( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_NONGDI" )]
		NonGDI = 5
	
	}

}