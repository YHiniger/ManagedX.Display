using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553920%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Contains display information about the device.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	internal struct DeviceInfoHeader : IEquatable<DeviceInfoHeader>
	{

		/// <summary>Determines the type of device information to retrieve or set. The remainder of the packet for the retrieve or set operation follows immediately after the <see cref="DeviceInfoHeader"/> structure.</summary>
		private DeviceInfoType infoType;
		/// <summary>The size, in bytes, of the device information that is retrieved or set.
		/// It includes the size of the header and the size of the additional data that follows the header.
		/// This device information depends on the request type.
		/// </summary>
		private int structSize;
		/// <summary>The identifier of the adapter the device information packet refers to.</summary>
		internal readonly Luid AdapterId;
		/// <summary>The source or target identifier to get or set the device information for.
		/// The meaning of this identifier is related to the <see cref="infoType"/> of information being requested.
		/// For example, in the case of <see cref="DeviceInfoType.GetSourceName"/>, this is the source identifier.</summary>
		internal readonly int Id;


		/// <summary>Initializes a new <see cref="DeviceInfoHeader"/> structure.</summary>
		/// <param name="deviceInfoType">Info type.</param>
		/// <param name="structureSize">The size, in bytes, of the structure (including the header).</param>
		/// <param name="adapterId">Identifies the adapter the device information refers to.</param>
		/// <param name="id">The identifier of the source or target to get or set information for.</param>
		internal DeviceInfoHeader( DeviceInfoType deviceInfoType, int structureSize, Luid adapterId, int id )
			: this()
		{
			infoType = deviceInfoType;
			structSize = structureSize;
			this.AdapterId = adapterId;
			this.Id = id;
		}


		/// <summary>Returns a hash code for this <see cref="DeviceInfoHeader"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DeviceInfoHeader"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)infoType ^ structSize ^ AdapterId.GetHashCode() ^ Id;
		}


		/// <summary>Returns a value indicating whether this <see cref="DeviceInfoHeader"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="DeviceInfoHeader"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( DeviceInfoHeader other )
		{
			return ( infoType == other.infoType ) && ( structSize == other.structSize ) && AdapterId.Equals( other.AdapterId ) && ( Id == other.Id );
		}


		/// <summary>Returns a value indicating whether this <see cref="DeviceInfoHeader"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DeviceInfoHeader"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is DeviceInfoHeader ) && this.Equals( (DeviceInfoHeader)obj );
		}


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="deviceInfoHeader">A <see cref="DeviceInfoHeader"/> structure.</param>
		/// <param name="other">A <see cref="DeviceInfoHeader"/> structure.</param>
		/// <returns>Returns true if both structures are equal, otherwise returns false.</returns>
		public static bool operator ==( DeviceInfoHeader deviceInfoHeader, DeviceInfoHeader other )
		{
			return deviceInfoHeader.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="deviceInfoHeader">A <see cref="DeviceInfoHeader"/> structure.</param>
		/// <param name="other">A <see cref="DeviceInfoHeader"/> structure.</param>
		/// <returns>Returns false if both structures are equal, otherwise returns true.</returns>
		public static bool operator !=( DeviceInfoHeader deviceInfoHeader, DeviceInfoHeader other )
		{
			return !deviceInfoHeader.Equals( other );
		}


		#endregion

	}

}