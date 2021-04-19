using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;
using System.Net;

namespace AltaPay.Service
{
	public class FundingsResult
		: ApiResult
	{
		public List<Funding> Fundings { get; set; }
		public int Pages { get; set; }

		public FundingsResult(APIResponse apiResponse, NetworkCredential networkCredential)
		{
			Fundings = new List<Funding>();
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);
				Fundings = new List<Funding>(apiResponse.Body.Fundings.Funding);
				/* TODO, When implementing Com Support
				foreach (Funding funding in Fundings)
				{
					funding.inject(networkCredential);
				}
				*/
				Pages = apiResponse.Body.Fundings.numberOfPages;
			}
			else
			{
				Result = Result.SystemError;
				ResultMerchantMessage = apiResponse.Header.ErrorMessage;
				ResultMessage = "An error occurred";
			}
		}
	}


}
