using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ManagedX.Graphics
{

	/// <summary>A read-only collection of <see cref="DisplayAdapter"/> instances.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public sealed class ReadOnlyDisplayAdapterCollection : ReadOnlyCollection<DisplayAdapter>
	{

		/// <summary>Initializes a new <see cref="ReadOnlyDisplayAdapterCollection"/>.</summary>
		/// <param name="list">The list to be wrapped.</param>
		internal ReadOnlyDisplayAdapterCollection( IList<DisplayAdapter> list )
			: base( list )
		{
		}

	}

}