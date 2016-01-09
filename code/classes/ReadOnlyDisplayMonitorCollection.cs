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
		
	}

}