using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Contains target information for a single path.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553954%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_TARGET_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 48 )]
	public struct PathTargetInfo : IEquatable<PathTargetInfo>
	{

		/// <summary>Defines the invalid <see cref="ModeInfoIndex"/>: -1.</summary>
		public const int InvalidModeInfoIndex = PathSourceInfo.InvalidModeInfoIndex;


		/// <summary>Defines the invalid <see cref="DesktopModeInfoIndex"/>: 0xFFFF.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_DESKTOP_IMAGE_IDX_INVALID" )]
		public const int InvalidDesktopModeInfoIndex = 0x0000FFFF;


		/// <summary>Defines the invalid <see cref="ModeInfoIndex2"/>: 0xFFFF.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_TARGET_MODE_IDX_INVALID" )]
		public const int InvalidModeInfoIndex2 = 0x0000FFFF;



		/// <summary>The identifier of the adapter that the path is on.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Luid AdapterId;

		/// <summary>The target identifier on the specified adapter that this path relates to.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Id;

		private readonly int modeInfoIdx;   // desktopModeInfoIndex (16 bits), targetModeInfoIndex (16 bits)

		/// <summary>The target's connector type.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly VideoOutputTechnology OutputTechnology;

		/// <summary>Specifies the rotation of the target.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly DisplayRotation Rotation;

		/// <summary>Specifies how the source image is scaled to the target.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Scaling Scaling;

		/// <summary>Specifies the refresh rate of the target.
		/// <para>
		/// If the caller specifies target mode information, the operating system will instead use the refresh rate that is stored in the vSyncFreq member of the <see cref="VideoSignalInfo"/> structure.
		/// In this case, the caller specifies this value in the targetVideoSignalInfo member of the <see cref="TargetMode"/> structure.
		/// </para>
		/// A refresh rate with both the numerator and denominator set to zero (<see cref="Rational.Empty"/>) indicates that the caller does not specify a refresh rate and the operating system should use the most optimal refresh rate available.
		/// For this case, in a call to the SetDisplayConfig function, the caller must set the scanLineOrdering member to <see cref="Graphics.ScanlineOrdering.Unspecified"/>; otherwise, SetDisplayConfig fails.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Rational RefreshRate;

		/// <summary>Specifies the scan-line ordering of the output on the target.
		/// <para>If the caller specifies target mode information, the operating system will instead use the scan-line ordering that is stored in the scanLineOrdering member of the <see cref="VideoSignalInfo"/> structure.</para>
		/// In this case, the caller specifies this value in the targetVideoSignalInfo member of the <see cref="TargetMode"/> structure.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly ScanlineOrdering ScanLineOrdering;

		/// <summary>Indicates whether the target is available.
		/// true indicates that the target is available.
		/// Because the asynchronous nature of display topology changes when a monitor is removed, a path might still be marked as active even though the monitor has been removed.
		/// In such a case, targetAvailable could be false for an active path.
		/// This is typically a transient situation that will change after the operating system takes action on the monitor removal.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[MarshalAs( UnmanagedType.Bool )]
		public readonly bool IsTargetAvailable;

		/// <summary>The status of the target.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly PathTargetInfoStateIndicators State;



		/// <summary>Gets the index into the mode information table that contains the target mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is false.
		/// <para>If target mode information is not available, the value of this property is <see cref="InvalidModeInfoIndex"/>(-1).</para>
		/// </summary>
		public int ModeInfoIndex => modeInfoIdx;


		/// <summary>Gets the index into the mode array of the <see cref="DesktopImageInfo"/> entry that contains the desktop mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is true.
		/// <para>If there is no entry for this in the mode array, the value of this property is <see cref="InvalidDesktopModeInfoIndex"/>.</para>
		/// Requires Windows 10 or newer.
		/// </summary>
		public int DesktopModeInfoIndex => modeInfoIdx & 0x0000FFFF;


		/// <summary>Gets the index into the mode array of the <see cref="TargetMode"/> entry which contains the target mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is true.
		/// <para>If there is no entry for this in the mode array, the value of targetModeInfoIdx is <see cref="InvalidModeInfoIndex2"/>.</para>
		/// Requires Windows 10 or newer.
		/// </summary>
		public int ModeInfoIndex2 => modeInfoIdx >> 16;



		/// <summary>Returns a hash code for this <see cref="PathTargetInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathTargetInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return AdapterId.GetHashCode() ^ Id ^ modeInfoIdx ^ (int)OutputTechnology ^ (int)Rotation ^ (int)Scaling ^ RefreshRate.GetHashCode() ^ (int)ScanLineOrdering ^ ( IsTargetAvailable ? -1 : 0 ) ^ (int)State;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathTargetInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathTargetInfo"/> structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> structure equals this <see cref="PathTargetInfo"/> structure, otherwise returns false.</returns>
		public bool Equals( PathTargetInfo other )
		{
			return AdapterId.Equals( other.AdapterId ) && ( Id == other.Id ) && ( modeInfoIdx == other.modeInfoIdx ) && ( OutputTechnology == other.OutputTechnology ) && ( Rotation == other.Rotation ) && ( Scaling == other.Scaling ) && RefreshRate.Equals( other.RefreshRate ) && ( ScanLineOrdering == other.ScanLineOrdering ) && ( IsTargetAvailable == other.IsTargetAvailable ) && ( State == other.State );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathTargetInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathTargetInfo"/> structure that equals this structure; otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is PathTargetInfo pti ) && this.Equals( pti );
		}


		/// <summary>The empty <see cref="PathTargetInfo"/> structure.</summary>
		public static readonly PathTargetInfo Empty;


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

		#endregion Operators

	}

}