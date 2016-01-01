using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553983%28v=vs.85%29.aspx


	/// <summary>Contains the GDI device name for the source or view.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 84 )]
	public struct SourceDeviceName : IEquatable<SourceDeviceName>
	{

		/// <summary>A <see cref="DeviceInfoHeader"/> structure that contains information about the request for the source device name.
		/// The caller should set the type member of <see cref="DeviceInfoHeader"/> to <see cref="DeviceInfoType.GetSourceName"/> and the adapterId and id members of <see cref="DeviceInfoHeader"/> to the source for which the caller wants the source device name.
		/// The caller should set the size member of <see cref="DeviceInfoHeader"/> to at least the size of the <see cref="SourceDeviceName"/> structure.</summary>
		private DeviceInfoHeader header;

		/// <summary>A null-terminated WCHAR string that is the GDI device name for the source, or view.
		/// This name can be used in a call to EnumDisplaySettings to obtain a list of available modes for the specified source. 
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 32 )]
		private string viewGdiDeviceName;


		/// <summary>Initializes a new <see cref="SourceDeviceName"/> structure.</summary>
		/// <param name="adapterId">The identifier of the source adapter device the information packet refers to.</param>
		/// <param name="sourceId">The identifier of the source adapter to get or set the device information for.</param>
		public SourceDeviceName( Luid adapterId, int sourceId )
		{
			header = new DeviceInfoHeader( DeviceInfoType.GetSourceName, Marshal.SizeOf( typeof( SourceDeviceName ) ), adapterId, sourceId );
			viewGdiDeviceName = string.Empty;
		}


		/// <summary>Gets the identifier of the source adapter device the information packet refers to.</summary>
		public Luid AdapterId { get { return header.AdapterId; } }


		/// <summary>Gets the identifier of the source to get or set the device information for.</summary>
		public int SourceId { get { return header.Id; } }


		/// <summary>Gets the GDI device name of the source or view; can't be null.
		/// This name can be used in a call to EnumDisplaySettings to obtain a list of available modes for the specified source.
		/// </summary>
		public string Name { get { return string.Copy( viewGdiDeviceName ?? string.Empty ); } }


		/// <summary>Returns a hash code for this <see cref="SourceDeviceName"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="SourceDeviceName"/> structure.</returns>
		public override int GetHashCode()
		{
			return header.GetHashCode() ^ this.Name.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="SourceDeviceName"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="SourceDeviceName"/> structure.</param>
		/// <returns>Returns true if this <see cref="SourceDeviceName"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( SourceDeviceName other )
		{
			return header.Equals( other.header ) && this.Name.Equals( other.Name, StringComparison.Ordinal );
		}


		/// <summary>Returns a value indicating whether this <see cref="SourceDeviceName"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="SourceDeviceName"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is SourceDeviceName ) && this.Equals( (SourceDeviceName)obj );
		}


		/// <summary>Returns the GDI device name of the source or view.</summary>
		/// <returns>Returns the GDI device name of the source or view.</returns>
		public override string ToString()
		{
			return string.Copy( viewGdiDeviceName ?? string.Empty );
		}
		
		
		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="sourceDeviceName">A <see cref="SourceDeviceName"/> structure.</param>
		/// <param name="other">A <see cref="SourceDeviceName"/> structure.</param>
		/// <returns>Returns true if both structures are equal, otherwise returns false.</returns>
		public static bool operator ==( SourceDeviceName sourceDeviceName, SourceDeviceName other )
		{
			return sourceDeviceName.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="sourceDeviceName">A <see cref="SourceDeviceName"/> structure.</param>
		/// <param name="other">A <see cref="SourceDeviceName"/> structure.</param>
		/// <returns>Returns false if both structures are equal, otherwise returns true.</returns>
		public static bool operator !=( SourceDeviceName sourceDeviceName, SourceDeviceName other )
		{
			return !sourceDeviceName.Equals( other );
		}


		/// <summary>String converter.</summary>
		/// <param name="sourceDeviceName">A <see cref="SourceDeviceName"/> structure.</param>
		/// <returns>Returns the GDI device name of the source or view.</returns>
		public static implicit operator string( SourceDeviceName sourceDeviceName )
		{
			return sourceDeviceName.ToString();
		}


		#endregion

	}

}