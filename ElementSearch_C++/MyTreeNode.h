#pragma once

using namespace System;

// Define a custom TreeNode class that derives from System::Windows::Forms::TreeNode
ref class MyTreeNode : public System::Windows::Forms::TreeNode
{
	public:

		// Custom data members
		unsigned int m_id;
		unsigned int m_handle;

		// Default constructor
		MyTreeNode() : TreeNode() 
		{
			this->m_id = 0;
			this->m_handle = 0;
		}

		// Constructor with text ONLY
		MyTreeNode(String^ text) : TreeNode(text)
		{
			// Initialize custom data members to default values
			this->m_id = 0;
			this->m_handle = 0;
		}

		// Constructor with name, ID and handle parameters (name is not used)
        MyTreeNode(String^ text, unsigned int id, unsigned int handle) : TreeNode(text)
        {
			this->m_id = id;
			this->m_handle = handle;
        }

		// Constructor with text and children parameters
		MyTreeNode(String^ text, array<MyTreeNode^>^ children) : TreeNode(text)
		{
			// Initialize custom data members to default values
			this->m_id = 0;
			this->m_handle = 0;

			// Add children to node
			this->Nodes->AddRange(children);
		}

		// Copy constructor
		MyTreeNode(MyTreeNode^ other) : TreeNode(other->Text) 
		{
			// Copy any custom data members
			this->m_id = other->m_id;
			this->m_handle = other->m_handle;
			this->Checked = other->Checked;	
		}
};
