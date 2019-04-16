using Sitecore.Data.Items;
using System.IO;
using Score.ComponentManager.Parts.T4;
using System.Linq;
using System.Collections.Generic;
using Sitecore.Data.Fields;

namespace Score.ComponentManager.Parts
{
    public class RenderingParameterClass : PartFileBase
    {
        public RenderingParameterClass(Item componentItem) : base(componentItem)
        {
            Path = $@"{DataProjectPath}\RenderingParameters{ComponentPhysicalPath}.cs";

            ClassName = ComponentPath.Split('/').Reverse().Take(1).FirstOrDefault() ?? "";

            ModelType = $"{ProjectName}.Data.RenderingParameters{ComponentPath.Replace("/", ".")}, {ProjectName}.Data";

            Status = File.Exists(Path) ? Status.Created : Status.Missing;
        }

        public override int SortOrder => 110;

        public override string Title => "Rendering Parameter C# Class";

        public string ModelType { get; set; }

        public override void Generate()
        {
            string folder = CreatePhysicalFolderPath();

            RenderingParameterClassT4 tt = new RenderingParameterClassT4(new T4Args()
            {
                ProjectName = ProjectName,
                ComponentName = ComponentItem.Name,
                ComponentPath = ComponentPath,
                RenderingParameterFields = GetFields().ToList(),
                ComponentFolderOnly = ComponentFolderOnly
            });

            File.WriteAllText(Path, tt.TransformText());
        }

        private IEnumerable<T4Field> GetFields()
        {
            List<string> ignoreFields = new List<string>()
            {
                "Show Hide",
                "Caching",
                "Data Source",
                "Placeholder",
                "Additional Parameters",
                "Personalization",
                "Tests"
            };

            string stdValuesPath = $"{new RenderingParameterTemplate(ComponentItem).Path}/__Standard Values";
            Item stdValues = Database.GetItem(stdValuesPath);

            stdValues.Fields.ReadAll();

            foreach (Field f in stdValues.Fields)
            {
                if (!f.Name.StartsWith("__") && !(ignoreFields.Contains(f.Name)))
                {
                    yield return new T4Field()
                    {
                        Name = f.Name,
                        Type = f.Type,
                        ID = f.ID.ToString()
                    };
                }
            }
        }
    }
}
