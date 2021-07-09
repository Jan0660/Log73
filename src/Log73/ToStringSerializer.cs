namespace Log73
{
    public class ToStringSerializer : IObjectSerializer
    {
        public string Serialize(object obj)
            => obj.ToString();
    }
}