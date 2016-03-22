using System;
using System.Runtime.Serialization;


namespace ManagedX.Display.DisplayConfig
{

	/// <summary>An exception to be thrown on DisplayConfig error.</summary>
	[Serializable]
	public class DisplayConfigException : Exception
	{

		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		public DisplayConfigException()
			: base()
		{
		}


		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		/// <param name="message"></param>
		public DisplayConfigException( string message )
			: base( message )
		{
		}


		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		public DisplayConfigException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected DisplayConfigException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}
