using System.IO;
using dk.nsi.seal;

namespace LoadXmlTest
{
    class Program
    {

        static void Main(string[] args)
        {
            /**
             * We load SignIn_Xml.xml with a "<signature id=.../>" and attempt to check the signature
             * Open SealSignedXml to switch between copied/test dotnetcore impletation and actual framework
             * Open SignIn_Xml.xml and find <ds:Signature id="OCESSignature"> and correct id to Id as test
             *
             * Note that the files have been reduced to minimum to showcase the error in question.
             *
             * The Exception will be thrown by throw new CryptographicException("Hej2", "Signature"); in Signature.cs
             *
             *
             * See https://github.com/dotnet/corefx/issues/29698 for additional comments (if any)
             */
            using (FileStream fileStream = new FileStream("SignIn_Xml.xml", FileMode.Open))
            {
                SealSignedXml sealSignedXml = new SealSignedXml(fileStream);
                sealSignedXml.CheckAssertionSignature();
            }
        }
    }
}
