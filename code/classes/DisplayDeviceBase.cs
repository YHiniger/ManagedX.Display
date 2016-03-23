using System;


namespace ManagedX.Display
{

	/// <summary>Base class for GDI display devices (adapters, monitors).
	/// <para>Requires Windows Vista or newer. For Desktop applications only.</para>
	/// </summary>
	public abstract class DisplayDeviceBase : IEquatable<DisplayDeviceBase>
	{

		/// <summary>Defines the maximum length, in chars, of the GDI <see cref="DeviceName"/>.</summary>
		public const int MaxDeviceNameChars = DisplayDevice.MaxDeviceNameChars;


		private DisplayDevice device;



		/// <summary>Base constructor.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure representing the display adapter or monitor.</param>
		internal DisplayDeviceBase( DisplayDevice displayDevice )
		{
			device = displayDevice;
		}



		/// <summary>Gets the <see cref="DeviceName"/> of this display device.
		/// <para>The device name is in the form "\\.\DISPLAY1" for an adapter, and "\\.\DISPLAY1\Monitor0" for a monitor.</para>
		/// </summary>
		public string DeviceIdentifier { get { return device.DeviceName; } }


		/// <summary>Gets the friendly name of this display device.</summary>
		public virtual string DisplayName
		{
			get { return device.DeviceString; }
			internal set { } // for DisplayMonitor to get a chance to receive a more friendly name than "Generic PnP Monitor"
		}

		
		#region Protected properties
		
		/// <summary>Gets the state of this display device.</summary>
		protected int RawState { get { return device.State; } }


		/// <summary>Gets the device id of this display device.
		/// <para>This property is named DeviceId for adapters and DevicePath for monitors (to improve consistency with DisplayConfig).</para>
		/// </summary>
		protected string DeviceId { get { return device.DeviceId; } }

		#endregion Protected properties


		/// <summary>Gets the device name of this display device.
		/// <para>The device name is in the form "\\.\DISPLAY1" for an adapter, and "\\.\DISPLAY1\Monitor0" for a monitor.</para>
		/// </summary>
		public string DeviceName { get { return device.DeviceName; } }


		/// <summary>Resets, if relevant, the underlying <see cref="DisplayDevice"/> structure and raises events when required.</summary>
		internal virtual void Refresh( DisplayDevice displayDevice )
		{
			if( !device.Equals( displayDevice ) )
			{
				var stateChanged = ( device.State != displayDevice.State );
				
				device = displayDevice;

				if( stateChanged )
				{
					var evt = this.StateChanged;
					if( evt != null )
						evt( this, EventArgs.Empty );
				}
			}
		}


		/// <summary>Raised when the state of this display device changed.</summary>
		public event EventHandler StateChanged;



		/// <summary>Gets the (registry?) key associated with this display device.</summary>
		public string DeviceKey { get { return device.DeviceKey; } }


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
			if( obj is DisplayDevice )
				return device.Equals( (DisplayDevice)obj );

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