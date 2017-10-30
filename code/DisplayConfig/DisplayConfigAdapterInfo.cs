namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayAdapter"/>.</summary>
	public sealed class DisplayConfigAdapterInfo : DisplayConfigInfo
	{

		private readonly Size size;
		private readonly PixelFormat format;
		private readonly Point position;
		private readonly bool isInUse;



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
		public Point SurfacePosition => position;


		/// <summary>Gets the size, in pixels, of the source surface.</summary>
		public Size SurfaceSize => size;


		/// <summary>Gets the pixel format of the source surface.</summary>
		public PixelFormat SurfaceFormat => format;


		/// <summary>Gets a value indicating whether the source is in use.</summary>
		public bool IsInUse => isInUse;

	}

}