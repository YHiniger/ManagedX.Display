using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{

	/// <summary>Contains information about the dimensions and color format of a device-independent bitmap (DIB).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd318229(v=vs.85).aspx</remarks>
	[Win32.Source( "WinGDI.h", "BITMAPINFOHEADER" )]
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 40 )]
	public struct BitmapInfoHeader : IEquatable<BitmapInfoHeader>
	{

		/// <summary>The size, in bytes, of a <see cref="BitmapInfoHeader"/> structure: must be set to 40.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int StructSize;

		/// <summary>The width, in pixels, of the bitmap.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Width;

		/// <summary>The height, in pixels, of the bitmap.
		/// <para>For uncompressed RGB bitmaps, if Height is positive, the bitmap is a bottom-up DIB with the origin at the lower left corner. If Height is negative, the bitmap is a top-down DIB with the origin at the upper left corner.</para>
		/// For YUV bitmaps, the bitmap is always top-down, regardless of the sign of Height. Decoders should offer YUV formats with postive Height, but for backward compatibility they should accept YUV formats with either positive or negative Height.
		/// <para>For compressed formats, Height must be positive, regardless of image orientation.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Height;

		/// <summary>The number of planes for the target device; must be set to 1.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public short PlaneCount;

		/// <summary>Number of bits per pixel (bpp).
		/// <para>For uncompressed formats, this value is the average number of bits per pixel.</para>
		/// For compressed formats, this value is the implied bit depth of the uncompressed image, after the image has been decoded.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public short BitsPerPixel;

		/// <summary>For compressed video and YUV formats, this member is a FOURCC code, specified as a DWORD in little-endian order.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Compression;

		/// <summary>The size, in bytes, of the image; can be set to 0 for uncompressed RGB bitmaps.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int ImageSize;

		/// <summary>The horizontal resolution, in pixels per meter, of the target device for the bitmap.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int HorizontalResolution;

		/// <summary>The vertical resolution, in pixels per meter, of the target device for the bitmap.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int VerticalResolution;

		/// <summary>The number of color indices in the color table that are actually used by the bitmap.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int UsedColorCount;

		/// <summary>The number of color indices that are considered important for displaying the bitmap, or zero if all colors are important.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int ImportantColorCount;



		/// <summary>Initializes a new <see cref="BitmapInfoHeader"/> structure.</summary>
		/// <param name="width">The width, in pixels, of the bitmap.</param>
		/// <param name="height">The height, in pixels, of the bitmap.
		/// <para>For uncompressed RGB bitmaps, if Height is positive, the bitmap is a bottom-up DIB with the origin at the lower left corner. If Height is negative, the bitmap is a top-down DIB with the origin at the upper left corner.</para>
		/// For YUV bitmaps, the bitmap is always top-down, regardless of the sign of Height. Decoders should offer YUV formats with postive Height, but for backward compatibility they should accept YUV formats with either positive or negative Height.
		/// <para>For compressed formats, Height must be positive, regardless of image orientation.</para>
		/// </param>
		/// <param name="bitsPerPixel">Number of bits per pixel (bpp).
		/// <para>For uncompressed formats, this value is the average number of bits per pixel.</para>
		/// For compressed formats, this value is the implied bit depth of the uncompressed image, after the image has been decoded.
		/// </param>
		/// <param name="compression">For compressed video and YUV formats, this member is a FOURCC code, specified as a DWORD in little-endian order.</param>
		/// <param name="imageSize">The size, in bytes, of the image; can be set to 0 for uncompressed RGB bitmaps.</param>
		/// <param name="horizontalResolution">The horizontal resolution, in pixels per meter, of the target device for the bitmap.</param>
		/// <param name="verticalResolution">The vertical resolution, in pixels per meter, of the target device for the bitmap.</param>
		public BitmapInfoHeader( int width, int height, short bitsPerPixel, int compression, int imageSize, int horizontalResolution, int verticalResolution )
		{
			StructSize = 40;
			Width = width;
			Height = height;
			PlaneCount = 1;
			BitsPerPixel = bitsPerPixel;
			Compression = compression;
			ImageSize = imageSize;
			HorizontalResolution = horizontalResolution;
			VerticalResolution = verticalResolution;
			UsedColorCount = 0;
			ImportantColorCount = 0;
		}



		/// <summary>Returns a value indicating whether this <see cref="BitmapInfoHeader"/> structure is equivalent to another <see cref="BitmapInfoHeader"/> structure.</summary>
		/// <param name="other">A <see cref="BitmapInfoHeader"/> structure.</param>
		/// <returns>Returns true if this <see cref="BitmapInfoHeader"/> structure and the <paramref name="other"/> structure are equivalent, otherwise returns false.</returns>
		public bool Equals( BitmapInfoHeader other )
		{
			return 
				StructSize == other.StructSize && 
				Width == other.Width && 
				Height == other.Height && 
				PlaneCount == other.PlaneCount && 
				BitsPerPixel == other.BitsPerPixel && 
				Compression == other.Compression && 
				ImageSize == other.ImageSize && 
				HorizontalResolution == other.HorizontalResolution && 
				VerticalResolution == other.VerticalResolution && 
				UsedColorCount == other.UsedColorCount && 
				ImportantColorCount == other.ImportantColorCount;
		}


		/// <summary>Returns a value indicating whether this <see cref="BitmapInfoHeader"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="BitmapInfoHeader"/> structure which is equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is BitmapInfoHeader bih && this.Equals( bih );
		}


		/// <summary>Returns a hash code for this <see cref="BitmapInfoHeader"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="BitmapInfoHeader"/> structure.</returns>
		public override int GetHashCode()
		{
			return StructSize ^ Width ^ Height ^ PlaneCount ^ BitsPerPixel ^ Compression ^ ImageSize ^ HorizontalResolution ^ VerticalResolution ^ UsedColorCount ^ ImportantColorCount;
		}


		/// <summary>The empty (and invalid!) <see cref="BitmapInfoHeader"/> structure.</summary>
		public static readonly BitmapInfoHeader Empty;


		/// <summary>The default <see cref="BitmapInfoHeader"/> structure.</summary>
		public static readonly BitmapInfoHeader Default = new BitmapInfoHeader()
		{
			StructSize = 40,
			PlaneCount = 1
		};


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="header">A <see cref="BitmapInfoHeader"/> structure.</param>
		/// <param name="other">A <see cref="BitmapInfoHeader"/> structure.</param>
		/// <returns>Returns true if the specified structures are equivalent, otherwise returns false.</returns>
		public static bool operator ==( BitmapInfoHeader header, BitmapInfoHeader other )
		{
			return header.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="header">A <see cref="BitmapInfoHeader"/> structure.</param>
		/// <param name="other">A <see cref="BitmapInfoHeader"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equivalent, otherwise returns false.</returns>
		public static bool operator !=( BitmapInfoHeader header, BitmapInfoHeader other )
		{
			return !header.Equals( other );
		}

		#endregion Operators

	}

}