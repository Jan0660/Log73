using Newtonsoft.Json;

namespace Log73.Extensions.NewtonsoftJson
{
    public class NewtonsoftJsonSerializer : IObjectSerializer
    {
        public JsonSerializerSettings Settings;

        public NewtonsoftJsonSerializer() : this(new()
        {
            Formatting = Formatting.Indented
        })
        {
        }

        public NewtonsoftJsonSerializer(JsonSerializerSettings settings) => Settings = settings;

        public string Serialize(object obj)
            => JsonConvert.SerializeObject(obj, Settings);
    }
}