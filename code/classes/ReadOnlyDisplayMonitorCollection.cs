using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ManagedX.Graphics
{

	/// <summary>A read-only collection of <see cref="DisplayMonitor"/> instances.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public sealed class ReadOnlyDisplayMonitorCollection : ReadOnlyCollection<DisplayMonitor>
	{


		/// <summary>Initializes a new <see cref="ReadOnlyDisplayMonitorCollection"/>.</summary>
		/// <param name="list">The list to be wrapped.</param>
		internal ReadOnlyDisplayMonitorCollection( IList<DisplayMonitor> list )
			: base( list )
		{
		}



		/// <summary>Returns the monitor corresponding to the specified handle.</summary>
		/// <param name="monitorHandle">A monitor handle.</param>
		/// <returns>Returns the requested monitor, or null.</returns>
		public DisplayMonitor GetMonitorByHandle( IntPtr monitorHandle )
		{
			if( monitorHandle != IntPtr.Zero )
			{
				for( var m = 0; m < base.Count; m++ )
				{
					if( base[ m ].Handle == monitorHandle )
						return base[ m ];
				}
			}
			return null;
		}


		/// <summary>Returns the display monitor corresponding to the specified device path, or null.</summary>
		/// <param name="devicePath">The display monitor's device path.</param>
		/// <returns>Returns the display monitor corresponding to the specified device path, or null.</returns>
		public DisplayMonitor GetMonitorByDevicePath( string devicePath )
		{
			var count = this.Count;
			for( var m = 0; m < count; m++ )
				if( base[ m ].DevicePath.Equals( devicePath, StringComparison.OrdinalIgnoreCase ) )
					return base[ m ];
			return null;
		}

	}

}