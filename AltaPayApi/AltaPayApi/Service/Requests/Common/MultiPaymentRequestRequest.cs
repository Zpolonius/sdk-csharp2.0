using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class MultiPaymentRequestRequest : BasePaymentRequestRequest
	{
		public MultiPaymentRequestRequest()
		{
			this.Children = new List<MultiPaymentRequestRequestChild>();
		}

		// Required Parameters
		public IList<MultiPaymentRequestRequestChild> Children { get; private set; }

		public MultiPaymentRequestRequest AddChild(MultiPaymentRequestRequestChild child)
		{
			this.Children.Add(child);
			return this;
		}
	}
}

