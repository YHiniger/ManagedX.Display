using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>A read-only collection of <see cref="ModeInfo"/> structures.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public sealed class ReadOnlyModeInfoCollection : ReadOnlyCollection<ModeInfo>
	{

		/// <summary>Initializes a new <see cref="ReadOnlyModeInfoCollection"/>.</summary>
		/// <param name="list">The list to be wrapped.</param>
		internal ReadOnlyModeInfoCollection( IList<ModeInfo> list )
			: base( list )
		{
		}

	}

}
