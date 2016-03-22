namespace ManagedX.Display.DisplayConfig
{
	
	/// <summary>Specifies the scaling transformation applied to content displayed on a video present network (VidPN) present path (as defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553974%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1027:MarkEnumsWithFlags" )]
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_SCALING" )]
	public enum Scaling : int
	{

		/// <summary>Unspecified scaling.</summary>
		Unspecified,

		/// <summary>The identity transformation; the source content is presented with no change.
		/// <para>This transformation is available only if the path's source mode has the same spatial resolution as the path's target mode.</para>
		/// </summary>
		Identity = 1,

		/// <summary>The centering transformation; the source content is presented unscaled, centered with respect to the spatial resolution of the target mode.</summary>
		Centered = 2,

		/// <summary>The content is scaled to fit the path's target.</summary>
		Stretched = 3,

		/// <summary>The aspect-ratio centering transformation.</summary>
		AspectRatioCenteredMax = 4,

		/// <summary>The caller requests a custom scaling that the caller cannot describe with any of the other DISPLAYCONFIG_SCALING_XXX values.
		/// <para>Only a hardware vendor's value-add application should use this value, because the value-add application might require a private interface to the driver.</para>
		/// The application can then use this value to indicate additional context for the driver for the custom value on the specified path.
		/// </summary>
		Custom = 5,

		/// <summary>The caller does not have any preference for the scaling.
		/// <para>The SetDisplayConfig function will use the scaling value that was last saved in the database for the path.</para>
		/// If such a scaling value does not exist, SetDisplayConfig will use the default scaling for the computer.
		/// <para>For example, <see cref="Stretched"/> for tablet computers and <see cref="AspectRatioCenteredMax"/> for non-tablet computers.</para>
		/// </summary>
		Preferred = 128

	}

}