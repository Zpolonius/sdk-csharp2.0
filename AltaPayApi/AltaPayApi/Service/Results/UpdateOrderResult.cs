using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class UpdateOrderResult : PaymentResult
	{
		public UpdateOrderResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

