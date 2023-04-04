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
        private bool isClearingTreeView = false;
        private bool isTreeViewUpdating = false;
        private ListViewColumnSorter listViewColumnSorter;
        private Dictionary<uint, string> elementTypeById = new Dictionary<uint, string>();
        private Dictionary<uint, string> channelById = new Dictionary<uint, string>();
        private Dictionary<uint, string> databaseById = new Dictionary<uint, string>();
        private Dictionary<uint, ElementData> elementDataById = new Dictionary<uint, ElementData>();
        private HashSet<uint> uniqueElementIDs = new HashSet<uint>();
        private List<uint> selectedListViewElements = new List<uint>();
        private List<uint> unselectedListViewElements = new List<uint>();

        public ElementSearch()
        {
            InitializeComponent();
            LoadData();
            InitializeSorter();
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

        private void InitializeSorter()
        {
            listViewColumnSorter = new ListViewColumnSorter();
            listViewColumnSorter.NumericColumns.Add(0);
            listViewElements.ListViewItemSorter = listViewColumnSorter;
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

        private void FillDictionaries(Dictionary<uint, string> dict, List<List<string>> fileTokens)
        {
            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count != 2)
                    continue;

                uint.TryParse(lineTokens[0], out uint id);
                dict[id] = lineTokens[1];
            }
        }

        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
        {
            var nodeLookup = new Dictionary<string, MyTreeNode>();

            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count != 2)
                    continue;

                uint.TryParse(lineTokens[0], out uint id);
                string family_hierarchy = lineTokens[1];

                var relatives = family_hierarchy.Split('/').ToList();
                AddNode(treeView.Nodes, relatives, 0, id, nodeLookup);
            }
        }

        private void AddNode(TreeNodeCollection nodes, List<string> family, int index, uint id, Dictionary<string, MyTreeNode> nodeLookup)
        {
            if (index < family.Count)
            {
                var currentNodeKey = string.Join("/", family.Take(index + 1));

                if (!nodeLookup.TryGetValue(currentNodeKey, out MyTreeNode currentNode))
                {
                    // Set the ID only when we reach the last relative in the family
                    currentNode = new MyTreeNode(family[index], index == family.Count - 1 ? id : 0);

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

                AddNode(currentNode.Nodes, family, index + 1, id, nodeLookup);
            }
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
                //if (IsLeafNode(node))
                //{
                    if (elementDataById.TryGetValue(myNode._ID, out ElementData element))
                    {
                        if (isChecked)
                        {
                            AddElementToListView(element.ID);
                        }
                        else
                        {
                            RemoveElementFromListView(element.ID);
                        }
                    }
                //}
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateListViewForNodeHierarchy(childNode, isChecked);
            }
        }

        private void AddElementToListView(uint nodeId)
        {
            if (elementDataById.TryGetValue(nodeId, out ElementData elementData))
            {
                if (uniqueElementIDs.Add(nodeId))
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
                uniqueElementIDs.Remove(id);
            }
        }

        private void SetTreeViewNodeCheckState(TreeNodeCollection nodes, bool checkState)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = checkState;
                SetTreeViewNodeCheckState(node.Nodes, checkState);
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
            isClearingTreeView = true;
            parentNode.Checked = false;
            isClearingTreeView = false;

            parentNode.Collapse();

            foreach (TreeNode childNode in parentNode.Nodes)
            {
                UncheckAndCollapseNodes(childNode);
            }
        }

        private void SearchAndCheckNode(TreeNode node, string searchText, bool check, bool forceCheck = false)
        {
            string nodeTextLower = node.Text.ToLower();
            string searchTextLower = searchText.ToLower();

            if (nodeTextLower.Contains(searchTextLower) || searchText == "" || forceCheck)
            {
                if ((check && !node.Checked) || (!check && node.Checked) || forceCheck)
                {
                    node.Checked = check;
                    uint nodeId = ((MyTreeNode)node)._ID;

                    if (check)
                    {
                        AddElementToListView(nodeId);
                    }
                    else
                    {
                        RemoveElementFromListView(nodeId);
                    }
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
                SearchAndCheckNode(childNode, searchText, check, forceCheck);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var textBox = sender as TextBox;
                string searchText = textBox.Text;
                TreeView targetTreeView = null;

                if (textBox == textBoxElementType)
                {
                    targetTreeView = treeViewElementType;
                }
                else if (textBox == textBoxChannel)
                {
                    targetTreeView = treeViewChannel;
                }
                else if (textBox == textBoxDatabase)
                {
                    targetTreeView = treeViewDatabase;
                }

                if (searchText.Length >= 3 && targetTreeView != null)
                {
                    foreach (TreeNode node in targetTreeView.Nodes)
                    {
                        SearchAndCheckNode(node, searchText, true);
                    }
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

        private void listViewElements_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == listViewColumnSorter.Column)
            {
                if (listViewColumnSorter.Order == SortOrder.Ascending)
                {
                    listViewColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    listViewColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                listViewColumnSorter.Column = e.Column;
                listViewColumnSorter.Order = SortOrder.Ascending;
            }

            listViewElements.Sort();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxElementType.Clear();
            textBoxChannel.Clear();
            textBoxDatabase.Clear();
            textBoxElementName.Clear();

            checkBoxElementType.Checked = false;
            checkBoxChannel.Checked = false;
            checkBoxDatabase.Checked = false;
            checkBoxElements.Checked = false;

            ClearTreeView(treeViewElementType);
            ClearTreeView(treeViewChannel);
            ClearTreeView(treeViewDatabase);

            uniqueElementIDs.Clear();
            listViewElements.Items.Clear();
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string selectedElementIDs = string.Join(",", listViewElements.SelectedItems.Cast<ListViewItem>().Select(item => item.SubItems[0].Text));

            await SendDataToPipeAsync(selectedElementIDs);
        }

        private async Task SendDataToPipeAsync(string data)
        {
            string pipeName = "Pipe_Element_ID";
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
            {
                try
                {
                    await pipeClient.ConnectAsync(3000);

                    using (StreamWriter sw = new StreamWriter(pipeClient, Encoding.ASCII))
                    {
                        await sw.WriteLineAsync(data);
                        await sw.FlushAsync();
                    }
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("The receiving application is not running.", "Connection Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBoxElementType_CheckedChanged(object sender, EventArgs e)
        {
            SetTreeViewNodeCheckState(treeViewElementType.Nodes, checkBoxElementType.Checked);
        }

        private void checkBoxChannel_CheckedChanged(object sender, EventArgs e)
        {
            SetTreeViewNodeCheckState(treeViewChannel.Nodes, checkBoxChannel.Checked);
        }

        private void checkBoxDatabase_CheckedChanged(object sender, EventArgs e)
        {
            SetTreeViewNodeCheckState(treeViewDatabase.Nodes, checkBoxDatabase.Checked);
        }

        private void textBoxElementType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string searchText = textBoxElementType.Text;
                if (searchText.Length >= 3)
                {
                    foreach (TreeNode node in treeViewElementType.Nodes)
                    {
                        SearchAndCheckNode(node, searchText, true);
                    }
                }
            }
        }

        private void textBoxChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string searchText = textBoxChannel.Text;
                if (searchText.Length >= 3)
                {
                    foreach (TreeNode node in treeViewChannel.Nodes)
                    {
                        SearchAndCheckNode(node, searchText, true);
                    }
                }
            }
        }

        private void textBoxDatabase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                string searchText = textBoxDatabase.Text;
                if (searchText.Length >= 3)
                {
                    foreach (TreeNode node in treeViewDatabase.Nodes)
                    {
                        SearchAndCheckNode(node, searchText, true);
                    }
                }
            }
        }

        private void treeViewElementType_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeView_AfterCheck(sender, e);
        }

        private void treeViewChannel_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeView_AfterCheck(sender, e);
        }

        private void treeViewDatabase_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeView_AfterCheck(sender, e);
        }

        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isTreeViewUpdating || isClearingTreeView)
            {
                return;
            }

            if (e.Node is MyTreeNode myNode)
            {
                isTreeViewUpdating = true;
                UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
                UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
                SearchAndCheckNode(e.Node, "", e.Node.Checked, true);
                isTreeViewUpdating = false;
            }
        }

        private void checkBoxElements_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxElements.Checked)
            {
                selectedListViewElements.Clear();
                unselectedListViewElements.Clear();

                foreach (ListViewItem item in listViewElements.Items)
                {
                    if (item.Selected)
                        selectedListViewElements.Add(uint.Parse(item.SubItems[0].Text));
                    else
                        unselectedListViewElements.Add(uint.Parse(item.SubItems[0].Text));
                }

                // Clear the ListView and HashSet
                listViewElements.Items.Clear();
                uniqueElementIDs.Clear();

                // Add only the selected items back to the ListView and HashSet
                if (selectedListViewElements.Count > 0)
                {
                    foreach (uint selectedElementId in selectedListViewElements)
                    {
                        AddElementToListView(selectedElementId);
                    }
                }
            }
            else
            {
                // Clear the ListView and HashSet
                listViewElements.Items.Clear();
                uniqueElementIDs.Clear();

                foreach (uint selectedElementId in selectedListViewElements)
                {
                    AddElementToListView(selectedElementId);
                }

                foreach (uint unselectedElementId in unselectedListViewElements)
                {
                    AddElementToListView(unselectedElementId);
                }
            }
        }
    }
}