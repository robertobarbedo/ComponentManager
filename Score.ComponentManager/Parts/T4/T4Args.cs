using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score.ComponentManager.Parts.T4
{
    public class T4Args
    {
        public string ProjectName { get; set; }
        public string ComponentName { get; set; }
        public string ComponentPath { get; set; }
        public string ComponentFolderOnly { get; set; }
        public string TemplateID { get; set; }
        public string ModelType { get; set; }

        public List<T4Field> DatasourceFields { get; set; }

        public List<T4Field> RenderingParameterFields { get; set; }
    }

    public class T4Field
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ID { get; set; }
    }
}
