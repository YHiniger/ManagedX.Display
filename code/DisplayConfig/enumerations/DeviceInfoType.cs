namespace ManagedX.Display.DisplayConfig
{
	
	/// <summary>Enumerates the types of display device info to configure or obtain through the DisplayConfigSetDeviceInfo or DisplayConfigGetDeviceInfo function.
	/// <para>This enumeration is equivalent to the native <code>DISPLAYCONFIG_DEVICE_INFO_TYPE</code> enumeration (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553924%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_TYPE" )]
	public enum DeviceInfoType : int
	{

		/// <summary>Undefined.</summary>
		None = 0,

		/// <summary>Specifies the source name of the display device.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns the source name in the <see cref="SourceDeviceName"/> structure.</para>
		/// </summary>
		GetSourceName = 1,

		/// <summary>Specifies information about the monitor.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns info about the monitor in the <see cref="TargetDeviceName"/> structure.</para>
		/// </summary>
		GetTargetName = 2,

		/// <summary>Specifies information about the preferred mode of a monitor.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns info about the preferred mode of a monitor in the <see cref="TargetPreferredMode"/> structure.</para>
		/// </summary>
		GetTargetPreferredMode = 3,

		/// <summary>Specifies the graphics adapter name.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns the adapter name in the <see cref="AdapterName"/> structure.</para>
		/// </summary>
		GetAdapterName = 4,

		/// <summary>Specifies how to set the monitor.
		/// <para>If the DisplayConfigSetDeviceInfo function is successful, it uses info in the DISPLAYCONFIG_SET_TARGET_PERSISTENCE structure to force the output in a boot-persistent manner.</para>
		/// </summary>
		SetTargetPersistence = 5,

		/// <summary>Specifies how to set the base output technology for a given target ID.
		/// <para>If the DisplayConfigGetDeviceInfo function is successful, it returns base output technology info in the DISPLAYCONFIG_TARGET_BASE_TYPE structure.</para>
		/// Supported by WDDM 1.3 and later user-mode display drivers running on Windows 8.1 and later.
		/// </summary>
		GetTargetBaseType = 6

	}

}