using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Specifies base output technology info for a given target ID (defined in WinGDI.h).
	/// <para>Requires Windows 8.1 or newer.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/dn362043%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_BASE_TYPE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	internal sealed class TargetBaseInformation : TargetInformation
	{

		private VideoOutputTechnology outputTechnology;



		/// <summary>Initializes a new <see cref="TargetBaseInformation"/>.</summary>
		/// <param name="adapterId">The adapter the device information refers to.</param>
		/// <param name="id">The identifier of the source or target to get or set information for.</param>
		/// <exception cref="ArgumentException"/>
		public TargetBaseInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetTargetBaseType, 24, adapterId, id )
		{
		}



		/// <summary>When overridden, gets the video output technology.</summary>
		public sealed override VideoOutputTechnology OutputTechnology { get { return outputTechnology; } }

	}

}