using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Contains information about the preferred mode of a display (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553996%28v=vs.85%29.aspx</remarks>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 76 )]
	public struct TargetPreferredMode : IEquatable<TargetPreferredMode>
	{

		private DeviceInfoHeader header;
		private int width;
		private int height;
		private TargetMode targetMode;


		/// <summary>Initializes a new <see cref="TargetPreferredMode"/> structure.</summary>
		/// <param name="adapterId">The adapter LUID of the target.</param>
		/// <param name="targetId">The target id.</param>
		internal TargetPreferredMode( Luid adapterId, int targetId )
		{
			header = new DeviceInfoHeader( DeviceInfoType.GetTargetPreferredMode, Marshal.SizeOf( typeof( TargetPreferredMode ) ), adapterId, targetId );
			width = height = 0;
			targetMode = TargetMode.Empty;
		}


		/// <summary>Gets the adapter LUID.</summary>
		public Luid AdapterId { get { return header.AdapterId; } }

		/// <summary>Gets the target id.</summary>
		public int TargetId { get { return header.Id; } }


		/// <summary>Gets the width, in pixels, of the best mode for the monitor that is connected to the target that <see cref="TargetMode"/> specifies.</summary>
		public int Width { get { return width; } }

		/// <summary>Gets the height, in pixels, of the best mode for the monitor that is connected to the target that <see cref="TargetMode"/> specifies.</summary>
		public int Height { get { return height; } }
		

		/// <summary>Gets the size (width and height) of this display mode.</summary>
		public Size Size { get { return new Size( width, height ); } }


		/// <summary>Gets a structure describing the best mode for the monitor that is connected to the specified target.</summary>
		public TargetMode TargetMode { get { return targetMode; } }


		/// <summary>Returns a hash code for this <see cref="TargetPreferredMode"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="TargetPreferredMode"/> structure.</returns>
		public override int GetHashCode()
		{
			return header.GetHashCode() ^ width ^ height ^ targetMode.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="TargetPreferredMode"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="TargetPreferredMode"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( TargetPreferredMode other )
		{
			return header.Equals( other.header ) && ( width == other.width ) && ( height == other.height ) && targetMode.Equals( other.targetMode );
		}


		/// <summary>Returns a value indicating whether this <see cref="TargetPreferredMode"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="TargetPreferredMode"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is TargetPreferredMode ) && this.Equals( (TargetPreferredMode)obj );
		}

		
		/// <summary>Returns a string representing this <see cref="TargetPreferredMode"/> structure, in the form:
		/// <para>{<see cref="Width"/>}x{<see cref="Height"/>}@{frequency}Hz</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="TargetPreferredMode"/> structure.</returns>
		public override string ToString()
		{
			return this.Size.ToString() + "@" + targetMode.TargetVideoSignalInfo.VSyncFrequency + "Hz";
		}


		/// <summary>The empty <see cref="TargetPreferredMode"/> structure.</summary>
		public static readonly TargetPreferredMode Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="mode">A <see cref="TargetPreferredMode"/> structure.</param>
		/// <param name="other">A <see cref="TargetPreferredMode"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( TargetPreferredMode mode, TargetPreferredMode other )
		{
			return mode.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="mode">A <see cref="TargetPreferredMode"/> structure.</param>
		/// <param name="other">A <see cref="TargetPreferredMode"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( TargetPreferredMode mode, TargetPreferredMode other )
		{
			return !mode.Equals( other );
		}

		#endregion

	}

}