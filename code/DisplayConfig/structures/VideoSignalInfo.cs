using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information about the video signal for a display.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff554007%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_VIDEO_SIGNAL_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 48 )]
	public struct VideoSignalInfo : IEquatable<VideoSignalInfo>
	{

		private readonly long pixelRate;
		private readonly Rational hSyncFreq;
		private readonly Rational vSyncFreq;
		private readonly Size activeSize;
		private readonly Size totalSize;
		private readonly int videoStandard;	// bits 0..15: videoStandard, 16..21: vSyncFreqDivider, 22..31: reserved
		private readonly ScanLineOrdering scanLineOrdering;



		/// <summary>Gets the pixel clock rate.</summary>
		public long PixelRate => pixelRate;


		/// <summary>Gets the horizontal frequency, in hertz (Hz).</summary>
		public Rational HSyncFrequency => hSyncFreq;


		/// <summary>Gets the vertical frequency, in hertz (Hz).</summary>
		public Rational VSyncFrequency => vSyncFreq;
		

		/// <summary>Gets the size, in pixels, of the active portion of the video signal.</summary>
		public Size ActiveSize => activeSize;
		

		/// <summary>Gets the size, in pixels, of the entire video signal.</summary>
		public Size TotalSize => totalSize;
		

		/// <summary>Gets the video standard (if any) which defines the video signal.</summary>
		public VideoSignalStandard VideoStandard => (VideoSignalStandard)( videoStandard & 0x0000ffff );


		/// <summary>Additional signal information (supported by WDDM 1.3 and later display miniport drivers running on Windows 8.1 and later).</summary>
		public int VSyncFrequencyDivider => ( videoStandard >> 16 ) & 0x0000003f;


        /// <summary>The scan-line ordering (for example, progressive or interlaced) of the video signal.</summary>
        public ScanLineOrdering ScanLineOrdering => scanLineOrdering;


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
			return ( pixelRate == other.pixelRate ) && hSyncFreq.Equals( other.hSyncFreq ) && vSyncFreq.Equals( other.vSyncFreq ) && activeSize.Equals( other.activeSize ) && totalSize.Equals( other.totalSize ) && ( videoStandard == other.videoStandard ) && ( scanLineOrdering == other.scanLineOrdering );
		}


		/// <summary>Returns a value indicating whether this <see cref="VideoSignalInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="VideoSignalInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj != null ) && ( obj is VideoSignalInfo vsi ) && this.Equals( vsi );
		}


		/// <summary>Returns a string representing this <see cref="VideoSignalInfo"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="VideoSignalInfo"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Pixel rate={0}, HSyncFrequency={1}, VSyncFrequency={2}, ActiveSize={3}, TotalSize={4}, VideoStandard={5}, ScanlineOrdering={6}}}", pixelRate, hSyncFreq, vSyncFreq, activeSize, totalSize, videoStandard, scanLineOrdering );
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

		#endregion Operators

	}

}