using System;
using System.Collections.Generic;
using AltaPay.Service;
using NUnit.Framework;
namespace AltaPay.Service.Examples
{
	public class Example_Klarna
	{
		private static IMerchantApi _api = new MerchantApi("http://esteban.earth.pensio.com/merchant.php/API/", "shop api", "testpassword");

		public Example_Klarna()
		{
		}

		[Test] // Not really a unit test, just a Klarna calling example
		public static void Main()
		{
			String orderId = "Example_Klarna_" + new Random().Next();

			CustomerAddress address = new CustomerAddress {
				Address = "Sæffleberggate 56,1 mf",
				City = "Varde",
				Country = "DK",
				Firstname = "Testperson-dk",
				Lastname = "Approved",
				Region = "DK",
				PostalCode = "6800"
			};

			PaymentRequestRequest request = new PaymentRequestRequest {
				ShopOrderId = orderId,
				Terminal = "AltaPay Klarna DK",
				Amount = Amount.Get(5.5, Currency.DKK),
				Type = AuthType.payment,
				CustomerInfo = new CustomerInfo {
					Email = "myuser@mymail.com",
					Username = "myuser",
					CustomerPhone = "20123456",
					BankName = "My Bank",
					BankPhone = "+45 12-34 5678",
					BillingAddress = address,
					ShippingAddress = address
				}
			};

			request.OrderLines = new List<PaymentOrderLine> {
				new PaymentOrderLine {
					Description = "description 1", 
					ItemId = "id 01", 
					Quantity = 1, 
					UnitPrice = 1.1,
					GoodsType = GoodsType.Item
				},
				new PaymentOrderLine {
					Description = "description 2", 
					ItemId = "id 02", 
					Quantity = 2, 
					UnitPrice = 2.2,
					GoodsType = GoodsType.Item
				}
			};


			PaymentRequestResult result = _api.CreatePaymentRequest(request);

			// Access the url below and use the social security number 0801363945 in the page form to complete the Klarna order
			System.Console.WriteLine(result.Url);
		}
	}
}

