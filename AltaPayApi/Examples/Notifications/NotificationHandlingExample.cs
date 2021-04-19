using System;
using System.Collections.Specialized;
using AltaPay.Service;
using AltaPay.Service.Dto;

namespace Examples
{
	public class NotificationHandlingExample
	{
		public void HandleNotification(NameValueCollection postParameters, IMerchantApi merchantApi)
		{
			PaymentResult paymentResult = merchantApi.ParsePostBackXmlResponse(postParameters["xml"]) as PaymentResult;
			
			//
			// the posted data contains a "status" field, which is where you can see what kind of notification this is
			//
			if (postParameters["status"].Equals("ChargebackEvent"))
			{
				//
				// something chargeback related happened
				// could be the chargeback itself, a dispute etc
				//
				foreach (ChargebackEvent cbe in paymentResult.Payment.ChargebackEvents)
				{
					// do whatever you need to do
				}
			}
			
			else
			{
				//
				// payment status changed
				//
				
				// check the amounts against what you expect them to be and act accordingly
				var reservedAmount = paymentResult.Payment.ReservedAmount;
				var capturedAmount = paymentResult.Payment.CapturedAmount;
				var refundedAmount = paymentResult.Payment.RefundedAmount;
				
				// or even better... check the ReconciliationIdentifiers, which
				// contains information on captures and refunds (nothing else)
				foreach (ReconciliationIdentifier refundOrCapture in paymentResult.Payment.ReconciliationIdentifiers)
				{
				}
			}
		}
	}
}

