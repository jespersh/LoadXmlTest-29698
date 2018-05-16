using System.IO;
using System.Xml;

namespace dk.nsi.seal
{
    // Switch between implementations below
    internal class SealSignedXml : 
        //System.Security.Cryptography.Xml.SignedXml // Microsoft's dotnet framework implement
        LoadXmlTest.SignedXml // Implemention copied from dotnetcore
    {
        public XmlDocument xml { get; private set; }
        
        public SealSignedXml(Stream stream)
            : this(SealSignedXml.streamToXml(stream))
        {
        }

        public SealSignedXml(XmlDocument xml)
            : base(xml)
        {
            this.xml = xml;
        }

        private static XmlDocument streamToXml(Stream stream)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(stream);
            return xmlDocument;
        }

        public bool CheckAssertionSignature()
        {
            ns.MakeNsManager(this.xml.NameTable);
            XmlElement xmlElement =
                (this.xml.DocumentElement.LocalName == "Assertion"
                    ? this.xml.DocumentElement
                    : this.xml.GetElementsByTagName("Assertion", "urn:oasis:names:tc:SAML:2.0:assertion")[0] as
                        XmlElement)
                .GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#")[0] as XmlElement;
            if (xmlElement == null)
                return false;
            this.LoadXml(xmlElement);
            return true;
            //return this.CheckSignature(this.KeyInfo.Cast<KeyInfoX509Data>().Select<KeyInfoX509Data, X509Certificate2>((Func<KeyInfoX509Data, X509Certificate2>) (d => d.Certificates[0] as X509Certificate2)).Where<X509Certificate2>((Func<X509Certificate2, bool>) (c => c != null)).FirstOrDefault<X509Certificate2>(), true);
        }

        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            return doc.SelectSingleNode("//*[@wsu:Id=\"" + id + "\"]", ns.MakeNsManager(doc.NameTable)) as XmlElement ??
                   base.GetIdElement(doc, id);
        }
    }
}