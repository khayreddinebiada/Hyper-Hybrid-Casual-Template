
namespace Engine.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TemplateSettingsAttribute : System.Attribute
    {
        public string Path { get; private set; }
        public string Name { get; private set; }

        public TemplateSettingsAttribute(string path, string name)
        {
            Path = path;
            Name = name;
        }
    }
}