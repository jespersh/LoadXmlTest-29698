using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LoadXmlTest
{
    // Heavily reduced dotnetcore implementation from
    // https://github.com/dotnet/corefx/blob/master/src/System.Security.Cryptography.Xml/src/System/Security/Cryptography/Xml/Utils.cs#L21
    class Utils
    {

        internal static string GetAttribute(XmlElement element, string localName, string namespaceURI)
        {
            string s = (element.HasAttribute(localName) ? element.GetAttribute(localName) : null);
            if (s == null && element.HasAttribute(localName, namespaceURI))
                s = element.GetAttribute(localName, namespaceURI);
            return s;
        }

        internal static bool VerifyAttributes(XmlElement element, string expectedAttrName)
        {
            return VerifyAttributes(element, expectedAttrName == null ? null : new string[] { expectedAttrName });
        }

        internal static bool VerifyAttributes(XmlElement element, string[] expectedAttrNames)
        {
            foreach (XmlAttribute attr in element.Attributes)
            {
                // There are a few Xml Special Attributes that are always allowed on any node. Make sure we allow those here.
                bool attrIsAllowed = attr.Name == "xmlns" || attr.Name.StartsWith("xmlns:") || attr.Name == "xml:space" || attr.Name == "xml:lang" || attr.Name == "xml:base";
                int expectedInd = 0;
                while (!attrIsAllowed && expectedAttrNames != null && expectedInd < expectedAttrNames.Length)
                {
                    attrIsAllowed = attr.Name == expectedAttrNames[expectedInd];
                    expectedInd++;
                }
                if (!attrIsAllowed)
                    return false;
            }
            return true;
        }


        internal static string DiscardWhiteSpaces(string inputBuffer)
        {
            return DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
        }


        internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
        {
            int i, iCount = 0;
            for (i = 0; i < inputCount; i++)
                if (Char.IsWhiteSpace(inputBuffer[inputOffset + i])) iCount++;
            char[] rgbOut = new char[inputCount - iCount];
            iCount = 0;
            for (i = 0; i < inputCount; i++)
                if (!Char.IsWhiteSpace(inputBuffer[inputOffset + i]))
                {
                    rgbOut[iCount++] = inputBuffer[inputOffset + i];
                }
            return new string(rgbOut);
        }
    }
}
