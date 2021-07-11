namespace Log73
{
    /// <summary>
    /// Serializes objects using their ToString method.
    /// </summary>
    public class ToStringSerializer : IObjectSerializer
    {
        /// <summary>
        /// Returns the ToString() of the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The output of ToString().</returns>
        public string Serialize(object obj)
            => obj.ToString();
    }
}