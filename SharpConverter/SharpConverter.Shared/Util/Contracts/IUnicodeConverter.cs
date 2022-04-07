using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util.Contracts
{
    internal interface IUnicodeConverter
    {
        public char[,] UnicodeArray();
        public string BinaryToString(string binaryValue);
        public string StringToBinary(string stringValue);
    }
}
