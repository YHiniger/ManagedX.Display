using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Display.DisplayConfig
{

	/// <summary>Enumerates constants which represent video signal standards.
	/// <para>This enumeration reflects the native <code>D3DKMDT_VIDEO_SIGNAL_STANDARD</code> enumeration (defined in D3Dkmdt.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff546632%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "D3Dkmdt.h", "D3DKMDT_VIDEO_SIGNAL_STANDARD" )]
	public enum VideoSignalStandard : int
	{

		/// <summary>Indicates that a variable of type VideoSignalStandard has not yet been assigned a meaningful value.</summary>
		Uninitialized = 0,

		/// <summary>Represents the Video Electronics Standards Association (VESA) Display Monitor Timing (DMT) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "VESA" )]
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DMT" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		VESA_DMT = 1,

		/// <summary>Represents the Video Electronics Standards Association (VESA) Generalized Timing Formula (GTF) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "VESA" )]
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GTF" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		VESA_GTF = 2,

		/// <summary>Represents the Video Electronics Standards Association (VESA) Coordinated Video Timing (CVT) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "VESA" )]
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CVT" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		VESA_CVT = 3,

		/// <summary>Represents the IBM standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IBM" )]
		IBM = 4,

		/// <summary>Represents the Apple standard.</summary>
		Apple = 5,

		/// <summary>Represents the National Television Standards Committee (NTSC) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NTSC" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		NTSC_M = 6,

		/// <summary>Represents the National Television Standards Committee (NTSC) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NTSC" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		NTSC_J = 7,

		/// <summary>Represents the National Television Standards Committee (NTSC) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NTSC" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		NTSC_443 = 8,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_B = 9,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_B1 = 10,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_G = 11,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_H = 12,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_I = 13,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_D = 14,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_N = 15,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_NC = 16,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_B = 17,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_D = 18,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_G = 19,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_H = 20,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_K = 21,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_K1 = 22,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_L = 23,

		/// <summary>Represents the Système Electronique Pour Couleur Avec Mémoire (SECAM) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SECAM" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		SECAM_L1 = 24,

		/// <summary>Represents the Electronics Industries Association (EIA) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EIA" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		EIA_861 = 25,

		/// <summary>Represents the Electronics Industries Association (EIA) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EIA" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		EIA_861A = 26,

		/// <summary>Represents the Electronics Industries Association (EIA) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "EIA" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		EIA_861B = 27,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_K = 28,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_K1 = 29,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		PAL_L = 30,

		/// <summary>Represents the Phase Alteration Line (PAL) standard.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores" )]
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PAL" )]
		PAL_M = 31,

		/// <summary>Represents any video standard other than those represented by the previous constants in this enumeration.</summary>
		Other = 255

	}

}