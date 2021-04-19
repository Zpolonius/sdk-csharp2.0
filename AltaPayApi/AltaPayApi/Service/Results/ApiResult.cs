using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public abstract class ApiResult
	{
		public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public string ResultMerchantMessage { get; set; }
		public bool ResultMessageMustBeShown { get; set; }
	}
}
