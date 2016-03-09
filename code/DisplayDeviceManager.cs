using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ManagedX.Display
{
	//using DisplayConfig;


	/// <summary>ManagedX display device manager.</summary>
	public static class DisplayDeviceManager
	{

		/// <summary>Defines the maximum number of display adapters supported by the system: 16.</summary>
		public const int MaxAdapterCount = DisplayAdapter.MaxAdapterCount;



		private static readonly Dictionary<string, DisplayAdapter> adaptersByDeviceName = new Dictionary<string, DisplayAdapter>( MaxAdapterCount );
		private static string primaryAdapterDeviceName;



		/// <summary>Refreshes the device list and raises events.</summary>
		public static void Refresh()
		{
			var newList = new List<DisplayAdapter>();

			bool primaryAdapterChanged = false;

			DisplayAdapter adapter;
			var displayAdapters = DisplayAdapter.EnumDisplayDevices( null, false );
			for( var a = 0; a < displayAdapters.Count; a++ )
			{
				var displayAdapter = displayAdapters[ a ];

				if( ( ( displayAdapter.State & (int)AdapterStateIndicators.PrimaryDevice ) == (int)AdapterStateIndicators.PrimaryDevice ) && ( primaryAdapterDeviceName != displayAdapter.DeviceName ) )
				{
					primaryAdapterChanged = ( primaryAdapterDeviceName != null );
					primaryAdapterDeviceName = displayAdapter.DeviceName;
				}

				if( adaptersByDeviceName.TryGetValue( displayAdapter.DeviceName, out adapter ) )
					adapter.Refresh( displayAdapter );
				else
					adapter = new DisplayAdapter( displayAdapter );

				newList.Add( adapter );
			}

			adaptersByDeviceName.Clear();

			while( newList.Count > 0 )
			{
				adapter = newList[ 0 ];
				newList.RemoveAt( 0 );
				adaptersByDeviceName.Add( adapter.DeviceIdentifier, adapter );
			}
			
			if( primaryAdapterChanged && PrimaryAdapterChanged != null )
				PrimaryAdapterChanged( null, EventArgs.Empty );
		}


		#region Display adapters

		/// <summary>Gets the default display adapter.</summary>
		public static DisplayAdapter PrimaryAdapter
		{
			get
			{
				if( primaryAdapterDeviceName != null )
				{
					DisplayAdapter adapter;
					if( adaptersByDeviceName.TryGetValue( primaryAdapterDeviceName, out adapter ) )
						return adapter;
				}
				return null;
			}
		}


		/// <summary>Raised when the <see cref="PrimaryAdapter"/> changed.</summary>
		public static event EventHandler PrimaryAdapterChanged;


		/// <summary>Gets a read-only collection containing all known display adapters.</summary>
		public static ReadOnlyDisplayAdapterCollection Adapters
		{
			get
			{
				var adapters = new DisplayAdapter[ adaptersByDeviceName.Count ];
				adaptersByDeviceName.Values.CopyTo( adapters, 0 );
				return new ReadOnlyDisplayAdapterCollection( adapters );
			}
		}


		/// <summary>Returns a display adapter given its device name.</summary>
		/// <param name="deviceIdentifier">The adapter device name.</param>
		/// <returns>Returns the <see cref="DisplayAdapter"/> whose device identifier matches the specified <paramref name="deviceIdentifier"/>, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static DisplayAdapter GetAdapterByDeviceIdentifier( string deviceIdentifier )
		{
			if( string.IsNullOrWhiteSpace( deviceIdentifier ) )
			{
				if( deviceIdentifier == null )
					throw new ArgumentNullException( "deviceIdentifier" );
				throw new ArgumentException( "Invalid device identifier.", "deviceIdentifier" );
			}

			DisplayAdapter adapter;
			if( adaptersByDeviceName.TryGetValue( deviceIdentifier, out adapter ) )
				return adapter;
			return null;
		}

		#endregion Display adapters


		#region Display monitors

		/// <summary>Returns a monitor given its handle (HMONITOR).</summary>
		/// <param name="monitorHandle">The monitor handle.</param>
		/// <returns>Returns the monitor associated with the specified handle, or null.</returns>
		public static DisplayMonitor GetMonitorByHandle( IntPtr monitorHandle )
		{
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

		#endregion Display monitors


		#region DisplayConfig extension methods
		
		// Placeholder
		
		#endregion DisplayConfig extension methods

	}

}