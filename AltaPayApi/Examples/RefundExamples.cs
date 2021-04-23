using AltaPay.Service;
using AltaPay.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    public class RefundExamples
    {
        private readonly IMerchantApi _api;
        public RefundExamples()
        {
            //This is the URL to connect to your gateway instance. If you are in doubt contact support.
			//For test, use: testgateway.altapaysecure.com
            string gatewayUrl = "https://testgateway.altapaysecure.com/merchant/API/";

            //username to be authenticated on the gateway
            string username = "shop api";

            //provided password for user authentication
            string password = "testpassword";

            //Instatiation of the API helper class which provide all necessary merchant api methods
            //This class requires gateway URL, username and password params are forwarded in the contructor
            _api = new MerchantApi(gatewayUrl, username, password);
        }

        /// <summary>
        /// Example for performing simple refund request. 
        /// Payment has to be in captured state in order to refund be successful.
        /// </summary>
        public void SimpleRefund()
        {
            //Reserving and capturing payment in order to refund method example could be successful
            CaptureResult captureResult = ReserveAndCapture(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = captureResult.Payment.TransactionId;

            //initialize refund request class, this class is used for forwarding all the data needed for refund request
            //for simple refund requests transaction ID is mandatory field
            RefundRequest refundRequest = new RefundRequest
            {
                PaymentId = transactionId
            };

            //call refund method
            RefundResult refundResult = _api.Refund(refundRequest);

            //Result property contains information if the request was successful or not
            if (refundResult.Result == Result.Success)
            {
                //refund was successful
                Transaction transaction = refundResult.Payment;
            }
            else
            {
                //refund unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = refundResult.ResultMerchantMessage;
                string errorMessage = refundResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing simple partial refund request. 
        /// Payment has to be in captured state in order to refund be successful.
        /// Amount sent to be partialy refundend has to be less than total amount of the payment
        /// </summary>
        public void SimplePartialRefund()
        {
            //Reserving and capturing payment in order to refund method example could be successful
            CaptureResult captureResult = ReserveAndCapture(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = captureResult.Payment.TransactionId;

            //initialize refund request class
            //for simple partial refund amount property should be set with amount less than total amount
            RefundRequest refundRequest = new RefundRequest
            {
                PaymentId = transactionId,
                Amount = Amount.Get(450.00, Currency.EUR)
            };

            //call refund method
            RefundResult refundResult = _api.Refund(refundRequest);

            //Result property contains information if the request was successful or not
            if (refundResult.Result == Result.Success)
            {
                //refund was successful
                Transaction transaction = refundResult.Payment;
            }
            else
            {
                //refund unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = refundResult.ResultMerchantMessage;
                string errorMessage = refundResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing complex refund request. 
        /// Payment has to be in captured state in order to refund be successful.
        /// In complex refund request orderlines are sent to the gateway
        /// </summary>
        public void ComplexRefund()
        {
            //Reserving and capturing payment in order to refund method example could be successful
            CaptureResult captureResult = ReserveAndCapture(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = captureResult.Payment.TransactionId;

            //inizialize orderlines
            List<PaymentOrderLine> orderLines = new List<PaymentOrderLine>
            {
                new PaymentOrderLine()
                {
                    Description = "The Item Desc",
                    ItemId = "itemId1",
                    Quantity = 10,
                    TaxPercent = 0,
                    UnitCode = "unitCode",
                    UnitPrice = 120,
                    Discount = 0.00,
                    GoodsType = GoodsType.Item,
                    ProductUrl = "product/path/product.html",
                }
            };

            //initialize refund request class
            //for simple partial refund amount property should be set with amount less than total amount
            RefundRequest refundRequest = new RefundRequest
            {
                PaymentId = transactionId,
                Amount = Amount.Get(1200, Currency.EUR),
                OrderLines = orderLines
            };

            //call refund method
            RefundResult refundResult = _api.Refund(refundRequest);

            //Result property contains information if the request was successful or not
            if (refundResult.Result == Result.Success)
            {
                //refund was successful
                Transaction transaction = refundResult.Payment;
            }
            else
            {
                //refund unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = refundResult.ResultMerchantMessage;
                string errorMessage = refundResult.ResultMessage;
            }
        }

        #region helpers

        /// <summary>
        /// Helper method needed for reserving and capturing payment in order to refund examples could work
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private CaptureResult ReserveAndCapture(Amount amount, AuthType type)
        {
            var request = new ReserveRequest
            {
                ShopOrderId = "csharpexample" + Guid.NewGuid().ToString(),
                Terminal = "AltaPay Dev Terminal",
                Amount = amount,
                PaymentType = type,
                Pan = "4111000011110000",
                ExpiryMonth = 1,
                ExpiryYear = 2012,
                Cvc = "123",
            };

            PaymentResult paymentResult = _api.ReserveAmount(request);

            if (paymentResult.Result != Result.Success)
            {
                throw new Exception("The result was: " + paymentResult.Result + ", message: " + paymentResult.ResultMerchantMessage);
            }

            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = paymentResult.Payment.TransactionId
            };

            CaptureResult captureResult = _api.Capture(captureRequest);

            if (captureResult.Result != Result.Success)
            {
                throw new Exception("The result was: " + captureResult.Result + ", message: " + captureResult.ResultMerchantMessage);
            }

            return captureResult;
        }

        #endregion helpers
    }
}
