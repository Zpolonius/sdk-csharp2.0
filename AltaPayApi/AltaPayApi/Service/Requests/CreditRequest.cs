using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class CreditRequest
	{
		public CreditRequest()
		{
			PaymentInfos = new Dictionary<string, object>();
		}

		public string Terminal { get; set; }
		public string ShopOrderId {get; set;}
		public Amount Amount { get; set; }
		
		// First option
		public string Pan { get; set;}	
		public int ? ExpiryMonth {get; set; }
		public int ? ExpiryYear { get; set; }
		
		//Second option
		public string CreditCardToken { get; set;}

		// Optional parameters
		public string Cvc {get; set; }
		public PaymentSource? PaymentSource { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; }

		//3D Secure parameter, optional
		public string CardHolderName { get; set;}
	}
}

