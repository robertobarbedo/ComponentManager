using Sitecore.Data.Items;
using System.IO;
using Score.ComponentManager.Parts.T4;
using System.Linq;
using System.Collections.Generic;
using Sitecore.Data.Fields;


namespace Score.ComponentManager.Parts
{
    public class CSHTML : PartFileBase
    {
        public CSHTML(Item componentItem) : base(componentItem)
        {
            Path = $@"{WebProjectPath}\Areas\{ProjectName}\Views\Shared{ComponentPhysicalPath}.cshtml";

            ClassName = ComponentPath.Split('/').Reverse().Take(1).FirstOrDefault() ?? "";

            Status = File.Exists(Path) ? Status.Created : Status.Missing;

            RelativePath = $"/Areas/{ProjectName}/Views/Shared{ComponentPath.Replace(" ","")}.cshtml";
        }

        public override int SortOrder => 120;

        public override string Title => "CSHTML File";

        public string RelativePath { get; set; }

        public override void Generate()
        {
            string folder = CreatePhysicalFolderPath();

            CSHTMLT4 tt = new CSHTMLT4(new T4Args()
            {
                ProjectName = ProjectName,
                BaseNamespace = BaseNamespace,
                ComponentName = ComponentItem.Name,
                ComponentPath = ComponentPath,
                ModelType = new ModelClass(ComponentItem).ModelType,
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
