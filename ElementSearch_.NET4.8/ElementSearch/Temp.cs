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