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
        public uint m_ID { get; set; }
        public uint m_Handle { get; set; }

        public MyTreeNode()
        {
            m_ID = 0;
            m_Handle = 0;
        }

        public MyTreeNode(string text) : base(text)
        {
            m_ID = 0;
            m_Handle = 0;
        }

        public MyTreeNode(string text, uint id, uint handle) : base(text)
        {
            m_ID = id;
            m_Handle = handle;
        }

        public MyTreeNode(string text, MyTreeNode[] children) : base(text)
        {
            m_ID = 0;
            m_Handle = 0;
            Nodes.AddRange(children);
        }

        public MyTreeNode(MyTreeNode other) : base(other.Text)
        {
            m_ID = other.m_ID;
            m_Handle = other.m_Handle;
            Checked = other.Checked;
        }
    }
}