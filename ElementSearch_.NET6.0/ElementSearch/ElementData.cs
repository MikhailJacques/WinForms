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
        public string? LongName { get; set; }
        public string? ShortName { get; set; }
        public string? ElementType { get; set; }
        public string? Channel { get; set; }
        public string? Database { get; set; }
        public string? Location { get; set; }
        public uint Handle { get; set; }
    }
}