using System;
using AltaPay.Api.Tests;
using NUnit.Framework;
using AltaPay.Service.Dto;

namespace AltaPay.Service.Tests.Unit
{
	public class MultiPaymentApiResultTests : BaseTest
	{
		[Test]
		public void HasAnyFailedPaymentActions_NoPaymentsAtAll()
		{
			var apiResponse = new APIResponse() {
				Header = new Header() {
					ErrorCode = 0
				},
				Body = new Body() {
					Actions = new AltaPay.Service.Dto.Action[] { }
				}
			};

			var multiResult = new MultiPaymentApiResult(apiResponse);

			Assert.AreEqual(false, multiResult.HasAnyFailedPaymentActions());
		}

		[Test]
		public void HasAnyFailedPaymentActions_OnlySuccessfulPayments()
		{
			var apiResponse = new APIResponse() {
				Header = new Header() {
					ErrorCode = 0
				},
				Body = new Body() {
					Actions = new AltaPay.Service.Dto.Action[] {
						new AltaPay.Service.Dto.Action() {
							Result = Result.Success.ToString()
						},
						new AltaPay.Service.Dto.Action() {
							Result = Result.Success.ToString()
						}
					}
				}
			};

			var multiResult = new MultiPaymentApiResult(apiResponse);

			Assert.AreEqual(false, multiResult.HasAnyFailedPaymentActions());
		}

		[Test]
		public void HasAnyFailedPaymentActions_OnlyFailedPayments()
		{
			var apiResponse = new APIResponse() {
				Header = new Header() {
					ErrorCode = 0
				},
				Body = new Body() {
					Actions = new AltaPay.Service.Dto.Action[] {
						new AltaPay.Service.Dto.Action() {
							Result = Result.Failed.ToString()
						},
						new AltaPay.Service.Dto.Action() {
							Result = Result.Error.ToString()
						}
					}
				}
			};

			var multiResult = new MultiPaymentApiResult(apiResponse);

			Assert.AreEqual(true, multiResult.HasAnyFailedPaymentActions());
		}

		[Test]
		public void HasAnyFailedPaymentActions_MixtureOfFailedAndSuccess()
		{
			var apiResponse = new APIResponse() {
				Header = new Header() {
					ErrorCode = 0
				},
				Body = new Body() {
					Actions = new AltaPay.Service.Dto.Action[] {
						new AltaPay.Service.Dto.Action() {
							Result = Result.Failed.ToString()
						},
						new AltaPay.Service.Dto.Action() {
							Result = Result.Error.ToString()
						},
						new AltaPay.Service.Dto.Action() {
							Result = Result.Success.ToString()
						}
					}
				}
			};

			var multiResult = new MultiPaymentApiResult(apiResponse);

			Assert.AreEqual(true, multiResult.HasAnyFailedPaymentActions());
		}
	}
}

