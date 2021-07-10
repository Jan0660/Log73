using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Log73.ExtensionMethod
{
    public static class SerializeExtensionMethods
    {
        /// <summary>
        /// Serializes an object to XML using the <see cref="XmlWriterSettings"/> specified in <see cref="ConsoleOptions.XmlWriterSettings"/>.
        /// </summary>
        /// <param name="obj">The object to serialize as XML.</param>
        /// <returns><paramref name="obj"/> serialized as XML.</returns>
        public static string SerializeAsXml(this object obj)
            => obj.SerializeAsXml(Console.Options.XmlWriterSettings);
        /// <summary>
        /// Serializes an object to XML using the specified <see cref="XmlWriterSettings"/>>.
        /// </summary>
        /// <param name="obj">The object to serialize as XML.</param>
        /// <param name="xmlWriterSettings">The <see cref="XmlWriterSettings"/> to use when serializing the object.</param>
        /// <returns><paramref name="obj"/> serialized as XML.</returns>
        public static string SerializeAsXml(this object obj, XmlWriterSettings xmlWriterSettings)
        {
            // stolen from https://stackoverflow.com/a/16853268/12520276
            string xml = "";
            var xmlserializer = new XmlSerializer(obj.GetType());
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter, xmlWriterSettings))
            {
                xmlserializer.Serialize(writer, obj);
                xml = stringWriter.ToString();
            }
            return xml;
        }
    }
}
