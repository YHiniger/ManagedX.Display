namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Enumerates orientations images should be presented at.
	/// <para>This enumeration is equivalent to the native <code>DMDO_*</code> constants (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183565%28v=vs.85%29.aspx</remarks>
	internal enum DisplayOrientation : int
	{

		/// <summary>The display orientation is the natural orientation of the display device; it should be used as the default.</summary>
		[Source( "WinGDI.h", "DMDO_DEFAULT" )]
		Default,

		/// <summary>The display orientation is rotated 90 degrees (measured clockwise) from <see cref="Default"/>.</summary>
		[Source( "WinGDI.h", "DMDO_90" )]
		Rotate90,

		/// <summary>The display orientation is rotated 180 degrees (measured clockwise) from <see cref="Default"/>.</summary>
		[Source( "WinGDI.h", "DMDO_180" )]
		Rotate180,

		/// <summary>The display orientation is rotated 270 degrees (measured clockwise) from <see cref="Default"/>.</summary>
		[Source( "WinGDI.h", "DMDO_270" )]
		Rotate270

	}

}