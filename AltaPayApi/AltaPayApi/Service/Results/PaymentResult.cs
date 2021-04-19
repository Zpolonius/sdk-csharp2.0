using System.Runtime.InteropServices;
using AltaPay.Service.Dto;
using System;

namespace AltaPay.Service
{
    public abstract class PaymentResult
		: ApiResult
    {
		public Transaction Payment { get; set; }

		public PaymentResult(APIResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;
				ResultMessageMustBeShown = apiResponse.Body.CardHolderMessageMustBeShown;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

				Payment = (apiResponse.Body.Transactions != null && apiResponse.Body.Transactions.Length > 0 ? apiResponse.Body.Transactions[0] : null);
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
