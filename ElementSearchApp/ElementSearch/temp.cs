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
