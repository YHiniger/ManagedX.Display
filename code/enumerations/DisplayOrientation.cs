namespace ManagedX.Display
{

	/// <summary>Enumerates the orientation images should be presented at.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_ROTATION</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183565%28v=vs.85%29.aspx</remarks>
	[Design.Native( "WinGDI.h", "DISPLAYCONFIG_ROTATION" )]
	internal enum DisplayOrientation : int
	{

		/// <summary>The display orientation is the natural orientation of the display device; it should be used as the default.</summary>
		Default,

		/// <summary>The display orientation is rotated 90 degrees (measured clockwise) from <see cref="Default"/>.</summary>
		NinetyDegrees,

		/// <summary>The display orientation is rotated 180 degrees (measured clockwise) from <see cref="Default"/>.</summary>
		OneHundredAndEightyDegrees,

		/// <summary>The display orientation is rotated 270 degrees (measured clockwise) from <see cref="Default"/>.</summary>
		TwoHundredAndSeventyDegrees

	}

}
