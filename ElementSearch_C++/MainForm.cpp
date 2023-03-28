
#include "MainForm.h"

using namespace System;
using namespace System::Windows::Forms;
using namespace System::Collections::Generic;
[STAThreadAttribute]

int main(array<String^>^ args)
{
	Application::SetCompatibleTextRenderingDefault(false);
	Application::EnableVisualStyles();
	ElementSearch::MainForm frm;
	Application::Run(% frm);
}

// This function loads the main form
System::Void ElementSearch::MainForm::MainForm_Load(System::Object^ sender, System::EventArgs^ e)
{
	// Add some sample data to ListView
	//ListViewItem^ row_item = gcnew ListViewItem(gcnew array<String^> { L"Long Name 1", L"Short Name 1",
	//	L"Elem Type 1", L"Channel 1", L"Database 1", L"Location 1", L"ID 1", L"Handle 1" });

	// Define vectors to store vectors of tokens for each data file
	std::vector<std::vector<std::string>> file_tokens_elm, file_tokens_chn, file_tokens_dbs;
	file_tokens_elm.reserve(8);
	file_tokens_chn.reserve(2000);
	file_tokens_dbs.reserve(950);
	std::vector<std::vector<std::vector<std::string>>> file_tokens = { file_tokens_elm, file_tokens_chn, file_tokens_dbs };

	// Define file paths
	std::vector<std::string> files_paths = { "logs\\_lst_LogData_elm.cvs", "logs\\_lst_LogData_chn.cvs", "logs\\_lst_LogData_dbs.cvs" };

	// Read data from text files
	ReadTextFiles(files_paths, file_tokens);
}

// This function reads text files
System::Void ElementSearch::MainForm::ReadTextFiles(const std::vector<std::string>& file_paths, std::vector<std::vector<std::vector<std::string>>>& file_tokens)
{
	ReadTextFile(file_paths[0], file_tokens[0]);
	ReadTextFile(file_paths[1], file_tokens[1]);
	ReadTextFile(file_paths[2], file_tokens[2]);

	FillTreeView(this->treeViewElement, file_tokens[0]);
	FillTreeView(this->treeViewChannel, file_tokens[1]);
	FillTreeView(this->treeViewDatabase, file_tokens[2]);
}

System::Void ElementSearch::MainForm::ReadTextFile(const std::string file_path, std::vector<std::vector<std::string>>& file_tokens)
{
	// Open a file in read mode
	std::ifstream infile;

	// std::istringstream ss(msclr::interop::marshal_as<std::string>(file_path));
	std::istringstream ss(file_path);
	infile.open(ss.str());

	// Check whether the file is open
	if (infile.is_open())
	{
		std::string line, token;

		// Define vector to store tokens for each line
		std::vector<std::string> line_tokens;
		line_tokens.reserve(3);

		// Read text lines from file one line at a time
		while (getline(infile, line))
		{
			std::istringstream ss(line);

			// Tokenize line
			while (std::getline(ss, token, '@'))
			{
				if (token.empty() == false)
				{
					// Store line tokens
					line_tokens.push_back(token);
				}
			}

			// Store file tokens
			file_tokens.push_back(line_tokens);
			line_tokens.clear();
			line_tokens.reserve(3);
		}

		infile.close(); // Close file
	}
}

// This function fills out TreeView control with data from vector of strings (previously extracted from the text files)
System::Void ElementSearch::MainForm::FillTreeView(TreeView^ tree_view, std::vector<std::vector<std::string>>& file_tokens)
{
	// Disable redrawing of TreeView while adding nodes
	tree_view->BeginUpdate();

	// Clear any existing nodes
	tree_view->Nodes->Clear();

	// Traverse file tokens
	for each (std::vector<std::string> line_tokens in file_tokens)
	{
		if (line_tokens[0].empty() == false)
		{
			// Store parent/children description
			std::istringstream ss(line_tokens[0]);

			int id = 0, handle = 0;

			if (line_tokens[1].empty() == false)
			{
				// Store node ID
				id = std::stoi(line_tokens[1]);
			}

			if (line_tokens[2].empty() == false)
			{
				// Store node Handle
				handle = std::stoi(line_tokens[2]);
			}

			List<MyTreeNode^>^ family = gcnew List<MyTreeNode^>();

			// Tokenize hierarchy of family relatives
			std::string relative;
			while (getline(ss, relative, '/'))
			{
				family->Add(gcnew MyTreeNode(gcnew System::String(relative.c_str()), id, handle));
			}

			AddNode(tree_view->Nodes, family, 0);
		}
	}

	// Enable redrawing of TreeView
	tree_view->EndUpdate();
}

void ElementSearch::MainForm::AddNode(TreeNodeCollection^ nodes, List<MyTreeNode^>^ family, int index)
{
	MyTreeNode^ current = family[index];

	// Find existing node with matching text, or create new node
	MyTreeNode^ node = nullptr;

	for each (MyTreeNode^ child in nodes)
	{
		if (child->Text == current->Text)
		{
			node = child;
			break;
		}
	}

	if (node == nullptr)
	{
		node = gcnew MyTreeNode(current->Text, current->m_id, current->m_handle);
		nodes->Add(node);
	}

	// Recursively add child nodes
	if (index < family->Count - 1)
	{
		AddNode(node->Nodes, family, index + 1);
	}
}