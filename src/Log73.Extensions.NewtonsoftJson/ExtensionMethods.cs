using Log73.Extensible;
using Log73.Extensions.NewtonsoftJson;
using Newtonsoft.Json;

namespace Log73.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Log an object as JSON.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        /// <param name="obj">The object.</param>
        public static void Json(this ConsoleLogObject ext, object obj)
            => Json(ext, MessageTypes.Info, obj);

        /// <summary>
        /// Log an object as JSON using the specified <see cref="MessageType"/>.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        /// <param name="msgType">The <see cref="MessageType"/> to log as.</param>
        /// <param name="obj">The object.</param>
        public static void Json(this ConsoleLogObject ext, MessageType msgType, object obj)
        {
            if(Console.Options.ObjectSerializer is NewtonsoftJsonSerializer serializer)
                Console.Log(msgType, serializer.Serialize(obj));
            else
                Console.Log(msgType, new NewtonsoftJsonSerializer().Serialize(obj));
        }

        /// <summary>
        /// Use <see cref="Log73.Extensions.NewtonsoftJson.NewtonsoftJsonSerializer"/> for object serialization.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        public static void UseNewtonsoftJson(this ConsoleConfigureObject ext)
            => UseNewtonsoftJson(ext, new());

        /// <summary>
        /// Use <see cref="Log73.Extensions.NewtonsoftJson.NewtonsoftJsonSerializer"/> with the specified <see cref="JsonSerializerSettings"/> for object serialization.
        /// </summary>
        /// <param name="ext">Extension method object.</param>
        /// <param name="jsonSerializerSettings">The <see cref="JsonSerializerSettings"/> to use when serializing objects.</param>
        public static void UseNewtonsoftJson(this ConsoleConfigureObject ext,
            JsonSerializerSettings jsonSerializerSettings)
            => Console.Options.ObjectSerializer = new NewtonsoftJsonSerializer(jsonSerializerSettings);
    }
}