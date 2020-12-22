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
        public static string SerializeAsJson(this object obj)
            => SerializeAsJson(obj, Console.Options.JsonSerializerSettings);
        public static string SerializeAsJson(this object obj, JsonSerializerSettings serializerSettings)
            => JsonConvert.SerializeObject(obj, serializerSettings);
        public static string SerializeAsXml(this object obj)
            => obj.SerializeAsXml(Console.Options.XmlWriterSettings);
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
        public static string SerializeAsYaml(this object obj)
        {
            var serializer = new YamlDotNet.Serialization.Serializer();
            return serializer.Serialize(obj);
        }
    }
}
