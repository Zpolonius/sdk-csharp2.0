using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class SubscriptionResult : PaymentResult
	{
		public Transaction RecurringPayment { get; set; }
        public string RedirectUrl { get; set; }

        public SubscriptionResult(APIResponse apiResponse)
			:base(apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0 && apiResponse.Body.Transactions != null && apiResponse.Body.Transactions.Length > 1)
			{
				RecurringPayment = apiResponse.Body.Transactions[1];
			}

            if (apiResponse.Header.ErrorCode == 0)
            {
                ResultMessage = apiResponse.Body.CardHolderErrorMessage;
                ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

                if (!String.IsNullOrEmpty(apiResponse.Body.Result))
                    Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

                if (apiResponse.Body.Result == Result.Redirect.ToString())
                {
                    RedirectUrl = apiResponse.Body.RedirectResponse.Url; 
                }
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
