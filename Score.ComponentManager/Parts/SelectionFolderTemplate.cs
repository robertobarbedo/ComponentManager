using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Score.ComponentManager.Parts
{
    public class SelectionFolderTemplate : PartItemBase
    {
        private ID TemplateFolderID = new ID("{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}");
        private ID TemplateID = new ID("{AB86861A-6030-46C5-B394-E8F99E8B87DB}");
        private ID ScoreListItemTemplateID = new ID("{0B812923-4A0F-4472-BB25-A2D4B10D881E}");

        private ID FolderBaseTemplateID = new ID("{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}");
        
        public SelectionFolderTemplate(Item componentItem) : base(componentItem)
        {
            Path = $"/sitecore/templates/{ProjectName}/Selection Folders{ComponentPath}";

            var item = Database.GetItem(Path);
            Status = item == null ? Status.Missing : Status.Created;
            ExtraInfo = item == null ? "" : item.ID.ToString();

            ID = item?.ID ?? ID.Null;
        }

        public override int SortOrder => 20;

        public override string Title => "Selections Folder Template";

        public ID ID { get; set; }

        public override void Generate()
        {
            Item currentItem = CreateFolderPath(TemplateFolderID);

            //create template
            var item = currentItem.Add(ComponentItem.Name, new TemplateItem(Database.GetItem(TemplateID)));

            //edit fields
            item.Editing.BeginEdit();
            item.Fields["__Base template"].Value = FolderBaseTemplateID.ToString();
            item.Editing.EndEdit();

            //ItemRuntimeSettings std values
            var template = new TemplateItem(item);
            var stdValues = template.CreateStandardValues();

            //edit std values
            stdValues.Editing.BeginEdit();
            stdValues.Fields["__Masters"].Value = ScoreListItemTemplateID.ToString();
            stdValues.Editing.EndEdit();
        }
    }
}
