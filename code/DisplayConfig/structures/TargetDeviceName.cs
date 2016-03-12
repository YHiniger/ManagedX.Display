using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Contains information about the target (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553989%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2, Size = 420 )]
	public struct TargetDeviceName : IEquatable<TargetDeviceName>
	{

		/// <summary>A <see cref="DeviceInfoHeader"/> structure that contains information about the request for the target device name.
		/// The caller should set the <code>type</code> member of <see cref="DeviceInfoHeader"/> to <code><see cref="DeviceInfoType"/>.GetTargetName</code> and the <code>adapterId</code> and <code>id</code> members of <see cref="DeviceInfoHeader"/> to the target for which the caller wants the target device name.
		/// The caller should set the <code>size</code> member of <see cref="DeviceInfoHeader"/> to at least the size of the <see cref="TargetDeviceName"/> structure.</summary>
		private DeviceInfoHeader header;
		private TargetDeviceNameFlags flags;
		private VideoOutputTechnology outputTechnology;
		private short edidManufactureId;	// might not be valid (see flags)
		private short edidProductCodeId;	// might not be valid (see flags)
		private int connectorInstance;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 64 )]
		private string monitorFriendlyDeviceName; // might not be valid (see flags)
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		private string monitorDevicePath;


		/// <summary>Instantiates a new <see cref="TargetDeviceName"/> structure.</summary>
		/// <param name="adapterId">The adapter identifier.</param>
		/// <param name="targetId">The target device identifier.</param>
		public TargetDeviceName( Luid adapterId, int targetId )
			: this()
		{
			header = new DeviceInfoHeader( DeviceInfoType.GetTargetName, Marshal.SizeOf( typeof( TargetDeviceName ) ), adapterId, targetId );
			monitorDevicePath = monitorFriendlyDeviceName = string.Empty;
		}


		/// <summary>Gets the identifier of the target adapter device the information packet refers to.</summary>
		public Luid AdapterId { get { return header.AdapterId; } }


		/// <summary>Gets the identifier of the target adapter device to get or set the information for.</summary>
		public int TargetId { get { return header.Id; } }


		/// <summary>A <see cref="TargetDeviceNameFlags"/> value that identifies, in bit-field flags, information about the target.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags" )]
		public TargetDeviceNameFlags Flags { get { return flags; } }


		/// <summary>A value from the <see cref="VideoOutputTechnology"/> enumeration that specifies the target's connector type.</summary>
		public VideoOutputTechnology OutputTechnology { get { return outputTechnology; } }


		/// <summary>The manufacture identifier from the monitor extended display identification data (EDID).
		/// This member is set only when the <code><see cref="TargetDeviceNameFlags.ExtendedDisplayInformationDataIdsValid"/></code> bit-field is set in the <see cref="Flags"/> member.
		/// </summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EDID" )]
		public short EDIDManufactureId { get { return edidManufactureId; } }


		/// <summary>The product code from the monitor EDID.
		/// This member is set only when the <code><see cref="TargetDeviceNameFlags.ExtendedDisplayInformationDataIdsValid"/></code> bit-field is set in the <see cref="Flags"/> member.
		/// </summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EDID" )]
		public short EDIDProductCodeId { get { return edidProductCodeId; } }


		/// <summary>The one-based instance number of this particular target only when the adapter has multiple targets of this type.
		/// The connector instance is a consecutive one-based number that is unique within each adapter.
		/// If this is the only target of this type on the adapter, this value is zero.
		/// </summary>
		public int ConnectorInstance { get { return connectorInstance; } }


		/// <summary>Gets a (unicode) string that is the device name for the monitor.
		/// <para>This name can be used with SetupAPI.dll to obtain the device name that is contained in the installation package.</para>
		/// </summary>
		public string FriendlyName { get { return string.Copy( monitorFriendlyDeviceName ?? string.Empty ); } }


		/// <summary>A (unicode) string that is the path to the device name for the monitor.
		/// <para>This path can be used with SetupAPI.dll to obtain the device name that is contained in the installation package.</para>
		/// </summary>
		public string DevicePath { get { return string.Copy( monitorDevicePath ?? string.Empty ); } }


		/// <summary>Returns a hash code for this <see cref="TargetDeviceName"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="TargetDeviceName"/> structure.</returns>
		public override int GetHashCode()
		{
			return header.GetHashCode() ^ (int)flags ^ (int)outputTechnology ^ connectorInstance ^ ( monitorDevicePath ?? string.Empty ).GetHashCode();
		}


		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( TargetDeviceName other )
		{
			return header.Equals( other.header ) && ( flags == other.flags ) && ( outputTechnology == other.outputTechnology ) && ( connectorInstance == other.connectorInstance ) && this.DevicePath.Equals( other.DevicePath );
		}


		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return ( obj is TargetDeviceName ) && this.Equals( (TargetDeviceName)obj );
		}


		/// <summary>Returns the <see cref="FriendlyName"/>.</summary>
		/// <returns>Returns the <see cref="FriendlyName"/>.</returns>
		public override string ToString()
		{
			return this.FriendlyName;
		}


		#region Operators


		/// <summary>Equality operator.</summary>
		/// <param name="targetDeviceName">A <see cref="TargetDeviceName"/> structure.</param>
		/// <param name="other">A <see cref="TargetDeviceName"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( TargetDeviceName targetDeviceName, TargetDeviceName other )
		{
			return targetDeviceName.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="targetDeviceName">A <see cref="TargetDeviceName"/> structure.</param>
		/// <param name="other">A <see cref="TargetDeviceName"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( TargetDeviceName targetDeviceName, TargetDeviceName other )
		{
			return !targetDeviceName.Equals( other );
		}


		#endregion

	}

}