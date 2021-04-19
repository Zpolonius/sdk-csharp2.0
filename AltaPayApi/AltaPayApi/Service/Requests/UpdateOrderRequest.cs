using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class UpdateOrderRequest
	{
		private readonly string _paymentId;
		private readonly IList<PaymentOrderLine> _orderLines;
		public string PaymentId { get { return _paymentId; }  }
		public IList<PaymentOrderLine> OrderLines { get { return _orderLines; } }

		public UpdateOrderRequest(string paymentId, IList<PaymentOrderLine> orderLines)
		{
		
			if (orderLines.Count != 2)
			{
				throw new ArgumentException("orderLines must contain exactly two elements");
			}

			this._paymentId = paymentId;
			this._orderLines = orderLines;
		
		}
	}
}

