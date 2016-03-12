using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Display
{

	/// <summary>Enumerates state flags used by <see cref="DisplayDevice"/> structures related to display adapters.
	/// <para>This enumeration is equivalent to the native <code>DISPLAY_DEVICE_*</code> constants (defined in WinGDI.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183569%28v=vs.85%29.aspx</remarks>
	[System.Flags]
	[Design.Native( "WinGDI.h", "DISPLAY_DEVICE_*" )]
	public enum AdapterStateIndicators : int
	{

		/// <summary>No states specified.</summary>
		None = 0x00000000,

		/// <summary>The device is attached to the desktop.</summary>
		AttachedToDesktop = 0x00000001,

		/// <summary>No documentation.</summary>
		MultiDriver = 0x00000002,

		/// <summary>The primary desktop is on the device.
		/// <para>For a system with a single display card, this is always set.</para>
		/// <para>For a system with multiple display cards, only one device can have this set.</para>
		/// </summary>
		PrimaryDevice = 0x00000004,

		/// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.
		/// <para>An invisible pseudo monitor is associated with this device. For example, NetMeeting uses it.</para>
		/// <para>Note that GetSystemMetrics( SM_MONITORS ) only accounts for visible display monitors.</para>
		/// </summary>
		MirroringDriver = 0x00000008,

		/// <summary>The device is VGA compatible.
		/// <para>This value should not be present unless <see cref="MultiDriver"/> and <see cref="PrimaryDevice"/> are also present.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "VGA", Justification = "VGA = Video Graphics Array" )]
		VGACompatible = 0x00000010,

		/// <summary>The device is removable; it cannot be the primary display.
		/// <para>This value is present since Windows 2000.</para>
		/// </summary>
		Removable = 0x00000020,

		/// <summary>No documentation.
		/// <para>This value is present since Windows 8.</para>
		/// </summary>
		AccDriver = 0x00000040,


		/// <summary>No documentation.
		/// <para>This value is present since Windows Vista.</para>
		/// </summary>
		UnsafeModesOn = 0x00080000,


		/// <summary>No documentation.</summary>
		TSCompatible = 0x00200000,


		/// <summary>No documentation.
		/// <para>This value is present since Windows 2000.</para>
		/// </summary>
		Disconnect = 0x02000000,

		/// <summary>No documentation.
		/// <para>This value is present since Windows 2000.</para>
		/// </summary>
		Remote = 0x04000000,

		/// <summary>The device has more display modes than its output devices support.</summary>
		ModesPruned = 0x08000000

	}

}