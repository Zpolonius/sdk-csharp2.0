using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class MultiPaymentRequestRequestChild
	{
		private AuthType type;
		private bool typeIsSet = false;

		private ShippingType shippingType;
		private bool shippingTypeIsSet = false;

		// Required
		public Amount Amount { get; set; }

		// Optional
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; }
		public AuthType Type
		{
			get { return type; }
			set
			{
				type = value;
				typeIsSet = true;
			}
		}
		public ShippingType ShippingType
		{
			get { return shippingType; }

			set
			{
				shippingType = value;
				shippingTypeIsSet = true;
			}
		}
		public string SalesReconciliationIdentifier { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set;}

		internal bool TypeIsSet
		{
			get { return typeIsSet; }
		}

		internal bool ShippingTypeIsSet
		{
			get { return shippingTypeIsSet; }
		}
	}
}
