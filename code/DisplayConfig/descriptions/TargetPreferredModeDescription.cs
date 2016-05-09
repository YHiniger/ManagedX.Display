using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Contains information about the preferred mode of a display (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553996%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_PREFERRED_MODE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 76 )]
	internal sealed class TargetPreferredModeDescription : DeviceDescription
	{

		private int width;
		private int height;
		private TargetMode targetMode;



		/// <summary>Initializes a new <see cref="TargetPreferredModeDescription"/>.</summary>
		/// <param name="adapterId">The adapter LUID of the target.</param>
		/// <param name="id">The target id.</param>
		public TargetPreferredModeDescription( Luid adapterId, int id )
			: base( DeviceInfoType.GetTargetPreferredMode, 76, adapterId, id )
		{
		}



		/// <summary>Gets the width, in pixels, of the best mode for the monitor which is connected to the target that <see cref="VideoSignal"/> specifies.</summary>
		public int Width { get { return width; } }


		/// <summary>Gets the height, in pixels, of the best mode for the monitor which is connected to the target that <see cref="VideoSignal"/> specifies.</summary>
		public int Height { get { return height; } }


		/// <summary>Gets information about the best mode for the monitor which is connected to the specified target.</summary>
		public VideoSignalInfo VideoSignal { get { return targetMode.TargetVideoSignalInfo; } }


		/// <summary>Returns a string representing this <see cref="TargetPreferredModeDescription"/>.</summary>
		/// <returns>Returns a string representing this <see cref="TargetPreferredModeDescription"/>.</returns>
		public sealed override string ToString()
		{
			return width + "x" + height + "@" + VideoSignal.VSyncFrequency.ToSingle() + "Hz";
		}

	}

}