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
            columnHeaderLongName = new ColumnHeader();
            columnHeaderShortName = new ColumnHeader();
            columnHeaderElemType = new ColumnHeader();
            columnHeaderChannel = new ColumnHeader();
            columnHeaderDatabase = new ColumnHeader();
            columnHeaderLocation = new ColumnHeader();
            columnHeaderID = new ColumnHeader();
            columnHeaderHandle = new ColumnHeader();
            buttonSearch = new Button();
            labelElemType = new Label();
            labelChannel = new Label();
            textBoxChannel = new TextBox();
            labelDatabase = new Label();
            textBoxDatabase = new TextBox();
            SuspendLayout();
            // 
            // textBoxElemType
            // 
            textBoxElemType.Location = new Point(81, 11);
            textBoxElemType.Name = "textBoxElemType";
            textBoxElemType.Size = new Size(231, 23);
            textBoxElemType.TabIndex = 0;
            // 
            // treeViewElemType
            // 
            treeViewElemType.Location = new Point(12, 40);
            treeViewElemType.Name = "treeViewElemType";
            treeViewElemType.Size = new Size(300, 225);
            treeViewElemType.TabIndex = 1;
            // 
            // treeViewChannel
            // 
            treeViewChannel.Location = new Point(12, 305);
            treeViewChannel.Name = "treeViewChannel";
            treeViewChannel.Size = new Size(300, 225);
            treeViewChannel.TabIndex = 3;
            // 
            // treeViewDatabase
            // 
            treeViewDatabase.Location = new Point(12, 569);
            treeViewDatabase.Name = "treeViewDatabase";
            treeViewDatabase.Size = new Size(300, 225);
            treeViewDatabase.TabIndex = 5;
            // 
            // listViewElements
            // 
            listViewElements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewElements.Columns.AddRange(new ColumnHeader[] { columnHeaderLongName, columnHeaderShortName, columnHeaderElemType, columnHeaderChannel, columnHeaderDatabase, columnHeaderLocation, columnHeaderID, columnHeaderHandle });
            listViewElements.Location = new Point(325, 40);
            listViewElements.Name = "listViewElements";
            listViewElements.Size = new Size(910, 754);
            listViewElements.TabIndex = 6;
            listViewElements.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeaderLongName
            // 
            columnHeaderLongName.Text = "Long Name";
            // 
            // columnHeaderShortName
            // 
            columnHeaderShortName.Text = "Short Name";
            // 
            // columnHeaderElemType
            // 
            columnHeaderElemType.Text = "Elem Type";
            // 
            // columnHeaderChannel
            // 
            columnHeaderChannel.Text = "Channel";
            // 
            // columnHeaderDatabase
            // 
            columnHeaderDatabase.Text = "Database";
            // 
            // columnHeaderLocation
            // 
            columnHeaderLocation.Text = "Location";
            // 
            // columnHeaderID
            // 
            columnHeaderID.Text = "ID";
            // 
            // columnHeaderHandle
            // 
            columnHeaderHandle.Text = "Handle";
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
            // 
            // FormElementSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 803);
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
            Text = "Form1";
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
    }
}