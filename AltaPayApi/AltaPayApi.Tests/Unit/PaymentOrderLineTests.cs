using System;
using AltaPay.Api.Tests;
using NUnit.Framework;

namespace AltaPay.Service.Tests.Unit
{
	public class PaymentOrderLineTests : BaseTest
	{
		private PaymentOrderLine orderline;
		
		[SetUp]
		public void Setup()
		{
			this.orderline = new PaymentOrderLine();
		}
		
		[Test]
		public void TaxPercent_and_TaxAmount_defaultsToMinValue()
		{
			Assert.AreEqual(double.MinValue, this.orderline.TaxPercent, "TaxPercent should have min value to signal that it has not been set");
			Assert.AreEqual(double.MinValue, this.orderline.TaxAmount, "TaxAmount should have min value to signal that it has not been set");
		}
		
		[Test]
		public void TaxPercent_canSet()
		{
			this.orderline.TaxPercent = 99d;
		}
		
		[Test]
		public void TaxAmount_canSet()
		{
			this.orderline.TaxAmount = 12d;
		}

	}
}

