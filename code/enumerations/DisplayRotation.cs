namespace ManagedX.Display
{

	/// <summary>Specifies the clockwise rotation of the display.
	/// <para>This enumeration is equivalent to the native enumerations <code>DISPLAYCONFIG_ROTATION</code> (defined in WinGDI.h) and <code>DXGI_MODE_ROTATION</code> (defined in DXGI.h).</para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553970%28v=vs.85%29.aspx (DISPLAYCONFIG_ROTATION)
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/bb173065%28v=vs.85%29.aspx (DXGI_MODE_ROTATION)
	/// </remarks>
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