using System;

namespace AltaPay.Service.Loggers
{
	public class BlackholeAltaPayLogger : IAltaPayLogger
	{
		public void Trace (string message)
		{
			// blackhole
		}

		public void Trace (string format, params object[] args)
		{
			// blackhole
		}

		public void Debug (string message)
		{
			// blackhole
		}

		public void Debug (string format, params object[] args)
		{
			// blackhole
		}

		public void Information (string message)
		{
			// blackhole
		}

		public void Information (string format, params object[] args)
		{
			// blackhole
		}
		
		public void Error (string message)
		{
			// blackhole
		}

		public void Error (string format, params object[] args)
		{
			// blackhole
		}

		public AltaPayLogLevel LogLevel { get; set; }
		
		public string WhereDoYouLogTo()
		{
			return String.Format("No where (give {0} an {1} to change this)", typeof(MerchantApi).Name, typeof(IAltaPayLogger).Name);
		}
	}
}

