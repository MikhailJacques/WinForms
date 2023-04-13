
using System.Windows.Forms;

namespace ElementSearch
{
    partial class ElementSearch : Form
    {
        /// Required designer variable.
        private System.ComponentModel.IContainer components = null;

        /// Clean up any resources being used.
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// Required method for Designer support - do not modify the contents of this method with the code editor.
        private void InitializeComponent()
        {
            this.treeViewElementType = new System.Windows.Forms.TreeView();
            this.listViewElements = new System.Windows.Forms.ListView();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLongName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderShortName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderElementType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderChannel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHandle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewChannel = new System.Windows.Forms.TreeView();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.textBoxElementType = new System.Windows.Forms.TextBox();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxElementName = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxElementType = new System.Windows.Forms.CheckBox();
            this.checkBoxChannel = new System.Windows.Forms.CheckBox();
            this.checkBoxDatabase = new System.Windows.Forms.CheckBox();
            this.checkBoxElements = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeViewElementType
            // 
            this.treeViewElementType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewElementType.CheckBoxes = true;
            this.treeViewElementType.Location = new System.Drawing.Point(4, 35);
            this.treeViewElementType.Name = "treeViewElementType";
            this.treeViewElementType.Size = new System.Drawing.Size(326, 224);
            this.treeViewElementType.TabIndex = 0;
            this.treeViewElementType.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
            // 
            // listViewElements
            // 
            this.listViewElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewElements.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnHeaderLongName,
            this.columnHeaderShortName,
            this.columnHeaderElementType,
            this.columnHeaderChannel,
            this.columnHeaderDatabase,
            this.columnHeaderLocation,
            this.columnHeaderHandle});
            this.listViewElements.FullRowSelect = true;
            this.listViewElements.HideSelection = false;
            this.listViewElements.Location = new System.Drawing.Point(337, 35);
            this.listViewElements.Name = "listViewElements";
            this.listViewElements.Size = new System.Drawing.Size(988, 728);
            this.listViewElements.TabIndex = 1;
            this.listViewElements.UseCompatibleStateImageBehavior = false;
            this.listViewElements.View = System.Windows.Forms.View.Details;
            this.listViewElements.VirtualMode = true;
            this.listViewElements.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewElements_ColumnClick);
            this.listViewElements.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewElements_RetrieveVirtualItem);
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "ID";
            this.columnHeaderID.Width = 40;
            // 
            // columnHeaderLongName
            // 
            this.columnHeaderLongName.Text = "Long Name";
            this.columnHeaderLongName.Width = 200;
            // 
            // columnHeaderShortName
            // 
            this.columnHeaderShortName.Text = "Short Name";
            this.columnHeaderShortName.Width = 100;
            // 
            // columnHeaderElementType
            // 
            this.columnHeaderElementType.Text = "Element Type";
            this.columnHeaderElementType.Width = 100;
            // 
            // columnHeaderChannel
            // 
            this.columnHeaderChannel.Text = "Channel";
            this.columnHeaderChannel.Width = 200;
            // 
            // columnHeaderDatabase
            // 
            this.columnHeaderDatabase.Text = "Database";
            this.columnHeaderDatabase.Width = 200;
            // 
            // columnHeaderLocation
            // 
            this.columnHeaderLocation.Text = "Location";
            // 
            // columnHeaderHandle
            // 
            this.columnHeaderHandle.Text = "Handle";
            this.columnHeaderHandle.Width = 80;
            // 
            // treeViewChannel
            // 
            this.treeViewChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewChannel.CheckBoxes = true;
            this.treeViewChannel.Location = new System.Drawing.Point(4, 287);
            this.treeViewChannel.Name = "treeViewChannel";
            this.treeViewChannel.Size = new System.Drawing.Size(326, 224);
            this.treeViewChannel.TabIndex = 2;
            this.treeViewChannel.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewDatabase.CheckBoxes = true;
            this.treeViewDatabase.Location = new System.Drawing.Point(4, 539);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(326, 224);
            this.treeViewDatabase.TabIndex = 3;
            this.treeViewDatabase.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
            // 
            // textBoxElementType
            // 
            this.textBoxElementType.Location = new System.Drawing.Point(139, 12);
            this.textBoxElementType.Name = "textBoxElementType";
            this.textBoxElementType.Size = new System.Drawing.Size(191, 20);
            this.textBoxElementType.TabIndex = 4;
            this.textBoxElementType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxElementType_KeyDown);
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(139, 261);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(191, 20);
            this.textBoxChannel.TabIndex = 5;
            this.textBoxChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxChannel_KeyDown);
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(139, 513);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(191, 20);
            this.textBoxDatabase.TabIndex = 6;
            this.textBoxDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxDatabase_KeyDown);
            // 
            // textBoxElementName
            // 
            this.textBoxElementName.Location = new System.Drawing.Point(366, 12);
            this.textBoxElementName.Name = "textBoxElementName";
            this.textBoxElementName.Size = new System.Drawing.Size(170, 20);
            this.textBoxElementName.TabIndex = 7;
            this.textBoxElementName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxElementName_KeyDown);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonClear.Location = new System.Drawing.Point(542, 11);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(76, 21);
            this.buttonClear.TabIndex = 17;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSend.Location = new System.Drawing.Point(624, 11);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(76, 21);
            this.buttonSend.TabIndex = 18;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(51, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Element Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(51, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Channel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(51, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Database";
            // 
            // checkBoxElementType
            // 
            this.checkBoxElementType.AutoSize = true;
            this.checkBoxElementType.Location = new System.Drawing.Point(26, 14);
            this.checkBoxElementType.Name = "checkBoxElementType";
            this.checkBoxElementType.Size = new System.Drawing.Size(15, 14);
            this.checkBoxElementType.TabIndex = 22;
            this.checkBoxElementType.UseVisualStyleBackColor = true;
            this.checkBoxElementType.CheckedChanged += new System.EventHandler(this.checkBoxElementType_CheckedChanged);
            // 
            // checkBoxChannel
            // 
            this.checkBoxChannel.AutoSize = true;
            this.checkBoxChannel.Location = new System.Drawing.Point(26, 264);
            this.checkBoxChannel.Name = "checkBoxChannel";
            this.checkBoxChannel.Size = new System.Drawing.Size(15, 14);
            this.checkBoxChannel.TabIndex = 23;
            this.checkBoxChannel.UseVisualStyleBackColor = true;
            this.checkBoxChannel.CheckStateChanged += new System.EventHandler(this.checkBoxChannel_CheckedChanged);
            // 
            // checkBoxDatabase
            // 
            this.checkBoxDatabase.AutoSize = true;
            this.checkBoxDatabase.Location = new System.Drawing.Point(26, 519);
            this.checkBoxDatabase.Name = "checkBoxDatabase";
            this.checkBoxDatabase.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDatabase.TabIndex = 24;
            this.checkBoxDatabase.UseVisualStyleBackColor = true;
            this.checkBoxDatabase.Click += new System.EventHandler(this.checkBoxDatabase_CheckedChanged);
            // 
            // checkBoxElements
            // 
            this.checkBoxElements.AutoSize = true;
            this.checkBoxElements.Location = new System.Drawing.Point(345, 16);
            this.checkBoxElements.Name = "checkBoxElements";
            this.checkBoxElements.Size = new System.Drawing.Size(15, 14);
            this.checkBoxElements.TabIndex = 25;
            this.checkBoxElements.UseVisualStyleBackColor = true;
            this.checkBoxElements.CheckedChanged += new System.EventHandler(this.checkBoxElements_CheckedChanged);
            // 
            // ElementSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 767);
            this.Controls.Add(this.checkBoxElements);
            this.Controls.Add(this.checkBoxDatabase);
            this.Controls.Add(this.checkBoxChannel);
            this.Controls.Add(this.checkBoxElementType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxElementName);
            this.Controls.Add(this.textBoxDatabase);
            this.Controls.Add(this.textBoxChannel);
            this.Controls.Add(this.textBoxElementType);
            this.Controls.Add(this.treeViewDatabase);
            this.Controls.Add(this.treeViewChannel);
            this.Controls.Add(this.listViewElements);
            this.Controls.Add(this.treeViewElementType);
            this.Name = "ElementSearch";
            this.Text = "Element Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView treeViewElementType;
        private ListView listViewElements;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderLongName;
        private ColumnHeader columnHeaderShortName;
        private ColumnHeader columnHeaderElementType;
        private ColumnHeader columnHeaderChannel;
        private ColumnHeader columnHeaderDatabase;
        private ColumnHeader columnHeaderLocation;
        private ColumnHeader columnHeaderHandle;
        private TreeView treeViewChannel;
        private TreeView treeViewDatabase;
        private TextBox textBoxElementType;
        private TextBox textBoxChannel;
        private TextBox textBoxDatabase;
        private TextBox textBoxElementName;
        private Button buttonClear;
        private Button buttonSend;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox checkBoxElementType;
        private CheckBox checkBoxChannel;
        private CheckBox checkBoxDatabase;
        private CheckBox checkBoxElements;
    }
}