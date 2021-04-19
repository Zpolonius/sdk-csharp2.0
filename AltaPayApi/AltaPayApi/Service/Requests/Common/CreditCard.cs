using System;

namespace AltaPay.Service
{
	public class CreditCard
	{
		public string Token { get; set; }
		public string CardNumber { get; set; }
		public string ExpiryMonth { get; set; }
		public string ExpiryYear { get; set; }
		public string Cvc { get; set; }
	}
}

