using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class MultiPaymentRequestResult : ApiResult
	{
		public MultiPaymentRequestResult(APIResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
				{
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result, true);
				}

				Url = apiResponse.Body.Url;
			}
			else
			{
				Result = Result.SystemError;
				ResultMerchantMessage = apiResponse.Header.ErrorMessage;
				ResultMessage = "An error occurred";
			}
		}

		public string Url { get; set; }
	}
}
