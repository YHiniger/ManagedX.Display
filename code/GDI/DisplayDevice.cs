﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics
{
	using Win32;


	/// <summary>This structure receives information about the display device specified by the <code>deviceIndex</code> parameter of the EnumDisplayDevices function.
	/// <para>The four string members are set based on the parameters passed to EnumDisplayDevices.</para>
	/// If the <code>device</code> param is null, then the structure is filled in with information about the display adapter(s); if it is a valid device name, then
	/// it's filled in with information about the monitor(s) for that device.
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/dd183569%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Source( "WinGDI.h", "DISPLAY_DEVICE" )]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4, Size = 840 )]
	internal struct DisplayDevice : IEquatable<DisplayDevice>
	{

		/// <summary>Defines the maximum length, in chars, of the <see cref="DeviceName"/> string.</summary>
		internal const int MaxDeviceNameChars = 32;

		/// <summary>Defines the maximum length, in chars, of the <see cref="DeviceString"/>, <see cref="DeviceId"/> and <see cref="DeviceKey"/> strings.</summary>
		internal const int MaxStringChars = 128;


		#region Fields

		private readonly int structureSize;

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxDeviceNameChars )]
		private readonly string deviceName;

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxStringChars )]
		private readonly string deviceString;

		/// <summary>The device state; can be either a combination of <see cref="DisplayAdapterStateIndicators"/> values or a combination of <see cref="DisplayMonitorStateIndicators"/> values.</summary>
		internal readonly int State;

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxStringChars )]
		private readonly string deviceID;

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = MaxStringChars )]
		private readonly string deviceKey;

		#endregion Fields



		private DisplayDevice( int structSize )
		{
			structureSize = structSize;
			deviceName = deviceString = null;
			State = 0;
			deviceID = deviceKey = null;
		}



		/// <summary>Gets the device name of the display adapter or monitor; can't be null.</summary>
		public string DeviceName => string.Copy( deviceName?.TrimEnd( '\0' ) ?? string.Empty );


		/// <summary>Gets a description of the display adapter/monitor device; can't be null.</summary>
		public string DeviceString => string.Copy( deviceString ?? string.Empty );


		/// <summary>Gets the (adapter?) device id string corresponding to the display device; can't be null.</summary>
		public string DeviceId => string.Copy( deviceID ?? string.Empty );


		/// <summary>Gets the (registry?) key associated with the display device; can't be null.</summary>
		public string DeviceKey => string.Copy( deviceKey ?? string.Empty );



		/// <summary>Returns a hash code for this <see cref="DisplayDevice"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DisplayDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return this.DeviceName.GetHashCode() ^ this.DeviceString.GetHashCode() ^ this.State ^ this.DeviceId.GetHashCode() ^ this.DeviceKey.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDevice"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="DisplayDevice"/> structure.</param>
		/// <returns>Returns true if this <see cref="DisplayDevice"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( DisplayDevice other )
		{
			return this.DeviceName.Equals( other.DeviceName, StringComparison.Ordinal ) && 
				this.DeviceString.Equals( other.DeviceString, StringComparison.Ordinal ) && 
				( State == other.State ) && 
				this.DeviceId.Equals( other.DeviceId, StringComparison.Ordinal ) && 
				this.DeviceKey.Equals( other.DeviceKey, StringComparison.Ordinal );
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDevice"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DisplayDevice"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is DisplayDevice dd ) && this.Equals( dd );
		}


		/// <summary>Returns the <see cref="DeviceString"/> of the adapter/monitor device.</summary>
		/// <returns>Returns the <see cref="DeviceString"/> of the adapter/monitor device.</returns>
		public override string ToString()
		{
			return this.DeviceString;
		}



		/// <summary>The empty (and invalid) <see cref="DisplayDevice"/> structure.</summary>
		internal static readonly DisplayDevice Empty;


		/// <summary>The default <see cref="DisplayDevice"/> structure.
		/// <para>Use this to initialize a valid structure.</para>
		/// </summary>
		public static readonly DisplayDevice Default = new DisplayDevice( Marshal.SizeOf( typeof( DisplayDevice ) ) );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="displayDevice">A <see cref="DisplayDevice"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDevice"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( DisplayDevice displayDevice, DisplayDevice other )
		{
			return displayDevice.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="displayDevice">A <see cref="DisplayDevice"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDevice"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( DisplayDevice displayDevice, DisplayDevice other )
		{
			return !displayDevice.Equals( other );
		}

		#endregion Operators

	}

}