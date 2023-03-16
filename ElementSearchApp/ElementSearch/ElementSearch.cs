using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElementSearch
{
    public partial class FormElementSearch : Form
    {
        public FormElementSearch()
        {
            InitializeComponent();
            this.Load += FormElementSearch_Load;
        }

        private async void FormElementSearch_Load(object sender, EventArgs e)
        {
            var fileTokens = new List<List<List<string>>>
            {
                new List<List<string>>(8),
                new List<List<string>>(2000),
                new List<List<string>>(950)
            };

            string[] filesPaths = { "logs\\_lst_LogData_elm.cvs", "logs\\_lst_LogData_chn.cvs", "logs\\_lst_LogData_dbs.cvs" };

            await Task.WhenAll(
                ReadTextFile(filesPaths[0], fileTokens[0]),
                ReadTextFile(filesPaths[1], fileTokens[1]),
                ReadTextFile(filesPaths[2], fileTokens[2])
            );

            FillTreeView(treeViewElemType, fileTokens[0]);
            FillTreeView(treeViewChannel, fileTokens[1]);
            FillTreeView(treeViewDatabase, fileTokens[2]);
        }

        private async Task ReadTextFile(string filePath, List<List<string>> fileTokens)
        {
            using var reader = new StreamReader(filePath);
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var tokens = line.Split('@').Where(token => !string.IsNullOrEmpty(token)).ToList();
                fileTokens.Add(tokens);
            }
        }

        private void FillTreeView(TreeView treeView, List<List<string>> fileTokens)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            foreach (var lineTokens in fileTokens)
            {
                if (lineTokens.Count < 3) continue;

                string hierarchy = lineTokens[0];
                uint.TryParse(lineTokens[1], out uint id);
                uint.TryParse(lineTokens[2], out uint handle);

                var relatives = hierarchy.Split('/').Select(relative => new MyTreeNode(relative, id, handle)).ToList();
                AddNode(treeView.Nodes, relatives, 0);
            }

            treeView.EndUpdate();
        }

        private void AddNode(TreeNodeCollection nodes, List<MyTreeNode> family, int index)
        {
            var current = family[index];
            var node = nodes.Cast<MyTreeNode>().FirstOrDefault(child => child.Text == current.Text);

            if (node == null)
            {
                node = new MyTreeNode(current.Text, current.Id, current.m_Handle);
                nodes.Add(node);
            }

            if (index < family.Count - 1)
            {
                AddNode(node.Nodes, family, index + 1);
            }
        }
    }
}
