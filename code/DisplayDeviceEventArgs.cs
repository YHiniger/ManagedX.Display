using System;


namespace ManagedX.Graphics
{

	/// <summary>Arguments for use with <see cref="DisplayDeviceManager.AdapterAdded"/>, <see cref="DisplayAdapter.MonitorConnected"/> and <see cref="DisplayAdapter.MonitorDisconnected"/> events.</summary>
	[Serializable]
	public sealed class DisplayDeviceEventArgs : EventArgs
	{

		private readonly string deviceIdentifier;



		internal DisplayDeviceEventArgs( string deviceIdentifier )
			: base()
		{
			this.deviceIdentifier = deviceIdentifier;
		}



		/// <summary>Gets the device name of the added display device.</summary>
		public string DeviceIdentifier => string.Copy( deviceIdentifier );

	}

}