using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ManagedX.Display.DisplayConfig
{
	
	/// <summary>A read-only collection of <see cref="PathInfo"/> structures.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public sealed class ReadOnlyPathInfoCollection : ReadOnlyCollection<PathInfo>
	{

		/// <summary>Initializes a new <see cref="ReadOnlyPathInfoCollection"/>.</summary>
		/// <param name="list">A list to initialize the read-only collection from.</param>
		internal ReadOnlyPathInfoCollection( IList<PathInfo> list )
			: base( list )
		{
		}

	}

}
