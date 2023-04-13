
using System.Collections.Generic;

namespace ElementSearch
{
    public class ElementTypeData
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public List<ElementTypeData> Children { get; set; }

        public ElementTypeData()
        {
            Children = new List<ElementTypeData>();
        }
    }
}
