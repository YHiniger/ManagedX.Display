namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayAdapter"/>.</summary>
	public sealed class DisplayConfigAdapterInfo : DisplayConfigInfo
	{

		private Size size;
		private PixelFormat format;
		private Point position;
		private bool isInUse;



		internal DisplayConfigAdapterInfo( DisplayConfiguration displayConfiguration, PathSourceInfo source, bool supportsVirtualMode )
			: base( source.AdapterId, source.Id, displayConfiguration.Topology )
		{
			int modeInfoIndex;
			if( supportsVirtualMode )
			{
				modeInfoIndex = source.ModeInfoIndex2;
				if( modeInfoIndex == PathSourceInfo.InvalidModeInfoIndex2 )
					modeInfoIndex = PathSourceInfo.InvalidModeInfoIndex;
			}
			else
				modeInfoIndex = source.ModeInfoIndex;

			if( modeInfoIndex > PathSourceInfo.InvalidModeInfoIndex && modeInfoIndex < displayConfiguration.ModeInfo.Count )
			{
				var mode = displayConfiguration.ModeInfo[ modeInfoIndex ];
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