using Score.ComponentManager.Parts;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace Score.ComponentManager.Shell.Applications.ComponentManager
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private Database Database = Sitecore.Configuration.Factory.GetDatabase("master");

        public Item ContextItem { get { return Database.GetItem(new ID(Request.QueryString["id"])); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            var type = typeof(PartBase);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(c => c.FullName.StartsWith("Score") || c.FullName.StartsWith(Sitecore.Configuration.Settings.GetSetting("ComponentManager.ProjectName", "Undefined")))
                .SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p))
                .Where(c => !c.IsAbstract);

            rptParts.DataSource = types.Select(c => (PartBase)Activator.CreateInstance(c, ContextItem)).ToList().OrderBy(c => c.SortOrder);
            rptParts.DataBind();
        }


        protected void BindRepeater(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                var part = (PartBase)e.Item.DataItem;
                if (part == null)
                    return;

                var btn = (Button)e.Item.FindControl("generate");
                if (btn != null)
                    btn.Enabled = part.Status != Status.Created;
            }
        }

        protected void GenerateClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string typeName = btn.CommandArgument;

            var type = AppDomain.CurrentDomain.GetAssemblies()
                .Where(c => c.FullName.StartsWith("Score"))
                .SelectMany(s => s.GetTypes()).Where(p => (typeof(PartBase)).IsAssignableFrom(p))
                .Where(c => c.FullName == typeName).FirstOrDefault();

            PartBase part = (PartBase)Activator.CreateInstance(type, ContextItem);

            part.Generate();

            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }
    }
}