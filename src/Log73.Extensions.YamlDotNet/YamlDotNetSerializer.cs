using YamlDotNet.Serialization;

namespace Log73.Extensions.YamlDotNet
{
    public class YamlDotNetSerializer : IObjectSerializer
    {
        public Serializer Serializer { get; set; }

        public YamlDotNetSerializer() : this(new())
        {
        }

        public YamlDotNetSerializer(Serializer serializer)
            => (Serializer) = (serializer);

        public string Serialize(object obj)
            => Serializer.Serialize(obj);
    }
}