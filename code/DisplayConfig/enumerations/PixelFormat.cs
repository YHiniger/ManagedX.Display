using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Enumerates pixel format in various bits per pixel (BPP) values.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_PIXELFORMAT</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553963%28v=vs.85%29.aspx</remarks>
	[Source( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT" )]
	public enum PixelFormat : int
	{

		/// <summary>Undefined.</summary>
		Undefined = 0,

		/// <summary>8 bits per pixel format.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BPP" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_8BPP" )]
		EightBPP = 1,

		/// <summary>16 bits per pixel format.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BPP" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_16BPP" )]
		SixteenBPP = 2,

		/// <summary>24 bits per pixel format.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BPP" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_24BPP" )]
		TwentyFourBPP = 3,

		/// <summary>32 bits per pixel format. </summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BPP" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_32BPP" )]
		ThirtyTwoBPP = 4,

		/// <summary>The current display is not an 8, 16, 24, or 32 BPP GDI desktop mode.
		/// <para>For example, a call to the QueryDisplayConfig function returns NonGDI if a DirectX application previously set the desktop to A2R10G10B10 format.</para>
		/// A call to the SetDisplayConfig function fails if any pixel formats for active paths are set to NonGDI.
		/// </summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GDI" )]
		[Source( "WinGDI.h", "DISPLAYCONFIG_PIXELFORMAT_NONGDI" )]
		NonGDI = 5
	
	}

}