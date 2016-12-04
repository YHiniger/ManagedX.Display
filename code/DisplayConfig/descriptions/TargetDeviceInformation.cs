using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Contains information about the target (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553989%28v=vs.85%29.aspx</remarks>
	[Source( "WinGDI.h", "DISPLAYCONFIG_TARGET_DEVICE_NAME" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 420 )]
	public sealed class TargetDeviceInformation : TargetInformation
	{

		/// <summary>Defines the maximum length, in unicode chars, of the <see cref="FriendlyName"/>.</summary>
		public const int MaxFriendlyNameLength = 64;

		/// <summary>Defines the maximum length, in unicode chars, of the <see cref="DevicePath"/>.</summary>
		public const int MaxDevicePathLength = 128;



		/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553990%28v=vs.85%29.aspx</remarks>
		[Source( "WinGDI.h", "DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS" )]
		[Flags]
		private enum Indicators : int
		{

			None = 0x00000000,

			/// <summary>The string in the monitorFriendlyDeviceName member was constructed from the manufacture identification string in the extended display identification data (EDID).</summary>
			[Source( "WinGDI.h", "friendlyNameFromEdid" )]
			FriendlyNameFromExtendedDisplayInformationData = 0x00000001,

			/// <summary>The target is forced with no detectable monitor attached and the monitorFriendlyDeviceName member is a null-terminated empty string.</summary>
			[Source( "WinGDI.h", "friendlyNameForced" )]
			FriendlyNameForced = 0x00000002,

			/// <summary>The edidManufactureId and edidProductCodeId members are valid and were obtained from the extended display information data (EDID).</summary>
			[Source( "WinGDI.h", "edidIdsValid" )]
			ExtendedDisplayInformationDataIdsValid = 0x00000004

		}



		private Indicators indicators;
		private VideoOutputTechnology outputTechnology;
		private int edid;    // might not be valid (see indicators)
		private int connectorInstance;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxFriendlyNameLength )]
		private string monitorFriendlyDeviceName; // might not be valid (see indicators)
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxDevicePathLength )]
		private string monitorDevicePath;



		/// <summary>Initializes a new <see cref="TargetDeviceInformation"/>.</summary>
		/// <param name="adapterId">The adapter identifier.</param>
		/// <param name="id">The target device identifier.</param>
		public TargetDeviceInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetTargetName, 420, adapterId, id )
		{
		}



		/// <summary>A value from the <see cref="VideoOutputTechnology"/> enumeration that specifies the target's connector type.</summary>
		public sealed override VideoOutputTechnology OutputTechnology { get { return outputTechnology; } }


		#region EDID

		/// <summary>Gets a value indicating whether extended display identification data (EDID) is valid; see <see cref="ExtendedDisplayIdentificationDataManufactureId"/> and <see cref="ExtendedDisplayIdentificationDataProductCodeId"/>.</summary>
		public bool IsExtendedDisplayIdentificationDataValid { get { return ( indicators & Indicators.ExtendedDisplayInformationDataIdsValid ) == Indicators.ExtendedDisplayInformationDataIdsValid; } }


		/// <summary>When <see cref="IsExtendedDisplayIdentificationDataValid"/> is true, gets the manufacture identifier from the monitor extended display identification data (EDID).</summary>
		public short ExtendedDisplayIdentificationDataManufactureId
		{
			get
			{
				if( indicators.HasFlag( Indicators.ExtendedDisplayInformationDataIdsValid ) )
					return (short)( edid & 0x0000ffff );
				return 0;
			}
		}


		/// <summary>When <see cref="IsExtendedDisplayIdentificationDataValid"/> is true, gets the product code from the monitor extended display identification data (EDID).</summary>
		public short ExtendedDisplayIdentificationDataProductCodeId
		{
			get
			{
				if( indicators.HasFlag( Indicators.ExtendedDisplayInformationDataIdsValid ) )
					return (short)( edid >> 16 );
				return 0;
			}
		}

		#endregion EDID


		/// <summary>The one-based instance number of this particular target only when the adapter has multiple targets of this type.
		/// <para>The connector instance is a consecutive one-based number that is unique within each adapter.</para>
		/// If this is the only target of this type on the adapter, this value is zero.
		/// </summary>
		public int ConnectorInstance { get { return connectorInstance; } }


		/// <summary>Gets the friendly name for the monitor, or an empty string.
		/// <para>This name can be used with SetupAPI.dll to obtain the device name that is contained in the installation package.</para>
		/// </summary>
		public string FriendlyName
		{
			get
			{
				if( indicators.HasFlag( Indicators.FriendlyNameFromExtendedDisplayInformationData ) && !string.IsNullOrWhiteSpace( monitorFriendlyDeviceName ) )
					return string.Copy( monitorFriendlyDeviceName );

				return string.Empty;
			}
		}


		/// <summary>Gets a (unicode) string that is the path to the device name for the monitor.
		/// <para>This path can be used with SetupAPI.dll to obtain the device name that is contained in the installation package.</para>
		/// </summary>
		public string DevicePath { get { return string.Copy( monitorDevicePath ?? string.Empty ); } }


		/// <summary>Returns the <see cref="FriendlyName"/>.</summary>
		/// <returns>Returns the <see cref="FriendlyName"/>.</returns>
		public sealed override string ToString()
		{
			return this.FriendlyName;
		}

	}

}