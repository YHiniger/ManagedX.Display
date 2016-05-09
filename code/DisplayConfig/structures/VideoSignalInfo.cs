using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Contains information about the video signal for a display.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff554007%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_VIDEO_SIGNAL_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 48 )]
	public struct VideoSignalInfo : IEquatable<VideoSignalInfo>
	{

		private long pixelRate;
		private Rational hSyncFreq;
		private Rational vSyncFreq;
		private Size activeSize;
		private Size totalSize;
		private int videoStandard;	// bits 0..15: videoStandard, 16..21: vSyncFreqDivider, 22..31: reserved
		private ScanLineOrdering scanLineOrdering;


		/// <summary>Gets the pixel clock rate.</summary>
		public long PixelRate { get { return pixelRate; } }

		/// <summary>Gets the horizontal frequency, in hertz (Hz).</summary>
		public Rational HSyncFrequency { get { return hSyncFreq; } }

		/// <summary>Gets the vertical frequency, in hertz (Hz).</summary>
		public Rational VSyncFrequency { get { return vSyncFreq; } }
		
		/// <summary>A <see cref="Size"/> structure structure that specifies the width and height (in pixels) of the active portion of the video signal.</summary>
		public Size ActiveSize { get { return activeSize; } }
		
		/// <summary>A <see cref="Size"/> structure that specifies the width and height (in pixels) of the entire video signal.</summary>
		public Size TotalSize { get { return totalSize; } }
		
		/// <summary>Either:
		/// Additional signal information (supported by WDDM 1.3 and later display miniport drivers running on Windows 8.1 and later), or the video standard (if any) that defines the video signal.
		/// <para>For a list of possible values, see the <see cref="VideoSignalStandard"/> enumerated type.</para>
		/// </summary>
		public int VideoStandard { get { return videoStandard & 0x0000ffff; } }

        /// <summary>The scan-line ordering (for example, progressive or interlaced) of the video signal.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scanline")]
        public ScanLineOrdering ScanlineOrdering { get { return scanLineOrdering; } }


		/// <summary>Returns a hash code for this <see cref="VideoSignalInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="VideoSignalInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return pixelRate.GetHashCode() ^ hSyncFreq.GetHashCode() ^ vSyncFreq.GetHashCode() ^ activeSize.GetHashCode() ^ totalSize.GetHashCode() ^ videoStandard ^ (int)scanLineOrdering;
		}


		/// <summary>Returns a value indicating whether this <see cref="VideoSignalInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="VideoSignalInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="VideoSignalInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( VideoSignalInfo other )
		{
			return ( pixelRate == other.pixelRate ) && hSyncFreq.Equals( other.hSyncFreq ) && vSyncFreq.Equals( other.vSyncFreq ) && ( activeSize == other.activeSize ) && ( totalSize == other.totalSize ) && ( videoStandard == other.videoStandard ) && ( scanLineOrdering == other.scanLineOrdering );
		}


		/// <summary>Returns a value indicating whether this <see cref="VideoSignalInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="VideoSignalInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj != null ) && ( obj is VideoSignalInfo ) && this.Equals( (VideoSignalInfo)obj );
		}


		/// <summary>Returns a string representing this <see cref="VideoSignalInfo"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="VideoSignalInfo"/> structure.</returns>
		public override string ToString()
		{
			return '{' + string.Format( System.Globalization.CultureInfo.InvariantCulture, "Pixel rate={0}, HSyncFrequency={1}, VSyncFrequency={2}, ActiveSize={3}, TotalSize={4}, VideoStandard={5}, ScanlineOrdering={6}", pixelRate, hSyncFreq, vSyncFreq, activeSize, totalSize, videoStandard, scanLineOrdering ) + '}';
		}


		/// <summary>The empty <see cref="VideoSignalInfo"/> structure.</summary>
		public static readonly VideoSignalInfo Empty;


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="videoSignalInfo">A <see cref="VideoSignalInfo"/> structure.</param>
		/// <param name="other">A <see cref="VideoSignalInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( VideoSignalInfo videoSignalInfo, VideoSignalInfo other )
		{
			return videoSignalInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="videoSignalInfo">A <see cref="VideoSignalInfo"/> structure.</param>
		/// <param name="other">A <see cref="VideoSignalInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( VideoSignalInfo videoSignalInfo, VideoSignalInfo other )
		{
			return !videoSignalInfo.Equals( other );
		}


		#endregion

	}

}