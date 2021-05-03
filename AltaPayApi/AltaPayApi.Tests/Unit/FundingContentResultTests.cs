using System;
using AltaPay.Api.Tests;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AltaPay.Service.Tests.Unit
{
	public class FundingContentResultTests : BaseTest
	{


		[Test]
		public void TestFundingFileWithEmptyFields()
		{

			Amount amount = Amount.Get(0.0M, Currency.EUR);

			Mock<FundingContentResult> mock = new Mock<FundingContentResult>(null, null);

			mock.Setup(t => t.GetFundingContent()).Returns(File.ReadAllText(@"AltaPayApi/AltaPayApi.Tests/Unit/txt/Funding1.csv"));

			List<FundingRecord> list = mock.Object.GetFundingRecordList();

			FundingRecord record = list.ElementAt(0);

			Assert.AreEqual(new DateTime(2010, 12, 24), record.FundingDate);
			Assert.AreEqual("fee", record.RecordType);
			Assert.AreEqual("Monthly fee", record.Id);
			Assert.AreEqual("", record.ReconciliationIdentifier);
			Assert.AreEqual("", record.PaymentId);
			Assert.AreEqual("", record.OrderId);
			Assert.AreEqual("", record.Terminal);
			Assert.AreEqual("AltaPay Functional Test Shop", record.Shop);

			Assert.AreEqual(amount, record.PaymentAmount);
			Assert.AreEqual(1.0M, record.ExchangeRate);
			Assert.AreEqual(amount, record.FundingAmount);
			Assert.AreEqual(amount, record.FixedFeeAmount);
			Assert.AreEqual(amount, record.FixedFeeVatAmount);
			Assert.AreEqual(amount, record.RateBasedFeeAmount);
			Assert.AreEqual(amount, record.RateBasedFeeVatAmount);

			record = list.ElementAt(1);

			Assert.AreEqual(new DateTime(2010, 12, 24), record.FundingDate);
			Assert.AreEqual("payment", record.RecordType);
			Assert.AreEqual("FunctionalTestContractID-record1", record.Id);
			Assert.AreEqual("", record.ReconciliationIdentifier);
			Assert.AreEqual("", record.PaymentId);
			Assert.AreEqual("", record.OrderId);
			Assert.AreEqual("", record.Terminal);
			Assert.AreEqual("AltaPay Functional Test Shop", record.Shop);

			Assert.AreEqual(amount, record.PaymentAmount);
			Assert.AreEqual(1.0M, record.ExchangeRate);
			Assert.AreEqual(amount, record.FundingAmount);
			Assert.AreEqual(amount, record.FixedFeeAmount);
			Assert.AreEqual(amount, record.FixedFeeVatAmount);
			Assert.AreEqual(amount, record.RateBasedFeeAmount);
			Assert.AreEqual(amount, record.RateBasedFeeVatAmount);
		}

		[Test]
		public void TestFundingFileWithNonEmptyFields()
		{
			//Amount amount = Amount.Get(0.0M, Currency.EUR);

			Mock<FundingContentResult> mock = new Mock<FundingContentResult>(null, null);

			mock.Setup(t => t.GetFundingContent()).Returns(File.ReadAllText(@"AltaPayApi/AltaPayApi.Tests/Unit/txt/Funding42221.csv"));

			List<FundingRecord> list = mock.Object.GetFundingRecordList();

			FundingRecord record = list.ElementAt(0);

			Assert.AreEqual(new DateTime(2016, 5, 27), record.FundingDate);
			Assert.AreEqual("payment", record.RecordType);
			Assert.AreEqual("385a159f-dc8a-4f25-926f-b57bb985a875", record.Id);
			Assert.AreEqual("6800", record.ReconciliationIdentifier);
			Assert.AreEqual("10029471", record.PaymentId);
			Assert.AreEqual("SLS00000925", record.OrderId);
			Assert.AreEqual("Lars Bertelsen Test Terminal", record.Terminal);
			Assert.AreEqual("Lars Bertelsen", record.Shop);

			Assert.AreEqual(Amount.Get(86.56M, Currency.USD), record.PaymentAmount);
			Assert.AreEqual(73.648M, record.ExchangeRate);
			Assert.AreEqual(Amount.Get(63.75M, Currency.EUR), record.FundingAmount);
			Assert.AreEqual(Amount.Get(-0.25M, Currency.EUR), record.FixedFeeAmount);
			Assert.AreEqual(Amount.Get(0M, Currency.EUR), record.FixedFeeVatAmount);
			Assert.AreEqual(Amount.Get(-0.86M, Currency.EUR), record.RateBasedFeeAmount);
			Assert.AreEqual(Amount.Get(0M, Currency.EUR), record.RateBasedFeeVatAmount);

		}

	}
}

