using System;

namespace AltaPay.Service
{
	public class ChargeSubscriptionRequest
	{
		public string SubscriptionId { get; set; }
		public Amount Amount { get; set; }
	}
}

