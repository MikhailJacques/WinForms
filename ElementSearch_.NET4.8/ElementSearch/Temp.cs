//private void FindAndCheckNode(TreeView treeView, string nameToFind)
//{
//    foreach (TreeNode node in treeView.Nodes)
//    {
//        TreeNode foundNode = FindNodeByName(node, nameToFind);
//        if (foundNode != null)
//        {
//            foundNode.Checked = true;
//            treeView.SelectedNode = foundNode;
//            foundNode.Expand();
//            break;
//        }
//    }
//}

//private void FindAndCheckNode(TreeView treeView, string nameToFind)
//{
//    foreach (TreeNode node in treeView.Nodes)
//    {
//        TreeNode foundNode = FindNodeByName(node, nameToFind);
//        if (foundNode != null)
//        {
//            foundNode.Checked = true;
//            treeView.SelectedNode = foundNode;
//            foundNode.Expand();
//            break;
//        }
//    }
//}

//private void FindAndCheckNodes(TreeView treeView, string nameToFind)
//{
//    bool firstMatch = true;
//    foreach (TreeNode node in treeView.Nodes)
//    {
//        bool nodeFound = FindNodesByName(node, nameToFind, ref firstMatch);
//        if (nodeFound && firstMatch)
//        {
//            treeView.SelectedNode = node;
//            node.Expand();
//            firstMatch = false;
//        }
//    }
//}

//private TreeNode FindNodeByName(TreeNode parentNode, string nameToFind)
//{
//    if (parentNode.Text == nameToFind)
//    {
//        return parentNode;
//    }

//    foreach (TreeNode childNode in parentNode.Nodes)
//    {
//        TreeNode foundNode = FindNodeByName(childNode, nameToFind);
//        if (foundNode != null)
//        {
//            return foundNode;
//        }
//    }

//    return null;
//}

//private TreeNode FindNodeByName(TreeNode parentNode, string nameToFind)
//{
//    if (parentNode.Text.IndexOf(nameToFind, StringComparison.OrdinalIgnoreCase) >= 0)
//    {
//        return parentNode;
//    }

//    foreach (TreeNode childNode in parentNode.Nodes)
//    {
//        TreeNode foundNode = FindNodeByName(childNode, nameToFind);
//        if (foundNode != null)
//        {
//            return foundNode;
//        }
//    }

//    return null;
//}

//private bool FindNodesByName(TreeNode parentNode, string nameToFind, ref bool firstMatch)
//{
//    bool foundNode = false;
//    if (parentNode.Text.IndexOf(nameToFind, StringComparison.OrdinalIgnoreCase) >= 0)
//    {
//        parentNode.Checked = true;
//        foundNode = true;

//        if (firstMatch)
//        {
//            firstMatch = false;
//        }
//    }

//    foreach (TreeNode childNode in parentNode.Nodes)
//    {
//        bool childFound = FindNodesByName(childNode, nameToFind, ref firstMatch);
//        foundNode = foundNode || childFound;
//    }

//    return foundNode;
//}

//private bool FindNodesByName(TreeNode parentNode, string nameToFind)
//{
//    bool foundNode = false;
//    if (parentNode.Text.IndexOf(nameToFind, StringComparison.OrdinalIgnoreCase) >= 0)
//    {
//        parentNode.Checked = true;
//        foundNode = true;
//        parentNode.Expand();
//    }

//    foreach (TreeNode childNode in parentNode.Nodes)
//    {
//        bool childFound = FindNodesByName(childNode, nameToFind);
//        foundNode = foundNode || childFound;
//    }

//    return foundNode;
//}

//private void buttonSearch_Click(object sender, EventArgs e)
//{
//    string elemTypeToFind = textBoxElementType.Text;
//    string channelToFind = textBoxChannel.Text;
//    string databaseToFind = textBoxDatabase.Text;

//    FindAndCheckNode(treeViewElementType, elemTypeToFind);
//    FindAndCheckNode(treeViewChannel, channelToFind);
//    FindAndCheckNode(treeViewDatabase, databaseToFind);
//    UpdateListView();
//}

//private void buttonSearch_Click(object sender, EventArgs e)
//{
//    string elemTypeToFind = textBoxElementType.Text;
//    string channelToFind = textBoxChannel.Text;
//    string databaseToFind = textBoxDatabase.Text;

//    if (elemTypeToFind.Length >= 3)
//        FindAndCheckNodes(treeViewElementType, elemTypeToFind);

//    if (channelToFind.Length >= 3)
//        FindAndCheckNodes(treeViewChannel, channelToFind);

//    if (databaseToFind.Length >= 3)
//        FindAndCheckNodes(treeViewDatabase, databaseToFind);

//    UpdateListView();
//}

//private void textBoxElementType_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        string elemTypeToFind = textBoxElementType.Text;
//        FindAndCheckNodes(treeViewElementType, elemTypeToFind);
//        UpdateListView();
//    }
//}

//private void textBoxChannel_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        string channelToFind = textBoxChannel.Text;
//        FindAndCheckNodes(treeViewChannel, channelToFind);
//        UpdateListView();
//    }
//}

//private void textBoxDatabase_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        string databaseToFind = textBoxDatabase.Text;
//        FindAndCheckNodes(treeViewDatabase, databaseToFind);
//        UpdateListView();
//    }
//}

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

//private bool FindNodesByName(Dictionary<uint, ElementTypeData> data, string nameToFind)
//{
//    var foundItems = data.Values.Where(d => d.Name.IndexOf(nameToFind, StringComparison.OrdinalIgnoreCase) >= 0).Select(d => d.ID).ToList();

//    return foundItems;
//}


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