using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using AltaPay.Service.Dto;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Schema;
using AltaPay.Service.Loggers;


namespace AltaPay.Service
{
	public class MerchantApi : IMerchantApi
	{
		// Dependency
		private ParameterHelper ParameterHelper = new ParameterHelper();

		private string _gatewayUrl;
		private string _username;
		private string _password;
		private IAltaPayLogger logger;
		private string _sdkVersion;

		public MerchantApi(string gatewayUrl, string username, string password) : this(gatewayUrl, username, password, null)
		{
		}

		public MerchantApi(string gatewayUrl, string username, string password, IAltaPayLogger logger)
		{
			_gatewayUrl = gatewayUrl;
			_username = username;
			_password = password;

			if (logger == null)
			{
				this.logger = new BlackholeAltaPayLogger();
			}
			else
			{
				this.logger = logger;
			}
		}

		public ReserveResult Reserve(ReserveRequest request) 
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount.GetAmountString());
			parameters.Add("currency", request.Amount.Currency.GetNumericString());
			parameters.Add("type", request.PaymentType);
			parameters.Add("payment_source", request.Source);

			if (request.CreditCardToken!=null) {
				parameters.Add("credit_card_token", request.CreditCardToken);
			} else {
				parameters.Add("cardnum", request.Pan);
				parameters.Add("emonth", request.ExpiryMonth);
				parameters.Add("eyear", request.ExpiryYear);
			}
			parameters.Add("cvc", request.Cvc);

			if (request.CustomerInfo!=null)
				request.CustomerInfo.AddToDictionary(parameters);

			if(request.CustomerCreatedDate != null){
				parameters.Add("customer_created_date", request.CustomerCreatedDate);
			}

			parameters.Add("fraud_service", request.FraudService.ToString().ToLower());

			// Order lines
			parameters = getOrderLines(parameters, request.OrderLines);

			return new ReserveResult(GetResponseFromApiCall("reservationOfFixedAmount", parameters));
		}

		public ReserveResult ReserveAmount(ReserveRequest request) 
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount.GetAmountString());
			parameters.Add("currency", request.Amount.Currency.GetNumericString());
			parameters.Add("type", request.PaymentType);
			parameters.Add("payment_source", request.Source);

			if (request.CreditCardToken!=null) {
				parameters.Add("credit_card_token", request.CreditCardToken);
			} else {
				parameters.Add("cardnum", request.Pan);
				parameters.Add("emonth", request.ExpiryMonth);
				parameters.Add("eyear", request.ExpiryYear);
			}
			parameters.Add("cvc", request.Cvc);

			if (request.CustomerInfo != null) {
				parameters.Add("customer_info", request.CustomerInfo.AddToDictionary(new Dictionary<string, object>()));
			}

			if(request.CustomerCreatedDate != null){
				parameters.Add("customer_created_date", request.CustomerCreatedDate);
			}

			parameters.Add("fraud_service", request.FraudService.ToString().ToLower());

			// Order lines
			parameters = getOrderLines(parameters, request.OrderLines);

			return new ReserveResult(GetResponseFromApiCall("reservation", parameters));
		}

		public CreditResult Credit(CreditRequest request) 
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			// Mandatory
			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount.GetAmountString());
			parameters.Add("currency", request.Amount.Currency.GetNumericString());

			if (request.CreditCardToken!=null) {
				//Second option
				parameters.Add("credit_card_token", request.CreditCardToken);
			} else {
				//First option
				parameters.Add("cardnum", request.Pan);
				parameters.Add("emonth", request.ExpiryMonth);
				parameters.Add("eyear", request.ExpiryYear);
			}
			// Optional parameters
			if (request.Cvc!=null)
			{
				parameters.Add("cvc", request.Cvc);
			}
			// 3D Secure parameter
			if (request.CardHolderName!=null)
			{
				parameters.Add("cardholder_name", request.CardHolderName);
			}
			if (request.PaymentInfos!=null)
			{
				parameters.Add("transaction_info", request.PaymentInfos);
			}

			parameters.Add("payment_source", request.PaymentSource);
	

			return new CreditResult(GetResponseFromApiCall("credit", parameters));
		}

		private Dictionary<string,Object> getOrderLines(Dictionary<string,Object> parameters, IList<PaymentOrderLine> orderLines)
		{
			int lineNumber = 0;
			Dictionary<string,Object> orderLinesParam = new Dictionary<string,Object>();
			foreach (PaymentOrderLine orderLine in orderLines)
			{
				Dictionary<string,Object> orderLineParam = new Dictionary<string,Object>();
				orderLineParam.Add("itemId", orderLine.ItemId);
				orderLineParam.Add("quantity", orderLine.Quantity);

				if (orderLine.TaxPercent != double.MinValue)
				{
					orderLineParam.Add("taxPercent", orderLine.TaxPercent);
				}

				if (orderLine.TaxAmount != double.MinValue)
				{
					orderLineParam.Add("taxAmount", orderLine.TaxAmount);
				}

				orderLineParam.Add("unitCode", orderLine.UnitCode);
				orderLineParam.Add("unitPrice", orderLine.UnitPrice);
				orderLineParam.Add("description", orderLine.Description);
				orderLineParam.Add("discount", orderLine.Discount);
				orderLineParam.Add("goodsType", orderLine.GoodsType.ToString().ToLower());
				orderLineParam.Add("imageUrl", orderLine.ImageUrl);

				orderLinesParam.Add(lineNumber.ToString(), orderLineParam);
				lineNumber++;
			}
			parameters.Add("orderLines", orderLinesParam);
			return parameters;
		}

		public CaptureResult Capture(CaptureRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);

			if (request.Amount != default(Amount))
			{
				parameters.Add("amount", request.Amount.GetAmountString());
			}

			if (request.ReconciliationId!=null)
			{
				parameters.Add("reconciliation_identifier", request.ReconciliationId);
			}

			if (request.InvoiceNumber!=null)
			{
				parameters.Add("invoice_number", request.InvoiceNumber);
			}

			if (request.SalesTax.HasValue)
			{
				parameters.Add("sales_tax", request.SalesTax);
			}

			getOrderLines(parameters, request.OrderLines);

			return new CaptureResult(GetResponseFromApiCall("captureReservation", parameters));
		}

		public RefundResult Refund(RefundRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);
			if (request.Amount != null)
			{
				parameters.Add("amount", request.Amount.GetAmountString());
			}
			if (request.ReconciliationId != null)
			{
				parameters.Add("reconciliation_identifier", request.ReconciliationId);
			}
			getOrderLines(parameters, request.OrderLines);
			return new RefundResult(GetResponseFromApiCall("refundCapturedReservation", parameters));
		}


		public ReleaseResult Release(ReleaseRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);

			return new ReleaseResult(GetResponseFromApiCall("releaseReservation", parameters));
		}

		public GetPaymentResult GetPayment(GetPaymentRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);

			return new GetPaymentResult(GetResponseFromApiCall("transactions", parameters));
		}

		public GetPaymentsResult GetPayments(GetPaymentsRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("shop_orderid", request.ShopOrderId);

			return new GetPaymentsResult(GetResponseFromApiCall("transactions", parameters));
		}



		public ChargeSubscriptionResult ChargeSubscription(ChargeSubscriptionRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.SubscriptionId);
			parameters.Add("amount", request.Amount.GetAmountString());

			return new ChargeSubscriptionResult(GetResponseFromApiCall("chargeSubscription",parameters));
		}

		public ReserveSubscriptionChargeResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.SubscriptionId);
			parameters.Add("amount", request.Amount.GetAmountString());

			return new ReserveSubscriptionChargeResult(GetResponseFromApiCall("reserveSubscriptionCharge", parameters));
		}

		public FundingsResult GetFundings(GetFundingsRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("page", request.Page);
			return new FundingsResult(GetResponseFromApiCall("fundingList",parameters), new NetworkCredential(_username, _password));
		}

		public FundingContentResult GetFundingContent(Funding funding)
		{
			return new FundingContentResult(funding.DownloadLink, new NetworkCredential(_username, _password));
		}

		public void SaveFunding(Funding funding, String folder)
		{
			FundingContentResult fundingContenResult = this.GetFundingContent(funding);
			String cvs = fundingContenResult.GetFundingContent();

			String end = folder.Substring(folder.Length - 1);
			String localPath = folder;
			if (!end.Equals("/"))
			{
				localPath = localPath + "/";			
			}

			String path = localPath + funding.Filename + ".cvs";
			StreamWriter file = new StreamWriter(path);
			file.WriteLine(cvs);
			file.Close();
		}

		private Dictionary<string,Object> GetBasicCreatePaymentRequestParameters(BasePaymentRequestRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			// Mandatory arguments
			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("currency", request.Amount.Currency.GetNumericString());

			// Config
			parameters.Add("config", request.Config.ToDictionary());

			// Optional Arguments
			parameters.Add("language", request.Language);
			parameters.Add("transaction_info", request.PaymentInfos);
			parameters.Add("type", request.Type);
			parameters.Add("ccToken", request.CreditCardToken);
			parameters.Add("cookie", request.Cookie);
			parameters.Add("fraud_service", request.FraudService.ToString().ToLower());

			return parameters;
		}

		private Dictionary<string,Object> GetCreateInvoiceReservationRequestParameters(InvoiceReservationRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			// Mandatory arguments
			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount.Value);
			parameters.Add("currency", request.Amount.Currency);
			parameters.Add("customer_info", request.CustomerInfo.AddToDictionary(new Dictionary<string, object>()));

			// Optional arguments
			parameters.Add("type", request.AuthType);
			parameters.Add("transaction_info", request.PaymentInfos);
			parameters.Add("accountNumber", request.AccountNumber);
			parameters.Add("bankCode", request.BankCode);
			parameters.Add("fraud_service", request.FraudService == null ? null : request.FraudService.ToString().ToLower());
			parameters.Add("payment_source", request.PaymentSource);

			parameters = getOrderLines(parameters, request.OrderLines);

			parameters.Add("organisationNumber", request.OrganisationNumber);
			parameters.Add("personalIdentifyNumber", request.PersonalIdentifyNumber);
			parameters.Add("birthDate", request.BirthDate);

			return parameters;
		}

		private Dictionary<string,Object> GetCreateUpdateOrderRequestParameters(UpdateOrderRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			// Mandatory arguments
			parameters.Add("payment_id", request.PaymentId);
			parameters = getOrderLines(parameters, request.OrderLines);

			return parameters;
		}

		public InvoiceReservationResult CreateInvoiceReservation(InvoiceReservationRequest request)
		{

			var parameters = GetCreateInvoiceReservationRequestParameters(request);

			return new InvoiceReservationResult(GetResponseFromApiCall("createInvoiceReservation", parameters));

		}

		public UpdateOrderResult UpdateOrder(UpdateOrderRequest request)
		{

			var parameters = GetCreateUpdateOrderRequestParameters(request);

			return new UpdateOrderResult(GetResponseFromApiCall("updateOrder", parameters));

		}

		public PaymentRequestResult CreatePaymentRequest(PaymentRequestRequest request)
		{
			var parameters = GetBasicCreatePaymentRequestParameters(request);

			// Mandatory arguments
			parameters.Add("amount", request.Amount.GetAmountString());

			// Optional Arguments
			parameters.Add("sales_reconciliation_identifier", request.SalesReconciliationIdentifier);
			parameters.Add("sales_invoice_number", request.SalesInvoiceNumber);
			parameters.Add("sales_tax", request.SalesTax);
			parameters.Add("shipping_method", request.ShippingType);
			parameters.Add("customer_created_date", request.CustomerCreatedDate);
			parameters.Add("organisation_number", request.OrganisationNumber);
			parameters.Add("account_offer", request.AccountOffer);
			parameters.Add("payment_source", request.Source);

			// Customer Info
			parameters.Add("customer_info", request.CustomerInfo.AddToDictionary(new Dictionary<string, object>()));

			// Order lines
			parameters = getOrderLines(parameters, request.OrderLines);

			return new PaymentRequestResult(GetResponseFromApiCall("createPaymentRequest", parameters));
		}

		public MultiPaymentRequestResult CreateMultiPaymentRequest(MultiPaymentRequestRequest request)
		{
			var parameters = GetBasicCreatePaymentRequestParameters(request);

			// Mandatory arguments
			parameters.Add("multi", GetMultiPaymentRequestChildrenAsParameter(request));

			return new MultiPaymentRequestResult(GetResponseFromApiCall("createMultiPaymentRequest", parameters));
		}

		private Dictionary<string,Object> GetMultiPaymentRequestChildrenAsParameter(MultiPaymentRequestRequest request)
		{
			var parameter = new Dictionary<string,Object>();

			int childIndex = 0;
			foreach (MultiPaymentRequestRequestChild child in request.Children)
			{
				var childParam = new Dictionary<string,Object>();

				childParam.Add("amount", child.Amount.GetAmountString());

				if (child.TypeIsSet)
				{
					childParam.Add("type", child.Type);
				}

				if (!String.IsNullOrEmpty(child.Terminal))
				{
					childParam.Add("terminal", child.Terminal);
				}

				if (!String.IsNullOrEmpty(child.ShopOrderId))
				{
					childParam.Add("shop_orderid", child.ShopOrderId);
				}

				if (child.PaymentInfos != null && child.PaymentInfos.Count > 0)
				{
					childParam.Add("transaction_info", request.PaymentInfos);
				}

				if (child.ShippingTypeIsSet)
				{
					childParam.Add("shipping_method", child.ShippingType);
				}

				if (!String.IsNullOrEmpty(child.SalesReconciliationIdentifier))
				{
					childParam.Add("sale_reconciliation_identifier", child.SalesReconciliationIdentifier);
				}

				if (child.OrderLines != null && child.OrderLines.Count > 0)
				{
					childParam = getOrderLines(childParam, child.OrderLines);
				}


				parameter.Add(childIndex.ToString(), childParam);
				childIndex++;
			}

			return parameter;
		}

		private string StreamToString(Stream stream)
		{
			var sr = new StreamReader(stream);
			stream.Position = 0;
			return sr.ReadToEnd();
		}

		public ApiResult ParsePostBackXmlResponse(string responseStr)
		{
			using (Stream stream = new MemoryStream()) {
				StreamWriter writer = new StreamWriter(stream);
				writer.Write(responseStr);
				writer.Flush();
				stream.Position = 0;
				return ParsePostBackXmlResponse(stream);
			}
		}

		public ApiResult ParsePostBackXmlResponse(Stream responseStream)
		{
			// Get the apiResponse
			APIResponse apiResponse = GetApiResponse(responseStream);
			if (apiResponse.Header == null)
			{
				logger.Error("ParsePostBackXmlResponse: Header is null - received the following...");
				logger.Error(StreamToString(responseStream));
				throw new Exception("Invalid response : API response header is null - check " + logger.WhereDoYouLogTo());
			}

			if (apiResponse.Header.ErrorCode != 0)
			{
				throw new Exception("Invalid response : " + apiResponse.Header.ErrorMessage);
			}


			string authType = "payment";

			// Detect auth type 
			if (!apiResponse.Body.Result.Equals("Error"))
			{
				if (apiResponse.Body.Transactions == null || apiResponse.Body.Transactions.Length == 0)
				{
					throw new Exception("The response contains no transactions");
				}
				authType = apiResponse.Body.Transactions[0].AuthType;
			}

			// Wrap Api Respons to proper result
			switch (authType) 
			{
				case "payment":
				case "paymentAndCapture":
				case "recurring":
				case "subscription":
				case "verifyCard":
					return new ReserveResult(apiResponse);

				case "subscriptionAndCharge":
				case "recurringAndCapture":
				case "subscriptionAndReserve":
					return new SubscriptionResult(apiResponse);

				default: 
					throw new Exception("Unhandled Authtype : " + authType);
			}
		}

		public MultiPaymentApiResult ParseMultiPaymentPostBackXmlResponse(string responseStr)
		{
			using (Stream stream = new MemoryStream()) {
				StreamWriter writer = new StreamWriter(stream);
				writer.Write(responseStr);
				writer.Flush();
				stream.Position = 0;
				return ParseMultiPaymentPostBackXmlResponse(stream);
			}
		}

		public MultiPaymentApiResult ParseMultiPaymentPostBackXmlResponse(Stream responseStream)
		{
			// Get the apiResponse
			APIResponse apiResponse = GetApiResponse(responseStream);

			if (apiResponse.Header == null)
			{
				logger.Error("ParseMultiPaymentPostBackXmlResponse: Header is null - received the following...");
				logger.Error(StreamToString(responseStream));
				throw new Exception("Invalid response : API response header is null - check " + logger.WhereDoYouLogTo());
			}

			if (apiResponse.Header.ErrorCode != 0)
			{
				throw new Exception("Invalid response : " + apiResponse.Header.ErrorMessage);
			}

			// Detect auth type 
			if (apiResponse.Body.Actions.Length == 0)
			{
				throw new Exception("The response contains no actions");
			}

			return new MultiPaymentApiResult(apiResponse);
		}

		private APIResponse GetApiResponse(Stream stream)
		{
			try
			{
				return ConvertXml<APIResponse>(stream);
			}
			catch (Exception exception)
			{
				logger.Error("GetApiResponse: {0}", exception);
				logger.Error("GetApiResponse received the following...");
				logger.Error(StreamToString(stream));

				APIResponse response = new APIResponse();
				response.Header = new Header();

				response.Header.ErrorMessage = exception.Message + "\n" + exception.StackTrace;
				if (exception.InnerException != null)
					response.Header.ErrorMessage += "\n" + exception.InnerException.Message;

				response.Header.ErrorCode = 1;
				return response;
			}
		}

		private APIResponse GetResponseFromApiCall(string method, Dictionary<string,Object> parameters)
		{
			using (Stream responseStream = CallApi(method, parameters))
			{
				/*
				// dumping response for debugging... this would be easier with .NET 4 as it has Stream.CopyTo(..)
				using (var fileStream = File.Create("/tmp/multipaymentrequest_response"))
				{
					byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
					int bytesRead;

					while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
					{
						fileStream.Write(buffer, 0, bytesRead);
					}
				}//*/

				return GetApiResponse(responseStream);
			}
		}

		private string GetSdkVersion()
		{
			if (String.IsNullOrEmpty(_sdkVersion))
			{
				Version v = this.GetType().Assembly.GetName().Version;
				_sdkVersion = String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
			}
			else
			{
			    _sdkVersion = "0.0.1";
			}

			return _sdkVersion;
		}

		private Stream CallApi(string method, Dictionary<string,Object> parameters)
		{
		    //Use either TLS 1.1 or TLS 1.2
		    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

			WebRequest request = WebRequest.Create(String.Format("{0}{1}", _gatewayUrl, method));
			request.Credentials = new NetworkCredential(_username, _password);

			HttpWebRequest http = (HttpWebRequest)request;
			http.Method = "POST";
			http.ContentType = "application/x-www-form-urlencoded";
			http.Headers.Add("x-altapay-client-version", String.Format("C#SDK/{0}", GetSdkVersion()));

			string encodedData = ParameterHelper.Convert(parameters);
			//File.AppendAllText("/tmp/multipaymentrequest", + encodedData + "\n");
			Byte[] postBytes = System.Text.Encoding.ASCII.GetBytes(encodedData);
			http.ContentLength = postBytes.Length;

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(postBytes, 0, postBytes.Length);
			requestStream.Close();

			WebResponse response = request.GetResponse();
			return response.GetResponseStream();
		}

		private T ConvertXml<T>(Stream xml)
		{
			var serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(xml);
		}

	}
}
