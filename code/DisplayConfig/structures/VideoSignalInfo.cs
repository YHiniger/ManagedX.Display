using System;
using System.Diagnostics.CodeAnalysis;
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

		/// <summary>The pixel clock rate.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly long PixelRate;

		/// <summary>The horizontal frequency, in hertz (Hz).</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Rational HSyncFrequency;

		/// <summary>The vertical frequency, in hertz (Hz).</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Rational VSyncFrequency;

		/// <summary>The size, in pixels, of the active portion of the video signal.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Size ActiveSize;

		/// <summary>The size, in pixels, of the entire video signal.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Size TotalSize;

		private readonly int videoStandard; // bits 0..15: videoStandard, 16..21: vSyncFreqDivider, 22..31: reserved

		/// <summary>The scan-line ordering (for example, progressive or interlaced) of the video signal.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly ScanlineOrdering ScanlineOrdering;



		/// <summary>Gets the video standard (if any) which defines the video signal.</summary>
		public VideoSignalStandard VideoStandard => (VideoSignalStandard)( videoStandard & 0x0000FFFF );


		/// <summary>Additional signal information (supported by WDDM 1.3 and later display miniport drivers running on Windows 8.1 and later).</summary>
		public int VSyncFrequencyDivider => ( videoStandard >> 16 ) & 0x0000003F;


		/// <summary>Returns a hash code for this <see cref="VideoSignalInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="VideoSignalInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return PixelRate.GetHashCode() ^ HSyncFrequency.GetHashCode() ^ VSyncFrequency.GetHashCode() ^ ActiveSize.GetHashCode() ^ TotalSize.GetHashCode() ^ videoStandard ^ (int)ScanlineOrdering;
		}


		/// <summary>Returns a value indicating whether this <see cref="VideoSignalInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="VideoSignalInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="VideoSignalInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( VideoSignalInfo other )
		{
			return ( PixelRate == other.PixelRate ) && HSyncFrequency.Equals( other.HSyncFrequency ) && VSyncFrequency.Equals( other.VSyncFrequency ) && ActiveSize.Equals( other.ActiveSize ) && TotalSize.Equals( other.TotalSize ) && ( videoStandard == other.videoStandard ) && ( ScanlineOrdering == other.ScanlineOrdering );
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
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Pixel rate={0}, HSyncFrequency={1}, VSyncFrequency={2}, ActiveSize={3}, TotalSize={4}, VideoStandard={5}, ScanlineOrdering={6}}}", PixelRate, HSyncFrequency, VSyncFrequency, ActiveSize, TotalSize, videoStandard, ScanlineOrdering );
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