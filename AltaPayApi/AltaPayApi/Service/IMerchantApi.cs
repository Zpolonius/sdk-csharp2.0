using System;
using System.IO;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public interface IMerchantApi
	{
		ChargeSubscriptionResult ChargeSubscription(ChargeSubscriptionRequest request);
		ReserveSubscriptionChargeResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request);
		InvoiceReservationResult CreateInvoiceReservation(InvoiceReservationRequest request);
		UpdateOrderResult UpdateOrder(UpdateOrderRequest request);
		PaymentRequestResult CreatePaymentRequest(PaymentRequestRequest request);
		MultiPaymentRequestResult CreateMultiPaymentRequest(MultiPaymentRequestRequest request);

		[System.Obsolete("Reserve is deprecated, please use ReserveAmount instead.")]
		ReserveResult Reserve(ReserveRequest request);

		ReserveResult ReserveAmount(ReserveRequest request);
		CreditResult Credit(CreditRequest request);
		ReleaseResult Release(ReleaseRequest request);
		CaptureResult Capture(CaptureRequest request);
		RefundResult Refund(RefundRequest request);
		GetPaymentResult GetPayment(GetPaymentRequest request);
		GetPaymentsResult GetPayments(GetPaymentsRequest request);
		FundingsResult GetFundings(GetFundingsRequest request);
		FundingContentResult GetFundingContent(Funding funding);
		void SaveFunding(Funding funding, String folder);
		ApiResult ParsePostBackXmlResponse(string responseStr);
		ApiResult ParsePostBackXmlResponse(Stream responseStr);
		MultiPaymentApiResult ParseMultiPaymentPostBackXmlResponse(string responseStr);
		MultiPaymentApiResult ParseMultiPaymentPostBackXmlResponse(Stream responseStream);
	}
}
