//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;
//using ElementSearch;

//namespace ElementSearchApp
//{
//    public partial class ElementSearch : Form
//    {
//        private TreeView treeViewDatabase;
//        private ListView listViewElements;
//        private Dictionary<uint, ElementData> elementById;

//        public ElementSearch()
//        {
//            InitializeComponent();
//            InitializeTreeViewAndListView();
//            LoadData();
//            LoadListViewData();
//            AddTreeViewEventHandlers();
//        }

//        private void InitializeTreeViewAndListView()
//        {
//            treeViewDatabase = new TreeView();
//            listViewElements = new ListView();
//            // Add necessary columns to listViewElements
//            // Set other required properties for treeViewDatabase and listViewElements
//            // Add treeViewDatabase and listViewElements to the form's Controls collection
//        }

//        private void LoadData()
//        {
//            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
//            string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_dbs.txt");

//            var fileTokens = new List<List<string>>();

//            using (StreamReader reader = new StreamReader(databaseFilePath))
//            {
//                string line;
//                while ((line = reader.ReadLine()) != null)
//                {
//                    var tokens = line.Split('@').ToList();
//                    fileTokens.Add(tokens);
//                }
//            }

//            FillTreeView(treeViewDatabase, fileTokens);
//        }

//        private void LoadListViewData()
//        {
//            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
//            string elementFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_all.txt");

//            elementById = new Dictionary<uint, ElementData>();

//            using (StreamReader reader = new StreamReader(elementFilePath))
//            {
//                string line;
//                while ((line = reader.ReadLine()) != null)
//                {
//                    var tokens = line.Split('@');
//                    var element = new ElementData
//                    {
//                        ID = uint.Parse(tokens[0]),
//                        LongName = tokens[1],
//                        ShortName = tokens[2],
//                        Type = tokens[3],
//                        Channel = tokens[4],
//                        Database = tokens[5],
//                        Location = tokens[6],
//                        Handle = uint.Parse(tokens[7])
//                    };

//                    elementById[element.ID] = element;
//                }
//            }
//        }

//        private void AddTreeViewEventHandlers()
//        {
//            treeViewDatabase.AfterCheck += (sender, e) =>
//            {
//                if (e.Node is MyTreeNode myNode)
//                {
//                    if (elementById.TryGetValue(myNode._ID, out ElementData element))
//                    {
//                        if (e.Node.Checked)
//                        {
//                            AddElementToListView(element);
//                        }
//                        else
//                        {
//                            RemoveElementFromListView(element.ID);
//                        }
//                    }
//                }
//            };
//        }

//        private void AddElementToListView(ElementData element)
//        {
//            var item = new ListViewItem(element.ID.ToString());
//            item.SubItems.Add(element.LongName);
//            item.SubItems.Add(element.ShortName);
//            item.SubItems.Add(element.Type);
//            item.SubItems.Add(element.Channel);
//            item.SubItems.Add(element.Database);
//            item.SubItems.Add(element.Location);
//            item.SubItems.Add(element.Handle.ToString());

//            listViewElements.Items.Add(item);
//        }

//        private void RemoveElementFromListView(uint id)
//        {
//            ListViewItem itemToRemove = null;

//            foreach (ListViewItem item in listViewElements.Items)
//            {
//                if (item.Text == id.ToString())
//                {
//                    itemToRemove = item;
//                    break;
//                }
//            }

//            if (itemToRemove != null)
//            {
//                listViewElements.Items.Remove(itemToRemove);
//            }
//        }
//    }
//}

//private void treeViewDatabase_AfterCheck(object sender, TreeViewEventArgs e)
//{
//    // Update the checked state of all children nodes recursively
//    UpdateChildrenNodesCheckedState(e.Node, e.Node.Checked);

//    // Update the ListView based on the checked state of the node
//    if (e.Node.Checked)
//    {
//        AddNodeDataToListView((uint)e.Node.Tag);
//    }
//    else
//    {
//        RemoveNodeDataFromListView((uint)e.Node.Tag);
//    }
//}

//// Recursively update the checked state of all children nodes
//private void UpdateChildrenNodesCheckedState(TreeNode parentNode, bool isChecked)
//{
//    foreach (TreeNode childNode in parentNode.Nodes)
//    {
//        childNode.Checked = isChecked;
//        UpdateChildrenNodesCheckedState(childNode, isChecked);
//    }
//}

//private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//{
//    var nodeLookup = new Dictionary<string, MyTreeNode>();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (lineTokens.Count < 3)
//            continue;

//        uint.TryParse(lineTokens[0], out uint id);
//        string family_hierarhy = lineTokens[1];
//        uint.TryParse(lineTokens[2], out uint handle);

//        var relatives = family_hierarhy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//        treeView.Invoke(new Action(() =>
//        {
//            AddNode(treeView.Nodes, relatives, 0, nodeLookup);
//        }));
//    }
//}

//private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index, Dictionary<string, MyTreeNode> nodeLookup)
//{
//    if (index >= family.Count)
//    {
//        return;
//    }

//    MyTreeNode currentNode = family[index];
//    nodes.Add(currentNode);

//    foreach (TreeNode childNode in currentNode.Nodes)
//    {
//        if (nodeLookup.TryGetValue(childNode.Name, out MyTreeNode child))
//        {
//            AddNode(currentNode.Nodes, family, family.IndexOf(child), nodeLookup);
//        }
//    }
//}

//private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index, Dictionary<string, MyTreeNode> nodeLookup)
//{
//    MyTreeNode currentNode = family[index];

//    Create a new TreeNode and set its properties
//    TreeNode newNode = new TreeNode
//    {
//        Text = currentNode.Name,
//        Tag = currentNode._ID // Set the Tag property to the ID of the MyTreeNode
//    };

//    Add the new TreeNode to the nodes collection
//    nodes.Add(newNode);

//    If the current node has children, add them recursively
//    foreach (var childId in currentNode.Children)
//    {
//        if (nodeLookup.TryGetValue(childId, out MyTreeNode childNode))
//        {
//            AddNode(newNode.Nodes, family, family.IndexOf(childNode), nodeLookup);
//        }
//    }
//}

//This implementation handles node checking and unchecking in the TreeView control.
//When a node is checked, it finds the corresponding element data in the collection, and adds it to the ListView.
//When a node is unchecked, it finds and removes the corresponding element data from the ListView.
//Note that this implementation assumes that the elementDataCollection is a collection of ElementData objects.

//private void treeViewDatabase_AfterCheck(object sender, TreeViewEventArgs e)
//{
//    // Check if the event was raised by user interaction, not by code
//    if (e.Action != TreeViewAction.Unknown)
//    {
//        MyTreeNode node = e.Node as MyTreeNode;
//        if (node != null)
//        {
//            uint nodeId = node._ID;

//            if (node.Checked)
//            {
//                // Find the element data in the collection based on the node's ID
//                ElementData element = elementById.FirstOrDefault(el => el.Value.ID == nodeId).Value;

//                if (element != null)
//                {
//                    // Add element data to the ListView control
//                    ListViewItem listViewItem = new ListViewItem(new string[]
//                    {
//                        element.ID.ToString(),
//                        element.LongName,
//                        element.ShortName,
//                        element.Type,
//                        element.Channel,
//                        element.Database,
//                        element.Location,
//                        element.Handle.ToString()
//                    });
//                    listViewElements.Items.Add(listViewItem);
//                }
//            }
//            else
//            {
//                // Find and remove the element data from the ListView control
//                ListViewItem listViewItemToRemove = listViewElements.Items.Cast<ListViewItem>()
//                    .FirstOrDefault(item => uint.TryParse(item.SubItems[0].Text, out uint itemID) && itemID == nodeId);

//                if (listViewItemToRemove != null)
//                {
//                    listViewElements.Items.Remove(listViewItemToRemove);
//                }
//            }
//        }
//    }
//}

//private void AddTreeViewEventHandlers()
//{
//    treeViewDatabase.AfterCheck += (sender, e) =>
//    {
//        if (e.Node is MyTreeNode myNode)
//        {
//            if (elementById.TryGetValue(myNode._ID, out ElementData element))
//            {
//                if (e.Node.Checked)
//                {
//                    AddElementToListView(element);
//                }
//                else
//                {
//                    RemoveElementFromListView(element.ID);
//                }
//            }
//        }
//    };
//}

//private void AddTreeViewEventHandlers()
//{
//    treeViewDatabase.AfterCheck += (sender, e) =>
//    {
//        if (e.Node is MyTreeNode myNode)
//        {
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);

//            if (elementById.TryGetValue(myNode._ID, out ElementData element))
//            {
//                if (e.Node.Checked)
//                {
//                    AddElementToListView(element);
//                }
//                else
//                {
//                    RemoveElementFromListView(element.ID);
//                }
//            }
//        }
//    };
//}

//private void AddTreeViewEventHandlers()
//{
//    treeViewDatabase.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating) // Check if it's updating
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true; // Set the flag to true before updating
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            isUpdating = false; // Set the flag to false after updating

//            if (elementById.TryGetValue(myNode._ID, out ElementData element))
//            {
//                if (e.Node.Checked)
//                {
//                    AddElementToListView(element);
//                }
//                else
//                {
//                    RemoveElementFromListView(element.ID);
//                }
//            }
//        }
//    };
//}

//private void LoadListViewData()
//{
//    string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
//    string elementFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_element_all.txt");

//    elementById = new Dictionary<uint, ElementData>();

//    using (StreamReader reader = new StreamReader(elementFilePath))
//    {
//        string line;
//        while ((line = reader.ReadLine()) != null)
//        {
//            var tokens = line.Split('@');
//            var element = new ElementData
//            {
//                ID = uint.Parse(tokens[0]),
//                LongName = tokens[1],
//                ShortName = tokens[2],
//                Type = tokens[3],
//                Channel = tokens[4],
//                Database = tokens[5],
//                Location = tokens[6],
//                Handle = uint.Parse(tokens[7])
//            };

//            elementById[element.ID] = element;
//        }
//    }
//}

//private void SearchAndCheckNode(TreeNode node, string searchText)
//{
//    if (node.Text.Contains(searchText))
//    {
//        node.Checked = true;
//        uint nodeId = ((MyTreeNode)node)._ID;
//        AddNodeDataToListView(nodeId);
//    }
//    else
//    {
//        node.Checked = false;
//    }

//    foreach (TreeNode childNode in node.Nodes)
//    {
//        SearchAndCheckNode(childNode, searchText);
//    }
//}


//private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index, Dictionary<string, MyTreeNode> nodeLookup)
//{
//    if (index < family.Count)
//    {
//        var currentRelative = family[index];
//        var currentNodeKey = currentRelative.Text;

//        if (!nodeLookup.TryGetValue(currentNodeKey, out MyTreeNode currentNode))
//        {
//            currentNode = currentRelative;
//            nodes.Add(currentNode);
//            nodeLookup[currentNodeKey] = currentNode;
//        }

//        AddNode(currentNode.Nodes, family, index + 1, nodeLookup);
//    }
//}


//private async void ButtonSend_Click(object sender, EventArgs e)
//{
//    string selectedElementIDs = string.Join(",", listViewElements.SelectedItems.Cast<ListViewItem>().Select(item => item.SubItems[0].Text));

//    await Task.Run(() => SendDataToPipe(selectedElementIDs));
//}

//private void SendDataToPipe(string data)
//{
//    string pipeName = "Pipe_Element_ID";
//    using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
//    {
//        try
//        {
//            pipeClient.Connect(3000);

//            using (StreamWriter sw = new StreamWriter(pipeClient, Encoding.ASCII))
//            {
//                sw.WriteLine(data);
//                sw.Flush();
//            }
//        }
//        catch (TimeoutException)
//        {
//            MessageBox.Show("The receiving application is not running.", "Connection Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }
//}

//private void SearchAndCheckNode(TreeNode node, string searchText)
//{
//    if (node.Text.Contains(searchText))
//    {
//        if (!node.Checked)
//        {
//            node.Checked = true;
//            uint nodeId = ((MyTreeNode)node)._ID;
//            AddNodeDataToListView(nodeId);
//        }

//        TreeNode currentNode = node;
//        while (currentNode.Parent != null)
//        {
//            currentNode.Parent.Expand();
//            currentNode = currentNode.Parent;
//        }
//    }

//    foreach (TreeNode childNode in node.Nodes)
//    {
//        SearchAndCheckNode(childNode, searchText);
//    }
//}


//private void AddTreeViewEventHandlers()
//{
//    treeViewType.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating)
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true;
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//            isUpdating = false;
//        }
//    };

//    treeViewChannel.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating)
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true;
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//            isUpdating = false;
//        }
//    };

//    treeViewDatabase.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating)
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true;
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//            isUpdating = false;
//        }
//    };
//}

//private void AddNodeDataToListView(uint nodeId)
//{
//    if (elementById.TryGetValue(nodeId, out ElementData element))
//    {
//        ListViewItem listViewItem = new ListViewItem(element.ID.ToString());
//        listViewItem.SubItems.Add(element.LongName);
//        listViewItem.SubItems.Add(element.ShortName);
//        listViewItem.SubItems.Add(typeById.ContainsKey(element.Type) ? typeById[element.Type] : string.Empty);
//        listViewItem.SubItems.Add(channelById.ContainsKey(element.Channel) ? channelById[element.Channel] : string.Empty);
//        listViewItem.SubItems.Add(databaseById.ContainsKey(element.Database) ? databaseById[element.Database] : string.Empty);
//        listViewItem.SubItems.Add(element.Location);
//        listViewItem.SubItems.Add(element.Handle.ToString());
//        listViewElements.Items.Add(listViewItem);
//    }
//}

//private void SearchAndCheckNode(TreeNode node, string searchText)
//{
//    string nodeTextLower = node.Text.ToLower();
//    string searchTextLower = searchText.ToLower();

//    if (nodeTextLower.Contains(searchTextLower))
//    {
//        if (!node.Checked)
//        {
//            node.Checked = true;
//            uint nodeId = ((MyTreeNode)node)._ID;
//            AddNodeDataToListView(nodeId);
//        }

//        TreeNode currentNode = node;
//        while (currentNode.Parent != null)
//        {
//            currentNode.Parent.Expand();
//            currentNode = currentNode.Parent;
//        }
//    }

//    foreach (TreeNode childNode in node.Nodes)
//    {
//        SearchAndCheckNode(childNode, searchText);
//    }
//}

//private void AddElementToListView(ElementData element)
//{
//    bool itemExists = false;

//    foreach (ListViewItem item in listViewElements.Items)
//    {
//        if (item.SubItems[0].Text == element.ID.ToString())
//        {
//            itemExists = true;
//            break;
//        }
//    }

//    if (itemExists == false)
//    {
//        var item = new ListViewItem(element.ID.ToString());
//        item.SubItems.Add(element.LongName);
//        item.SubItems.Add(element.ShortName);

//        item.SubItems.Add(typeById.ContainsKey(element.Type) ? typeById[element.Type] : string.Empty);
//        item.SubItems.Add(channelById.ContainsKey(element.Channel) ? channelById[element.Channel] : string.Empty);
//        item.SubItems.Add(databaseById.ContainsKey(element.Database) ? databaseById[element.Database] : string.Empty);

//        item.SubItems.Add(element.Location);
//        item.SubItems.Add(element.Handle.ToString());

//        listViewElements.Items.Add(item);
//    }
//}

//internal class ListViewColumnSorter : IComparer
//{
//    public int Column { get; set; }
//    public SortOrder Order { get; set; }
//    public HashSet<int> NumericColumns { get; set; }

//    public ListViewColumnSorter()
//    {
//        Column = 0;
//        Order = SortOrder.None;
//        NumericColumns = new HashSet<int>();
//    }

//    public int Compare(object x, object y)
//    {
//        ListViewItem itemX = (ListViewItem)x;
//        ListViewItem itemY = (ListViewItem)y;

//        int compareResult;

//        if (NumericColumns.Contains(Column))
//        {
//            // Compare as numbers
//            int numericValueX = int.TryParse(itemX.SubItems[Column].Text, out int tempX) ? tempX : int.MinValue;
//            int numericValueY = int.TryParse(itemY.SubItems[Column].Text, out int tempY) ? tempY : int.MinValue;
//            compareResult = numericValueX.CompareTo(numericValueY);
//        }
//        else
//        {
//            // Compare as strings
//            compareResult = string.Compare(itemX.SubItems[Column].Text, itemY.SubItems[Column].Text);
//        }

//        if (Order == SortOrder.Ascending)
//        {
//            return compareResult;
//        }
//        else if (Order == SortOrder.Descending)
//        {
//            return -compareResult;
//        }
//        else
//        {
//            return 0;
//        }
//    }
//}

//private bool ItemWithIDExists(uint nodeId)
//{
//    foreach (ListViewItem listViewItem in listViewElements.Items)
//    {
//        if (listViewItem.Text == nodeId.ToString())
//        {
//            return true;
//        }
//    }

//    return false;
//}


//private void AddNodeDataToListView(uint nodeId)
//{
//    //if (elementById.TryGetValue(nodeId, out ElementData element) && !ItemWithIDExists(nodeId))
//    if (elementById.TryGetValue(nodeId, out ElementData element))
//    {
//        if (uniqueElementIds.Add(nodeId.ToString()))
//        {
//            ListViewItem listViewItem = new ListViewItem(element.ID.ToString());
//            listViewItem.SubItems.Add(element.LongName);
//            listViewItem.SubItems.Add(element.ShortName);
//            listViewItem.SubItems.Add(typeById.ContainsKey(element.Type) ? typeById[element.Type] : string.Empty);
//            listViewItem.SubItems.Add(channelById.ContainsKey(element.Channel) ? channelById[element.Channel] : string.Empty);
//            listViewItem.SubItems.Add(databaseById.ContainsKey(element.Database) ? databaseById[element.Database] : string.Empty);
//            listViewItem.SubItems.Add(element.Location);
//            listViewItem.SubItems.Add(element.Handle.ToString());
//            listViewElements.Items.Add(listViewItem);
//        }
//    }
//}

//private void RemoveNodeDataFromListView(uint nodeId)
//{
//    ListViewItem itemToRemove = null;

//    foreach (ListViewItem listViewItem in listViewElements.Items)
//    {
//        if (listViewItem.Text == nodeId.ToString())
//        {
//            itemToRemove = listViewItem;
//            break;
//        }
//    }

//    if (itemToRemove != null)
//    {
//        listViewElements.Items.Remove(itemToRemove);
//        uniqueElementIds.Remove(nodeId.ToString());
//    }
//}

//private void TextBoxType_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        string searchText = textBoxType.Text;
//        if (searchText.Length >= 3)
//        {
//            foreach (TreeNode node in treeViewType.Nodes)
//            {
//                SearchAndCheckNode(node, searchText, true);
//            }
//        }
//    }
//}

//private void TextBoxChannel_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        string searchText = textBoxChannel.Text;
//        if (searchText.Length >= 3)
//        {
//            foreach (TreeNode node in treeViewChannel.Nodes)
//            {
//                SearchAndCheckNode(node, searchText, true);
//            }
//        }
//    }
//}

//private void TextBoxDatabase_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        string searchText = textBoxDatabase.Text;
//        if (searchText.Length >= 3)
//        {
//            foreach (TreeNode node in treeViewDatabase.Nodes)
//            {
//                SearchAndCheckNode(node, searchText, true);
//            }
//        }
//    }
//}

//private void AddTreeViewEventHandlers()
//{
//    treeViewType.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating)
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true;
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//            SearchAndCheckNode(e.Node, "", e.Node.Checked, true);
//            isUpdating = false;
//        }
//    };

//    treeViewChannel.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating)
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true;
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//            SearchAndCheckNode(e.Node, "", e.Node.Checked, true);
//            isUpdating = false;
//        }
//    };

//    treeViewDatabase.AfterCheck += (sender, e) =>
//    {
//        if (isUpdating)
//        {
//            return;
//        }

//        if (e.Node is MyTreeNode myNode)
//        {
//            isUpdating = true;
//            UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//            UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//            SearchAndCheckNode(e.Node, "", e.Node.Checked, true);
//            isUpdating = false;
//        }
//    };
//}

//private void SendDataToPipe(string data)
//{
//    string pipeName = "Pipe_Element_ID";
//    using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
//    {
//        try
//        {
//            pipeClient.Connect(3000);

//            using (StreamWriter sw = new StreamWriter(pipeClient, Encoding.ASCII))
//            {
//                sw.WriteLine(data);
//                sw.Flush();
//            }
//        }
//        catch (TimeoutException)
//        {
//            MessageBox.Show("The receiving application is not running.", "Connection Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        }
//    }
//}

//private async void ButtonSend_Click(object sender, EventArgs e)
//{
//    string selectedElementIDs = string.Join(",", listViewElements.SelectedItems.Cast<ListViewItem>().Select(item => item.SubItems[0].Text));

//    await Task.Run(() => SendDataToPipe(selectedElementIDs));
//}

//private void CheckBoxElements_CheckedChanged(object sender, EventArgs e)
//{
//    if (checkBoxElements.Checked)
//    {
//        // Store the selected items in a temporary list
//        List<ListViewItem> selectedItems = new List<ListViewItem>();
//        foreach (ListViewItem item in listViewElements.SelectedItems)
//        {
//            selectedItems.Add(item);
//        }

//        // Clear the ListView
//        listViewElements.Items.Clear();

//        // Add only the selected items back to the ListView
//        if (selectedItems.Count > 0)
//        {
//            foreach (ListViewItem selectedItem in selectedItems)
//            {
//                listViewElements.Items.Add(selectedItem);
//            }
//        }
//    }
//    else
//    {
//        // Restore the ListView to its original state (all items)
//        UpdateListViewForNodeHierarchy(treeViewType.Nodes, true);
//        UpdateListViewForNodeHierarchy(treeViewChannel.Nodes, true);
//        UpdateListViewForNodeHierarchy(treeViewDatabase.Nodes, true);
//    }
//}

//private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
//{
//    if (isTreeViewUpdating)
//    {
//        return;
//    }

//    if (e.Node is MyTreeNode myNode)
//    {
//        isTreeViewUpdating = true;
//        UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//        UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//        SearchAndCheckNode(e.Node, "", e.Node.Checked, true);
//        isTreeViewUpdating = false;
//    }
//}

//private void UncheckAndCollapseNodes(TreeNode parentNode)
//{
//    parentNode.Checked = false;
//    parentNode.Collapse();

//    foreach (TreeNode childNode in parentNode.Nodes)
//    {
//        UncheckAndCollapseNodes(childNode);
//    }
//}
