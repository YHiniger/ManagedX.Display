using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Describes the bitmap and color information for a video image, including interlace, copy protection, and pixel aspect ratio information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd407326(v=vs.85).aspx</remarks>
	[Source( "DVDMedia.h", "VIDEOINFOHEADER2" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 88 )]
	public struct VideoInfoHeader2 : IEquatable<VideoInfoHeader2>
	{

		/// <summary>The source video window.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Rect Source;

		/// <summary>The destination video window.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Rect Target;

		/// <summary>Approximate data rate of the video stream, in bits per second.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Bitrate;

		/// <summary>Data error rate, in bit errors per second.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int BitErrorRate;

		/// <summary>The desired average display time of the video frames, in 100-nanosecond units. The actual time per frame may be longer.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public long AverageTimePerFrame;


		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int InterlaceOptions;

		/// <summary>Flag set with the AMCOPYPROTECT_RestrictDuplication value (0x00000001) to indicate that the duplication of the stream should be restricted. If undefined, specify zero or else the connection will be rejected.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int CopyProtectOptions;

		/// <summary>The horizontal aspect ratio. For example, 16 for a 16/9 display.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int HorizontalAspectRatio;

		/// <summary>The vertical aspect ratio. For example, 9 for a 16/9 display.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int VerticalAspectRatio;

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int ControlOptions;

		/// <summary>Reserved for future use; must be zero.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public int Reserved;

		/// <summary>Color and dimension information for the video image bitmap.
		/// If the format block contains a color table or color masks, they immediately follow this member.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public BitmapInfoHeader Header;



		/// <summary>Returns a <see cref="VideoInfoHeader"/> structure corresponding to this <see cref="VideoInfoHeader2"/> structure.</summary>
		/// <returns>Returns a <see cref="VideoInfoHeader"/> structure corresponding to this <see cref="VideoInfoHeader2"/> structure.</returns>
		public VideoInfoHeader ToVideoInfoHeader()
		{
			return new VideoInfoHeader( ref Source, ref Target, Bitrate, BitErrorRate, AverageTimePerFrame, ref Header );
		}





		/// <summary>Returns a value indicating whether this <see cref="VideoInfoHeader2"/> structure is equivalent to another <see cref="VideoInfoHeader2"/> structure.</summary>
		/// <param name="other">A <see cref="VideoInfoHeader2"/> structure.</param>
		/// <returns>Returns true if the <see cref="VideoInfoHeader2"/> structures are equivalent, otherwise returns false.</returns>
		public bool Equals( VideoInfoHeader2 other )
		{
			return
				Source.Equals( other.Source ) &&
				Target.Equals( other.Target ) &&
				Bitrate == other.Bitrate &&
				BitErrorRate == other.BitErrorRate &&
				AverageTimePerFrame == other.AverageTimePerFrame &&
				InterlaceOptions == other.InterlaceOptions &&
				CopyProtectOptions == other.CopyProtectOptions &&
				HorizontalAspectRatio == other.HorizontalAspectRatio &&
				VerticalAspectRatio == other.VerticalAspectRatio &&
				ControlOptions == other.ControlOptions &&
				Reserved == other.Reserved &&
				Header.Equals( other.Header );
		}


		/// <summary>Returns a value indicating whether this <see cref="VideoInfoHeader2"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="VideoInfoHeader2"/> structure which is equivalent to this <see cref="VideoInfoHeader2"/> structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is VideoInfoHeader2 vih2 && this.Equals( vih2 );
		}


		/// <summary>Returns a hash code for this <see cref="VideoInfoHeader2"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="VideoInfoHeader2"/> structure.</returns>
		public override int GetHashCode()
		{
			return Source.GetHashCode() ^ Target.GetHashCode() ^ Bitrate ^ BitErrorRate ^ AverageTimePerFrame.GetHashCode() ^ InterlaceOptions ^ CopyProtectOptions ^ HorizontalAspectRatio ^ VerticalAspectRatio ^ ControlOptions ^ Reserved ^ Header.GetHashCode();
		}

		
		
		/// <summary>The empty <see cref="VideoInfoHeader2"/> structure.</summary>
		public static readonly VideoInfoHeader2 Empty;


		/// <summary>Defines the <see cref="Guid"/> representing the format of this structure.</summary>
		[Source( "UUIDs.h", "FORMAT_VideoInfo2" )]
		public static readonly Guid FormatId = new Guid( "F72A76A0-EB0A-11D0-ACE4-0000C0CC16BA" );


		/// <summary><see cref="VideoInfoHeader2"/> to <see cref="VideoInfoHeader"/> conversion operator.</summary>
		/// <param name="header">A <see cref="VideoInfoHeader2"/> structure.</param>
		public static explicit operator VideoInfoHeader( VideoInfoHeader2 header )
		{
			return new VideoInfoHeader( ref header.Source, ref header.Target, header.Bitrate, header.BitErrorRate, header.AverageTimePerFrame, ref header.Header );
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="header">A <see cref="VideoInfoHeader2"/> structure.</param>
		/// <param name="other">A <see cref="VideoInfoHeader2"/> structure.</param>
		/// <returns>Returns true if the <see cref="VideoInfoHeader2"/> structures are equivalent, otherwise returns false.</returns>
		public static bool operator ==( VideoInfoHeader2 header, VideoInfoHeader2 other )
		{
			return header.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="header">A <see cref="VideoInfoHeader2"/> structure.</param>
		/// <param name="other">A <see cref="VideoInfoHeader2"/> structure.</param>
		/// <returns>Returns true if the <see cref="VideoInfoHeader2"/> structures are not equivalent, otherwise returns false.</returns>
		public static bool operator !=( VideoInfoHeader2 header, VideoInfoHeader2 other )
		{
			return !header.Equals( other );
		}

		#endregion Operators
	}

}