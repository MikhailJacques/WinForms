
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
            this.treeViewType = new System.Windows.Forms.TreeView();
            this.listViewElements = new System.Windows.Forms.ListView();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLongName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderShortName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderChannel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHandle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewChannel = new System.Windows.Forms.TreeView();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxElementName = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelType = new System.Windows.Forms.Label();
            this.labelChannel = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.checkBoxType = new System.Windows.Forms.CheckBox();
            this.checkBoxChannel = new System.Windows.Forms.CheckBox();
            this.checkBoxDatabase = new System.Windows.Forms.CheckBox();
            this.checkBoxElements = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeViewType
            // 
            this.treeViewType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewType.CheckBoxes = true;
            this.treeViewType.Location = new System.Drawing.Point(4, 35);
            this.treeViewType.Name = "treeViewType";
            this.treeViewType.Size = new System.Drawing.Size(326, 224);
            this.treeViewType.TabIndex = 0;
            this.treeViewType.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
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
            this.columnHeaderType,
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
            this.listViewElements.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewElements_ColumnClick);
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
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Element Type";
            this.columnHeaderType.Width = 100;
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
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(139, 12);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(191, 20);
            this.textBoxType.TabIndex = 4;
            this.textBoxType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(139, 261);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(191, 20);
            this.textBoxChannel.TabIndex = 5;
            this.textBoxChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(139, 513);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(191, 20);
            this.textBoxDatabase.TabIndex = 6;
            this.textBoxDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // textBoxElementName
            // 
            this.textBoxElementName.Location = new System.Drawing.Point(366, 12);
            this.textBoxElementName.Name = "textBoxElementName";
            this.textBoxElementName.Size = new System.Drawing.Size(170, 20);
            this.textBoxElementName.TabIndex = 7;
            this.textBoxElementName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxElementName_KeyDown);
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
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
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
            this.buttonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelType.Location = new System.Drawing.Point(51, 13);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(82, 15);
            this.labelType.TabIndex = 19;
            this.labelType.Text = "Element Type";
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelChannel.Location = new System.Drawing.Point(51, 263);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(51, 15);
            this.labelChannel.TabIndex = 20;
            this.labelChannel.Text = "Channel";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelDatabase.Location = new System.Drawing.Point(51, 518);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(58, 15);
            this.labelDatabase.TabIndex = 21;
            this.labelDatabase.Text = "Database";
            // 
            // checkBoxType
            // 
            this.checkBoxType.AutoSize = true;
            this.checkBoxType.Location = new System.Drawing.Point(26, 14);
            this.checkBoxType.Name = "checkBoxType";
            this.checkBoxType.Size = new System.Drawing.Size(15, 14);
            this.checkBoxType.TabIndex = 22;
            this.checkBoxType.UseVisualStyleBackColor = true;
            this.checkBoxType.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);  // CheckBoxElementType_CheckedChanged);
            // 
            // checkBoxChannel
            // 
            this.checkBoxChannel.AutoSize = true;
            this.checkBoxChannel.Location = new System.Drawing.Point(26, 264);
            this.checkBoxChannel.Name = "checkBoxChannel";
            this.checkBoxChannel.Size = new System.Drawing.Size(15, 14);
            this.checkBoxChannel.TabIndex = 23;
            this.checkBoxChannel.UseVisualStyleBackColor = true;
            this.checkBoxChannel.CheckStateChanged += new System.EventHandler(this.CheckBox_CheckedChanged); // CheckBoxChannel_CheckedChanged);
            // 
            // checkBoxDatabase
            // 
            this.checkBoxDatabase.AutoSize = true;
            this.checkBoxDatabase.Location = new System.Drawing.Point(26, 519);
            this.checkBoxDatabase.Name = "checkBoxDatabase";
            this.checkBoxDatabase.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDatabase.TabIndex = 24;
            this.checkBoxDatabase.UseVisualStyleBackColor = true;
            this.checkBoxDatabase.Click += new System.EventHandler(this.CheckBox_CheckedChanged);   // CheckBoxDatabase_CheckedChanged);
            // 
            // checkBoxElements
            // 
            this.checkBoxElements.AutoSize = true;
            this.checkBoxElements.Location = new System.Drawing.Point(345, 16);
            this.checkBoxElements.Name = "checkBoxElements";
            this.checkBoxElements.Size = new System.Drawing.Size(15, 14);
            this.checkBoxElements.TabIndex = 25;
            this.checkBoxElements.UseVisualStyleBackColor = true;
            this.checkBoxElements.CheckedChanged += new System.EventHandler(this.CheckBoxElements_CheckedChanged);
            // 
            // ElementSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 767);
            this.Controls.Add(this.checkBoxElements);
            this.Controls.Add(this.checkBoxDatabase);
            this.Controls.Add(this.checkBoxChannel);
            this.Controls.Add(this.checkBoxType);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.labelChannel);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxElementName);
            this.Controls.Add(this.textBoxDatabase);
            this.Controls.Add(this.textBoxChannel);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.treeViewDatabase);
            this.Controls.Add(this.treeViewChannel);
            this.Controls.Add(this.listViewElements);
            this.Controls.Add(this.treeViewType);
            this.Name = "ElementSearch";
            this.Text = "Element Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView treeViewType;
        private ListView listViewElements;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderLongName;
        private ColumnHeader columnHeaderShortName;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderChannel;
        private ColumnHeader columnHeaderDatabase;
        private ColumnHeader columnHeaderLocation;
        private ColumnHeader columnHeaderHandle;
        private TreeView treeViewChannel;
        private TreeView treeViewDatabase;
        private TextBox textBoxType;
        private TextBox textBoxChannel;
        private TextBox textBoxDatabase;
        private TextBox textBoxElementName;
        private Button buttonClear;
        private Button buttonSend;
        private Label labelType;
        private Label labelChannel;
        private Label labelDatabase;
        private CheckBox checkBoxType;
        private CheckBox checkBoxChannel;
        private CheckBox checkBoxDatabase;
        private CheckBox checkBoxElements;
    }
}