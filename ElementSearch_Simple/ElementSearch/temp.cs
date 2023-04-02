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
//        private Dictionary<uint, ElementData> elementDataDict;

//        public ElementSearch()
//        {
//            InitializeComponent();
//            InitializeTreeViewAndListView();
//            LoadTreeViewData();
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

//        private void LoadTreeViewData()
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
//            string elementDataFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_all.txt");

//            elementDataDict = new Dictionary<uint, ElementData>();

//            using (StreamReader reader = new StreamReader(elementDataFilePath))
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
//                        ElementType = tokens[3],
//                        Channel = tokens[4],
//                        Database = tokens[5],
//                        Location = tokens[6],
//                        Handle = uint.Parse(tokens[7])
//                    };

//                    elementDataDict[element.ID] = element;
//                }
//            }
//        }

//        private void AddTreeViewEventHandlers()
//        {
//            treeViewDatabase.AfterCheck += (sender, e) =>
//            {
//                if (e.Node is MyTreeNode myNode)
//                {
//                    if (elementDataDict.TryGetValue(myNode._ID, out ElementData element))
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
//            item.SubItems.Add(element.ElementType);
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
//                ElementData elementData = elementDataDict.FirstOrDefault(el => el.Value.ID == nodeId).Value;

//                if (elementData != null)
//                {
//                    // Add element data to the ListView control
//                    ListViewItem listViewItem = new ListViewItem(new string[]
//                    {
//                        elementData.ID.ToString(),
//                        elementData.LongName,
//                        elementData.ShortName,
//                        elementData.ElementType,
//                        elementData.Channel,
//                        elementData.Database,
//                        elementData.Location,
//                        elementData.Handle.ToString()
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
//            if (elementDataDict.TryGetValue(myNode._ID, out ElementData element))
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

