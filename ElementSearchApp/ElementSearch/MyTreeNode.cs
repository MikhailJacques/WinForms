using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSearch
{
    public class MyTreeNode : TreeNode
    {
        public uint _ID { get; set; }
        public uint _Handle { get; set; }

        public MyTreeNode()
        {
            _ID = 0;
            _Handle = 0;
        }

        public MyTreeNode(string text) : base(text)
        {
            _ID = 0;
            _Handle = 0;
        }

        public MyTreeNode(string text, uint id, uint handle) : base(text)
        {
            _ID = id;
            _Handle = handle;
        }

        public MyTreeNode(string text, MyTreeNode[] children) : base(text)
        {
            _ID = 0;
            _Handle = 0;
            Nodes.AddRange(children);
        }

        public MyTreeNode(MyTreeNode other) : base(other.Text)
        {
            _ID = other._ID;
            _Handle = other._Handle;
            Checked = other.Checked;
        }
    }
}