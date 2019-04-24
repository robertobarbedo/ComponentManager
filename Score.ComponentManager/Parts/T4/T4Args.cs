using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Score.ComponentManager.Parts.T4
{
    public class T4Args
    {
        public string ProjectName { get; set; }
        public string BaseNamespace { get; set; }
        public string ComponentName { get; set; }
        public string ComponentPath { get; set; }
        public string ComponentFolderOnly { get; set; }
        public string TemplateID { get; set; }
        public string ModelType { get; set; }

        public List<T4Field> DatasourceFields { get; set; }

        public List<T4Field> RenderingParameterFields { get; set; }

        public string MainCSSClass
        {
            get
            {
                return ToCSSName(ComponentName);
            }
        }

        public static string ToCSSName(string name)
        {
            string s = Regex.Replace(name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1-");
            s = s.Replace(" ", "-");
            return s.ToLower();
        }


        public string ComponentNameNoSpace { get { return ComponentName.Replace(" ", ""); } }
        public string ComponentPathNoSpace
        {
            get
            {
                string s = string.Join("/", ComponentPath.Split('/').Reverse().Skip(1).Reverse());
                s += "/" + ComponentNameNoSpace;
                return s;

            }
        }

        public string ComponentFolderOnlyNoSpace
        {
            get
            {
                return ComponentFolderOnly.Replace(" ", "");
            }
        }
    }

    public class T4Field
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ID { get; set; }
    }
}
