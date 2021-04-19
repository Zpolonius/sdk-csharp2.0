using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class PaymentRequestRequest : BasePaymentRequestRequest
	{
		// Optional parameters
		public string SalesReconciliationIdentifier { get; set; }
		public string SalesInvoiceNumber { get; set; }
		public double SalesTax { get; set; }
		public CustomerInfo CustomerInfo { get; set; }
		/// <summary>
		/// This will be changed to DateTime at some point.
		/// </summary>
		public string CustomerCreatedDate { get; set; }  
		public IList<PaymentOrderLine> OrderLines { get; set;}
		public ShippingType ShippingType { get; set; }
		public string OrganisationNumber { get; set; } // If the organisation_number parameter is given the organisation number field in the invoice payment form is prepopulated, and if no other payment options is enabled on the terminal the form will auto submit.
		public AccountOffer AccountOffer { get; set; } // To require having account enabled for an invoice payment for this specific customer, set this to required. To disable account for this specific customer, set to disabled.
		public PaymentSource Source { get; set; }

		
		
		public PaymentRequestRequest()
		{
			CustomerInfo = new CustomerInfo();
			OrderLines = new List<PaymentOrderLine>();
		}
	}
}
