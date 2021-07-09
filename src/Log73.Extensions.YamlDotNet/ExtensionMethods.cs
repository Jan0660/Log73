using Log73.Extensible;

namespace Log73.Extensions.YamlDotNet
{
    public static class ExtensionMethods
    {
        public static void Yaml(this ConsoleLogObject ext, object obj)
        {
            if(Console.Options.ObjectSerializer is YamlDotNetSerializer serializer)
                Console.Log(serializer.Serialize(obj));
            else
                Console.Log(new YamlDotNetSerializer().Serialize(obj));
        }
    }
}