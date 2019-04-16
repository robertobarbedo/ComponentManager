using Sitecore.Data.Items;
using System.Linq;
using Sitecore.Data;
using System;
using System.Collections.Generic;

namespace Score.ComponentManager.Parts
{
    public class AddToPlaceholderSettings : PartItemBase
    {
        public List<Item> Placeholders { get; set; }

        public AddToPlaceholderSettings(Item componentItem) : base(componentItem)
        {
            //set placehodlers
            Placeholders = new List<Item>();
            foreach (var id in (componentItem.Fields["Placeholders"]?.Value ?? "").Split('|'))
            {
                Guid guid;
                if (Guid.TryParse(id, out guid))
                {
                    var item = Database.GetItem(new ID(guid));
                    if (item != null)
                        Placeholders.Add(item);
                }
            }

            Path = String.Join("<br>", Placeholders.Select(c => c.Paths.FullPath).ToArray());


            string renderingId = new RenderingDefinitionItem(ComponentItem).ID.ToString();
            bool isCreated = false;
            foreach (Item ph in Placeholders)
            {
                if (ph?.Fields?["Allowed Controls"] != null)
                {
                    if ((ph.Fields["Allowed Controls"].Value ?? "").IndexOf(renderingId) >= 0)
                    {
                        isCreated = true;
                    }
                }
            }

            Status = !isCreated ? Status.Missing : Status.Created;
        }

        public override int SortOrder => 950;

        public override string Title => "Add Rendering to Placeholders";

        public override void Generate()
        {
            if (new RenderingDefinitionItem(ComponentItem).ID == ID.Null)
                return;

            string renderingId = new RenderingDefinitionItem(ComponentItem).ID.ToString();

            foreach (Item ph in Placeholders)
            {
                if (ph?.Fields?["Allowed Controls"] != null)
                {
                    if ((ph.Fields["Allowed Controls"].Value ?? "").IndexOf(renderingId) < 0)
                    {
                        ph.Editing.BeginEdit();
                        ph.Fields["Allowed Controls"].Value = ph.Fields["Allowed Controls"].Value + "|" + renderingId;
                        ph.Editing.EndEdit();
                    }
                }
            }
        }

    }
}


