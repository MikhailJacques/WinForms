
//// Create first sub node
//TreeNode^ TreeNode2 = gcnew TreeNode("Tree Node 2");
//TreeNode2->Nodes->Add("Sub 2");

//// Create second sub node
//TreeNode^ TreeNode3 = gcnew TreeNode("Tree Node 3");
//TreeNode3->Nodes->Add("Sub 3A");
//TreeNode3->Nodes->Add("Sub 3B");

//// Combine them into a master node
//array<TreeNode^>^ TreeNodeArray1 = { TreeNode2, TreeNode3 };
//TreeNode^ TreeNode4 = gcnew TreeNode("Tree Node Array 1", TreeNodeArray1);

//// Add this to the TreeView
//treeViewElement->Nodes->Add(TreeNode4);

//TreeNode^ SelectedTreeNode3 = treeViewElement->SelectedNode;

//MyTreeNode^ treeNode1 = (gcnew MyTreeNode(L"Node1Text"));
//treeNode1->Name = L"Node1";
//treeNode1->Checked = true;

//MyTreeNode^ treeNode3 = (gcnew MyTreeNode(L"Node3Text"));
//treeNode3->Name = L"Node3";
//treeNode3->Checked = true;

//MyTreeNode^ treeNode4 = (gcnew MyTreeNode(L"Node4Text"));
//treeNode4->Name = L"Node4";
//treeNode4->Checked = false;

//MyTreeNode^ treeNode5 = (gcnew MyTreeNode(L"Node5Text"));
//treeNode5->Name = L"Node5";
////treeNode5->Text = L"Node5Text";
//treeNode5->Checked = true;

//MyTreeNode^ treeNode2 = (gcnew MyTreeNode(L"Node2Text", gcnew cli::array<MyTreeNode^>(2) { treeNode3, treeNode4 }));
//treeNode2->Name = L"Node2";
////treeNode2->Text = L"Node2Text";
//treeNode2->Checked = false;

//this->treeViewElement->Nodes->AddRange(gcnew cli::array<MyTreeNode^>(3) { treeNode1, treeNode2, treeNode5 });

//this->treeViewElement->Nodes->Add(L"Node6", L"Node6Text");

//ListViewItem^ row_item2 = gcnew ListViewItem(gcnew array<String^> { L"Long Name 2", L"Short Name 2",
//	L"Type 2", L"Database 2", L"Channel 2", L"Location 2", L"Handle 2", L"ID 2" });
//this->listViewData->Items->AddRange(gcnew array<ListViewItem^> { row_item, row_item2 });

//ListViewItem^ row_item3 = static_cast<ListViewItem^>(row_item2->Clone());
//this->listViewData->Items->Add(row_item3);

//ListViewItem^ item = listViewData->Items->Add(L"Long Name 3");
//item->SubItems->Add(L"Short Name 2");
//item->SubItems->Add(L"Type 3");
//item->SubItems->Add(L"Database 3");
//item->SubItems->Add(L"Channel 3");
//item->SubItems->Add(L"Location 3");
//item->SubItems->Add(L"Handle 3");
//item->SubItems->Add(L"ID 3");

//// Count columns 
//int num_of_columns = this->listViewData->Columns->Count;
//this->textBoxElement->Text = num_of_columns.ToString();
//
//// Count rows
//int num_of_rows = this->listViewData->Items->Count;
//this->textBoxChannel->Text = num_of_rows.ToString();


//// Parse the first token into parent and its children
//char* parsed_token_ptr = const_cast<char*>(strstr(token.c_str(), "/"));
//
//if (parsed_token_ptr)
//{
//	memmove(&parsed_token_ptr[0], &parsed_token_ptr[1], strlen(parsed_token_ptr));
//	MyTreeNode^ tree_node = (gcnew MyTreeNode(gcnew String(parsed_token_ptr)));
//}
//else
//{
//	// Parent
//}

//while (infile)
//{
//	std::getline(infile, line);
//	std::stringstream ss(line);

//	while (std::getline(ss, token, ','))
//	{
//		token2 = gcnew String(token.c_str());
//		MyTreeNode^ treeNode = (gcnew MyTreeNode(token2));
//		treeNode->Name = token2;
//		this->treeViewElement->Nodes->Add(treeNode);
//	}
//}

//
//#include <vector>
//#include <string>
//#include <msclr/marshal_cppstd.h>
//
//using namespace System::Windows::Forms;
//using namespace System::Collections::Generic;
//
//// Recursive function to add nodes to the TreeView control
//void AddNode(TreeNodeCollection^ nodes, List<String^>^ hierarchy, int index) {
//    String^ current = hierarchy[index];
//
//    // Find existing node with matching text, or create new node
//    TreeNode^ node = nullptr;
//    for each (TreeNode ^ child in nodes) {
//        if (child->Text == current) {
//            node = child;
//            break;
//        }
//    }
//
//    if (node == nullptr) {
//        node = gcnew TreeNode(current);
//        nodes->Add(node);
//    }
//
//    // Recursively add child nodes
//    if (index < hierarchy->Count - 1) {
//        AddNode(node->Nodes, hierarchy, index + 1);
//    }
//}
//
//// Function to fill out TreeView control from vector of strings
//void FillTreeView(TreeView^ treeView, std::vector<std::string> families) {
//    treeView->BeginUpdate(); // Disable redrawing of TreeView while adding nodes
//    treeView->Nodes->Clear(); // Clear any existing nodes
//
//    for each (std::string family in families) {
//        List<String^>^ hierarchy = gcnew List<String^>();
//        std::istringstream ss(family);
//        std::string token;
//
//        while (getline(ss, token, '/')) { // Tokenize hierarchy
//            hierarchy->Add(msclr::interop::marshal_as<String^>(token));
//        }
//
//        AddNode(treeView->Nodes, hierarchy, 0); // Add nodes to TreeView
//    }
//
//    treeView->EndUpdate(); // Enable redrawing of TreeView
//}

//// Create first sub node
//TreeNode^ TreeNode2 = gcnew TreeNode("Tree Node 2");
//TreeNode2->Nodes->Add("Sub 2");

//// Create second sub node
//TreeNode^ TreeNode3 = gcnew TreeNode("Tree Node 3");
//TreeNode3->Nodes->Add("Sub 3A");
//TreeNode3->Nodes->Add("Sub 3B");

//// Combine them into a master node
//array<TreeNode^>^ TreeNodeArray1 = { TreeNode2, TreeNode3 };
//TreeNode^ TreeNode4 = gcnew TreeNode("Tree Node Array 1", TreeNodeArray1);

//// Add this to the TreeView
//treeViewElement->Nodes->Add(TreeNode4);

//TreeNode^ SelectedTreeNode3 = treeViewElement->SelectedNode;


//// Find out which item is selected?
//for (int ii = 0; ii < listViewData->Items->Count; ii++)
//{
//	if (listViewData->Items[ii]->Selected)
//		break;
//}

//std::vector<std::vector<std::vector<std::string>>> file_tokens;
//file_tokens.push_back(file_tokens_elm);
//file_tokens.push_back(file_tokens_chn);
//file_tokens.push_back(file_tokens_dbs);

//std::string element_type_file_path("logs\\_lst_LogData_elm_type.cvs"), 
//	channel_file_path("logs\\_lst_LogData_chn.cvs"), 
//	database_file_path("logs\\_lst_LogData_dbs.cvs");
//std::vector<std::string> files_paths;
//files_paths.push_back(element_type_file_path);
//files_paths.push_back(channel_file_path);
//files_paths.push_back(database_file_path);


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		// Task^ task = Task::Run(gcnew Action(std::bind(&ReadTextFile, file_tokens[ii], file_path[ii])));
//
//		//Task^ task = Task::Run(gcnew Action([file_tokens[ii], file_path[ii]]() {
//		//	ReadTextFile(file_tokens[ii], file_path[ii]);
//		//	}));
//
//	}
//
//	ReadTextFile(file_tokens[0], file_path[0]);
//	ReadTextFile(file_tokens[1], file_path[1]);
//	ReadTextFile(file_tokens[2], file_path[2]);
//}
//
//
//
//System::Void ElementSearch::MainForm::ReadTextFile(std::vector<std::vector<std::string>>& file_tokens, const std::string file_path)
//{
//	// Open a file in read mode
//	std::ifstream infile;
//
//	// std::istringstream ss(msclr::interop::marshal_as<std::string>(file_path));
//	std::istringstream ss(file_path);
//	infile.open(ss.str());
//
//	// Check whether the file is open
//	if (infile.is_open())
//	{
//		std::string line, token;
//
//		// Define vector to store tokens for each line
//		std::vector<std::string> line_tokens;
//		line_tokens.reserve(3);
//
//		// Read text lines from file one line at a time
//		while (getline(infile, line))
//		{
//			std::istringstream ss(line);
//
//			// Tokenize line
//			while (std::getline(ss, token, '@'))
//			{
//				if (token.empty() == false)
//				{
//					// Store line tokens
//					line_tokens.push_back(token);
//				}
//			}
//
//			// Store file tokens
//			file_tokens.push_back(line_tokens);
//			line_tokens.clear();
//			line_tokens.reserve(3);
//		}
//
//		infile.close(); // Close file
//	}
//}


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Define delegate to ReadTextFile member function
//	Action<std::vector<std::vector<std::string>>&, const std::string>^ readTextFileDelegate = gcnew Action<std::vector<std::vector<std::string>>&, const std::string>(this, &ElementSearch::MainForm::ReadTextFile);
//
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		Task^ task = Task::Run(gcnew Action(readTextFileDelegate, std::ref(file_tokens[ii]), file_path[ii]));
//	}
//}

//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	std::vector<std::future<void>> futures;
//
//	// Process each family in parallel using std::async
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		futures.emplace_back(std::async(std::launch::async, &ElementSearch::MainForm::ReadTextFile, this, std::ref(file_tokens[ii]), file_path[ii]));
//	}
//
//	// Wait for all tasks to complete
//	for (auto& future : futures)
//	{
//		future.wait();
//	}
//}

//// Define lambda function to read text file
//auto readTextFile = [](std::vector<std::vector<std::string>>& file_tokens, const std::string& file_path) 
//{
//	// Open a file in read mode
//	std::ifstream infile;
//
//	// std::istringstream ss(msclr::interop::marshal_as<std::string>(file_path));
//	std::istringstream ss(file_path);
//	infile.open(ss.str());
//
//	// Check whether the file is open
//	if (infile.is_open())
//	{
//		std::string line, token;
//
//		// Define vector to store tokens for each line
//		std::vector<std::string> line_tokens;
//		line_tokens.reserve(3);
//
//		// Read text lines from file one line at a time
//		while (getline(infile, line))
//		{
//			std::istringstream ss(line);
//
//			// Tokenize line
//			while (std::getline(ss, token, '@'))
//			{
//				if (token.empty() == false)
//				{
//					// Store line tokens
//					line_tokens.push_back(token);
//				}
//			}
//
//			// Store file tokens
//			file_tokens.push_back(line_tokens);
//			line_tokens.clear();
//			line_tokens.reserve(3);
//		}
//
//		infile.close(); // Close file
//	}
//};
//
//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Process each family in parallel using Parallel STL
//	concurrency::parallel_for(size_t(0), std::min(file_tokens.size(), file_path.size()), [&](size_t ii) {
//		readTextFile(file_tokens[ii], file_path[ii]);
//		});
//}


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	concurrency::parallel_for(size_t(0), std::min(file_tokens.size(), file_path.size()), [&](size_t ii) {
//		ReadTextFile(file_tokens[ii], file_path[ii]);
//		});
//}

//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	concurrency::parallel_for(size_t(0), std::min(file_tokens.size(), file_path.size()), [this, &file_tokens, &file_path](size_t ii) {
//		ReadTextFile(file_tokens[ii], file_path[ii]);
//		});
//}

//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		// Define lambda function to read text file
//		auto readTextFile = [this](std::vector<std::vector<std::string>>& file_tokens, const std::string& file_path) {
//			ReadTextFile(file_tokens, file_path);
//		};
//
//		try
//		{
//			// Define task to read text file and store tokens
//			Task^ task = Task::Run(gcnew Action(gcnew TaskLambdaDelegate(&readTextFile), std::ref(file_tokens[ii]), file_path[ii]));
//
//			// Wait for task to complete
//			task->Wait();
//		}
//		catch (Exception^ ex)
//		{
//			MessageBox::Show(ex->ToString());
//		}
//	}
//}

//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		// Define lambda function to read text file
//		auto readTextFile = [this](std::vector<std::vector<std::string>>& file_tokens, const std::string& file_path) {
//			ReadTextFile(file_tokens, file_path);
//		};
//
//		// Define task to read text file and store tokens
//		//Task^ task = Task::Run(gcnew Action(gcnew TaskLambdaDelegate(&readTextFile), std::ref(file_tokens[ii]), file_path[ii]));
//		Task^ task = Task::Run(gcnew Action(gcnew TaskLambdaDelegate(readTextFile), std::ref(file_tokens[ii]), file_path[ii]));
//
//		// Wait for task to complete
//		task->Wait();
//	}
//}


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Define lambda function to read text file
//	auto readTextFile = [this](std::vector<std::vector<std::string>>& file_tokens, const std::string& file_path) {
//		ReadTextFile(file_tokens, file_path);
//	};
//
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		// Define task to read text file and store tokens
//		Task^ task = Task::Run(gcnew Action<std::vector<std::vector<std::string>>&, const std::string&>(readTextFile), gcnew System::Collections::Generic::List<Object^> { std::ref(file_tokens[ii]), gcnew String(file_path[ii].c_str()) });
//
//		// Wait for task to complete
//		task->Wait();
//	}
//}


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Define lambda function to read text file
//	auto readTextFile = [this](std::vector<std::vector<std::string>>& file_tokens, const std::string& file_path) {
//		ReadTextFile(file_tokens, file_path);
//	};
//
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		// Define task to read text file and store tokens
//		Task^ task = Task::Run(gcnew Action<std::vector<std::vector<std::string>>&, const std::string&>(readTextFile), std::ref(file_tokens[ii]), file_path[ii]);
//
//		// Wait for task to complete
//		task->Wait();
//	}
//}


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Define lambda function to read text file
//	auto readTextFile = [this](std::vector<std::vector<std::string>>& file_tokens, const std::string& file_path) {
//		ReadTextFile(file_tokens, file_path);
//	};
//
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		// Define task to read text file and store tokens
//		Task^ task = Task::Run(gcnew Action(gcnew TaskLambdaDelegate(&readTextFile), std::ref(file_tokens[ii]), file_path[ii]));
//
//		// Wait for task to complete
//		task->Wait();
//	}
//}


//
//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		std::vector<std::vector<std::string>>& tokens = file_tokens[ii];
//		const std::string& path = file_path[ii];
//
//		Task^ task = Task::Run(gcnew Action([tokens, path]() {
//			ReadTextFile(tokens, path);
//			}));
//	}
//}


//System::Void ElementSearch::MainForm::ReadTextFiles(std::vector<std::vector<std::vector<std::string>>>& file_tokens, const std::vector<std::string>& file_path)
//{
//	// Define lambda as static member function
//	static auto taskLambda = [](std::vector<std::vector<std::string>>& tokens, const std::string& path) {
//		ReadTextFile(tokens, path);
//	};
//
//	// Process each family in parallel using Tasks
//	for (size_t ii = 0; (ii < file_tokens.size()) && (ii < file_path.size()); ii++)
//	{
//		std::vector<std::vector<std::string>>& tokens = file_tokens[ii];
//		const std::string& path = file_path[ii];
//
//		Task^ task = Task::Run(gcnew Action(gcnew TaskLambdaDelegate(&taskLambda), tokens, path));
//	}
//}

//// This function fills out TreeView controls with data (previously extracted from the text files)
//System::Void ElementSearch::MainForm::FillTreeViews(TreeView^ tree_view, std::vector<std::vector<std::string>> file_tokens)
//{
//	std::vector<std::string> families;
//
//	// Traverse file tokens
//	for (size_t ii = 0; ii < file_tokens.size(); ii++)
//	{
//		std::vector<std::string> line_tokens = file_tokens[ii];
//
//		// Traverse line tokens
//		for (size_t jj = 0; jj < line_tokens.size(); jj++)
//		{
//			// Save individual token, which can either be the first token that stores parent/children description (family) or
//			// any subsequent token that stores either ID or Handle numerical value in string representation
//			std::string token = line_tokens[jj];
//
//			// Check to see whether it is the first token in line that stores family description
//			if (jj == 0)
//			{
//				families.push_back(token);
//			}
//		}
//	}
//
//	FillTreeView(tree_view, families);
//}

//// This function fills out TreeView control with data from vector of strings (previously extracted from the text files)
//System::Void ElementSearch::MainForm::FillTreeView(TreeView^ tree_view, std::vector<std::vector<std::string>>& file_tokens)
//{
//	// Disable redrawing of TreeView while adding nodes
//	tree_view->BeginUpdate();
//
//	// Clear any existing nodes
//	tree_view->Nodes->Clear();
//
//	// Traverse file tokens
//	for each (std::vector<std::string> line_tokens in file_tokens)
//	{
//		if (line_tokens[0].empty() == false)
//		{
//			List<String^>^ hierarchy = gcnew List<String^>();
//			// std::istringstream ss(msclr::interop::marshal_as<std::string>(family));
//			std::istringstream ss(line_tokens[0]);	// line_tokens[0] - stores parent / children description (family relatives)
//			std::string relative;
//
//			// Tokenize hierarchy
//			while (getline(ss, relative, '/'))
//			{
//				hierarchy->Add(gcnew System::String(relative.c_str()));
//				//hierarchy->Add(msclr::interop::marshal_as<String^>(token));
//			}
//
//			// MJ TODO:
//			int id = stoi(line_tokens[1]);			// Stores node ID 
//			int handle = stoi(line_tokens[2]);		// Stores node Handle
//			MyTreeNode^ tree_node = gcnew MyTreeNode(gcnew System::String(line_tokens[0].c_str()), id, handle);
//
//			// Add nodes to TreeView
//			AddNode(tree_view->Nodes, hierarchy, 0);
//		}
//	}
//
//	// Enable redrawing of TreeView
//	tree_view->EndUpdate();
//}


// This function recursively adds nodes to the TreeView control
//System::Void ElementSearch::MainForm::AddNode(TreeNodeCollection^ nodes, List<String^>^ hierarchy, int index)
//{
//	String^ current = hierarchy[index];
//
//	// Find existing node with matching text, or create new node
//	TreeNode^ node = nullptr;
//	for each (TreeNode ^ child in nodes)
//	{
//		if (child->Text == current)
//		{
//			node = child;
//			break;
//		}
//	}
//
//	if (node == nullptr)
//	{
//		node = gcnew TreeNode(current);
//		nodes->Add(node);
//	}
//
//	// Recursively add child nodes
//	if (index < hierarchy->Count - 1)
//	{
//		AddNode(node->Nodes, hierarchy, index + 1);
//	}
//}

//// Store node Handle
//std::stringstream ss;
//ss << line_tokens[2];
//ss >> handle;

// This function recursively adds nodes to the TreeView control
//System::Void ElementSearch::MainForm::AddNode(TreeNodeCollection^ nodes, List<MyTreeNode^>^ family, int index)
//{
//	MyTreeNode^ current = family[index];
//
//	// Find existing node with matching text, or create new node
//	MyTreeNode^ node = nullptr;
//	for each (MyTreeNode^ child in nodes)
//	{
//		if (child->Text == current->Text)
//		{
//			node = child;
//			break;
//		}
//	}
//
//	if (node == nullptr)
//	{
//		node = gcnew MyTreeNode(current);
//		nodes->Add(node);
//	}
//
//	// Recursively add child nodes
//	if (index < family->Count - 1)
//	{
//		AddNode(node->Nodes, family, index + 1);
//	}
//}

	//public ref class ReaderClass
	//{
	//	public: static void ReadTextFile(std::vector<std::vector<std::string>>& file_token, const std::string file_path)
	//	{
	//		// Open a file in read mode
	//		std::ifstream infile;

	//		// std::istringstream ss(msclr::interop::marshal_as<std::string>(file_path));
	//		std::istringstream ss(file_path);
	//		infile.open(ss.str());

	//		// Check whether the file is open
	//		if (infile.is_open())
	//		{
	//			std::string line, token;

	//			// Define vector to store tokens for each line
	//			std::vector<std::string> line_token;
	//			line_token.reserve(3);

	//			// Read text lines from file one line at a time
	//			while (getline(infile, line))
	//			{
	//				std::istringstream ss(line);

	//				// Tokenize line
	//				while (std::getline(ss, token, '@'))
	//				{
	//					if (token.empty() == false)
	//					{
	//						// Store line tokens
	//						line_token.push_back(token);
	//					}
	//				}

	//				// Store file tokens
	//				file_token.push_back(line_token);
	//				line_token.clear();
	//				line_token.reserve(3);
	//			}

	//			infile.close(); // Close file
	//		}
	//	}
	//};

	//public ref class ThreadParams
	//{
	//	public:

	//		std::vector<std::vector<std::string>>& file_tokens;
	//		String^ file_path;

	//		ThreadParams(std::vector<std::vector<std::string>>& file_tokens, const std::string file_path)
	//		{
	//			this->file_tokens = file_tokens;
	//			this->file_path = file_path;
	//		}
	//};


//// This function reads text files
//System::Void ElementSearch::MainForm::ReadTextFiles(const std::vector<std::string>& file_paths, std::vector<std::vector<std::vector<std::string>>>& file_tokens)
//{
//	//for (size_t ii = 0; (ii < file_paths.size()) && (ii < file_tokens.size()); ii++)
//	//{
//	//	ReadTextFile(file_paths[ii], file_tokens[ii]);
//	//}
//
//	//// Process each family in parallel using Tasks
//	//for (size_t ii = 0; (ii < file_token.size()) && (ii < file_path.size()); ii++)
//	//{
//	//	// Task^ task = Task::Run(gcnew Action(std::bind(&ReadTextFile, file_token[ii], file_path[ii])));
//	//	Task^ task = Task::Run(gcnew Action(std::bind(&ReaderClass::ReadTextFile, file_token[ii], file_path[ii])));
//	//}
//
//	//ThreadStart^ threadDelegate = gcnew ThreadStart(&ReaderClass::ReadTextFile, file_token[0], file_path[0]);
//	//Thread^ newThread = gcnew Thread(threadDelegate);
//	//newThread->Start();
//}
