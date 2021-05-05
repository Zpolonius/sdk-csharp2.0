using AltaPay.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    public class CreatePaymentRequestExamples
    {
        private readonly IMerchantApi _api;

        public CreatePaymentRequestExamples()
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
        /// Example for performing simple create payment request.
        /// </summary>
        public void CreateSimplePaymentRequest()
        {
            //dedicated terminal on the gateway
            string terminal = "AltaPay Dev Terminal";

            //Instantiation of the payment request class
            //this class is used for forwarding all the data needed for create payment request
            //For simple payment request required properties to be set are gateway terminal, shop order ID 
            //and amont along with currency
            PaymentRequestRequest paymentRequest = new PaymentRequestRequest
            {
                Terminal = terminal,
                ShopOrderId = "payment-req-" + Guid.NewGuid().ToString(),
                Amount = Amount.Get(120.20, Currency.EUR),
                //set the properties for redirection URLs 
                //where user should be redirected after submitting payment on the gateway
                Config = new PaymentRequestConfig
                {
                    CallbackFormUrl = "http://demoshop.pensio.com/Form",
                    CallbackOkUrl = "http://demoshop.pensio.com/Ok",
                    CallbackFailUrl = "http://demoshop.pensio.com/Fail",
                    CallbackRedirectUrl = "http://demoshop.pensio.com/Redirect",
                    CallbackNotificationUrl = "http://demoshop.pensio.com/Notification",
                    CallbackOpenUrl = "http://demoshop.pensio.com/Open",
                    CallbackVerifyOrderUrl = "http://demoshop.pensio.com/VerifyOrder"
                }
                
            };

            //execute create payment method
            PaymentRequestResult paymentRequestResult = _api.CreatePaymentRequest(paymentRequest);

            //Result property contains information if the request was successful or not
            if (paymentRequestResult.Result == Result.Success)
            {
                //URL to the payment form page to redirect user
                string paymentFormURL = paymentRequestResult.Url;

                //Payment request ID returned from the gateway
                string paymentRequestId = paymentRequestResult.PaymentRequestId;
            }
            else
            {
                //error messages contain information about what went wrong
                string errorMerchantMessage = paymentRequestResult.ResultMerchantMessage;
                string errorMessage = paymentRequestResult.ResultMessage;
            }
        }

        /// <summary>
        /// Example for performing complex create payment request. 
        /// </summary>
        public void CreateComplexPaymentRequest()
        {
            //dedicated terminal on the gateway
            string terminal = "AltaPay Dev Terminal";

            CustomerInfo customerInfo = new CustomerInfo
            {
                Email = "johndoe@example.com",
                Username = "johndoe",
                CustomerPhone = "+4512345678",
                BankName = "Example Bank",
                BankPhone = "+4511122356",
                BillingAddress = new CustomerAddress
                {
                    Address = "Skole Allé 63",
                    City = "København K",
                    Country = "DK",
                    Firstname = "John",
                    Lastname = "Doe",
                    Region = "Region Sjælland",
                    PostalCode = "1406"
                },

                ShippingAddress = new CustomerAddress
                {
                    Address = "Skole Allé 63",
                    City = "København K",
                    Country = "DK",
                    Firstname = "John",
                    Lastname = "Doe",
                    Region = "Region Sjælland",
                },
                CardHolder = new CardHolderData
                {
                    Name = "Test cardholder name",
                    Email = "cardholder@example.com",
                    HomePhone = "0012345678",
                    MobilePhone = "0001234567",
                    WorkPhone = "0123456789"
                }

            };

            //initialize orderlines
            List<PaymentOrderLine> orderLines = new List<PaymentOrderLine>
            {
                new PaymentOrderLine()
                {
                    Description = "The Item Desc",
                    ItemId = "itemId1",
                    Quantity = 10,
                    TaxPercent = 10,
                    UnitCode = "unitCode",
                    UnitPrice = 500,
                    Discount = 0.00,
                    GoodsType = GoodsType.Item,
                    ProductUrl = "product/path/product.html",
                }
            };

            //Instantiation of the payment request class
            //this class is used for forwarding all the data needed for create payment request
            PaymentRequestRequest paymentRequest = new PaymentRequestRequest()
            {
                Terminal = terminal,
                ShopOrderId = "payment-req-" + Guid.NewGuid().ToString(),
                Amount = Amount.Get(5056.93, Currency.EUR),
                FraudService = FraudService.Test,
                Source = PaymentSource.eCommerce,

                Config = new PaymentRequestConfig()
                {
                    CallbackFormUrl = "http://demoshop.pensio.com/Form",
                    CallbackOkUrl = "http://demoshop.pensio.com/Ok",
                    CallbackFailUrl = "http://demoshop.pensio.com/Fail",
                    CallbackRedirectUrl = "http://demoshop.pensio.com/Redirect",
                    CallbackNotificationUrl = "http://demoshop.pensio.com/Notification",
                    CallbackOpenUrl = "http://demoshop.pensio.com/Open",
                    CallbackVerifyOrderUrl = "http://demoshop.pensio.com/VerifyOrder"
                },

                // Customer Data
                CustomerInfo = customerInfo,

                CustomerCreatedDate = "2017-11-16",
                Cookie = "thecookie=isgood",
                Language = "en",
                OrganisationNumber = "Orgnumber42",
                SalesInvoiceNumber = "87654321",
                SalesReconciliationIdentifier = "sales_recon_id",
                SalesTax = 56.93,
                ShippingType = ShippingType.TwoDayService,
                AccountOffer = AccountOffer.disabled,
                Type = AuthType.payment,


                // Orderlines
                OrderLines = orderLines,

                // Payment Infos
                PaymentInfos = {
                    {"auxinfo1", "auxvalue1"},
                },
            };

            //execute create payment method
            PaymentRequestResult paymentRequestResult = _api.CreatePaymentRequest(paymentRequest);

            //Result property contains information if the request was successful or not
            if (paymentRequestResult.Result == Result.Success)
            {
                //URL to the payment form page to redirect user
                string paymentFormURL = paymentRequestResult.Url;

                //Payment request ID returned from the gateway
                string paymentRequestId = paymentRequestResult.PaymentRequestId;
            }
            else
            {
                //error messages contain information about what went wrong
                string errorMerchantMessage = paymentRequestResult.ResultMerchantMessage;
                string errorMessage = paymentRequestResult.ResultMessage;
            }
        }
    }
}
