//using System;
//using Score.ComponentManager.Templates;
//using Sitecore.Data.Items;
//using Sitecore.Events;

//namespace Score.ComponentManager.Events
//{
//    public class CMRemoveWhiteSpaces
//    {
//        protected void OnItemSaved(object sender, System.EventArgs args)
//        {
//            if (args == null) { return; }

//            Item item = Event.ExtractParameter(args, 0) as Item;

//            if (item?.Name == null) { return; }

//            if (item.Name.IndexOf(" ") < 0) { return; }

//            if (item.TemplateID.Equals(Component.TemplateId))
//            {
//                try
//                {
//                    item.Editing.BeginEdit();
//                    item.Name = item.Name.Replace(" ", "");
//                    item.Editing.AcceptChanges();
//                    item.Editing.EndEdit();
//                }
//                catch (Exception ex)
//                {
//                    Sitecore.Diagnostics.Log.Error(string.Format("Failed to rename item {0}: ", item.ID), ex, this);
//                }
//            }
//        }
//    }
//}


