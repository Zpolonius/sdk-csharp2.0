using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace AltaPay.Service
{
	public class Currency
	{
		public static readonly Currency ALL  = new Currency(8, "ALL", "Lek",2);
		public static readonly Currency DZD  = new Currency(12, "DZD", "Algerian Dinar",2);
		public static readonly Currency ARS  = new Currency(32, "ARS", "Argentine Peso",2);
		public static readonly Currency AUD  = new Currency(36, "AUD", "Australian Dollar",2);
		public static readonly Currency BSD  = new Currency(44, "BSD", "Bahamian Dollar",2);
		public static readonly Currency BHD  = new Currency(48, "BHD", "Bahraini Dinar",3);
		public static readonly Currency BDT  = new Currency(50, "BDT", "Taka",2);
		public static readonly Currency AMD  = new Currency(51, "AMD", "Armenian Dram",2);
		public static readonly Currency BBD  = new Currency(52, "BBD", "Barbados Dollar",2);
		public static readonly Currency BMD  = new Currency(60, "BMD", "Bermudian Dollar (customarily known as Bermuda Dollar)",2);
		public static readonly Currency BTN  = new Currency(64, "BTN", "Ngultrum",2);
		public static readonly Currency BOB  = new Currency(68, "BOB", "Boliviano",2);
		public static readonly Currency BWP  = new Currency(72, "BWP", "Pula",2);
		public static readonly Currency BZD  = new Currency(84, "BZD", "Belize Dollar",2);
		public static readonly Currency SBD  = new Currency(90, "SBD", "Solomon Islands Dollar",2);
		public static readonly Currency BND  = new Currency(96, "BND", "Brunei Dollar",2);
		public static readonly Currency MMK  = new Currency(104, "MMK", "Kyat",0);
		public static readonly Currency BIF  = new Currency(108, "BIF", "Burundi Franc",0);
		public static readonly Currency KHR  = new Currency(116, "KHR", "Riel",0);
		public static readonly Currency CAD  = new Currency(124, "CAD", "Canadian Dollar",2);
		public static readonly Currency CVE  = new Currency(132, "CVE", "Cape Verde Escudo",0);
		public static readonly Currency KYD  = new Currency(136, "KYD", "Cayman Islands Dollar",2);
		public static readonly Currency LKR  = new Currency(144, "LKR", "Sri Lanka Rupee",2);
		public static readonly Currency CLP  = new Currency(152, "CLP", "Chilean Peso",0);
		public static readonly Currency CNY  = new Currency(156, "CNY", "Yuan Renminbi",2);
		public static readonly Currency COP  = new Currency(170, "COP", "Colombian Peso",2);
		public static readonly Currency KMF  = new Currency(174, "KMF", "Comoro Franc",0);
		public static readonly Currency CRC  = new Currency(188, "CRC", "Costa Rican Colon",2);
		public static readonly Currency HRK  = new Currency(191, "HRK", "Croatian Kuna",2);
		public static readonly Currency CUP  = new Currency(192, "CUP", "Cuban Peso",2);
		public static readonly Currency CZK  = new Currency(203, "CZK", "Czech Koruna",2);
		public static readonly Currency DKK  = new Currency(208, "DKK", "Danish Krone",2);
		public static readonly Currency DOP  = new Currency(214, "DOP", "Dominican Peso",2);
		public static readonly Currency SVC  = new Currency(222, "SVC", "El Salvador Colon",2);
		public static readonly Currency ETB  = new Currency(230, "ETB", "Ethiopian Birr",2);
		public static readonly Currency ERN  = new Currency(232, "ERN", "Nakfa",2);
		public static readonly Currency EEK  = new Currency(233, "EEK", "Kroon",2);
		public static readonly Currency FKP  = new Currency(238, "FKP", "Falkland Islands Pound",2);
		public static readonly Currency FJD  = new Currency(242, "FJD", "Fiji Dollar",2);
		public static readonly Currency DJF  = new Currency(262, "DJF", "Djibouti Franc",0);
		public static readonly Currency GMD  = new Currency(270, "GMD", "Dalasi",2);
		public static readonly Currency GIP  = new Currency(292, "GIP", "Gibraltar Pound",2);
		public static readonly Currency GTQ  = new Currency(320, "GTQ", "Quetzal",2);
		public static readonly Currency GNF  = new Currency(324, "GNF", "Guinea Franc",0);
		public static readonly Currency GYD  = new Currency(328, "GYD", "Guyana Dollar",2);
		public static readonly Currency HTG  = new Currency(332, "HTG", "Gourde",2);
		public static readonly Currency HNL  = new Currency(340, "HNL", "Lempira",2);
		public static readonly Currency HKD  = new Currency(344, "HKD", "Hong Kong Dollar",2);
		public static readonly Currency HUF  = new Currency(348, "HUF", "Forint",2);
		public static readonly Currency ISK  = new Currency(352, "ISK", "Iceland Krona",0);
		public static readonly Currency INR  = new Currency(356, "INR", "Indian Rupee",2);
		public static readonly Currency IDR  = new Currency(360, "IDR", "Rupiah",2);
		public static readonly Currency IRR  = new Currency(364, "IRR", "Iranian Rial",0);
		public static readonly Currency IQD  = new Currency(368, "IQD", "Iraqi Dinar",0);
		public static readonly Currency ILS  = new Currency(376, "ILS", "New Israeli Sheqel",2);
		public static readonly Currency JMD  = new Currency(388, "JMD", "Jamaican Dollar",2);
		public static readonly Currency JPY  = new Currency(392, "JPY", "Yen",0);
		public static readonly Currency KZT  = new Currency(398, "KZT", "Tenge",2);
		public static readonly Currency JOD  = new Currency(400, "JOD", "Jordanian Dinar",3);
		public static readonly Currency KES  = new Currency(404, "KES", "Kenyan Shilling",2);
		public static readonly Currency KPW  = new Currency(408, "KPW", "North Korean Won",0);
		public static readonly Currency KRW  = new Currency(410, "KRW", "Won",0);
		public static readonly Currency KWD  = new Currency(414, "KWD", "Kuwaiti Dinar",3);
		public static readonly Currency KGS  = new Currency(417, "KGS", "Som",2);
		public static readonly Currency LAK  = new Currency(418, "LAK", "Kip",0);
		public static readonly Currency LBP  = new Currency(422, "LBP", "Lebanese Pound",0);
		public static readonly Currency LSL  = new Currency(426, "LSL", "Loti",2);
		public static readonly Currency LVL  = new Currency(428, "LVL", "Latvian Lats",2);
		public static readonly Currency LRD  = new Currency(430, "LRD", "Liberian Dollar",2);
		public static readonly Currency LYD  = new Currency(434, "LYD", "Libyan Dinar",3);
		public static readonly Currency LTL  = new Currency(440, "LTL", "Lithuanian Litas",2);
		public static readonly Currency MOP  = new Currency(446, "MOP", "Pataca",1);
		public static readonly Currency MWK  = new Currency(454, "MWK", "Kwacha",2);
		public static readonly Currency MYR  = new Currency(458, "MYR", "Malaysian Ringgit",2);
		public static readonly Currency MVR  = new Currency(462, "MVR", "Rufiyaa",2);
		public static readonly Currency MUR  = new Currency(480, "MUR", "Mauritius Rupee",2);
		public static readonly Currency MXN  = new Currency(484, "MXN", "Mexican Peso",2);
		public static readonly Currency MNT  = new Currency(496, "MNT", "Tugrik",2);
		public static readonly Currency MDL  = new Currency(498, "MDL", "Moldovan Leu",2);
		public static readonly Currency MAD  = new Currency(504, "MAD", "Moroccan Dirham",2);
		public static readonly Currency OMR  = new Currency(512, "OMR", "Rial Omani",3);
		public static readonly Currency NAD  = new Currency(516, "NAD", "Namibia Dollar",2);
		public static readonly Currency NPR  = new Currency(524, "NPR", "Nepalese Rupee",2);
		public static readonly Currency ANG  = new Currency(532, "ANG", "Netherlands Antillian Guilder",2);
		public static readonly Currency AWG  = new Currency(533, "AWG", "Aruban Guilder",2);
		public static readonly Currency VUV  = new Currency(548, "VUV", "Vatu",0);
		public static readonly Currency NZD  = new Currency(554, "NZD", "New Zealand Dollar",2);
		public static readonly Currency NIO  = new Currency(558, "NIO", "Cordoba Oro",2);
		public static readonly Currency NGN  = new Currency(566, "NGN", "Naira",2);
		public static readonly Currency NOK  = new Currency(578, "NOK", "Norwegian Krone",2);
		public static readonly Currency PKR  = new Currency(586, "PKR", "Pakistan Rupee",2);
		public static readonly Currency PGK  = new Currency(598, "PGK", "Kina",2);
		public static readonly Currency PYG  = new Currency(600, "PYG", "Guarani",0);
		public static readonly Currency PEN  = new Currency(604, "PEN", "Nuevo Sol",2);
		public static readonly Currency PHP  = new Currency(608, "PHP", "Philippine Peso",2);
		public static readonly Currency GWP  = new Currency(624, "GWP", "Guinea-Bissau Peso",0);
		public static readonly Currency QAR  = new Currency(634, "QAR", "Qatari Rial",2);
		public static readonly Currency RUB  = new Currency(643, "RUB", "Russian Ruble",2);
		public static readonly Currency RWF  = new Currency(646, "RWF", "Rwanda Franc",0);
		public static readonly Currency SHP  = new Currency(654, "SHP", "Saint Helena Pound",2);
		public static readonly Currency STD  = new Currency(678, "STD", "Dobra",0);
		public static readonly Currency SAR  = new Currency(682, "SAR", "Saudi Riyal",2);
		public static readonly Currency SCR  = new Currency(690, "SCR", "Seychelles Rupee",2);
		public static readonly Currency SLL  = new Currency(694, "SLL", "Leone",0);
		public static readonly Currency SGD  = new Currency(702, "SGD", "Singapore Dollar",2);
		public static readonly Currency SKK  = new Currency(703, "SKK", "Slovak Koruna",1);
		public static readonly Currency VND  = new Currency(704, "VND", "Dong",0);
		public static readonly Currency SOS  = new Currency(706, "SOS", "Somali Shilling",2);
		public static readonly Currency ZAR  = new Currency(710, "ZAR", "Rand",2);
		public static readonly Currency SZL  = new Currency(748, "SZL", "Lilangeni",2);
		public static readonly Currency SEK  = new Currency(752, "SEK", "Swedish Krona",2);
		public static readonly Currency CHF  = new Currency(756, "CHF", "Swiss Franc",2);
		public static readonly Currency SYP  = new Currency(760, "SYP", "Syrian Pound",2);
		public static readonly Currency THB  = new Currency(764, "THB", "Baht",2);
		public static readonly Currency TOP  = new Currency(776, "TOP", "Pa'anga",2);
		public static readonly Currency TTD  = new Currency(780, "TTD", "Trinidad and Tobago Dollar",2);
		public static readonly Currency AED  = new Currency(784, "AED", "UAE Dirham",2);
		public static readonly Currency TND  = new Currency(788, "TND", "Tunisian Dinar",3);
		public static readonly Currency TMM  = new Currency(795, "TMM", "Manat",0);
		public static readonly Currency UGX  = new Currency(800, "UGX", "Uganda Shilling",0);
		public static readonly Currency MKD  = new Currency(807, "MKD", "Denar",2);
		public static readonly Currency EGP  = new Currency(818, "EGP", "Egyptian Pound",2);
		public static readonly Currency GBP  = new Currency(826, "GBP", "Pound Sterling",2);
		public static readonly Currency TZS  = new Currency(834, "TZS", "Tanzanian Shilling",2);
		public static readonly Currency USD  = new Currency(840, "USD", "US Dollar",2);
		public static readonly Currency UYU  = new Currency(858, "UYU", "Peso Uruguayo",2);
		public static readonly Currency UZS  = new Currency(860, "UZS", "Uzbekistan Sum",2);
		public static readonly Currency WST  = new Currency(882, "WST", "Tala",2);
		public static readonly Currency YER  = new Currency(886, "YER", "Yemeni Rial",0);
		public static readonly Currency ZMK  = new Currency(894, "ZMK", "Zambian Kwacha",0);
		public static readonly Currency TWD  = new Currency(901, "TWD", "New Taiwan Dollar",2);
		public static readonly Currency ZWR  = new Currency(935, "ZWR", "Zimbabwe Dollar",0);
		public static readonly Currency GHS  = new Currency(936, "GHS", "Cedi",2);
		public static readonly Currency VEF  = new Currency(937, "VEF", "Bolivar Fuerte",2);
		public static readonly Currency SDG  = new Currency(938, "SDG", "Sudanese Pound",2);
		public static readonly Currency RSD  = new Currency(941, "RSD", "Serbian Dinar",2);
		public static readonly Currency MZN  = new Currency(943, "MZN", "Metical",2);
		public static readonly Currency AZN  = new Currency(944, "AZN", "Azerbaijanian Manat",2);
		public static readonly Currency RON  = new Currency(946, "RON", "New Leu",2);
		public static readonly Currency CHE  = new Currency(947, "CHE", "WIR Euro",2);
		public static readonly Currency CHW  = new Currency(948, "CHW", "WIR Franc",2);
		public static readonly Currency TRY  = new Currency(949, "TRY", "Turkish Lira",2);
		public static readonly Currency XAF  = new Currency(950, "XAF", "CFA Franc BEAC",0);
		public static readonly Currency XCD  = new Currency(951, "XCD", "East Caribbean Dollar",2);
		public static readonly Currency XOF  = new Currency(952, "XOF", "CFA Franc BCEAO",0);
		public static readonly Currency XPF  = new Currency(953, "XPF", "CFP Franc",0);
		public static readonly Currency XBA  = new Currency(955, "XBA", "Bond Markets Units European Composite Unit (EURCO)",0);
		public static readonly Currency XBB  = new Currency(956, "XBB", "European Monetary Unit (E.M.U.-6)",0);
		public static readonly Currency XBC  = new Currency(957, "XBC", "European Unit of Account 9(E.U.A.-9)",0);
		public static readonly Currency XBD  = new Currency(958, "XBD", "European Unit of Account 17(E.U.A.-17)",0);
		public static readonly Currency XAU  = new Currency(959, "XAU", "Gold",0);
		public static readonly Currency XDR  = new Currency(960, "XDR", "SDR",0);
		public static readonly Currency XAG  = new Currency(961, "XAG", "Silver",0);
		public static readonly Currency XPT  = new Currency(962, "XPT", "Platinum",0);
		public static readonly Currency XTS  = new Currency(963, "XTS", "Codes specifically reserved for testing purposes",0);
		public static readonly Currency XPD  = new Currency(964, "XPD", "Palladium",0);
		public static readonly Currency SRD  = new Currency(968, "SRD", "Surinam Dollar",2);
		public static readonly Currency COU  = new Currency(970, "COU", "Unidad de Valor Real",2);
		public static readonly Currency AFN  = new Currency(971, "AFN", "Afghani",2);
		public static readonly Currency TJS  = new Currency(972, "TJS", "Somoni",2);
		public static readonly Currency AOA  = new Currency(973, "AOA", "Kwanza",2);
		public static readonly Currency BYR  = new Currency(974, "BYR", "Belarussian Ruble",0);
		public static readonly Currency BGN  = new Currency(975, "BGN", "Bulgarian Lev",2);
		public static readonly Currency CDF  = new Currency(976, "CDF", "Congolese Franc",2);
		public static readonly Currency BAM  = new Currency(977, "BAM", "Convertible Marks",2);
		public static readonly Currency EUR  = new Currency(978, "EUR", "Euro",2);
		public static readonly Currency MXV  = new Currency(979, "MXV", "Mexican Unidad de Inversion (UDI)",2);
		public static readonly Currency UAH  = new Currency(980, "UAH", "Hryvnia",2);
		public static readonly Currency GEL  = new Currency(981, "GEL", "Lari",2);
		public static readonly Currency BOV  = new Currency(984, "BOV", "Mvdol",2);
		public static readonly Currency PLN  = new Currency(985, "PLN", "Zloty",2);
		public static readonly Currency BRL  = new Currency(986, "BRL", "Brazilian Real",2);
		public static readonly Currency CLF  = new Currency(990, "CLF", "Unidades de fomento",0);
		public static readonly Currency USN  = new Currency(997, "USN", "US Dollar (Next day)",2);
		public static readonly Currency USS  = new Currency(998, "USS", "US Dollar (Same day)",2);
		public static readonly Currency XXX  = new Currency(999, "XXX", "The code assigned for transactions where no currency is involved",2);

		private static Currency[] values = null;
		private static Dictionary<string,Currency> shortNameToCurrency = null;
		private static Dictionary<int,Currency> numericToCurrency = null;
		
		public int NumericValue {get; private set; }
		public string ShortName {get; private set; }
		public string Name {get; private set; }
		public int Decimals {get; private set; }

		
		private Currency(int numericValue, string shortName, string name, int decimals)
		{
			NumericValue = numericValue;
			ShortName = shortName;
			Name = name;
			Decimals = decimals;
		}
		
		public static IEnumerable<Currency> GetValues() {
			if (values==null) {
				var staticFields = typeof(Currency).GetFields(BindingFlags.Public | BindingFlags.Static);
				var currencyValues = staticFields.Select(x=>x.GetValue(null)).Where(x=>x.GetType()==typeof(Currency));
				values = currencyValues.Cast<Currency>().ToArray();	
			}
			return values; 
		}
		
		public override string ToString ()
		{
			return ShortName;
		}
		
		public string GetNumericString()
		{
			return NumericValue.ToString("D3");
		}
		
		public static Currency FromNumeric(int numericValue)
		{
			if (numericToCurrency==null)
				numericToCurrency=GetValues().ToDictionary(x=>x.NumericValue, y=>y);
			
			Currency currency;
			if (!numericToCurrency.TryGetValue(numericValue, out currency))
				throw new Exception("Unknown currency : " + numericValue);
				
			return currency;
		}
	
		public static Currency FromString(string shortName)
		{
			if (shortNameToCurrency==null)
				shortNameToCurrency=GetValues().ToDictionary(x=>x.ShortName, y=>y);
			
			Currency currency;
			if (!shortNameToCurrency.TryGetValue(shortName, out currency))
				throw new Exception("Unknown currency : " + shortName);
				
			return currency;
		}
	}
}

