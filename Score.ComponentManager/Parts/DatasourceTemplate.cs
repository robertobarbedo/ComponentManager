using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Score.ComponentManager.Parts
{
    public class DatasourceTemplate : PartItemBase
    {
        private ID TemplateFolderID = new ID("{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}");
        private ID TemplateID = new ID("{AB86861A-6030-46C5-B394-E8F99E8B87DB}");

        public DatasourceTemplate(Item componentItem) : base(componentItem)
        {
            Path = $"/sitecore/templates/{ProjectName}/Datasource Templates{ComponentPath}";

            var item = Database.GetItem(Path);
            Status = item == null ? Status.Missing : Status.Created;
            ExtraInfo = item == null ? "" : item.ID.ToString();

            ID = item?.ID.ToString() ?? "";
        }

        public override int SortOrder => 10;

        public override string Title => "Datasource Template";

        public string ID { get; set; }
        
        public override void Generate()
        {
            Item currentItem = CreateFolderPath(TemplateFolderID);

            //create template
            var template = new TemplateItem(currentItem.Add(Path.Split('/').Reverse().Take(1).First(), new TemplateItem(Database.GetItem(TemplateID))));
            template.CreateStandardValues();
        }
    }
}
