using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>Describes the bitmap and color information for a video image.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd407325(v=vs.85).aspx</remarks>
	[Source( "AMVideo.h", "VIDEOINFOHEADER" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 88 )]
	public struct VideoInfoHeader : IEquatable<VideoInfoHeader>
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

		/// <summary>Color and dimension information for the video image bitmap.
		/// If the format block contains a color table or color masks, they immediately follow this member.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public BitmapInfoHeader Header;



		internal VideoInfoHeader( ref Rect source, ref Rect target, int bitRate, int bitErrorRate, long avgTimePerFrame, ref BitmapInfoHeader header )
		{
			Source = source;
			Target = target;
			Bitrate = bitRate;
			BitErrorRate = bitErrorRate;
			AverageTimePerFrame = avgTimePerFrame;
			Header = header;
		}



		/// <summary>Returns a value indicating whether this <see cref="VideoInfoHeader"/> structure is equivalent to another <see cref="VideoInfoHeader"/> structure.</summary>
		/// <param name="other">A <see cref="VideoInfoHeader"/> structure.</param>
		/// <returns>Returns true if the <see cref="VideoInfoHeader"/> structures are equivalent, otherwise returns false.</returns>
		public bool Equals( VideoInfoHeader other )
		{
			return
				Source.Equals( other.Source ) &&
				Target.Equals( other.Target ) &&
				Bitrate == other.Bitrate &&
				BitErrorRate == other.BitErrorRate &&
				AverageTimePerFrame == other.AverageTimePerFrame &&
				Header.Equals( other.Header );
		}


		/// <summary>Returns a value indicating whether this <see cref="VideoInfoHeader"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="VideoInfoHeader"/> structure which is equivalent to this <see cref="VideoInfoHeader"/> structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is VideoInfoHeader vih && this.Equals( vih );
		}


		/// <summary>Returns a hash code for this <see cref="VideoInfoHeader"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="VideoInfoHeader"/> structure.</returns>
		public override int GetHashCode()
		{
			return Source.GetHashCode() ^ Target.GetHashCode() ^ Bitrate ^ BitErrorRate ^ AverageTimePerFrame.GetHashCode() ^ Header.GetHashCode();
		}



		/// <summary>The empty <see cref="VideoInfoHeader"/> structure.</summary>
		public static readonly VideoInfoHeader Empty;


		/// <summary>A <see cref="Guid"/> representing the format of this structure.</summary>
		[Source( "UUIDs.h", "FORMAT_VideoInfo" )]
		public static readonly Guid FormatId = new Guid( "05589F80-C356-11CE-BF01-00AA0055595A" );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="header">A <see cref="VideoInfoHeader"/> structure.</param>
		/// <param name="other">A <see cref="VideoInfoHeader"/> structure.</param>
		/// <returns>Returns true if the <see cref="VideoInfoHeader"/> structures are equivalent, otherwise returns false.</returns>
		public static bool operator ==( VideoInfoHeader header, VideoInfoHeader other )
		{
			return header.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="header">A <see cref="VideoInfoHeader"/> structure.</param>
		/// <param name="other">A <see cref="VideoInfoHeader"/> structure.</param>
		/// <returns>Returns true if the <see cref="VideoInfoHeader"/> structures are not equivalent, otherwise returns false.</returns>
		public static bool operator !=( VideoInfoHeader header, VideoInfoHeader other )
		{
			return !header.Equals( other );
		}

		#endregion Operators

	}

}