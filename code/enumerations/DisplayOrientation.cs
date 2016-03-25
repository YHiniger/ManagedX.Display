namespace ManagedX.Display
{
	using Win32;


	/// <summary>Enumerates the orientation images should be presented at.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_ROTATION</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183565%28v=vs.85%29.aspx</remarks>
	[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION" )]
	internal enum DisplayOrientation : int
	{

		/// <summary>The display orientation is the natural orientation of the display device; it should be used as the default.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_IDENTITY" )]
		Identity,

		/// <summary>The display orientation is rotated 90 degrees (measured clockwise) from <see cref="Identity"/>.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_ROTATE90" )]
		Rotate90,

		/// <summary>The display orientation is rotated 180 degrees (measured clockwise) from <see cref="Identity"/>.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_ROTATE180" )]
		Rotate180,

		/// <summary>The display orientation is rotated 270 degrees (measured clockwise) from <see cref="Identity"/>.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION_ROTATE270" )]
		Rotate270

	}

}