using NinjaStore.Parser.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStore.Parser.Services
{
	public interface IParserService
	{
		Task<ParserResult> ParseAsync(string fileId, byte[] content);
	}
}
