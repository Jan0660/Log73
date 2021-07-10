namespace Log73
{
    /// <summary>
    /// The interface for object serializers to inherit from.
    /// </summary>
    public interface IObjectSerializer
    {
        /// <summary>
        /// Serialize the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The serialized string of the object.</returns>
        public string Serialize(object obj);
    }
}