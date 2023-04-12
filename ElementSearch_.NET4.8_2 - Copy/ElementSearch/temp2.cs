//using ElementSearch;
//using System.IO.Pipes;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System;

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    string selectedElementIDs = string.Join(",", listViewElements.SelectedItems.Cast<ListViewItem>().Select(item => item.SubItems[0].Text));

//    await SendDataToPipeAsync(selectedElementIDs);
//}

//private async Task SendDataToPipeAsync(string data)
//{
//    const string pipeName = "Pipe_Element_ID";
//    try
//    {
//        using (NamedPipeClientStream pipeClient = new(".", pipeName, PipeDirection.Out))
//        {
//            await pipeClient.ConnectAsync(3000);
//            using StreamWriter sw = new(pipeClient, Encoding.ASCII);
//            await sw.WriteLineAsync(data);
//            await sw.FlushAsync();
//        }
//    }
//    catch (TimeoutException)
//    {
//        MessageBox.Show("The receiving application is not running.", "Connection Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
//    }
//}

//private void checkBox_CheckedChanged(object sender, EventArgs e)
//{
//    CheckBox checkBox = (CheckBox)sender;
//    TreeView treeView = checkBox.Tag as TreeView;
//    SetTreeViewNodeCheckState(treeView.Nodes, checkBox.Checked);
//}

//private void textBox_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        e.Handled = true;
//        e.SuppressKeyPress = true;

//        TextBox textBox = (TextBox)sender;
//        string searchText = textBox.Text;
//        if (searchText.Length >= 3)
//        {
//            TreeView treeView = textBox.Tag as TreeView;
//            foreach (TreeNode node in treeView.Nodes)
//            {
//                SearchAndCheckNode(node, searchText, true);
//            }
//        }
//    }
//}

//private void TextBoxElementName_KeyDown(object sender, KeyEventArgs e)
//{
//    if (e.KeyCode == Keys.Enter)
//    {
//        e.Handled = true;
//        e.SuppressKeyPress = true;

//        string searchText = textBoxElementName.Text;

//        if (searchText.Length >= 3)
//        {
//            listViewElements.SelectedItems.Clear();

//            foreach (ListViewItem item in listViewElements.Items)
//            {
//                string longName = item.SubItems[1].Text;
//                string shortName = item.SubItems[2].Text;

//                if (longName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
//                    shortName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
//                {
//                    item.Selected = true;
//                    item.EnsureVisible();
//                }
//            }
//        }
//    }
//}

//private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
//{
//    if (isTreeViewUpdating || isClearingTreeView)
//    {
//        return;
//    }

//    if (e.Node is MyTreeNode)
//    {
//        isTreeViewUpdating = true;
//        UpdateChildNodesCheckedState(e.Node, e.Node.Checked);
//        UpdateListViewForNodeHierarchy(e.Node, e.Node.Checked);
//        SearchAndCheckNode(e.Node, "", e.Node.Checked, true);
//        isTreeViewUpdating = false;
//    }
//}

//private void checkBoxElements_CheckedChanged(object sender, EventArgs e)
//{
//    if (checkBoxElements.Checked)
//    {
//        UpdateSelectedElementsList(true);
//    }
//    else
//    {
//        UpdateSelectedElementsList(false);
//    }
//}

//private void buttonToggleFilter_Click(object sender, EventArgs e)
//{
//    filterSelectedElements = !filterSelectedElements;
//    listViewElements.BeginUpdate();
//    ApplyElementFilter();
//    listViewElements.EndUpdate();
//}
