using System;


namespace ManagedX.Graphics
{

	/// <summary>Arguments for the <see cref="DisplayDeviceManager.AdapterAdded"/> and <see cref="DisplayDeviceManager.AdapterRemoved"/> events.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public sealed class DisplayDeviceManagerEventArgs : EventArgs
	{

		private readonly DisplayAdapter adapter;



		internal DisplayDeviceManagerEventArgs( DisplayAdapter adapter )
			: base()
		{
			this.adapter = adapter;
		}



		/// <summary>Gets the <see cref="DisplayAdapter"/> associated with this <see cref="DisplayDeviceManagerEventArgs"/> object.</summary>
		public DisplayAdapter Adapter => adapter;

	}

}