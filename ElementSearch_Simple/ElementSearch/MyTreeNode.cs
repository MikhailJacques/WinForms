using System.Windows.Forms;

namespace ElementSearch
{
    public class MyTreeNode : TreeNode
    {
        public uint _ID { get; set; }

        public MyTreeNode()
        {
            _ID = 0;
        }

        public MyTreeNode(string text) : base(text)
        {
            _ID = 0;
        }

        public MyTreeNode(string text, uint id, uint handle) : base(text)
        {
            _ID = id;
        }

        public MyTreeNode(string text, MyTreeNode[] children) : base(text)
        {
            _ID = 0;
            Nodes.AddRange(children);
        }

        public MyTreeNode(MyTreeNode other) : base(other.Text)
        {
            _ID = other._ID;
            Checked = other.Checked;
        }
    }
}