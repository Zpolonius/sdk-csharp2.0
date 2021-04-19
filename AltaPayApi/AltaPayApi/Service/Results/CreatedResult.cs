using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CreatedResult : PaymentResult
	{
		public CreatedResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

