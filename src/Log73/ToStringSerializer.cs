namespace Log73
{
    /// <summary>
    /// Serializes objects using their ToString method.
    /// </summary>
    public class ToStringSerializer : IObjectSerializer
    {
        /// <inheritdoc cref="IObjectSerializer.Serialize"/>
        public string Serialize(object obj)
            => obj.ToString();
    }
}