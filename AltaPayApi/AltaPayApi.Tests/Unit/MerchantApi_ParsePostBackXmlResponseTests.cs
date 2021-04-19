using System;
using System.IO;
using NUnit.Framework;
using AltaPay.Api.Tests;
using AltaPay.Service.Loggers;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace AltaPay.Service.Tests.Unit
{
	public class MerchantApi_ParsePostBackXmlResponseTests : BaseTest
	{
        private string _baseProjectPath;
        [SetUp]
        public void SetUp()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var executingAssemblyPath = Path.GetDirectoryName(path);
            _baseProjectPath = Directory.GetParent(Directory.GetParent(executingAssemblyPath).FullName).FullName;
        }

        [Test]
		public void ParsePostBackXmlResponse_Success()
		{
			string xmlResponse = @"<?xml version=""1.0""?>
<APIResponse version=""20130430""><Header><Date>2014-11-07T14:30:48+01:00</Date><Path>/</Path><ErrorCode>0</ErrorCode><ErrorMessage></ErrorMessage></Header><Body><Result>Error</Result><MerchantErrorMessage>Invalid account number (no such number)[54321]</MerchantErrorMessage><CardHolderErrorMessage>Internal Error</CardHolderErrorMessage><Transactions><Transaction><TransactionId>391</TransactionId><AuthType>payment</AuthType><CardStatus>InvalidLuhn</CardStatus><CreditCardExpiry><Year>2016</Year><Month>10</Month></CreditCardExpiry><CreditCardToken>fe36d10bdb1ab8c1139605b18e8acece2755b254</CreditCardToken><CreditCardMaskedPan>457160******1575</CreditCardMaskedPan><ThreeDSecureResult>Not_Applicable</ThreeDSecureResult><CVVCheckResult>Not_Applicable</CVVCheckResult><BlacklistToken>50c5ec1e29b71aae26a8c309860323dbfbbf97f4</BlacklistToken><ShopOrderId>1418112</ShopOrderId><Shop>Freetrailer</Shop><Terminal>Freetrailer CC DKK</Terminal><TransactionStatus>preauth_error</TransactionStatus><MerchantCurrency>208</MerchantCurrency><CardHolderCurrency>208</CardHolderCurrency><ReservedAmount>0.00</ReservedAmount><CapturedAmount>0.00</CapturedAmount><RefundedAmount>0.00</RefundedAmount><RecurringDefaultAmount>0.00</RecurringDefaultAmount><CreatedDate>2014-11-07 14:30:45</CreatedDate><UpdatedDate>2014-11-07 14:30:47</UpdatedDate><PaymentNature>CreditCard</PaymentNature><PaymentSchemeName>Visa</PaymentSchemeName><PaymentNatureService name=""ValitorAcquirer""><SupportsRefunds>true</SupportsRefunds><SupportsRelease>true</SupportsRelease><SupportsMultipleCaptures>true</SupportsMultipleCaptures><SupportsMultipleRefunds>true</SupportsMultipleRefunds></PaymentNatureService><ChargebackEvents/><PaymentInfos/><CustomerInfo><UserAgent>Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.111 Safari/537.36</UserAgent><IpAddress>86.58.170.35</IpAddress><Email><![CDATA[fk@freetrailer.dk]]></Email><Username/><CustomerPhone>28911648</CustomerPhone><OrganisationNumber></OrganisationNumber><CountryOfOrigin><Country>DK</Country><Source>CardNumber</Source></CountryOfOrigin><BillingAddress><Firstname><![CDATA[Fie]]></Firstname><Lastname/><Address><![CDATA[sdf]]></Address><City><![CDATA[sdf]]></City><Region/><Country><![CDATA[De]]></Country><PostalCode><![CDATA[2400]]></PostalCode></BillingAddress></CustomerInfo><ReconciliationIdentifiers/></Transaction></Transactions></Body></APIResponse>";
			
			var merchantApi = new MerchantApi("url", "username", "password");
			merchantApi.ParsePostBackXmlResponse(xmlResponse);
			/*
			Assert.AreEqual(false, result.HasAnyFailedPaymentActions());
			Assert.AreEqual(2, result.PaymentActions.Count);
			
			Assert.AreEqual(Result.Success, result.PaymentActions[0].Result);
			Assert.AreEqual(12.34m, result.PaymentActions[0].Payment.ReservedAmount);
			
			Assert.AreEqual(Result.Success, result.PaymentActions[1].Result);
			Assert.AreEqual(98.76m, result.PaymentActions[1].Payment.ReservedAmount);
			*/
		}

		[Test]
		public void ParsePostBackXmlResponse_Cancelled()
		{
			string xmlResponse = @"<?xml version=""1.0""?>
<APIResponse version=""20150526""><Header><Date>2015-09-07T09:31:44+02:00</Date><Path>API/ePaymentVerify?result=CANCEL&amp;payment_id=ae5e0de7-5238-4746-be1c-f3ef2b141a3a&amp;checksum=6c4eb3b36c95438ae09b519f2f458fc9&amp;token=EC-0DT17833PH498183L</Path><ErrorCode>0</ErrorCode><ErrorMessage/></Header><Body><Result>Cancelled</Result><Transactions><Transaction><TransactionId>25</TransactionId><PaymentId>ae5e0de7-5238-4746-be1c-f3ef2b141a3a</PaymentId><AuthType>payment</AuthType><CardStatus>NoCreditCard</CardStatus><CreditCardToken/><CreditCardMaskedPan/><ThreeDSecureResult>Not_Applicable</ThreeDSecureResult><LiableForChargeback>Merchant</LiableForChargeback><CVVCheckResult>Not_Applicable</CVVCheckResult><BlacklistToken/><ShopOrderId>payment-request-906354c2-c6b8-4cd4-9d8e-d6381ff66b28</ShopOrderId><Shop>AltaPay Shop Integration</Shop><Terminal>AltaPay Paypal Test Terminal</Terminal><TransactionStatus>epayment_cancelled</TransactionStatus><MerchantCurrency>840</MerchantCurrency><MerchantCurrencyAlpha>USD</MerchantCurrencyAlpha><CardHolderCurrency>840</CardHolderCurrency><CardHolderCurrencyAlpha>USD</CardHolderCurrencyAlpha><ReservedAmount>0.00</ReservedAmount><CapturedAmount>0.00</CapturedAmount><RefundedAmount>0.00</RefundedAmount><CreditedAmount>0.00</CreditedAmount><RecurringDefaultAmount>0.00</RecurringDefaultAmount><SurchargeAmount>0.00</SurchargeAmount><CreatedDate>2015-09-07 09:31:24</CreatedDate><UpdatedDate>2015-09-07 09:31:44</UpdatedDate><PaymentNature>Wallet</PaymentNature><PaymentSchemeName>PayPal</PaymentSchemeName><PaymentNatureService name=""PaypalAcquirer""><SupportsRefunds>true</SupportsRefunds><SupportsRelease>true</SupportsRelease><SupportsMultipleCaptures>true</SupportsMultipleCaptures><SupportsMultipleRefunds>true</SupportsMultipleRefunds></PaymentNatureService><ChargebackEvents/><PaymentInfos/><CustomerInfo><UserAgent>Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36</UserAgent><IpAddress>127.0.0.1</IpAddress><Email/><Username/><CustomerPhone/><OrganisationNumber/><CountryOfOrigin><Country/><Source>NotSet</Source></CountryOfOrigin><RegisteredAddress><Firstname/><Lastname/><Address/><City/><Region/><Country/><PostalCode/></RegisteredAddress></CustomerInfo><ReconciliationIdentifiers/></Transaction></Transactions></Body></APIResponse>";

			var merchantApi = new MerchantApi("url", "username", "password");
			ApiResult actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);

			PaymentResult pr = actual as PaymentResult;

            NUnit.Framework.Assert.AreEqual(Result.Cancelled, actual.Result);
            NUnit.Framework.Assert.AreEqual("epayment_cancelled", pr.Payment.TransactionStatus);
		}
	
		
		[Test]
		[ExpectedException(typeof(Exception))]
		public void ParsePostBackXmlResponse_InvalidXml()
		{
            var logFileName = Path.GetTempPath() + "skarptests.log";
			var logger = new FileAltaPayLogger(logFileName);
			logger.LogLevel = AltaPayLogLevel.Error;
			
			string xmlResponse = @"<?xml version=""1.0""?><APIResponse version=""20130430""><NotValid>Not even a little bit</NotValid></APIResponse>";
			
			var merchantApi = new MerchantApi("url", "username", "password", logger);
			merchantApi.ParsePostBackXmlResponse(xmlResponse);
		}

		[Test]
		public void ParsePostBackXmlResponse_ErrorResponseWithoutTransactions()
		{
            var logFileName = Path.GetTempPath() + "skarptests.log";
            var logger = new FileAltaPayLogger(logFileName);
			logger.LogLevel = AltaPayLogLevel.Error;

			string xmlResponse = @"<?xml version=""1.0""?> <APIResponse version=""20141202""><Header><Date>2015-04-24T13:03:21+02:00</Date><Path>/</Path><ErrorCode>0</ErrorCode><ErrorMessage></ErrorMessage></Header><Body><Result>Error</Result></Body></APIResponse>";

			var merchantApi = new MerchantApi("url", "username", "password", logger);
			ApiResult actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);

            NUnit.Framework.Assert.AreEqual(Result.Error, actual.Result);
		}

		[Test]
		public void ParsePostBackXmlResponse_ChargebackEvent()
		{
            var logFileName = Path.GetTempPath() + "skarptests.log";
            var logger = new FileAltaPayLogger(logFileName);
			logger.LogLevel = AltaPayLogLevel.Error;

			string xmlResponse = File.ReadAllText("Unit/txt/ChargebackEvent.xml");

			var merchantApi = new MerchantApi("url", "username", "password", logger);
			ApiResult actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);

            NUnit.Framework.Assert.AreEqual(Result.ChargebackEvent, actual.Result);
		}

		[Test]
		public void ParsePostBackXmlResponse_ReadCardHolderMessageMustBeShown()
		{
            var logFileName = Path.GetTempPath() + "skarptests.log";
            var logger = new FileAltaPayLogger(logFileName);
			logger.LogLevel = AltaPayLogLevel.Error;

            var merchantApi = new MerchantApi("url", "username", "password", logger);

			string xmlResponse = File.ReadAllText(_baseProjectPath + Path.DirectorySeparatorChar 
                + "Unit" + Path.DirectorySeparatorChar + "txt" + Path.DirectorySeparatorChar 
                + "CardHolderMessageMustBeShownFalse.xml");

			ApiResult actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);
            Assert.AreEqual(false, actual.ResultMessageMustBeShown);
			xmlResponse = File.ReadAllText(_baseProjectPath + Path.DirectorySeparatorChar
                + "Unit" + Path.DirectorySeparatorChar + "txt" + Path.DirectorySeparatorChar 
                + "CardHolderMessageMustBeShownTrue.xml");
			actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);
			Assert.AreEqual(true, actual.ResultMessageMustBeShown);
		}

		[Test]
		public void ParsePostBackXmlResponse_ReadReasonCode()
		{
            var logFileName = Path.GetTempPath() + "skarptests.log";
            var logger = new FileAltaPayLogger(logFileName);
			logger.LogLevel = AltaPayLogLevel.Error;

			var merchantApi = new MerchantApi("url", "username", "password", logger);

			string xmlResponse = File.ReadAllText(_baseProjectPath + Path.DirectorySeparatorChar
                + "Unit" + Path.DirectorySeparatorChar + "txt" + Path.DirectorySeparatorChar
                + "ReasonCode.xml");
			ApiResult actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);
			PaymentResult result = actual as PaymentResult;
			Assert.AreEqual("NONE", result.Payment.ReasonCode);

		}

		[Test]
		public void ParsePostBackXmlResponse_ReadPaymentId()
		{
            var logFileName = Path.GetTempPath() + "skarptests.log";
            var logger = new FileAltaPayLogger(logFileName);
			logger.LogLevel = AltaPayLogLevel.Error;

			var merchantApi = new MerchantApi("url", "username", "password", logger);

			string xmlResponse = File.ReadAllText(_baseProjectPath + Path.DirectorySeparatorChar
                + "Unit" + Path.DirectorySeparatorChar + "txt" + Path.DirectorySeparatorChar
                + "ReasonCode.xml");
			ApiResult actual = merchantApi.ParsePostBackXmlResponse(xmlResponse);
			PaymentResult result = actual as PaymentResult;
			Assert.AreEqual("17794956-9bb6-4854-9712-bce5931e6e3a", result.Payment.PaymentId);

		}

	}
}