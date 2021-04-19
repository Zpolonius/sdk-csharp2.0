using System;
using NUnit.Framework;
using AltaPay.Service;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class CreateMultiPaymentRequestTests : BaseTest
	{		

		private IMerchantApi _api;


		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void SimpleMultiPaymentRequest()
		{
			var paymentRequest = new MultiPaymentRequestRequest() {
				Terminal = 	GatewayConstants.terminal,
				ShopOrderId = "multi-payment-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(0, Currency.EUR),
			};

			paymentRequest.AddChild(new MultiPaymentRequestRequestChild() {
				Amount = Amount.Get(12.34m, Currency.EUR)
			});
			
			paymentRequest.AddChild(new MultiPaymentRequestRequestChild() {
				Amount = Amount.Get(98.76m, Currency.EUR)
			});

			MultiPaymentRequestResult result = _api.CreateMultiPaymentRequest(paymentRequest);

			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(Result.Success, result.Result);
			Assert.IsNotEmpty(result.Url);
		}
	}
}
