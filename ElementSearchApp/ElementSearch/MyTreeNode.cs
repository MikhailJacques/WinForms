using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSearch
{
    ////internal class MyTreeNode
    //public class MyTreeNode : TreeNode
    //{
    //    public uint m_ID { get; set; }
    //    public uint m_Handle { get; set; }

    //    public MyTreeNode() : base()
    //    {
    //        m_ID = 0;
    //        m_Handle = 0;
    //    }

    //    public MyTreeNode(string text) : base(text)
    //    {
    //        m_ID = 0;
    //        m_Handle = 0;
    //    }

    //    public MyTreeNode(string text, uint id, uint handle) : base(text)
    //    {
    //        m_ID = id;
    //        m_Handle = handle;
    //    }

    //    public MyTreeNode(string text, MyTreeNode[] children) : base(text)
    //    {
    //        m_ID = 0;
    //        m_Handle = 0;
    //        Nodes.AddRange(children);
    //    }
    //}

    public class MyTreeNode : TreeNode
    {
        public uint Id { get; }
        public uint m_Handle { get; }

        public MyTreeNode(string text, uint id, uint handle) : base(text)
        {
            Id = id;
            m_Handle = handle;
        }
    }

}





