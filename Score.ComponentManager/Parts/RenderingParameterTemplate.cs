using Sitecore.Data.Items;
using Sitecore.Data;

namespace Score.ComponentManager.Parts
{
    public class RenderingParameterTemplate : PartItemBase
    {
        private ID TemplateFolderID = new ID("{0437FEE2-44C9-46A6-ABE9-28858D9FEE8C}");
        private ID TemplateID = new ID("{AB86861A-6030-46C5-B394-E8F99E8B87DB}");
        private ID TemplateSectionID = new ID("{E269FBB5-3750-427A-9149-7AA950B49301}");
        private ID TemplateFieldID = new ID("{455A3E98-A627-4B40-8035-E683A0331AC7}");


        private ID ScoreUIBaseComponentID = new ID("{D191AB49-7616-46CC-B9D9-BA6FC3AA3639}");

        public RenderingParameterTemplate(Item componentItem) : base(componentItem)
        {
            Path = $"/sitecore/templates/{ProjectName}/Rendering Parameters{ComponentPath}";

            var item = Database.GetItem(Path);
            Status = item == null ? Status.Missing : Status.Created;
            ExtraInfo = item == null ? "" : item.ID.ToString();

            ID = item?.ID ?? ID.Null;
        }

        public override int SortOrder => 30;

        public override string Title => "Rendering Parameter Template";

        public ID ID { get; set; }

        public override void Generate()
        {
            Item currentItem = CreateFolderPath(TemplateFolderID);

            //create template
            var item = currentItem.Add(ComponentItem.Name, new TemplateItem(Database.GetItem(TemplateID)));
            var template = new TemplateItem(item);
            template.CreateStandardValues();

            //edit fields
            item.Editing.BeginEdit();
            item.Fields["__Base template"].Value = ScoreUIBaseComponentID.ToString();
            item.Editing.EndEdit();

            //add default style field
            var section = item.Add(ComponentItem.Name, new TemplateID(TemplateSectionID));
            var field = section.Add(ComponentItem.Name + " Style", new TemplateID(TemplateFieldID));

            //get sel folder id
            string selectionFolderID = Database.GetItem(new SelectionFolderTemplate(ComponentItem).Path)?.ID.ToString() ?? "{missing-id}";

            //edit fields
            field.Editing.BeginEdit();
            field.Fields["Type"].Value = "Droplink";
            field.Fields["Source"].Value = $@"DataSource=query:#selections#*[@@templateid='{selectionFolderID}']/*&Display=Text";
            field.Editing.EndEdit();

        }
    }
}
