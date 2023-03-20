using ElementSearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSearch
{
    public partial class FormElementSearch : Form
    {
        public FormElementSearch()
        {
            InitializeComponent();
        }

        // This implementation uses the async/await pattern and the Task.WhenAll() function to read the three text files in parallel.
        // The ReadTextFile function returns a Task<List<List<string>>> instead of directly modifying a passed-in list.
        // This allows us to use Task.WhenAll() to wait for all file reading tasks to complete before proceeding to fill the TreeView controls.
        private async void FormElementSearch_Load(object sender, EventArgs e)
        {
            string[] filesPaths = { "logs\\_lst_LogData_elm_type.cvs", "logs\\_lst_LogData_chn.cvs", 
                "logs\\_lst_LogData_dbs.cvs", "logs\\_lst_LogData_elm_all.cvs" };

            var fileTokensTasks = filesPaths.Select(ReadTextFile).ToArray();

            await Task.WhenAll(fileTokensTasks);

            FillTreeView(treeViewElemType, fileTokensTasks[0].Result);
            FillTreeView(treeViewChannel, fileTokensTasks[1].Result);
            FillTreeView(treeViewDatabase, fileTokensTasks[2].Result);

            FillListView2(listViewElements, fileTokensTasks[3].Result);
        }

        private async Task<List<List<string>>> ReadTextFile(string filePath)
        {
            var fileTokens = new List<List<string>>();

            using (var reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
                    fileTokens.Add(tokens);
                }
            }

            return fileTokens;
        }

        private void FillListView2(ListView listView, List<List<string>> fileTokens)
        {
            listView.BeginUpdate();
            listView.Items.Clear();

            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count < 8)
                    continue;

                ListViewItem newItem = new ListViewItem(lineTokens[0]); // ID

                newItem.SubItems.Add(lineTokens[1]); // Long Name
                newItem.SubItems.Add(lineTokens[2]); // Short Name
                newItem.SubItems.Add(lineTokens[3]); // Elem Type
                newItem.SubItems.Add(lineTokens[4]); // Channel
                newItem.SubItems.Add(lineTokens[5]); // Database
                newItem.SubItems.Add(lineTokens[6]); // Location
                newItem.SubItems.Add(lineTokens[7]); // Handle

                listView.Items.Add(newItem);
            }

            listView.EndUpdate();
        }

        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count < 3)
                    continue;

                string hierarchy = lineTokens[0];
                uint.TryParse(lineTokens[1], out uint id);
                uint.TryParse(lineTokens[2], out uint handle);

                var relatives = hierarchy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

                AddNode(treeView.Nodes, relatives, 0);
            }

            treeView.EndUpdate();
        }

        private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index)
        {
            if (index < family.Count)
            {
                var currentNode = nodes.Cast<MyTreeNode>().FirstOrDefault(node => node.Text == family[index].Text);
                if (currentNode == null)
                {
                    currentNode = family[index];
                    nodes.Add(currentNode);
                }
                AddNode(currentNode.Nodes, family, index + 1);
            }
        }

        private void UpdateChildNodes(MyTreeNode node, bool isChecked)
        {
            foreach (MyTreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
                UpdateChildNodes(child, isChecked);
            }
        }

        private void UpdateParentNode(MyTreeNode? node)
        {
            if (node == null)
            {
                return;
            }

            bool anyChecked = node.Nodes.Cast<TreeNode>().Any(childNode => childNode.Checked);

            if (node.Checked != anyChecked)
            {
                node.Checked = anyChecked;
                UpdateParentNode(node.Parent as MyTreeNode);
            }
        }

        // In this implementation, we have three separate event handlers, one for each TreeView control.
        // Each of these handlers calls the HandleTreeViewAfterCheck function, which contains the shared logic for all three TreeView controls.
        // The HandleTreeViewAfterCheck function manages the addition and removal of the event handler based on the sender,
        // which is the specific TreeView control that fired the event.
        private void treeViewElemType_AfterCheck(object sender, TreeViewEventArgs e)
        {
            HandleTreeViewAfterCheck(sender, e);

            UpdateListView();
        }

        private void treeViewChannel_AfterCheck(object sender, TreeViewEventArgs e)
        {
            HandleTreeViewAfterCheck(sender, e);

            UpdateListView();
        }

        private void treeViewDatabase_AfterCheck(object sender, TreeViewEventArgs e)
        {
            HandleTreeViewAfterCheck(sender, e);

            UpdateListView();
        }

        // This function is an event handler for the AfterCheck event on the TreeView control.
        // When a node's checkbox is checked or unchecked, it updates the parent and child nodes accordingly.
        // It also temporarily removes the event handler to prevent recursion before re-adding it after the updates are completed.
        private void HandleTreeViewAfterCheck(object? sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
            {
                return;
            }

            var node = e.Node as MyTreeNode;

            if (node != null)
            {
                UpdateChildNodes(node, node.Checked);
            }

            if (sender == null)
            {
                return;
            }

            TreeView treeView = (TreeView)sender;
            treeView.AfterCheck -= HandleTreeViewAfterCheck;

            if (node != null && node.Parent != null)
            {
                UpdateParentNode(node.Parent as MyTreeNode);
            }

            treeView.AfterCheck += HandleTreeViewAfterCheck;
        }


        private IEnumerable<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);
                }

                checkedNodes.AddRange(GetCheckedNodes(node.Nodes));
            }

            return checkedNodes;
        }

        private void UpdateListView()
        {
            listViewElements.Items.Clear();

            var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes);
            var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes);
            var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes);

            AddCheckedNodesToListView(checkedElemTypeNodes, 0);
            AddCheckedNodesToListView(checkedChannelNodes, 1);
            AddCheckedNodesToListView(checkedDatabaseNodes, 2);
        }

        private void AddCheckedNodesToListView(IEnumerable<TreeNode> checkedNodes, int columnIndex)
        {
            foreach (var node in checkedNodes)
            {
                // Check if the node is a child node (has no children)
                if (node.Nodes.Count == 0)
                {
                    var newItem = new ListViewItem();

                    // Replace the node separator with the desired separator
                    string fullPath = node.FullPath.Replace("\\", "/");

                    if (columnIndex == 0)
                    {
                        newItem.Text = fullPath;
                    }
                    else
                    {
                        newItem.Text = "";

                        // Add empty subitems for the previous columns
                        for (int i = 0; i < columnIndex; i++)
                        {
                            newItem.SubItems.Add("");
                        }

                        newItem.SubItems.Add(fullPath);
                    }

                    listViewElements.Items.Add(newItem);
                }
            }
        }
    }
}