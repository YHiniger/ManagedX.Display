using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains the GDI device name for the source or view.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553983%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_SOURCE_DEVICE_NAME" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 84 )]
	internal sealed class SourceDeviceInformation : DeviceInformation
	{

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = DisplayDevice.MaxDeviceNameChars )]
		private readonly string viewGdiDeviceName;



		/// <summary>Initializes a new <see cref="SourceDeviceInformation"/>.</summary>
		/// <param name="adapterId">The identifier of the source adapter device the information packet refers to.</param>
		/// <param name="id">The identifier of the source adapter to get or set the device information for.</param>
		public SourceDeviceInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetSourceName, 84, adapterId, id )
		{
			viewGdiDeviceName = string.Empty;
		}



		/// <summary>Gets the GDI device name for the source, or view.
		/// <para>This name can be used in a call to EnumDisplaySettings to obtain a list of available modes for the specified source.</para>
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gdi" )]
		public string GdiDeviceName => string.Copy( viewGdiDeviceName ?? string.Empty );


		/// <summary>Returns the <see cref="GdiDeviceName"/>.</summary>
		/// <returns>Returns the <see cref="GdiDeviceName"/>.</returns>
		public sealed override string ToString()
		{
			return this.GdiDeviceName;
		}

	}

}