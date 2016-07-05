using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Contains source information for a single path.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553951%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_SOURCE_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public struct PathSourceInfo : IEquatable<PathSourceInfo>
	{

		/// <summary>Defines the invalid <see cref="ModeInfoIndex"/>: -1.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_MODE_IDX_INVALID" )]
		public const int InvalidModeInfoIndex = -1;

		/// <summary>Defines the invalid <see cref="CloneGroupId"/>: 0xFFFF.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_CLONE_GROUP_INVALID" )]
		public const int InvalidCloneGroupId = 0xffff;

		/// <summary>Defines the invalid <see cref="ModeInfoIndex2"/>: 0xFFFF.</summary>
		[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_SOURCE_MODE_IDX_INVALID" )]
		public const int InvalidModeInfoIndex2 = 0xffff;



		[Flags]
		private enum StatusIndicators : int
		{

			None = 0x00000000,

			/// <summary>The source is in use by at least one active path.</summary>
			[Native( "WinGDI.h", "DISPLAYCONFIG_SOURCE_IN_USE" )]
			InUse = 0x00000001,

		}



		private Luid adapterId;
		private int id;
		private int modeInfoIdx;	// cloneGroupId (16 bits) + sourceModeInfoIdx (16 bits)
		private StatusIndicators status;



		/// <summary>Gets the identifier of the adapter that this source information relates to.</summary>
		public Luid AdapterId { get { return adapterId; } }


		/// <summary>Gets the source identifier on the specified adapter that this path relates to.</summary>
		public int Id { get { return id; } }


		/// <summary>Gets the index into the mode information table that contains the source mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is false.
		/// <para>If source mode information is not available, the value of this property is <see cref="InvalidModeInfoIndex"/>(-1).</para>
		/// </summary>
		public int ModeInfoIndex { get { return modeInfoIdx; } }


		/// <summary>A valid identifier used to show which clone group the path is a member of only when <see cref="PathInfo.SupportsVirtualMode"/> is true.
		/// <para>If this value is invalid, then it must be set to <see cref="InvalidCloneGroupId"/>.</para>
		/// Supported starting in Windows 10.
		/// </summary>
		public int CloneGroupId { get { return modeInfoIdx & 0x0000ffff; } }


		/// <summary>A valid index into the mode array of the <see cref="SourceMode"/> entry that contains the source mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is true.
		/// <para>If there is no entry for this in the mode array, the value of this property is <see cref="InvalidModeInfoIndex2"/>.</para>
		/// Supported starting in Windows 10.
		/// </summary>
		public int ModeInfoIndex2 { get { return modeInfoIdx >> 16; } }


		/// <summary>Gets a value indicating whether the source is in use.</summary>
		public bool InUse { get { return status.HasFlag( StatusIndicators.InUse ); } }


		/// <summary>Returns a hash code for this <see cref="PathSourceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathSourceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return adapterId.GetHashCode() ^ id ^ modeInfoIdx ^ (int)status;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathSourceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( PathSourceInfo other )
		{
			return this.adapterId.Equals( other.adapterId ) && ( id == other.id ) && ( modeInfoIdx == other.modeInfoIdx ) && ( status == other.status );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathSourceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathSourceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is PathSourceInfo ) && this.Equals( (PathSourceInfo)obj );
		}


		/// <summary>The empty (and invalid) <see cref="PathSourceInfo"/> structure.</summary>
		public static readonly PathSourceInfo Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="pathSourceInfo">A <see cref="PathSourceInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( PathSourceInfo pathSourceInfo, PathSourceInfo other )
		{
			return pathSourceInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="pathSourceInfo">A <see cref="PathSourceInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( PathSourceInfo pathSourceInfo, PathSourceInfo other )
		{
			return !pathSourceInfo.Equals( other );
		}

		#endregion Operators

	}

}