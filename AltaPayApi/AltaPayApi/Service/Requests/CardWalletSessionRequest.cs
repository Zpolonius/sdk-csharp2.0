using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class CardWalletSessionRequest
	{		
		public string Terminal { get; set; }
		public string ValidationUrl { get; set; }
		public string Domain { get; set; }

		public CardWalletSessionRequest(string terminal, string validationUrl, string domain)
		{
			this.Terminal = terminal;
			this.ValidationUrl = validationUrl;
			this.Domain = domain;
			
		}		
	}
}