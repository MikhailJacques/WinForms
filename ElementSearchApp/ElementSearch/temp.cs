//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace ElementSearch
//{
//    public partial class MainForm : Form
//    {
//        public MainForm()
//        {
//            InitializeComponent();
//        }

//        private async void MainForm_Load(object sender, EventArgs e)
//        {
//            string[] filePaths = {
//                "logs\\_lst_LogData_elm.cvs",
//                "logs\\_lst_LogData_chn.cvs",
//                "logs\\_lst_LogData_dbs.cvs"
//            };

//            List<List<List<string>>> fileTokens = new List<List<List<string>>>(3);

//            for (int i = 0; i < filePaths.Length; i++)
//            {
//                fileTokens.Add(await ReadTextFileAsync(filePaths[i]));
//            }

//            FillTreeView(treeViewElement, fileTokens[0]);
//            FillTreeView(treeViewChannel, fileTokens[1]);
//            FillTreeView(treeViewDatabase, fileTokens[2]);
//        }

//        private async Task<List<List<string>>> ReadTextFileAsync(string filePath)
//        {
//            using (StreamReader reader = new StreamReader(filePath))
//            {
//                List<List<string>> fileTokens = new List<List<string>>();
//                string line;

//                while ((line = await reader.ReadLineAsync()) != null)
//                {
//                    string[] tokens = line.Split('@');
//                    fileTokens.Add(tokens.ToList());
//                }

//                return fileTokens;
//            }
//        }

//        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//        {
//            treeView.BeginUpdate();
//            treeView.Nodes.Clear();

//            foreach (List<string> lineTokens in fileTokens)
//            {
//                if (!string.IsNullOrWhiteSpace(lineTokens[0]))
//                {
//                    string[] hierarchy = lineTokens[0].Split('/');
//                    int id = int.Parse(lineTokens[1]);
//                    int handle = int.Parse(lineTokens[2]);

//                    List<MyTreeNode> family = hierarchy.Select(relative => new MyTreeNode(relative, id, handle)).ToList();
//                    AddNode(treeView.Nodes, family, 0);
//                }
//            }

//            treeView.EndUpdate();
//            treeView.AfterCheck += TreeView_AfterCheck;
//        }

//        private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index)
//        {
//            MyTreeNode current = family[index];
//            MyTreeNode node = null;

//            foreach (MyTreeNode child in nodes)
//            {
//                if (child.Text == current.Text)
//                {
//                    node = child;
//                    break;
//                }
//            }

//            if (node == null)
//            {
//                node = new MyTreeNode(current.Text, current.Id, current.Handle);
//                nodes.Add(node);
//            }

//            if (index < family.Count - 1)
//            {
//                AddNode(node.Nodes, family, index + 1);
//            }
//        }

//        private void Tree


//private async void FormElementSearch_Load(object sender, EventArgs e)
//{
//    var fileTokens = new List<List<List<string>>>
//            {
//                new List<List<string>>(8),
//                new List<List<string>>(2000),
//                new List<List<string>>(950)
//            };

//    string[] filesPaths = { "logs\\_lst_LogData_elm.cvs", "logs\\_lst_LogData_chn.cvs", "logs\\_lst_LogData_dbs.cvs" };

//    await Task.WhenAll(
//        ReadTextFile(filesPaths[0], fileTokens[0]),
//        ReadTextFile(filesPaths[1], fileTokens[1]),
//        ReadTextFile(filesPaths[2], fileTokens[2])
//    );

//    FillTreeView(treeViewElemType, fileTokens[0]);
//    FillTreeView(treeViewChannel, fileTokens[1]);
//    FillTreeView(treeViewDatabase, fileTokens[2]);
//}

//private async Task ReadTextFile(string filePath, List<List<string>> fileTokens)
//{
//    using var reader = new StreamReader(filePath);
//    string line;

//    while ((line = await reader.ReadLineAsync()) != null)
//    {
//        var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//        fileTokens.Add(tokens);
//    }
//}

//private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//{
//    treeView.BeginUpdate();
//    treeView.Nodes.Clear();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (lineTokens.Count < 3)
//            continue;

//        string hierarchy = lineTokens[0];
//        uint.TryParse(lineTokens[1], out uint id);
//        uint.TryParse(lineTokens[2], out uint handle);

//        var relatives = hierarchy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//        AddNode(treeView.Nodes, relatives, 0);
//    }

//    treeView.EndUpdate();
//}


//// This method reads each text file and populates the respective TreeView control
//private async Task LoadTreeViewFromFileAsync(TreeView treeView, string filePath)
//{
//    try
//    {
//        using (StreamReader sr = new StreamReader(filePath))
//        {
//            string line;

//            while ((line = await sr.ReadLineAsync()) != null)
//            {
//                string[] parts = line.Split(',');

//                TreeNode parentNode = new TreeNode(parts[0]);
//                treeView.Nodes.Add(parentNode);

//                for (int i = 1; i < parts.Length; i++)
//                {
//                    TreeNode childNode = new TreeNode(parts[i]);
//                    parentNode.Nodes.Add(childNode);
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show($"Error loading TreeView: {ex.Message}");
//    }
//}

//private async void FormElementSearch_Load3(object sender, EventArgs e)
//{
//    var fileTokens = new List<List<List<string>>>
//            {
//                new List<List<string>>(8),
//                new List<List<string>>(2000),
//                new List<List<string>>(950)
//            };

//    string[] filesPaths = { "logs\\_lst_LogData_elm.cvs", "logs\\_lst_LogData_chn.cvs", "logs\\_lst_LogData_dbs.cvs" };

//    var readTasks = new List<Task>
//            {
//                ReadTextFile(filesPaths[0], fileTokens[0]),
//                ReadTextFile(filesPaths[1], fileTokens[1]),
//                ReadTextFile(filesPaths[2], fileTokens[2]),
//            };

//    var fillTasks = readTasks.Select((task, index) => task.ContinueWith(_ => FillTreeView3(treeViewList[index], fileTokens[index]))).ToList();

//    treeViewList.Add(treeViewElemType);
//    treeViewList.Add(treeViewChannel);
//    treeViewList.Add(treeViewDatabase);

//    await Task.WhenAll(fillTasks);
//}

//using ElementSearch;

//private void AddNode2(TreeNodeCollection nodes, List<MyTreeNode> family, int index)
//{
//    var current = family[index];

//    MyTreeNode node = null;

//    foreach (MyTreeNode child in nodes)
//    {
//        if (child.Text == current.Text)
//        {
//            node = child;
//            break;
//        }
//    }

//    if (node == null)
//    {
//        node = new MyTreeNode(current.Text, current.m_ID, current.m_Handle);
//        nodes.Add(node);
//    }

//    if (index < family.Count - 1)
//    {
//        AddNode2(node.Nodes, family, index + 1);
//    }
//}

//using ElementSearch;

//private async void FormElementSearch_Load(object sender, EventArgs e)
//{
//    var fileTokens = new List<List<List<string>>>
//            {
//                new List<List<string>>(8),
//                new List<List<string>>(2000),
//                new List<List<string>>(950)
//            };

//    string[] filesPaths = { "logs\\_lst_LogData_elm.cvs", "logs\\_lst_LogData_chn.cvs", "logs\\_lst_LogData_dbs.cvs" };

//    await Task.WhenAll(
//        ReadTextFile(filesPaths[0], fileTokens[0]),
//        ReadTextFile(filesPaths[1], fileTokens[1]),
//        ReadTextFile(filesPaths[2], fileTokens[2])
//    );

//    FillTreeView(treeViewElemType, fileTokens[0]);
//    FillTreeView(treeViewChannel, fileTokens[1]);
//    FillTreeView(treeViewDatabase, fileTokens[2]);
//}

//private async Task ReadTextFile(string filePath, List<List<string>> fileTokens)
//{
//    using var reader = new StreamReader(filePath);
//    string line;

//    while ((line = await reader.ReadLineAsync()) != null)
//    {
//        var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//        fileTokens.Add(tokens);
//    }
//}

//private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//{
//    treeView.BeginUpdate();
//    treeView.Nodes.Clear();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (lineTokens.Count < 3)
//            continue;

//        string hierarchy = lineTokens[0];
//        uint.TryParse(lineTokens[1], out uint id);
//        uint.TryParse(lineTokens[2], out uint handle);

//        var relatives = hierarchy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//        AddNode(treeView.Nodes, relatives, 0);
//    }

//    treeView.EndUpdate();
//}

//private async void FormElementSearch_Load2(object sender, EventArgs e)
//{
//    var fileTokens = new List<List<List<string>>>
//    {
//        new List<List<string>>(8),
//        new List<List<string>>(2000),
//        new List<List<string>>(950)
//    };

//    string[] filesPaths = { "logs\\_lst_LogData_elm.cvs", "logs\\_lst_LogData_chn.cvs", "logs\\_lst_LogData_dbs.cvs" };

//    var readTasks = new List<Task>
//    {
//        ReadTextFile2(filesPaths[0], fileTokens[0]),
//        ReadTextFile2(filesPaths[1], fileTokens[1]),
//        ReadTextFile2(filesPaths[2], fileTokens[2]),
//    };

//    var fillTasks = readTasks.Select((task, index) => task.ContinueWith(_ => Task.Run(() => FillTreeView2(treeViewList[index], fileTokens[index])))).ToList();

//    treeViewList.Add(treeViewElemType);
//    treeViewList.Add(treeViewChannel);
//    treeViewList.Add(treeViewDatabase);

//    await Task.WhenAll(fillTasks);
//}

//private async Task ReadTextFile2(string filePath, List<List<string>> fileTokens)
//{
//    using var reader = new StreamReader(filePath);
//    string line;

//    while ((line = await reader.ReadLineAsync()) != null)
//    {
//        var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//        fileTokens.Add(tokens);
//    }
//}

//private void FillTreeView2(TreeView treeView, List<List<string>> fileTokens)
//{
//    treeView.BeginUpdate();
//    treeView.Nodes.Clear();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (lineTokens.Count < 3)
//            continue;

//        string hierarchy = lineTokens[0];
//        uint.TryParse(lineTokens[1], out uint id);
//        uint.TryParse(lineTokens[2], out uint handle);

//        var relatives = hierarchy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//        AddNode(treeView.Nodes, relatives, 0);
//    }

//    treeView.EndUpdate();
//}

//private void FormElementSearch_Load(object sender, EventArgs e);
//private List<List<string>> ReadTextFile(string filePath);
//private void FillTreeView(TreeView treeView, IReadOnlyList<List<string>> fileTokens);
//private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index);

//private async void FormElementSearch_Load2(object sender, EventArgs e);
//private async Task ReadTextFile2(string filePath, List<List<string>> fileTokens);
//private void FillTreeView2(TreeView treeView, List<List<string>> fileTokens);
//private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index);


//private void FormElementSearch_Load(object sender, EventArgs e)
//{
//    var fileTokens = new List<List<List<string>>>
//    {
//        new List<List<string>>(8),
//        new List<List<string>>(2000),
//        new List<List<string>>(950)
//    };

//    var filesPaths = new List<string>
//    {
//        "logs\\_lst_LogData_elm.cvs",
//        "logs\\_lst_LogData_chn.cvs",
//        "logs\\_lst_LogData_dbs.cvs"
//    };

//    var tasks = filesPaths.Select((filePath, i) => Task.Run(() =>
//    {
//        fileTokens[i] = ReadTextFile(filePath);
//    })).ToArray();

//    Task.WhenAll(tasks).ContinueWith(_ =>
//    {
//        FillTreeView(treeViewElemType, fileTokens[0]);
//        FillTreeView(treeViewChannel, fileTokens[1]);
//        FillTreeView(treeViewDatabase, fileTokens[2]);
//    }, TaskScheduler.FromCurrentSynchronizationContext());
//}

//private List<List<string>> ReadTextFile(string filePath)
//{
//    var fileTokens = new List<List<string>>();
//    using (var infile = new StreamReader(filePath))
//    {
//        string line;
//        while ((line = infile.ReadLine()) != null)
//        {
//            var lineTokens = line.Split('@').ToList();
//            fileTokens.Add(lineTokens);
//        }
//    }

//    return fileTokens;
//}

//private void FillTreeView(TreeView treeView, IReadOnlyList<List<string>> fileTokens)
//{
//    treeView.BeginUpdate();
//    treeView.Nodes.Clear();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (string.IsNullOrEmpty(lineTokens[0]))
//            continue;

//        var id = string.IsNullOrEmpty(lineTokens[1]) ? 0 : uint.Parse(lineTokens[1]);
//        var handle = string.IsNullOrEmpty(lineTokens[2]) ? 0 : uint.Parse(lineTokens[2]);
//        var family = lineTokens[0].Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//        AddNode(treeView.Nodes, family, 0);
//    }

//    treeView.EndUpdate();
//}

//private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index)
//{
//    var current = family[index];
//    var node = nodes.Cast<MyTreeNode>().FirstOrDefault(child => child.Text == current.Text);

//    if (node == null)
//    {
//        node = new MyTreeNode(current.Text, current.m_ID, current.m_Handle);
//        nodes.Add(node);
//    }

//    if (index < family.Count - 1)
//    {
//        AddNode(node.Nodes, family, index + 1);
//    }
//}

//// This method takes a TreeView as input and returns the corresponding column index for the ListView.
//// It maps treeViewElemType to 0, treeViewChannel to 1, and treeViewDatabase to 2.
//// If the provided TreeView doesn't match any of the known tree views, it returns -1.
//using ElementSearch;

//private int GetTreeViewIndex(TreeView treeView)
//{
//    if (treeView == treeViewElemType)
//    {
//        return 0;
//    }
//    else if (treeView == treeViewChannel)
//    {
//        return 1;
//    }
//    else if (treeView == treeViewDatabase)
//    {
//        return 2;
//    }

//    return -1;
//}

//private void RemoveCheckedNodesFromListView(string nodeText, int columnIndex)
//{
//    foreach (ListViewItem item in listViewElements.Items)
//    {
//        if (item.SubItems[columnIndex].Text == nodeText)
//        {
//            listViewElements.Items.Remove(item);
//            break;
//        }
//    }
//}


//private void HandleTreeViewAfterCheck(object? sender, TreeViewEventArgs e)
//{
//    if (e.Action == TreeViewAction.Unknown)
//    {
//        return;
//    }

//    var node = e.Node as MyTreeNode;

//    if (node != null)
//    {
//        UpdateChildNodes(node, node.Checked);
//    }

//    TreeView treeView = (TreeView)sender;
//    treeView.AfterCheck -= HandleTreeViewAfterCheck;

//    if (node != null)
//    {
//        if (node.Checked)
//        {
//            AddCheckedNodesToListView(new[] { node }, GetTreeViewIndex(treeView));
//        }
//        else
//        {
//            RemoveCheckedNodesFromListView(new[] { node }, GetTreeViewIndex(treeView));
//        }

//        if (node.Parent != null)
//        {
//            UpdateParentNode(node.Parent as MyTreeNode);
//        }
//    }

//    treeView.AfterCheck += HandleTreeViewAfterCheck;
//}

// Latest TODO:

//private void FormElementSearch_Load(object sender, EventArgs e)
//{
//    string filePath = "logs\\lst_LogData_elm_all.cvs";
//    List<List<string>> fileTokens = ReadTextFile(filePath);
//    FillListView(listViewElements, fileTokens);
//}

//private List<List<string>> ReadTextFile(string filePath)
//{
//    List<List<string>> fileTokens = new List<List<string>>();

//    using (var reader = new StreamReader(filePath))
//    {
//        string line;

//        while ((line = reader.ReadLine()) != null)
//        {
//            var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//            fileTokens.Add(tokens);
//        }
//    }

//    return fileTokens;
//}

//private void FillListView(ListView listView, List<List<string>> fileTokens)
//{
//    listView.BeginUpdate();
//    listView.Items.Clear();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (lineTokens.Count < 8)
//            continue;

//        ListViewItem newItem = new ListViewItem(lineTokens[0]); // ID

//        newItem.SubItems.Add(lineTokens[1]); // Long Name
//        newItem.SubItems.Add(lineTokens[2]); // Short Name
//        newItem.SubItems.Add(lineTokens[3]); // Elem Type
//        newItem.SubItems.Add(lineTokens[4]); // Channel
//        newItem.SubItems.Add(lineTokens[5]); // Database
//        newItem.SubItems.Add(lineTokens[6]); // Location
//        newItem.SubItems.Add(lineTokens[7]); // Handle

//        listView.Items.Add(newItem);
//    }

//    listView.EndUpdate();
//}

//// This implementation uses the async/await pattern and the Task.WhenAll() function to read the three text files in parallel.
//// The ReadTextFile function returns a Task<List<List<string>>> instead of directly modifying a passed-in list.
//// This allows us to use Task.WhenAll() to wait for all file reading tasks to complete before proceeding to fill the TreeView controls.
//private async void FormElementSearch_Load(object sender, EventArgs e)


//// In this implementation, we have three separate event handlers, one for each TreeView control.
//// Each of these handlers calls the HandleTreeViewAfterCheck function, which contains the shared logic for all three TreeView controls.
//// The HandleTreeViewAfterCheck function manages the addition and removal of the event handler based on the sender,
//// which is the specific TreeView control that fired the event.
//private void treeViewElemType_AfterCheck(object sender, TreeViewEventArgs e)
//{
//    HandleTreeViewAfterCheck(sender, e);

//    UpdateListView();
//}


//// This function is an event handler for the AfterCheck event on the TreeView control.
//// When a node's checkbox is checked or unchecked, it updates the parent and child nodes accordingly.
//// It also temporarily removes the event handler to prevent recursion before re-adding it after the updates are completed.
//private void HandleTreeViewAfterCheck(object? sender, TreeViewEventArgs e)


//private async Task<List<List<string>>> ReadTextFile(string filePath)
//{
//    var fileTokens = new List<List<string>>();

//    using (var reader = new StreamReader(filePath))
//    {
//        string? line;
//        while ((line = await reader.ReadLineAsync()) != null)
//        {
//            var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//            fileTokens.Add(tokens);
//        }
//    }

//    return fileTokens;
//}

//private async Task<List<List<string>>> ReadTextFile(string filePath)
//{
//    var fileTokens = new List<List<string>>();

//    using (var reader = File.ReadLines(filePath))
//    {
//        foreach (var line in reader)
//        {
//            var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
//            fileTokens.Add(tokens);
//        }
//    }

//    return fileTokens;
//}


//private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
//{
//    treeView.BeginUpdate();
//    treeView.Nodes.Clear();

//    foreach (var lineTokens in fileTokens)
//    {
//        if (lineTokens.Count < 3)
//            continue;

//        uint.TryParse(lineTokens[0], out uint id);
//        string family_hierarhy = lineTokens[1];
//        uint.TryParse(lineTokens[2], out uint handle);

//        var relatives = family_hierarhy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

//        AddNode(treeView.Nodes, relatives, 0);
//    }

//    treeView.EndUpdate();
//}


//private IEnumerable<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
//{
//    return nodes.Cast<TreeNode>()
//        .Where(node => node.Checked)
//        .Concat(nodes.Cast<TreeNode>().SelectMany(node => GetCheckedNodes(node.Nodes)));
//}

//        private void UpdateListView()
//        {
//            listViewElements.Items.Clear();

//            var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes).OfType<TreeNode>().ToList();
//            var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<TreeNode>().ToList();
//            var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<TreeNode>().ToList();

//            var allCheckedNodes = checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes);

//            foreach (var node in allCheckedNodes)
//            {
//                var originalNode = node.Tag as MyTreeNode; // Retrieve the original MyTreeNode object from the Tag property

//#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//                if (originalNode != null && _elementDataById.TryGetValue(originalNode.m_ID, out ElementData elementData))
//#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
//                {
//                    ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
//                    newItem.SubItems.Add(elementData.LongName);
//                    newItem.SubItems.Add(elementData.ShortName);
//                    newItem.SubItems.Add(elementData.ElementType);
//                    newItem.SubItems.Add(elementData.Channel);
//                    newItem.SubItems.Add(elementData.Database);
//                    newItem.SubItems.Add(elementData.Location);
//                    newItem.SubItems.Add(elementData.Handle.ToString());

//                    listViewElements.Items.Add(newItem);
//                }
//            }
//        }


//private void UpdateListView()
//{
//    listViewElements.Items.Clear();

//    var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes).OfType<MyTreeNode>().ToList();
//    var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<MyTreeNode>().ToList();
//    var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<MyTreeNode>().ToList();

//    var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => node.m_ID));

//    foreach (var id in _elementDataById.Keys)
//    {
//        if (allCheckedNodeIds.Contains(id))
//        {
//            ElementData elementData = _elementDataById[id];
//            ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
//            newItem.SubItems.Add(elementData.LongName);
//            newItem.SubItems.Add(elementData.ShortName);
//            newItem.SubItems.Add(elementData.ElementType);
//            newItem.SubItems.Add(elementData.Channel);
//            newItem.SubItems.Add(elementData.Database);
//            newItem.SubItems.Add(elementData.Location);
//            newItem.SubItems.Add(elementData.Handle.ToString());

//            listViewElements.Items.Add(newItem);
//        }
//    }
//}

//private void AddCheckedNodesToListView(IEnumerable<TreeNode> checkedNodes, int columnIndex)
//{
//    foreach (var node in checkedNodes)
//    {
//        // Check if the node is a child node (has no children)
//        if (node.Nodes.Count == 0)
//        {
//            var newItem = new ListViewItem();

//            // Replace the node separator with the desired separator
//            string fullPath = node.FullPath.Replace("\\", "/");

//            if (columnIndex == 0)
//            {
//                newItem.Text = fullPath;
//            }
//            else
//            {
//                newItem.Text = "";

//                // Add empty subitems for the previous columns
//                for (int i = 0; i < columnIndex; i++)
//                {
//                    newItem.SubItems.Add("");
//                }

//                newItem.SubItems.Add(fullPath);
//            }

//            listViewElements.Items.Add(newItem);
//        }
//    }
//}

//private void UpdateListView()
//{
//    listViewElements.Items.Clear();

//    var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes).OfType<MyTreeNode>().ToList();
//    var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<MyTreeNode>().ToList();
//    var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<MyTreeNode>().ToList();

//    var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => node.m_ID));

//    Dictionary<uint, string> elementTypeLongNameById = treeViewElemType.Nodes
//        .Cast<MyTreeNode>()
//        .ToDictionary(node => node.m_ID, node => node.Text);

//    Dictionary<uint, string> channelLongNameById = treeViewChannel.Nodes
//        .Cast<MyTreeNode>()
//        .ToDictionary(node => node.m_ID, node => node.Text);

//    Dictionary<uint, string> databaseLongNameById = treeViewDatabase.Nodes
//        .Cast<MyTreeNode>()
//        .ToDictionary(node => node.m_ID, node => node.Text);

//    foreach (var id in _elementDataById.Keys)
//    {
//        if (allCheckedNodeIds.Contains(id))
//        {
//            ElementData elementData = _elementDataById[id];
//            ListViewItem newItem = new ListViewItem(elementData.ID.ToString());
//            newItem.SubItems.Add(elementData.LongName);
//            newItem.SubItems.Add(elementData.ShortName);

//            uint.TryParse(elementData.ElementType, out uint elementType);
//            newItem.SubItems.Add(elementTypeLongNameById.TryGetValue(elementType, out string elementTypeLongName) ? elementTypeLongName : elementData.ElementType);

//            uint.TryParse(elementData.Channel, out uint channel);
//            newItem.SubItems.Add(channelLongNameById.TryGetValue(channel, out string channelLongName) ? channelLongName : elementData.Channel);

//            uint.TryParse(elementData.Database, out uint database);
//            newItem.SubItems.Add(databaseLongNameById.TryGetValue(database, out string databaseLongName) ? databaseLongName : elementData.Database);

//            newItem.SubItems.Add(elementData.Location);
//            newItem.SubItems.Add(elementData.Handle.ToString());

//            listViewElements.Items.Add(newItem);
//        }
//    }
//}