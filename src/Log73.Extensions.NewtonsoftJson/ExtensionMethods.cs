using Log73.Extensible;

namespace Log73.Extensions.NewtonsoftJson
{
    public static class ExtensionMethods
    {
        public static void Json(this ConsoleLogObject ext, object obj)
        {
            if(Console.Options.ObjectSerializer is NewtonsoftJsonSerializer serializer)
                Console.Log(serializer.Serialize(obj));
            else
                Console.Log(new NewtonsoftJsonSerializer().Serialize(obj));
        }
    }
}