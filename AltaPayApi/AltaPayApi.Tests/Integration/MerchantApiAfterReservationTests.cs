using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class MerchantApiAfterReservationTests : BaseTest
	{
		private MerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void CapturePaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			
			this.WaitForDataToFlowIntoReporting();

			var request = new CaptureRequest() {
				PaymentId =  reserveResult.Payment.TransactionId,
				Amount = Amount.Get(1.23, Currency.DKK),
			};
			PaymentResult result = _api.Capture( request);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CapturePaymentWithOrderLinesReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			if (reserveResult.Result != Result.Success)
				throw new Exception(reserveResult.ResultMessage);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new CaptureRequest() {
				PaymentId =  reserveResult.Payment.TransactionId,
				Amount = Amount.Get(1.23, Currency.DKK),
				OrderLines = {
					new PaymentOrderLine() {
						Description = "Ninja",
						ItemId = "N1",
						Quantity = 1.0,
						TaxPercent = 0.25,
						UnitCode = "kg",
						UnitPrice = 100.00,
						Discount = 10,
						GoodsType = GoodsType.Item
					}
				}
			};
			PaymentResult result = _api.Capture( request);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void RefundPaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new RefundRequest() {
				PaymentId = reserveResult.Payment.TransactionId,
				Amount = Amount.Get(1.23, Currency.XXX),
			};
			Assert.AreEqual(Result.Success, _api.Refund(request).Result);
		}

		[Test]
		public void RefundPaymentReturnsSuccess_WithoutSendingAMount()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new RefundRequest() {
				PaymentId = reserveResult.Payment.TransactionId,
			};
			Assert.AreEqual(Result.Success, _api.Refund(request).Result);
		}
		
		[Test]
		public void RefundPaymentReturnsRefundedAmount()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new RefundRequest() {
				PaymentId = reserveResult.Payment.TransactionId,
				Amount = Amount.Get(1.11, Currency.DKK),
			};
			Assert.AreEqual(1.11, _api.Refund(request).Payment.RefundedAmount);
		}

		[Test]
		public void ReleasePaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new ReleaseRequest {
				PaymentId = reserveResult.Payment.TransactionId,
			};
			PaymentResult result = _api.Release(request);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void GetPaymentReturnsPayment()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			
			this.WaitForDataToFlowIntoReporting();
			
			PaymentResult result = _api.GetPayment(new GetPaymentRequest { PaymentId = createPaymentResult.Payment.TransactionId} );

			Assert.AreEqual(createPaymentResult.Payment.TransactionId, result.Payment.TransactionId);
		}

		[Test]
		public void GetNonExistingPaymentReturnsNullPayment()
		{
			PaymentResult result = _api.GetPayment(new GetPaymentRequest { PaymentId = "-1"});

			Assert.IsNull(result.Payment);
		}

		[Test]
		public void ChargeSubscriptionReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new ChargeSubscriptionRequest() {
				SubscriptionId = createPaymentResult.Payment.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ChargeSubscription(request);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void ChargeSubscriptionReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new ChargeSubscriptionRequest() {
				SubscriptionId = createPaymentResult.Payment.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ChargeSubscription(request);

			Assert.AreEqual(createPaymentResult.Payment.TransactionId, result.Payment.TransactionId);
			Assert.AreEqual("recurring_confirmed", result.Payment.TransactionStatus);
			Assert.AreEqual("captured", result.RecurringPayment.TransactionStatus);
		}

		[Test]
		public void ReserveSubscriptionChargeReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new ReserveSubscriptionChargeRequest {
				SubscriptionId = createPaymentResult.Payment.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ReserveSubscriptionCharge(request);
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void ReserveSubscriptionChargeReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			
			this.WaitForDataToFlowIntoReporting();
			
			var request = new ReserveSubscriptionChargeRequest {
				SubscriptionId = createPaymentResult.Payment.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ReserveSubscriptionCharge(request);

			Assert.AreEqual(createPaymentResult.Payment.TransactionId, result.Payment.TransactionId);
			Assert.AreEqual("recurring_confirmed", result.Payment.TransactionStatus);
			Assert.AreEqual("preauth", result.RecurringPayment.TransactionStatus);
		}

		private PaymentResult ReserveAmount(double amount, AuthType type)
		{
			var request = new ReserveRequest {
				ShopOrderId = "csharptest"+Guid.NewGuid().ToString(),
				Terminal = "AltaPay Soap Test Terminal",
				Amount = Amount.Get(amount, Currency.DKK),
				PaymentType = type,
				Pan = "4111000011110000",
				ExpiryMonth = 1,
				ExpiryYear = 2012,
				Cvc = "123",
			};

			PaymentResult result = _api.Reserve(request);
			
			if(result.Result != Result.Success)
			{
				throw new Exception("The result was: "+result.Result+", message: "+result.ResultMerchantMessage);
			}
			
			return result;
		}
	}
}
