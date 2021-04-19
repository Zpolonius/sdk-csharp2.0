using System;
using System.Collections.Generic;
using AltaPay.Api.Tests;
using NUnit.Framework;

namespace AltaPay.Service.Tests.Unit
{
	[TestFixture]
	public class CustomerInfoTests : BaseTest
	{
		private CustomerInfo _customerInfo;
		private Dictionary<string, Object> _parameters;

		[SetUp]
		public void Setup ()
		{
			_customerInfo = new CustomerInfo ();
			_parameters = new Dictionary<string, object> ();
		}

		[Test]
		public void BirthdayProvided()
		{
			_customerInfo.BirthDate = new DateTime (2020, 04, 20);
			_parameters = _customerInfo.AddToDictionary(_parameters);

			if (!_parameters.TryGetValue ("birthdate", out object birthdate)) {
				// the key isn't in the dictionary.
				Assert.Fail ();
			}
			Assert.AreEqual ("2020-04-20", birthdate);
		}

		[Test]
		public void BirthdayOmitted ()
		{
			_parameters = _customerInfo.AddToDictionary (_parameters);

			if (!_parameters.TryGetValue ("birthdate", out object birthdate)) {
				// the key isn't in the dictionary.
				Assert.Pass();
			}
			Assert.Fail();
		}
	}
}
