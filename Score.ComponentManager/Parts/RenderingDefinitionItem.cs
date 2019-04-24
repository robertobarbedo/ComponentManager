using Sitecore.Data.Items;
using System.Linq;
using Sitecore.Data;

namespace Score.ComponentManager.Parts
{
    public class RenderingDefinitionItem : PartItemBase
    {
        private ID RenderingFolderID = new ID("{7EE0975B-0698-493E-B3A2-0B2EF33D0522}");

        private ID ViewRenderingID = new ID("{99F8905D-4A87-4EB8-9F8B-A9BEBFB3ADD6}");

        public RenderingDefinitionItem(Item componentItem) : base(componentItem)
        {
            Path = $"/sitecore/layout/Renderings/{ProjectName}{ComponentPath}";

            var item = Database.GetItem(Path);
            Status = item == null ? Status.Missing : Status.Created;
            ExtraInfo = item == null ? "" : item.ID.ToString();

            ID = item?.ID ?? ID.Null;
        }

        public override int SortOrder => 900;

        public override string Title => "Rendering Definition Item";

        public ID ID { get; set; }

        public override void Generate()
        {
            Item currentItem = CreateFolderPath(RenderingFolderID);

            //create template
            var rendering = currentItem.Add(Path.Split('/').Reverse().Take(1).First(), new TemplateItem(Database.GetItem(ViewRenderingID)));

            //set fields
            rendering.Editing.BeginEdit();
            rendering.Fields["Path"].Value = new CSHTML(ComponentItem).RelativePath;
            rendering.Fields["Model"].Value = new ModelItem(ComponentItem).Path;
            rendering.Fields["Parameters Template"].Value = new RenderingParameterTemplate(ComponentItem).ID.ToString();
            rendering.Fields["Datasource Location"].Value = GetDatasourceLocation();
            rendering.Fields["Datasource Template"].Value = new DatasourceTemplate(ComponentItem)?.Path ?? "";
            rendering.Editing.EndEdit();
        }

        private string GetDatasourceLocation()
        {
            string onlyPath = string.Join("/", ComponentPath.Split('/').Reverse().Skip(1).Reverse());
            return $"content-all:/Content{onlyPath}";
        }
    }
}
