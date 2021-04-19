using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class ChargeSubscriptionResult : SubscriptionResult
	{
		public ChargeSubscriptionResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

