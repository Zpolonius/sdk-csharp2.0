using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PensioMoto.Service.Dto
{
	[XmlRoot(ElementName="APIResponse")]
	public class PaymentApiResponse
			: ApiResponse
	{
		public PaymentBody Body { get; set; }
	}
}
