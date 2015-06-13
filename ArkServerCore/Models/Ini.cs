using System.Collections.Generic;
using System.Collections.Specialized;

namespace ArkServerCore.Models
{
    public class Ini
    {
        public List<Section> Sections = new List<Section>();

        public Ini()
        {
            
        }

        public void AddSection(Section newSection)
        {
            this.Sections.Add(newSection);
        }

        public void AddSection(string sectionName, Dictionary<string, string> arguements)
        {
            this.Sections.Add(new Section(sectionName, arguements));
        }
    }

    public class Section
    {
        public string SectionName { get; set; }
        public Dictionary<string, string> KeyValues = new Dictionary<string, string>();

        public Section()
        {
                
        }

        public Section(string sectionName, Dictionary<string, string> keyValues)
        {
            this.SectionName = sectionName;
            this.KeyValues = keyValues;
        }

    }
}
