using System;

namespace AltaPay.Service
{
	public class ChargeSubscriptionRequest
	{
	    [System.Obsolete("SubscriptionId is deprecated, please use AgreementId instead.")]
		public string SubscriptionId { get; set; }
		public string AgreementId { get; set; }
		public AgreementUnscheduledType? AgreementUnscheduledType { get; set; }
		public Amount Amount { get; set; }
	}
}

