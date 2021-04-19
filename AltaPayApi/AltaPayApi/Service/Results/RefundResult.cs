using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class RefundResult : PaymentResult
	{
		public RefundResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

