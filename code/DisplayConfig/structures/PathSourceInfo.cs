using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553951%28v=vs.85%29.aspx


	/// <summary>Contains source information for a single path.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public struct PathSourceInfo : IEquatable<PathSourceInfo>
	{

		/// <summary>Defines the invalid <see cref="ModeInfoIndex"/>: -1.</summary>
		public const int InvalidModeInfoIndex = -1;


		/// <summary>Enumerates a <see cref="PathSourceInfo"/>'s status flags.</summary>
		[Flags]
		private enum PathSourceInfoStatus : int
		{

			/// <summary></summary>
			None = 0x00000000,

			/// <summary>The source is in use by at least one active path.</summary>
			InUse = 0x00000001

		}


		private Luid adapterId;
		private int id;
		private int modeInfoIdx;
		private PathSourceInfoStatus statusFlags;


		/// <summary>Gets the identifier of the adapter that this source information relates to.</summary>
		public Luid AdapterId { get { return adapterId; } }


		/// <summary>Gets the source identifier on the specified adapter that this path relates to.</summary>
		public int Id { get { return id; } }


		/// <summary>Gets the index into the mode information table that contains the source mode information for this path.
		/// <para>If source mode information is not available, the value of this property is <see cref="InvalidModeInfoIndex"/>(-1).</para>
		/// </summary>
		public int ModeInfoIndex { get { return modeInfoIdx; } }


		/// <summary>Gets a value indicating whether the source is in use.</summary>
		public bool InUse { get { return statusFlags.HasFlag( PathSourceInfoStatus.InUse ); } }


		/// <summary>Returns a hash code for this <see cref="PathSourceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathSourceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return adapterId.GetHashCode() ^ id ^ modeInfoIdx ^ (int)statusFlags;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathSourceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( PathSourceInfo other )
		{
			return this.adapterId.Equals( other.adapterId ) && ( id == other.id ) && ( modeInfoIdx == other.modeInfoIdx ) && ( statusFlags == other.statusFlags );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathSourceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathSourceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is PathSourceInfo ) && this.Equals( (PathSourceInfo)obj );
		}


		/// <summary>The empty (and invalid) <see cref="PathSourceInfo"/> structure.</summary>
		public static readonly PathSourceInfo Empty = new PathSourceInfo();


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

	
		#endregion

	}

}