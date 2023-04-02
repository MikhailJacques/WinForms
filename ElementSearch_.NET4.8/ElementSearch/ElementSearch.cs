using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace ElementSearch
{
    public partial class FormElementSearch : Form
    {
        public FormElementSearch()
        {
            InitializeComponent();
        }

        private Dictionary<uint, string> _elementTypeById = new Dictionary<uint, string>();
        private Dictionary<uint, string> _channelById = new Dictionary<uint, string>();
        private Dictionary<uint, string> _databaseById = new Dictionary<uint, string>();
        private Dictionary<uint, ElementData> _elementDataById = new Dictionary<uint, ElementData>();

        //private Dictionary<uint, NamedEntity> _elementTypeDataById = new Dictionary<uint, NamedEntity>();
        //private Dictionary<uint, NamedEntity> _channelDataById = new Dictionary<uint, NamedEntity>();
        //private Dictionary<uint, NamedEntity> _databaseDataById = new Dictionary<uint, NamedEntity>();

        private void FormElementSearch_Load(object sender, EventArgs e)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? Environment.CurrentDirectory;

            string elmentTypeFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_type.txt");
            string channelFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_chn.txt");
            string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_dbs.txt");
            string elementDataFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_all.txt");

            string[] filesPaths = { elmentTypeFilePath, channelFilePath, databaseFilePath, elementDataFilePath };

            var fileTokens = filesPaths.Select(ReadTextFile).ToArray();

            var fillTreeViewTasks = new[]
            {
                Task.Factory.StartNew(() => FillTreeView(treeViewElementType, fileTokens[0])),
                Task.Factory.StartNew(() => FillTreeView(treeViewChannel, fileTokens[1])),
                Task.Factory.StartNew(() => FillTreeView(treeViewDatabase, fileTokens[2]))
            };

            Task.Factory.ContinueWhenAll(fillTreeViewTasks, _ =>
            {
                foreach (var lineTokens in fileTokens[3])
                {
                    if (lineTokens.Count < 8)
                        continue;

                    uint.TryParse(lineTokens[0], out uint id);
                    uint.TryParse(lineTokens[7], out uint handle);

                    _elementDataById[id] = new ElementData
                    {
                        ID = id,
                        LongName = lineTokens[1],
                        ShortName = lineTokens[2],
                        ElementType = lineTokens[3],
                        Channel = lineTokens[4],
                        Database = lineTokens[5],
                        Location = lineTokens[6],
                        Handle = handle
                    };
                }

                FillDictionaries(_elementTypeById, fileTokens[0]);
                FillDictionaries(_channelById, fileTokens[1]);
                FillDictionaries(_databaseById, fileTokens[2]);
            });
        }

        private void FillDictionaries(Dictionary<uint, string> dict, List<List<string>> fileTokens)
        {
            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count < 3)
                    continue;

                uint.TryParse(lineTokens[0], out uint id);
                dict[id] = lineTokens[1];
            }
        }

        //private void FillDictionaries(Dictionary<uint, NamedEntity> dictionary, IReadOnlyList<List<string>> fileTokens)
        //{
        //    foreach (var tokens in fileTokens)
        //    {
        //        if (tokens.Count < 2)
        //            continue;

        //        uint.TryParse(tokens[0], out uint id);
        //        dictionary[id] = new NamedEntity { ID = id, Name = tokens[1] };
        //    }
        //}

        private List<List<string>> ReadTextFile(string filePath)
        {
            var fileTokens = new List<List<string>>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
                    fileTokens.Add(tokens);
                }
            }

            return fileTokens;
        }

        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
        {
            var nodeLookup = new Dictionary<string, MyTreeNode>();

            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count < 3)
                    continue;

                uint.TryParse(lineTokens[0], out uint id);
                string family_hierarhy = lineTokens[1];
                uint.TryParse(lineTokens[2], out uint handle);

                var relatives = family_hierarhy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

                treeView.Invoke(new Action(() =>
                {
                    AddNode(treeView.Nodes, relatives, 0, nodeLookup);
                }));
            }
        }

        private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index, Dictionary<string, MyTreeNode> nodeLookup)
        {
            if (index < family.Count)
            {
                var currentRelative = family[index];
                var currentNodeKey = currentRelative.Text;

                if (!nodeLookup.TryGetValue(currentNodeKey, out MyTreeNode currentNode))
                {
                    currentNode = currentRelative;
                    nodes.Add(currentNode);
                    nodeLookup[currentNodeKey] = currentNode;
                }

                AddNode(currentNode.Nodes, family, index + 1, nodeLookup);
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

        private void UpdateParentNode(MyTreeNode node)
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

        private void treeViewElementType_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                MyTreeNode node = e.Node as MyTreeNode;
                if (node != null)
                {
                    UpdateChildNodes(node, node.Checked);
                    UpdateParentNode(node.Parent as MyTreeNode);
                }
            }

            UpdateListView();
        }

        private void treeViewChannel_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                MyTreeNode node = e.Node as MyTreeNode;
                if (node != null)
                {
                    UpdateChildNodes(node, node.Checked);
                    UpdateParentNode(node.Parent as MyTreeNode);
                }
            }

            UpdateListView();
        }

        private void treeViewDatabase_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                MyTreeNode node = e.Node as MyTreeNode;
                if (node != null)
                {
                    UpdateChildNodes(node, node.Checked);
                    UpdateParentNode(node.Parent as MyTreeNode);
                }
            }

            UpdateListView();
        }

        private void HandleTreeViewAfterCheck(object sender, TreeViewEventArgs e)
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
                if (node.Checked && node.Nodes.Count == 0)
                {
                    TreeNode fullPathNode = new TreeNode(node.FullPath);
                    fullPathNode.Tag = node; // Store the original node in the Tag property
                    checkedNodes.Add(fullPathNode);
                }

                checkedNodes.AddRange(GetCheckedNodes(node.Nodes));
            }

            return checkedNodes;
        }

        //private void UpdateListView()
        //{
        //    listViewElements.Items.Clear();

        //    var checkedElemTypeNodes = GetCheckedNodes(treeViewElementType.Nodes).OfType<TreeNode>().ToList();
        //    var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<TreeNode>().ToList();
        //    var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<TreeNode>().ToList();

        //    var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => (node.Tag as MyTreeNode)?._ID ?? 0));

        //    foreach (var id in _elementDataById.Keys)
        //    {
        //        if (allCheckedNodeIds.Contains(id))
        //        {
        //            ElementData elementData = _elementDataById[id];

        //            ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
        //            newItem.SubItems.Add(elementData.LongName);
        //            newItem.SubItems.Add(elementData.ShortName);

        //            newItem.SubItems.Add(uint.TryParse(elementData.ElementType, out uint elementTypeID) && _elementTypeById.TryGetValue(elementTypeID, out string elementType) ? elementType : elementData.ElementType);
        //            newItem.SubItems.Add(uint.TryParse(elementData.Channel, out uint channelID) && _channelById.TryGetValue(channelID, out string channel) ? channel : elementData.Channel);
        //            newItem.SubItems.Add(uint.TryParse(elementData.Database, out uint databaseID) && _databaseById.TryGetValue(databaseID, out string database) ? database : elementData.Database);

        //            newItem.SubItems.Add(elementData.Location);
        //            newItem.SubItems.Add(elementData.Handle.ToString());

        //            listViewElements.Items.Add(newItem);
        //        }
        //    }
        //}

        private void UpdateListView()
        {
            listViewElements.Items.Clear();

            var checkedElemTypeNodes = GetCheckedNodes(treeViewElementType.Nodes).OfType<TreeNode>().ToList();
            var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<TreeNode>().ToList();
            var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<TreeNode>().ToList();

            var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => (node.Tag as MyTreeNode)?._ID ?? 0));

            foreach (var id in _elementDataById.Keys)
            {
                if (allCheckedNodeIds.Contains(id))
                {
                    ElementData elementData = _elementDataById[id];

                    ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
                    newItem.SubItems.Add(elementData.LongName);
                    newItem.SubItems.Add(elementData.ShortName);

                    newItem.SubItems.Add(uint.TryParse(elementData.ElementType, out uint elementTypeID) && _elementTypeById.TryGetValue(elementTypeID, out string elementType) ? elementType : elementData.ElementType);
                    newItem.SubItems.Add(uint.TryParse(elementData.Channel, out uint channelID) && _channelById.TryGetValue(channelID, out string channel) ? channel : elementData.Channel);
                    newItem.SubItems.Add(uint.TryParse(elementData.Database, out uint databaseID) && _databaseById.TryGetValue(databaseID, out string database) ? database : elementData.Database);

                    newItem.SubItems.Add(elementData.Location);
                    newItem.SubItems.Add(elementData.Handle.ToString());

                    listViewElements.Items.Add(newItem);
                }
            }
        }

        private void FindAndCheckNodes(TreeView treeView, string nameToFind)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                FindNodesByName(node, nameToFind);
            }
        }

        private bool FindNodesByName(TreeNode parentNode, string nameToFind)
        {
            bool foundNode = false;
            if (parentNode.Text.IndexOf(nameToFind, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                parentNode.Checked = true;
                foundNode = true;

                if (parentNode.Parent != null)
                {
                    parentNode.Parent.Expand();
                }
                else
                {
                    parentNode.Expand();
                }
            }

            foreach (TreeNode childNode in parentNode.Nodes)
            {
                bool childFound = FindNodesByName(childNode, nameToFind);
                foundNode = foundNode || childFound;
            }

            return foundNode;
        }

        //private bool FindNodesByName(Dictionary<uint, ElementTypeData> data, string nameToFind)
        //{
        //    var foundItems = data.Values.Where(d => d.Name.IndexOf(nameToFind, StringComparison.OrdinalIgnoreCase) >= 0).Select(d => d.ID).ToList();

        //    return foundItems;
        //}

        private void ClearTreeView(TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                UncheckAndCollapseNodes(node);
            }
        }

        private void UncheckAndCollapseNodes(TreeNode parentNode)
        {
            parentNode.Checked = false;
            parentNode.Collapse();

            foreach (TreeNode childNode in parentNode.Nodes)
            {
                UncheckAndCollapseNodes(childNode);
            }
        }

        private void textBoxElementType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string elemTypeToFind = textBoxElementType.Text;
                if (elemTypeToFind.Length >= 3)
                {
                    foreach (MyTreeNode node in treeViewElementType.Nodes)
                    {
                        FindNodesByName(node, elemTypeToFind);
                    }

                    UpdateListView();
                }
            }
        }

        //private void textBoxElementType_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.Handled = true;
        //        e.SuppressKeyPress = true;

        //        string elemTypeToFind = textBoxElementType.Text;
        //        if (elemTypeToFind.Length >= 3)
        //        {
        //            var foundIds = FindNodesByName(_elementTypeDataById, elemTypeToFind);
        //            UpdateTreeView(treeViewElementType, foundIds);
        //            UpdateListView();
        //        }
        //    }
        //}

        private void UpdateTreeView(TreeView treeView, List<uint> idsToDisplay)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                MyTreeNode myNode = node.Tag as MyTreeNode;
                node.Checked = idsToDisplay.Contains(myNode._ID);
            }
        }

        private void textBoxChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string channelToFind = textBoxChannel.Text;
                if (channelToFind.Length >= 3)
                {
                    foreach (MyTreeNode node in treeViewChannel.Nodes)
                    {
                        FindNodesByName(node, channelToFind);
                    }

                    UpdateListView();
                }
            }
        }

        private void textBoxDatabase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string databaseToFind = textBoxDatabase.Text;
                if (databaseToFind.Length >= 3)
                {
                    foreach (MyTreeNode node in treeViewDatabase.Nodes)
                    {
                        FindNodesByName(node, databaseToFind);
                    }

                    UpdateListView();
                }
            }
        }

        private void textBoxElementName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string searchText = textBoxElementName.Text;

                if (searchText.Length >= 3)
                {
                    // Deselect all items in the ListView
                    foreach (ListViewItem item in listViewElements.Items)
                    {
                        item.Selected = false;
                    }

                    // Search and select matching items
                    foreach (ListViewItem item in listViewElements.Items)
                    {
                        string longName = item.SubItems[1].Text;
                        string shortName = item.SubItems[2].Text;

                        if (longName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            shortName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                        }
                    }
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxElementType.Clear();
            textBoxChannel.Clear();
            textBoxDatabase.Clear();
            textBoxElementName.Clear();

            ClearTreeView(treeViewElementType);
            ClearTreeView(treeViewChannel);
            ClearTreeView(treeViewDatabase);

            listViewElements.Items.Clear();
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string selectedElementIDs = string.Join(",", listViewElements.SelectedItems.Cast<ListViewItem>().Select(item => item.SubItems[0].Text));

            await Task.Run(() => SendDataToPipe(selectedElementIDs));
        }

        private void SendDataToPipe(string data)
        {
            string pipeName = "Pipe_Element_ID";
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
            {
                try
                {
                    pipeClient.Connect(3000);

                    using (StreamWriter sw = new StreamWriter(pipeClient, Encoding.ASCII))
                    {
                        sw.WriteLine(data);
                        sw.Flush();
                    }
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("The receiving application is not running.", "Connection Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}