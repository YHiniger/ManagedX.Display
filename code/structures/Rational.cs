using System;
using System.Runtime.InteropServices;


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



		#region Constructors

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

		#endregion Constructors



		/// <summary>Returns a double-precision floating-point value equivalent to this <see cref="Rational"/>.</summary>
		/// <returns>Returns a double-precision floating-point value equivalent to this <see cref="Rational"/>.</returns>
		public double ToDouble()
		{
			if( denominator == 0u )
				return 1.0;
			// this follows the "rule" defined in DXGI.
			
			return (double)numerator / (double)denominator;
		}


		/// <summary>Returns a single-precision floating-point value equivalent to this <see cref="Rational"/>.</summary>
		/// <returns>Returns a single-precision floating-point value equivalent to this <see cref="Rational"/>.</returns>
		public float ToSingle()
		{
			if( denominator == 0u )
				return 1.0f;
			// this follows the "rule" defined in DXGI.

			return (float)numerator / (float)denominator;
		}


		/// <summary>Returns a hash code for this <see cref="Rational"/>.</summary>
		/// <returns>Returns a hash code for this <see cref="Rational"/>.</returns>
		public override int GetHashCode()
		{
			return ( numerator ^ denominator ).GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Rational"/> equals another <see cref="Rational"/>.</summary>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the rationals are equal, otherwise returns false.</returns>
		public bool Equals( Rational other )
		{
			return ( numerator * other.denominator == other.numerator * denominator );
		}


		/// <summary>Returns a value indicating whether this <see cref="Rational"/> is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Rational"/> which equals this <see cref="Rational"/>, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Rational ) && this.Equals( (Rational)obj );
		}


		/// <summary>Returns a value indicating whether this <see cref="Rational"/> is equal to, greater or smaller than another <see cref="Rational"/>.</summary>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns -1 if this structure is smaller than the <paramref name="other"/> <see cref="Rational"/>, +1 if it's greater, or 0 if they are equal.</returns>
		public int CompareTo( Rational other )
		{
			var n1 = numerator * other.denominator;
			var n2 = other.numerator * denominator;
			return n1.CompareTo( n2 );
		}


		/// <summary>Returns a string representing this <see cref="Rational"/>, in the form "numerator:denominator".</summary>
		/// <returns>Returns a string representing this <see cref="Rational"/>.</returns>
		public override string ToString()
		{
			return numerator.ToString( System.Globalization.CultureInfo.InvariantCulture ) + ':' + denominator.ToString( System.Globalization.CultureInfo.InvariantCulture );
		}


		
		/// <summary>The empty <see cref="Rational"/>.
		/// <para>While "Zero" would be a better name, it might be confusing to have it convert into 1 (due to the DXGI rule).</para>
		/// </summary>
		public static readonly Rational Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the rationals are equal, otherwise returns false.</returns>
		public static bool operator ==( Rational rational, Rational other )
		{
			return rational.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the rationals are not equal, otherwise returns false.</returns>
		public static bool operator !=( Rational rational, Rational other )
		{
			return !rational.Equals( other );
		}



		/// <summary>Inferiority comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the specified <paramref name="rational"/> is lower than the <paramref name="other"/> <see cref="Rational"/>, otherwise returns false.</returns>
		public static bool operator <( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) < 0;
		}


		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the specified <paramref name="rational"/> is lower than or equal to the <paramref name="other"/> <see cref="Rational"/>, otherwise returns false.</returns>
		public static bool operator <=( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) <= 0;
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the specified <paramref name="rational"/> is greater than the <paramref name="other"/> <see cref="Rational"/>, otherwise returns false.</returns>
		public static bool operator >( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) > 0;
		}


		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <param name="other">A <see cref="Rational"/>.</param>
		/// <returns>Returns true if the specified <paramref name="rational"/> is greater than or equal to the <paramref name="other"/> <see cref="Rational"/>, otherwise returns false.</returns>
		public static bool operator >=( Rational rational, Rational other )
		{
			return rational.CompareTo( other ) >= 0;
		}



		/// <summary><see cref="Rational"/> to <see cref="float"/> conversion operator.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <returns>Returns a single-precision floating-point value equivalent to the specified <paramref name="rational"/>; can be any finite positive value, including zero.</returns>
		public static explicit operator float( Rational rational )
		{
			return rational.ToSingle();
		}


		/// <summary><see cref="Rational"/> to <see cref="double"/> conversion operator.</summary>
		/// <param name="rational">A <see cref="Rational"/>.</param>
		/// <returns>Returns a double-precision floating-point value equivalent to the specified <paramref name="rational"/>; can be any finite positive value, including zero.</returns>
		public static explicit operator double( Rational rational )
		{
			return rational.ToDouble();
		}

		#endregion Operators

	}

}