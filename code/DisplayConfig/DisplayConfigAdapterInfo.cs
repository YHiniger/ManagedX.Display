namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayAdapter"/>.</summary>
	public sealed class DisplayConfigAdapterInfo : DisplayConfigInfo
	{

		private Size size;
		private PixelFormat format;
		private Point position;
		private bool isInUse;



		internal DisplayConfigAdapterInfo( DisplayConfiguration displayConfiguration, PathSourceInfo source )
			: base( source.AdapterId, source.Id, displayConfiguration.Topology )
		{
			if( source.ModeInfoIndex > -1 && source.ModeInfoIndex < displayConfiguration.ModeInfo.Count )
			{
				var mode = displayConfiguration.ModeInfo[ source.ModeInfoIndex ];
				size = mode.Size;
				format = mode.Format;
				position = mode.Position;
			}

			isInUse = source.InUse;
		}



		/// <summary>Gets the position, in desktop-space coordinates, of the source surface.</summary>
		public Point SurfacePosition { get { return position; } }


		/// <summary>Gets the size, in pixels, of the source surface.</summary>
		public Size SurfaceSize { get { return size; } }


		/// <summary>Gets the pixel format of the source surface.</summary>
		public PixelFormat SurfaceFormat { get { return format; } }


		/// <summary>Gets a value indicating whether the source is in use.</summary>
		public bool IsInUse { get { return isInUse; } }

	}

}