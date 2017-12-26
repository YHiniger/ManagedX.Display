using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Specifies base output technology info for a given target ID (defined in WinGDI.h).</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential )]
	public abstract class TargetInformation : DeviceInformation
	{


		/// <summary>Constructor.</summary>
		/// <param name="type">Indicates the type of information to configure or obtain; must not be <see cref="DeviceInfoType.Undefined"/>.</param>
		/// <param name="size">The size, in bytes, of the description (including the header, which is 20 bytes).</param>
		/// <param name="adapterId">The adapter the device information refers to.</param>
		/// <param name="id">The identifier of the source or target to get or set information for.</param>
		/// <exception cref="ArgumentException"/>
		internal TargetInformation( DeviceInfoType type, int size, Luid adapterId, int id )
			: base( type, size, adapterId, id )
		{
		}



		/// <summary>When overridden, gets the video output technology.</summary>
		public abstract VideoOutputTechnology OutputTechnology { get; }

	}

}