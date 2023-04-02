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
            this.SuspendLayout();
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.CheckBoxes = true;
            this.treeViewDatabase.Location = new System.Drawing.Point(12, 12);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.Size = new System.Drawing.Size(219, 671);
            this.treeViewDatabase.TabIndex = 0;
            //this.treeViewDatabase.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDatabase_AfterCheck);
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
            this.listViewElements.Location = new System.Drawing.Point(237, 12);
            this.listViewElements.Name = "listViewElements";
            this.listViewElements.Size = new System.Drawing.Size(975, 671);
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
            // ElementSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 685);
            this.Controls.Add(this.listViewElements);
            this.Controls.Add(this.treeViewDatabase);
            this.Name = "ElementSearch";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TreeView treeViewDatabase;
        private ListView listViewElements;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderLongName;
        private ColumnHeader columnHeaderShortName;
        private ColumnHeader columnHeaderElementType;
        private ColumnHeader columnHeaderChannel;
        private ColumnHeader columnHeaderDatabase;
        private ColumnHeader columnHeaderLocation;
        private ColumnHeader columnHeaderHandle;
    }
}

