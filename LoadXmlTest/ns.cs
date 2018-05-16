using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace dk.nsi.seal
{
    internal class ns
    {
        public static XNamespace xsoap = (XNamespace)"http://schemas.xmlsoap.org/soap/envelope/";
        public static XNamespace xwsu = (XNamespace)"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        public static XNamespace xwsa = (XNamespace)"http://www.w3.org/2005/08/addressing";
        public static XNamespace xwsa04 = (XNamespace)"http://schemas.xmlsoap.org/ws/2004/08/addressing";
        public static XNamespace xds = (XNamespace)"http://www.w3.org/2000/09/xmldsig#";
        public static XNamespace xwsse = (XNamespace)"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        public static XNamespace xwsa2 = (XNamespace)"http://schemas.microsoft.com/ws/2005/05/addressing/none";
        public static XNamespace xdgws = (XNamespace)"http://www.medcom.dk/dgws/2006/04/dgws-1.0.xsd";
        public static XNamespace xsaml = (XNamespace)"urn:oasis:names:tc:SAML:2.0:assertion";
        public static XNamespace xsosi = (XNamespace)"http://sosi.dk/gw/2007.09.01";
        public static XNamespace xwst = (XNamespace)"http://schemas.xmlsoap.org/ws/2005/02/trust";
        public static XNamespace xtrust = (XNamespace)"http://docs.oasis-open.org/ws-sx/ws-trust/200512";
        public static Dictionary<string, string> alias = new Dictionary<string, string>()
    {
      {
        "http://schemas.xmlsoap.org/soap/envelope/",
        nameof (soap)
      },
      {
        "http://www.w3.org/2005/08/addressing",
        nameof (wsa)
      },
      {
        "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd",
        nameof (wsu)
      },
      {
        "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd",
        nameof (wsse)
      },
      {
        "http://www.w3.org/2000/09/xmldsig#",
        nameof (ds)
      },
      {
        "http://docs.oasis-open.org/ws-sx/ws-trust/200512",
        nameof (trust)
      },
      {
        "http://docs.oasis-open.org/ws-sx/ws-trust/200802",
        nameof (tr)
      },
      {
        "urn:oasis:names:tc:SAML:2.0:assertion",
        nameof (saml)
      },
      {
        "http://www.medcom.dk/dgws/2006/04/dgws-1.0.xsd",
        nameof (dgws)
      }
    };
        public const string wsa = "http://www.w3.org/2005/08/addressing";
        public const string wsa04 = "http://schemas.xmlsoap.org/ws/2004/08/addressing";
        public const string wsa2 = "http://schemas.microsoft.com/ws/2005/05/addressing/none";
        public const string wsu = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        public const string wsse = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        public const string ds = "http://www.w3.org/2000/09/xmldsig#";
        public const string soap = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string trust = "http://docs.oasis-open.org/ws-sx/ws-trust/200512";
        public const string tr = "http://docs.oasis-open.org/ws-sx/ws-trust/200802";
        public const string saml = "urn:oasis:names:tc:SAML:2.0:assertion";
        public const string HealthContextAssertion = "HealthContextAssertion";
        public const string DGWSAssertion = "DGWSAssertion";
        public const string dgws = "http://www.medcom.dk/dgws/2006/04/dgws-1.0.xsd";
        public const string sosi = "http://sosi.dk/gw/2007.09.01";
        public const string wst = "http://schemas.xmlsoap.org/ws/2005/02/trust";

        internal static void SetMissingNamespaces(XDocument doc)
        {
            HashSet<string> docnss = new HashSet<string>(doc.Root.Attributes().Where<XAttribute>((Func<XAttribute, bool>)(a => a.Name.Namespace == XNamespace.Xmlns)).Select<XAttribute, string>((Func<XAttribute, string>)(a => a.Value)));
            foreach (KeyValuePair<string, string> keyValuePair in ns.alias.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(kv => !docnss.Contains(kv.Key))))
                doc.Root.Add((object)new XAttribute(XNamespace.Xmlns + keyValuePair.Value, (object)keyValuePair.Key));
        }

        internal static XmlNamespaceManager MakeNsManager(XmlNameTable nt)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nt);
            foreach (KeyValuePair<string, string> alia in ns.alias)
                namespaceManager.AddNamespace(alia.Value, alia.Key);
            return namespaceManager;
        }
    }
}
