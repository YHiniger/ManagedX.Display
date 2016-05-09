using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Base class for device descriptions.
	/// <para>Represents a device info header.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553920%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_DEVICE_INFO_HEADER" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public abstract class DeviceInformation
	{
		
		private DeviceInfoType infoType;
		private int structSize;
		private Luid adapterId;
		private int id;



		/// <summary>Constructor.</summary>
		/// <param name="type">Indicates the type of information to configure or obtain; must not be <see cref="DeviceInfoType.None"/>.</param>
		/// <param name="size">The size, in bytes, of the description (including the header, which is 20 bytes).</param>
		/// <param name="adapterId">The adapter the device information refers to.</param>
		/// <param name="id">The identifier of the source or target to get or set information for.</param>
		/// <exception cref="ArgumentException"/>
		internal DeviceInformation( DeviceInfoType type, int size, Luid adapterId, int id )
		{
			if( type == DeviceInfoType.None )
				throw new ArgumentException( "Invalid DisplayConfig device info type.", "type" );

			infoType = type;
			structSize = size;
			this.adapterId = adapterId;
			this.id = id;
		}



		/// <summary>Gets the type of device information to retrieve or set.</summary>
		public DeviceInfoType InfoType { get { return infoType; } }


		/// <summary>Gets the identifier of the adapter the device information packet refers to.</summary>
		public Luid AdapterId { get { return AdapterId; } }


		/// <summary>The source or target identifier to get or set the device information for.
		/// <para>The meaning of this identifier is related to the <see cref="InfoType"/> of information being requested.</para>
		/// For example, in the case of <see cref="DeviceInfoType.GetSourceName"/>, this is the source identifier.
		/// </summary>
		public int Id { get { return Id; } }

	}

}