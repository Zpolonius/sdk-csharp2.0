using System;
using System.Globalization;

namespace AltaPay
{
	internal static class Globalisation
	{
		public static NumberFormatInfo AmountNumberFormat
		{
			get
			{
				return (new CultureInfo("en-US")).NumberFormat;
			}
		}

		public static CultureInfo DecimalCultureInfo
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		public static CultureInfo DateTimeCultureInfo
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		public static DateTimeStyles DateTimeStyle
		{
			get
			{
				return DateTimeStyles.None;
			}
		}
				
	}
}

