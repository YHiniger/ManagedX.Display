using System;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Describes a display path target mode (defined in WinGDI.h).</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553993%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Native( "WinGDI.h", "DISPLAYCONFIG_TARGET_MODE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 48 )]
	internal struct TargetMode : IEquatable<TargetMode>
	{

		/// <summary>Contains a detailed description of the current target mode.</summary>
		public VideoSignalInfo TargetVideoSignalInfo;



		/// <summary>Returns a hash code for this <see cref="TargetMode"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="TargetMode"/> structure.</returns>
		public override int GetHashCode()
		{
			return TargetVideoSignalInfo.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="TargetMode"/> structure is equivalent to an object.</summary>
		/// <param name="other">A <see cref="TargetMode"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( TargetMode other )
		{
			return TargetVideoSignalInfo.Equals( other.TargetVideoSignalInfo );
		}


		/// <summary>Returns a value indicating whether this <see cref="TargetMode"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is an equivalent <see cref="TargetMode"/> structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is TargetMode ) && this.Equals( (TargetMode)obj );
		}


		/// <summary>Returns a string representing this <see cref="TargetMode"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="TargetMode"/> structure.</returns>
		public override string ToString()
		{
			return TargetVideoSignalInfo.ToString();
		}



		/// <summary>The empty <see cref="TargetMode"/> structure.</summary>
		public static readonly TargetMode Empty;


		#region Operators

		/// <summary>Equality operator.</summary>
		/// <param name="targetMode">A <see cref="TargetMode"/> structure.</param>
		/// <param name="other">A <see cref="TargetMode"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( TargetMode targetMode, TargetMode other )
		{
			return targetMode.TargetVideoSignalInfo.Equals( other.TargetVideoSignalInfo );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="targetMode">A <see cref="TargetMode"/> structure.</param>
		/// <param name="other">A <see cref="TargetMode"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( TargetMode targetMode, TargetMode other )
		{
			return !targetMode.TargetVideoSignalInfo.Equals( other.TargetVideoSignalInfo );
		}
		
		#endregion Operators

	}

}