using Sitecore.Data.Items;
using Sitecore.Data;
using System.Linq;

namespace Score.ComponentManager.Parts
{
    public abstract class PartBase
    {
        public Item ComponentItem { get; set; }

        public virtual string ComponentPath { get; set; }

        public string ComponentFolderOnly { get { return string.Join("/", ComponentPath.Split('/').Reverse().Skip(1).Reverse().ToArray()); } }

        public Database Database { get; set; }

        public string ProjectName => Sitecore.Configuration.Settings.GetSetting("ComponentManager.ProjectName", "Undefined");

        public string BaseNamespace
        {
            get
            {
                return string.IsNullOrWhiteSpace(Sitecore.Configuration.Settings.GetSetting("ComponentManager.BaseNamespace", "")) ? ProjectName : Sitecore.Configuration.Settings.GetSetting("ComponentManager.BaseNamespace", "");
            }
        }

        public string WebProjectPath => Sitecore.Configuration.Settings.GetSetting("ComponentManager.WebProjectPath", "Undefined");

        public string DataProjectPath => Sitecore.Configuration.Settings.GetSetting("ComponentManager.DataProjectPath", "Undefined");

        public PartBase(Item componentItem)
        {
            ComponentItem = componentItem;
            ComponentPath = BuildPath();
            Database = componentItem.Database;
        }

        private string BuildPath()
        {
            var moduleFolder = GetModulesFolder(ComponentItem);

            return ComponentItem.Paths.FullPath.Replace(moduleFolder.Paths.FullPath, "");
        }

        private Item GetModulesFolder(Item context)
        {
            if (context.Parent.TemplateID.Equals(Templates.ComponentManagerModule.TemplateId))
            {
                return context.Parent;
            }
            else
            {
                return GetModulesFolder(context.Parent);
            }
        }

        public abstract int SortOrder { get; }

        public abstract string Title { get; }

        public virtual string Path { get; set; }

        public virtual string ExtraInfo { get; set; }

        public virtual Status Status { get; set; }

        //public abstract Item Artifact { get; set; }

        public abstract void Generate();
    }

    public enum Status
    {
        Created,
        Missing
    }
}
