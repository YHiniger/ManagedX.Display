using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Display.DisplayConfig
{
	using Graphics;


	/// <summary>Provides access to DisplayConfig (defined in WinUser.h).
	/// <para>Requires Windows 7 or newer.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/hh406259%28v=vs.85%29.aspx</remarks>
	public sealed class DisplayConfiguration
	{

		private const int MaxSourceCount = 16;
		private const int MaxClonePerSource = 4;

		/// <summary>Defines the maximum number of paths: 1024.</summary>
		public const int MaxPathCount = MaxSourceCount * DisplayAdapter.MaxAdapterCount * MaxClonePerSource;


		/// <summary>Provides access to native functions (located in User32.dll, defined in WinUser.h) related to DisplayConfig.
		/// <para>Requires Windows 7 or newer.</para>
		/// </summary>
		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Retrieves the size of the buffers that are required to call the QueryDisplayConfig function.
			/// <para>Not available on Windows Vista.</para>
			/// </summary>
			/// <param name="flags">The type of information to retrieve.</param>
			/// <param name="pathArrayElementCount">Pointer to a variable that receives the number of elements in the path information table.
			/// The <paramref name="pathArrayElementCount"/> parameter value is then used by a subsequent call to the QueryDisplayConfig function.
			/// This parameter cannot be null.
			/// </param>
			/// <param name="modeInfoArrayElementCount">Pointer to a variable that receives the number of elements in the mode information table.
			/// The <paramref name="modeInfoArrayElementCount"/> parameter value is then used by a subsequent call to the QueryDisplayConfig function.
			/// This parameter cannot be null.
			/// </param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>, 
			/// <see cref="ErrorCode.NotSupported"/>, 
			/// <see cref="ErrorCode.AccessDenied"/>, or
			/// <see cref="ErrorCode.GenFailure"/>.
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			private static extern ErrorCode GetDisplayConfigBufferSizes(
				[In] QueryDisplayConfigRequest flags,
				out int pathArrayElementCount,
				out int modeInfoArrayElementCount
			);
			// https://msdn.microsoft.com/en-us/library/windows/hardware/ff566772%28v=vs.85%29.aspx
			//LONG GetDisplayConfigBufferSizes(
			//	_In_ UINT32 flags,
			//	_Out_ UINT32* numPathArrayElements,
			//	_Out_ UINT32* numModeInfoArrayElements);


			#region QueryDisplayConfig

			// https://msdn.microsoft.com/en-us/library/windows/hardware/ff569215%28v=vs.85%29.aspx
			//LONG QueryDisplayConfig(
			//	_In_ UINT32 flags,
			//	_Inout_ UINT32* numPathArrayElements,
			//	_Out_writes_to_(*numPathArrayElements, *numPathArrayElements) DISPLAYCONFIG_PATH_INFO* pathArray,
			//	_Inout_ UINT32* numModeInfoArrayElements,
			//	_Out_writes_to_(*numModeInfoArrayElements, *numModeInfoArrayElements) DISPLAYCONFIG_MODE_INFO* modeInfoArray,
			//	_When_(!(flags & QDC_DATABASE_CURRENT), _Pre_null_)
			//	_When_(flags & QDC_DATABASE_CURRENT, _Out_)
			//		DISPLAYCONFIG_TOPOLOGY_ID* currentTopologyId);


			/// <summary>Retrieves information about all possible display paths for all display devices, or views, in the current setting.</summary>
			/// <param name="flags">The type of information to retrieve; must not be <see cref="QueryDisplayConfigRequest.None"/> nor specify <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>.</param>
			/// <param name="pathInfoArrayElementCount">
			/// Pointer to a variable that contains the number of elements in <paramref name="pathInfoArray"/>. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="pathInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="pathInfoArray"/>.
			/// </param>
			/// <param name="pathInfoArray">
			/// Pointer to a variable that contains an array of <see cref="PathInfo"/> elements. Each element in <paramref name="pathInfoArray"/> describes a single path from a source to a target.
			/// The source and target mode information indexes are only valid in combination with the <paramref name="modeInfoArray"/> tables that are returned for the API at the same time.
			/// This parameter cannot be null.
			/// The <paramref name="pathInfoArray"/> is always returned in path priority order.
			/// </param>
			/// <param name="modeInfoArrayElementCount">
			/// Pointer to a variable that specifies the number in element of the mode information table. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="modeInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="modeInfoArray"/>. 
			/// </param>
			/// <param name="modeInfoArray">Pointer to a variable that contains an array of <see cref="ModeInfo"/> elements. This parameter cannot be null.</param>
			/// <param name="reserved">The currentTopologyId parameter is only set when the <paramref name="flags"/> parameter is set to <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>; must be null.</param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.GenFailure"/> or
			/// <see cref="ErrorCode.InsufficientBuffer"/>.
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			private static extern ErrorCode QueryDisplayConfig(
				[In] QueryDisplayConfigRequest flags,
				[In, Out] ref int pathInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 )] PathInfo[] pathInfoArray,
				[In, Out] ref int modeInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 3 )] ModeInfo[] modeInfoArray,
				[In] IntPtr reserved
			);


			/// <summary>Retrieves information about all possible display paths for all display devices, or views, in the current setting.</summary>
			/// <param name="flags">The type of information to retrieve; must be or specify <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>.</param>
			/// <param name="pathInfoArrayElementCount">
			/// Pointer to a variable that contains the number of elements in <paramref name="pathInfoArray"/>. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="pathInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="pathInfoArray"/>.
			/// </param>
			/// <param name="pathInfoArray">
			/// Pointer to a variable that contains an array of <see cref="PathInfo"/> elements. Each element in <paramref name="pathInfoArray"/> describes a single path from a source to a target.
			/// The source and target mode information indexes are only valid in combination with the <paramref name="modeInfoArray"/> tables that are returned for the API at the same time.
			/// This parameter cannot be null.
			/// The <paramref name="pathInfoArray"/> is always returned in path priority order.
			/// </param>
			/// <param name="modeInfoArrayElementCount">
			/// Pointer to a variable that specifies the number in element of the mode information table. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="modeInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="modeInfoArray"/>. 
			/// </param>
			/// <param name="modeInfoArray">Pointer to a variable that contains an array of <see cref="ModeInfo"/> elements. This parameter cannot be null.</param>
			/// <param name="currentTopologyId">Pointer to a variable that receives the identifier of the currently active topology in the CCD database.
			/// For a list of possible values, see the <see cref="TopologyId"/> enumerated type.
			/// The currentTopologyId parameter is only set when the <paramref name="flags"/> parameter value is QDC_DATABASE_CURRENT.
			/// If the <paramref name="flags"/> parameter value is set to QDC_DATABASE_CURRENT, the currentTopologyId parameter must not be null.
			/// If the <paramref name="flags"/> parameter value is not set to QDC_DATABASE_CURRENT, the currentTopologyId parameter value must be null.
			/// </param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.GenFailure"/> or
			/// <see cref="ErrorCode.InsufficientBuffer"/>.
			/// </returns>
			/// <remarks></remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			private static extern ErrorCode QueryDisplayConfig(
				[In] QueryDisplayConfigRequest flags,
				[In, Out] ref int pathInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 )] PathInfo[] pathInfoArray,
				[In, Out] ref int modeInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 3 )] ModeInfo[] modeInfoArray,
				out TopologyId currentTopologyId
			);



			/// <summary>Retrieves information about all possible display paths for all display devices, or views, in the current setting.</summary>
			/// <param name="request">The type of information to retrieve; must not be <see cref="QueryDisplayConfigRequest.None"/>.</param>
			/// <param name="pathInfoArray">Receives an array of <see cref="PathInfo"/> elements; can't be null.
			/// <para>
			/// Each element in <paramref name="pathInfoArray"/> describes a single path from a source to a target.
			/// The source and target mode information indexes are only valid in combination with the <paramref name="modeInfoArray"/> tables that are returned for the API at the same time.
			/// The <paramref name="pathInfoArray"/> is always returned in path priority order.
			/// </para>
			/// </param>
			/// <param name="modeInfoArray">Receives an array of <see cref="ModeInfo"/> elements; can't be null.</param>
			/// <param name="currentTopologyId">When <paramref name="request"/> is or specifies <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>, receives the identifier of the currently active topology in the CCD database.</param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.GenFailure"/> or
			/// <see cref="ErrorCode.InsufficientBuffer"/>.
			/// </returns>
			/// <exception cref="InvalidEnumArgumentException"/>
			internal static ErrorCode QueryDisplayConfig( QueryDisplayConfigRequest request, out PathInfo[] pathInfoArray, out ModeInfo[] modeInfoArray, out TopologyId currentTopologyId )
			{
				if( request == QueryDisplayConfigRequest.None )
					throw new InvalidEnumArgumentException( "request", (int)request, typeof( QueryDisplayConfigRequest ) );

				currentTopologyId = TopologyId.Unspecified;

				int pathInfoArrayElementCount;
				int modeInfoArrayElementCount;

				var errorCode = GetDisplayConfigBufferSizes( request, out pathInfoArrayElementCount, out modeInfoArrayElementCount );
				if( errorCode != ErrorCode.None )
				{
					pathInfoArray = new PathInfo[ 0 ];
					modeInfoArray = new ModeInfo[ 0 ];
					return errorCode;
				}

				pathInfoArray = new PathInfo[ pathInfoArrayElementCount ];
				modeInfoArray = new ModeInfo[ modeInfoArrayElementCount ];

				if( request.HasFlag( QueryDisplayConfigRequest.DatabaseCurrent ) )
					return QueryDisplayConfig( request, ref pathInfoArrayElementCount, pathInfoArray, ref modeInfoArrayElementCount, modeInfoArray, out currentTopologyId );
				else
					return QueryDisplayConfig( request, ref pathInfoArrayElementCount, pathInfoArray, ref modeInfoArrayElementCount, modeInfoArray, IntPtr.Zero );
			}

			#endregion QueryDisplayConfig


			#region DisplayConfigGetDeviceInfo

			// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553903%28v=vs.85%29.aspx
			//LONG DisplayConfigGetDeviceInfo(
			//	_Inout_ DISPLAYCONFIG_DEVICE_INFO_HEADER* requestPacket);

			/// <summary>Retrieves display configuration information about the device.</summary>
			/// <param name="requestPacket">
			/// A pointer to a <see cref="DeviceInfoHeader"/> structure.
			/// This structure contains information about the request, which includes the packet type in the type member.
			/// The type and size of additional data that the function returns after the header structure depend on the packet type.
			/// </param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.InsufficientBuffer"/>, or
			/// <see cref="ErrorCode.GenFailure"/>.</returns>
			/// <remarks>
			/// Use this function to obtain additional information about a source or target for an adapter, such as the display name, the preferred display mode, and source device name.
			/// The caller can call this function to obtain more friendly names to display in the user interface.
			/// The caller can obtain names for the adapter, the source, and the target.
			/// The caller can also call this function to obtain the best resolution of the connected display device.
			/// </remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode DisplayConfigGetDeviceInfo(
				[In, Out] ref AdapterName requestPacket
			);

			/// <summary>Retrieves display configuration information about the device.</summary>
			/// <param name="requestPacket">
			/// A pointer to a <see cref="DeviceInfoHeader"/> structure.
			/// This structure contains information about the request, which includes the packet type in the type member.
			/// The type and size of additional data that the function returns after the header structure depend on the packet type.
			/// </param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.InsufficientBuffer"/>, or
			/// <see cref="ErrorCode.GenFailure"/>.</returns>
			/// <remarks>
			/// Use this function to obtain additional information about a source or target for an adapter, such as the display name, the preferred display mode, and source device name.
			/// The caller can call this function to obtain more friendly names to display in the user interface.
			/// The caller can obtain names for the adapter, the source, and the target.
			/// The caller can also call this function to obtain the best resolution of the connected display device.
			/// </remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode DisplayConfigGetDeviceInfo(
				[In, Out] ref SourceDeviceName requestPacket
			);

			/// <summary>Retrieves display configuration information about the device.</summary>
			/// <param name="requestPacket">
			/// A pointer to a <see cref="DeviceInfoHeader"/> structure.
			/// This structure contains information about the request, which includes the packet type in the type member.
			/// The type and size of additional data that the function returns after the header structure depend on the packet type.
			/// </param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.InsufficientBuffer"/>, or
			/// <see cref="ErrorCode.GenFailure"/>.
			/// </returns>
			/// <remarks>
			/// Use this function to obtain additional information about a source or target for an adapter, such as the display name, the preferred display mode, and source device name.
			/// The caller can call this function to obtain more friendly names to display in the user interface.
			/// The caller can obtain names for the adapter, the source, and the target.
			/// The caller can also call this function to obtain the best resolution of the connected display device.
			/// </remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode DisplayConfigGetDeviceInfo(
				[In, Out] ref TargetDeviceName requestPacket
			);

			///// <summary>Retrieves display configuration information about the device.</summary>
			///// <param name="requestPacket">
			///// A pointer to a <see cref="DeviceInfoHeader"/> structure.
			///// This structure contains information about the request, which includes the packet type in the type member.
			///// The type and size of additional data that the function returns after the header structure depend on the packet type.
			///// </param>
			///// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			///// <see cref="ErrorCode.InvalidParameter"/>,
			///// <see cref="ErrorCode.NotSupported"/>,
			///// <see cref="ErrorCode.AccessDenied"/>,
			///// <see cref="ErrorCode.InsufficientBuffer"/>, or
			///// <see cref="ErrorCode.GenFailure"/>.</returns>
			///// <remarks>
			///// Use this function to obtain additional information about a source or target for an adapter, such as the display name, the preferred display mode, and source device name.
			///// The caller can call this function to obtain more friendly names to display in the user interface.
			///// The caller can obtain names for the adapter, the source, and the target.
			///// The caller can also call this function to obtain the best resolution of the connected display device.
			///// </remarks>
			//[DllImport( LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//internal static extern ErrorCode DisplayConfigGetDeviceInfo(
			//	[In, Out] ref TargetPreferredMode requestPacket
			//);

			///// <summary>Retrieves display configuration information about the device.
			///// <para>Requires Windows 8.1 or newer.</para>
			///// </summary>
			///// <param name="requestPacket">
			///// A pointer to a <see cref="DeviceInfoHeader"/> structure.
			///// This structure contains information about the request, which includes the packet type in the type member.
			///// The type and size of additional data that the function returns after the header structure depend on the packet type.
			///// </param>
			///// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			///// <see cref="ErrorCode.InvalidParameter"/>,
			///// <see cref="ErrorCode.NotSupported"/>,
			///// <see cref="ErrorCode.AccessDenied"/>,
			///// <see cref="ErrorCode.InsufficientBuffer"/>, or
			///// <see cref="ErrorCode.GenFailure"/>.</returns>
			///// <remarks>
			///// Use this function to obtain additional information about a source or target for an adapter, such as the display name, the preferred display mode, and source device name.
			///// The caller can call this function to obtain more friendly names to display in the user interface.
			///// The caller can obtain names for the adapter, the source, and the target.
			///// The caller can also call this function to obtain the best resolution of the connected display device.
			///// </remarks>
			//[DllImport( LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//internal static extern ErrorCode DisplayConfigGetDeviceInfo(
			//	[In, Out] ref TargetBaseType requestPacket
			//);

			#endregion DisplayConfigGetDeviceInfo

		}


		/// <summary>Returns an exception for the specified error code:
		/// <para>NotSupportedException</para>
		/// <para>UnauthorizedAccessException</para>
		/// <para>InsufficientMemoryException</para>
		/// <para>ArgumentException</para>
		/// <para>InvalidOperationException</para>
		/// <para>Win32Exception</para>
		/// </summary>
		/// <param name="errorCode">An error code.</param>
		/// <returns>Returns an exception, or null if <paramref name="errorCode"/> is <see cref="ErrorCode.None"/>.</returns>
		private static Exception GetException( ErrorCode errorCode )
		{
			if( errorCode == ErrorCode.None )
				return null;
			
			if( errorCode == ErrorCode.AccessDenied )
				return new UnauthorizedAccessException( "Access denied." );

			if( errorCode == ErrorCode.NotSupported )
				return new NotSupportedException( "The function is only supported on a system with a WDDM driver running." );

			if( errorCode == ErrorCode.GenFailure )
				return new InvalidOperationException( "A device attached to the system is not functioning." );

			if( errorCode == ErrorCode.InsufficientBuffer )
				return new InsufficientMemoryException( "The supplied path and mode buffers are too small." );

			if( errorCode == ErrorCode.InvalidParameter )
				return new ArgumentException( "Invalid combination of parameters and flags." );

			var ex = Marshal.GetExceptionForHR( (int)errorCode );
			if( ex == null )
				ex = new Win32Exception( (int)errorCode );
			return ex;
		}


		/// <summary>Returns a <see cref="SourceDeviceName"/> structure containing the device name of the source device.</summary>
		/// <param name="sourceInfo">A valid <see cref="PathSourceInfo"/> structure; must not be empty.</param>
		/// <returns>Returns a <see cref="SourceDeviceName"/> structure containing the device name of the source device.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="Exception"/>
		public static SourceDeviceName GetSourceDeviceName( PathSourceInfo sourceInfo )
		{
			if( sourceInfo == PathSourceInfo.Empty )
				throw new ArgumentException( "Invalid source info: empty.", "sourceInfo" );

			var deviceName = new SourceDeviceName( sourceInfo.AdapterId, sourceInfo.Id );
			var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( ref deviceName );
			if( errorCode == ErrorCode.None )
				return deviceName;

			throw GetException( errorCode );
		}

		/// <summary>Returns a <see cref="TargetDeviceName"/> structure containing information about the specified display target.</summary>
		/// <param name="targetInfo">A valid <see cref="PathTargetInfo"/> structure; must not be empty.</param>
		/// <returns>Returns a <see cref="TargetDeviceName"/> structure containing information about the specified display target.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="Exception"/>
		public static TargetDeviceName GetTargetDeviceName( PathTargetInfo targetInfo )
		{
			if( targetInfo == PathTargetInfo.Empty )
				throw new ArgumentException( "Invalid target info: empty.", "targetInfo" );

			var deviceName = new TargetDeviceName( targetInfo.AdapterId, targetInfo.Id );
			var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( ref deviceName );
			if( errorCode == ErrorCode.None )
				return deviceName;

			throw GetException( errorCode );
		}


		/// <summary>Returns the adapter device path for a given adapter LUID and id.</summary>
		/// <param name="adapterId">The adapter <see cref="Luid"/>.</param>
		/// <param name="id">The adapter id.</param>
		/// <returns>Returns the adapter device path.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="Exception"/>
		private static string GetAdapterDeviceName( Luid adapterId, int id )
		{
			if( adapterId == Luid.Zero )
				throw new ArgumentException( "Invalid adapter id.", "adapterId" );

			var adapterName = new AdapterName( adapterId, id );
			var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( ref adapterName );
			if( errorCode == ErrorCode.None )
				return adapterName.DeviceName;
			
			throw GetException( errorCode );
		}


		/// <summary>Returns the device path of an adapter given its source info.</summary>
		/// <param name="sourceInfo">A valid <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns the adapter device path.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="Exception"/>
		public static string GetAdapterDeviceName( PathSourceInfo sourceInfo )
		{
			if( sourceInfo == PathSourceInfo.Empty )
				throw new ArgumentException( "Invalid source info.", "sourceInfo" );

			try
			{
				return GetAdapterDeviceName( sourceInfo.AdapterId, sourceInfo.Id );
			}
			catch( ArgumentException ex )
			{
				throw new ArgumentException( "Invalid source info.", "sourceInfo", ex );
			}
			catch( Exception )
			{
				throw;
			}
		}

		/// <summary>Returns the device path of an adapter given its target info.</summary>
		/// <param name="targetInfo">A valid <see cref="PathTargetInfo"/> structure.</param>
		/// <returns>Returns the adapter device path.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="Exception"/>
		public static string GetAdapterDeviceName( PathTargetInfo targetInfo )
		{
			if( targetInfo == PathTargetInfo.Empty )
				throw new ArgumentException( "Invalid target info.", "targetInfo" );

			try
			{
				return GetAdapterDeviceName( targetInfo.AdapterId, targetInfo.Id );
			}
			catch( ArgumentException ex )
			{
				throw new ArgumentException( "Invalid target info.", "targetInfo", ex );
			}
			catch( Exception )
			{
				throw;
			}
		}



		///// <summary></summary>
		///// <param name="targetInfo"></param>
		///// <returns></returns>
		///// <exception cref="ArgumentException"/>
		///// <exception cref="Exception"/>
		//public static TargetPreferredMode GetPreferredMode( PathTargetInfo targetInfo )
		//{
		//	if( targetInfo == PathTargetInfo.Empty )
		//		throw new ArgumentException( "Invalid target info.", "targetInfo" );

		//	var preferredMode = new TargetPreferredMode( targetInfo.AdapterId, targetInfo.Id );
		//	var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( ref preferredMode );
		//	// FIXME - returns InvalidParameter
		//	if( errorCode == ErrorCode.None )
		//		return preferredMode;

		//	//throw GetException( errorCode );
		//	return TargetPreferredMode.Empty;
		//}


		/// <summary>Defines the minimum version of Windows supported by DisplayConfig: 6.1 (Windows 7).</summary>
		private static readonly Version MinimumOSVersion = new Version( 6, 1 );

		/// <summary>Gets a value indicating whether DisplayConfig is supported by the operating system.</summary>
		public static bool IsSupported
		{
			get
			{
				try
				{
					var osVersion = Environment.OSVersion;
					return ( osVersion.Platform == PlatformID.Win32NT ) && ( osVersion.Version >= MinimumOSVersion );
				}
				catch( InvalidOperationException )
				{
					return false;
				}
			}
		}


		/// <summary>Queries and returns a display configuration.</summary>
		/// <param name="request">The request (flags?); must not be <see cref="QueryDisplayConfigRequest.None"/>.</param>
		/// <returns>Returns a <see cref="DisplayConfiguration"/> object containing the requested information.</returns>
		/// <exception cref="PlatformNotSupportedException"/>
		/// <exception cref="InvalidEnumArgumentException"/>
		/// <exception cref="InvalidOperationException"/>
		public static DisplayConfiguration Query( QueryDisplayConfigRequest request )
		{
			if( !IsSupported )
				throw new PlatformNotSupportedException( "DisplayConfig is only available on Windows 7 or greater." );

			if( request == QueryDisplayConfigRequest.None )
				throw new InvalidEnumArgumentException( "request", (int)request, typeof( QueryDisplayConfigRequest ) );

			// TODO - store the DisplayConfiguration in a Dictionary<QueryDisplayConfigRequest, DisplayConfiguration> ?
			try
			{
				return new DisplayConfiguration( request );
			}
			catch( Exception )
			{
				throw;
			}
		}



		private QueryDisplayConfigRequest request;
		private PathInfo[] paths;
		private ModeInfo[] modes;
		private TopologyId topologyId;



		/// <summary>Instantiates a new <see cref="DisplayConfiguration"/>.</summary>
		/// <param name="request">A value of the <see cref="QueryDisplayConfigRequest"/> enumeration; must not be <see cref="QueryDisplayConfigRequest.None"/>.</param>
		/// <exception cref="InvalidOperationException"/>
		private DisplayConfiguration( QueryDisplayConfigRequest request )
		{
			this.request = request;

			try
			{
				this.Refresh();
			}
			catch( InvalidOperationException ex )
			{
				throw new InvalidOperationException( "Failed to instantiate DisplayConfiguration.", ex );
			}
		}



		/// <summary>Gets the request associated with this <see cref="DisplayConfiguration"/>.
		/// <para>This is the request passed to the constructor (via the <see cref="Query"/> method) and used to <see cref="Refresh"/> the configuration.</para>
		/// </summary>
		public QueryDisplayConfigRequest Request { get { return request; } }


		/// <summary>Refreshes the <see cref="DisplayConfiguration"/>.</summary>
		/// <exception cref="InvalidOperationException"/>
		public void Refresh()
		{
			var error = SafeNativeMethods.QueryDisplayConfig( request, out paths, out modes, out topologyId );
			if( error != ErrorCode.None )
				throw new InvalidOperationException( "Failed to refresh display configuration.", GetException( error ) );
		}


		/// <summary>Gets a read-only collection containing information about all display paths for this configuration.</summary>
		public ReadOnlyPathInfoCollection PathInfo { get { return new ReadOnlyPathInfoCollection( paths ); } }


		/// <summary>Gets a read-only collection containing information about supported display modes for this configuration.</summary>
		public ReadOnlyModeInfoCollection ModeInfo { get { return new ReadOnlyModeInfoCollection( modes ); } }


		/// <summary>Gets the type of display topology.</summary>
		public TopologyId Topology { get { return topologyId; } }

	}

}