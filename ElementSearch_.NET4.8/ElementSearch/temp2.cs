﻿//namespace ElementSearch
//{
//    public partial class ElementSearch : Form
//    {
//        private Dictionary<uint, string> typeById = new Dictionary<uint, string>();
//        private Dictionary<uint, string> channelById = new Dictionary<uint, string>();
//        private Dictionary<uint, string> databaseById = new Dictionary<uint, string>();
//        private Dictionary<uint, Element> elementById = new Dictionary<uint, Element>();

//        private Dictionary<uint, uint> typeIdToElementId = new Dictionary<uint, uint>();
//        private Dictionary<uint, uint> channelIdToElementId = new Dictionary<uint, uint>();
//        private Dictionary<uint, uint> databaseIdToElementId = new Dictionary<uint, uint>();

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
//            TreeView treeView = sender as TreeView;

//            if (e.Node is MyTreeNode)
//            {
//                UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//                UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked, treeView);
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

//        private void UpdateListViewForNodeHierarchy(TreeNode node, bool isChecked, TreeView treeView)
//        {
//            if (node is MyTreeNode myNode)
//            {
//                uint id = myNode._ID;

//                if (treeView == treeViewType && typeIdToElementId.ContainsKey(id))
//                {
//                    if (isChecked)
//                    {
//                        AddElementToListView(typeIdToElementId[id]);
//                    }
//                    else
//                    {
//                        RemoveElementFromListView(typeIdToElementId[id]);
//                    }
//                }
//                else if (treeView == treeViewChannel && channelIdToElementId.ContainsKey(id))
//                {
//                    if (isChecked)
//                    {
//                        AddElementToListView(channelIdToElementId[id]);
//                    }
//                    else
//                    {
//                        RemoveElementFromListView(channelIdToElementId[id]);
//                    }
//                }
//                else if (treeView == treeViewDatabase && databaseIdToElementId.ContainsKey(id))
//                {
//                    if (isChecked)
//                    {
//                        AddElementToListView(databaseIdToElementId[id]);
//                    }
//                    else
//                    {
//                        RemoveElementFromListView(databaseIdToElementId[id]);
//                    }
//                }
//            }

//            foreach (TreeNode childNode in node.Nodes)
//            {
//                UpdateListViewForNodeHierarchy(childNode, isChecked, treeView);
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
//            // Good code ...
//        }

//        private void ClearTreeView(TreeView treeView)
//        {
//            // Good code ...
//        }

//        private void SearchAndCheckNode(TreeView treeView, TreeNode node, string searchText, bool check, bool forceCheck = false)
//        {
//            string nodeTextLower = node.Text.ToLower();
//            string searchTextLower = searchText.ToLower();

//            if (nodeTextLower.Contains(searchTextLower) || searchText == "" || forceCheck)
//            {
//                if ((check && !node.Checked) || (!check && node.Checked) || forceCheck)
//                {
//                    node.Checked = check;
//                    uint nodeId = ((MyTreeNode)node)._ID;

//                    if (treeView == treeViewType && typeIdToElementId.ContainsKey(nodeId))
//                    {
//                        if (check)
//                        {
//                            AddElementToListView(typeIdToElementId[nodeId]);
//                        }
//                        else
//                        {
//                            RemoveElementFromListView(typeIdToElementId[nodeId]);
//                        }
//                    }
//                    else if (treeView == treeViewChannel && channelIdToElementId.ContainsKey(nodeId))
//                    {
//                        if (check)
//                        {
//                            AddElementToListView(channelIdToElementId[nodeId]);
//                        }
//                        else
//                        {
//                            RemoveElementFromListView(channelIdToElementId[nodeId]);
//                        }
//                    }
//                    else if (treeView == treeViewDatabase && databaseIdToElementId.ContainsKey(nodeId))
//                    {
//                        if (check)
//                        {
//                            AddElementToListView(databaseIdToElementId[nodeId]);
//                        }
//                        else
//                        {
//                            RemoveElementFromListView(databaseIdToElementId[nodeId]);
//                        }
//                    }
//                }

//                TreeNode currentNode = node;
//                while (currentNode.Parent != null)
//                {
//                    currentNode.Parent.Expand();
//                    currentNode = currentNode.Parent;
//                }
//            }

//            foreach (TreeNode childNode in node.Nodes)
//            {
//                SearchAndCheckNode(treeView, childNode, searchText, check, forceCheck);
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
//            // Good code ...
//        }

//        private void TextBox_KeyDown(object sender, KeyEventArgs e)
//        {
//            // Good code ...
//        }
//    }
//}