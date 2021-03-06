﻿namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Enumerates the types of display device info to configure or obtain through the DisplayConfigSetDeviceInfo or DisplayConfigGetDeviceInfo function.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_DEVICE_INFO_TYPE</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553924%28v=vs.85%29.aspx</remarks>
	[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_TYPE" )]
	internal enum DeviceInfoType : int
	{

		/// <summary>Undefined.</summary>
		Undefined,

		/// <summary>Specifies the source name of the display device.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns the source name in the <see cref="SourceDeviceName"/> object.</para>
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME" )]
		GetSourceName,

		/// <summary>Specifies information about the monitor.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns info about the monitor in the <see cref="TargetDeviceDescription"/> object.</para>
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME" )]
		GetTargetName,

		/// <summary>Specifies information about the preferred mode of a monitor.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns info about the preferred mode of a monitor in the <see cref="TargetPreferredMode"/> object.</para>
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE" )]
		GetTargetPreferredMode,

		/// <summary>Specifies the graphics adapter name.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns the adapter name in the <see cref="AdapterDevicePath"/> object.</para>
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME" )]
		GetAdapterName,

		/// <summary>Specifies how to set the monitor.
		/// <para>If the DisplayConfigSetDeviceInfo function is successful, it uses info in the DISPLAYCONFIG_SET_TARGET_PERSISTENCE structure to force the output in a boot-persistent manner.</para>
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE" )]
		SetTargetPersistence,

		/// <summary>Specifies how to set the base output technology for a given target ID.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns base output technology info in the DISPLAYCONFIG_TARGET_BASE_TYPE structure.</para>
		/// Supported by WDDM 1.3 and later user-mode display drivers running on Windows 8.1 and later.
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE" )]
		GetTargetBaseType,

		/// <summary>Specifies the state of virtual mode support.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo returns virtual mode support information in the DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION structure.</para>
		/// Supported starting in Windows 10.
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_SUPPORT_VIRTUAL_RESOLUTION" )]
		GetSupportVirtualResolution,

		/// <summary>Specifies how to set the state of virtual mode support.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, DisplayConfigGetDeviceInfo uses info in the DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION structure to change the state of virtual mode support.</para>
		/// Supported starting in Windows 10.
		/// </summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_SET_SUPPORT_VIRTUAL_RESOLUTION" )]
		SetSupportVirtualResolution,

		
		/// <summary></summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO" )]
		GetAdvancedColorInfo,
		
		/// <summary></summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE" )]
		SetAdvancedColorState,

	}

}