using System;
using NUnit.Framework;
using AltaPay.Api.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace AltaPay.Service.Tests.Unit
{
	public class MerchantApi_ParseMultiPaymentPostBackXmlResponseTests : BaseTest
	{
		[Test]
		public void ParseMultiPaymentPostBackXmlResponse_Success()
		{
			string xmlResponse = @"<?xml version=""1.0""?>
<APIResponse version=""20130430""><Header><Date>2014-09-17T14:30:47+02:00</Date><Path>/</Path><ErrorCode>0</ErrorCode><ErrorMessage></ErrorMessage></Header><Body><Result>Success</Result><Actions><Action><Result>Success</Result><Transactions><Transaction><TransactionId>5</TransactionId><AuthType>payment</AuthType><CardStatus>InvalidLuhn</CardStatus><CreditCardExpiry><Year>2014</Year><Month>01</Month></CreditCardExpiry><CreditCardToken>99d390d29deb2c09e0879cd0f5dca5dd5e659d8d</CreditCardToken><CreditCardMaskedPan>457122******9876</CreditCardMaskedPan><ThreeDSecureResult>Not_Attempted</ThreeDSecureResult><CVVCheckResult>Not_Attempted</CVVCheckResult><BlacklistToken>ba44475915a225618c250fccfc81f0403e713167</BlacklistToken><ShopOrderId>multi-payment-request-335e58a5-5139-4604-8e47-8fc68c8bd367</ShopOrderId><Shop>AltaPay Shop Integration</Shop><Terminal>AltaPay Soap Test Terminal</Terminal><TransactionStatus>preauth</TransactionStatus><MerchantCurrency>978</MerchantCurrency><CardHolderCurrency>978</CardHolderCurrency><ReservedAmount>12.34</ReservedAmount><CapturedAmount>0.00</CapturedAmount><RefundedAmount>0.00</RefundedAmount><RecurringDefaultAmount>0.00</RecurringDefaultAmount><CreatedDate>2014-09-17 14:30:46</CreatedDate><UpdatedDate>2014-09-17 14:30:46</UpdatedDate><PaymentNature>CreditCard</PaymentNature><PaymentSchemeName>Visa</PaymentSchemeName><PaymentNatureService name=""SoapTestAcquirer""><SupportsRefunds>true</SupportsRefunds><SupportsRelease>true</SupportsRelease><SupportsMultipleCaptures>true</SupportsMultipleCaptures><SupportsMultipleRefunds>true</SupportsMultipleRefunds></PaymentNatureService><FraudRiskScore>12</FraudRiskScore><FraudExplanation>For the test fraud service the risk score is always equal mod 101 of the created amount for the payment</FraudExplanation><FraudRecommendation>Deny</FraudRecommendation><ChargebackEvents/><PaymentInfos/><CustomerInfo/><ReconciliationIdentifiers/></Transaction></Transactions></Action><Action><Result>Success</Result><Transactions><Transaction><TransactionId>6</TransactionId><AuthType>payment</AuthType><CardStatus>InvalidLuhn</CardStatus><CreditCardExpiry><Year>2014</Year><Month>01</Month></CreditCardExpiry><CreditCardToken>99d390d29deb2c09e0879cd0f5dca5dd5e659d8d</CreditCardToken><CreditCardMaskedPan>457122******9876</CreditCardMaskedPan><ThreeDSecureResult>Not_Attempted</ThreeDSecureResult><CVVCheckResult>Not_Attempted</CVVCheckResult><BlacklistToken>ba44475915a225618c250fccfc81f0403e713167</BlacklistToken><ShopOrderId>multi-payment-request-335e58a5-5139-4604-8e47-8fc68c8bd367</ShopOrderId><Shop>AltaPay Shop Integration</Shop><Terminal>AltaPay Soap Test Terminal</Terminal><TransactionStatus>preauth</TransactionStatus><MerchantCurrency>978</MerchantCurrency><CardHolderCurrency>978</CardHolderCurrency><ReservedAmount>98.76</ReservedAmount><CapturedAmount>0.00</CapturedAmount><RefundedAmount>0.00</RefundedAmount><RecurringDefaultAmount>0.00</RecurringDefaultAmount><CreatedDate>2014-09-17 14:30:46</CreatedDate><UpdatedDate>2014-09-17 14:30:47</UpdatedDate><PaymentNature>CreditCard</PaymentNature><PaymentSchemeName>Visa</PaymentSchemeName><PaymentNatureService name=""SoapTestAcquirer""><SupportsRefunds>true</SupportsRefunds><SupportsRelease>true</SupportsRelease><SupportsMultipleCaptures>true</SupportsMultipleCaptures><SupportsMultipleRefunds>true</SupportsMultipleRefunds></PaymentNatureService><FraudRiskScore>98</FraudRiskScore><FraudExplanation>For the test fraud service the risk score is always equal mod 101 of the created amount for the payment</FraudExplanation><FraudRecommendation>Deny</FraudRecommendation><ChargebackEvents/><PaymentInfos/><CustomerInfo/><ReconciliationIdentifiers/></Transaction></Transactions></Action></Actions></Body></APIResponse>";
			
			var merchantApi = new MerchantApi("url", "username", "password");
			MultiPaymentApiResult result = merchantApi.ParseMultiPaymentPostBackXmlResponse(xmlResponse);

            Assert.AreEqual(false, result.HasAnyFailedPaymentActions());
            Assert.AreEqual(2, result.PaymentActions.Count);
			
			Assert.AreEqual(Result.Success, result.PaymentActions[0].Result);
			Assert.AreEqual(12.34m, result.PaymentActions[0].Payment.ReservedAmount);
			
			Assert.AreEqual(Result.Success, result.PaymentActions[1].Result);
			Assert.AreEqual(98.76m, result.PaymentActions[1].Payment.ReservedAmount);
		}
	}
}

