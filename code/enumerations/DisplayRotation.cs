namespace ManagedX.Display
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553970%28v=vs.85%29.aspx
	// WinGDI.h (DISPLAYCONFIG_ROTATION)

	// https://msdn.microsoft.com/en-us/library/windows/desktop/bb173065%28v=vs.85%29.aspx
	// DXGI.h (DXGI_MODE_ROTATION)


	/// <summary>Specifies the clockwise rotation of the display.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_ROTATION</code> and <code>DXGI_MODE_ROTATION</code> enumerations.</para>
	/// </summary>
	public enum DisplayRotation : int
	{

		/// <summary>Unspecified.</summary>
		Unspecified,

		/// <summary>Indicates that rotation is 0 degrees—landscape mode.</summary>
		Identity,

		/// <summary>Indicates that rotation is 90 degrees clockwise—portrait mode.</summary>
		Rotate90,

		/// <summary>Indicates that rotation is 180 degrees clockwise—inverted landscape mode.</summary>
		Rotate180,

		/// <summary>Indicates that rotation is 270 degrees clockwise—inverted portrait mode.</summary>
		Rotate270

	}

}