using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

// Comments checked (2015/09/24)


namespace ManagedX.Display
{

	// https://msdn.microsoft.com/en-us/library/dd183565%28v=vs.85%29.aspx
	// WinGDI.h


	/// <summary>Contains information about the initialization and environment of a display device.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2, Size = 220 )]
	public struct DisplayDeviceMode : IEquatable<DisplayDeviceMode>, IComparable<DisplayDeviceMode>//, IFormattable
	{

		/// <summary>Specifies which members of the <see cref="DisplayDeviceMode"/> structure have been initialized.</summary>
		[Flags]
		private enum FieldFlags : int
		{

			/// <summary>None.</summary>
			None = 0,

			/// <summary></summary>
			Orientation = 1,

			/// <summary></summary>
			PaperSize = 2,

			/// <summary></summary>
			PaperLength = 4,

			/// <summary></summary>
			PaperWidth = 8,

			/// <summary></summary>
			Scale = 16,

			/// <summary><see cref="DisplayDeviceMode.DisplayInfo.Position"/> is initialized.</summary>
			Position = 32,

			/// <summary></summary>
			[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NUP" )]
			NUP = 64,

			/// <summary><see cref="DisplayDeviceMode.DisplayInfo.DisplayOrientation"/> is initialized.</summary>
			DisplayOrientation = 128,

			/// <summary></summary>
			Copies = 256,

			/// <summary></summary>
			DefaultSource = 512,

			/// <summary></summary>
			PrintQuality = 1024,

			/// <summary></summary>
			Color = 2048,

			/// <summary></summary>
			Duplex = 4096,

			/// <summary></summary>
			YResolution = 8192,

			/// <summary></summary>
			TTOption = 16384,

			/// <summary></summary>
			Collate = 32768,

			/// <summary></summary>
			FormName = 65536,

			/// <summary><see cref="DisplayDeviceMode.LogPixels"/> is initialized.</summary>
			LogPixels = 131072,

			/// <summary><see cref="DisplayDeviceMode.BitsPerPel"/> is initialized.</summary>
			BitsPerPixel = 262144,

			/// <summary><see cref="DisplayDeviceMode.PelsWidth"/> is initialized.</summary>
			PelsWidth = 524288,

			/// <summary><see cref="DisplayDeviceMode.PelsHeight"/> is initialized.</summary>
			PelsHeight = 1048576,

			/// <summary><see cref="DisplayDeviceMode.displayFlags"/> is initialized.</summary>
			[SuppressMessage( "Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags" )]
			DisplayFlags = 2097152,

			/// <summary><see cref="DisplayDeviceMode.DisplayFrequency"/> is initialized.</summary>
			DisplayFrequency = 4194304,

			/// <summary></summary>
			IcmMethod = 8388608,

			/// <summary></summary>
			IcmIntent = 16777216,

			/// <summary></summary>
			MediaType = 33554432,

			/// <summary></summary>
			DitherType = 67108864,

			/// <summary></summary>
			PanningWidth = 134217728,

			/// <summary></summary>
			PanningHeight = 268435456,

			/// <summary><see cref="DisplayDeviceMode.DisplayInfo.DisplayFixedOutput"/> is initialized.</summary>
			DisplayFixedOutput = 536870912

		}


		/// <summary>Part of the <see cref="DisplayDeviceMode"/> structure related to display devices only.</summary>
		[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
		private struct DisplayInfo : IEquatable<DisplayInfo>
		{

			/// <summary>A <see cref="Point"/> structure which indicates the positional coordinates of the display device in reference to the desktop area. The primary display device is always located at coordinates (0,0).</summary>
			internal Point Position;

			/// <summary>The orientation images should be presented at. If DM_DISPLAYORIENTATION is not set, this member must be zero.</summary>
			/// <remarks>To determine whether the display orientation is portrait or landscape orientation, check the ratio of pelsWidth to pelsHeight.</remarks>
			internal DisplayOrientation DisplayOrientation;

			/// <summary>For fixed-resolution display devices only, indicates how the display presents a low-resolution mode on a higher-resolution display.
			/// <para>For example, if a display device's resolution is fixed at 1024 x 768 pixels but its mode is set to 640 x 480 pixels, the device can either display a 640 x 480 image somewhere in the interior of the 1024 x 768 screen space or stretch the 640 x 480 image to fill the larger screen space.</para>
			/// If <see cref="FieldFlags.DisplayFixedOutput"/> is not set, this member must be zero.
			/// </summary>
			internal DisplayFixedOutput DisplayFixedOutput;


			/// <summary>Returns a hash code for this <see cref="DisplayInfo"/> structure.</summary>
			/// <returns>Returns a hash code for this <see cref="DisplayInfo"/> structure.</returns>
			public override int GetHashCode()
			{
				return Position.GetHashCode() ^ ( (int)DisplayOrientation | ( (int)DisplayFixedOutput << 8 ) );
			}


			/// <summary>Returns a value indicating whether this <see cref="DisplayInfo"/> structure equals another structure of the same type.</summary>
			/// <param name="other">A <see cref="DisplayInfo"/> structure.</param>
			/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
			public bool Equals( DisplayInfo other )
			{
				return ( Position.X == other.Position.X ) && ( Position.Y == other.Position.Y ) && ( DisplayOrientation == other.DisplayOrientation ) && ( DisplayFixedOutput == other.DisplayFixedOutput );
			}


			/// <summary>Returns a value indicating whether this <see cref="DisplayInfo"/> structure is equivalent to an object.</summary>
			/// <param name="obj">An object.</param>
			/// <returns>Returns true if the specified object is a <see cref="DisplayInfo"/> structure which equals this structure, otherwise returns false.</returns>
			public override bool Equals( object obj )
			{
				return ( obj is DisplayInfo ) && this.Equals( (DisplayInfo)obj );
			}


			#region Operators


			/// <summary>Equality comparer.</summary>
			/// <param name="displayDeviceModeDisplay">A <see cref="DisplayInfo"/> structure.</param>
			/// <param name="other">A <see cref="DisplayInfo"/> structure.</param>
			/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
			public static bool operator ==( DisplayInfo displayDeviceModeDisplay, DisplayInfo other )
			{
				return displayDeviceModeDisplay.Equals( other );
			}


			/// <summary>Inequality comparer.</summary>
			/// <param name="displayDeviceModeDisplay">A <see cref="DisplayInfo"/> structure.</param>
			/// <param name="other">A <see cref="DisplayInfo"/> structure.</param>
			/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
			public static bool operator !=( DisplayInfo displayDeviceModeDisplay, DisplayInfo other )
			{
				return !displayDeviceModeDisplay.Equals( other );
			}


			#endregion

		}



		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 32 )]
		private string deviceName;
		private short specVersion;
		private short driverVersion;
		/// <summary>Specifies the size, in bytes, of the <see cref="DisplayDeviceMode"/> structure, not including any private driver-specific data that might follow the structure's public members. Set this member to sizeof (<see cref="DisplayDeviceMode"/>) to indicate the version of the <see cref="DisplayDeviceMode"/> structure being used.</summary>
		private ushort structureSize;
		/// <summary>Contains the number of bytes of private driver-data that follow this structure. If a device driver does not use device-specific information, set this member to zero.</summary>
		private ushort driverExtra;
		/// <summary>Specifies whether certain members of the <see cref="DisplayDeviceMode"/> structure have been initialized. If a member is initialized, its corresponding bit is set, otherwise the bit is clear. A driver supports only those <see cref="DisplayDeviceMode"/> members that are appropriate for the printer or display technology.</summary>
		private FieldFlags fields;
		private DisplayInfo displayInfo;	// union DeviceModePrinter printerInfo;
		#region Printer fields
		/// <summary>Switches between color and monochrome on color printers.</summary>
		private short color;
		/// <summary>Selects duplex or double-sided printing for printers capable of duplex printing. Following are the possible values: Simplex, Horizontal, Vertical.</summary>
		private short duplex;
		/// <summary>Specifies the y-resolution, in dots per inch, of the printer. If the printer initializes this member, the dmPrintQuality member specifies the x-resolution, in dots per inch, of the printer.</summary>
		private short yResolution;
		/// <summary>Specifies how TrueType fonts should be printed. This member can be one of the following values: Bitmap, Download, DownloadOutline, SubDev.</summary>
		private short ttOption;
		/// <summary>Specifies whether collation should be used when printing multiple copies. (This member is ignored unless the printer driver indicates support for collation by setting the dmFields member to DM_COLLATE.) This member can be one of the following values: true, false.</summary>
		private short collate;
		/// <summary>A zero-terminated character array that specifies the name of the form to use; for example, "Letter" or "Legal". A complete set of names can be retrieved by using the EnumForms function.</summary>
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 32 )]
		private string formName;
		#endregion
		private short logPixels;
		private int bitsPerPel;
		private int pelsWidth;
		private int pelsHeight;
		private int displayFlags;	// or NUP (?)
		private int displayFrequency;
		#region Printer fields (again)
		private int icmMethod;
		private int icmIntent;
		private int mediaType;
		private int ditherType;
		#endregion
		#region Not used, must be zero
		private int reserved1;
		private int reserved2;
		private int panningWidth;
		private int panningHeight;
		#endregion


		private DisplayDeviceMode( ushort structSize )
			: this()
		{
			structureSize = structSize;
		}



		/// <summary>Gets a null-terminated string that specifies the "friendly" name of the display (or printer); for example, "PCL/HP LaserJet" in the case of PCL/HP LaserJet.
		/// <para>This string is unique among device drivers. Note that this name may be truncated to fit in the deviceName variable (32 chars).</para>
		/// </summary>
		public string DeviceName { get { return string.Copy( deviceName ?? string.Empty ); } }


		/// <summary>Gets the version number of the initialization data specification the structure is based on.
		/// <para>To ensure the correct version is used for any operating system, use DM_SPECVERSION.</para>
		/// </summary>
		public short SpecVersion { get { return specVersion; } }


		/// <summary>Gets the driver version number assigned by the driver developer.</summary>
		public short DriverVersion { get { return driverVersion; } }


		#region "Fields"


		/// <summary>Gets a <see cref="Point"/> structure indicating the positional coordinates of the display device in reference to the desktop area.
		/// <para>The primary display device is always located at coordinates (0,0).</para>
		/// </summary>
		public Point? DisplayPosition
		{
			get
			{
				if( fields.HasFlag( FieldFlags.Position ) )
					return displayInfo.Position;
				return null;
			}
		}


		/// <summary>Gets the orientation images should be presented at. If <see cref="FieldFlags.DisplayOrientation"/> is not set, this member must be <see cref="DisplayRotation.Unspecified"/>.
		/// <para>To determine whether the display orientation is portrait or landscape orientation, check the <see cref="PelsWidth"/>:<see cref="PelsHeight"/> ratio.</para>
		/// </summary>
		public DisplayRotation DisplayOrientation
		{
			get
			{
				if( !fields.HasFlag( FieldFlags.DisplayOrientation ) )
					return DisplayRotation.Unspecified;

				return (DisplayRotation)( (int)displayInfo.DisplayOrientation + 1 );
			}
		}


		/// <summary>For fixed-resolution display devices only: gets a value indicating how the display presents a low-resolution mode on a higher-resolution display.
		/// <para>For example, if a display device's resolution is fixed at 1024×768 pixels but its mode is set to 640×480 pixels, the device can either display a 640×480 image somewhere in the interior of the 1024×768 screen space or stretch the 640×480 image to fill the larger screen space.</para>
		/// If <see cref="FieldFlags.DisplayFixedOutput"/> is not set, this member must be <see cref="DisplayFixedOutput.Default"/>.
		/// </summary>
		public DisplayFixedOutput FixedOutput
		{
			get
			{
				if( !fields.HasFlag( FieldFlags.DisplayFixedOutput ) )
					return DisplayFixedOutput.Default;
				
				return displayInfo.DisplayFixedOutput;
			}
		}


		/// <summary>Gets a value indicating whether the device's display mode is grayscale.</summary>
		public bool? IsGrayscale
		{
			get
			{
				if( fields.HasFlag( FieldFlags.DisplayFlags ) )
					return ( displayFlags & 0x00000001 ) == 0x00000001;
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the device's display mode is interlaced.</summary>
		public bool? IsInterlaced
		{
			get
			{
				if( fields.HasFlag( FieldFlags.DisplayFlags ) )
					return ( displayFlags & 0x00000002 ) == 0x00000002;
				return null;
			}
		}



		/// <summary>Gets the frequency, in hertz (cycles per second), of the display device in a particular mode. This value is also known as the display device's vertical refresh rate.
		/// <para>When you call the EnumDisplaySettings function, the DisplayFrequency member may return with the value 0 or 1; these values represent the display hardware's default refresh rate.</para>
		/// This default rate is typically set by switches on a display card or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.
		/// </summary>
		public int? DisplayFrequency
		{
			get
			{
				if( fields.HasFlag( FieldFlags.DisplayFrequency ) )
					return displayFrequency;
				return null;
			}
		}


		/// <summary>Gets the color resolution, in bits per pixel, of the display device (for example: 4 bits for 16 colors, 8 bits for 256 colors, or 16 bits for 65'536 colors).</summary>
		public int? BitsPerPel
		{
			get
			{
				if( fields.HasFlag( FieldFlags.BitsPerPixel ) )
					return bitsPerPel;
				return null;
			}
		}


		/// <summary>Gets the width, in pixels, of the visible device surface.</summary>
		public int? PelsWidth
		{
			get
			{
				if( fields.HasFlag( FieldFlags.PelsWidth ) )
					return pelsWidth;
				return null;
			}
		}


		/// <summary>Gets the height, in pixels, of the visible device surface.</summary>
		public int? PelsHeight
		{
			get
			{
				if( fields.HasFlag( FieldFlags.PelsHeight ) )
					return pelsHeight;
				return null;
			}
		}


		/// <summary>Gets the number of pixels per logical inch.</summary>
		public short? LogPixels
		{
			get
			{
				if( fields.HasFlag( FieldFlags.LogPixels ) )
					return logPixels;
				return null;
			}
		}


		#endregion


		/// <summary>Returns a hash code for this <see cref="DisplayDeviceMode"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DisplayDeviceMode"/> structure.</returns>
		public override int GetHashCode()
		{
			return this.DeviceName.GetHashCode() ^ specVersion.GetHashCode() ^ driverVersion.GetHashCode() ^ structureSize.GetHashCode() ^ driverExtra.GetHashCode() ^ 
				fields.GetHashCode() ^ displayInfo.GetHashCode() ^ color.GetHashCode() ^ duplex.GetHashCode() ^ yResolution.GetHashCode() ^ ttOption.GetHashCode() ^ collate.GetHashCode() ^ 
				( formName == null ? 0 : formName.GetHashCode() ) ^ logPixels.GetHashCode() ^ bitsPerPel ^ pelsWidth ^ pelsHeight ^ displayFlags ^ displayFrequency ^ icmMethod ^ icmIntent ^
				mediaType ^ ditherType ^ reserved1  ^ reserved2 ^ panningWidth ^ panningHeight;
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDeviceMode"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( DisplayDeviceMode other )
		{
			return this.DeviceName.Equals( other.DeviceName, StringComparison.Ordinal ) &&
				( specVersion == other.specVersion ) && ( driverVersion == other.driverVersion ) && ( structureSize == other.structureSize ) && ( driverExtra == other.driverExtra ) &&
				( fields == other.fields ) && displayInfo.Equals( other.displayInfo ) && ( color == other.color ) && ( duplex == other.duplex ) &&
				( yResolution == other.yResolution ) && ( ttOption == other.ttOption ) && ( collate == other.collate ) &&
				( formName == null ? other.formName == null : formName.Equals( other.formName, StringComparison.Ordinal ) ) &&
				( logPixels == other.logPixels ) && ( bitsPerPel == other.bitsPerPel ) && ( pelsWidth == other.pelsWidth ) && ( pelsHeight == other.pelsHeight ) &&
				( displayFlags == other.displayFlags ) && ( displayFrequency == other.displayFrequency ) && ( icmMethod == other.icmMethod ) && ( icmIntent == other.icmIntent ) &&
				( mediaType == other.mediaType ) && ( ditherType == other.ditherType ) && ( reserved1 == other.reserved1 ) && ( reserved2 == other.reserved2 ) &&
				( panningWidth == other.panningWidth ) && ( panningHeight == other.panningHeight );
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDeviceMode"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DisplayDeviceMode"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is DisplayDeviceMode ) && this.Equals( (DisplayDeviceMode)obj );
		}


		/// <summary>Returns a string representing this <see cref="DisplayDeviceMode"/> structure, in the form:
		/// <para>{<see cref="PelsWidth"/>}×{<see cref="PelsHeight"/>}@{<see cref="DisplayFrequency"/>}Hz</para>
		/// </summary>
		/// <returns>Returns a string representing this <see cref="DisplayDeviceMode"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( CultureInfo.InvariantCulture, "{0}×{1}@{2}Hz", pelsWidth, pelsHeight, displayFrequency );
		}


		/// <summary>Returns a value indicating whether this <see cref="DisplayDeviceMode"/> structure is smaller, greater than or equal to another structure of the same type.</summary>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns></returns>
		public int CompareTo( DisplayDeviceMode other )
		{
			if( pelsWidth < other.pelsWidth )
				return -1;

			if( pelsWidth > other.pelsWidth )
				return +1;


			if( pelsHeight < other.pelsHeight )
				return -1;

			if( pelsHeight > other.pelsHeight )
				return +1;


			if( displayFrequency < other.displayFrequency )
				return -1;

			if( displayFrequency > other.displayFrequency )
				return +1;


			if( bitsPerPel < other.bitsPerPel )
				return -1;

			if( bitsPerPel > other.bitsPerPel )
				return +1;

			return 0;
		}



		/// <summary>The empty (and invalid) <see cref="DisplayDeviceMode"/> structure.</summary>
		private static readonly DisplayDeviceMode Empty = new DisplayDeviceMode();

		/// <summary>The default <see cref="DisplayDeviceMode"/> structure.</summary>
		public static readonly DisplayDeviceMode Default = new DisplayDeviceMode( (ushort)Marshal.SizeOf( typeof( DisplayDeviceMode ) ) );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="displayDeviceMode">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( DisplayDeviceMode displayDeviceMode, DisplayDeviceMode other )
		{
			return displayDeviceMode.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="displayDeviceMode">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( DisplayDeviceMode displayDeviceMode, DisplayDeviceMode other )
		{
			return !displayDeviceMode.Equals( other );
		}


		/// <summary>Inferiority comparer.</summary>
		/// <param name="displayDeviceMode">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if the <paramref name="displayDeviceMode"/> structure is lower than the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator <( DisplayDeviceMode displayDeviceMode, DisplayDeviceMode other )
		{
			return displayDeviceMode.CompareTo( other ) < 0;
		}


		/// <summary>Superiority comparer.</summary>
		/// <param name="displayDeviceMode">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if the <paramref name="displayDeviceMode"/> structure is greater than the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator >( DisplayDeviceMode displayDeviceMode, DisplayDeviceMode other )
		{
			return displayDeviceMode.CompareTo( other ) > 0;
		}


		/// <summary>Inferiority or equality comparer.</summary>
		/// <param name="displayDeviceMode">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if the <paramref name="displayDeviceMode"/> structure is lower than or equal to the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator <=( DisplayDeviceMode displayDeviceMode, DisplayDeviceMode other )
		{
			return displayDeviceMode.CompareTo( other ) <= 0;
		}


		/// <summary>Superiority or equality comparer.</summary>
		/// <param name="displayDeviceMode">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <param name="other">A <see cref="DisplayDeviceMode"/> structure.</param>
		/// <returns>Returns true if the <paramref name="displayDeviceMode"/> structure is greater than or equal to the <paramref name="other"/> structure, otherwise returns false.</returns>
		public static bool operator >=( DisplayDeviceMode displayDeviceMode, DisplayDeviceMode other )
		{
			return displayDeviceMode.CompareTo( other ) >= 0;
		}

		#endregion


	}

}