using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information on the state of virtual resolution support for the monitor.
	/// <para>Requires Windows 10 or newer.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/mt622103(v=vs.85).aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 24 )]
	internal sealed class VirtualResolutionSupportInformation : DeviceInformation
	{

		[Flags]
		private enum Indicators : int
		{

			None = 0x00000000,
			
			/// <summary>Disables virtual mode for the monitor using information found in header.</summary>
			DisableMonitorVirtualResolution = 0x00000001,

		}



		private readonly Indicators indicators;



		internal VirtualResolutionSupportInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetSupportVirtualResolution, 24, adapterId, id )
		{
		}



		/// <summary>Gets or sets a value indicating whether to disable virtual mode for the monitor.</summary>
		public bool DisableMonitorVirtualResolution
		{
			get => indicators.HasFlag( Indicators.DisableMonitorVirtualResolution );
			set
			{
				if( value )
					indicators |= Indicators.DisableMonitorVirtualResolution;
				else
					indicators &= ~Indicators.DisableMonitorVirtualResolution;
			}
		}

	}

}