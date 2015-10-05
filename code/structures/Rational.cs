using System;
using System.Runtime.InteropServices;

// Comments checked (2015/09/24)


namespace ManagedX.Display
{

	/// <summary>Represents a rational number (unsigned).
	/// <para>This structure is also used by DXGI.</para>
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 8 )]
	public struct Rational : IEquatable<Rational>, IComparable<Rational>
	{

		private uint numerator;
		private uint denominator;


		/// <summary>Initializes a new <see cref="Rational"/> structure.</summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		[CLSCompliant( false )]
		public Rational( uint numerator, uint denominator )
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		/// <summary>Initializes a new <see cref="Rational"/> structure.</summary>
		/// <param name="numerator">The numerator; must be greater than or equal to 0.</param>
		/// <param name="denominator">The denominator; must be greater than or equal to 0.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public Rational( int numerator, int denominator )
		{
			if( numerator < 0 )
				throw new ArgumentOutOfRangeException( "numerator" );
			
			if( denominator < 0 )
				throw new ArgumentOutOfRangeException( "denominator" );
			
			this.numerator = (uint)numerator;
			this.denominator = (uint)denominator;
		}


		/// <summary>Returns a double-precision floating-point value representing this <see cref="Rational"/> structure.</summary>
		/// <returns>Returns a double-precision floating-point value representing this <see cref="Rational"/> structure.</returns>
		public double ToDouble()
		{
			if( denominator == 0u )
				return 1.0;
			// this follows the rule defined in DXGI.
			
			return (double)numerator / (double)denominator;
		}


		/// <summary>Returns a single-precision floating-point value representing this <see cref="Rational"/> structure.</summary>
		/// <returns>Returns a single-precision floating-point value representing this <see cref="Rational"/> structure.</returns>
		public float ToSingle()
		{
			if( denominator == 0u )
				return 1.0f;
			// this follows the "rule" defined in DXGI.

			return (float)numerator / (float)denominator;
		}


		/// <summary>Returns a 32-bit integer representing this <see cref="Rational"/> structure.</summary>
		/// <returns>Returns a 32-bit integer representing this <see cref="Rational"/> structure.</returns>
		public int ToInt32()
		{
			if( denominator == 0u )
				return 1;
			// this follows the "rule" defined in DXGI.

			return (int)( numerator / denominator );
		}


		/// <summary>Returns a hash code for this <see cref="Rational"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Rational"/> structure.</returns>
		public override int GetHashCode()
		{
			return ( numerator ^ denominator ).GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Rational"/> stucture equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( Rational other )
		{
			return ( numerator == other.numerator ) && ( denominator == other.denominator );
		}


		/// <summary>Returns a value indicating whether this <see cref="Rational"/> stucture is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Rational"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Rational ) && this.Equals( (Rational)obj );
		}


		/// <summary>Returns a value indicating whether this <see cref="Rational"/> stucture is equal to, greater or smaller than another structure of the same type.</summary>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns -1 if this structure is smaller than the <paramref name="other"/> structure, +1 if it's greater, or 0 if they are equal.</returns>
		public int CompareTo( Rational other )
		{
			return this.ToDouble().CompareTo( other.ToDouble() );
		}


		/// <summary>Returns a string representing this <see cref="Rational"/> structure, in the form "numerator:denominator".</summary>
		/// <returns>Returns a string representing this <see cref="Rational"/> structure, in the form "numerator:denominator".</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0}:{1}", numerator, denominator );
		}


		/// <summary>The empty <see cref="Rational"/> structure.
		/// <para>While "Zero" would be a better name, it might confuse people to have it convert into 1 (due to the DXGI rule).</para>
		/// </summary>
		public static readonly Rational Empty = new Rational();


		#region Operators


		/// <summary>Converts a <see cref="Rational"/> stucture to a double-precision floating-point value.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns a double-precision floating-point value equivalent to this structure; can be any finite positive value, or zero.</returns>
		public static implicit operator double( Rational rational )
		{
			return rational.ToDouble();
		}

		/// <summary>Converts a <see cref="Rational"/> stucture to a single-precision floating-point value.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns a single-precision floating-point value equivalent to this structure; can be any finite positive value, or zero.</returns>
		public static explicit operator float( Rational rational )
		{
			return rational.ToSingle();
		}

		/// <summary>Converts a <see cref="Rational"/> stucture to a single-precision floating-point value.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns a 32-bit integer value equivalent to this structure; can be any positive value, or zero.</returns>
		public static explicit operator int( Rational rational )
		{
			return rational.ToInt32();
		}


		/// <summary>Equality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Rational rational, Rational other )
		{
			return rational.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Rational rational, Rational other )
		{
			return !rational.Equals( other );
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the <paramref name="rational"/> structure is lower than the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator <( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) < 0;
		}


		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the <paramref name="rational"/> structure is lower than or equal to the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator <=( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) <= 0;
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the <paramref name="rational"/> structure is greater than the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator >( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) > 0;
		}


		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/> stucture.</param>
		/// <param name="other">A <see cref="Rational"/> stucture.</param>
		/// <returns>Returns true if the <paramref name="rational"/> structure is greater than or equal to the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator >=( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) >= 0;
		}

	
		#endregion

	}

}