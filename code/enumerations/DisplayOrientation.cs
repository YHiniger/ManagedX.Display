namespace ManagedX.Display
{

	// WinGDI.h

	/// <summary>Enumerates the orientation images should be presented at.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183565%28v=vs.85%29.aspx</remarks>
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
