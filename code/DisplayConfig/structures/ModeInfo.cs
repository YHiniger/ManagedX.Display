using System;
using System.Runtime.InteropServices;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Contains either source mode or target mode information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553933%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 64 )]
	public struct ModeInfo : IEquatable<ModeInfo>
	{

		[FieldOffset( 0 )]
		private ModeInfoType infoType;

		[FieldOffset( 4 )]
		private int id;

		[FieldOffset( 8 )]
		private Luid adapterId;

		/// <summary>A valid <see cref="TargetMode"/> structure that describes the specified target only when <see cref="infoType"/> is <see cref="ModeInfoType.Target"/>.</summary>
		[FieldOffset( 16 )]
		private TargetMode targetMode;

		/// <summary>A valid <see cref="SourceMode"/> structure that describes the specified source only when <see cref="infoType"/> is <see cref="ModeInfoType.Source"/>.</summary>
		[FieldOffset( 16 )]
		private SourceMode sourceMode;



		/// <summary>Gets a value indicating whether the <see cref="ModeInfo"/> structure represents source or target mode information.
		/// <para>If InfoType is <see cref="ModeInfoType.Source"/>, the sourceMode parameter value contains a valid <see cref="SourceMode"/> structure describing the specified source.</para>
		/// <para>If InfoType is <see cref="ModeInfoType.Target"/>, the targetMode parameter value contains a valid <see cref="TargetMode"/> structure describing the specified target.</para>
		/// </summary>
		public ModeInfoType InfoType { get { return infoType; } }


		/// <summary>Gets the source or target identifier on the specified adapter this path relates to.</summary>
		public int Id { get { return id; } }


		/// <summary>Gets the identifier of the adapter this source or target mode information relates to.</summary>
		public Luid AdapterId { get { return adapterId; } }


		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.Source"/>, gets a structure describing the specified source.
		/// <para>Otherwise returns an empty structure.</para>
		/// </summary>
		public SourceMode SourceMode
		{
			get
			{
				if( infoType == ModeInfoType.Source )
					return sourceMode;
				return SourceMode.Empty;
			}
		}


		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.Target"/>, gets a structure describing the specified target.
		/// <para>Otherwise returns an empty structure.</para></summary>
		public TargetMode TargetMode
		{
			get
			{
				if( infoType == ModeInfoType.Target )
					return targetMode;
				return TargetMode.Empty;
			}
		}


		/// <summary>Returns a hash code for this <see cref="ModeInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="ModeInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			int value = (int)infoType ^ id ^ adapterId.GetHashCode();
			
			if( infoType == ModeInfoType.Source )
				value ^= sourceMode.GetHashCode();
			else if( infoType == ModeInfoType.Target )
				value ^= targetMode.GetHashCode();
				
			return value;
		}


		/// <summary>Returns a value indicating whether this <see cref="ModeInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="ModeInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="ModeInfo"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( ModeInfo other )
		{
			bool equal = ( infoType == other.infoType ) && ( id == other.id ) && adapterId.Equals( other.adapterId );
			if( !equal )
				return false;

			if( infoType == ModeInfoType.Source )
				return sourceMode.Equals( other.sourceMode );

			if( infoType == ModeInfoType.Target )
				return targetMode.Equals( other.targetMode );

			return true;
		}


		/// <summary>Returns a value indicating whether this <see cref="ModeInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="ModeInfo"/> structure which equals this structure.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is ModeInfo ) && this.Equals( (ModeInfo)obj );
		}



		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="modeInfo">A <see cref="ModeInfo"/> structure.</param>
		/// <param name="other">A <see cref="ModeInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( ModeInfo modeInfo, ModeInfo other )
		{
			return modeInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="modeInfo">A <see cref="ModeInfo"/> structure.</param>
		/// <param name="other">A <see cref="ModeInfo"/> structure.</param>
		/// <returns>Returns false if the structures are equal, otherwise returns true.</returns>
		public static bool operator !=( ModeInfo modeInfo, ModeInfo other )
		{
			return !modeInfo.Equals( other );
		}


		#endregion

	}
	
}