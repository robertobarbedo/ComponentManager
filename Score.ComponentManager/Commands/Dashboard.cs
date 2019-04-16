using Score.ComponentManager.Templates;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Shell;
using Sitecore.Shell.Applications.ContentEditor.Tabs;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Framework.Scripts;
using Sitecore.Web.UI.Sheer;
using System.Linq;

namespace Score.ComponentManager.Commands
{
    public class Dashboard : Command
    {
        public override void Execute(CommandContext context)
        {
            //null check
            Assert.ArgumentNotNull(context, "context");
            if ((int)context.Items.Length != 1)
                return;
            
            Item contextItem = context?.Items?[0];
            if (contextItem == null)
                return;

            ShortID shortID = contextItem.ID.ToShortID();
            string str = string.Concat("OpenComponentDashboard", shortID);
            if ((new EditorTabsClientManager()).ShowEditorTabs.Any<ShowEditorTab>((ShowEditorTab tab) =>
            {
                if (tab.Command != "componentmanager:dashboard") { return false; }
                return tab.Id == str;
            }))
            {
                SheerResponse.Eval(string.Concat("scContent.onEditorTabClick(null, null, '", str, "')"));
                return;
            }

            //open the master dashboard in the content editor tab area
            UrlString urlString = new UrlString("/sitecore/shell/Applications/ComponentManager/Dashboard.aspx");

            //add db name and context id to the querystring
            contextItem.Uri.AddToUrlString(urlString);
            UIUtil.AddContentDatabaseParameter(urlString);

            SheerResponse.Eval((new ShowEditorTab()
            {
                Command = "sitecluster:dashboard",
                Header = Translate.Text("Component Scaffolding"),
                Title = string.Concat(Translate.Text("Component Scaffolding"), ": ", (!UserOptions.View.UseDisplayName ? contextItem.Name : contextItem.DisplayName)),
                Icon = Images.GetThemedImageSource("Apps/16x16/Codes.png"),
                Url = urlString.ToString(),
                Id = str,
                Closeable = true
            }).ToString());
        }

        public override CommandState QueryState(CommandContext context)
        {
            Error.AssertObject(context, "context");

            //disable is no item is selected
            if ((int)context.Items.Length != 1)
                return CommandState.Disabled;

            //disable is no item is selected
            if (context.Items[0] == null)
                return CommandState.Disabled;
            
            //disable if cannot write on that page
            if (!context.Items[0].Access.CanWriteLanguage())
                return CommandState.Disabled;

            //disable if is not a component config item
            if (!(Component.TemplateId.Equals(context.Items[0].TemplateID)))
                return CommandState.Disabled;

            //enabled otherwise
            return base.QueryState(context);
        }
    }
}
