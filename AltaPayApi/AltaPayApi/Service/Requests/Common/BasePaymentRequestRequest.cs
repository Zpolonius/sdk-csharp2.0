using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public abstract class BasePaymentRequestRequest
	{
		protected BasePaymentRequestRequest()
		{
			Config = new PaymentRequestConfig();
			PaymentInfos = new Dictionary<string, object>();
		}

		// required
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public Amount Amount { get; set; }

		// optional
		public string Language { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; }
		public AuthType Type { get; set; }

		public AgreementType? AgreementType { get; set; }
		public string AgreementId { get; set; }
		public AgreementUnscheduledType? AgreementUnscheduledType { get; set; }
		public string AgreementExpiry { get; set; }
		public string AgreementFrequency { get; set; }
		public string AgreementNextChargeDate { get; set; }
		public string AgreementAdminUrl { get; set; }

		public string CreditCardToken { get; set; }
		public string Cookie { get; set; }
		public PaymentRequestConfig Config { get; set; }
		public FraudService FraudService { get; set; }
	}
}

