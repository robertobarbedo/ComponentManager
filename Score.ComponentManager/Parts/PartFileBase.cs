using System;
using Sitecore.Data.Items;
using System.Linq;
using System.IO;

namespace Score.ComponentManager.Parts
{
    public abstract class PartFileBase : PartBase
    {
        public PartFileBase(Item componentItem) : base(componentItem) { }

        public string ComponentPhysicalPath
        {
            get
            {
                return ComponentPath.Replace('/', '\\').Replace(" ", "");
            }
        }

        protected string ClassName { get; set; }

        protected virtual String CreatePhysicalFolderPath()
        {
            //create folders
            string newPath = "";
            foreach (var segment in Path.Split('\\').Reverse().Skip(1).Reverse())
            {

                if (!Directory.Exists(newPath + segment))
                {
                    Directory.CreateDirectory(newPath + segment);
                }

                newPath += segment + "\\";
            }

            return newPath;
        }
    }
}
