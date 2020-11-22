using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Parser.Exceptions
{
    [Serializable]
    class InternalParserException : Exception
    {

        public InternalParserException()
        {

        }

        public InternalParserException(string name) : base(name)
        {

        }
    }
}
