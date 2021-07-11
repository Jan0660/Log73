using YamlDotNet.Serialization;

namespace Log73.Extensions.YamlDotNet
{
    /// <summary>
    /// Serializes objects as YAML using YamlDotNet.
    /// </summary>
    public class YamlDotNetSerializer : IObjectSerializer
    {
        public Serializer Serializer { get; set; }

        public YamlDotNetSerializer() : this(new())
        {
        }

        public YamlDotNetSerializer(Serializer serializer)
            => (Serializer) = (serializer);

        /// <summary>
        /// Serialize the object as YAML using <see cref="Serializer"/>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The serialized object.</returns>
        public string Serialize(object obj)
            => Serializer.Serialize(obj);
    }
}