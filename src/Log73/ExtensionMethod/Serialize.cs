using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Log73.ExtensionMethod
{
    public static class SerializeExtensionMethods
    {
        /// <summary>
        /// Serializes an object to JSON using the <see cref="JsonSerializerSettings"/> specified in <see cref="ConsoleOptions.JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="obj">The object to serialize as JSON.</param>
        /// <returns>The object serialized as JSON.</returns>
        public static string SerializeAsJson(this object obj)
            => SerializeAsJson(obj, Console.Options.JsonSerializerSettings);
        /// <summary>
        /// Serializes an object to JSON using the specified <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="obj">The object to serialize as JSON.</param>
        /// <param name="serializerSettings">The <see cref="JsonSerializerSettings"/> to use when serializing the object.</param>
        /// <returns>The object serialized as JSON using the specified <see cref="JsonSerializerSettings"/>.</returns>
        public static string SerializeAsJson(this object obj, JsonSerializerSettings serializerSettings)
            => JsonConvert.SerializeObject(obj, serializerSettings);
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
        /// <summary>
        /// Serializes an object to YAML.
        /// </summary>
        /// <param name="obj">The object to serialize as YAML.</param>
        /// <returns>The YAML string of the serialized object.</returns>
        public static string SerializeAsYaml(this object obj)
        {
            var serializer = new YamlDotNet.Serialization.Serializer();
            return serializer.Serialize(obj);
        }
    }
}
