using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics
{

	/// <summary>Identifies a display device (source or target).</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	public struct DisplayDeviceId : IEquatable<DisplayDeviceId>
	{

		/// <summary>The identifier of the display adapter associated with the display device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Luid AdapterId;

		/// <summary>The source or target identifier for the display device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Id;



		//internal DisplayDeviceId( Luid adapterId, int id )
		//{
		//	AdapterId = adapterId;
		//	Id = id;
		//}



		/// <summary>Returns a hash code for this <see cref="DisplayDeviceId"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DisplayDeviceId"/> structure.</returns>
		public override int GetHashCode()
		{
			return AdapterId.GetHashCode() ^ Id;
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDeviceId"/> structure is equivalent to another <see cref="DisplayDeviceId"/> structure.</summary>
		/// <param name="other">A <see cref="DisplayDeviceId"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		public bool Equals( DisplayDeviceId other )
		{
			return AdapterId.Equals( other.AdapterId ) && ( Id == other.Id );
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDeviceId"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DisplayDeviceId"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is DisplayDeviceId id && this.Equals( id );
		}


		/// <summary>Returns a string representing this <see cref="DisplayDeviceId"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="DisplayDeviceId"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}:{1:X8}", AdapterId, Id );
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="displayDeviceId">A <see cref="DisplayDeviceId"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceId"/> structure.</param>
		/// <returns>Returns true if the specified structures are equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( DisplayDeviceId displayDeviceId, DisplayDeviceId other )
		{
			return displayDeviceId.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="displayDeviceId">A <see cref="DisplayDeviceId"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceId"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( DisplayDeviceId displayDeviceId, DisplayDeviceId other )
		{
			return !displayDeviceId.Equals( other );
		}

		#endregion Operators

	}

}