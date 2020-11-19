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
		public int anim_count { get; set; }
		public Dictionary<string, int> date { get; set; }
		public string creator { get; set; }
		public IList<Frame> frames { get; set; }

		// TODO Gergo: add properties for serializing parser metadata (see JSON file)
	}

	// TODO Gergo: create additional classes for serializing parser metadata (etc. Frame)
	public class Frame
	{
		public int duration { get; set; }
		public Image image { get; set; }
	}

	public class Image
    {
		public int width { get; set; }
		public int height { get; set; }
		public string caption { get; set; }
		public string[] tags { get; set; }

	}

}
