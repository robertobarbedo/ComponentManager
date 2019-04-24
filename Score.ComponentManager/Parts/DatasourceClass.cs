using Sitecore.Data.Items;
using System.IO;
using Score.ComponentManager.Parts.T4;
using System.Linq;
using System.Collections.Generic;
using Sitecore.Data.Fields;

namespace Score.ComponentManager.Parts
{
    public class DatasourceClass : PartFileBase
    {
        public DatasourceClass(Item componentItem) : base(componentItem)
        {
            Path = $@"{DataProjectPath}\CustomItems{ComponentPhysicalPath}.cs";

            ClassName = ComponentPath.Split('/').Reverse().Take(1).FirstOrDefault() ?? "";

            ModelType = $"{BaseNamespace}.Data.CustomItems{ComponentPath.Replace(" ", "").Replace("/", ".")}, {BaseNamespace}.Data";

            Status = File.Exists(Path) ? Status.Created : Status.Missing;
        }

        public override int SortOrder => 100;

        public override string Title => "Datasource C# Class";

        public string ModelType { get; set; }

        public override void Generate()
        {
            string folder = CreatePhysicalFolderPath();

            DatasourceClassT4 tt = new DatasourceClassT4(new T4Args()
            {
                ProjectName = ProjectName,
                BaseNamespace = BaseNamespace,
                ComponentName = ComponentItem.Name,
                ComponentPath = ComponentPath,
                TemplateID = new DatasourceTemplate(ComponentItem).ID,
                DatasourceFields = GetDSFields().ToList(),
                ComponentFolderOnly = ComponentFolderOnly
            });

            File.WriteAllText(Path, tt.TransformText());
        }

        private IEnumerable<T4Field> GetDSFields()
        {
            string stdValuesPath = $"{new DatasourceTemplate(ComponentItem).Path}/__Standard Values";
            Item stdValues = Database.GetItem(stdValuesPath);

            stdValues.Fields.ReadAll();

            foreach (Field f in stdValues.Fields)
            {
                if (!f.Name.StartsWith("__"))
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
