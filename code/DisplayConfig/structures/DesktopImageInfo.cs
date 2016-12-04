using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information about the image displayed on the desktop.
	/// <para>Only available on Windows 10 or newer.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/mt622102%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_DESKTOP_IMAGE_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 40 )]
	public struct DesktopImageInfo : IEquatable<DesktopImageInfo>
	{

		/// <summary>Indicates the size of the VidPn source surface that is being displayed on the monitor.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Point PathSourceSize;

		/// <summary>Indicates where the desktop image will be positioned within path source.
		/// <para>Region must be completely inside the bounds of the path source size.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Rect ImageRegion;

		/// <summary>Indicates which part of the desktop image for this clone group will be displayed on this path.
		/// <para>This currently must be set to the desktop size.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Rect ImageClip;



		/// <summary>Returns a hash code for this <see cref="DesktopImageInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DesktopImageInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return PathSourceSize.GetHashCode() ^ ImageRegion.GetHashCode() ^ ImageClip.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DesktopImageInfo"/> structure is equivalent to another <see cref="DesktopImageInfo"/> structure.</summary>
		/// <param name="other">A <see cref="DesktopImageInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		public bool Equals( DesktopImageInfo other )
		{
			return PathSourceSize.Equals( other.PathSourceSize ) && ImageRegion.Equals( other.ImageRegion ) && ImageClip.Equals( other.ImageClip );
		}


		/// <summary>Returns a value indicating whether this <see cref="DesktopImageInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DesktopImageInfo"/> structure which is equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is DesktopImageInfo ) && this.Equals( (DesktopImageInfo)obj );
		}


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="desktopImageInfo">A <see cref="DesktopImageInfo"/> structure.</param>
		/// <param name="other">A <see cref="DesktopImageInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		public static bool operator ==( DesktopImageInfo desktopImageInfo, DesktopImageInfo other )
		{
			return desktopImageInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="desktopImageInfo">A <see cref="DesktopImageInfo"/> structure.</param>
		/// <param name="other">A <see cref="DesktopImageInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equivalent, otherwise returns false.</returns>
		public static bool operator !=( DesktopImageInfo desktopImageInfo, DesktopImageInfo other )
		{
			return !desktopImageInfo.Equals( other );
		}

		#endregion Operators

	}

}