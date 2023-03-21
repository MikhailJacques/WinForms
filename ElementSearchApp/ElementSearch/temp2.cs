//namespace ElementSearch
//{
//    public partial class FormElementSearch : Form
//    {
//        public FormElementSearch()
//        {
//            InitializeComponent();
//        }

//        private Dictionary<uint, ElementData> _elementDataById = new Dictionary<uint, ElementData>();

//        private async void FormElementSearch_Load(object sender, EventArgs e)
//        {
//            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? Environment.CurrentDirectory;

//            string elm_type_file_path = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_type.txt");
//            string chn_file_path = Path.Combine(projectDirectory, "data", "_lst_LogData_chn.txt");
//            string dbs_file_path = Path.Combine(projectDirectory, "data", "_lst_LogData_dbs.txt");
//            string elm_all_file_path = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_all.txt");

//            string[] filesPaths = { elm_type_file_path, chn_file_path, dbs_file_path, elm_all_file_path };

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

//        private void UpdateListView()
//        {
//            listViewElements.Items.Clear();

//            var checkedElemTypeNodes = GetCheckedNodes(treeViewElemType.Nodes).OfType<TreeNode>().ToList();
//            var checkedChannelNodes = GetCheckedNodes(treeViewChannel.Nodes).OfType<TreeNode>().ToList();
//            var checkedDatabaseNodes = GetCheckedNodes(treeViewDatabase.Nodes).OfType<TreeNode>().ToList();

//            var allCheckedNodeIds = new HashSet<uint>(checkedElemTypeNodes.Concat(checkedChannelNodes).Concat(checkedDatabaseNodes).Select(node => (node.Tag as MyTreeNode)?.m_ID ?? 0));

//            foreach (var id in _elementDataById.Keys)
//            {
//                if (allCheckedNodeIds.Contains(id))
//                {
//                    ElementData elementData = _elementDataById[id];
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
//    }
//}