﻿namespace ManagedX.Display
{

	/// <summary>Enumerates state flags used by <see cref="DisplayDevice"/> structures related to output/target devices (ie: monitors).
	/// <para>This enumeration is equivalent to the native <code>DISPLAY_DEVICE_</code> constants (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183569%28v=vs.85%29.aspx</remarks>
	[Design.Native( "WinGDI.h", "DISPLAY_DEVICE_*" )]
	[System.Flags]
	public enum MonitorStateIndicators : int
	{

		/// <summary>No state specified.</summary>
		None = 0x00000000,

		/// <summary>The monitor is presented as being "on" by the respective GDI view.
		/// <para>Windows Vista: EnumDisplayDevices will only enumerate monitors that can be presented as being "on".</para>
		/// </summary>
		Active = 0x00000001,

		/// <summary>The monitor is attached to the desktop.</summary>
		Attached = 0x00000002

	}

}