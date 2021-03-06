﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Graphics.DisplayConfig
{
	using Win32;


	/// <summary>Provides access to DisplayConfig.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/hh406259%28v=vs.85%29.aspx</remarks>
	public sealed class DisplayConfiguration
	{

		private const int MaxSourceCount = 16;
		private const int MaxClonePerSource = 4;

		/// <summary>Defines the maximum number of paths: 1024.</summary>
		[Source( "WinGDI.h", "DISPLAYCONFIG_MAXPATH" )]
		public const int MaxPathCount = DisplayAdapter.MaxAdapterCount * MaxSourceCount * MaxClonePerSource;



		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";


			/// <summary>Retrieves the size of the buffers that are required to call the QueryDisplayConfig function.</summary>
			/// <param name="request">The type of information to retrieve.</param>
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode GetDisplayConfigBufferSizes(
				[In] QueryDisplayConfigRequest request,
				[Out] out int pathArrayElementCount,
				[Out] out int modeInfoArrayElementCount
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
			/// <param name="request">The type of information to retrieve; must not be <see cref="QueryDisplayConfigRequest.None"/> nor specify <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>.</param>
			/// <param name="pathInfoArrayElementCount">
			/// Pointer to a variable that contains the number of elements in <paramref name="pathInfoArray"/>. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="pathInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="pathInfoArray"/>.
			/// </param>
			/// <param name="pathInfoArray">
			/// Pointer to a variable that contains an array of <see cref="Paths"/> elements. Each element in <paramref name="pathInfoArray"/> describes a single path from a source to a target.
			/// The source and target mode information indexes are only valid in combination with the <paramref name="modeInfoArray"/> tables that are returned for the API at the same time.
			/// This parameter cannot be null.
			/// The <paramref name="pathInfoArray"/> is always returned in path priority order.
			/// </param>
			/// <param name="modeInfoArrayElementCount">
			/// Pointer to a variable that specifies the number in element of the mode information table. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="modeInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="modeInfoArray"/>. 
			/// </param>
			/// <param name="modeInfoArray">Pointer to a variable that contains an array of <see cref="ModeInfo"/> elements. This parameter cannot be null.</param>
			/// <param name="reserved">The currentTopologyId parameter is only set when the <paramref name="request"/> parameter is set to <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>; must be null.</param>
			/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
			/// <see cref="ErrorCode.InvalidParameter"/>,
			/// <see cref="ErrorCode.NotSupported"/>,
			/// <see cref="ErrorCode.AccessDenied"/>,
			/// <see cref="ErrorCode.GenFailure"/> or
			/// <see cref="ErrorCode.InsufficientBuffer"/>.
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode QueryDisplayConfig(
				[In] QueryDisplayConfigRequest request,
				[In, Out] ref int pathInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 )] PathInfo[] pathInfoArray,
				[In, Out] ref int modeInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 3 )] ModeInfo[] modeInfoArray,
				[In] IntPtr reserved
			);


			/// <summary>Retrieves information about all possible display paths for all display devices, or views, in the current setting.</summary>
			/// <param name="request">The type of information to retrieve; must be or specify <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>.</param>
			/// <param name="pathInfoArrayElementCount">
			/// Pointer to a variable that contains the number of elements in <paramref name="pathInfoArray"/>. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="pathInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="pathInfoArray"/>.
			/// </param>
			/// <param name="pathInfoArray">
			/// Pointer to a variable that contains an array of <see cref="Paths"/> elements. Each element in <paramref name="pathInfoArray"/> describes a single path from a source to a target.
			/// The source and target mode information indexes are only valid in combination with the <paramref name="modeInfoArray"/> tables that are returned for the API at the same time.
			/// This parameter cannot be null.
			/// The <paramref name="pathInfoArray"/> is always returned in path priority order.
			/// </param>
			/// <param name="modeInfoArrayElementCount">
			/// Pointer to a variable that specifies the number in element of the mode information table. This parameter cannot be null.
			/// If QueryDisplayConfig returns <see cref="ErrorCode.None"/>, <paramref name="modeInfoArrayElementCount"/> is updated with the number of valid entries in <paramref name="modeInfoArray"/>. 
			/// </param>
			/// <param name="modeInfoArray">Pointer to a variable that contains an array of <see cref="DisplayModes"/> elements. This parameter cannot be null.</param>
			/// <param name="currentTopologyId">Pointer to a variable that receives the identifier of the currently active topology in the CCD database.
			/// For a list of possible values, see the <see cref="TopologyIndicators"/> enumerated type.
			/// The currentTopologyId parameter is only set when the <paramref name="request"/> parameter value is QDC_DATABASE_CURRENT.
			/// If the <paramref name="request"/> parameter value is set to QDC_DATABASE_CURRENT, the currentTopologyId parameter must not be null.
			/// If the <paramref name="request"/> parameter value is not set to QDC_DATABASE_CURRENT, the currentTopologyId parameter value must be null.
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
			internal static extern ErrorCode QueryDisplayConfig(
				[In] QueryDisplayConfigRequest request,
				[In, Out] ref int pathInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 )] PathInfo[] pathInfoArray,
				[In, Out] ref int modeInfoArrayElementCount,
				[In, Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 3 )] ModeInfo[] modeInfoArray,
				[Out] out TopologyIndicators currentTopologyId
			);


			#endregion QueryDisplayConfig


			#region DisplayConfigGetDeviceInfo

			// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553903%28v=vs.85%29.aspx
			//LONG DisplayConfigGetDeviceInfo(
			//	_Inout_ DISPLAYCONFIG_DEVICE_INFO_HEADER* requestPacket);
			// NOTE - sadly, we can't rely on DeviceInformation: it causes a FatalEngineExecutionException.


			/// <summary>Retrieves display configuration information about the device.</summary>
			/// <param name="adapterInformation">An initialized <see cref="AdapterDevicePath"/>.
			/// <para>This object contains information about the request, which includes the packet type in the type member.
			/// The type and size of additional data that the function returns after the header structure depend on the packet type.
			/// </para>
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
			[SuppressMessage( "Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "AdapterDevicePath.DevicePath" )]
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode DisplayConfigGetDeviceInfo(
				[In, Out] AdapterDevicePath adapterInformation
			);

			/// <summary>Retrieves display configuration information about the device.</summary>
			/// <param name="sourceDeviceInformation">An initialized <see cref="SourceDeviceName"/>.
			/// <para>This object contains information about the request, which includes the packet type in the type member.
			/// The type and size of additional data that the function returns after the header structure depend on the packet type.
			/// </para>
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
			[SuppressMessage( "Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "SourceDeviceName.GDIDeviceName" )]
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode DisplayConfigGetDeviceInfo(
				[In, Out] SourceDeviceName sourceDeviceInformation
			);

			/// <summary>Retrieves display configuration information about the device.</summary>
			/// <param name="targetDeviceInformation">An initialized <see cref="TargetDeviceDescription"/>.
			/// <para>This object contains information about the request, which includes the packet type in the type member.
			/// The type and size of additional data that the function returns after the header structure depend on the packet type.
			/// </para>
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
			[SuppressMessage( "Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "TargetDeviceDescription.monitorFriendlyDeviceName" )]
			[SuppressMessage( "Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "TargetDeviceDescription.monitorDevicePath" )]
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern ErrorCode DisplayConfigGetDeviceInfo(
				[In, Out] TargetDeviceDescription targetDeviceInformation
			);


			///// <summary>Retrieves display configuration information about the device.</summary>
			///// <param name="requestPacket">Contains information about the request, which includes the packet type in the type member.
			///// <para>The type and size of additional data that the function returns after the header structure depend on the packet type.</para>
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
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//internal static extern ErrorCode DisplayConfigGetDeviceInfo(
			//	[In, Out] TargetPreferredModeInformation requestPacket
			//);


			///// <summary></summary>
			///// <param name="requestPacket"></param>
			///// <returns></returns>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//internal static extern ErrorCode DisplayConfigGetDeviceInfo(
			//	[In, Out] AdvancedColorInformation requestPacket
			//);

			///// <summary></summary>
			///// <param name="requestPacket"></param>
			///// <returns></returns>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//internal static extern ErrorCode DisplayConfigGetDeviceInfo(
			//	[In, Out] VirtualResolutionSupportInformation requestPacket
			//);

			#endregion DisplayConfigGetDeviceInfo

		}



		/// <summary>Retrieves information about all possible display paths for all display devices, or views, in the current setting.</summary>
		/// <param name="request">The type of information to retrieve; must not be <see cref="QueryDisplayConfigRequest.None"/>.</param>
		/// <param name="pathInfoArray">Receives an array of <see cref="Paths"/> elements; can't be null.
		/// <para>
		/// Each element in <paramref name="pathInfoArray"/> describes a single path from a source to a target.
		/// The source and target mode information indexes are only valid in combination with the <paramref name="modeInfoArray"/> tables that are returned for the API at the same time.
		/// The <paramref name="pathInfoArray"/> is always returned in path priority order.
		/// </para>
		/// </param>
		/// <param name="modeInfoArray">Receives an array of <see cref="DisplayModes"/> elements; can't be null.</param>
		/// <param name="currentTopologyId">When <paramref name="request"/> is or specifies <see cref="QueryDisplayConfigRequest.DatabaseCurrent"/>, receives the identifier of the currently active topology in the CCD database.</param>
		/// <returns>Returns <see cref="ErrorCode.None"/> on success, otherwise returns one of the following <see cref="ErrorCode"/>:
		/// <see cref="ErrorCode.InvalidParameter"/>,
		/// <see cref="ErrorCode.NotSupported"/>,
		/// <see cref="ErrorCode.AccessDenied"/>,
		/// <see cref="ErrorCode.GenFailure"/> or
		/// <see cref="ErrorCode.InsufficientBuffer"/>.
		/// </returns>
		private static ErrorCode QueryDisplayConfig( QueryDisplayConfigRequest request, out PathInfo[] pathInfoArray, out ModeInfo[] modeInfoArray, out TopologyIndicators currentTopologyId )
		{
			currentTopologyId = TopologyIndicators.Unspecified;

			var errorCode = SafeNativeMethods.GetDisplayConfigBufferSizes( request, out int pathInfoArrayElementCount, out int modeInfoArrayElementCount );
			if( errorCode != ErrorCode.None )
			{
				pathInfoArray = new PathInfo[ 0 ];
				modeInfoArray = new ModeInfo[ 0 ];
				return errorCode;
			}

			pathInfoArray = new PathInfo[ pathInfoArrayElementCount ];
			modeInfoArray = new ModeInfo[ modeInfoArrayElementCount ];

			if( request.HasFlag( QueryDisplayConfigRequest.DatabaseCurrent ) )
				return SafeNativeMethods.QueryDisplayConfig( request, ref pathInfoArrayElementCount, pathInfoArray, ref modeInfoArrayElementCount, modeInfoArray, out currentTopologyId );

			return SafeNativeMethods.QueryDisplayConfig( request, ref pathInfoArrayElementCount, pathInfoArray, ref modeInfoArrayElementCount, modeInfoArray, IntPtr.Zero );
		}


		/// <summary>Returns a <see cref="DisplayConfigException"/> for a given error code.</summary>
		/// <param name="errorCode">An error code (HRESULT).</param>
		/// <returns>Returns an exception, or null if <paramref name="errorCode"/> is <see cref="ErrorCode.None"/>.</returns>
		private static DisplayConfigException GetException( ErrorCode errorCode )
		{
			if( errorCode == ErrorCode.None )
				return null;
			
			switch( errorCode )
			{
				case ErrorCode.AccessDenied:
					return new DisplayConfigException( "Access denied." );

				case ErrorCode.NotSupported:
					return new DisplayConfigException( "The function is only supported on a system with a WDDM driver running." );

				case ErrorCode.GenFailure:
					return new DisplayConfigException( "A device attached to the system is not functioning." );

				case ErrorCode.InsufficientBuffer:
					return new DisplayConfigException( "The supplied path and mode buffers are too small." );

				case ErrorCode.InvalidParameter:
					return new DisplayConfigException( "Invalid combination of parameters and flags." );

				default:
					var ex = Marshal.GetExceptionForHR( (int)errorCode );
					if( ex == null )
						ex = new Win32Exception( (int)errorCode );
					return new DisplayConfigException( ex.Message, ex );
			}
		}


		/// <summary>Returns the GDI device name of a source device.</summary>
		/// <param name="sourceInfo">A valid <see cref="PathSourceInfo"/> structure; must not be empty.</param>
		/// <returns>Returns the GDI device name of a source device.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="DisplayConfigException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "GDI" )]
		public static string GetSourceGDIDeviceName( PathSourceInfo sourceInfo )
		{
			if( sourceInfo == PathSourceInfo.Empty )
				throw new ArgumentException( "Invalid source info: empty.", "sourceInfo" );

			var deviceName = new SourceDeviceName( sourceInfo.Identifier );
			var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( deviceName );
			if( errorCode == ErrorCode.None )
				return deviceName.GDIDeviceName.TrimEnd( '\0' );

			throw GetException( errorCode );
		}

		/// <summary>Returns a <see cref="TargetDeviceDescription"/> structure containing information about the specified display target.</summary>
		/// <param name="targetInfo">A valid <see cref="PathTargetInfo"/> structure; must not be empty.</param>
		/// <returns>Returns a <see cref="TargetDeviceDescription"/> structure containing information about the specified display target.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="DisplayConfigException"/>
		public static TargetDeviceDescription GetTargetDeviceDescription( PathTargetInfo targetInfo )
		{
			if( targetInfo == PathTargetInfo.Empty )
				throw new ArgumentException( "Invalid target info: empty.", "targetInfo" );

			var deviceName = new TargetDeviceDescription( targetInfo.Identifier );
			var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( deviceName );
			if( errorCode == ErrorCode.None )
				return deviceName;

			throw GetException( errorCode );
		}


		private static string GetAdapterDevicePath( DisplayDeviceId displayDeviceId )
		{
			var adapterName = new AdapterDevicePath( displayDeviceId );
			var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( adapterName );
			if( errorCode == ErrorCode.None )
				return adapterName.DevicePath.TrimEnd( '\0' );
			
			throw GetException( errorCode );
		}

		/// <summary>Returns the device path of an adapter given its source info.</summary>
		/// <param name="sourceInfo">A valid <see cref="PathSourceInfo"/> structure.</param>
		/// <returns>Returns the adapter device path.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="DisplayConfigException"/>
		public static string GetAdapterDevicePath( PathSourceInfo sourceInfo )
		{
			if( sourceInfo == PathSourceInfo.Empty )
				throw new ArgumentException( "Invalid source info.", "sourceInfo" );

			return GetAdapterDevicePath( sourceInfo.Identifier );
		}

		/// <summary>Returns the device path of an adapter given its target info.</summary>
		/// <param name="targetInfo">A valid <see cref="PathTargetInfo"/> structure.</param>
		/// <returns>Returns the adapter device path.</returns>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="DisplayConfigException"/>
		public static string GetAdapterDevicePath( PathTargetInfo targetInfo )
		{
			if( targetInfo == PathTargetInfo.Empty )
				throw new ArgumentException( "Invalid target info.", "targetInfo" );

			return GetAdapterDevicePath( targetInfo.Identifier );
		}


		//internal static TargetPreferredModeInformation GetPreferredModeInfo( PathTargetInfo targetInfo )
		//{
		//	var info = new TargetPreferredModeInformation( targetInfo.AdapterId, targetInfo.Id );
		//	var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( info );
		//	if( errorCode == ErrorCode.None )
		//		return info;

		//	throw GetException( errorCode );
		//}

		
		///// <summary></summary>
		///// <param name="targetInfo"></param>
		///// <returns></returns>
		//public static AdvancedColorInformation GetAdvancedColorInformation( PathTargetInfo targetInfo )
		//{
		//	var info = new AdvancedColorInformation( targetInfo.AdapterId, targetInfo.Id );
		//	var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( info );
		//	if( errorCode != ErrorCode.None )
		//		throw GetException( errorCode );
		//	return info;
		//}


		///// <summary>
		///// <para>Requires Windows 10 or newer.</para>
		///// </summary>
		///// <param name="targetInfo"></param>
		///// <returns></returns>
		//public static VirtualResolutionSupportInformation GetVirtualResolutionSupportInformation( PathTargetInfo targetInfo )
		//{
		//	var info = new VirtualResolutionSupportInformation( targetInfo.AdapterId, targetInfo.Id );
		//	var errorCode = SafeNativeMethods.DisplayConfigGetDeviceInfo( info );
		//	if( errorCode != ErrorCode.None )
		//		throw GetException( errorCode );
		//	return info;
		//}



		/// <summary>Queries and returns a display configuration.</summary>
		/// <param name="request">The request (flags?); must not be <see cref="QueryDisplayConfigRequest.None"/>.</param>
		/// <returns>Returns a <see cref="DisplayConfiguration"/> object containing the requested information.</returns>
		/// <exception cref="PlatformNotSupportedException"/>
		/// <exception cref="InvalidEnumArgumentException"/>
		/// <exception cref="DisplayConfigException"/>
		[SuppressMessage( "Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DisplayConfig" )]
		public static DisplayConfiguration Query( QueryDisplayConfigRequest request )
		{
			if( request == QueryDisplayConfigRequest.None )
				throw new InvalidEnumArgumentException( "request", (int)request, typeof( QueryDisplayConfigRequest ) );

			// TODO - store the DisplayConfiguration in a Dictionary<QueryDisplayConfigRequest, DisplayConfiguration> ?
			try
			{
				return new DisplayConfiguration( request );
			}
			catch( DisplayConfigException )
			{
				throw;
			}
		}



		private readonly QueryDisplayConfigRequest request;
		private PathInfo[] paths;
		private ModeInfo[] modes;
		private TopologyIndicators topologyId;



		/// <summary>Instantiates a new <see cref="DisplayConfiguration"/>.</summary>
		/// <param name="request">A value of the <see cref="QueryDisplayConfigRequest"/> enumeration; must not be <see cref="QueryDisplayConfigRequest.None"/>.</param>
		/// <exception cref="DisplayConfigException"/>
		private DisplayConfiguration( QueryDisplayConfigRequest request )
		{
			this.request = request;

			try
			{
				this.Refresh();
			}
			catch( Exception ex )
			{
				throw new DisplayConfigException( "Failed to instantiate display configuration.", ex );
			}
		}



		/// <summary>Gets the request associated with this <see cref="DisplayConfiguration"/>.
		/// <para>This is the request passed to the constructor (via the <see cref="Query"/> method) and used to <see cref="Refresh"/> the configuration.</para>
		/// </summary>
		public QueryDisplayConfigRequest Request => request;


		/// <summary>Refreshes the <see cref="DisplayConfiguration"/>.</summary>
		/// <exception cref="DisplayConfigException"/>
		public void Refresh()
		{
			var errorCode = QueryDisplayConfig( request, out paths, out modes, out topologyId );

			if( errorCode != ErrorCode.None )
				throw GetException( errorCode );
		}


		/// <summary>Gets a read-only collection containing information about all display paths for this configuration.</summary>
		public ReadOnlyCollection<PathInfo> Paths => new ReadOnlyCollection<PathInfo>( paths );


		/// <summary>Gets a read-only collection containing information about supported display modes for this configuration.</summary>
		public ReadOnlyCollection<ModeInfo> DisplayModes => new ReadOnlyCollection<ModeInfo>( modes );


		/// <summary>Gets the type of display topology.</summary>
		public TopologyIndicators Topology => topologyId;

	}

}