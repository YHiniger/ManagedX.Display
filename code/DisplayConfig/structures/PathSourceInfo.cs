using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Contains source information for a single path.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553951%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_SOURCE_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public struct PathSourceInfo : IEquatable<PathSourceInfo>
	{

		/// <summary>Defines the invalid <see cref="ModeInfoIndex"/>: -1.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_MODE_IDX_INVALID" )]
		public const int InvalidModeInfoIndex = -1;

		[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_CLONE_GROUP_INVALID" )]
		private const int InvalidCloneGroupId = 0x0000FFFF;

		[Source( "WinGDI.h", "DISPLAYCONFIG_PATH_SOURCE_MODE_IDX_INVALID" )]
		private const int InvalidModeInfoIndex2 = 0x0000FFFF;



		[Flags]
		private enum StatusIndicators : int
		{

			None = 0x00000000,

			/// <summary>The source is in use by at least one active path.</summary>
			[Source( "WinGDI.h", "DISPLAYCONFIG_SOURCE_IN_USE" )]
			InUse = 0x00000001,

		}


		/// <summary>The identifier of the adapter that this source information relates to, and the source identifier on the specified adapter that this path relates to.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly DisplayDeviceId Identifier;
		private readonly int modeInfoIdx;	// cloneGroupId (16 bits) + sourceModeInfoIdx (16 bits)
		private readonly StatusIndicators status;



		/// <summary>Gets the index into the mode information table that contains the source mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is false.
		/// <para>If source mode information is not available, the value of this property is <see cref="InvalidModeInfoIndex"/>(-1).</para>
		/// </summary>
		public int ModeInfoIndex => modeInfoIdx;


		/// <summary>A valid identifier used to show which clone group the path is a member of only when <see cref="PathInfo.SupportsVirtualMode"/> is true.
		/// <para>If this value is invalid, then it must be set to <see cref="InvalidCloneGroupId"/>.</para>
		/// Supported starting in Windows 10.
		/// </summary>
		public int CloneGroupId
		{
			get
			{
				var id = modeInfoIdx & 0x0000FFFF;
				return id == InvalidCloneGroupId ? InvalidModeInfoIndex : id;
			}
		}


		/// <summary>A valid index into the mode array of the <see cref="SourceMode"/> entry that contains the source mode information for this path only when <see cref="PathInfo.SupportsVirtualMode"/> is true.
		/// <para>If there is no entry for this in the mode array, the value of this property is <see cref="InvalidModeInfoIndex2"/>.</para>
		/// Supported starting in Windows 10.
		/// </summary>
		public int ModeInfoIndex2
		{
			get
			{
				var index = modeInfoIdx >> 16;
				return index == InvalidModeInfoIndex2 ? InvalidModeInfoIndex : index;
			}
		}


		/// <summary>Gets a value indicating whether the source is in use by at least one active path.</summary>
		public bool InUse => status.HasFlag( StatusIndicators.InUse );


		/// <summary>Returns a hash code for this <see cref="PathSourceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathSourceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return Identifier.GetHashCode() ^ modeInfoIdx ^ (int)status;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathSourceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( PathSourceInfo other )
		{
			return Identifier.Equals( other.Identifier ) && ( modeInfoIdx == other.modeInfoIdx ) && ( status == other.status );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathSourceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathSourceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is PathSourceInfo info && this.Equals( info );
		}


		/// <summary>The empty (and invalid) <see cref="PathSourceInfo"/> structure.</summary>
		public static readonly PathSourceInfo Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="pathSourceInfo">A <see cref="PathSourceInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( PathSourceInfo pathSourceInfo, PathSourceInfo other )
		{
			return pathSourceInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="pathSourceInfo">A <see cref="PathSourceInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( PathSourceInfo pathSourceInfo, PathSourceInfo other )
		{
			return !pathSourceInfo.Equals( other );
		}

		#endregion Operators

	}

}