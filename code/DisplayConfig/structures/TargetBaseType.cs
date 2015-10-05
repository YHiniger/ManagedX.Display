using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{

	// https://msdn.microsoft.com/en-us/library/windows/hardware/dn362043%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Specifies base output technology info for a given target ID.
	/// <para>Requires Windows 8.1 or newer.</para>
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	internal struct TargetBaseType : IEquatable<TargetBaseType>
	{
		
		private DeviceInfoHeader header;
		private VideoOutputTechnology outputTechnology;


		/// <summary>Initializes a new <see cref="TargetBaseType"/> structure.</summary>
		/// <param name="adapterId">The display adapter LUID.</param>
		/// <param name="id">The target id.</param>
		internal TargetBaseType( Luid adapterId, int id )
		{
			header = new DeviceInfoHeader( DeviceInfoType.GetTargetBaseType, Marshal.SizeOf( typeof( TargetBaseType ) ), adapterId, id );
			outputTechnology = VideoOutputTechnology.Other;
		}


		/// <summary>Gets the display adapter LUID.</summary>
		public Luid AdapterId { get { return header.AdapterId; } }

		/// <summary>Gets the display target id.</summary>
		public int Id { get { return header.Id; } }

		/// <summary>Gets the video output technology.</summary>
		public VideoOutputTechnology OutputTechnology { get { return outputTechnology; } }


		/// <summary></summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return header.GetHashCode() ^ (int)outputTechnology;
		}


		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( TargetBaseType other )
		{
			return header.Equals( other.header ) && ( outputTechnology == other.outputTechnology );
		}

		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return ( obj is TargetBaseType ) && this.Equals( (TargetBaseType)obj );
		}

		/// <summary></summary>
		/// <returns></returns>
		public override string ToString()
		{
			return outputTechnology.ToString();
		}

	}

}