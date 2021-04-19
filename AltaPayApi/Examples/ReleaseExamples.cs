using AltaPay.Service;
using AltaPay.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    public class ReleaseExamples
    {
        private readonly IMerchantApi _api;
        public ReleaseExamples()
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
        /// Example for performing  release payment request.
        /// </summary>
        public void Release()
        {
            //Reserving amount in order to release method example could be successful
            PaymentResult paymentResult = ReserveAmount(Amount.Get(2190.00, Currency.EUR), AuthType.payment);
            //Transaction ID is returned from the gateway when payment request was successful
            string transactionId = paymentResult.Payment.TransactionId;

            //initialize release request class
            ReleaseRequest releaseRequest = new ReleaseRequest
            {
                PaymentId = transactionId
            };

            //call release method
            ReleaseResult releaseResult = _api.Release(releaseRequest);

            //Result property contains information if the request was successful or not
            if (releaseResult.Result == Result.Success)
            {
                //release was successful
                Transaction transaction = releaseResult.Payment;
            }
            else
            {
                //release unsuccessful
                //error messages contain information about what went wrong
                string errorMerchantMessage = releaseResult.ResultMerchantMessage;
                string errorMessage = releaseResult.ResultMessage;
            }

        }

        #region helpers

        /// <summary>
        /// Helper method needed for reserving amount in order to release examples could work
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

        #endregion helpers
    }
}
