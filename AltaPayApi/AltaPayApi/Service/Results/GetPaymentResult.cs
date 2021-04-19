using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class GetPaymentResult : PaymentResult
	{
		public GetPaymentResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

