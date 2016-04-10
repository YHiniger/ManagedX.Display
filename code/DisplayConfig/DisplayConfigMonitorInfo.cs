namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Contains DisplayConfig information about a <see cref="DisplayMonitor"/>.</summary>
	public sealed class DisplayConfigMonitorInfo : DisplayConfigInfo
	{

		private PathTargetInfo info;
		private VideoSignalInfo mode;
		private string displayName;
		private int connectorInstance;
		private string devicePath;


		
		internal DisplayConfigMonitorInfo( DisplayConfiguration displayConfiguration, PathTargetInfo info, TargetDeviceName targetDeviceName )
			: base( info.AdapterId, info.Id, displayConfiguration.Topology )
		{
			if( info.ModeInfoIndex > -1 )
				mode = displayConfiguration.ModeInfo[ info.ModeInfoIndex ].TargetMode.TargetVideoSignalInfo;
			else
				mode = VideoSignalInfo.Empty;

			this.info = info;
			
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

		//public ScanlineOrdering ScanlineOrdering { get { return info.ScanlineOrdering; } }
		
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Scanline")]
        public ScanLineOrdering ScanlineOrdering { get { return mode.ScanlineOrdering; } }
		

		/// <summary>On Windows 8.1 and greater (WDDM 1.3 or greater), gets additional video signal information.
		/// <para>Otherwise, gets the video standard (if any) which defines the video signal.</para>
		/// </summary>
		public int VideoStandard { get { return mode.VideoStandard; } }

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