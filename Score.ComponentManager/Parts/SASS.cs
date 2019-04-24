using Sitecore.Data.Items;
using System.IO;
using Score.ComponentManager.Parts.T4;
using System.Linq;

namespace Score.ComponentManager.Parts
{
    public class SASS : PartFileBase
    {
        public SASS(Item componentItem) : base(componentItem)
        {
            //string fileName = string.Join("\\", ComponentPhysicalPath.Split('\\').Reverse().Skip(1).Reverse().ToArray());
            string fileName = "_site-component-" + T4Args.ToCSSName(ComponentPhysicalPath.Split('\\').Reverse().Take(1).FirstOrDefault() ?? "");

            Path = $@"{WebProjectPath}\Areas\{ProjectName}\scss\site\{fileName}.scss";

            ClassName = ComponentPath.Split('/').Reverse().Take(1).FirstOrDefault() ?? "";

            Status = File.Exists(Path) ? Status.Created : Status.Missing;
        }

        public override int SortOrder => 128;

        public override string Title => "SASS .scss File";
        
        public override void Generate()
        {
            string folder = CreatePhysicalFolderPath();

            SASST4 tt = new SASST4(new T4Args()
            {
                ProjectName = ProjectName,
                BaseNamespace = BaseNamespace,
                ComponentName = ComponentItem.Name,
                ComponentPath = ComponentPath,
                ModelType = new ModelClass(ComponentItem).ModelType,
                TemplateID = new DatasourceTemplate(ComponentItem).ID,
                
                ComponentFolderOnly = ComponentFolderOnly
            });

            File.WriteAllText(Path, tt.TransformText());
        }
    }
}
