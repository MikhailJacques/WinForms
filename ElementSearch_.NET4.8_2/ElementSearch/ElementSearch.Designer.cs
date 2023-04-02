using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace ElementSearch
{
    partial class ElementSearch : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.SuspendLayout();
            // 
            // treeViewElementType
            // 
            this.treeViewElementType.CheckBoxes = true;
            this.treeViewElementType.Location = new System.Drawing.Point(14, 35);
            this.treeViewElementType.Name = "treeViewElementType";
            this.treeViewElementType.Size = new System.Drawing.Size(270, 220);
            this.treeViewElementType.TabIndex = 0;
            // 
            // listViewElements
            // 
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
            this.listViewElements.Location = new System.Drawing.Point(290, 35);
            this.listViewElements.Name = "listViewElements";
            this.listViewElements.Size = new System.Drawing.Size(988, 724);
            this.listViewElements.TabIndex = 1;
            this.listViewElements.UseCompatibleStateImageBehavior = false;
            this.listViewElements.View = System.Windows.Forms.View.Details;
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
            this.treeViewChannel.CheckBoxes = true;
            this.treeViewChannel.Location = new System.Drawing.Point(14, 287);
            this.treeViewChannel.Name = "treeViewChannel";
            this.treeViewChannel.Size = new System.Drawing.Size(270, 220);
            this.treeViewChannel.TabIndex = 2;
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.CheckBoxes = true;
            this.treeViewDatabase.Location = new System.Drawing.Point(14, 539);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(270, 220);
            this.treeViewDatabase.TabIndex = 3;
            // 
            // textBoxElementType
            // 
            this.textBoxElementType.Location = new System.Drawing.Point(114, 12);
            this.textBoxElementType.Name = "textBoxElementType";
            this.textBoxElementType.Size = new System.Drawing.Size(170, 20);
            this.textBoxElementType.TabIndex = 4;
            this.textBoxElementType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxElementType_KeyDown);
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(114, 261);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(170, 20);
            this.textBoxChannel.TabIndex = 5;
            this.textBoxChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChannel_KeyDown);
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(114, 513);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(170, 20);
            this.textBoxDatabase.TabIndex = 6;
            this.textBoxDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDatabase_KeyDown);
            // 
            // textBoxElementName
            // 
            this.textBoxElementName.Location = new System.Drawing.Point(290, 12);
            this.textBoxElementName.Name = "textBoxElementName";
            this.textBoxElementName.Size = new System.Drawing.Size(170, 20);
            this.textBoxElementName.TabIndex = 7;
            this.textBoxElementName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxElementName_KeyDown);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonClear.Location = new System.Drawing.Point(466, 12);
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
            this.buttonSend.Location = new System.Drawing.Point(548, 12);
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
            this.label1.Location = new System.Drawing.Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Element Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(19, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Channel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(19, 515);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Database";
            // 
            // ElementSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 767);
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
            this.Text = "Form1";
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
    }
}

