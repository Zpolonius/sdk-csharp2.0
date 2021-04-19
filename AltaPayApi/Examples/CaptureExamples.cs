using AltaPay.Service;
using AltaPay.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    public class CaptureExamples
    {
        private readonly IMerchantApi _api;

        public CaptureExamples()
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
        /// Example for performing simple capture request.
        /// </summary>
        public void SimpleCapture()
        {
            //Reserving amount in order to capture method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(2190.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

            //initialize capture request class, this class is used for forwarding all the data needed for capture request
            //for simple capture request only transaction ID is required
            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = transactionId
            };

            //call capture method
            CaptureResult captureResult = _api.Capture(captureRequest);

            //Result property contains information if the request was successful or not
            if (captureResult.Result == Result.Success)
            {
                //capture was successful
                Transaction transaction = captureResult.Payment;
            }
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = captureResult.ResultMerchantMessage;
                string errorMessage = captureResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing simple partial capture request. 
        /// Amount sent to be partialy captured has to be less than total amount of the payment
        /// </summary>
        public void SimplePartialCapture()
        {
            //Reserving amount in order to capture method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

            //initialize capture request class, this class is used for forwarding all the data needed for capture request
            //for simple partial capture amount property should be set with amount less than total amount
            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = transactionId,
                Amount = Amount.Get(600.00, Currency.EUR)
            };

            //call capture method
            CaptureResult captureResult = _api.Capture(captureRequest);

            //Result property contains information if the request was successful or not
            if (captureResult.Result == Result.Success)
            {
                //capture was successful
                Transaction transaction = captureResult.Payment;
            }
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = captureResult.ResultMerchantMessage;
                string errorMessage = captureResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing complex capture request. 
        /// In complex capture request orderlines are sent to the gateway
        /// </summary>
        public void ComplexCapture()
        {
            //Reserving amount in order to capture method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(1200.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

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
                }
            };
            //initialize capture request class, this class is used for forwarding all the data needed for capture request
            //for simple partial capture Amount property should be set with amount less than total amount
            CaptureRequest captureRequest = new CaptureRequest
            {
                PaymentId = transactionId,
                Amount = Amount.Get(1200.00, Currency.EUR),
                OrderLines = orderLines
            };

            //call capture method
            CaptureResult captureResult = _api.Capture(captureRequest);

            //Result property contains information if the request was successful or not
            if (captureResult.Result == Result.Success)
            {
                //capture was successful
                Transaction transaction = captureResult.Payment;
            }
            else
            {
                //capture unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = captureResult.ResultMerchantMessage;
                string errorMessage = captureResult.ResultMessage;
            }
        }

        #region helpers

        /// <summary>
        /// Helper method needed for reserving amount in order to capture examples could work
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private PaymentResult ReserveAmount(Amount amount, AuthType type)
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

            PaymentResult result = _api.ReserveAmount(request);

            if (result.Result != Result.Success)
            {
                throw new Exception("The result was: " + result.Result + ", message: " + result.ResultMerchantMessage);
            }

            return result;
        }

        #endregion helprers
    }
}
