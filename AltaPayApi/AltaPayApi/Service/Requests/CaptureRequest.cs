using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class CaptureRequest
	{
		public string PaymentId { get; set; }
		public string ReconciliationId { get; set; }
		public string InvoiceNumber { get; set; }
		public double? SalesTax { get; set; }
		public Amount Amount { get; set; }

		public IList<PaymentOrderLine> OrderLines { get; set; } 

		public CaptureRequest() 
		{
			OrderLines = new List<PaymentOrderLine>();
		}
	}
}

