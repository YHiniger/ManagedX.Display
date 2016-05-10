using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Graphics
{
	using DisplayConfig;


	// "GDI": Graphics Device Interface


	/// <summary>ManagedX GDI (Graphics Device Interface) device manager.</summary>
	public static class DisplayDeviceManager
	{

		/// <summary>Defines the maximum number of display adapters supported by the system: 16.</summary>
		public const int MaxAdapterCount = DisplayAdapter.MaxAdapterCount;


		/// <summary>Defines the maximum length, in chars, of a GDI device name.</summary>
		public const int MaxDeviceNameChars = DisplayDeviceBase.MaxDeviceNameChars;


		private const QueryDisplayConfigRequest DisplayConfigRequest = QueryDisplayConfigRequest.OnlyActivePaths;



		private static readonly Dictionary<string, DisplayAdapter> adaptersByDeviceName = new Dictionary<string, DisplayAdapter>( MaxAdapterCount );
		private static string primaryAdapterDeviceName;
		private static bool isInitialized;
		private static DisplayConfiguration currentConfiguration;
		//private static DisplayConfiguration registryConfiguration;



		/// <summary>Refreshes the device list and states, and raises events.</summary>
		public static void Refresh()
		{
			var removedAdapters = new List<string>( adaptersByDeviceName.Keys );
			var addedAdapters = new List<string>();
			var adaptersToRefresh = new List<DisplayDevice>( removedAdapters.Count );
			var primaryAdapterChanged = false;

			DisplayAdapter adapter;
			var displayDevices = DisplayAdapter.EnumDisplayDevices( null, false );
			for( var a = 0; a < displayDevices.Count; a++ )
			{
				var displayDevice = displayDevices[ a ];

				if( adaptersByDeviceName.TryGetValue( displayDevice.DeviceName, out adapter ) )
				{
					adaptersToRefresh.Add( displayDevice );
					removedAdapters.Remove( adapter.DeviceIdentifier );
				}
				else
				{
					adapter = new DisplayAdapter( displayDevice );
					adaptersByDeviceName.Add( adapter.DeviceIdentifier, adapter );
					addedAdapters.Add( adapter.DeviceIdentifier );
				}

				if( adapter.State.HasFlag( AdapterStateIndicators.PrimaryDevice ) && ( primaryAdapterDeviceName != adapter.DeviceIdentifier ) )
				{
					primaryAdapterChanged = ( primaryAdapterDeviceName != null );
					primaryAdapterDeviceName = displayDevice.DeviceName;
				}
			}


			if( DisplayConfiguration.IsSupported )
			{
				if( currentConfiguration == null )
					currentConfiguration = DisplayConfiguration.Query( DisplayConfigRequest );
				else
					currentConfiguration.Refresh();
			}


			if( removedAdapters.Count > 0 )
			{
				var adapterRemovedEvent = AdapterRemoved;
				while( removedAdapters.Count > 0 )
				{
					var s = removedAdapters[ 0 ];
					removedAdapters.RemoveAt( 0 );

					adapter = adaptersByDeviceName[ s ];
					adaptersByDeviceName.Remove( s );

					adapter.OnRemoved();
					if( adapterRemovedEvent != null )
						adapterRemovedEvent.Invoke( null, new DisplayDeviceEventArgs( adapter.DeviceIdentifier ) );
				}
			}

			if( addedAdapters.Count > 0 )
			{
				var adapterAddedEvent = AdapterAdded;
				while( addedAdapters.Count > 0 )
				{
					if( adapterAddedEvent != null )
						adapterAddedEvent.Invoke( null, new DisplayDeviceEventArgs( addedAdapters[ 0 ] ) );
					addedAdapters.RemoveAt( 0 );
				}
			}


			while( adaptersToRefresh.Count > 0 )
			{
				var device = adaptersToRefresh[ 0 ];
				if( adaptersByDeviceName.TryGetValue( device.DeviceName, out adapter ) )
					adapter.Refresh( device );
				adaptersToRefresh.RemoveAt( 0 );
			}


			if( primaryAdapterChanged && PrimaryAdapterChanged != null )
				PrimaryAdapterChanged.Invoke( null, EventArgs.Empty );

			isInitialized = true;
		}


		#region Display adapters

		/// <summary>Gets the primary display adapter.</summary>
		public static DisplayAdapter PrimaryAdapter
		{
			get
			{
				if( !isInitialized )
					Refresh();
				
				if( primaryAdapterDeviceName != null )
				{
					DisplayAdapter adapter;
					if( adaptersByDeviceName.TryGetValue( primaryAdapterDeviceName, out adapter ) )
						return adapter;
				}
				return null;
			}
		}


		/// <summary>Gets a read-only collection containing all known display adapters.</summary>
		public static ReadOnlyDisplayAdapterCollection Adapters
		{
			get
			{
				if( !isInitialized )
					Refresh();

				var list = new List<DisplayAdapter>( adaptersByDeviceName.Values );
				return new ReadOnlyDisplayAdapterCollection( list );
			}
		}


		/// <summary>Returns a GDI display adapter given its device name (ie: \\.\DISPLAY1).</summary>
		/// <param name="deviceName">The adapter device name.</param>
		/// <returns>Returns the <see cref="DisplayAdapter"/> whose device identifier matches the specified <paramref name="deviceName"/>, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gdi" )]
		public static DisplayAdapter GetAdapterByGdiDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device identifier.", "deviceName" );
			}

			if( !isInitialized )
				Refresh();

			DisplayAdapter adapter;
			if( adaptersByDeviceName.TryGetValue( deviceName, out adapter ) )
				return adapter;
			return null;
		}


		/// <summary>Returns a read-only collection of GDI display adapters, given their device id.</summary>
		/// <param name="gdiDeviceId">A GDI device id.</param>
		/// <returns>Returns a read-only collection of GDI display adapters, given their device id.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "gdi" )]
		public static ReadOnlyDisplayAdapterCollection GetAdaptersByDeviceId( string gdiDeviceId )
		{
			if( !isInitialized )
				Refresh();

			var adapters = new List<DisplayAdapter>( adaptersByDeviceName.Values );
			var adapterIndex = 0;
			while( adapterIndex < adapters.Count )
			{
				if( adapters[ adapterIndex ].DeviceId == gdiDeviceId )
					++adapterIndex;
				else
					adapters.RemoveAt( adapterIndex );
			}
			
			return new ReadOnlyDisplayAdapterCollection( adapters );
		}


		/// <summary>Raised when the <see cref="PrimaryAdapter"/> changed.</summary>
		public static event EventHandler PrimaryAdapterChanged;


		/// <summary>Raised when a <see cref="DisplayAdapter"/> is added to the system.</summary>
		public static event EventHandler<DisplayDeviceEventArgs> AdapterAdded;


		/// <summary>Raised when a <see cref="DisplayAdapter"/> is removed from the system.</summary>
		public static event EventHandler<DisplayDeviceEventArgs> AdapterRemoved;


		/// <summary>Returns a <see cref="DisplayAdapter"/> given its adapter id, or null.
		/// <para>Requires Windows 7 or newer.</para>
		/// </summary>
		/// <param name="adapterId">The adapter id of the requested adapter.</param>
		/// <returns>Returns the requested <see cref="DisplayAdapter"/>, or null if it doesn't exit.</returns>
		public static DisplayAdapter GetAdapterById( Graphics.Luid adapterId )
		{
			var adapters = Adapters;
			for( var a = 0; a < adapters.Count; a++ )
			{
				var adapter = adapters[ a ];
				var info = adapter.GetDisplayConfigInfo();
				if( info.AdapterId.Equals( adapterId ) )
					return adapter;
			}
			return null;
		}

		#endregion Display adapters


		#region Display monitors

		/// <summary>Returns a monitor given its handle (HMONITOR).</summary>
		/// <param name="monitorHandle">The monitor handle.</param>
		/// <returns>Returns the monitor associated with the specified handle, or null.</returns>
		public static DisplayMonitor GetMonitorByHandle( IntPtr monitorHandle )
		{
			if( !isInitialized )
				Refresh();

			var monitorInfo = DisplayMonitor.GetMonitorInfo( monitorHandle );

			DisplayAdapter adapter;
			if( adaptersByDeviceName.TryGetValue( monitorInfo.AdapterDeviceName, out adapter ) )
				return adapter.Monitors.GetMonitorByHandle( monitorHandle );

			return null;
		}


		/// <summary>Returns the <see cref="DisplayMonitor"/> which has the largest area of intersection with the bounding rectangle of a window.</summary>
		/// <param name="windowHandle">A window handle.</param>
		/// <returns>Returns the <see cref="DisplayMonitor"/> which has the largest area of intersection with the bounding rectangle of a window.</returns>
		public static DisplayMonitor GetMonitorFromWindowHandle( IntPtr windowHandle )
		{
			return GetMonitorByHandle( DisplayAdapter.GetMonitorHandleFromWindow( windowHandle ) );
		}


		/// <summary>Returns a monitor given its device path.</summary>
		/// <param name="devicePath">The monitor device path.</param>
		/// <returns>Returns the monitor associated with the specified <paramref name="devicePath"/>, or null.</returns>
		public static DisplayMonitor GetMonitorByDevicePath( string devicePath )
		{
			if( !isInitialized )
				Refresh();

			var adapters = new DisplayAdapter[ adaptersByDeviceName.Count ];
			adaptersByDeviceName.Values.CopyTo( adapters, 0 );

			for( var a = 0; a < adapters.Length; a++ )
			{
				var monitor = adapters[ a ].Monitors.GetMonitorByDevicePath( devicePath );
				if( monitor != null )
					return monitor;
			}

			return null;
		}


		/// <summary>Gets the primary <see cref="DisplayMonitor"/>.</summary>
		public static DisplayMonitor PrimaryMonitor
		{
			get
			{
				if( !isInitialized )
					Refresh();

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


		#region DisplayConfig extension methods

		/// <summary>Gets DisplayConfig information about a <see cref="DisplayAdapter"/>.
		/// <para>Requires Windows 7 or greater.</para>
		/// </summary>
		/// <param name="adapter">A <see cref="DisplayAdapter"/>.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="DisplayConfigException"/>
		[SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters" )]
		public static DisplayConfigAdapterInfo GetDisplayConfigInfo( this DisplayAdapter adapter )
		{
			if( adapter == null )
				throw new ArgumentNullException( "adapter" );

			if( DisplayConfiguration.IsSupported )
			{
				if( currentConfiguration == null )
					currentConfiguration = DisplayConfiguration.Query( DisplayConfigRequest );
				else
					currentConfiguration.Refresh();

				var paths = currentConfiguration.PathInfo;
				for( var p = 0; p < paths.Count; p++ )
				{
					var source = paths[ p ].SourceInfo;
					
					var sourceDeviceName = DisplayConfiguration.GetSourceGdiDeviceName( source );
					if( sourceDeviceName == adapter.DeviceIdentifier )
						return new DisplayConfigAdapterInfo( currentConfiguration, source );
				}
			}

			return null;
		}


		/// <summary>Gets DisplayConfig information about a <see cref="DisplayMonitor"/>.
		/// <para>Requires Windows 7 or greater.</para>
		/// <note type="note">Calling this method will change the monitor's display name to a more friendly name than "Generic PnP Monitor".</note>
		/// </summary>
		/// <param name="monitor">A <see cref="DisplayMonitor"/>.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="DisplayConfigException"/>
		[SuppressMessage( "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters" )]
		public static DisplayConfigMonitorInfo GetDisplayConfigInfo( this DisplayMonitor monitor )
		{
			if( monitor == null )
				throw new ArgumentNullException( "monitor" );

			if( DisplayConfiguration.IsSupported )
			{
				if( currentConfiguration == null )
					currentConfiguration = DisplayConfiguration.Query( DisplayConfigRequest );
				else
					currentConfiguration.Refresh();

				var paths = currentConfiguration.PathInfo;
				for( var p = 0; p < paths.Count; p++ )
				{
					var target = paths[ p ].TargetInfo;
					var targetDeviceName = DisplayConfiguration.GetTargetDeviceName( target );
					
					var monitor2 = GetMonitorByDevicePath( targetDeviceName.DevicePath );
					if( monitor2 != null )
					{
						if( !string.IsNullOrWhiteSpace( targetDeviceName.FriendlyName ) )
							monitor.DisplayName = targetDeviceName.FriendlyName;

						//var preferredMode = DisplayConfiguration.GetPreferredModeInfo( target );

						return new DisplayConfigMonitorInfo( currentConfiguration, target, targetDeviceName );
					}
				}
			}

			return null;
		}

		#endregion DisplayConfig extension methods

	}

}