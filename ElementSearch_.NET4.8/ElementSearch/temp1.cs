//namespace ElementSearch
//{
//    public partial class ElementSearch : Form
//    {
//        private bool isTreeViewClearing = false;
//        private bool isTreeViewUpdating = false;
//        private ListViewColumnSorter listViewColumnSorter;

//        private Dictionary<uint, string> typeById = new Dictionary<uint, string>();
//        private Dictionary<uint, string> channelById = new Dictionary<uint, string>();
//        private Dictionary<uint, string> databaseById = new Dictionary<uint, string>();

//        private Dictionary<uint, uint> typeIdToElementId = new Dictionary<uint, uint>();
//        private Dictionary<uint, uint> channelIdToElementId = new Dictionary<uint, uint>();
//        private Dictionary<uint, uint> databaseIdToElementId = new Dictionary<uint, uint>();

//        private Dictionary<uint, Element> elementById = new Dictionary<uint, Element>();
//        private Dictionary<uint, ListViewItem> listViewItemById = new Dictionary<uint, ListViewItem>();

//        private List<ListViewItem> listViewItemAll = new List<ListViewItem>();
//        private HashSet<uint> listViewItemIds = new HashSet<uint>();

//        public ElementSearch()
//        {
//            InitializeComponent();
//            LoadData();
//            InitializeSorter();
//        }

//        private void LoadData()
//        {
//            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
//            string typeFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_type.txt");
//            string channelFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_channel.txt");
//            string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_database.txt");
//            string elementFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elements.txt");

//            string[] filesPaths = { typeFilePath, channelFilePath, databaseFilePath, elementFilePath };

//            var fileTokens = filesPaths.Select(ReadTextFile).ToArray();

//            var fillTreeViewTasks = new[]
//            {
//                Task.Factory.StartNew(() => FillTreeView(treeViewType, fileTokens[0])),
//                Task.Factory.StartNew(() => FillTreeView(treeViewChannel, fileTokens[1])),
//                Task.Factory.StartNew(() => FillTreeView(treeViewDatabase, fileTokens[2]))
//            };

//            Task.Factory.ContinueWhenAll(fillTreeViewTasks, _ =>
//            {
//                foreach (var lineTokens in fileTokens[3])
//                {
//                    if (lineTokens.Count < 8)
//                        continue;

//                    uint.TryParse(lineTokens[0], out uint id);

//                    Element element = new Element();

//                    element.ID = id;
//                    element.LongName = lineTokens[1];
//                    element.ShortName = lineTokens[2];

//                    if (uint.TryParse(lineTokens[3], out uint type))
//                    {
//                        element.Type = type;
//                        typeIdToElementId[element.Type] = element.ID;
//                    }
//                    else
//                        element.Type = uint.MaxValue;

//                    if (uint.TryParse(lineTokens[4], out uint channel))
//                    {
//                        element.Channel = channel;
//                        channelIdToElementId[element.Channel] = element.ID;
//                    }
//                    else
//                        element.Channel = uint.MaxValue;

//                    if (uint.TryParse(lineTokens[5], out uint database))
//                    {
//                        element.Database = database;
//                        databaseIdToElementId[element.Database] = element.ID;
//                    }
//                    else
//                        element.Database = uint.MaxValue;

//                    element.Location = lineTokens[6];

//                    if (uint.TryParse(lineTokens[7], out uint handle))
//                        element.Handle = handle;
//                    else
//                        element.Handle = uint.MaxValue;

//                    elementById[id] = element;
//                }

//                FillDictionaries(typeById, fileTokens[0]);
//                FillDictionaries(channelById, fileTokens[1]);
//                FillDictionaries(databaseById, fileTokens[2]);
//            });
//        }

//        private void InitializeSorter()
//        {
//            // Good code ...
//        }

//        private List<List<string>> ReadTextFile(string filePath)
//        {
//            // Good code ...
//        }

//        private void FillDictionaries(Dictionary<uint, string> dict, List<List<string>> fileTokens)
//        {
//            // Good code ...
//        }

//        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//        {
//            // Good code ...
//        }

//        private void AddNode(TreeNodeCollection nodes, List<string> family, int index, uint id, Dictionary<string, MyTreeNode> nodeLookup)
//        {
//            // Good code ...
//        }

//        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
//        {
//            if (isTreeViewUpdating || isTreeViewClearing)
//            {
//                return;
//            }

//            TreeView treeView = sender as TreeView;

//            if (e.Node is MyTreeNode)
//            {
//                isTreeViewUpdating = true;
//                UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//                UpdateListViewForNodeHierarchy(treeView, e.Node, e.Node.Checked);
//                isTreeViewUpdating = false;
//            }
//        }


//        private void UpdateChildNodesCheckedState(TreeNode parentNode, bool isChecked)
//        {
//            foreach (TreeNode childNode in parentNode.Nodes)
//            {
//                childNode.Checked = isChecked;
//                UpdateChildNodesCheckedState(childNode, isChecked);
//            }
//        }

//        private void UpdateListViewForNodeHierarchy(TreeView treeView, TreeNode node, bool isChecked)
//        {
//            if (node is MyTreeNode myNode)
//            {
//                uint id = myNode._ID;

//                if (treeView == treeViewType)
//                {
//                    UpdateListViewForType(id, isChecked);
//                }
//                else if (treeView == treeViewChannel)
//                {
//                    UpdateListViewForChannel(id, isChecked);
//                }
//                else if (treeView == treeViewDatabase)
//                {
//                    UpdateListViewForDatabase(id, isChecked);
//                }
//            }

//            foreach (TreeNode childNode in node.Nodes)
//            {
//                UpdateListViewForNodeHierarchy(treeView, childNode, isChecked);
//            }
//        }

//        private void UpdateListViewForType(uint typeId, bool isChecked)
//        {
//            var matchingElementIds = elementById.Values
//                .Where(element => element.Type == typeId)
//                .Select(element => element.ID);

//            UpdateListView(matchingElementIds, isChecked);
//        }

//        private void UpdateListViewForChannel(uint channelId, bool isChecked)
//        {
//            var matchingElementIds = elementById.Values
//                .Where(element => element.Channel == channelId)
//                .Select(element => element.ID);

//            UpdateListView(matchingElementIds, isChecked);
//        }

//        private void UpdateListViewForDatabase(uint databaseId, bool isChecked)
//        {
//            var matchingElementIds = elementById.Values
//                .Where(element => element.Database == databaseId)
//                .Select(element => element.ID);

//            UpdateListView(matchingElementIds, isChecked);
//        }

//        private void UpdateListView(IEnumerable<uint> elementIds, bool isChecked)
//        {
//            foreach (uint id in elementIds)
//            {
//                if (isChecked)
//                {
//                    AddElementToListView(id);
//                }
//                else
//                {
//                    RemoveElementFromListView(id);
//                }
//            }
//        }

//        private void AddElementToListView(uint nodeId)
//        {
//            if (elementById.TryGetValue(nodeId, out Element element))
//            {
//                if (listViewItemIds.Add(nodeId))
//                {
//                    ListViewItem listViewItem = new ListViewItem(element.ID.ToString());
//                    listViewItem.SubItems.Add(element.LongName);
//                    listViewItem.SubItems.Add(element.ShortName);
//                    listViewItem.SubItems.Add(typeById.ContainsKey(element.Type) ? typeById[element.Type] : string.Empty);
//                    listViewItem.SubItems.Add(channelById.ContainsKey(element.Channel) ? channelById[element.Channel] : string.Empty);
//                    listViewItem.SubItems.Add(databaseById.ContainsKey(element.Database) ? databaseById[element.Database] : string.Empty);
//                    listViewItem.SubItems.Add(element.Location);
//                    listViewItem.SubItems.Add(element.Handle.ToString());

//                    listViewElements.Items.Add(listViewItem);
//                    listViewItemAll.Add(listViewItem);
//                    listViewItemById[nodeId] = listViewItem;
//                }
//            }
//        }

//        private void RemoveElementFromListView(uint id)
//        {
//            if (listViewItemById.TryGetValue(id, out ListViewItem itemToRemove))
//            {
//                listViewItemIds.Remove(id);
//                listViewElements.Items.Remove(itemToRemove);
//                listViewItemAll.Remove(itemToRemove);
//                listViewItemById.Remove(id);
//            }
//        }

//        private void SetTreeViewNodeCheckState(TreeNodeCollection nodes, bool checkState)
//        {
//            foreach (TreeNode node in nodes)
//            {
//                node.Checked = checkState;

//                if (checkState)
//                    node.Expand();
//                else
//                    node.Collapse();

//                SetTreeViewNodeCheckState(node.Nodes, checkState);
//            }
//        }

//        private void ClearTreeView(TreeView treeView)
//        {
//            isTreeViewClearing = true;

//            Stack<TreeNode> nodesToProcess = new Stack<TreeNode>();

//            foreach (TreeNode node in treeView.Nodes)
//            {
//                nodesToProcess.Push(node);
//            }

//            while (nodesToProcess.Count > 0)
//            {
//                TreeNode currentNode = nodesToProcess.Pop();
//                currentNode.Checked = false;
//                currentNode.Collapse();

//                foreach (TreeNode childNode in currentNode.Nodes)
//                {
//                    nodesToProcess.Push(childNode);
//                }
//            }

//            isTreeViewClearing = false;
//        }

//        private void SearchAndCheckNode(TreeView treeView, TreeNode node, string searchText, bool check, bool forceCheck = false)
//        {
//            string nodeTextLower = node.Text.ToLower();
//            string searchTextLower = searchText.ToLower();

//            if (nodeTextLower.Contains(searchTextLower) || string.IsNullOrEmpty(searchText) || forceCheck)
//            {
//                ToggleNodeCheckState(treeView, node, check, forceCheck);
//                ExpandAncestors(node);
//            }

//            foreach (TreeNode childNode in node.Nodes)
//            {
//                SearchAndCheckNode(treeView, childNode, searchText, check, forceCheck);
//            }
//        }

//        private void ToggleNodeCheckState(TreeView treeView, TreeNode node, bool check, bool forceCheck)
//        {
//            if ((check && !node.Checked) || (!check && node.Checked) || forceCheck)
//            {
//                node.Checked = check;
//                uint nodeId = ((MyTreeNode)node)._ID;

//                if (treeView == treeViewType)
//                {
//                    UpdateListViewForType(nodeId, check);
//                }
//                else if (treeView == treeViewChannel)
//                {
//                    UpdateListViewForChannel(nodeId, check);
//                }
//                else if (treeView == treeViewDatabase)
//                {
//                    UpdateListViewForDatabase(nodeId, check);
//                }
//            }
//        }

//        private void ExpandAncestors(TreeNode node)
//        {
//            TreeNode currentNode = node;
//            while (currentNode.Parent != null)
//            {
//                currentNode.Parent.Expand();
//                currentNode = currentNode.Parent;
//            }
//        }


//        private void CheckBoxElements_CheckedChanged(object sender, EventArgs e)
//        {
//            // Good code ...
//        }

//        private void TextBoxElementName_KeyDown(object sender, KeyEventArgs e)
//        {
//            // Good code ...
//        }

//        private void ListViewElements_ColumnClick(object sender, ColumnClickEventArgs e)
//        {
//            // Good code ...
//        }

//        private void ButtonClear_Click(object sender, EventArgs e)
//        {
//            // Good code ...
//        }

//        private async void ButtonSend_Click(object sender, EventArgs e)
//        {
//            // Good code ...
//        }

//        private async Task SendDataToPipeAsync(string data)
//        {
//            // Good code ...
//        }

//        private void CheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            CheckBox sourceCheckBox = sender as CheckBox;
//            TreeView targetTreeView;

//            if (sourceCheckBox == checkBoxType)
//            {
//                targetTreeView = treeViewType;
//            }
//            else if (sourceCheckBox == checkBoxChannel)
//            {
//                targetTreeView = treeViewChannel;
//            }
//            else //if (sourceCheckBox == checkBoxDatabase)
//            {
//                targetTreeView = treeViewDatabase;
//            }

//            SetTreeViewNodeCheckState(targetTreeView.Nodes, sourceCheckBox.Checked);
//        }

//        private void TextBox_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter)
//            {
//                e.Handled = true;
//                e.SuppressKeyPress = true;

//                var textBox = sender as TextBox;
//                string searchText = textBox.Text;
//                TreeView targetTreeView = null;

//                if (textBox == textBoxType)
//                {
//                    targetTreeView = treeViewType;
//                }
//                else if (textBox == textBoxChannel)
//                {
//                    targetTreeView = treeViewChannel;
//                }
//                else if (textBox == textBoxDatabase)
//                {
//                    targetTreeView = treeViewDatabase;
//                }

//                if (searchText.Length >= 3 && targetTreeView != null)
//                {
//                    foreach (TreeNode node in targetTreeView.Nodes)
//                    {
//                        SearchAndCheckNode(targetTreeView, node, searchText, true);
//                    }
//                }
//            }
//        }
//    }
//}