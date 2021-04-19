using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;
using System.Net;
using LumenWorks.Framework.IO.Csv;

namespace AltaPay.Service
{
	public class FundingContentResult
	{

		private const char csv_delimiter = ';';

		//public virtual String FundingContent { get; private set; }

		private String url;
		private NetworkCredential networkCredential;
		private String fundingContent;

		public FundingContentResult(String url, NetworkCredential networkCredential)
		{
			this.url = url;
			this.networkCredential = networkCredential;
		}

		public virtual String GetFundingContent() 
		{
			if (this.fundingContent == null)
			{
				WebRequest request = WebRequest.Create(url);
				request.Credentials = networkCredential;
				WebResponse response = request.GetResponse();
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				fundingContent = reader.ReadToEnd();
				reader.Close();
				response.Close();
			}

			return this.fundingContent;
		}

		public List<FundingRecord> GetFundingRecordList ()
		{
			List<FundingRecord> records = new List<FundingRecord>();

			using (StreamReader reader = GenerateStreamFromString(GetFundingContent()))
			using (CsvReader csv = new CsvReader(reader, true, csv_delimiter))
			{
				while (csv.ReadNextRecord())
				{
					records.Add(new FundingRecord(csv));
				}
			}

			return records;
		}

		private StreamReader GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return new StreamReader(stream);
		}
			
	}
}

