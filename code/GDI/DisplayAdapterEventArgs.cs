using System;


namespace ManagedX.Graphics
{

	/// <summary>Arguments for the <see cref="DisplayAdapter.MonitorConnected"/> and <see cref="DisplayAdapter.MonitorDisconnected"/> events.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public sealed class DisplayAdapterEventArgs : EventArgs
	{

		private readonly DisplayMonitor monitor;



		internal DisplayAdapterEventArgs( DisplayMonitor monitor )
		{
			this.monitor = monitor;
		}



		/// <summary>Gets the monitor associated with this <see cref="DisplayAdapterEventArgs"/> object.</summary>
		public DisplayMonitor Monitor => monitor;

	}

}