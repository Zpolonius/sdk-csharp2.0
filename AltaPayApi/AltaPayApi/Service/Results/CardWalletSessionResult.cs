using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CardWalletSessionResult : ApiResult
	{
		public string ApplePaySession { get; set; }

		public CardWalletSessionResult(APIResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

				ApplePaySession = apiResponse.Body.ApplePaySession;
			}
			else
			{
				Result = Result.SystemError;
				ResultMerchantMessage = apiResponse.Header.ErrorMessage;
				ResultMessage = "An error occurred";
			}
		}
	}
}