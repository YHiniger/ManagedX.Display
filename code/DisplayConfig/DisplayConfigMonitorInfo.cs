namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayMonitor"/>.</summary>
	public sealed class DisplayConfigMonitorInfo : DisplayConfigInfo
	{

		private readonly PathTargetInfo info;
		private readonly VideoSignalInfo mode;
		private readonly string displayName;
		private readonly int connectorInstance;
		private readonly string devicePath;
		//private TargetPreferredModeInformation preferredMode;



		internal DisplayConfigMonitorInfo( DisplayConfiguration displayConfiguration, PathTargetInfo info, TargetDeviceInformation targetDeviceName, bool supportsVirtualMode )
			: base( info.AdapterId, info.Id, displayConfiguration.Topology )
		{
			this.info = info;

			int modeInfoIndex;
			if( supportsVirtualMode )
			{
				modeInfoIndex = info.ModeInfoIndex2;
				if( modeInfoIndex == PathTargetInfo.InvalidModeInfoIndex2 )
					modeInfoIndex = PathTargetInfo.InvalidModeInfoIndex;
			}
			else
				modeInfoIndex = info.ModeInfoIndex;

			if( modeInfoIndex > PathTargetInfo.InvalidModeInfoIndex && modeInfoIndex < displayConfiguration.ModeInfo.Count )
				mode = displayConfiguration.ModeInfo[ modeInfoIndex ].VideoSignalInformation;

			displayName = targetDeviceName.FriendlyName;
			connectorInstance = targetDeviceName.ConnectorInstance;
			devicePath = targetDeviceName.DevicePath;
		}



		#region PathTargetInfo properties

		/// <summary>Gets a value indicating whether the target is available.</summary>
		public bool IsAvailable => info.IsTargetAvailable;
		

		/// <summary>Gets the target's connector type.</summary>
		public VideoOutputTechnology OutputTechnology => info.OutputTechnology;
		

		/// <summary>Gets a <see cref="Rational"/> indicating the refresh rate, in hertz (Hz).</summary>
		public Rational RefreshRate => info.RefreshRate;
		

		/// <summary>Gets the target's orientation or rotation.</summary>
		public DisplayRotation Orientation => info.Rotation;
		

		/// <summary>Gets a value indicating how the source image is scaled to the target.</summary>
		public Scaling Scaling => info.Scaling;


		///// <summary></summary>
		//public ScanLineOrdering ScanLineOrdering => info.ScanLineOrdering;
		

		/// <summary>Gets the target's state.</summary>
		public PathTargetInfoStateIndicators State => info.State;

		#endregion PathTargetInfo properties


		#region VideoSignalInfo properties

		/// <summary>Gets the size, in pixels, of the entire video signal.</summary>
		public Size TotalSize => mode.TotalSize;


		/// <summary>Gets the size, in pixels, of the active portion of the video signal.</summary>
		public Size ActiveSize => mode.ActiveSize;
		

		/// <summary>Gets a <see cref="Rational"/> indicating the horizontal frequency, in hertz (Hz).</summary>
		public Rational HSyncFrequency => mode.HSyncFrequency;


		/// <summary>Gets a <see cref="Rational"/> indicating the vertical frequency, in hertz (Hz).</summary>
		public Rational VSyncFrequency => mode.VSyncFrequency;
		

		/// <summary>Gets the pixel clock rate.</summary>
		public long PixelRate => mode.PixelRate;


        /// <summary>Gets the scan-line ordering of the video signal.</summary>
        public ScanLineOrdering ScanLineOrdering => mode.ScanLineOrdering;
		

		/// <summary>Gets the video standard (if any) which defines the video signal.</summary>
		public VideoSignalStandard VideoStandard => mode.VideoStandard;

		#endregion VideoSignalInfo properties


		#region TargetDeviceName properties

		/// <summary>Gets the one-based instance number of this target when the adapter has multiple targets of this type, or 0.</summary>
		public int ConnectorInstance => connectorInstance;
		

		/// <summary>Gets the path to the monitor's device name.</summary>
		public string DevicePath => string.Copy( devicePath ?? string.Empty );
		

		/// <summary>Gets the display monitor's friendly name.</summary>
		public string DisplayName => string.Copy( displayName ?? string.Empty );

		#endregion TargetDeviceName properties

	}

}