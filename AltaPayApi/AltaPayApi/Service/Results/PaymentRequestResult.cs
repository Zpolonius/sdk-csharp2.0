using System;
using AltaPay.Service.Dto;


namespace AltaPay.Service
{
    public class PaymentRequestResult
		: ApiResult
    {
		public string Url { get; set; }
		public string DynamicJavascriptUrl { get; set; }
		public string PaymentRequestId { get; set; }
		
		public PaymentRequestResult(APIResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

				Url = apiResponse.Body.Url;
				DynamicJavascriptUrl = apiResponse.Body.DynamicJavascriptUrl;
				PaymentRequestId = apiResponse.Body.PaymentRequestId;
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
