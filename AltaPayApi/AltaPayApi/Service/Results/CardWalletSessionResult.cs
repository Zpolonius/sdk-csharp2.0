using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CardWalletSessionResult : PaymentResult
	{
		public CardWalletSessionResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}