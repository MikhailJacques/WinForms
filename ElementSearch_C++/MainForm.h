#pragma once

#include "MyTreeNode.h"
#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <functional>
//#include <future>
//#include <cliext/vector>
//#include <vcclr.h>
//#include <ppl.h>			// for parallel_for
//#include <msclr/gcroot.h> // for TaskLambdaDelegate
//#include <msclr/marshal_cppstd.h>

namespace ElementSearch {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Collections::Generic;
	//using namespace System::Threading;
	//using namespace System::Threading::Tasks;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MainForm
	/// </summary>
	public ref class MainForm : public System::Windows::Forms::Form
	{
		public:

			MainForm(void)
			{
				InitializeComponent();
				//
				//TODO: Add the constructor code here
				//
			}

		protected:

			/// <summary>
			/// Clean up any resources being used.
			/// </summary>
			~MainForm()
			{
				if (components)
				{
					delete components;
				}
			}

		private: System::Windows::Forms::GroupBox^ groupBoxSearch;

		protected:

		private: System::Windows::Forms::Button^ buttonFind;
		private: System::Windows::Forms::TextBox^ textBoxDatabase;
		private: System::Windows::Forms::TextBox^ textBoxChannel;
		private: System::Windows::Forms::Label^ labelDatabase;
		private: System::Windows::Forms::Label^ labelChannel;
		private: System::Windows::Forms::TextBox^ textBoxElement;
		private: System::Windows::Forms::Label^ labelElement;
		private: System::Windows::Forms::ListView^ listViewData;

		private: System::Windows::Forms::ColumnHeader^ columnHeader1;
		private: System::Windows::Forms::ColumnHeader^ columnHeader2;
		private: System::Windows::Forms::TreeView^ treeViewDatabase;
		private: System::Windows::Forms::TreeView^ treeViewChannel;
		private: System::Windows::Forms::TreeView^ treeViewElement;
		private: System::Windows::Forms::ColumnHeader^ columnHeader3;
		private: System::Windows::Forms::ColumnHeader^ columnHeader4;
		private: System::Windows::Forms::ColumnHeader^ columnHeader5;
		private: System::Windows::Forms::ColumnHeader^ columnHeader6;
		private: System::Windows::Forms::ColumnHeader^ columnHeader7;
		private: System::Windows::Forms::ColumnHeader^ columnHeader8;
		private: System::Windows::Forms::GroupBox^ groupBoxTreeView;
		private: System::Windows::Forms::GroupBox^ groupBoxListView;

		private:

			/// <summary>
			/// Required designer variable.
			/// </summary>
			System::ComponentModel::Container ^components;

	#pragma region Windows Form Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			void InitializeComponent(void)
			{
				this->groupBoxSearch = (gcnew System::Windows::Forms::GroupBox());
				this->buttonFind = (gcnew System::Windows::Forms::Button());
				this->textBoxDatabase = (gcnew System::Windows::Forms::TextBox());
				this->textBoxChannel = (gcnew System::Windows::Forms::TextBox());
				this->labelDatabase = (gcnew System::Windows::Forms::Label());
				this->labelChannel = (gcnew System::Windows::Forms::Label());
				this->textBoxElement = (gcnew System::Windows::Forms::TextBox());
				this->labelElement = (gcnew System::Windows::Forms::Label());
				this->listViewData = (gcnew System::Windows::Forms::ListView());
				this->columnHeader1 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader2 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader3 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader4 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader5 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader6 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader7 = (gcnew System::Windows::Forms::ColumnHeader());
				this->columnHeader8 = (gcnew System::Windows::Forms::ColumnHeader());
				this->treeViewDatabase = (gcnew System::Windows::Forms::TreeView());
				this->treeViewChannel = (gcnew System::Windows::Forms::TreeView());
				this->treeViewElement = (gcnew System::Windows::Forms::TreeView());
				this->groupBoxTreeView = (gcnew System::Windows::Forms::GroupBox());
				this->groupBoxListView = (gcnew System::Windows::Forms::GroupBox());
				this->groupBoxSearch->SuspendLayout();
				this->groupBoxTreeView->SuspendLayout();
				this->groupBoxListView->SuspendLayout();
				this->SuspendLayout();
				// 
				// groupBoxSearch
				// 
				this->groupBoxSearch->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Left)
					| System::Windows::Forms::AnchorStyles::Right));
				this->groupBoxSearch->Controls->Add(this->buttonFind);
				this->groupBoxSearch->Controls->Add(this->textBoxDatabase);
				this->groupBoxSearch->Controls->Add(this->textBoxChannel);
				this->groupBoxSearch->Controls->Add(this->labelDatabase);
				this->groupBoxSearch->Controls->Add(this->labelChannel);
				this->groupBoxSearch->Controls->Add(this->textBoxElement);
				this->groupBoxSearch->Controls->Add(this->labelElement);
				this->groupBoxSearch->Location = System::Drawing::Point(7, 12);
				this->groupBoxSearch->Name = L"groupBoxSearch";
				this->groupBoxSearch->Size = System::Drawing::Size(1271, 55);
				this->groupBoxSearch->TabIndex = 5;
				this->groupBoxSearch->TabStop = false;
				// 
				// buttonFind
				// 
				this->buttonFind->Location = System::Drawing::Point(627, 16);
				this->buttonFind->Name = L"buttonFind";
				this->buttonFind->Size = System::Drawing::Size(75, 23);
				this->buttonFind->TabIndex = 6;
				this->buttonFind->Text = L"Find";
				this->buttonFind->UseVisualStyleBackColor = true;
				// 
				// textBoxDatabase
				// 
				this->textBoxDatabase->Location = System::Drawing::Point(458, 16);
				this->textBoxDatabase->Name = L"textBoxDatabase";
				this->textBoxDatabase->Size = System::Drawing::Size(100, 20);
				this->textBoxDatabase->TabIndex = 2;
				// 
				// textBoxChannel
				// 
				this->textBoxChannel->Location = System::Drawing::Point(262, 16);
				this->textBoxChannel->Name = L"textBoxChannel";
				this->textBoxChannel->Size = System::Drawing::Size(100, 20);
				this->textBoxChannel->TabIndex = 1;
				// 
				// labelDatabase
				// 
				this->labelDatabase->AutoSize = true;
				this->labelDatabase->Location = System::Drawing::Point(399, 19);
				this->labelDatabase->Name = L"labelDatabase";
				this->labelDatabase->Size = System::Drawing::Size(53, 13);
				this->labelDatabase->TabIndex = 5;
				this->labelDatabase->Text = L"Database";
				// 
				// labelChannel
				// 
				this->labelChannel->AutoSize = true;
				this->labelChannel->Location = System::Drawing::Point(210, 19);
				this->labelChannel->Name = L"labelChannel";
				this->labelChannel->Size = System::Drawing::Size(46, 13);
				this->labelChannel->TabIndex = 4;
				this->labelChannel->Text = L"Channel";
				// 
				// textBoxElement
				// 
				this->textBoxElement->Location = System::Drawing::Point(57, 16);
				this->textBoxElement->Name = L"textBoxElement";
				this->textBoxElement->Size = System::Drawing::Size(136, 20);
				this->textBoxElement->TabIndex = 0;
				// 
				// labelElement
				// 
				this->labelElement->AutoSize = true;
				this->labelElement->Location = System::Drawing::Point(6, 19);
				this->labelElement->Name = L"labelElement";
				this->labelElement->Size = System::Drawing::Size(45, 13);
				this->labelElement->TabIndex = 3;
				this->labelElement->Text = L"Element";
				// 
				// listViewData
				// 
				this->listViewData->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
					| System::Windows::Forms::AnchorStyles::Left)
					| System::Windows::Forms::AnchorStyles::Right));
				this->listViewData->Columns->AddRange(gcnew cli::array< System::Windows::Forms::ColumnHeader^  >(8) {
					this->columnHeader1,
						this->columnHeader2, this->columnHeader3, this->columnHeader4, this->columnHeader5, this->columnHeader6, this->columnHeader7,
						this->columnHeader8
				});
				this->listViewData->HideSelection = false;
				this->listViewData->Location = System::Drawing::Point(6, 19);
				this->listViewData->Name = L"listViewData";
				this->listViewData->Size = System::Drawing::Size(929, 767);
				this->listViewData->TabIndex = 9;
				this->listViewData->UseCompatibleStateImageBehavior = false;
				this->listViewData->View = System::Windows::Forms::View::Details;
				// 
				// columnHeader1
				// 
				this->columnHeader1->Text = L"Long Name";
				this->columnHeader1->Width = 120;
				// 
				// columnHeader2
				// 
				this->columnHeader2->Text = L"Short Name";
				this->columnHeader2->Width = 100;
				// 
				// columnHeader3
				// 
				this->columnHeader3->Text = L"Type";
				// 
				// columnHeader4
				// 
				this->columnHeader4->Text = L"Database";
				this->columnHeader4->Width = 120;
				// 
				// columnHeader5
				// 
				this->columnHeader5->Text = L"Channel";
				this->columnHeader5->Width = 180;
				// 
				// columnHeader6
				// 
				this->columnHeader6->Text = L"Location";
				this->columnHeader6->Width = 100;
				// 
				// columnHeader7
				// 
				this->columnHeader7->Text = L"Handle";
				// 
				// columnHeader8
				// 
				this->columnHeader8->Text = L"ID";
				// 
				// treeViewDatabase
				// 
				this->treeViewDatabase->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
					| System::Windows::Forms::AnchorStyles::Left));
				this->treeViewDatabase->CheckBoxes = true;
				this->treeViewDatabase->Location = System::Drawing::Point(6, 551);
				this->treeViewDatabase->Name = L"treeViewDatabase";
				this->treeViewDatabase->Size = System::Drawing::Size(300, 235);
				this->treeViewDatabase->TabIndex = 8;
				// 
				// treeViewChannel
				// 
				this->treeViewChannel->CheckBoxes = true;
				this->treeViewChannel->Location = System::Drawing::Point(6, 285);
				this->treeViewChannel->Name = L"treeViewChannel";
				this->treeViewChannel->Size = System::Drawing::Size(300, 260);
				this->treeViewChannel->TabIndex = 7;
				// 
				// treeViewElement
				// 
				this->treeViewElement->CheckBoxes = true;
				this->treeViewElement->Location = System::Drawing::Point(6, 18);
				this->treeViewElement->Name = L"treeViewElement";
				this->treeViewElement->Size = System::Drawing::Size(300, 261);
				this->treeViewElement->TabIndex = 6;
				// 
				// groupBoxTreeView
				// 
				this->groupBoxTreeView->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
					| System::Windows::Forms::AnchorStyles::Left));
				this->groupBoxTreeView->Controls->Add(this->treeViewElement);
				this->groupBoxTreeView->Controls->Add(this->treeViewChannel);
				this->groupBoxTreeView->Controls->Add(this->treeViewDatabase);
				this->groupBoxTreeView->Location = System::Drawing::Point(10, 73);
				this->groupBoxTreeView->Name = L"groupBoxTreeView";
				this->groupBoxTreeView->Size = System::Drawing::Size(316, 792);
				this->groupBoxTreeView->TabIndex = 10;
				this->groupBoxTreeView->TabStop = false;
				// 
				// groupBoxListView
				// 
				this->groupBoxListView->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
					| System::Windows::Forms::AnchorStyles::Left)
					| System::Windows::Forms::AnchorStyles::Right));
				this->groupBoxListView->Controls->Add(this->listViewData);
				this->groupBoxListView->Location = System::Drawing::Point(337, 73);
				this->groupBoxListView->Name = L"groupBoxListView";
				this->groupBoxListView->Size = System::Drawing::Size(941, 792);
				this->groupBoxListView->TabIndex = 11;
				this->groupBoxListView->TabStop = false;
				// 
				// MainForm
				// 
				this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
				this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
				this->ClientSize = System::Drawing::Size(1284, 871);
				this->Controls->Add(this->groupBoxListView);
				this->Controls->Add(this->groupBoxTreeView);
				this->Controls->Add(this->groupBoxSearch);
				this->Name = L"MainForm";
				this->Text = L"Element Search";
				this->Load += gcnew System::EventHandler(this, &MainForm::MainForm_Load);
				this->groupBoxSearch->ResumeLayout(false);
				this->groupBoxSearch->PerformLayout();
				this->groupBoxTreeView->ResumeLayout(false);
				this->groupBoxListView->ResumeLayout(false);
				this->ResumeLayout(false);

			} // InitializeComponent

#pragma endregion

	private:

		System::Void MainForm_Load(System::Object^ sender, System::EventArgs^ e);	
		System::Void ReadTextFiles(const std::vector<std::string>& file_paths, std::vector<std::vector<std::vector<std::string>>>& file_tokens);
		System::Void ReadTextFile(const std::string file_path, std::vector<std::vector<std::string>>& file_tokens);
		System::Void FillTreeView(TreeView^ tree_view, std::vector<std::vector<std::string>>& file_tokens);
		System::Void AddNode(TreeNodeCollection^ nodes, List<MyTreeNode^>^ family, int index);

	}; // public ref class MainForm
}

