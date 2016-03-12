using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Describes a single path from a target to a source.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553945%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 72 )]
	public struct PathInfo : IEquatable<PathInfo>
	{

		/// <summary>Enumerates flag values used by QueryDisplayConfig and SetDisplayConfig.</summary>
		[Flags]
		private enum PathInfoStates : int
		{

			/// <summary>None.</summary>
			None = 0x00000000,

			/// <summary>Set by QueryDisplayConfig to indicate that the path is active and part of the desktop.
			/// If this flag value is set, SetDisplayConfig attempts to enable this path.</summary>
			Active = 0x00000001

		}


		private PathSourceInfo sourceInfo;
		private PathTargetInfo targetInfo;
		private PathInfoStates states;


		/// <summary>Gets a <see cref="PathSourceInfo"/> structure which contains the source information for the (display) path.</summary>
		public PathSourceInfo SourceInfo { get { return sourceInfo; } }


		/// <summary>Gets a <see cref="PathTargetInfo"/> structure which contains the target information for the (display) path.</summary>
		public PathTargetInfo TargetInfo { get { return targetInfo; } }


		/// <summary>Gets a value indicating whether the (display) path is active and part of the desktop.</summary>
		public bool Active { get { return states.HasFlag( PathInfoStates.Active ); } }


		/// <summary>Returns a hash code for this <see cref="PathInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return sourceInfo.GetHashCode() ^ targetInfo.GetHashCode() ^ (int)states;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathInfo"/> structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> <see cref="PathInfo"/> is equal to this structure.</returns>
		public bool Equals( PathInfo other )
		{
			return sourceInfo.Equals( other.sourceInfo ) && targetInfo.Equals( other.targetInfo ) && ( states == other.states );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathInfo"/> structure and is equal to this structure; otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is PathInfo ) && this.Equals( (PathInfo)obj );
		}


		/// <summary>The empty <see cref="PathInfo"/> structure.</summary>
		public static readonly PathInfo Empty = new PathInfo();


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="pathInfo">A <see cref="PathInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( PathInfo pathInfo, PathInfo other )
		{
			return pathInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="pathInfo">A <see cref="PathInfo"/> structure.</param>
		/// <param name="other">A <see cref="PathInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( PathInfo pathInfo, PathInfo other )
		{
			return !pathInfo.Equals( other );
		}

		
		#endregion

	}

}
