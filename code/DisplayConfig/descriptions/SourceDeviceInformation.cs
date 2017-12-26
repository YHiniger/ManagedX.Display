using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains the GDI device name for the source or view.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553983%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_SOURCE_DEVICE_NAME" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 84 )]
	internal sealed class SourceDeviceInformation : DeviceInformation
	{

		/// <summary>The GDI device name for the source, or view.
		/// <para>This name can be used in a call to EnumDisplaySettings to obtain a list of available modes for the specified source.</para>
		/// </summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = DisplayDevice.MaxDeviceNameChars )]
		public readonly string GDIDeviceName;



		internal SourceDeviceInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetSourceName, 84, adapterId, id )
		{
			GDIDeviceName = new string( '\0', DisplayDevice.MaxDeviceNameChars );
		}

	}

}