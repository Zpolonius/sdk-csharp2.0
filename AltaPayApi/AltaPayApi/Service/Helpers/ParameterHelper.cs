using System;
using System.Web;
using System.Collections.Generic;

using System.Globalization;
using System.Text;


namespace AltaPay.Service
{
	public class ParameterHelper
	{
		public string Convert(Dictionary<string, Object> parameters)
		{
			return string.Join ("&", ConvertInternal(null, parameters).ToArray());
		}
		
		private List<string> ConvertInternal(string prefix, Dictionary<string, Object> parameters)
		{
			List<string> asString = new List<string>();
			foreach(KeyValuePair<string, Object> parameter in parameters)
			{
				if(parameter.Value == null)
				{
					continue;
				}
				string key = (prefix == null) ? HttpUtility.UrlEncode(parameter.Key, Encoding.UTF8) : prefix + "[" + HttpUtility.UrlEncode(parameter.Key, Encoding.UTF8) + "]";
				
				if(parameter.Value.GetType() == typeof(Dictionary<string,object>))
				{
					asString.AddRange(ConvertInternal(key, (Dictionary<string,object>)parameter.Value));
				}
				else
				{
					string val = null;
					Type paramType = parameter.Value.GetType();
					if(paramType == typeof(Double) || paramType == typeof(Single) || paramType == typeof(Decimal))
					{
						val = string.Format(Globalisation.AmountNumberFormat, "{0:0.00}", parameter.Value);
					}
					else
					{
						val = parameter.Value.ToString();
					}
					asString.Add(key + "=" + HttpUtility.UrlEncode(val, Encoding.UTF8));
				}
			}
			return asString;
		}

		public string convertDateTimeToString(DateTime date){
			return date.ToString("yyyy-mm-dd");
		}

	}
}

