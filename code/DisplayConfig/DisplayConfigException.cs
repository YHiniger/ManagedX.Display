using System;
using System.Runtime.Serialization;


namespace ManagedX.Graphics.DisplayConfig
{

	/// <summary>An exception to be thrown on DisplayConfig error.</summary>
	[Serializable]
	public class DisplayConfigException : ManagedXException
	{

		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		public DisplayConfigException()
			: base()
		{
		}


		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		/// <param name="message">A message describing the error.</param>
		public DisplayConfigException( string message )
			: base( message )
		{
		}


		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		/// <param name="message">A message describing the error.</param>
		/// <param name="innerException">The exception which caused this exception, or null.</param>
		public DisplayConfigException( string message, Exception innerException )
			: base( message, innerException )
		{
		}


		/// <summary>Initializes a new <see cref="DisplayConfigException"/>.</summary>
		/// <param name="info">Serialization data about the exception being thrown.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="SerializationException"/>
		protected DisplayConfigException( SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}

	}

}