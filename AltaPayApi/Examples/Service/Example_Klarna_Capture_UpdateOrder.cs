using System;
using System.Collections.Generic;
using AltaPay.Service;
using NUnit.Framework;
namespace AltaPay.Service.Examples
{
	public class Example_Klarna_Capture_UpdateOrder
	{
		private static IMerchantApi _api = new MerchantApi("http://esteban.earth.pensio.com/merchant.php/API/", "shop api", "testpassword");

		public Example_Klarna_Capture_UpdateOrder()
		{
		}

		[Test] // Not really a unit test, just a Klarna calling example
		public static void Main()
		{		
			String paymentId = "35"; // PUT A PAYMENT ID FROM A PREVIOUSLY CREATED ORDER HERE

			CaptureResult captureResult = _api.Capture(new CaptureRequest {PaymentId = paymentId});

			Assert.AreEqual(Result.Success, captureResult.Result);

			List<PaymentOrderLine> orderLines = new List<PaymentOrderLine> {
				new PaymentOrderLine {
					Description = "description 1", 
					ItemId = "id 01", 
					Quantity = -1, 
					UnitPrice = 1.1,
					GoodsType = GoodsType.Item
				},
				new PaymentOrderLine {
					Description = "new item", 
					ItemId = "new id", 
					Quantity = 1, 
					UnitPrice = 1.1,
					GoodsType = GoodsType.Item
				}
			};

			UpdateOrderRequest req = new UpdateOrderRequest(paymentId, orderLines);

			UpdateOrderResult result = _api.UpdateOrder(req);

			Assert.AreEqual(Result.Success, result.Result);
		}
	}
}

