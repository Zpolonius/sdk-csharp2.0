using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CardWalletAuthorizeResult : PaymentResult
	{
		public CardWalletAuthorizeResult(APIResponse apiResponse) : base(apiResponse)
		{
		}
	}
}

