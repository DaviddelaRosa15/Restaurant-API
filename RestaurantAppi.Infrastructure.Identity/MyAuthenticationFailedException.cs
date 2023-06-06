using System;
using System.Runtime.Serialization;

namespace RestaurantAppi.Infrastructure.Identity
{
	[Serializable]
	public class MyAuthenticationFailedException : Exception
	{
		public MyAuthenticationFailedException()
		{
		}

		public MyAuthenticationFailedException(string message) : base(message)
		{
		}

		public MyAuthenticationFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MyAuthenticationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}