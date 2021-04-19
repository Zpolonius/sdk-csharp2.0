using System;
using AltaPay.Service.Dto;


namespace AltaPay.Service
{
	public class InvoiceReservationResult
		: PaymentResult
    {

		public InvoiceReservationResult(APIResponse apiResponse) : base(apiResponse)
		{
		}

    }
}
