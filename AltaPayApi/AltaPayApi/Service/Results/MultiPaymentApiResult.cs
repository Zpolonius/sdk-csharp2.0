using System;
using AltaPay.Service.Dto;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class MultiPaymentApiResult
	{
		public MultiPaymentApiResult(APIResponse apiResponse)
		{
			PaymentActions = new List<PaymentAction>();

			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
				{
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result, true);
				}

				if (apiResponse.Body.Actions != null)
				{
					foreach (AltaPay.Service.Dto.Action action in apiResponse.Body.Actions)
					{
						PaymentActions.Add(PaymentAction.FromAction(action));
					}
				}
			}
			else
			{
				Result = Result.SystemError;
				ResultMessage = "An error occurred";
			}
		}

		public bool HasAnyFailedPaymentActions()
		{
			foreach (var paymentAction in PaymentActions)
			{
				if (paymentAction.Result != Result.Success)
				{
					return true;
				}
			}

			return false;
		}

		public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public IList<PaymentAction> PaymentActions { get; protected set; }
	}
}
