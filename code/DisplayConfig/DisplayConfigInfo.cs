namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Base class for additional DisplayConfig information.</summary>
	public abstract class DisplayConfigInfo
	{

		private readonly Luid adapterId;
		private readonly int id;
		private readonly TopologyIndicators topology;
		


		internal DisplayConfigInfo( Luid adapterId, int id, TopologyIndicators topology )
		{
			this.adapterId = adapterId;
			this.id = id;
			this.topology = topology;
		}



		/// <summary>Gets the display adapter id.</summary>
		public Luid AdapterId => adapterId;


		/// <summary>Gets the display id.</summary>
		public int Id => id;


		/// <summary>Gets the display topology.</summary>
		public TopologyIndicators Topology => topology;

	}

}