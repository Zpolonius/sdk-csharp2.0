using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class ReserveResult : PaymentResult
	{
        public string RedirectUrl { get; set; }

        public ReserveResult(APIResponse response) : base(response) 
		{
            if (response.Header.ErrorCode == 0)
            {
                ResultMessage = response.Body.CardHolderErrorMessage;
                ResultMerchantMessage = response.Body.MerchantErrorMessage;

                if (!String.IsNullOrEmpty(response.Body.Result))
                    Result = (Result)Enum.Parse(typeof(Result), response.Body.Result);

                if (response.Body.Result == Result.Redirect.ToString())
                {
                    RedirectUrl = response.Body.RedirectUrl;
                }
            }
            else
            {
                Result = Result.SystemError;
                ResultMerchantMessage = response.Header.ErrorMessage;
                ResultMessage = "An error occurred";
            }
        }
	}
}

