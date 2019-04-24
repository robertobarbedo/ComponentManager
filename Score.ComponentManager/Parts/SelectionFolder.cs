using Sitecore.Data.Items;
using Sitecore.Data;

namespace Score.ComponentManager.Parts
{
    public class SelectionFolder : PartItemBase
    {
        private ID SelectionFolderTemplateID = new ID("{857D2D74-0619-435E-926B-6ABFE75C37F0}");

        private string FolderName { get { return ComponentItem.Name + " Styles"; } }

        public SelectionFolder(Item componentItem) : base(componentItem)
        {
            Path = $"/sitecore/content/{ProjectName}/{ProjectName} Selections/{FolderName}";

            var item = Database.GetItem(Path);
            Status = item == null ? Status.Missing : Status.Created;
            ExtraInfo = item == null ? "" : item.ID.ToString();
        }

        public override int SortOrder => 50;

        public override string Title => "Selections Shared Folder";

        public override void Generate()
        {
            //get selection folder 
            var folder = Database.GetItem($"/sitecore/content/{ProjectName}/{ProjectName} Selections");

            //create template
            var item = folder.Add(FolderName, new TemplateID(new SelectionFolderTemplate(ComponentItem).ID));
        }
    }
}
