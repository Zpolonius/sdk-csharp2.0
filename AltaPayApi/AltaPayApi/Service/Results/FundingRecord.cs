using System;
using System.Text.RegularExpressions;
using System.Globalization;
using LumenWorks.Framework.IO.Csv;

namespace AltaPay.Service
{

	public class FundingRecord
	{

		private const int funding_date = 0;
		private const int record_type = 1;
		private const int id = 2;
		private const int reconciliation_identifier = 3;
		private const int payment_id = 4;
		private const int order_id = 5;
		private const int terminal = 6;
		private const int shop = 7;
		private const int transaction_currency = 8;
		private const int transaction_amount = 9;
		private const int exchange_rate = 10;
		private const int settlement_currency = 11;
		private const int settlement_amount = 12;
		private const int fixed_fee = 13;
		private const int fixed_fee_vat = 14;
		private const int rate_based_fee = 15;
		private const int rate_based_fee_vat = 16;

		public DateTime FundingDate { get; set; }
		public string RecordType { get; set; }
		public string Id { get; set; }
		public string ReconciliationIdentifier { get; set; }
		public string PaymentId { get; set; }
		public string OrderId { get; set; }
		public string Terminal { get; set; }
		public string Shop { get; set; }

		public Amount PaymentAmount { get; set; }

		public Decimal ExchangeRate { get; set; }

		public Amount FundingAmount { get; set; }

		public Amount FixedFeeAmount { get; set; }
		public Amount FixedFeeVatAmount { get; set; }
		public Amount RateBasedFeeAmount { get; set; }
		public Amount RateBasedFeeVatAmount { get; set; }

		public FundingRecord(CsvReader fields)
		{

			if (fields.FieldCount != 17) // fields: a line from a CSV file
			{
				throw new System.ArgumentException("The number of fields in the line should be equal to 17, but was " + fields.FieldCount);
			}
		
			Currency settCurr = Currency.FromString(fields[settlement_currency]);
			Currency transCurr = Currency.FromString(fields[transaction_currency]);

			this.FundingDate = toDate(fields[funding_date]);

			this.RecordType = fields[record_type];
			this.Id = fields[id];
			this.ReconciliationIdentifier = fields[reconciliation_identifier];
			this.PaymentId = fields[payment_id];
			this.OrderId = fields[order_id];
			this.Terminal = fields[terminal];
			this.Shop = fields[shop];

			// returns a 1.0 decimal if there is no exchange rate:
			this.ExchangeRate = fields[exchange_rate].Length == 0 ? 1.0M : Decimal.Parse(fields[exchange_rate], Globalisation.DecimalCultureInfo);

			this.PaymentAmount = Amount.Get(fields[transaction_amount], transCurr);
			this.FundingAmount = Amount.Get(fields[settlement_amount], settCurr);

			this.FixedFeeAmount = Amount.Get(fields[fixed_fee], settCurr);
			this.FixedFeeVatAmount = Amount.Get(fields[fixed_fee_vat], settCurr);
			this.RateBasedFeeAmount = Amount.Get(fields[rate_based_fee], settCurr);
			this.RateBasedFeeVatAmount = Amount.Get(fields[rate_based_fee_vat], settCurr);

		}


		private DateTime toDate (string date) {

			DateTime dt;

			//Regex regex = new Regex("[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}:[0-9]{2}");
			//Match match = regex.Match(date);

			if (DateTime.TryParseExact(date, "yyyy-MM-dd hh:mm:ss", Globalisation.DateTimeCultureInfo, Globalisation.DateTimeStyle, out dt))
			{
				return dt;
			
			} else if (DateTime.TryParseExact(date, "yyyy-MM-dd", Globalisation.DateTimeCultureInfo, Globalisation.DateTimeStyle, out dt))
			{
				return dt;
			
			} else
			{
				throw new System.ArgumentException("Incorrect format for the following date: " + date);
			}

		}


	}
}

