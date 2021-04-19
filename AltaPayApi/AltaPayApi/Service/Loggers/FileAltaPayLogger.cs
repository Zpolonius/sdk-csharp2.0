using System;

namespace AltaPay.Service.Loggers
{
	public class FileAltaPayLogger : IAltaPayLogger
	{
		private string filename;
		
		public FileAltaPayLogger(string filename)
		{
			this.filename = filename;
		}
		
		private bool DoLog(AltaPayLogLevel incomingLevel)
		{
			if (LogLevel == AltaPayLogLevel.Off)
			{
				return false;
			}
			
			return (int)incomingLevel >= (int)LogLevel;
		}
		
		#region IAltaPayLogger implementation
		private void Write(AltaPayLogLevel type, string message)
		{
			if (DoLog(type))
			{
				System.IO.File.AppendAllText(filename, String.Format("[{0}] {1}{2}", type, message, Environment.NewLine));
			}
		}
		
		public void Trace (string message)
		{
			Write(AltaPayLogLevel.Trace, message);
		}

		public void Trace (string format, params object[] args)
		{
			Trace(String.Format(format, args));
		}

		public void Debug (string message)
		{
			Write(AltaPayLogLevel.Debug, message);
		}

		public void Debug (string format, params object[] args)
		{
			Debug(String.Format(format, args));
		}

		public void Information (string message)
		{
			Write(AltaPayLogLevel.Information, message);
		}

		public void Information (string format, params object[] args)
		{
			Information(String.Format(format, args));
		}
		
		public void Error (string message)
		{
			Write(AltaPayLogLevel.Error, message);
		}

		public void Error (string format, params object[] args)
		{
			Error(String.Format(format, args));
		}

		public AltaPayLogLevel LogLevel { get; set; }

		public string WhereDoYouLogTo()
		{
			return filename;
		}
		#endregion
	}
}

