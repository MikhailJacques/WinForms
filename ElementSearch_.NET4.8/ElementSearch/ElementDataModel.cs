using System;
using System.Collections.Generic;

namespace ElementSearch
{
    public class NamedEntity
    {
        public uint ID { get; set; }
        public string Name { get; set; }
    }

    public class ElementDataModel
    {
        public uint ID { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string ElementType { get; set; }
        public string Channel { get; set; }
        public string Database { get; set; }
        public string Location { get; set; }
        public uint Handle { get; set; }

        public static List<ElementDataModel> FilterElementsByText(List<ElementDataModel> elements, string searchText)
        {
            var filteredElements = new List<ElementDataModel>();

            foreach (var element in elements)
            {
                if (element.LongName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    element.ShortName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredElements.Add(element);
                }
            }

            return filteredElements;
        }
    }
}
