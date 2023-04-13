using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ElementSearch
{
    public class MyTreeNode : TreeNode, ISerializable
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

        public MyTreeNode(string text, uint id) : base(text)
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

        // Constructor for deserialization
        protected MyTreeNode(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _ID = info.GetUInt32("_ID");
        }

        // Explicit implementation of ISerializable.GetObjectData
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.Serialize(info, context);
            info.AddValue("_ID", _ID);
        }
    }
}