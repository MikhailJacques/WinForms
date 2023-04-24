using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ElementSearch
{
    public partial class ElementSearch : Form
    {
        private bool isTreeViewClearing = false;
        private bool isTreeViewUpdating = false;
        private ListViewColumnSorter listViewColumnSorter;

        private Dictionary<uint, string> typeById = new Dictionary<uint, string>();
        private Dictionary<uint, string> channelById = new Dictionary<uint, string>();
        private Dictionary<uint, string> databaseById = new Dictionary<uint, string>();

        private Dictionary<uint, Element> elementById = new Dictionary<uint, Element>();
        private Dictionary<uint, ListViewItem> listViewItemById = new Dictionary<uint, ListViewItem>();

        private List<ListViewItem> listViewItemAll = new List<ListViewItem>();
        private HashSet<uint> listViewItemIds = new HashSet<uint>();

        public ElementSearch()
        {
            InitializeComponent();
            LoadData();
            InitializeSorter();
        }

        private void LoadData()
        {
            //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            //string typeFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_type.txt");
            //string channelFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_channel.txt");
            //string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_database.txt");
            //string elementFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elements.txt");

            string typeFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_lst_LogData_type.txt");
            string channelFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_lst_LogData_channel.txt");
            string databaseFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_lst_LogData_database.txt");
            string elementFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "_lst_LogData_elements.txt");

            string[] filesPaths = { typeFilePath, channelFilePath, databaseFilePath, elementFilePath };

            var fileTokens = filesPaths.Select(ReadTextFile).ToArray();

            var fillTreeViewTasks = new[]
            {
                Task.Factory.StartNew(() => FillTreeView(treeViewType, fileTokens[0])),
                Task.Factory.StartNew(() => FillTreeView(treeViewChannel, fileTokens[1])),
                Task.Factory.StartNew(() => FillTreeView(treeViewDatabase, fileTokens[2]))
            };

            Task.Factory.ContinueWhenAll(fillTreeViewTasks, _ =>
            {
                foreach (var lineTokens in fileTokens[3])
                {
                    Element element = new Element();

                    element.ID = (uint.TryParse(lineTokens[0], out uint id)) ? id : uint.MaxValue;

                    element.LongName = lineTokens[1];
                    //element.LongName = (lineTokens[1].Equals("INVALID")) ? "NO NAME" : lineTokens[1];

                    element.ShortName = lineTokens[2];
                    //element.ShortName = (lineTokens[2].Equals("INVALID")) ? "NO NAME" : lineTokens[2];
                    
                    element.Type = (uint.TryParse(lineTokens[3], out uint type)) ? type : uint.MaxValue;
                    element.Channel = (uint.TryParse(lineTokens[4], out uint channel)) ? channel : uint.MaxValue;
                    element.Database = (uint.TryParse(lineTokens[5], out uint database)) ? database : uint.MaxValue;

                    element.Location = lineTokens[6];
                    //element.Location = (lineTokens[6].Equals("INVALID")) ? "NO LOCATION" : lineTokens[6];

                    element.Handle = (uint.TryParse(lineTokens[7], out uint handle)) ? handle : uint.MaxValue;

                    elementById[id] = element;
                }

                FillDictionaries(typeById, fileTokens[0]);
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
                    var tokens = line.Split('@').Select(token => string.IsNullOrEmpty(token) ? "INVALID" : token).ToList();
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

        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isTreeViewUpdating || isTreeViewClearing)
            {
                return;
            }

            TreeView treeView = sender as TreeView;

            if (e.Node is MyTreeNode)
            {
                isTreeViewUpdating = true;
                UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
                UpdateListViewForNodeHierarchy(treeView, e.Node, e.Node.Checked);
                isTreeViewUpdating = false;
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

        private void UpdateListViewForNodeHierarchy(TreeView treeView, TreeNode node, bool isChecked)
        {
            if (node is MyTreeNode myNode)
            {
                uint id = myNode._ID;

                if (treeView == treeViewType)
                {
                    UpdateListViewForType(id, isChecked);
                }
                else if (treeView == treeViewChannel)
                {
                    UpdateListViewForChannel(id, isChecked);
                }
                else if (treeView == treeViewDatabase)
                {
                    UpdateListViewForDatabase(id, isChecked);
                }
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                UpdateListViewForNodeHierarchy(treeView, childNode, isChecked);
            }
        }

        private void UpdateListViewForType(uint typeId, bool isChecked)
        {
            var matchingElementIds = elementById.Values
                .Where(element => element.Type == typeId)
                .Select(element => element.ID);

            UpdateListView(matchingElementIds, isChecked);
        }

        private void UpdateListViewForChannel(uint channelId, bool isChecked)
        {
            var matchingElementIds = elementById.Values
                .Where(element => element.Channel == channelId)
                .Select(element => element.ID);

            UpdateListView(matchingElementIds, isChecked);
        }

        private void UpdateListViewForDatabase(uint databaseId, bool isChecked)
        {
            var matchingElementIds = elementById.Values
                .Where(element => element.Database == databaseId)
                .Select(element => element.ID);

            UpdateListView(matchingElementIds, isChecked);
        }

        private void UpdateListView(IEnumerable<uint> elementIds, bool isChecked)
        {
            foreach (uint id in elementIds)
            {
                if (isChecked)
                {
                    AddElementToListView(id);
                }
                else
                {
                    RemoveElementFromListView(id);
                }
            }
        }

        private void AddElementToListView(uint nodeId)
        {
            if (elementById.TryGetValue(nodeId, out Element element))
            {
                if (listViewItemIds.Add(nodeId))
                {
                    ListViewItem listViewItem = new ListViewItem(element.ID.ToString());
                    listViewItem.SubItems.Add(element.LongName);
                    listViewItem.SubItems.Add(element.ShortName);
                    listViewItem.SubItems.Add(typeById.ContainsKey(element.Type) ? typeById[element.Type] : string.Empty);
                    listViewItem.SubItems.Add(channelById.ContainsKey(element.Channel) ? channelById[element.Channel] : string.Empty);
                    listViewItem.SubItems.Add(databaseById.ContainsKey(element.Database) ? databaseById[element.Database] : string.Empty);
                    listViewItem.SubItems.Add(element.Location);
                    listViewItem.SubItems.Add(element.Handle.ToString());

                    listViewElements.Items.Add(listViewItem);
                    listViewItemAll.Add(listViewItem);
                    listViewItemById[nodeId] = listViewItem;
                }
            }
        }

        private void RemoveElementFromListView(uint id)
        {
            if (listViewItemById.TryGetValue(id, out ListViewItem itemToRemove))
            {
                listViewItemIds.Remove(id);
                listViewElements.Items.Remove(itemToRemove);
                listViewItemAll.Remove(itemToRemove);               
                listViewItemById.Remove(id);      
            }
        }

        private void SetTreeViewNodeCheckState(TreeNodeCollection nodes, bool checkState)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = checkState;

                if (checkState)
                    node.Expand();
                else
                    node.Collapse();

                SetTreeViewNodeCheckState(node.Nodes, checkState);
            }
        }

        private void ClearTreeView(TreeView treeView)
        {
            isTreeViewClearing = true;

            Stack<TreeNode> nodesToProcess = new Stack<TreeNode>();

            foreach (TreeNode node in treeView.Nodes)
            {
                nodesToProcess.Push(node);
            }

            while (nodesToProcess.Count > 0)
            {
                TreeNode currentNode = nodesToProcess.Pop();
                currentNode.Checked = false;
                currentNode.Collapse();

                foreach (TreeNode childNode in currentNode.Nodes)
                {
                    nodesToProcess.Push(childNode);
                }
            }

            isTreeViewClearing = false;
        }

        private void SearchAndCheckNode(TreeView treeView, TreeNode node, string searchText, bool check, bool forceCheck = false)
        {
            string nodeTextLower = node.Text.ToLower();
            string searchTextLower = searchText.ToLower();

            if (nodeTextLower.Contains(searchTextLower) || string.IsNullOrEmpty(searchText) || forceCheck)
            {
                ToggleNodeCheckState(treeView, node, check, forceCheck);
                ExpandAncestors(node);
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                SearchAndCheckNode(treeView, childNode, searchText, check, forceCheck);
            }
        }

        private void ToggleNodeCheckState(TreeView treeView, TreeNode node, bool check, bool forceCheck)
        {
            if ((check && !node.Checked) || (!check && node.Checked) || forceCheck)
            {
                node.Checked = check;
                uint nodeId = ((MyTreeNode)node)._ID;

                if (treeView == treeViewType)
                {
                    UpdateListViewForType(nodeId, check);
                }
                else if (treeView == treeViewChannel)
                {
                    UpdateListViewForChannel(nodeId, check);
                }
                else if (treeView == treeViewDatabase)
                {
                    UpdateListViewForDatabase(nodeId, check);
                }
            }
        }

        private void ExpandAncestors(TreeNode node)
        {
            TreeNode currentNode = node;
            while (currentNode.Parent != null)
            {
                currentNode.Parent.Expand();
                currentNode = currentNode.Parent;
            }
        }

        private void CheckBoxElements_CheckedChanged(object sender, EventArgs e)
        {
            bool showSelectedOnly = checkBoxElements.Checked;

            listViewElements.BeginUpdate();
            listViewElements.Items.Clear();

            if (showSelectedOnly)
            {
                foreach (ListViewItem item in listViewItemAll)
                {
                    if (item.Selected)
                    {
                        listViewElements.Items.Add(item);
                    }
                }
            }
            else
            {
                listViewElements.Items.AddRange(listViewItemAll.ToArray());
            }

            listViewElements.EndUpdate();
        }

        private void TextBoxElementName_KeyDown(object sender, KeyEventArgs e)
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

        private void ListViewElements_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
            }

            listViewItemIds.Clear();
            listViewElements.Items.Clear();
            listViewItemAll.Clear();
            listViewItemById.Clear();

            ClearTreeView(treeViewType);
            ClearTreeView(treeViewChannel);
            ClearTreeView(treeViewDatabase);
        }

        private async void ButtonSend_Click(object sender, EventArgs e)
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

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox sourceCheckBox = sender as CheckBox;
            TreeView targetTreeView;

            if (sourceCheckBox == checkBoxType)
            {
                targetTreeView = treeViewType;
            }
            else if (sourceCheckBox == checkBoxChannel)
            {
                targetTreeView = treeViewChannel;
            }
            else //if (sourceCheckBox == checkBoxDatabase)
            {
                targetTreeView = treeViewDatabase;
            }

            SetTreeViewNodeCheckState(targetTreeView.Nodes, sourceCheckBox.Checked);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                var textBox = sender as TextBox;
                string searchText = textBox.Text;
                TreeView targetTreeView = null;

                if (textBox == textBoxType)
                {
                    targetTreeView = treeViewType;
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
                        SearchAndCheckNode(targetTreeView, node, searchText, true);
                    }
                }
            }
        }
    }
}