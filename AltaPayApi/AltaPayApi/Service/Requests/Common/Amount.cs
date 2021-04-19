using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AltaPay.Service
{
	public class Amount
	{
		public decimal Value { get; private set;}
		public Currency Currency { get; private set; }
		
		private Amount(decimal _value, Currency currency) {
			Value = _value; 
			Currency = currency;
		}
		
		public String GetAmountString() 
		{
			switch(Currency.Decimals) {
				case(0): return Value.ToString("0", Globalisation.AmountNumberFormat);
				case(1): return Value.ToString(".0", Globalisation.AmountNumberFormat); 
				case(2): return Value.ToString(".00", Globalisation.AmountNumberFormat); 
				case(3): return Value.ToString(".000", Globalisation.AmountNumberFormat); 
			};

			throw new Exception("Unsupported number of decimals : " + Currency.Decimals);
		}
		
		public override string ToString()
		{
			return String.Format("{0} {1}", GetAmountString(), Currency);
		}

		public static Amount Get(decimal _value, Currency currency) 
		{
			return new Amount(_value, currency);
		}
		
		public static Amount Get(long _value, Currency currency) 
		{
			return new Amount(_value, currency);
		}

		public static Amount Get(double _value, Currency currency) 
		{
			return new Amount((decimal)_value, currency);
		}

		public static Amount Get(string value, Currency currency)
		{

			if (String.IsNullOrEmpty(value))
			{
				return Get(0.0M, currency);

			} else
			{
				return Get(Decimal.Parse(value, Globalisation.DecimalCultureInfo), currency);
			}
				
		}

		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false:
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Amount return false:
			Amount am = obj as Amount;
			if ((System.Object)am == null)
			{
				return false;
			}

			// Return true if the fields match:
			// TODO it would be better to implement the method Equals in class Currency
			return (this.Value == am.Value) && (this.Currency.ToString().Equals(am.Currency.ToString()));
		}
        // Added due to the warning: 'Amount' overrides Object.Equals(object o) but does not override Object.GetHashCode()
		public override int GetHashCode()
        {
            return GetHashCode();
        }

	}
}

