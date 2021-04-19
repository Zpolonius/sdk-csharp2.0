using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CaptureResult : PaymentResult
	{
		public CaptureResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

