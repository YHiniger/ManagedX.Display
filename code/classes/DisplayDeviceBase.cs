using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display
{

	/// <summary>Encapsulates a <see cref="DisplayDevice"/> structure.
	/// <para>Requires Windows Vista or newer. For Desktop applications only.</para>
	/// </summary>
	public abstract class DisplayDeviceBase : ManagedX.Design.IDevice, IEquatable<DisplayDeviceBase>
	{

		private DisplayDevice device;



		/// <summary>Constructor.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure representing the display adapter or monitor.</param>
		internal DisplayDeviceBase( DisplayDevice displayDevice )
		{
			device = displayDevice;
		}



		internal bool Reset( DisplayDevice displayDevice )
		{
			if( device.Equals( displayDevice ) )
				return false;
			
			device = displayDevice;
			return true;
		}


		#region DisplayDevice members

		/// <summary>Gets the device name of this display device.</summary>
		public string Identifier { get { return device.DeviceName; } }


		/// <summary>Gets a description (=friendly name) of this display device.</summary>
		public string DisplayName { get { return device.DeviceString; } }


		/// <summary>Gets the state of this display device.</summary>
		protected int RawState { get { return device.State; } }


		/// <summary>Gets the device id of this display device.
		/// <para>This property is named DeviceId for adapters and DevicePath for monitors (to improve consistency with DisplayConfig).</para>
		/// </summary>
		protected string DeviceId { get { return device.DeviceId; } }


		/// <summary>Gets the (registry?) key associated with this display device.</summary>
		public string DeviceKey { get { return device.DeviceKey; } }

		#endregion DisplayDevice members


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


		/// <summary>Returns a value indicating whether this <see cref="DisplayDeviceBase"/> instance is equivalent to an object.</summary>
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
			return device.DeviceString;
		}

	}

}