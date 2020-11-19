using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Parser.Exceptions
{
    [Serializable]
    class InvalidCaffFileContentException : Exception
    {
        public InvalidCaffFileContentException()
        {

        }

        public InvalidCaffFileContentException(string name)
            : base(String.Format("UNKNOWN_ERROR: {0}", name))
        {

        }
    }
}