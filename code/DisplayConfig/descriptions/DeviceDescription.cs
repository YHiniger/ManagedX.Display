using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Base class for device descriptions.
	/// <para>Represents a device info header.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553920%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_HEADER" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public abstract class DeviceDescription
	{
		
		private readonly DeviceInfoType infoType;
		private readonly int structSize;
		private readonly DisplayDeviceId id;



		internal DeviceDescription( DeviceInfoType type, int size, DisplayDeviceId displayDeviceId )
		{
			if( type == DeviceInfoType.Undefined )
				throw new ArgumentException( "Invalid DisplayConfig device info type.", "type" );

			infoType = type;
			structSize = size;
			id = displayDeviceId;
		}

	}

}