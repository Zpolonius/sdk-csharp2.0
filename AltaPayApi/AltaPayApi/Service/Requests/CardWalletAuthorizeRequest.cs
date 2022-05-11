using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class CardWalletAuthorizeRequest : BasePaymentRequestRequest
	{		
		public string ProviderData { get; set; }
		
		// Optional parameters
		public CustomerInfo CustomerInfo { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set; }
		public string SalesReconciliationIdentifier { get; set; }
		public string SalesInvoiceNumber { get; set; }
		public double SalesTax { get; set; }
		public ShippingType? ShippingType { get; set; }
		public string CustomerCreatedDate { get; set; }

		public CardWalletAuthorizeRequest(string providerData, string terminal, string shopOrderId, Amount amount)
		{
			this.ProviderData = providerData;
			this.Terminal = terminal;
			this.ShopOrderId = shopOrderId;
			this.Amount = amount;
			
		}		
	}
}