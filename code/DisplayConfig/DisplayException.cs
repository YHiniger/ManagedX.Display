using System;
using System.Runtime.Serialization;


namespace ManagedX.Display
{

	/// <summary>An exception to be thrown on display device error.</summary>
	[Serializable]
	public class DisplayException : Exception
	{

		/// <summary>Initializes a new <see cref="DisplayException"/>.</summary>
		public DisplayException()
			: base()
		{
		}


		/// <summary>Initializes a new <see cref="DisplayException"/>.</summary>
		/// <param name="message"></param>
		public DisplayException( string message )
			: base( message )
		{
		}


		/// <summary>Initializes a new <see cref="DisplayException"/>.</summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		public DisplayException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Initializes a new <see cref="DisplayException"/>.</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected DisplayException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}
