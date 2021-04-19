using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class RefundRequest
	{
		public string PaymentId { get; set; }
		public Amount Amount { get; set; }
		public string ReconciliationId { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set; } 

		public RefundRequest(){
			OrderLines = new List<PaymentOrderLine>();
		}
	}
}

