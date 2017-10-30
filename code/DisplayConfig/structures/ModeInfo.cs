using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>Contains either source mode or target mode information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff553933%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.DebuggerStepThrough]
	[Win32.Source( "WinGDI.h", "DISPLAYCONFIG_MODE_INFO" )]
	[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 64 )]
	public struct ModeInfo : IEquatable<ModeInfo>
	{

		/// <summary>Indicates whether the <see cref="ModeInfo"/> structure represents source or target mode information.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 0 )]
		public readonly ModeInfoType InfoType;

		/// <summary>The source or target identifier on the specified adapter this path relates to.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 4 )]
		public readonly int Id;

		/// <summary>The identifier of the adapter this source or target mode information relates to.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[FieldOffset( 8 )]
		public readonly Luid AdapterId;

		/// <summary>A valid <see cref="TargetMode"/> structure that describes the specified target only when <see cref="InfoType"/> is <see cref="ModeInfoType.Target"/>.</summary>
		[FieldOffset( 16 )]
		private TargetMode targetMode;

		/// <summary>A valid <see cref="SourceMode"/> structure that describes the specified source only when <see cref="InfoType"/> is <see cref="ModeInfoType.Source"/>.</summary>
		[FieldOffset( 16 )]
		private SourceMode sourceMode;

		/// <summary>A <see cref="DesktopImageInfo"/> structure that describes information about the desktop image only when <see cref="InfoType"/> is <see cref="ModeInfoType.DesktopImage"/>.
		/// <para>Supported starting in Windows 10.</para>
		/// </summary>
		[FieldOffset( 16 )]
		private DesktopImageInfo desktopImageInfo;




		#region Source mode

		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.Source"/>, gets the size, in pixels, of the source mode.</summary>
		public Size Size => InfoType == ModeInfoType.Source ? new Size( sourceMode.Width, sourceMode.Height ) : Size.Empty;


		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.Source"/>, gets the pixel format of the source mode.</summary>
		public PixelFormat Format => InfoType == ModeInfoType.Source ? sourceMode.Format : PixelFormat.Undefined;
		

		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.Source"/>, gets the position, in desktop coordinates, of the upper-left corner of the source mode.</summary>
		public Point Position => InfoType == ModeInfoType.Source ? sourceMode.Position : Point.Zero;

		#endregion Source mode


		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.Target"/>, gets information about the target video signal.</summary>
		public VideoSignalInfo VideoSignalInformation => InfoType == ModeInfoType.Target ? targetMode.TargetVideoSignalInfo : VideoSignalInfo.Empty;


		/// <summary>When <see cref="InfoType"/> is <see cref="ModeInfoType.DesktopImage"/>, gets information about the desktop image.</summary>
		public DesktopImageInfo DesktopImage => InfoType == ModeInfoType.DesktopImage ? desktopImageInfo : DesktopImageInfo.Empty;


		/// <summary>Returns a hash code for this <see cref="ModeInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="ModeInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			var value = (int)InfoType ^ Id ^ AdapterId.GetHashCode();
			
			if( InfoType == ModeInfoType.Source )
				value ^= sourceMode.GetHashCode();
			else if( InfoType == ModeInfoType.Target )
				value ^= targetMode.GetHashCode();
				
			return value;
		}


		/// <summary>Returns a value indicating whether this <see cref="ModeInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="ModeInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="ModeInfo"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( ModeInfo other )
		{
			if( ( InfoType != other.InfoType ) || ( Id != other.Id ) || !AdapterId.Equals( other.AdapterId ) )
				return false;

			if( InfoType == ModeInfoType.Source )
				return sourceMode.Equals( other.sourceMode );

			if( InfoType == ModeInfoType.Target )
				return targetMode.Equals( other.targetMode );

			return true;
		}


		/// <summary>Returns a value indicating whether this <see cref="ModeInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="ModeInfo"/> structure which equals this structure.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is ModeInfo mi ) && this.Equals( mi );
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

		#endregion Operators

	}

}