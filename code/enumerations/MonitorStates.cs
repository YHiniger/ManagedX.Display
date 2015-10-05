namespace ManagedX.Display
{

	// https://msdn.microsoft.com/en-us/library/dd183569%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Enumerates state flags used by <see cref="DisplayDevice"/> structures related to output/target devices (ie: monitors).</summary>
	[System.Flags]
	public enum MonitorStates : int
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