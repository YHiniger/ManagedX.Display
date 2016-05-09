using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Contains information about a source display mode (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553986%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_SOURCE_MODE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public struct SourceMode : IEquatable<SourceMode>
	{

		private int width;
		private int height;
		private PixelFormat pixelFormat;
		private Point position;


		/// <summary>Gets the width in pixels of the source mode.</summary>
		public int Width { get { return width; } }


		/// <summary>Gets the height in pixels of the source mode.</summary>
		public int Height { get { return height; } }


		/// <summary>Gets a structure representing the size, in pixels, of the source mode.</summary>
		public Size Size { get { return new Size( width, height ); } }


		/// <summary>Gets the pixel format of the source mode.</summary>
		public PixelFormat PixelFormat { get { return pixelFormat; } }


		/// <summary>Gets a <see cref="Point"/> structure that specifies the position in the desktop coordinate space of the upper-left corner of this source surface.
		/// The source surface that is located at (0, 0) is always the primary source surface.</summary>
		public Point Position { get { return position; } }


		/// <summary>Returns a hash code for this <see cref="SourceMode"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="SourceMode"/> structure.</returns>
		public override int GetHashCode()
		{
			return width ^ height ^ (int)pixelFormat ^ position.GetHashCode();
		}

		
		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( SourceMode other )
		{
			return ( width == other.width ) && ( height == other.height ) && ( pixelFormat == other.pixelFormat ) && ( position == other.position );
		}

		
		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return ( obj is SourceMode ) && this.Equals( (SourceMode)obj );
		}


		/// <summary></summary>
		/// <returns></returns>
		public override string ToString()
		{
			string pixelFmt = "";
			if( pixelFormat == DisplayConfig.PixelFormat.None )
				pixelFmt = "? bpp";
			else if( pixelFormat == DisplayConfig.PixelFormat.NonGDI )
				pixelFmt = "Non GDI";
			else if( pixelFormat == DisplayConfig.PixelFormat.EightBpp )
				pixelFmt = "8 bpp";
			else if( pixelFormat == DisplayConfig.PixelFormat.SixteenBpp )
				pixelFmt = "16 bpp";
			else if( pixelFormat == DisplayConfig.PixelFormat.TwentyFourBpp )
				pixelFmt = "24 bpp";
			else if( pixelFormat == DisplayConfig.PixelFormat.ThirtyTwoBpp )
				pixelFmt = "32 bpp";
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}×{1} ({2})", width, height, pixelFmt );
		}


		/// <summary>The empty <see cref="SourceMode"/> structure.</summary>
		public static readonly SourceMode Empty;


		#region Operators

		
		/// <summary></summary>
		public static bool operator ==( SourceMode sourceMode, SourceMode other )
		{
			return sourceMode.Equals( other );
		}

		
		/// <summary></summary>
		public static bool operator !=( SourceMode sourceMode, SourceMode other )
		{
			return !sourceMode.Equals( other );
		}
		
		
		#endregion

	}

}