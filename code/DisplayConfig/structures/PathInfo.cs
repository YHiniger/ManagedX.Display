using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Describes a single path from a target to a source.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553945%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 72 )]
	public struct PathInfo : IEquatable<PathInfo>
	{

		[Flags]
		private enum StateIndicators : int
		{

			None = 0x00000000,

			/// <summary>Set by QueryDisplayConfig to indicate that the path is active and part of the desktop.
			/// If this flag value is set, SetDisplayConfig attempts to enable this path.</summary>
			[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_ACTIVE" )]
			Active = 0x00000001,

			[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_PREFERRED_UNSCALED" )]
			PreferredUnscaled = 0x00000004,

			/// <summary>Set by QueryDisplayConfig to indicate that the path supports the virtual mode.
			/// <para>Supported starting in Windows 10.</para>
			/// </summary>
			[Native( "WinGDI.h", "DISPLAYCONFIG_PATH_SUPPORT_VIRTUAL_MODE" )]
			SupportVirtualMode = 0x00000008,

		}



		private PathSourceInfo sourceInfo;
		private PathTargetInfo targetInfo;
		private StateIndicators state;



		/// <summary>Gets a <see cref="PathSourceInfo"/> structure which contains the source information for the (display) path.</summary>
		public PathSourceInfo SourceInfo { get { return sourceInfo; } }


		/// <summary>Gets a <see cref="PathTargetInfo"/> structure which contains the target information for the (display) path.</summary>
		public PathTargetInfo TargetInfo { get { return targetInfo; } }


		/// <summary>Gets a value indicating whether the (display) path is active and part of the desktop.</summary>
		public bool Active { get { return state.HasFlag( StateIndicators.Active ); } }


		///// <summary></summary>
		//public bool PreferredUnscaled { get { return state.HasFlag( StateIndicators.PreferredUnscaled ); } }


		/// <summary>Gets a value indicating whether the (display) path supports virtual mode.
		/// <para>Requires Windows 10 or newer.</para>
		/// </summary>
		public bool SupportsVirtualMode { get { return state.HasFlag( StateIndicators.SupportVirtualMode ); } }



		/// <summary>Returns a hash code for this <see cref="PathInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="PathInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return sourceInfo.GetHashCode() ^ targetInfo.GetHashCode() ^ (int)state;
		}


		/// <summary>Returns a value indicating whether this <see cref="PathInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="PathInfo"/> structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> <see cref="PathInfo"/> is equal to this structure.</returns>
		public bool Equals( PathInfo other )
		{
			return sourceInfo.Equals( other.sourceInfo ) && targetInfo.Equals( other.targetInfo ) && ( state == other.state );
		}


		/// <summary>Returns a value indicating whether this <see cref="PathInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="PathInfo"/> structure and is equal to this structure; otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is PathInfo ) && this.Equals( (PathInfo)obj );
		}


		/// <summary>The empty <see cref="PathInfo"/> structure.</summary>
		public static readonly PathInfo Empty;


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

		#endregion Operators

	}

}
