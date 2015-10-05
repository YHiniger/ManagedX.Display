namespace ManagedX.Display.DisplayConfig
{
	
	// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553974%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Specifies the scaling transformation applied to content displayed on a video present network (VidPN) present path.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1027:MarkEnumsWithFlags" )]
	public enum Scaling : int
	{

		/// <summary>Unspecified scaling.</summary>
		Unspecified,

		/// <summary>Indicates the identity transformation; the source content is presented with no change.
		/// This transformation is available only if the path's source mode has the same spatial resolution as the path's target mode.</summary>
		Identity = 1,

		/// <summary>Indicates the centering transformation; the source content is presented unscaled, centered with respect to the spatial resolution of the target mode.</summary>
		Centered = 2,

		/// <summary>Indicates the content is scaled to fit the path's target.</summary>
		Stretched = 3,

		/// <summary>Indicates the aspect-ratio centering transformation.</summary>
		AspectRatioCenteredMax = 4,

		/// <summary>Indicates that the caller requests a custom scaling that the caller cannot describe with any of the other DISPLAYCONFIG_SCALING_XXX values.
		/// Only a hardware vendor's value-add application should use this value, because the value-add application might require a private interface to the driver.
		/// The application can then use this value to indicate additional context for the driver for the custom value on the specified path.</summary>
		Custom = 5,

		/// <summary>Indicates that the caller does not have any preference for the scaling.
		/// The SetDisplayConfig function will use the scaling value that was last saved in the database for the path.
		/// If such a scaling value does not exist, SetDisplayConfig will use the default scaling for the computer.
		/// For example, <see cref="Stretched"/> for tablet computers and <see cref="AspectRatioCenteredMax">aspect-ratio centered</see> for non-tablet computers.</summary>
		Preferred = 128

	}

}