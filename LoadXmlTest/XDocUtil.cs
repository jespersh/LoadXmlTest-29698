using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace dk.nsi.seal
{
  public static class XDocUtil
  {
    public static void Save(this XDocument doc, Stream stream, SaveOptions so = SaveOptions.DisableFormatting)
    {
      using (XmlWriter writer = XmlWriter.Create(stream))
        doc.Save(writer);
    }

    public static XDocument Load(Stream stream, LoadOptions lo = LoadOptions.None)
    {
      using (XmlReader reader = XmlReader.Create(stream))
        return XDocument.Load(reader);
    }

    public static void Save(this XElement elm, Stream stream, SaveOptions so = SaveOptions.DisableFormatting)
    {
      using (XmlWriter writer = XmlWriter.Create(stream))
        elm.Save(writer);
    }

    public static void CopyTo(this Stream input, Stream output, SaveOptions so = SaveOptions.DisableFormatting)
    {
      byte[] buffer = new byte[32768];
      int count;
      while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
        output.Write(buffer, 0, count);
    }
  }
}