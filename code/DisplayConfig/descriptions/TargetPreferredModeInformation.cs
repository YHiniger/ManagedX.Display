using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains information about the preferred mode of a display (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553996%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_TARGET_PREFERRED_MODE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 76 )]
	public sealed class TargetPreferredModeInformation : DeviceInformation
	{

		private int width;
		private int height;
		private TargetMode targetMode;



		/// <summary>Initializes a new <see cref="TargetPreferredModeInformation"/>.</summary>
		/// <param name="adapterId">The adapter LUID of the target.</param>
		/// <param name="id">The target id.</param>
		public TargetPreferredModeInformation( Luid adapterId, int id )
			: base( DeviceInfoType.GetTargetPreferredMode, 76, adapterId, id )
		{
		}



		/// <summary>Gets the size, in pixels, of the best mode for the monitor which is connected to the target that <see cref="VideoSignal"/> specifies.</summary>
		public Size Size { get { return new Size( width, height ); } }


		/// <summary>Gets information about the best mode for the monitor which is connected to the specified target.</summary>
		public VideoSignalInfo VideoSignal { get { return targetMode.TargetVideoSignalInfo; } }


		/// <summary>Returns a string representing this <see cref="TargetPreferredModeInformation"/>.</summary>
		/// <returns>Returns a string representing this <see cref="TargetPreferredModeInformation"/>.</returns>
		public sealed override string ToString()
		{
			return width + "x" + height + "@" + VideoSignal.VSyncFrequency.ToSingle() + "Hz";
		}

	}

}