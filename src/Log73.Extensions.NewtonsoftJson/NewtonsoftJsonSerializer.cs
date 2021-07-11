using Newtonsoft.Json;

namespace Log73.Extensions.NewtonsoftJson
{
    /// <summary>
    /// Serializes objects as JSON using Newtonsoft.Json.
    /// </summary>
    public class NewtonsoftJsonSerializer : IObjectSerializer
    {
        /// <summary>
        /// The <see cref="JsonSerializerSettings"/> to use when serializing.
        /// </summary>
        public JsonSerializerSettings Settings;

        public NewtonsoftJsonSerializer() : this(new()
        {
            Formatting = Formatting.Indented
        })
        {
        }

        public NewtonsoftJsonSerializer(JsonSerializerSettings settings) => Settings = settings;

        /// <summary>
        /// Serialize the object as JSON using <see cref="Settings"/>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The serialized object.</returns>
        public string Serialize(object obj)
            => JsonConvert.SerializeObject(obj, Settings);
    }
}