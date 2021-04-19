using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class PaymentAction
	{
		public static PaymentAction FromAction(AltaPay.Service.Dto.Action action)
		{
			var paymentAction = new PaymentAction();

			paymentAction.Result = (Result)Enum.Parse(typeof(Result), action.Result, true);

			if (!String.IsNullOrEmpty(action.CardHolderErrorMessage))
			{
				paymentAction.CardHolderErrorMessage = action.CardHolderErrorMessage;
			}

			if (!String.IsNullOrEmpty(action.MerchantErrorMessage))
			{
				paymentAction.MerchantErrorMessage = action.MerchantErrorMessage;
			}

			if (action.Transactions != null && action.Transactions.Length > 0)
			{
				paymentAction.Payment = action.Transactions[0];
			}
		
			return paymentAction;
		}

		public Result Result { get; set; }
		public Transaction Payment { get; set; }
		public string CardHolderErrorMessage { get; set; }
		public string MerchantErrorMessage { get; set; }
	}
}
	