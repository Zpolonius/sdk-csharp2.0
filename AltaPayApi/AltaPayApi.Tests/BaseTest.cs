using System;
using NUnit.Framework;
using System.Globalization;

namespace AltaPay.Api.Tests
{
	public abstract class BaseTest
	{
		[SetUp]
		public void BaseSetup()
		{
			/**
			 * Setting default culture to a culture with a format that does not match
			 * what the gateway requires.
			 * This is to ensure that we handle culture/format differences in our code.
			 */
			SetCultureTo("da-DK");
		}
		
		/// <summary>
		/// Sets the culture to the given culture name
		/// </summary>
		/// <param name="cultureName">
		/// Something like 'en-US', 'da-DK'
		/// </param>
		protected void SetCultureTo(string cultureName)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		}
		
		protected void WaitForDataToFlowIntoReporting()
		{
			this.WaitForDataToFlowIntoReporting(3000);
		}
		
		protected void WaitForDataToFlowIntoReporting(int milliseconds)
		{
			System.Threading.Thread.Sleep(milliseconds);
		}
	}
}

