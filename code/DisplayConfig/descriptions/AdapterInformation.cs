using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information about the display adapter.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553915%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_ADAPTER_NAME" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 276 )]
	internal sealed class AdapterInformation : DeviceInformation
	{

		/// <summary>Defines the maximum length, in unicode chars, of the <see cref="DevicePath"/>.</summary>
		public const int MaxDevicePathLength = 128;



		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxDevicePathLength )]
		private readonly string devicePath;



		/// <summary>Initializes a new <see cref="AdapterInformation"/>.</summary>
		/// <param name="adapterId">The adapter the device information refers to.</param>
		/// <param name="id">The identifier of the source or target to get or set information for.</param>
		public AdapterInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetAdapterName, 276, adapterId, id )
		{
			devicePath = string.Empty;
		}



		/// <summary>Gets the device name for the adapter; can't be null.</summary>
		public string DevicePath { get { return ( devicePath == null ) ? string.Empty : string.Copy( devicePath ); } }



		/// <summary>Returns the <see cref="DevicePath"/>.</summary>
		/// <returns>Returns the <see cref="DevicePath"/>.</returns>
		public sealed override string ToString()
		{
			return this.DevicePath;
		}

	}

}