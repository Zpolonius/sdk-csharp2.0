using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CreditResult : PaymentResult
	{

		public CreditResult(APIResponse response) : base(response) 
		{

		}
	}
}

