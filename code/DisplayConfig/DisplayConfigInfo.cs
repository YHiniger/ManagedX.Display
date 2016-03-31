namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Base class for additional DisplayConfig information.</summary>
	public abstract class DisplayConfigInfo
	{

		private Luid adapterId;
		private int id;
		private TopologyIndicators topology;
		


		internal DisplayConfigInfo( Luid adapterId, int id, TopologyIndicators topology )
		{
			this.adapterId = adapterId;
			this.id = id;
			this.topology = topology;
		}



		/// <summary>Gets the display adapter id.</summary>
		public Luid AdapterId { get { return adapterId; } }


		/// <summary>Gets the display id.</summary>
		public int Id { get { return id; } }


		/// <summary>Gets the display topology.</summary>
		public TopologyIndicators Topology { get { return topology; } }

	}

}