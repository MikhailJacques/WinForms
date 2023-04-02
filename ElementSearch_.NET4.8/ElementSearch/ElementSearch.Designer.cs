
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using ElementSearch;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Text;

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
            this.textBoxElementType = new System.Windows.Forms.TextBox();
            this.treeViewElementType = new System.Windows.Forms.TreeView();
            this.treeViewChannel = new System.Windows.Forms.TreeView();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.listViewElements = new System.Windows.Forms.ListView();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLongName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderShortName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderElementType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderChannel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHandle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelElementType = new System.Windows.Forms.Label();
            this.labelChannel = new System.Windows.Forms.Label();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxElementName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxElementType
            // 
            this.textBoxElementType.Location = new System.Drawing.Point(98, 12);
            this.textBoxElementType.Name = "textBoxElementType";
            this.textBoxElementType.Size = new System.Drawing.Size(170, 20);
            this.textBoxElementType.TabIndex = 0;
            this.textBoxElementType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxElementType_KeyDown);
            // 
            // treeViewElementType
            // 
            this.treeViewElementType.CheckBoxes = true;
            this.treeViewElementType.Location = new System.Drawing.Point(10, 35);
            this.treeViewElementType.Name = "treeViewElementType";
            this.treeViewElementType.Size = new System.Drawing.Size(258, 196);
            this.treeViewElementType.TabIndex = 1;
            this.treeViewElementType.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewElementType_AfterCheck);
            // 
            // treeViewChannel
            // 
            this.treeViewChannel.CheckBoxes = true;
            this.treeViewChannel.Location = new System.Drawing.Point(10, 264);
            this.treeViewChannel.Name = "treeViewChannel";
            this.treeViewChannel.Size = new System.Drawing.Size(258, 196);
            this.treeViewChannel.TabIndex = 3;
            this.treeViewChannel.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewChannel_AfterCheck);
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.CheckBoxes = true;
            this.treeViewDatabase.Location = new System.Drawing.Point(10, 493);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(258, 196);
            this.treeViewDatabase.TabIndex = 5;
            this.treeViewDatabase.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDatabase_AfterCheck);
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
            this.listViewElements.Location = new System.Drawing.Point(279, 35);
            this.listViewElements.Name = "listViewElements";
            this.listViewElements.Size = new System.Drawing.Size(989, 654);
            this.listViewElements.TabIndex = 6;
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
            // labelElementType
            // 
            this.labelElementType.AutoSize = true;
            this.labelElementType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelElementType.Location = new System.Drawing.Point(10, 12);
            this.labelElementType.Name = "labelElementType";
            this.labelElementType.Size = new System.Drawing.Size(82, 15);
            this.labelElementType.TabIndex = 8;
            this.labelElementType.Text = "Element Type";
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelChannel.Location = new System.Drawing.Point(10, 242);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(51, 15);
            this.labelChannel.TabIndex = 10;
            this.labelChannel.Text = "Channel";
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(98, 240);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(170, 20);
            this.textBoxChannel.TabIndex = 9;
            this.textBoxChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChannel_KeyDown);
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelDatabase.Location = new System.Drawing.Point(10, 471);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(58, 15);
            this.labelDatabase.TabIndex = 12;
            this.labelDatabase.Text = "Database";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(98, 469);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(170, 20);
            this.textBoxDatabase.TabIndex = 11;
            this.textBoxDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDatabase_KeyDown);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonClear.Location = new System.Drawing.Point(455, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(76, 21);
            this.buttonClear.TabIndex = 13;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSend.Location = new System.Drawing.Point(537, 12);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(76, 21);
            this.buttonSend.TabIndex = 14;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxElementName
            // 
            this.textBoxElementName.Location = new System.Drawing.Point(279, 12);
            this.textBoxElementName.Name = "textBoxElementName";
            this.textBoxElementName.Size = new System.Drawing.Size(170, 20);
            this.textBoxElementName.TabIndex = 15;
            this.textBoxElementName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxElementName_KeyDown);
            // 
            // FormElementSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 697);
            this.Controls.Add(this.textBoxElementName);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.textBoxDatabase);
            this.Controls.Add(this.labelChannel);
            this.Controls.Add(this.textBoxChannel);
            this.Controls.Add(this.labelElementType);
            this.Controls.Add(this.listViewElements);
            this.Controls.Add(this.treeViewDatabase);
            this.Controls.Add(this.treeViewChannel);
            this.Controls.Add(this.treeViewElementType);
            this.Controls.Add(this.textBoxElementType);
            this.Name = "FormElementSearch";
            this.Text = "Element Search";
            this.Load += new System.EventHandler(this.FormElementSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label labelElementType;
        private Label labelChannel;
        private Label labelDatabase;

        private TreeView treeViewElementType;
        private TreeView treeViewChannel;
        private TreeView treeViewDatabase;
        private ListView listViewElements;
              
        private TextBox textBoxElementType;
        private TextBox textBoxChannel;
        private TextBox textBoxDatabase;
        private TextBox textBoxElementName;

        private List<TreeView> treeViewList = new List<TreeView>(3);

        private ColumnHeader columnHeaderLongName;
        private ColumnHeader columnHeaderShortName;
        private ColumnHeader columnHeaderElementType;
        private ColumnHeader columnHeaderChannel;
        private ColumnHeader columnHeaderDatabase;
        private ColumnHeader columnHeaderLocation;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderHandle;

        private Button buttonClear;
        private Button buttonSend;     
    }
}