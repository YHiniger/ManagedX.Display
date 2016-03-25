namespace ManagedX.Display
{
	using DisplayConfig; // for comments
	using Win32;


	/// <summary>Specifies the method the display uses to create an image on a screen, or the raster uses to create an image on a surface.
	/// <para>This enumeration is equivalent to the native
	/// <code>DISPLAYCONFIG_SCANLINE_ORDERING</code> (defined in WinGDI.h) and
	/// <code>DXGI_MODE_SCANLINE_ORDER</code> (defined in DXGIType.h) enumerations.
	/// </para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553977%28v=vs.85%29.aspx (DISPLAYCONFIG_SCANLINE_ORDERING)
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/bb173067%28v=vs.85%29.aspx (DXGI_SCANLINE_ORDER)
	/// </remarks>
	[Native( "DXGIType.h", "DXGI_MODE_SCANLINE_ORDER" )]
	[Native( "WinGDI.h", "DISPLAYCONFIG_SCANLINE_ORDERING" )]
	public enum ScanlineOrdering : int
	{

		/// <summary>Scan-line ordering of the output is unspecified.
		/// <para>The caller can only set the scanLineOrdering member of the <see cref="PathTargetInfo"/> structure in a call to the
		/// SetDisplayConfig function to <see cref="ScanlineOrdering.Unspecified"/> if the caller also set the refresh rate denominator and
		/// numerator of the refreshRate member both to zero.</para>
		/// In this case, SetDisplayConfig uses the best refresh rate it can find.
		/// </summary>
		Unspecified = 0,

		/// <summary>The output is a progressive image; the image is created from the first scanline to the last without skipping any.</summary>
		Progressive = 1,

		/// <summary>The output is an interlaced image, created beginning with the upper field.</summary>
		UpperFieldFirst = 2,

		/// <summary>The output is an interlaced image, created beginning with the lower field.</summary>
		LowerFieldFirst = 3

	}

}