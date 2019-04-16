using Sitecore.Data.Items;
using Sitecore.Data;
using System.Linq;

namespace Score.ComponentManager.Parts
{
    public abstract class PartItemBase: PartBase
    {
        public PartItemBase(Item componentItem): base(componentItem)        { }

        protected virtual Item CreateFolderPath(ID templateID)
        {
            //create folders
            string newPath = "";
            Item currentItem = null;
            foreach (var segment in Path.Split('/').Reverse().Skip(1).Reverse())
            {
                newPath += "/" + segment;
                var item = Database.GetItem(newPath);
                if (item == null)
                {
                    item = currentItem.Add(segment, new TemplateItem(Database.GetItem(templateID)));
                }

                //set current item for the next iteration
                currentItem = item;
            }

            return currentItem;
        }
    }
}
