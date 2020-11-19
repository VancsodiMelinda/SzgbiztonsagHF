using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace NinjaStore.Parser.Data
{
	[Serializable]
	public class ParserMetadata
	{
		public List<int> Frames;

		public ParserMetadata()
		{
			Frames = new List<int>();

		}

		public int ReturnSum()
		{
			return Frames.Sum();
		}

		// TODO Gergo: add properties for serializing parser metadata (see JSON file)
	}

	// TODO Gergo: create additional classes for serializing parser metadata (etc. Frame)
	public class Frame
	{

	}

}
