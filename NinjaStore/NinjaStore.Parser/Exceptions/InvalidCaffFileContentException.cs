using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Parser.Exceptions
{
    [Serializable]
    public class InvalidCaffFileContentException : Exception
    {
        public InvalidCaffFileContentException()
        {

        }

        public InvalidCaffFileContentException(string name) : base(name)
        {

        }
    }
}