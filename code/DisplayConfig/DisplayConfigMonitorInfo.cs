namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayMonitor"/>.</summary>
	public sealed class DisplayConfigMonitorInfo : DisplayConfigInfo
	{

		private PathTargetInfo info;
		private VideoSignalInfo mode;



		internal DisplayConfigMonitorInfo( DisplayConfiguration displayConfiguration, PathTargetInfo info )
			: base( info.AdapterId, info.Id, displayConfiguration.Topology )
		{
			if( info.ModeInfoIndex > -1 )
				mode = displayConfiguration.ModeInfo[ info.ModeInfoIndex ].TargetMode.TargetVideoSignalInfo;
			else
				mode = VideoSignalInfo.Empty;

			this.info = info;
		}



		#region PathTargetInfo properties

		/// <summary></summary>
		public bool IsAvailable { get { return info.IsTargetAvailable; } }
		
		/// <summary>Gets the target's connector type.</summary>
		public VideoOutputTechnology OutputTechnology { get { return info.OutputTechnology; } }
		
		/// <summary></summary>
		public Rational RefreshRate { get { return info.RefreshRate; } }
		
		/// <summary>Gets the target's orientation or rotation.</summary>
		public DisplayRotation Orientation { get { return info.Rotation; } }
		
		/// <summary>Gets a value indicating how the source image is scaled to the target.</summary>
		public Scaling Scaling { get { return info.Scaling; } }
		
		//public ScanlineOrdering ScanlineOrdering { get { return info.ScanlineOrdering; } }
		
		/// <summary>Gets the target's state.</summary>
		public PathTargetInfoStatus State { get { return info.State; } }

		#endregion PathTargetInfo properties


		#region VideoSignalInfo properties

		/// <summary></summary>
		public Size ActiveSize { get { return mode.ActiveSize; } }
		
		/// <summary></summary>
		public Rational HSyncFrequency { get { return mode.HSyncFrequency; } }
		
		/// <summary></summary>
		public Rational VSyncFrequency { get { return mode.VSyncFrequency; } }
		
		/// <summary></summary>
		public long PixelRate { get { return mode.PixelRate; } }
		
		/// <summary></summary>
		public ScanlineOrdering ScanlineOrdering { get { return mode.ScanlingOrdering; } }
		
		/// <summary></summary>
		public Size TotalSize { get { return mode.TotalSize; } }
		
		/// <summary></summary>
		public int VideoStandard { get { return mode.VideoStandard; } }

		#endregion VideoSignalInfo properties

	}

}