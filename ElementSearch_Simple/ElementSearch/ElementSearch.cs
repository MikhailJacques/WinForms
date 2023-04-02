using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ElementSearch
{
    public partial class ElementSearch : Form
    {
        private Dictionary<uint, ElementData> elementDataDict;

        public ElementSearch()
        {
            InitializeComponent();
            LoadTreeViewData();
            LoadListViewData();
            AddTreeViewEventHandlers();
        }

        private void LoadTreeViewData()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string databaseFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_dbs.txt");

            var fileTokens = new List<List<string>>();

            using (StreamReader reader = new StreamReader(databaseFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = line.Split('@').ToList();
                    fileTokens.Add(tokens);
                }
            }

            FillTreeView(treeViewDatabase, fileTokens);
        }

        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
        {
            var nodeLookup = new Dictionary<string, MyTreeNode>();

            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count < 3)
                    continue;

                uint.TryParse(lineTokens[0], out uint id);
                string family_hierarhy = lineTokens[1];
                uint.TryParse(lineTokens[2], out uint handle);

                var relatives = family_hierarhy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();

                AddNode(treeView.Nodes, relatives, 0, nodeLookup);
            }
        }
        private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index, Dictionary<string, MyTreeNode> nodeLookup)
        {
            if (index < family.Count)
            {
                var currentRelative = family[index];
                var currentNodeKey = currentRelative.Text;

                if (!nodeLookup.TryGetValue(currentNodeKey, out MyTreeNode currentNode))
                {
                    currentNode = currentRelative;
                    nodes.Add(currentNode);
                    nodeLookup[currentNodeKey] = currentNode;
                }

                AddNode(currentNode.Nodes, family, index + 1, nodeLookup);
            }
        }

        private void LoadListViewData()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string elementDataFilePath = Path.Combine(projectDirectory, "data", "_lst_LogData_elm_all.txt");

            elementDataDict = new Dictionary<uint, ElementData>();

            using (StreamReader reader = new StreamReader(elementDataFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = line.Split('@');
                    var element = new ElementData
                    {
                        ID = uint.Parse(tokens[0]),
                        LongName = tokens[1],
                        ShortName = tokens[2],
                        ElementType = tokens[3],
                        Channel = tokens[4],
                        Database = tokens[5],
                        Location = tokens[6],
                        Handle = uint.Parse(tokens[7])
                    };

                    elementDataDict[element.ID] = element;
                }
            }
        }

        private void AddTreeViewEventHandlers()
        {
            treeViewDatabase.AfterCheck += (sender, e) =>
            {
                if (e.Node is MyTreeNode myNode)
                {
                    // Update the checked state of all child nodes recursively
                    UpdateChildNodesCheckedState(e.Node, e.Node.Checked);

                    if (elementDataDict.TryGetValue(myNode._ID, out ElementData element))
                    {
                        if (e.Node.Checked)
                        {
                            AddElementToListView(element);
                        }
                        else
                        {
                            RemoveElementFromListView(element.ID);
                        }
                    }
                }
            };
        }

        private void UpdateChildNodesCheckedState(TreeNode parentNode, bool isChecked)
        {
            foreach (TreeNode childNode in parentNode.Nodes)
            {
                childNode.Checked = isChecked;
                UpdateChildNodesCheckedState(childNode, isChecked);
            }
        }



        private void AddElementToListView(ElementData element)
        {
            var item = new ListViewItem(element.ID.ToString());
            item.SubItems.Add(element.LongName);
            item.SubItems.Add(element.ShortName);
            item.SubItems.Add(element.ElementType);
            item.SubItems.Add(element.Channel);
            item.SubItems.Add(element.Database);
            item.SubItems.Add(element.Location);
            item.SubItems.Add(element.Handle.ToString());

            listViewElements.Items.Add(item);
        }

        private void RemoveElementFromListView(uint id)
        {
            ListViewItem itemToRemove = null;

            foreach (ListViewItem item in listViewElements.Items)
            {
                if (item.Text == id.ToString())
                {
                    itemToRemove = item;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                listViewElements.Items.Remove(itemToRemove);
            }
        }
        private void AddNodeDataToListView(uint nodeId)
        {
            if (elementDataDict.TryGetValue(nodeId, out ElementData elementData))
            {
                ListViewItem listViewItem = new ListViewItem(elementData.ID.ToString());
                listViewItem.SubItems.Add(elementData.LongName);
                listViewItem.SubItems.Add(elementData.ShortName);
                listViewItem.SubItems.Add(elementData.ElementType);
                listViewItem.SubItems.Add(elementData.Channel);
                listViewItem.SubItems.Add(elementData.Database);
                listViewItem.SubItems.Add(elementData.Location);
                listViewItem.SubItems.Add(elementData.Handle.ToString());
                listViewElements.Items.Add(listViewItem);
            }
        }

        private void RemoveNodeDataFromListView(uint nodeId)
        {
            ListViewItem itemToRemove = null;

            foreach (ListViewItem listViewItem in listViewElements.Items)
            {
                if (listViewItem.Text == nodeId.ToString())
                {
                    itemToRemove = listViewItem;
                    break;
                }
            }

            if (itemToRemove != null)
            {
                listViewElements.Items.Remove(itemToRemove);
            }
        }
    }
}
