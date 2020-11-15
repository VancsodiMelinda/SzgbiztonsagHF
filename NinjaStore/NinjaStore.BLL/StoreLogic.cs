using NinjaStore.DAL;
using NinjaStore.Parser.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.BLL
{
	public class StoreLogic : IStoreLogic
	{
		private StoreContext _storeContext;
		private IParserService _parserService;

		public StoreLogic(StoreContext storeContext, IParserService parserService)
		{
			_storeContext = storeContext;
			_parserService = parserService;
		}
	}
}
