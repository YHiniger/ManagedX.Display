using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display
{

	/// <summary>Encapsulates a <see cref="DisplayDevice"/> structure.
	/// <para>Requires Windows Vista or newer. For Desktop applications only.</para>
	/// </summary>
	public abstract class DisplayDeviceBase : IEquatable<DisplayDeviceBase>
	{

		private DisplayDevice device;


		/// <summary>Constructor.</summary>
		/// <param name="displayDevice">A valid <see cref="DisplayDevice"/> structure representing the adapter or monitor.</param>
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

		/// <summary>Gets the device name of the display adapter/monitor device.</summary>
		public virtual string DeviceName { get { return device.DeviceName; } }

		/// <summary>Gets a description of the display adapter/monitor device.</summary>
		public virtual string Description { get { return device.DeviceString; } }

		/// <summary>Gets the state of the display adapter/monitor device.</summary>
		protected int State { get { return device.State; } }

		/// <summary>Gets the device id of this display device.
		/// <para>This property is named DeviceId for adapters and DevicePath for monitors (to improve consistency with DisplayConfig).</para>
		/// </summary>
		protected string DeviceId { get { return device.DeviceId; } }

		/// <summary>Gets the (registry?) key associated with this display device.</summary>
		public string DeviceKey { get { return device.DeviceKey; } }

		#endregion


		/// <summary>Returns the hash code of the underlying <see cref="DisplayDevice"/> structure.</summary>
		/// <returns>Returns the hash code of the underlying <see cref="DisplayDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return device.GetHashCode();
		}


		/// <summary>Returns a value indicating whether the underlying <see cref="DisplayDevice"/> structure is equivalent to another structure of the same type.</summary>
		/// <param name="other">A <see cref="DisplayDevice"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		internal bool Equals( DisplayDevice other )
		{
			return device.Equals( other );
		}


		/// <summary>Returns a value indicating whether the underlying <see cref="DisplayDevice"/> structure equals the structure of another <see cref="DisplayDeviceBase"/> instance.</summary>
		/// <param name="other">A <see cref="DisplayDeviceBase"/> instance.</param>
		/// <returns></returns>
		public bool Equals( DisplayDeviceBase other )
		{
			if( other == null )
				return false;

			return this.Equals( other.device );
		}


		/// <summary>When overridden, returns a value indicating whether this <see cref="DisplayDeviceBase"/> instance is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns></returns>
		public abstract override bool Equals( object obj );


		/// <summary>Returns the description associated with the underlying <see cref="DisplayDevice"/> structure.</summary>
		/// <returns>Returns the description associated with the underlying <see cref="DisplayDevice"/> structure.</returns>
		public sealed override string ToString()
		{
			return device.DeviceString;
		}

	}

}