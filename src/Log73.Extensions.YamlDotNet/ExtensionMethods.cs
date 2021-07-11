using Log73.Extensible;
using Log73.Extensions.YamlDotNet;
using YamlDotNet.Serialization;

namespace Log73.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Log an object as YAML.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        /// <param name="obj">The object.</param>
        public static void Yaml(this ConsoleLogObject ext, object obj)
            => Yaml(ext, MessageTypes.Info, obj);

        /// <summary>
        /// Log an object as YAML using the specified <see cref="MessageType"/>.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        /// <param name="msgType">The <see cref="MessageType"/> to log as.</param>
        /// <param name="obj">The object.</param>
        public static void Yaml(this ConsoleLogObject ext, MessageType msgType, object obj)
        {
            if (Console.Options.ObjectSerializer is YamlDotNetSerializer serializer)
                Console.Log(msgType, serializer.Serialize(obj));
            else
                Console.Log(msgType, new YamlDotNetSerializer().Serialize(obj));
        }

        /// <summary>
        /// Use <see cref="Log73.Extensions.YamlDotNet.YamlDotNetSerializer"/> for object serialization.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        public static void UseYamlDotNet(this ConsoleConfigureObject ext)
            => UseYamlDotNet(ext, new());

        /// <summary>
        /// Use <see cref="Log73.Extensions.YamlDotNet.YamlDotNetSerializer"/> with the specified <see cref="Serializer"/> for object serialization.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        /// <param name="serializer">The <see cref="Serializer"/> to use.</param>
        public static void UseYamlDotNet(this ConsoleConfigureObject ext,
            Serializer serializer)
            => Console.Options.ObjectSerializer = new YamlDotNetSerializer(serializer);
    }
}