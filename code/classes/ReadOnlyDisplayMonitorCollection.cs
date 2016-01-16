using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ManagedX.Display
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
		public DisplayMonitor GetMonitorByHandle( System.IntPtr monitorHandle )
		{
			for( var m = 0; m < base.Count; m++ )
			{
				if( base[ m ].Handle == monitorHandle )
					return base[ m ];
			}
			return null;
		}

	}

}