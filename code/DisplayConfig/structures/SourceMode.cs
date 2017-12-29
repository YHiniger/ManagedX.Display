using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information about a source display mode (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553986%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_SOURCE_MODE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	internal struct SourceMode : IEquatable<SourceMode>
	{

		/// <summary>The size, in pixels, of the source mode.</summary>
		public readonly Size Size;

		/// <summary>The pixel format of the source mode.</summary>
		public readonly PixelFormat Format;

		/// <summary>The position in the desktop coordinate space of the upper-left corner of this source surface.
		/// <para>The source surface that is located at (0, 0) is always the primary source surface.</para>
		/// </summary>
		public readonly Point Position;



		/// <summary>Returns a hash code for this <see cref="SourceMode"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="SourceMode"/> structure.</returns>
		public override int GetHashCode()
		{
			return Size.GetHashCode() ^ (int)Format ^ Position.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="SourceMode"/> structure is equivalent to another <see cref="SourceMode"/> structure.</summary>
		/// <param name="other">A <see cref="SourceMode"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		public bool Equals( SourceMode other )
		{
			return Size.Equals( other.Size ) && ( Format == other.Format ) && ( Position == other.Position );
		}


		/// <summary>Returns a value indicating whether this <see cref="SourceMode"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="SourceMode"/> structure which is equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is SourceMode sm ) && this.Equals( sm );
		}


		/// <summary>Returns a string representing this <see cref="SourceMode"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="SourceMode"/> structure.</returns>
		public override string ToString()
		{
			string pixelFmt = "";
			if( Format == PixelFormat.Undefined )
				pixelFmt = "? bpp";
			else if( Format == PixelFormat.NonGDI )
				pixelFmt = "Non GDI";
			else if( Format == PixelFormat.EightBPP )
				pixelFmt = "8 bpp";
			else if( Format == PixelFormat.SixteenBPP )
				pixelFmt = "16 bpp";
			else if( Format == PixelFormat.TwentyFourBPP )
				pixelFmt = "24 bpp";
			else if( Format == PixelFormat.ThirtyTwoBPP )
				pixelFmt = "32 bpp";
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{0} ({1})", Size.ToString(), pixelFmt );
		}


		/// <summary>The empty <see cref="SourceMode"/> structure.</summary>
		public static readonly SourceMode Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="sourceMode">A <see cref="SourceMode"/> structure.</param>
		/// <param name="other">A <see cref="SourceMode"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( SourceMode sourceMode, SourceMode other )
		{
			return sourceMode.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="sourceMode">A <see cref="SourceMode"/> structure.</param>
		/// <param name="other">A <see cref="SourceMode"/> structure.</param>
		/// <returns>Returns true if the structures are not equivalent, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( SourceMode sourceMode, SourceMode other )
		{
			return !sourceMode.Equals( other );
		}

		#endregion Operators

	}

}