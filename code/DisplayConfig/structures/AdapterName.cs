using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553915%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Contains information about the display adapter.
	/// <para>The native name of this structure is DISPLAYCONFIG_ADAPTER_NAME.</para>
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 276 )]
	internal struct AdapterName : IEquatable<AdapterName>
	{

		private DeviceInfoHeader header;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		private string adapterDevicePath;


		/// <summary>Initializes a new <see cref="AdapterName"/> structure.</summary>
		/// <param name="adapterId">Identifies the adapter the device information refers to.</param>
		/// <param name="id">The identifier of the source or target to get or set information for.</param>
		internal AdapterName( Luid adapterId, int id )
		{
			header = new DeviceInfoHeader( DeviceInfoType.GetAdapterName, Marshal.SizeOf( typeof( AdapterName ) ), adapterId, id );
			adapterDevicePath = string.Empty;
		}


		/// <summary>Gets the identifier of the adapter the device information packet refers to.</summary>
		public Luid AdapterId { get { return header.AdapterId; } }

		/// <summary>Gets the source or target identifier to get or set the adapter device information for.</summary>
		public int Id { get { return header.Id; } }

		/// <summary>Gets the device name for the adapter; can't be null.</summary>
		public string DeviceName { get { return string.Copy( adapterDevicePath ?? string.Empty ); } }


		/// <summary>Returns a hash code for this <see cref="AdapterName"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="AdapterName"/> structure.</returns>
		public override int GetHashCode()
		{
			return header.GetHashCode() ^ adapterDevicePath.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="AdapterName"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="AdapterName"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( AdapterName other )
		{
			return header.Equals( other.header ) && this.DeviceName.Equals( other.DeviceName, StringComparison.Ordinal );
		}


		/// <summary>Returns a value indicating whether this <see cref="AdapterName"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="AdapterName"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is AdapterName ) && this.Equals( (AdapterName)obj );
		}
		

		/// <summary>Returns the <see cref="DeviceName"/> associated with this <see cref="AdapterName"/> structure.</summary>
		/// <returns>Returns the <see cref="DeviceName"/> associated with this <see cref="AdapterName"/> structure.</returns>
		public override string ToString()
		{
			return this.DeviceName;
		}


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="adapterName">An <see cref="AdapterName"/> structure.</param>
		/// <param name="other">An <see cref="AdapterName"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( AdapterName adapterName, AdapterName other )
		{
			return adapterName.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="adapterName">An <see cref="AdapterName"/> structure.</param>
		/// <param name="other">An <see cref="AdapterName"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( AdapterName adapterName, AdapterName other )
		{
			return !adapterName.Equals( other );
		}


		///// <summary>Implicit string converter.</summary>
		///// <param name="adapterName">An <see cref="AdapterName"/> structure.</param>
		///// <returns></returns>
		//public static implicit operator string( AdapterName adapterName )
		//{
		//	return adapterName.ToString();
		//}

		#endregion

	}

}