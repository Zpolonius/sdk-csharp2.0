using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class ReserveRequest
	{
		public PaymentSource Source { get; set; }
		public string Terminal { get; set; }
		public string ShopOrderId {get; set;}
		public Amount Amount { get; set; }
		public AuthType PaymentType { get; set; }

		public string CreditCardToken { get; set;}		// Option1 : Creditcard Token

		public string Pan { get; set;} 					// Option2 : Pan, Month, Year
		public int? ExpiryMonth {get; set; }
		public int? ExpiryYear { get; set; }

		public string Cvc {get; set; }

		public CustomerInfo CustomerInfo { get; set; }
		/// <summary>
		/// This will be changed to DateTime at some point.
		/// </summary>
		public string CustomerCreatedDate { get; set; }
		
		public FraudService FraudService { get; set; }

		public IList<PaymentOrderLine> OrderLines { get; set;}

		public ReserveRequest() 
		{
			Source = PaymentSource.moto;
			CustomerInfo = new CustomerInfo();
			OrderLines = new List<PaymentOrderLine>();
		}
	}
}

