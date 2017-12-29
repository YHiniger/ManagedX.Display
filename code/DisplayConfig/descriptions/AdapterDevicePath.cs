using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information about the display adapter.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553915%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_ADAPTER_NAME" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 276 )]
	internal sealed class AdapterDevicePath : DeviceDescription
	{

		/// <summary>Defines the maximum length, in Unicode chars, of the <see cref="DevicePath"/>.</summary>
		public const int MaxDevicePathChars = 128;



		/// <summary>The device name for the adapter; can't be null.</summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxDevicePathChars )]
		public readonly string DevicePath;



		internal AdapterDevicePath( DisplayDeviceId displayDeviceId )
			: base( DeviceInfoType.GetAdapterName, 276, displayDeviceId )
		{
			DevicePath = new string( '\0', MaxDevicePathChars );
		}

	}

}