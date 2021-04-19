using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;
using TransactionCardStatus = AltaPay.Service.Dto.TransactionCardStatus;
using AltaPay.Api.Tests;


namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class MerchantApiTests : BaseTest
	{
		IMerchantApi _api;
        private CustomerInfo _testCustomerInfo;
        private List<PaymentOrderLine> _testOrderlines;
        private const string _testKlarnaDKTerminal = "AltaPay Klarna DK";
        private const string _testTerminal = "Simion Demo Shop Test Terminal";
        private const string _expectedTerminal = "Simion Demo Shop Test Terminal";
        

        [SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithFailParametersReturnsFailedResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 5.66, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Failed, result.Result);
			Assert.AreEqual("Card Declined", result.ResultMessage);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithErrorParametersReturnsErrorResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 5.67, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Error, result.Result);
			Assert.AreEqual("Internal Error", result.ResultMessage);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithAPaymentId(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.IsNotNull(result.Payment.TransactionId);
			Assert.IsTrue(result.Payment.TransactionId.Length > 0);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectShopOrderId(Boolean callReservationOfFixedAmount)
		{
			string orderid = Guid.NewGuid().ToString();
			PaymentResult result = GetMerchantApiResult(orderid, 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(orderid, result.Payment.ShopOrderId);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithAlternativeSourceReturnsSuccessfulResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResultWithCustomerAndSource(Guid.NewGuid().ToString(), 1.23, null, PaymentSource.eCommerce, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Success, result.Result);
		}



		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectTerminal(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(_expectedTerminal, result.Payment.Terminal);
		}


		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectReservedAmount(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(1.23, result.Payment.ReservedAmount);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectCapturedAmount(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			Assert.AreEqual(1.23, result.Payment.CapturedAmount);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectPaymentStatus(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("captured", result.Payment.TransactionStatus);
		}

		
		[TestCase(true)]
		[TestCase(false)]
		public void RefundingACapturedPayment(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("captured", result.Payment.TransactionStatus);

			result = _api.Refund(new RefundRequest(){
				PaymentId = result.Payment.PaymentId,
				OrderLines = new List<PaymentOrderLine>()
				{
					new PaymentOrderLine()
					{
						ItemId = "123456",
						Description = "Test product",
						UnitPrice = 24.33
					}
				}
			});

			Assert.AreEqual("refunded", result.Payment.TransactionStatus);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithACreditCardToken(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			
			Assert.NotNull(result.Payment.CreditCardToken);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithACreditCardMaskedPan(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("411100******0002", result.Payment.CreditCardMaskedPan);
		}

        [TestCase(true)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsPaymentSource(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResultWithCustomerAndSource(Guid.NewGuid().ToString(), 40, null,PaymentSource.eCommerce);
			string ExpectedResult = (PaymentSource.eCommerce_without3ds).ToString();
			string ActualResult = (result.Payment.PaymentSource).ToString();

			Assert.AreEqual(ExpectedResult, ActualResult);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithCardStatus(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(TransactionCardStatus.Valid, result.Payment.CardStatus);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithCardTokenResultsInSuccessfullResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			PaymentResult secondResult = GetMerchantApiResultCardToken(Guid.NewGuid().ToString(), 1.23, result.Payment.CreditCardToken, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Success, secondResult.Result);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithAvsInfoReturnsAvsResult(Boolean callReservationOfFixedAmount)
		{
			var customerInfo = new CustomerInfo();
			customerInfo.BillingAddress.Address="Albertslund";

			PaymentResult result = GetMerchantApiResultWithCustomer(Guid.NewGuid().ToString(), 3.34, customerInfo, callReservationOfFixedAmount);

			Assert.AreEqual("A", result.Payment.AddressVerification);
			Assert.AreEqual("Address matches, but zip code does not", result.Payment.AddressVerificationDescription);
		}

        [Test]
        public void NoaNoa_LongOrderLines()
        {

			var captureRequest = new CaptureRequest() {
				PaymentId =  "59",
				Amount = Amount.Get(2039, Currency.XXX),
				OrderLines = {
					new PaymentOrderLine() { Description = "Beautiful linen dress with print", ItemId = "2-1099-1", Quantity = 1, TaxPercent = 0, UnitCode = "ROSEBUD", UnitPrice = 274.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped leggings", ItemId = "2-1357-1", Quantity = 1, TaxPercent = 0, UnitCode = "LIGHT ARO", UnitPrice = 74.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Gorgeous tunic with brodery anglaise", ItemId = "2-1135-1", Quantity = 1, TaxPercent = 0, UnitCode = "PURPLE", UnitPrice = 214.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Cool cotton trousers with stretch", ItemId = "2-1127-1", Quantity = 1, TaxPercent = 0, UnitCode = "WALNUT", UnitPrice = 249.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Cotton dress with print and short slee", ItemId = "2-1277-1", Quantity = 1, TaxPercent = 0, UnitCode = "DOE", UnitPrice = 449, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped, long-sleeved T-shirt", ItemId = "2-0177-11", Quantity = 1, TaxPercent = 0, UnitCode = "CHALK", UnitPrice = 99.5 , Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Cool cotton trousers with stretch", ItemId = "2-1127-1", Quantity = 1, TaxPercent = 0, UnitCode = "WALNUT", UnitPrice = 249.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "T-shirt I many colours", ItemId = "1-2766-1", Quantity = 1, TaxPercent = 0, UnitCode = "IBIS", UnitPrice = 89.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped leggings", ItemId = "2-1357-1", Quantity = 1, TaxPercent = 0, UnitCode = "LIGHT ARO", UnitPrice = 74.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Pretty printed sleeveless cotton top", ItemId = "2-1060-1", Quantity = 1, TaxPercent = 0, UnitCode = "PURPLE", UnitPrice = 189.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Feminine patterned cotton shirt", ItemId = "2-0856-1", Quantity = 1, TaxPercent = 0, UnitCode = "STONE", UnitPrice = 199.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped, long-sleeved T-shirt", ItemId = "2-0177-9", Quantity = 1, TaxPercent = 0, UnitCode = "LIGHT NEC", UnitPrice = 199.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Freight", ItemId = "", Quantity = 1, TaxPercent = 0, UnitCode = "", UnitPrice = 0, Discount = 0, GoodsType = GoodsType.Shipment },
				}
			};
			PaymentResult result = _api.Capture( captureRequest);

			Console.Out.WriteLine("test: " + result.ResultMessage);
        }
		
		[Test]
        public void Capture_DoNotSendAmountIfNotSpecified()
        {
			/**
			 * This test does not really check anything, but it does
			 * send a request to the gateway, where you can then check
			 * that everything is as expected.
			 */
			
			var captureRequest = new CaptureRequest() {
				PaymentId =  "60"
			};
			PaymentResult result = _api.Capture(captureRequest);

			Console.Out.WriteLine("test: " + result.ResultMessage);
        }

        #region ReserveAmount tests

        [Test]
        public void ReserveAmountWithoutOrderlines()
        {
            //arrange
            var sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
            var shopOrderId = Guid.NewGuid().ToString();
            double amount = 1.23;
            var currency = Currency.DKK;

            var request = new ReserveRequest
            {
                Source = PaymentSource.eCommerce,
                ShopOrderId = shopOrderId,
                Terminal = _testTerminal,
                PaymentType = AuthType.payment,
                Amount = Amount.Get(amount, currency),
                Pan = "4111000011110002",
                ExpiryMonth = sixMonthsFromNowDate.Month,
                ExpiryYear = sixMonthsFromNowDate.Year,
                Cvc = "123",
                CustomerInfo = InitializeCustomerInfoTestData(),
            };

            //act
            ReserveResult result = _api.ReserveAmount(request);

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Payment);

            Assert.AreEqual(result.Result, Result.Success);
            Assert.AreEqual(result.Payment.TransactionStatus, GatewayConstants.PreauthTransactionStatus);
            Assert.AreEqual(result.Payment.ShopOrderId, shopOrderId);
            Assert.AreEqual(result.Payment.ReservedAmount, amount);
            Assert.AreEqual(result.Payment.MerchantCurrencyAlpha, currency.ShortName);
            Assert.IsTrue(CompareCustomerInfos(result.Payment.CustomerInfo));
        }

        [Test]
        public void ReserveAmountWithOrderlines()
        {
            //arrange
            var sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
            var shopOrderId = Guid.NewGuid().ToString();
            double amount = 42.0;
            var currency = Currency.DKK;

            var request = new ReserveRequest
            {
                Source = PaymentSource.eCommerce,
                ShopOrderId = shopOrderId,
                Terminal = _testTerminal,
                PaymentType = AuthType.payment,
                Amount = Amount.Get(amount, currency),
                Pan = "4111000011110002",
                ExpiryMonth = sixMonthsFromNowDate.Month,
                ExpiryYear = sixMonthsFromNowDate.Year,
                Cvc = "123",
                CustomerInfo = InitializeCustomerInfoTestData(),
                OrderLines = InitializeOrderlinesTestData()
            };

            //act
            ReserveResult result = _api.ReserveAmount(request);
            
            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Payment);

            Assert.AreEqual(result.Result, Result.Success);
            Assert.AreEqual(result.Payment.TransactionStatus, GatewayConstants.PreauthTransactionStatus);
            Assert.AreEqual(result.Payment.ShopOrderId, shopOrderId);
            Assert.AreEqual(result.Payment.ReservedAmount, amount);
            Assert.AreEqual(result.Payment.MerchantCurrencyAlpha, currency.ShortName);
            Assert.IsTrue(CompareCustomerInfos(result.Payment.CustomerInfo));
        }

        #endregion ReserveAmount tests

        #region Credit tests
        [Test]
        public void CreditWithCardData()
        {
            //arrange
            var sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
            var shopOrderId = Guid.NewGuid().ToString();
            Double amount = 123.45;
            var currency = Currency.DKK;

            var request = new CreditRequest
            {
                Terminal = _testTerminal,
                ShopOrderId = shopOrderId,
                Amount = Amount.Get(amount, currency),
                Pan = "4111000011110087",
                ExpiryMonth = sixMonthsFromNowDate.Month,
                ExpiryYear = sixMonthsFromNowDate.Year,
                Cvc = "123",
                PaymentSource = PaymentSource.eCommerce,
                CardHolderName = "Test CardHolder Name"
            };

            //act
            CreditResult result = _api.Credit(request);

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Payment);

            Assert.AreEqual(result.Result, Result.Success);
            Assert.AreEqual(result.Payment.TransactionStatus, GatewayConstants.CreditedTransactionStatus);
            Assert.AreEqual(result.Payment.ShopOrderId, shopOrderId);
            //TODO:
            //Assert.AreEqual(result.Payment.ReservedAmount, 0d);
            Assert.AreEqual(result.Payment.MerchantCurrencyAlpha, currency.ShortName);
        }
        [Test]
        public void CreditWithCardDataAndTransactionInfo()
        {
            //arrange
            var sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
            var shopOrderId = Guid.NewGuid().ToString();
            Double amount = 100.23;
            var currency = Currency.DKK;

            IDictionary <string, object> TransactionInfo = new Dictionary<string, object>();
            TransactionInfo.Add("sdkName", "c sharp");
            TransactionInfo.Add("sdkVersion","1.1.0");

            var request = new CreditRequest
            {
                Terminal = _testTerminal,
                ShopOrderId = shopOrderId,
                Amount = Amount.Get(amount, currency),
                Pan = "4111000011110087",
                ExpiryMonth = sixMonthsFromNowDate.Month,
                ExpiryYear = sixMonthsFromNowDate.Year,
                Cvc = "123",
                PaymentSource = PaymentSource.eCommerce,
                CardHolderName = "Test CardHolder Name",
                PaymentInfos = TransactionInfo
            };

            //act
            CreditResult result = _api.Credit(request);

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Payment);

            Assert.AreEqual(result.Result, Result.Success);
            Assert.AreEqual(result.Payment.TransactionStatus, GatewayConstants.CreditedTransactionStatus);
            Assert.AreEqual(result.Payment.ShopOrderId, shopOrderId);
            //TODO:
            //Assert.AreEqual(result.Payment.ReservedAmount, 0d);
            Assert.AreEqual(result.Payment.MerchantCurrencyAlpha, currency.ShortName);
        }

        #endregion Credit tests

        #region UpdateOrder tests

        [Test]
        public void UpdateOrderForExistingOrder()
        {
            //arrange
            InvoiceReservationRequest request = new InvoiceReservationRequest
            {
                Terminal = _testKlarnaDKTerminal,
                ShopOrderId = "invoice-" + Guid.NewGuid().ToString(),
                Amount = Amount.Get(42.00, Currency.DKK),
                CustomerInfo = InitializeCustomerInfoTestData(),
                OrderLines = InitializeOrderlinesTestData(),
                PersonalIdentifyNumber = "0801363945",
                BirthDate = "0801363945"
            };
            InvoiceReservationResult reservationResult = _api.CreateInvoiceReservation(request);

            CaptureRequest captureRequest = new CaptureRequest
            {
                Amount = Amount.Get(42.00, Currency.DKK),
                PaymentId = reservationResult.Payment.PaymentId
            };
            CaptureResult captureResult = _api.Capture(captureRequest);

            List<PaymentOrderLine> orderlinesToUpdate = new List<PaymentOrderLine>();

            PaymentOrderLine orderlineToUpdate = InitializeOrderlinesTestData()[0];
            orderlineToUpdate.Quantity = 0 - orderlineToUpdate.Quantity;
            orderlinesToUpdate.Add(orderlineToUpdate);

            PaymentOrderLine newOrderLine = new PaymentOrderLine
            {
                Description = "New Item 1",
                ItemId = "3",
                Quantity = 2,
                GoodsType = GoodsType.Item,
                UnitPrice = 11.0
            };

            orderlinesToUpdate.Add(newOrderLine);
            
            //orderlinesToUpdate.ForEach(p => p.Quantity = 0 - p.Quantity);
            UpdateOrderRequest updateOrderRequest = new UpdateOrderRequest(captureResult.Payment.PaymentId, orderlinesToUpdate);

            //act
            UpdateOrderResult updateOrderResult = _api.UpdateOrder(updateOrderRequest);

            //assert
            Assert.AreEqual(updateOrderResult.Result, Result.Success);
        }

        [Test]
        public void UpdateOrderForNonExistingOrder()
        {
            //arrange
            List<PaymentOrderLine> orderlinesToUpdate = new List<PaymentOrderLine>();

            PaymentOrderLine orderlineToUpdate = InitializeOrderlinesTestData()[0];
            orderlineToUpdate.Quantity = 0 - orderlineToUpdate.Quantity;
            orderlinesToUpdate.Add(orderlineToUpdate);

            PaymentOrderLine newOrderLine = new PaymentOrderLine
            {
                Description = "New Item 1",
                ItemId = "3",
                Quantity = 2,
                GoodsType = GoodsType.Item,
                UnitPrice = 11.0
            };

            orderlinesToUpdate.Add(newOrderLine);

            UpdateOrderRequest updateOrderRequest = new UpdateOrderRequest("-1", orderlinesToUpdate);

            //act
            UpdateOrderResult updateOrderResult = _api.UpdateOrder(updateOrderRequest);

            //assert
            Assert.AreEqual(updateOrderResult.Result, Result.SystemError);
            Assert.IsNotEmpty(updateOrderResult.ResultMessage);
            Assert.IsNotEmpty(updateOrderResult.ResultMerchantMessage);
        }

        [Test]
        public void UpdateOrderForNotCapturedOrder()
        {
            //arrange
            InvoiceReservationRequest request = new InvoiceReservationRequest
            {
                Terminal = _testKlarnaDKTerminal,
                ShopOrderId = "invoice-" + Guid.NewGuid().ToString(),
                Amount = Amount.Get(42.00, Currency.DKK),
                CustomerInfo = InitializeCustomerInfoTestData(),
                OrderLines = InitializeOrderlinesTestData(),
                PersonalIdentifyNumber = "0801363945",
                BirthDate = "0801363945"
            };
            InvoiceReservationResult reservationResult = _api.CreateInvoiceReservation(request);

            List<PaymentOrderLine> orderlinesToUpdate = new List<PaymentOrderLine>();

            PaymentOrderLine orderlineToUpdate = InitializeOrderlinesTestData()[0];
            orderlineToUpdate.Quantity = 0 - orderlineToUpdate.Quantity;
            orderlinesToUpdate.Add(orderlineToUpdate);

            PaymentOrderLine newOrderLine = new PaymentOrderLine
            {
                Description = "New Item 1",
                ItemId = "3",
                Quantity = 2,
                GoodsType = GoodsType.Item,
                UnitPrice = 11.0
            };

            orderlinesToUpdate.Add(newOrderLine);

            UpdateOrderRequest updateOrderRequest = new UpdateOrderRequest(reservationResult.Payment.PaymentId,
                                                                            orderlinesToUpdate);

            //act
            UpdateOrderResult updateOrderResult = _api.UpdateOrder(updateOrderRequest);

            //assert
            Assert.AreEqual(updateOrderResult.Result, Result.SystemError);
            Assert.IsNotEmpty(updateOrderResult.ResultMessage);
            Assert.IsNotEmpty(updateOrderResult.ResultMerchantMessage);
        }

        #endregion UpdateOrder tests

        private PaymentResult GetMerchantApiResultWithCustomer(string shopOrderId, double amount, CustomerInfo customerInfo, 
			Boolean callReservationOfFixedAmount)
		{

			return GetMerchantApiResultWithCustomerAndSource(shopOrderId, amount, customerInfo, PaymentSource.moto, callReservationOfFixedAmount);

		}

		private PaymentResult GetMerchantApiResultWithCustomerAndSource(string shopOrderId, double amount, CustomerInfo customerInfo, 
			PaymentSource source = PaymentSource.moto, Boolean callReservationOfFixedAmount = true)
		{
            var sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
			var request = new ReserveRequest {
				Source = source,
				ShopOrderId = shopOrderId,
				Terminal = _testTerminal,
				PaymentType = AuthType.payment,
				Amount = Amount.Get(amount, Currency.DKK),
				Pan = "4111000011110002",
				ExpiryMonth = sixMonthsFromNowDate.Month,
				ExpiryYear = sixMonthsFromNowDate.Year,
				Cvc = "123",
				CustomerInfo = customerInfo,
			};

			return _api.ReserveAmount(request); // reservation

		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, Boolean callReservationOfFixedAmount = true)
		{
            DateTime sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
			var request = new ReserveRequest {
				Terminal = _testTerminal,
				ShopOrderId = shopOrderId,
				PaymentType = AuthType.paymentAndCapture,
				Amount = Amount.Get(amount, Currency.DKK),
				Pan = "4111000011110002",
				ExpiryMonth = sixMonthsFromNowDate.Month,
				ExpiryYear = sixMonthsFromNowDate.Year,
				Cvc = "123",
			};

			return _api.ReserveAmount(request); // reservation
		}

		private PaymentResult GetMerchantApiResultCardToken(string shopOrderId, double amount, string cardToken, Boolean callReservationOfFixedAmount = true)
		{
			var request = new ReserveRequest {
				Terminal = _testTerminal,
				ShopOrderId = shopOrderId,
				PaymentType = AuthType.payment,
				Amount = Amount.Get(amount, Currency.DKK),
				CreditCardToken = cardToken,
				Cvc = "123",
			};

				return _api.ReserveAmount(request); // reservation
		}

        #region CustomerInfo helper methods

        private CustomerInfo InitializeCustomerInfoTestData()
        {
            if (_testCustomerInfo == null)
            {
                _testCustomerInfo = new CustomerInfo
                {
                    BankName = "Banca Intesa",
                    BankPhone = "+4530312781",
                    CustomerPhone = "+4530312782",
                    Email = "test@example.com",
                    Username = "aa",
                    BirthDate = DateTime.Now,
                    BillingAddress = new CustomerAddress
                    {
                        Address = "Byvej 97",
                        City = "Fakse",
                        Country = "DK",
                        Firstname = "Andreas",
                        Lastname = "Andresen",
                        PostalCode = "4640",
                        Region = "Region Sjælland"
                    },
                    ShippingAddress = new CustomerAddress
                    {
                        Address = "Byvej 97",
                        City = "Fakse",
                        Country = "DK",
                        Firstname = "Andreas",
                        Lastname = "Andresen",
                        PostalCode = "4640",
                        Region = "Region Sjælland"
                    },
                    CardHolder = new CardHolderData
                    {
                        Name = "Test cardholder name",
                        Email = "cardholder@example.com",
                        WorkPhone = "0123456789",
                        HomePhone = "0012345678",
                        MobilePhone = "0001234567"
                    }
                };
            }

            return _testCustomerInfo;
        }

        private List<PaymentOrderLine> InitializeOrderlinesTestData()
        {
            if (_testOrderlines == null)
            {
               _testOrderlines = new List<PaymentOrderLine>
                {
                    new PaymentOrderLine
                    {
                        Description = "Item 1",
                        ItemId = "1",
                        Quantity = 2,
                        UnitPrice = 11.0,
                        GoodsType = GoodsType.Item,
                    },
                    new PaymentOrderLine
                    {
                        Description = "Item 2",
                        ItemId = "2",
                        Quantity = 1,
                        UnitPrice = 5.0,
                        GoodsType = GoodsType.Item
                    },
                    new PaymentOrderLine
                    {
                        Description = "shipment",
                        ItemId = "shipment",
                        Quantity = 1,
                        UnitPrice = 15.0,
                        GoodsType = GoodsType.Shipment
                    }
                };
            }

            return _testOrderlines;
        }

        /// <summary>
        /// Compares CustomerInfo sent from the server with private variable kept as test data 
        /// </summary>
        /// <param name="customerInfoToCompare"></param>
        /// <returns>True if customer infos match, False otherwise</returns>
        private bool CompareCustomerInfos(AltaPay.Service.Dto.CustomerInfo customerInfoToCompare)
        {
            if (_testCustomerInfo.Email != customerInfoToCompare.Email || _testCustomerInfo.Username != customerInfoToCompare.Username
                || !CompareCustomerAddresses(_testCustomerInfo.BillingAddress, customerInfoToCompare.BillingAddress)
                || !CompareCustomerAddresses(_testCustomerInfo.ShippingAddress, customerInfoToCompare.ShippingAddress))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compares if customer address returned from the server matches with local address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="addressToCompare"></param>
        /// <returns>True if addresses match, False otherwise</returns>
        private bool CompareCustomerAddresses(CustomerAddress address, AltaPay.Service.Dto.CustomerInfoAddress addressToCompare)
        {
            if (address.Address != addressToCompare.Address || address.City != addressToCompare.City
                || address.Country != addressToCompare.Country || address.Firstname != addressToCompare.Firstname
                || address.Lastname != addressToCompare.Lastname || address.PostalCode != addressToCompare.PostalCode
                || address.Region != addressToCompare.Region)

            {
                return false;
            }

            return true;
        }

        #endregion CustomerInfo helper methods
    }
}
