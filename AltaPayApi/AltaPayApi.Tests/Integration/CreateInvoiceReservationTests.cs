using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;
using AltaPay.Service;
using System.Diagnostics;
using AltaPay.Service.Dto;
using System.IO;
using System.Net;
using AltaPay.Api.Tests;


namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class CreateInvoiceReservationTests : BaseTest
	{

		//private ParameterHelper ParameterHelper = new ParameterHelper();

		private IMerchantApi _api;
		private const String terminal = "AltaPay Test Invoice Terminal DK";

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void CreateSimpleInvoiceReservationRequest()
		{
			InvoiceReservationRequest request = new InvoiceReservationRequest() {

				Terminal = terminal,
				ShopOrderId = "invoice-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get (42.42, Currency.DKK),

				// Customer Data
				CustomerInfo = {
					Email = "customer@example.com",

					BillingAddress = new CustomerAddress() {
						Address = "101 Night Street",
						PostalCode = "billing postal"					
					},
				},

			};

			InvoiceReservationResult result = _api.CreateInvoiceReservation(request);

			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(null, result.ResultMessage);
			Assert.AreEqual(Result.Success, result.Result);

			Assert.AreEqual (request.Terminal, result.Payment.Terminal);
			Assert.AreEqual (request.ShopOrderId, result.Payment.ShopOrderId);
			Assert.AreEqual (request.CustomerInfo.BillingAddress.Address, result.Payment.CustomerInfo.BillingAddress.Address);
			Assert.AreEqual (request.CustomerInfo.BillingAddress.PostalCode, result.Payment.CustomerInfo.BillingAddress.PostalCode);
			Assert.AreEqual (request.CustomerInfo.Email, result.Payment.CustomerInfo.Email);

		}

		[Test]
		public void CreateComplexInvoiceReservationRequest()
		{
			InvoiceReservationRequest request = new InvoiceReservationRequest() {

				Terminal = terminal,
				ShopOrderId = "invoice-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get (42.42, Currency.DKK),

				// Payment Infos
				PaymentInfos = new Dictionary<string, object>() {
					{"auxinfo1", "auxvalue1"},
				},

				AuthType = AuthType.payment,
				AccountNumber = "111",
				BankCode = "222",
				FraudService = FraudService.Red,
				PaymentSource = PaymentSource.eCommerce,
				OrganisationNumber = "333",
				PersonalIdentifyNumber = "444",
				BirthDate = "555",

				// Orderlines
				OrderLines = {
					new PaymentOrderLine() {
						Description = "The Item Desc", 
						ItemId = "itemId1",
						Quantity = 10,
						TaxPercent = 10,
						UnitCode = "unitCode",
						UnitPrice = 500,
						Discount = 0.00,
						GoodsType = GoodsType.Item,
					},
				},

				// Customer Data
				CustomerInfo = {
					Email = "customer@example.com",
					Username = "leatheruser",
					CustomerPhone = "+4512345678",
					BankName = "Gotham Bank",
					BankPhone = "666 666 666",
					
					BillingAddress = new CustomerAddress() {
						Address = "101 Night Street",
						City = "Gotham City",
						Country = "DK",
						Firstname = "Bruce",
						Lastname = "Wayne",
						Region = "Dark Region",
						PostalCode = "001"
					},
					
					ShippingAddress = new CustomerAddress() {
						Address = "42 Joker Avenue",
						City = "Big Smile City",
						Country = "BR",
						Firstname = "Jack",
						Lastname = "Napier",
						Region = "Umbrella Neighbourhood",
						PostalCode = "002"
					}
				},

			};
			
			// And make the actual invocation.
			InvoiceReservationResult result = _api.CreateInvoiceReservation(request);

			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(null, result.ResultMessage);
			Assert.AreEqual(Result.Success, result.Result);

			Assert.AreEqual(request.Terminal, result.Payment.Terminal);
			Assert.AreEqual(request.ShopOrderId, result.Payment.ShopOrderId);

			Assert.AreEqual(request.AuthType.ToString(), result.Payment.AuthType);

			Assert.AreEqual("auxinfo1", result.Payment.PaymentInfos[0].name);
			Assert.AreEqual(request.PaymentInfos["auxinfo1"], result.Payment.PaymentInfos[0].Value);

			AltaPay.Service.Dto.CustomerInfo ci = result.Payment.CustomerInfo;

			Assert.AreEqual(request.CustomerInfo.Email, ci.Email);
			Assert.AreEqual(request.CustomerInfo.Username, ci.Username);
			Assert.AreEqual(request.CustomerInfo.CustomerPhone, ci.CustomerPhone);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.Firstname, ci.BillingAddress.Firstname);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.Lastname, ci.BillingAddress.Lastname);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.Address, ci.BillingAddress.Address);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.City, ci.BillingAddress.City);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.Region, ci.BillingAddress.Region);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.PostalCode, ci.BillingAddress.PostalCode);
			Assert.AreEqual(request.CustomerInfo.BillingAddress.Country, ci.BillingAddress.Country);

			Assert.AreEqual(request.CustomerInfo.ShippingAddress.Firstname, ci.ShippingAddress.Firstname);
			Assert.AreEqual(request.CustomerInfo.ShippingAddress.Lastname, ci.ShippingAddress.Lastname);
			Assert.AreEqual(request.CustomerInfo.ShippingAddress.Address, ci.ShippingAddress.Address);
			Assert.AreEqual(request.CustomerInfo.ShippingAddress.City, ci.ShippingAddress.City);
			Assert.AreEqual(request.CustomerInfo.ShippingAddress.Region, ci.ShippingAddress.Region);
			Assert.AreEqual(request.CustomerInfo.ShippingAddress.PostalCode, ci.ShippingAddress.PostalCode);
			Assert.AreEqual(request.CustomerInfo.ShippingAddress.Country, ci.ShippingAddress.Country);

			// System.Diagnostics.Process.Start(result.Url);
		}

	}
}
