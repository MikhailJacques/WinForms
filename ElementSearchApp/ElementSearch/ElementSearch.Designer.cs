namespace ElementSearch
{
    partial class FormElementSearch
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
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

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxElemType = new TextBox();
            treeViewElemType = new TreeView();
            treeViewChannel = new TreeView();
            treeViewDatabase = new TreeView();
            listViewElements = new ListView();
            columnHeaderID = new ColumnHeader();
            columnHeaderLongName = new ColumnHeader();
            columnHeaderShortName = new ColumnHeader();
            columnHeaderElemType = new ColumnHeader();
            columnHeaderChannel = new ColumnHeader();
            columnHeaderDatabase = new ColumnHeader();
            columnHeaderLocation = new ColumnHeader();
            columnHeaderHandle = new ColumnHeader();
            buttonSearch = new Button();
            labelElemType = new Label();
            labelChannel = new Label();
            textBoxChannel = new TextBox();
            labelDatabase = new Label();
            textBoxDatabase = new TextBox();
            buttonClear = new Button();
            buttonSend = new Button();
            SuspendLayout();
            // 
            // textBoxElemType
            // 
            textBoxElemType.Location = new Point(81, 11);
            textBoxElemType.Name = "textBoxElemType";
            textBoxElemType.Size = new Size(231, 23);
            textBoxElemType.TabIndex = 0;
            textBoxElemType.KeyDown += TextBoxElemType_KeyDown;
            // 
            // treeViewElemType
            // 
            treeViewElemType.CheckBoxes = true;
            treeViewElemType.Location = new Point(12, 40);
            treeViewElemType.Name = "treeViewElemType";
            treeViewElemType.Size = new Size(300, 225);
            treeViewElemType.TabIndex = 1;
            treeViewElemType.AfterCheck += treeViewElemType_AfterCheck;
            // 
            // treeViewChannel
            // 
            treeViewChannel.CheckBoxes = true;
            treeViewChannel.Location = new Point(12, 305);
            treeViewChannel.Name = "treeViewChannel";
            treeViewChannel.Size = new Size(300, 225);
            treeViewChannel.TabIndex = 3;
            treeViewChannel.AfterCheck += treeViewChannel_AfterCheck;
            // 
            // treeViewDatabase
            // 
            treeViewDatabase.CheckBoxes = true;
            treeViewDatabase.Location = new Point(12, 569);
            treeViewDatabase.Name = "treeViewDatabase";
            treeViewDatabase.Size = new Size(300, 225);
            treeViewDatabase.TabIndex = 5;
            treeViewDatabase.AfterCheck += treeViewDatabase_AfterCheck;
            // 
            // listViewElements
            // 
            listViewElements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewElements.Columns.AddRange(new ColumnHeader[] { columnHeaderID, columnHeaderLongName, columnHeaderShortName, columnHeaderElemType, columnHeaderChannel, columnHeaderDatabase, columnHeaderLocation, columnHeaderHandle });
            listViewElements.FullRowSelect = true;
            listViewElements.Location = new Point(325, 40);
            listViewElements.Name = "listViewElements";
            listViewElements.Size = new Size(985, 754);
            listViewElements.TabIndex = 6;
            listViewElements.UseCompatibleStateImageBehavior = false;
            listViewElements.View = View.Details;
            // 
            // columnHeaderID
            // 
            columnHeaderID.Text = "ID";
            columnHeaderID.Width = 40;
            // 
            // columnHeaderLongName
            // 
            columnHeaderLongName.Text = "Long Name";
            columnHeaderLongName.Width = 200;
            // 
            // columnHeaderShortName
            // 
            columnHeaderShortName.Text = "Short Name";
            columnHeaderShortName.Width = 100;
            // 
            // columnHeaderElemType
            // 
            columnHeaderElemType.Text = "Elem Type";
            columnHeaderElemType.Width = 100;
            // 
            // columnHeaderChannel
            // 
            columnHeaderChannel.Text = "Channel";
            columnHeaderChannel.Width = 200;
            // 
            // columnHeaderDatabase
            // 
            columnHeaderDatabase.Text = "Database";
            columnHeaderDatabase.Width = 200;
            // 
            // columnHeaderLocation
            // 
            columnHeaderLocation.Text = "Location";
            // 
            // columnHeaderHandle
            // 
            columnHeaderHandle.Text = "Handle";
            columnHeaderHandle.Width = 80;
            // 
            // buttonSearch
            // 
            buttonSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonSearch.Location = new Point(325, 11);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(89, 24);
            buttonSearch.TabIndex = 7;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // labelElemType
            // 
            labelElemType.AutoSize = true;
            labelElemType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelElemType.Location = new Point(12, 14);
            labelElemType.Name = "labelElemType";
            labelElemType.Size = new Size(63, 15);
            labelElemType.TabIndex = 8;
            labelElemType.Text = "Elem Type";
            // 
            // labelChannel
            // 
            labelChannel.AutoSize = true;
            labelChannel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelChannel.Location = new Point(12, 279);
            labelChannel.Name = "labelChannel";
            labelChannel.Size = new Size(51, 15);
            labelChannel.TabIndex = 10;
            labelChannel.Text = "Channel";
            // 
            // textBoxChannel
            // 
            textBoxChannel.Location = new Point(81, 276);
            textBoxChannel.Name = "textBoxChannel";
            textBoxChannel.Size = new Size(231, 23);
            textBoxChannel.TabIndex = 9;
            textBoxChannel.KeyDown += TextBoxChannel_KeyDown;
            // 
            // labelDatabase
            // 
            labelDatabase.AutoSize = true;
            labelDatabase.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelDatabase.Location = new Point(12, 543);
            labelDatabase.Name = "labelDatabase";
            labelDatabase.Size = new Size(58, 15);
            labelDatabase.TabIndex = 12;
            labelDatabase.Text = "Database";
            // 
            // textBoxDatabase
            // 
            textBoxDatabase.Location = new Point(81, 540);
            textBoxDatabase.Name = "textBoxDatabase";
            textBoxDatabase.Size = new Size(231, 23);
            textBoxDatabase.TabIndex = 11;
            textBoxDatabase.KeyDown += TextBoxDatabase_KeyDown;
            // 
            // buttonClear
            // 
            buttonClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonClear.Location = new Point(420, 11);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(89, 24);
            buttonClear.TabIndex = 13;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonSend
            // 
            buttonSend.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonSend.Location = new Point(515, 11);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(89, 24);
            buttonSend.TabIndex = 14;
            buttonSend.Text = "Send";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // FormElementSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1322, 804);
            Controls.Add(buttonSend);
            Controls.Add(buttonClear);
            Controls.Add(labelDatabase);
            Controls.Add(textBoxDatabase);
            Controls.Add(labelChannel);
            Controls.Add(textBoxChannel);
            Controls.Add(labelElemType);
            Controls.Add(buttonSearch);
            Controls.Add(listViewElements);
            Controls.Add(treeViewDatabase);
            Controls.Add(treeViewChannel);
            Controls.Add(treeViewElemType);
            Controls.Add(textBoxElemType);
            Name = "FormElementSearch";
            Text = "Element Search";
            Load += FormElementSearch_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxElemType;
        private TreeView treeViewElemType;
        private TreeView treeViewChannel;
        private TreeView treeViewDatabase;
        private ListView listViewElements;
        private Button buttonSearch;
        private Button buttonClear;
        private Label labelElemType;
        private Label labelChannel;
        private TextBox textBoxChannel;
        private Label labelDatabase;
        private TextBox textBoxDatabase;
        private ColumnHeader columnHeaderLongName;
        private ColumnHeader columnHeaderShortName;
        private ColumnHeader columnHeaderElemType;
        private ColumnHeader columnHeaderChannel;
        private ColumnHeader columnHeaderDatabase;
        private ColumnHeader columnHeaderLocation;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderHandle;

        private List<TreeView> treeViewList = new List<TreeView>(3);
        private Button buttonSend;
    }
}