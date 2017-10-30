using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary></summary>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 32 )]
	public sealed class AdvancedColorInformation : DeviceInformation
	{

		[Flags]
		private enum StateIndicators : int
		{
			None = 0x00000000,
			AdvancedColorSupported = 0x00000001,
			AdvancedColorEnabled = 0x00000002,
			WideColorEnforced = 0x00000004,
		}



		private readonly StateIndicators stateIndicators;
		private readonly ColorEncoding colorEncoding;
		private readonly int bitsPerColorChannel;



		internal AdvancedColorInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetAdvancedColorInfo, 32, adapterId, id )
		{
		}



		/// <summary></summary>
		public bool IsAdvancedColorSupported => stateIndicators.HasFlag( StateIndicators.AdvancedColorSupported );

		
		/// <summary></summary>
		public bool IsAdvancedColorEnabled => stateIndicators.HasFlag( StateIndicators.AdvancedColorEnabled );


		/// <summary></summary>
		public bool IsWideColorEnforced => stateIndicators.HasFlag( StateIndicators.WideColorEnforced );


		/// <summary></summary>
		public ColorEncoding ColorEncoding => colorEncoding;


		/// <summary></summary>
		public int BitsPerColorChannel => bitsPerColorChannel;

	}

}