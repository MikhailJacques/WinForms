using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSearch
{
    public partial class ElementSearch : Form
    {
        private bool isUpdating = false;
        private Dictionary<uint, ElementData> elementDataById = new Dictionary<uint, ElementData>();
        private Dictionary<uint, string> elementTypeById = new Dictionary<uint, string>();
        private Dictionary<uint, string> channelById = new Dictionary<uint, string>();
        private Dictionary<uint, string> databaseById = new Dictionary<uint, string>();

        public ElementSearch()
        {
            InitializeComponent();
            LoadData();
            AddTreeViewEventHandlers();
        }
        private void LoadData()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string elementTypeFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_element_type.txt");
            string channelFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_channel.txt");
            string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_database.txt");
            string elementDataFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_element_all.txt");

            string[] filesPaths = { elementTypeFilePath, channelFilePath, databaseFilePath, elementDataFilePath };

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

                    elementDataById[id] = new ElementData
                    {
                        ID = id,
                        LongName = lineTokens[1],
                        ShortName = lineTokens[2],
                        ElementType = uint.Parse(lineTokens[3]),
                        Channel = uint.Parse(lineTokens[4]),
                        Database = uint.Parse(lineTokens[5]),
                        Location = lineTokens[6],
                        Handle = uint.Parse(lineTokens[7]),
                    };
                }

                FillDictionaries(elementTypeById, fileTokens[0]);
                FillDictionaries(channelById, fileTokens[1]);
                FillDictionaries(databaseById, fileTokens[2]);
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

                var relatives = family_hierarhy.Split('/').Select(relative => new MyTreeNode(relative, id)).ToList();

                AddNode(treeView.Nodes, relatives, 0, nodeLookup);
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

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => nodes.Add(currentNode)));
                    }
                    else
                    {
                        nodes.Add(currentNode);
                    }

                    nodeLookup[currentNodeKey] = currentNode;
                }

                AddNode(currentNode.Nodes, family, index + 1, nodeLookup);
            }
        }

        private void AddTreeViewEventHandlers()
        {
            treeViewElementType.AfterCheck += (sender, e) =>
            {
                if (isUpdating)
                {
                    return;
                }

                if (e.Node is MyTreeNode myNode)
                {
                    isUpdating = true;
                    UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
                    UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
                    isUpdating = false;
                }
            };

            treeViewChannel.AfterCheck += (sender, e) =>
            {
                if (isUpdating)
                {
                    return;
                }

                if (e.Node is MyTreeNode myNode)
                {
                    isUpdating = true;
                    UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
                    UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
                    isUpdating = false;
                }
            };

            treeViewDatabase.AfterCheck += (sender, e) =>
            {
                if (isUpdating)
                {
                    return;
                }

                if (e.Node is MyTreeNode myNode)
                {
                    isUpdating = true;
                    UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
                    UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
                    isUpdating = false;
                }
            };
        }

        private void UpdateChildNodesCheckedState(TreeNode parentNode, bool isChecked)
        {
            foreach (TreeNode childNode in parentNode.Nodes)
            {
                childNode.Checked = isChecked;
                UpdateChildNodesCheckedState(childNode, isChecked);
            }
        }

        private bool IsLeafNode(TreeNode node)
        {
            return node.Nodes.Count == 0;
        }

        private void UpdateListViewForNodeHierarchy(TreeNode node, bool isChecked)
        {
            if (node is MyTreeNode myNode)
            {
                if (IsLeafNode(node))
                {
                    if (elementDataById.TryGetValue(myNode._ID, out ElementData element))
                    {
                        if (isChecked)
                        {
                            AddElementToListView(element);
                        }
                        else
                        {
                            RemoveElementFromListView(element.ID);
                        }
                    }
                }
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateListViewForNodeHierarchy(childNode, isChecked);
            }
        }

        private void AddElementToListView(ElementData element)
        {
            var item = new ListViewItem(element.ID.ToString());
            item.SubItems.Add(element.LongName);
            item.SubItems.Add(element.ShortName);

            item.SubItems.Add(elementTypeById.ContainsKey(element.ElementType) ? elementTypeById[element.ElementType] : string.Empty);
            item.SubItems.Add(channelById.ContainsKey(element.Channel) ? channelById[element.Channel] : string.Empty);
            item.SubItems.Add(databaseById.ContainsKey(element.Database) ? databaseById[element.Database] : string.Empty);

            item.SubItems.Add(element.Location);
            item.SubItems.Add(element.Handle.ToString());

            listViewElements.Items.Add(item);
        }

        private void RemoveElementFromListView(uint id)
        {
            ListViewItem itemToRemove = null;

            foreach (ListViewItem item in listViewElements.Items)
            {
                if (item.Text == id.ToString())
                {
                    itemToRemove = item;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                listViewElements.Items.Remove(itemToRemove);
            }
        }

        private void AddNodeDataToListView(uint nodeId)
        {
            if (elementDataById.TryGetValue(nodeId, out ElementData elementData))
            {
                ListViewItem listViewItem = new ListViewItem(elementData.ID.ToString());
                listViewItem.SubItems.Add(elementData.LongName);
                listViewItem.SubItems.Add(elementData.ShortName);
                listViewItem.SubItems.Add(elementTypeById.ContainsKey(elementData.ElementType) ? elementTypeById[elementData.ElementType] : string.Empty);
                listViewItem.SubItems.Add(channelById.ContainsKey(elementData.Channel) ? channelById[elementData.Channel] : string.Empty);
                listViewItem.SubItems.Add(databaseById.ContainsKey(elementData.Database) ? databaseById[elementData.Database] : string.Empty);
                listViewItem.SubItems.Add(elementData.Location);
                listViewItem.SubItems.Add(elementData.Handle.ToString());
                listViewElements.Items.Add(listViewItem);
            }
        }

        private void RemoveNodeDataFromListView(uint nodeId)
        {
            ListViewItem itemToRemove = null;

            foreach (ListViewItem listViewItem in listViewElements.Items)
            {
                if (listViewItem.Text == nodeId.ToString())
                {
                    itemToRemove = listViewItem;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                listViewElements.Items.Remove(itemToRemove);
            }
        }

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

        private void textBoxElementName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string searchText = textBoxElementName.Text;

                if (searchText.Length >= 3)
                {
                    foreach (ListViewItem item in listViewElements.Items)
                    {
                        item.Selected = false;
                    }

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

        private void textBoxElementType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchText = textBoxElementType.Text;
                if (searchText.Length >= 3)
                {
                    foreach (TreeNode node in treeViewElementType.Nodes)
                    {
                        SearchAndCheckNode(node, searchText);
                    }
                }
            }
        }

        private void textBoxChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchText = textBoxChannel.Text;
                if (searchText.Length >= 3)
                {
                    foreach (TreeNode node in treeViewChannel.Nodes)
                    {
                        SearchAndCheckNode(node, searchText);
                    }
                }
            }
        }

        private void textBoxDatabase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchText = textBoxDatabase.Text;
                if (searchText.Length >= 3)
                {
                    foreach (TreeNode node in treeViewDatabase.Nodes)
                    {
                        SearchAndCheckNode(node, searchText);
                    }
                }
            }
        }

        private void SearchAndCheckNode(TreeNode node, string searchText)
        {
            if (node.Text.Contains(searchText))
            {
                if (!node.Checked)
                {
                    node.Checked = true;
                    uint nodeId = ((MyTreeNode)node)._ID;
                    AddNodeDataToListView(nodeId);
                }

                TreeNode currentNode = node;
                while (currentNode.Parent != null)
                {
                    currentNode.Parent.Expand();
                    currentNode = currentNode.Parent;
                }
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                SearchAndCheckNode(childNode, searchText);
            }
        }
    }
}