using System;

namespace AltaPay
{
	public interface IAltaPayLogger
	{
		AltaPayLogLevel LogLevel { get; set; }
		
		void Trace(string message);
		
		void Trace(string format, params object[] args);
		
		void Debug(string message);
		
		void Debug(string format, params object[] args);
		
		void Information(string message);
		
		void Information(string format, params object[] args);
		
		void Error(string message);
		
		void Error(string format, params object[] args);
		
		string WhereDoYouLogTo();
	}
}
