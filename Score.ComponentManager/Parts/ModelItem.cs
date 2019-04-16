using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Score.ComponentManager.Parts
{
    public class ModelItem : PartItemBase
    {
        private ID ModelFolderID = new ID("{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}");
        private ID ModelTemplateID = new ID("{FED6A14F-0D05-4E18-B160-17C0588A2005}");

        public ModelItem(Item componentItem) : base(componentItem)
        {
            Path = $"/sitecore/layout/Models/{ProjectName}{ComponentPath}";

            var item = Database.GetItem(Path);
            Status = item == null ? Status.Missing : Status.Created;
            ExtraInfo = item == null ? "" : item.ID.ToString();
        }

        public override int SortOrder => 200;

        public override string Title => "Model Item";

        public override void Generate()
        {
            Item currentItem = CreateFolderPath(ModelFolderID);
            
            //create template
            var model = currentItem.Add(Path.Split('/').Reverse().Take(1).First(), new TemplateItem(Database.GetItem(ModelTemplateID)));

            //set fields
            model.Editing.BeginEdit();
            model.Fields["Model Type"].Value = new ModelClass(ComponentItem)?.ModelFullyQualifiedType ?? "";
            model.Editing.EndEdit();
        }
    }
}
