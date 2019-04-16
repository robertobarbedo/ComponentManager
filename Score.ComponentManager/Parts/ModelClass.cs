using Sitecore.Data.Items;
using System.IO;
using Score.ComponentManager.Parts.T4;
using System.Linq;

namespace Score.ComponentManager.Parts
{
    public class ModelClass : PartFileBase
    {
        public ModelClass(Item componentItem) : base(componentItem)
        {
            Path = $@"{WebProjectPath}\Areas\{ProjectName}\Models{ComponentPhysicalPath}.cs";

            ClassName = ComponentPath.Split('/').Reverse().Take(1).FirstOrDefault() ?? "";

            ModelType = $"{ProjectName}.Web.Areas.{ProjectName}.Models{ComponentPath.Replace("/", ".")}RenderingModel";
            ModelFullyQualifiedType = $"{ProjectName}.Web.Areas.{ProjectName}.Models{ComponentPath.Replace("/", ".")}RenderingModel, {ProjectName}.Web";

            Status = File.Exists(Path) ? Status.Created : Status.Missing;
        }

        public override int SortOrder => 130;

        public override string Title => "Model C# Class";

        public string ModelFullyQualifiedType { get; set; }

        public string ModelType { get; set; }

        public override void Generate()
        {
            string folder = CreatePhysicalFolderPath();

            ModelClassT4 tt = new ModelClassT4(new T4Args() {
                ProjectName = ProjectName,
                ComponentName = ComponentItem.Name,
                ComponentPath = ComponentPath,
                ComponentFolderOnly = ComponentFolderOnly
            });

            File.WriteAllText(Path, tt.TransformText());
        }
    }
}
