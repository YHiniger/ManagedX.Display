using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Specifies base output technology info for a given target ID (defined in WinGDI.h).</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential )]
	public abstract class TargetDescription : DeviceDescription
	{


		internal TargetDescription( DeviceInfoType type, int size, DisplayDeviceId displayDeviceId )
			: base( type, size, displayDeviceId )
		{
		}



		/// <summary>When overridden, gets the video output technology.</summary>
		public abstract VideoOutputTechnology OutputTechnology { get; }

	}

}