using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util
{
    public static class Tools
    {
        public static string SplitDecimal(bool postPoint, string input)
        {
            char[] characters = input.ToCharArray();
            int pointIndex = 0;
            string returnValue = "";

            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == '.')
                    pointIndex = i;
            }

            if (!postPoint)
            {
                char[] returnValueArray = new char[pointIndex];

                for (int i = 0; i < pointIndex; i++)
                    returnValueArray[i] = characters[i];
                foreach (var c in returnValueArray)
                {
                    returnValue += c;
                }
            }
            else
            {
                char[] returnValueArray = new char[characters.Length];
                for (int i = pointIndex + 1; i < characters.Length; i++)
                    returnValueArray[i] = characters[i];
                foreach (var c in returnValueArray)
                {
                    returnValue += c;
                }
            }

            return returnValue;
        }
    }
}
