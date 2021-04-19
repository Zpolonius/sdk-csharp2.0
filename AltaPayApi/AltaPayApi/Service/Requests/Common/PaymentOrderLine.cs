using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaPay.Service
{
	public class PaymentOrderLine
	{
		protected double taxPercent = double.MinValue;
		protected double taxAmount = double.MinValue;
		
		
		public string Description { get; set; }
		public string ItemId { get; set; }
		public double Quantity { get; set; }
		public string UnitCode { get; set; }
		public double UnitPrice { get; set; }
		public double Discount { get; set; }
		public GoodsType GoodsType { get; set; }
		public string ImageUrl { get; set; }
		
		
		public double TaxPercent
		{
			get
			{
				return this.taxPercent;
			}
			
			set
			{
				this.taxPercent = value;
			}
		}
		
		public double TaxAmount
		{
			get
			{
				return this.taxAmount;
			}
			
			set
			{
				this.taxAmount = value;
			}
		}
	}
}
