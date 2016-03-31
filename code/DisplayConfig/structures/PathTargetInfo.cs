using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Contains target information for a single path.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553954%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 48 )]
	public struct PathTargetInfo : IEquatable<PathTargetInfo>
	{

		/// <summary>Defines the invalid <see cref="ModeInfoIndex"/>: -1.
		/// <para>This is the same value as <see cref="PathSourceInfo.InvalidModeInfoIndex"/>.</para>
		/// </summary>
		public const int InvalidModeInfoIndex = PathSourceInfo.InvalidModeInfoIndex;


		private Luid adapterId;
		private int id;
		private int modeInfoIdx;
		private VideoOutputTechnology outputTechnology;
		private DisplayRotation rotation;
		private Scaling scaling;
		private Rational refreshRate;
		private ScanlineOrdering scanLineOrdering;
		[MarshalAs( UnmanagedType.Bool )]
		private bool targetAvailable;
		private PathTargetInfoStatus statusFlags;


		/// <summary>Gets the identifier of the adapter that the path is on.</summary>
		public Luid AdapterId { get { return adapterId; } }


		/// <summary>Gets the target identifier on the specified adapter that this path relates to.</summary>
		public int Id { get { return id; } }


		/// <summary>Gets the index into the mode information table that contains the target mode information for this path.
		/// If target mode information is not available, the value of this property is <see cref="InvalidModeInfoIndex"/>(-1).</summary>
		public int ModeInfoIndex { get { return modeInfoIdx; } }


		/// <summary>Gets the target's connector type.</summary>
		public VideoOutputTechnology OutputTechnology { get { return outputTechnology; } }


		/// <summary>Gets a value which specifies the rotation of the target.</summary>
		public DisplayRotation Rotation { get { return rotation; } }


		/// <summary>Gets a value which specifies how the source image is scaled to the target.</summary>
		public Scaling Scaling { get { return scaling; } }


		/// <summary>Gets a <see cref="Rational"/> structure which specifies the refresh rate of the target.
		/// If the caller specifies target mode information, the operating system will instead use the refresh rate that is stored in the vSyncFreq member of the <see cref="VideoSignalInfo"/> structure.
		/// In this case, the caller specifies this value in the targetVideoSignalInfo member of the <see cref="TargetMode"/> structure.
		/// A refresh rate with both the numerator and denominator set to zero (<see cref="Rational.Empty"/>) indicates that the caller does not specify a refresh rate and the operating system should use the most optimal refresh rate available.
		/// For this case, in a call to the SetDisplayConfig function, the caller must set the scanLineOrdering member to <see cref="ManagedX.Display.ScanlineOrdering.Unspecified"/>; otherwise, SetDisplayConfig fails.</summary>
		public Rational RefreshRate { get { return refreshRate; } }


        /// <summary>Gets a value that specifies the scan-line ordering of the output on the target.
        /// If the caller specifies target mode information, the operating system will instead use the scan-line ordering that is stored in the scanLineOrdering member of the <see cref="VideoSignalInfo"/> structure.
        /// In this case, the caller specifies this value in the targetVideoSignalInfo member of the <see cref="TargetMode"/> structure.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scanline")]
        public ScanlineOrdering ScanlineOrdering { get { return scanLineOrdering; } }


		/// <summary>Gets a value that specifies whether the target is available.
		/// true indicates that the target is available.
		/// Because the asynchronous nature of display topology changes when a monitor is removed, a path might still be marked as active even though the monitor has been removed.
		/// In such a case, targetAvailable could be false for an active path.
		/// This is typically a transient situation that will change after the operating system takes action on the monitor removal.</summary>
		public bool IsTargetAvailable { get { return targetAvailable; } }


		/// <summary>Gets a bitwise OR of flag values that indicates the status of the target.</summary>
		public PathTargetInfoStatus State { get { return statusFlags; } }



		/// <summary>Returns a hash code for this <see cref="PathTargetInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathTargetInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return adapterId.GetHashCode() ^ id ^ modeInfoIdx ^ (int)outputTechnology ^ (int)rotation ^ (int)scaling ^ refreshRate.GetHashCode() ^ (int)scanLineOrdering ^ ( targetAvailable ? -1 : 0 ) ^ (int)statusFlags;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathTargetInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathTargetInfo"/> structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> structure equals this <see cref="PathTargetInfo"/> structure, otherwise returns false.</returns>
		public bool Equals( PathTargetInfo other )
		{
			return adapterId.Equals( other.adapterId ) && ( id == other.id ) && ( modeInfoIdx == other.modeInfoIdx ) && ( outputTechnology == other.outputTechnology ) && ( rotation == other.rotation ) && ( scaling == other.scaling ) && refreshRate.Equals( other.refreshRate ) && ( scanLineOrdering == other.scanLineOrdering ) && ( targetAvailable == other.targetAvailable ) && ( statusFlags == other.statusFlags );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathTargetInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathTargetInfo"/> structure that equals this structure; otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is PathTargetInfo ) && this.Equals( (PathTargetInfo)obj );
		}


		/// <summary>The empty <see cref="PathTargetInfo"/> structure.</summary>
		public static readonly PathTargetInfo Empty = new PathTargetInfo();


		#region Operators


		/// <summary>Equality operator.</summary>
		/// <param name="pathTargetInfo">A <see cref="PathTargetInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathTargetInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( PathTargetInfo pathTargetInfo, PathTargetInfo other )
		{
			return pathTargetInfo.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="pathTargetInfo">A <see cref="PathTargetInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathTargetInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( PathTargetInfo pathTargetInfo, PathTargetInfo other )
		{
			return !pathTargetInfo.Equals( other );
		}


		#endregion

	}

}