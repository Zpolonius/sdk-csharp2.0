using System;
using System.Linq;
using AltaPay;
using NUnit.Framework;
using AltaPay.Service;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Unit
{
	[TestFixture]
	public class CurrencyTests : BaseTest
	{
		[Test]
		public void Values()
		{
			Assert.IsTrue(Currency.GetValues().Contains(Currency.AFN));	
			Assert.IsTrue(Currency.GetValues().Contains(Currency.ARS));	
			Assert.IsTrue(Currency.GetValues().Contains(Currency.BZD));	
		}
	
		[Test]
		public void ShortName()
		{
			Assert.AreEqual(Currency.AFN.ShortName, "AFN");	
			Assert.AreEqual(Currency.ILS.ShortName, "ILS");	
			Assert.AreEqual(Currency.NZD.ShortName, "NZD");	
		}
		
		[Test]
		public void Name()
		{
			Assert.AreEqual(Currency.AFN.Name, "Afghani");	
			Assert.AreEqual(Currency.ILS.Name, "New Israeli Sheqel");	
			Assert.AreEqual(Currency.NZD.Name, "New Zealand Dollar");	
		}
		
		[Test]
		public void Decimal()
		{
			Assert.AreEqual(Currency.AFN.Decimals, 2);	
			Assert.AreEqual(Currency.ISK.Decimals, 0);	
			Assert.AreEqual(Currency.BHD.Decimals, 3);	
		}

		[Test]
		public void NumericValue()
		{
			Assert.AreEqual(Currency.AFN.NumericValue, 971);	
			Assert.AreEqual(Currency.ISK.NumericValue, 352);	
			Assert.AreEqual(Currency.BHD.NumericValue, 48);	
		}

		[Test]
		public void ToStringTest()
		{
			Assert.AreEqual(Currency.AFN.ShortName, Currency.AFN.ToString());
			Currency.GetValues().ToList().ForEach(x=>Assert.AreEqual(x.ShortName, x.ToString()));			
		}

		
		[Test]
		public void FromNumeric()
		{
			var dkk = Currency.DKK;
			Assert.AreSame(dkk, Currency.FromNumeric(dkk.NumericValue));
			Currency.GetValues().ToList().ForEach(x=>Assert.AreSame(x, Currency.FromNumeric(x.NumericValue)));			
		}

		
		[Test]
		public void GetNumericString()
		{
			Assert.AreEqual("208", Currency.DKK.GetNumericString());
			Assert.AreEqual("032", Currency.ARS.GetNumericString());
			Assert.AreEqual("008", Currency.ALL.GetNumericString());
		}

		[Test]
		public void FromString()
		{
			Assert.AreSame(Currency.DKK, Currency.FromString("DKK"));
			Currency.GetValues().ToList().ForEach(x=>Assert.AreSame(x, Currency.FromString(x.ShortName)));			
		}
		
	
	}
}


