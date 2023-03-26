//using ElementSearch;
//using Microsoft.VisualBasic.Logging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Metrics;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace ElementSearch
//{
//    public partial class FormElementSearch : Form
//    {
//        public FormElementSearch()
//        {
//            InitializeComponent();
//        }

//        private Dictionary<uint, string> _elementTypeById = new Dictionary<uint, string>();
//        private Dictionary<uint, string> _channelById = new Dictionary<uint, string>();
//        private Dictionary<uint, string> _databaseById = new Dictionary<uint, string>();
//        private Dictionary<uint, ElementData> _elementDataById = new Dictionary<uint, ElementData>();

//        private async void FormElementSearch_Load(object sender, EventArgs e)
//        {
//            // This will assign the FullName of the grandparent directory if available, and if not,
//            // it will use the current directory as the project directory. 
//            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? Environment.CurrentDirectory;

//            string elmentTypeFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_type.txt");
//            string channelFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_chn.txt");
//            string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_dbs.txt");
//            string elementDataFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_all.txt");

//            string[] filesPaths = { elmentTypeFilePath, channelFilePath, databaseFilePath, elementDataFilePath };

//            var fileTokens = filesPaths.Select(ReadTextFile).ToArray();

//            var fillTreeViewTasks = new[]
//            {
//                FillTreeView(treeViewElemType, fileTokens[0]),
//                FillTreeView(treeViewChannel, fileTokens[1]),
//                FillTreeView(treeViewDatabase, fileTokens[2])
//            };

//            await Task.WhenAll(fillTreeViewTasks);

//            // Fill element data dictionary
//            foreach (var lineTokens in fileTokens[3])
//            {
//                if (lineTokens.Count < 8)
//                    continue;

//                uint.TryParse(lineTokens[0], out uint id);
//                uint.TryParse(lineTokens[7], out uint handle);

//                _elementDataById[id] = new ElementData
//                {
//                    ID = id,
//                    LongName = lineTokens[1],
//                    ShortName = lineTokens[2],
//                    ElementType = lineTokens[3],
//                    Channel = lineTokens[4],
//                    Database = lineTokens[5],
//                    Location = lineTokens[6],
//                    Handle = handle
//                };
//            }

//            // Fill dictionaries for element types, channels, and databases
//            FillDictionaries(_elementTypeById, fileTokens[0]);
//            FillDictionaries(_channelById, fileTokens[1]);
//            FillDictionaries(_databaseById, fileTokens[2]);
//        }

//        private void FillDictionaries(Dictionary<uint, string> dict, List<List<string>> fileTokens)
//        {
//            foreach (var lineTokens in fileTokens)
//            {
//                if (lineTokens.Count < 3)
//                    continue;

//                uint.TryParse(lineTokens[0], out uint id);
//                dict[id] = lineTokens[1];
//            }
//        }

//        private List<List<string>> ReadTextFile(string filePath)
//        {
//            var fileTokens = new List<List<string>>();

//            using (var reader = new StreamReader(filePath))
//            {
//                string? line;
//                while ((line = reader.ReadLine()) != null)
//                {
//                    var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//                    fileTokens.Add(tokens);
//                }
//            }

//            return fileTokens;
//        }

//        private async Task FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//        {
//            await Task.Run(() =>
//            {
//                var nodeLookup = new Dictionary<string, MyTreeNode>();

//                foreach (var lineTokens in fileTokens)
//                {
//                    if (lineTokens.Count < 3)
//                        continue;

//                    uint.TryParse(lineTokens[0], out uint id);
//                    string family_hierarhy = lineTokens[1];
//                    uint.TryParse(lineTokens[2], out uint handle);

//                    var relatives = family_hierarhy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//                    treeView.Invoke(new Action(() =>
//                    {
//                        AddNode(treeView.Nodes, relatives, 0, nodeLookup);
//                    }));
//                }
//            });
//        }

//        private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index, Dictionary<string, MyTreeNode> nodeLookup)
//        {
//            if (index < family.Count)
//            {
//                var currentRelative = family[index];
//                var currentNodeKey = currentRelative.Text;

//                if (!nodeLookup.TryGetValue(currentNodeKey, out MyTreeNode currentNode))
//                {
//                    currentNode = currentRelative;
//                    nodes.Add(currentNode);
//                    nodeLookup[currentNodeKey] = currentNode;
//                }

//                AddNode(currentNode.Nodes, family, index + 1, nodeLookup);
//            }
//        }

//        private void UpdateChildNodes(MyTreeNode node, bool isChecked)
//        {
//            foreach (MyTreeNode child in node.Nodes)
//            {
//                child.Checked = isChecked;
//                UpdateChildNodes(child, isChecked);
//            }
//        }

//        private void UpdateParentNode(MyTreeNode? node)
//        {
//            if (node == null)
//            {
//                return;
//            }

//            bool anyChecked = node.Nodes.Cast<TreeNode>().Any(childNode => childNode.Checked);

//            if (node.Checked != anyChecked)
//            {
//                node.Checked = anyChecked;
//                UpdateParentNode(node.Parent as MyTreeNode);
//            }
//        }

//        private void treeViewElemType_AfterCheck(object sender, TreeViewEventArgs e)
//        {
//            HandleTreeViewAfterCheck(sender, e);

//            UpdateListView();
//        }

//        private void treeViewChannel_AfterCheck(object sender, TreeViewEventArgs e)
//        {
//            HandleTreeViewAfterCheck(sender, e);

//            UpdateListView();
//        }

//        private void treeViewDatabase_AfterCheck(object sender, TreeViewEventArgs e)
//        {
//            HandleTreeViewAfterCheck(sender, e);

//            UpdateListView();
//        }

//        private void HandleTreeViewAfterCheck(object? sender, TreeViewEventArgs e)
//        {
//            if (e.Action == TreeViewAction.Unknown)
//            {
//                return;
//            }

//            var node = e.Node as MyTreeNode;

//            if (node != null)
//            {
//                UpdateChildNodes(node, node.Checked);
//            }

//            if (sender == null)
//            {
//                return;
//            }

//            TreeView treeView = (TreeView)sender;
//            treeView.AfterCheck -= HandleTreeViewAfterCheck;

//            if (node != null && node.Parent != null)
//            {
//                UpdateParentNode(node.Parent as MyTreeNode);
//            }

//            treeView.AfterCheck += HandleTreeViewAfterCheck;
//        }

//        private IEnumerable<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
//        {
//            List<TreeNode> checkedNodes = new List<TreeNode>();

//            foreach (TreeNode node in nodes)
//            {
//                if (node.Checked && node.Nodes.Count == 0)
//                {
//                    TreeNode fullPathNode = new TreeNode(node.FullPath);
//                    fullPathNode.Tag = node; // Store the original node in the Tag property
//                    checkedNodes.Add(fullPathNode);
//                }

//                checkedNodes.AddRange(GetCheckedNodes(node.Nodes));
//            }

//            return checkedNodes;
//        }

//        private void UpdateListView() // with ID numbers taken from Nodes
//        {
//            listViewElements.Items.Clear();

//            var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes).OfType<TreeNode>().ToList();
//            var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<TreeNode>().ToList();
//            var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<TreeNode>().ToList();

//            var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => (node.Tag as MyTreeNode)?._ID ?? 0));
//            var allCheckedNodes = checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => node.Tag as MyTreeNode).Where(node => node != null).GroupBy(node => node._ID).ToDictionary(g => g.Key, g => g.First()._Handle);

//            foreach (var id in _elementDataById.Keys)
//            {
//                if (allCheckedNodeIds.Contains(id))
//                {
//                    ElementData elementData = _elementDataById[id];
//                    ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
//                    newItem.SubItems.Add(elementData.LongName);
//                    newItem.SubItems.Add(elementData.ShortName);

//                    newItem.SubItems.Add(_elementTypeById.TryGetValue(uint.Parse(elementData.ElementType), out string elementType) ? elementType : elementData.ElementType);
//                    newItem.SubItems.Add(_channelById.TryGetValue(uint.Parse(elementData.Channel), out string channel) ? channel : elementData.Channel);
//                    newItem.SubItems.Add(_databaseById.TryGetValue(uint.Parse(elementData.Database), out string database) ? database : elementData.Database);

//                    newItem.SubItems.Add(elementData.Location);

//                    if (allCheckedNodes.TryGetValue(id, out uint handle))
//                    {
//                        newItem.SubItems.Add(handle.ToString());
//                    }
//                    else
//                    {
//                        newItem.SubItems.Add(elementData.Handle.ToString());
//                    }

//                    listViewElements.Items.Add(newItem);
//                }
//            }
//        }


//        //// This UpdateListView retrieves the checked leaf nodes and combines them into a single HashSet containing
//        //// the unique ID numbers of all checked nodes. Then it iterates through the keys of the _elementDataById
//        //// dictionary and checks if the current key is present in the HashSet.
//        //// If it is, the corresponding ElementData object is used to create a new ListViewItem,
//        //// which is then added to the listViewElements.Items collection.
//        //// This implementation provides good performance, especially when there are many nodes in the tree view and
//        //// the internal data structure.
//        //private void UpdateListView()
//        //{
//        //    listViewElements.Items.Clear();

//        //    var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes).OfType<TreeNode>().ToList();
//        //    var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<TreeNode>().ToList();
//        //    var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<TreeNode>().ToList();

//        //    var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => (node.Tag as MyTreeNode)?._ID ?? 0));

//        //    foreach (var id in _elementDataById.Keys)
//        //    {
//        //        if (allCheckedNodeIds.Contains(id))
//        //        {
//        //            ElementData elementData = _elementDataById[id];
//        //            ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
//        //            newItem.SubItems.Add(elementData.LongName);
//        //            newItem.SubItems.Add(elementData.ShortName);

//        //            newItem.SubItems.Add(_elementTypeById.TryGetValue(uint.Parse(elementData.ElementType), out string elementType) ? elementType : elementData.ElementType);
//        //            newItem.SubItems.Add(_channelById.TryGetValue(uint.Parse(elementData.Channel), out string channel) ? channel : elementData.Channel);
//        //            newItem.SubItems.Add(_databaseById.TryGetValue(uint.Parse(elementData.Database), out string database) ? database : elementData.Database);

//        //            newItem.SubItems.Add(elementData.Location);
//        //            newItem.SubItems.Add(elementData.Handle.ToString());

//        //            listViewElements.Items.Add(newItem);
//        //        }
//        //    }
//        //}

//        private void FindAndCheckNode(TreeView treeView, string nameToFind)
//        {
//            foreach (TreeNode node in treeView.Nodes)
//            {
//                TreeNode foundNode = FindNodeByName(node, nameToFind);
//                if (foundNode != null)
//                {
//                    foundNode.Checked = true;
//                    treeView.SelectedNode = foundNode;
//                    foundNode.Expand();
//                    break;
//                }
//            }
//        }

//        private TreeNode FindNodeByName(TreeNode parentNode, string nameToFind)
//        {
//            if (parentNode.Text == nameToFind)
//            {
//                return parentNode;
//            }

//            foreach (TreeNode childNode in parentNode.Nodes)
//            {
//                TreeNode foundNode = FindNodeByName(childNode, nameToFind);
//                if (foundNode != null)
//                {
//                    return foundNode;
//                }
//            }

//            return null;
//        }

//        private void buttonSearch_Click(object sender, EventArgs e)
//        {
//            string elemTypeToFind = textBoxElemType.Text;
//            string channelToFind = textBoxChannel.Text;
//            string databaseToFind = textBoxDatabase.Text;

//            FindAndCheckNode(treeViewElemType, elemTypeToFind);
//            FindAndCheckNode(treeViewChannel, channelToFind);
//            FindAndCheckNode(treeViewDatabase, databaseToFind);

//            UpdateListView();
//        }

//        private void buttonClear_Click(object sender, EventArgs e)
//        {
//            // Clear all TextBoxes
//            textBoxElemType.Clear();
//            textBoxChannel.Clear();
//            textBoxDatabase.Clear();

//            // Uncheck all nodes and collapse the node hierarchy in all TreeViews
//            ClearTreeView(treeViewElemType);
//            ClearTreeView(treeViewChannel);
//            ClearTreeView(treeViewDatabase);

//            // Clear the entire ListView
//            listViewElements.Items.Clear();
//        }

//        private void ClearTreeView(TreeView treeView)
//        {
//            foreach (TreeNode node in treeView.Nodes)
//            {
//                UncheckAndCollapseNodes(node);
//            }
//        }

//        private void UncheckAndCollapseNodes(TreeNode parentNode)
//        {
//            parentNode.Checked = false;
//            parentNode.Collapse();

//            foreach (TreeNode childNode in parentNode.Nodes)
//            {
//                UncheckAndCollapseNodes(childNode);
//            }
//        }
//    }
//}

//private void buttonSend_Click(object sender, EventArgs e)
//{
//    List<string> selectedIds = new List<string>();
//    foreach (ListViewItem item in listViewElements.SelectedItems)
//    {
//        selectedIds.Add(item.SubItems[0].Text);
//    }

//    SendDataToCPipe(string.Join(",", selectedIds));
//}

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    var selectedIDs = listViewElements.SelectedItems
//        .Cast<ListViewItem>()
//        .Select(item => item.SubItems[0].Text)
//        .ToList();

//    var idsString = string.Join(",", selectedIDs);

//    await Task.Run(() =>
//    {
//        using (var pipeServer = new NamedPipeServerStream("Local\\my_pipe", PipeDirection.Out))
//        {
//            pipeServer.WaitForConnection();

//            using (var writer = new StreamWriter(pipeServer))
//            {
//                writer.Write(idsString);
//                writer.Flush();
//            }
//        }
//    });
//}

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    var selectedIDs = listViewElements.SelectedItems
//        .Cast<ListViewItem>()
//        .Select(item => item.SubItems[0].Text)
//        .ToList();

//    var idsString = string.Join(",", selectedIDs);

//    await Task.Run(() =>
//    {
//        using (var pipeServer = new NamedPipeServerStream("Global\\my_pipe", PipeDirection.Out))
//        {
//            pipeServer.WaitForConnection();

//            using (var writer = new StreamWriter(pipeServer))
//            {
//                writer.Write(idsString);
//                writer.Flush();
//            }
//        }
//    });
//}

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    var selectedIDs = listViewElements.SelectedItems
//        .Cast<ListViewItem>()
//        .Select(item => item.SubItems[0].Text)
//        .ToList();

//    var idsString = string.Join(",", selectedIDs);

//    await Task.Run(() =>
//    {
//        var securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
//        var accessRule = new PipeAccessRule(securityIdentifier, PipeAccessRights.ReadWrite, AccessControlType.Allow);
//        var security = new PipeSecurity();
//        security.AddAccessRule(accessRule);

//        using (var pipeServer = new NamedPipeServerStream("my_pipe", PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, security))
//        {
//            pipeServer.WaitForConnection();

//            using (var writer = new StreamWriter(pipeServer))
//            {
//                writer.Write(idsString);
//                writer.Flush();
//            }
//        }
//    });
//}


//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    using (var pipeServer = new NamedPipeServerStream("my_pipe", PipeDirection.Out, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
//    {
//        await pipeServer.WaitForConnectionAsync();

//        using (var writer = new StreamWriter(pipeServer))
//        {
//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                await writer.WriteLineAsync(id);
//            }
//        }
//    }
//}


//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    PipeSecurity pipeSecurity = new PipeSecurity();
//    pipeSecurity.AddAccessRule(new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow));

//    using (var pipeServer = new NamedPipeServerStream("my_pipe", PipeDirection.Out, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 512, 512, pipeSecurity))
//    {
//        await pipeServer.WaitForConnectionAsync();

//        using (var writer = new StreamWriter(pipeServer))
//        {
//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                await writer.WriteLineAsync(id);
//            }
//        }
//    }
//}


//private void SendDataToCPipe(string data)
//{
//    using (var pipeServer = new NamedPipeServerStream("my_pipe", PipeDirection.Out))
//    {
//        pipeServer.WaitForConnection();

//        using (var sw = new StreamWriter(pipeServer))
//        {
//            sw.AutoFlush = true;
//            sw.WriteLine(data);
//        }
//    }
//}

//private static NamedPipeServerStream CreateNamedPipeServerStreamWithSecurity(string pipeName)
//{
//    PipeSecurity pipeSecurity = new PipeSecurity();
//    SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
//    PipeAccessRule par = new PipeAccessRule(sid, PipeAccessRights.ReadWrite, AccessControlType.Allow);
//    pipeSecurity.AddAccessRule(par);

//    var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, pipeSecurity);
//    return pipeServer;
//}

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    using (var pipeServer = CreateNamedPipeServerStreamWithSecurity("my_pipe"))
//    {
//        await pipeServer.WaitForConnectionAsync();

//        using (var writer = new StreamWriter(pipeServer))
//        {
//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                await writer.WriteLineAsync(id);
//            }
//        }
//    }
//}

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    PipeSecurity pipeSecurity = new PipeSecurity();
//    SecurityIdentifier everyoneSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
//    PipeAccessRule everyoneRule = new PipeAccessRule(everyoneSid, PipeAccessRights.FullControl, AccessControlType.Allow);
//    pipeSecurity.AddAccessRule(everyoneRule);

//    using (var pipeServer = NamedPipeServerStreamAcl.Create("my_pipe", PipeDirection.Out, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, pipeSecurity))
//    {
//        await pipeServer.WaitForConnectionAsync();

//        using (var writer = new StreamWriter(pipeServer))
//        {
//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                await writer.WriteLineAsync(id);
//            }
//        }
//    }
//}


//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    string pipeName = "mypipe_" + Guid.NewGuid().ToString("N");

//    using (var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly))
//    {
//        await pipeServer.WaitForConnectionAsync();

//        using (var writer = new StreamWriter(pipeServer))
//        {
//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                await writer.WriteLineAsync(id);
//            }
//        }
//    }
//}


//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    string pipeName = "mypipe_" + Guid.NewGuid().ToString("N");
//    Environment.SetEnvironmentVariable("MY_PIPE_NAME", pipeName, EnvironmentVariableTarget.User);

//    //// Write the pipeName to a shared file
//    ////File.WriteAllText("../pipe_name.txt", pipeName);

//    //string filePath = @"D:\Sandbox\ElementSearchApp\pipe_name.txt";
//    //File.WriteAllText(filePath, pipeName);

//    using (var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly))
//    {
//        await pipeServer.WaitForConnectionAsync();

//        using (var writer = new StreamWriter(pipeServer))
//        {
//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                await writer.WriteLineAsync(id);
//            }
//        }
//    }
//}

//private async void buttonSend_Click(object sender, EventArgs e)
//{
//    string sharedMemoryName = "MySharedMemory";
//    int memorySize = 1024;

//    using (var sharedMemory = MemoryMappedFile.CreateNew(sharedMemoryName, memorySize))
//    {
//        using (var accessor = sharedMemory.CreateViewAccessor())
//        {
//            StringBuilder sb = new StringBuilder();

//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                sb.AppendLine(id);
//            }

//            string message = sb.ToString();
//            byte[] data = Encoding.UTF8.GetBytes(message);
//            accessor.WriteArray(0, data, 0, data.Length);
//        }
//    }

//    using (EventWaitHandle dataReady = new EventWaitHandle(false, EventResetMode.AutoReset, "DataReadyEvent"))
//    using (EventWaitHandle dataRead = new EventWaitHandle(false, EventResetMode.AutoReset, "DataReadEvent"))
//    {
//        Signal the client that data is ready to be read.
//        dataReady.Set();

//        Wait for the client to signal that it has finished reading the data.
//       dataRead.WaitOne();
//    }
//}

//private void buttonSend_Click(object sender, EventArgs e)
//{
//    string sharedMemoryName = "MySharedMemory";
//    int memorySize = 1024;

//    using (var sharedMemory = MemoryMappedFile.CreateNew(sharedMemoryName, memorySize))
//    {
//        using (var accessor = sharedMemory.CreateViewAccessor())
//        {
//            StringBuilder sb = new StringBuilder();

//            foreach (ListViewItem item in listViewElements.SelectedItems)
//            {
//                string id = item.SubItems[0].Text;
//                sb.AppendLine(id);
//            }

//            string message = sb.ToString();
//            byte[] data = Encoding.UTF8.GetBytes(message);
//            accessor.WriteArray(0, data, 0, data.Length);

//            MessageBox.Show("Data written to shared memory. Waiting for client to read...");
//        }
//    }
//}