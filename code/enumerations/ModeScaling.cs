﻿namespace ManagedX.Display
{
	using Win32;


	/// <summary>Enumerates values specifying how the graphics device presents a low-resolution mode on a higher resolution monitor.
	/// <para>This enumeration is equivalent to the native
	/// <code>DMDFO_*</code> (Device Mode Display Fixed Output) constants (defined in WinGDI.h), and
	/// the <code>DXGI_MODE_SCALING</code> enumeration (defined in DXGIType.h).</para>
	/// </summary>
	/// <remarks>
	/// https://msdn.microsoft.com/en-us/library/windows/desktop/bb173066%28v=vs.85%29.aspx (DXGI_MODE_SCALING)
	/// https://msdn.microsoft.com/en-us/library/dd183565%28v=vs.85%29.aspx (DMDFO_*)
	/// </remarks>
	[Native( "WinGDI.h", "DMDFO_*" )]
	[Native( "DXGIType.h", "DXGI_MODE_SCALING" )]
	public enum ModeScaling : int
	{
		
		/// <summary>Either the display's default setting, or unspecified.</summary>
		[Native( "WinGDI.h", "DMDFO_DEFAULT" )]
		[Native( "DXGIType.h", "DXGI_MODE_SCALING_UNSPECIFIED" )]
		Default,

		/// <summary>The low-resolution image is centered in the larger screen space.</summary>
		[Native( "WinGDI.h", "DMDFO_CENTER" )]
		[Native( "DXGIType.h", "DXGI_MODE_SCALING_CENTERED" )]
		Center,

		/// <summary>The low-resolution image is stretched to fill the larger screen space.</summary>
		[Native( "WinGDI.h", "DMDFO_STRETCH" )]
		[Native( "DXGIType.h", "DXGI_MODE_SCALING_STRETCHED" )]
		Stretch

	}

}