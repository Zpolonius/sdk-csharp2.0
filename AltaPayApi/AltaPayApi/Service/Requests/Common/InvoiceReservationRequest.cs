using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class InvoiceReservationRequest : BasePaymentRequestRequest
	{

		// required
		public CustomerInfo CustomerInfo { get; set; }

		// Optional parameters
		public AuthType? AuthType { get; set; }
		public string AccountNumber { get; set; }
		public string BankCode { get; set; }
		public new FraudService? FraudService { get; set; }
		public PaymentSource? PaymentSource { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set; }
		public string OrganisationNumber { get; set; } 
		public string PersonalIdentifyNumber { get; set; } 
		public string BirthDate { get; set; } // YYYY-MM-DD

		public InvoiceReservationRequest()
		{
			AuthType = null;
			FraudService = null;
			PaymentSource = null;
			CustomerInfo = new CustomerInfo();
			OrderLines = new List<PaymentOrderLine>();
		}
	}

}
