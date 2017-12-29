using System;


namespace ManagedX.Graphics
{

	/// <summary>Base class for GDI display devices (adapters, monitors).</summary>
	public abstract class DisplayDeviceBase : IEquatable<DisplayDeviceBase>
	{

		private DisplayDevice device;
		internal DisplayDeviceId identifier;



		/// <summary>Base constructor.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure representing the display adapter or monitor.</param>
		internal DisplayDeviceBase( ref DisplayDevice displayDevice )
		{
			device = displayDevice;
		}



		/// <summary>Gets the friendly name of this display device.</summary>
		public virtual string DisplayName
		{
			get => device.DeviceString;
			internal set { } // for DisplayMonitor to get a chance to receive a more friendly name than "Generic PnP Monitor"
		}

		
		/// <summary>Raised when the state of this display device changed.</summary>
		public event EventHandler StateChanged;


		#region Protected properties
		
		/// <summary>Gets the state of this display device.</summary>
		protected int State => device.State;


		/// <summary>Gets the device id of this display device.
		/// <para>This property is named DeviceId for adapters and DevicePath for monitors (to improve consistency with DisplayConfig).</para>
		/// </summary>
		protected string DeviceId => device.DeviceId;

		#endregion Protected properties


		/// <summary>Gets the GDI device name of this display device.
		/// <para>The device name is in the form "\\.\DISPLAY1" for an adapter, and "\\.\DISPLAY1\Monitor0" for a monitor.</para>
		/// </summary>
		public string DeviceName => device.DeviceName;


		/// <summary>Gets the registry key associated with this display device.</summary>
		public string DeviceKey => device.DeviceKey;


		internal virtual void Refresh( ref DisplayDevice displayDevice )
		{
			if( !device.Equals( displayDevice ) )
			{
				var stateChanged = ( device.State != displayDevice.State );

				device = displayDevice;

				if( stateChanged )
					this.StateChanged?.Invoke( this, EventArgs.Empty );
			}
		}


		#region DisplayConfig

		/// <summary>Gets the display identifier for this display device.</summary>
		public DisplayDeviceId Identifier => identifier;

		#endregion DisplayConfig


		/// <summary>Returns the hash code of the underlying <see cref="DisplayDevice"/> structure.</summary>
		/// <returns>Returns the hash code of the underlying <see cref="DisplayDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return device.GetHashCode();
		}

		
		/// <summary>Returns a value indicating whether the underlying <see cref="DisplayDevice"/> structure equals the structure of another <see cref="DisplayDeviceBase"/> instance.</summary>
		/// <param name="other">A <see cref="DisplayDeviceBase"/> instance.</param>
		/// <returns></returns>
		public bool Equals( DisplayDeviceBase other )
		{
			return ( other != null ) && device.Equals( other.device );
		}


		/// <summary>Returns a value indicating whether this display device is equivalent to another display device or a native <see cref="DisplayDevice"/> structure.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns a value indicating whether this <see cref="DisplayDeviceBase"/> instance is equivalent to an object.</returns>
		public override bool Equals( object obj )
		{
			if( obj is DisplayDevice dev )
				return dev.Equals( dev );

			return this.Equals( obj as DisplayDeviceBase );
		}


		/// <summary>Returns the description associated with the underlying <see cref="DisplayDevice"/> structure.</summary>
		/// <returns>Returns the description associated with the underlying <see cref="DisplayDevice"/> structure.</returns>
		public sealed override string ToString()
		{
			return this.DisplayName;
		}

	}

}