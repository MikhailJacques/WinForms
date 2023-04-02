using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementSearch
{
    public class ElementData
    {
        public uint ID { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public uint ElementType { get; set; }
        public uint Channel { get; set; }
        public uint Database { get; set; }
        public string Location { get; set; }
        public uint Handle { get; set; }

        //public static List<ElementData> FilterElementsByText(List<ElementData> elements, string searchText)
        //{
        //    var filteredElements = new List<ElementData>();

        //    foreach (var element in elements)
        //    {
        //        if (element.LongName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
        //            element.ShortName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
        //        {
        //            filteredElements.Add(element);
        //        }
        //    }

        //    return filteredElements;
        //}
    }
}