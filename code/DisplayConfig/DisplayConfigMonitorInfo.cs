namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayMonitor"/>.</summary>
	public sealed class DisplayConfigMonitorInfo : DisplayConfigInfo
	{

		private PathTargetInfo info;
		private VideoSignalInfo mode;
		private string displayName;
		private int connectorInstance;
		private string devicePath;


		
		internal DisplayConfigMonitorInfo( DisplayConfiguration displayConfiguration, PathTargetInfo info, TargetDeviceInformation targetDeviceName )
			: base( info.AdapterId, info.Id, displayConfiguration.Topology )
		{
			this.info = info;

			if( info.ModeInfoIndex > -1 && info.ModeInfoIndex < displayConfiguration.ModeInfo.Count )
				mode = displayConfiguration.ModeInfo[ info.ModeInfoIndex ].VideoSignalInformation;
			else
				mode = VideoSignalInfo.Empty;

			displayName = targetDeviceName.FriendlyName;
			connectorInstance = targetDeviceName.ConnectorInstance;
			devicePath = targetDeviceName.DevicePath;
		}



		#region PathTargetInfo properties

		/// <summary>Gets a value indicating whether the target is available.</summary>
		public bool IsAvailable { get { return info.IsTargetAvailable; } }
		

		/// <summary>Gets the target's connector type.</summary>
		public VideoOutputTechnology OutputTechnology { get { return info.OutputTechnology; } }
		

		/// <summary>Gets a <see cref="Rational"/> indicating the refresh rate, in hertz (Hz).</summary>
		public Rational RefreshRate { get { return info.RefreshRate; } }
		

		/// <summary>Gets the target's orientation or rotation.</summary>
		public DisplayRotation Orientation { get { return info.Rotation; } }
		

		/// <summary>Gets a value indicating how the source image is scaled to the target.</summary>
		public Scaling Scaling { get { return info.Scaling; } }


		//public ScanLineOrdering ScanLineOrdering { get { return info.ScanLineOrdering; } }
		

		/// <summary>Gets the target's state.</summary>
		public PathTargetInfoStatus State { get { return info.State; } }

		#endregion PathTargetInfo properties


		#region VideoSignalInfo properties

		/// <summary>Gets the size, in pixels, of the entire video signal.</summary>
		public Size TotalSize { get { return mode.TotalSize; } }


		/// <summary>Gets the size, in pixels, of the active portion of the video signal.</summary>
		public Size ActiveSize { get { return mode.ActiveSize; } }
		

		/// <summary>Gets a <see cref="Rational"/> indicating the horizontal frequency, in hertz (Hz).</summary>
		public Rational HSyncFrequency { get { return mode.HSyncFrequency; } }


		/// <summary>Gets a <see cref="Rational"/> indicating the vertical frequency, in hertz (Hz).</summary>
		public Rational VSyncFrequency { get { return mode.VSyncFrequency; } }
		

		/// <summary>Gets the pixel clock rate.</summary>
		public long PixelRate { get { return mode.PixelRate; } }


        /// <summary>Gets the scan-line ordering of the video signal.</summary>
        public ScanLineOrdering ScanLineOrdering { get { return mode.ScanLineOrdering; } }
		

		/// <summary>Gets the video standard (if any) which defines the video signal.</summary>
		public VideoSignalStandard VideoStandard { get { return mode.VideoStandard; } }

		#endregion VideoSignalInfo properties


		#region TargetDeviceName properties

		/// <summary>Gets the one-based instance number of this target when the adapter has multiple targets of this type, or 0.</summary>
		public int ConnectorInstance { get { return connectorInstance; } }
		

		/// <summary>Gets the path to the monitor's device name.</summary>
		public string DevicePath { get { return string.Copy( devicePath ?? string.Empty ); } }
		

		/// <summary>Gets the display monitor's friendly name.</summary>
		public string DisplayName { get { return string.Copy( displayName ?? string.Empty ); } }

		#endregion TargetDeviceName properties

	}

}