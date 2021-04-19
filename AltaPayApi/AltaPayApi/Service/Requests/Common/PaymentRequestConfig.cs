using System;
using System.Collections.Generic;


namespace AltaPay.Service
{
	public class PaymentRequestConfig
	{
		public string CallbackFormUrl {get;set;} // Sending this will override the "callback form" setting on the terminal	String (Url)
		public string CallbackOkUrl {get;set;} // Sending this will override the "callback ok" setting on the terminal	String (Url)
		public string CallbackFailUrl {get;set;} // Sending this will override the "callback fail" setting on the terminal	String (Url)
		public string CallbackRedirectUrl {get;set;} // Sending this will override the "callback redirect" setting on the terminal	String (Url)
		public string CallbackOpenUrl {get;set;} // Sending this will override the "callback open" setting on the terminal	String (Url)
		public string CallbackNotificationUrl {get;set;} // Sending this will override the "callback notification" setting on the terminal	String (Url)
		
		/**
		 * By settings this, a check will be made at the last possible time before taking the payment. This is
		 * used to verify that stock, discounts, etc. are still valid for the order/shopping basket. This callback 
		 * will be done in the same way as other callbacks, but you can prepend GET parameters to the URL if you 
		 * need anything in particular which is not part of the normal POST parameters. 
		 *
		 * To allow the payment you must return an HTML/TEXT response with the value "OKAY". Anything else will be
		 * assumed to be a sign that we should abort/decline the payment and will be placed as the error message.
		 * An example could be "Some of the items in the basked are out of stock". 
		 *
		 * If your server responds with any other http response code than 200, the payment will fail with an error. 
		 *
		 * To ensure consistance we will strip HTML/XML tags, and we will only allow the first 255 characters to 
		 * become the error message when the callback returns something different than "OKAY"
		 */
		public string CallbackVerifyOrderUrl {get;set;}
		
		public Dictionary<string,Object> ToDictionary()
		{
			Dictionary<string,Object> configParams = new Dictionary<string,Object>();
			configParams.Add("callback_form", CallbackFormUrl);
			configParams.Add("callback_ok", CallbackOkUrl);
			configParams.Add("callback_fail", CallbackFailUrl);
			configParams.Add("callback_redirect", CallbackRedirectUrl);
			configParams.Add("callback_open", CallbackOpenUrl);
			configParams.Add("callback_notification", CallbackNotificationUrl);
			configParams.Add("callback_verify_order", CallbackVerifyOrderUrl);
			
			return configParams;
		}
	}
}

