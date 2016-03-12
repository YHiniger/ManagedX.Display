namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayAdapter"/>.</summary>
	public sealed class DisplayConfigAdapterInfo : DisplayConfigInfo
	{

		private SourceMode mode;
		private bool isInUse;



		internal DisplayConfigAdapterInfo( DisplayConfiguration displayConfiguration, PathSourceInfo source )
			: base( source.AdapterId, source.Id, displayConfiguration.Topology )
		{
			if( source.ModeInfoIndex > -1 )
				mode = displayConfiguration.ModeInfo[ source.ModeInfoIndex ].SourceMode;
			else
				mode = SourceMode.Empty;

			isInUse = source.InUse;
		}



		/// <summary>Gets the position, in desktop-space coordinates, of the source surface.</summary>
		public Point SurfacePosition { get { return mode.Position; } }


		/// <summary>Gets the size, in pixels, of the source surface.</summary>
		public Size SurfaceSize { get { return mode.Size; } }


		/// <summary>Gets the pixel format of the source surface.</summary>
		public PixelFormat SurfaceFormat { get { return mode.PixelFormat; } }


		/// <summary>Gets a value indicating whether the source is in use.</summary>
		public bool IsInUse { get { return isInUse; } }

	}

}