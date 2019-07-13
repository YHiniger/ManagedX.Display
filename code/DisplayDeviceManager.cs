using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics
{
	using DisplayConfig;


	/// <summary>ManagedX GDI (Graphics Device Interface) device manager.</summary>
	public static class DisplayDeviceManager
	{

		/// <summary>Defines the maximum number of display adapters supported by the system: 16.</summary>
		public const int MaxAdapterCount = DisplayAdapter.MaxAdapterCount;

		/// <summary>Defines the maximum length, in (unicode) chars, of a GDI device name: 32.</summary>
		public const int MaxDeviceNameChars = DisplayDevice.MaxDeviceNameChars;

		/// <summary>Defines the maximum length, in (unicode) chars, of a GDI device path: 128.</summary>
		public const int MaxDevicePathChars = DisplayDevice.MaxStringChars;



		private static readonly Dictionary<string, DisplayAdapter> adaptersByDeviceName = new Dictionary<string, DisplayAdapter>( MaxAdapterCount );
		private static string primaryAdapterDeviceName;
		private static bool isInitialized;
		private static readonly DisplayConfiguration configuration = DisplayConfiguration.Query( QueryDisplayConfigRequest.AllPaths );
		//private static DisplayConfiguration registryConfiguration;



		#region Display adapters

		/// <summary>Gets the primary display adapter.</summary>
		public static DisplayAdapter PrimaryAdapter
		{
			get
			{
				if( !isInitialized )
					Update();
				
				if( primaryAdapterDeviceName != null )
				{
					if( adaptersByDeviceName.TryGetValue( primaryAdapterDeviceName, out DisplayAdapter adapter ) )
						return adapter;
				}
				return null;
			}
		}


		/// <summary>Gets a read-only collection containing all known display adapters.</summary>
		public static ReadOnlyCollection<DisplayAdapter> Adapters
		{
			get
			{
				if( !isInitialized )
					Update();
				return new ReadOnlyCollection<DisplayAdapter>( new List<DisplayAdapter>( adaptersByDeviceName.Values ) );
			}
		}


		/// <summary>Returns a <see cref="DisplayAdapter"/> given its GDI device name (ie: <code>\\.\DISPLAY1</code>).</summary>
		/// <param name="deviceName">The adapter device name.</param>
		/// <returns>Returns the <see cref="DisplayAdapter"/> whose device identifier matches the specified <paramref name="deviceName"/>, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GDI" )]
		public static DisplayAdapter GetAdapterByGDIDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device identifier.", "deviceName" );
			}

			if( !isInitialized )
				Update();

			if( adaptersByDeviceName.TryGetValue( deviceName, out DisplayAdapter adapter ) )
				return adapter;
			return null;
		}


		/// <summary>Returns a read-only collection of GDI display adapters, given their device id.</summary>
		/// <param name="gdiDeviceId">A GDI device id.</param>
		/// <returns>Returns a read-only collection of GDI display adapters, given their device id.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "gdi" )]
		public static ReadOnlyCollection<DisplayAdapter> GetAdaptersByDeviceId( string gdiDeviceId )
		{
			if( !isInitialized )
				Update();

			var adapters = new List<DisplayAdapter>( adaptersByDeviceName.Values );
			var adapterIndex = 0;
			while( adapterIndex < adapters.Count )
			{
				if( adapters[ adapterIndex ].DeviceId == gdiDeviceId )
					++adapterIndex;
				else
					adapters.RemoveAt( adapterIndex );
			}
			
			return new ReadOnlyCollection<DisplayAdapter>( adapters );
		}


		/// <summary>Raised when the <see cref="PrimaryAdapter"/> changed.</summary>
		public static event EventHandler PrimaryAdapterChanged;


		/// <summary>Raised when a <see cref="DisplayAdapter"/> is added to the system.</summary>
		public static event EventHandler<DisplayDeviceManagerEventArgs> AdapterAdded;


		/// <summary>Raised when a <see cref="DisplayAdapter"/> is removed from the system.</summary>
		public static event EventHandler<DisplayDeviceManagerEventArgs> AdapterRemoved;


		///// <summary></summary>
		///// <param name="identifier"></param>
		///// <returns></returns>
		//public static DisplayAdapter GetAdapter( DisplayDeviceId identifier )
		//{
		//	foreach( var adapter in adaptersByDeviceName.Values )
		//	{
		//		if( adapter.Identifier.Equals( identifier ) )
		//			return adapter;
		//	}
		//	return null;
		//}

		#endregion Display adapters


		#region Display monitors

		///// <summary></summary>
		///// <param name="identifier"></param>
		///// <returns></returns>
		//public static DisplayMonitor GetMonitor( DisplayDeviceId identifier )
		//{
		//	foreach( var adapter in adaptersByDeviceName.Values )
		//	{
		//		foreach( var monitor in adapter.Monitors )
		//		{
		//			if( monitor.identifier.Equals( identifier ) )
		//				return monitor;
		//		}
		//	}
		//	return null;
		//}


		/// <summary>Returns a monitor given its handle (HMONITOR).</summary>
		/// <param name="monitorHandle">The monitor handle.</param>
		/// <returns>Returns the monitor associated with the specified handle, or null.</returns>
		public static DisplayMonitor GetMonitorByHandle( IntPtr monitorHandle )
		{
			if( !isInitialized )
				Update();

			var monitorInfo = DisplayMonitor.GetInfo( monitorHandle );
			if( adaptersByDeviceName.TryGetValue( monitorInfo.AdapterDeviceName, out DisplayAdapter adapter ) )
				return adapter.GetMonitorByHandle( monitorHandle );

			return null;
		}


		/// <summary>Returns a monitor given its device path.</summary>
		/// <param name="devicePath">The monitor device path.</param>
		/// <returns>Returns the monitor associated with the specified <paramref name="devicePath"/>, or null.</returns>
		public static DisplayMonitor GetMonitorByDevicePath( string devicePath )
		{
			if( !isInitialized )
				Update();

			var adapters = new DisplayAdapter[ adaptersByDeviceName.Count ];
			adaptersByDeviceName.Values.CopyTo( adapters, 0 );

			var aMax = adapters.Length;
			for( var a = 0; a < aMax; ++a )
			{
				var monitor = adapters[ a ].GetMonitorByDevicePath( devicePath );
				if( monitor != null )
					return monitor;
			}

			return null;
		}


		/// <summary>Returns the <see cref="DisplayMonitor"/> which has the largest area of intersection with the bounding rectangle of a window.</summary>
		/// <param name="windowHandle">A window handle.</param>
		/// <returns>Returns the <see cref="DisplayMonitor"/> which has the largest area of intersection with the bounding rectangle of a window.</returns>
		public static DisplayMonitor GetMonitorFromWindowHandle( IntPtr windowHandle )
		{
			return GetMonitorByHandle( DisplayMonitor.GetMonitorHandleFromWindow( windowHandle ) );
		}


		/// <summary>Gets the primary <see cref="DisplayMonitor"/>.</summary>
		public static DisplayMonitor PrimaryMonitor
		{
			get
			{
				if( !isInitialized )
					Update();

				return DisplayAdapter.PrimaryMonitor;
			}
		}


		/// <summary>Raised when the <see cref="PrimaryMonitor"/> changed.</summary>
		public static event EventHandler PrimaryMonitorChanged
		{
			add { DisplayAdapter.PrimaryMonitorChanged += value; }
			remove { DisplayAdapter.PrimaryMonitorChanged -= value; }
		}

		#endregion Display monitors


		private static void GetDisplayConfigInfo( DisplayAdapter adapter )
		{
			var paths = configuration.Paths;
			var pMax = paths.Count;
			PathInfo path;
			PathSourceInfo source;
			PathTargetInfo target;
			TargetDeviceDescription desc;
			DisplayMonitor monitor;
			int index;
			for( var p = 0; p < pMax; ++p )
			{
				path = paths[ p ];
				source = path.SourceInfo;

				if( source == PathSourceInfo.Empty )
					continue;

				if( adapter.DeviceName.Equals( DisplayConfiguration.GetSourceGDIDeviceName( source ), StringComparison.OrdinalIgnoreCase ) )
				{
					adapter.identifier = source.Identifier;
					if( path.SupportsVirtualMode )
					{
						adapter.cloneGroupId = source.CloneGroupId;
						index = source.ModeInfoIndex2;
					}
					else
					{
						adapter.cloneGroupId = -1;
						index = source.ModeInfoIndex;
					}
					//if( index == PathSourceInfo.InvalidModeInfoIndex )
					//	adapter.currentModeFormat = PixelFormat.Undefined;
					//else
					//	adapter.currentModeFormat = configuration.DisplayModes[ index ].Format;

					target = path.TargetInfo;
					if( target == PathTargetInfo.Empty )
						continue;

					desc = DisplayConfiguration.GetTargetDeviceDescription( target );
					monitor = adapter.GetMonitorByDevicePath( desc.DevicePath );
					if( monitor != null )
					{
						monitor.identifier = target.Identifier;
						if( !string.IsNullOrWhiteSpace( desc.FriendlyName ) )
							monitor.DisplayName = desc.FriendlyName;

						monitor.videoOutputTechnology = target.OutputTechnology;
						monitor.scaling = target.Scaling;
						monitor.refreshRate = target.RefreshRate;
						monitor.scanlineOrdering = target.ScanlineOrdering;
						index = path.SupportsVirtualMode ? target.ModeInfoIndex2 : target.ModeInfoIndex;
						if( index != PathTargetInfo.InvalidModeInfoIndex )
							monitor.videoSignalInfo = configuration.DisplayModes[ index ].VideoSignalInformation;
						else
							monitor.videoSignalInfo = VideoSignalInfo.Empty;
						monitor.Orientation = target.Rotation;

						monitor.connectorInstance = desc.ConnectorInstance;
					}
				}
			}
		}


		/// <summary>Refreshes the device list and their state, and raises events.</summary>
		public static void Update()
		{
			configuration.Refresh();

			var adaptersToRefresh = new List<DisplayDevice>( adaptersByDeviceName.Count );
			var removedAdapters = new List<string>( adaptersByDeviceName.Keys );
			var newAdapters = new List<DisplayAdapter>();
			var primaryAdapterChanged = false;

			DisplayAdapter adapter;
			var displayDevices = DisplayAdapter.EnumDisplayDevices( null, false );
			var aMax = displayDevices.Count;
			for( var a = 0; a < aMax; ++a )
			{
				var displayDevice = displayDevices[ a ];

				if( adaptersByDeviceName.TryGetValue( displayDevice.DeviceName, out adapter ) )
				{
					adaptersToRefresh.Add( displayDevice );
					removedAdapters.Remove( adapter.DeviceName );
				}
				else
				{
					adapter = new DisplayAdapter( displayDevice );
					GetDisplayConfigInfo( adapter );
					adaptersByDeviceName.Add( adapter.DeviceName, adapter );
					newAdapters.Add( adapter );
				}

				if( adapter.State.HasFlag( DisplayAdapterStateIndicators.PrimaryDevice ) && ( primaryAdapterDeviceName != adapter.DeviceName ) )
				{
					primaryAdapterChanged = ( primaryAdapterDeviceName != null );
					primaryAdapterDeviceName = displayDevice.DeviceName;
				}
			}

			while( removedAdapters.Count > 0 )
			{
				var s = removedAdapters[ 0 ];
				removedAdapters.RemoveAt( 0 );

				adapter = adaptersByDeviceName[ s ];
				adaptersByDeviceName.Remove( s );

				adapter.OnRemoved();
				AdapterRemoved?.Invoke( null, new DisplayDeviceManagerEventArgs( adapter ) );
			}

			while( newAdapters.Count > 0 )
			{
				AdapterAdded?.Invoke( null, new DisplayDeviceManagerEventArgs( newAdapters[ 0 ] ) );
				newAdapters.RemoveAt( 0 );
			}

			while( adaptersToRefresh.Count > 0 )
			{
				var device = adaptersToRefresh[ 0 ];
				adaptersToRefresh.RemoveAt( 0 );
				if( adaptersByDeviceName.TryGetValue( device.DeviceName, out adapter ) )
				{
					adapter.Refresh( ref device );
					GetDisplayConfigInfo( adapter );
				}
			}


			if( primaryAdapterChanged )
				PrimaryAdapterChanged?.Invoke( null, EventArgs.Empty );

			isInitialized = true;
		}

	}

}