using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class ReleaseResult : PaymentResult
	{
		public ReleaseResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

