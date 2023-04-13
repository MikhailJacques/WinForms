
//High - level summary of the code:

// This code is for a C# Windows Forms application called ElementSearch.
// The primary purpose of the application is to search, filter, and display elements based on certain criteria, such as element type, channel, and database.
// Additionally, it allows users to search for elements by name and send the selected element IDs to another application through a named pipe.

// On the form load, the application reads data from four different text files containing information about element types, channels, databases, and all elements.
// It loads this data into dictionaries and tree views for further processing.

// Three tree views (treeViewElementType, treeViewChannel, and treeViewDatabase) are used to display the hierarchical structure of element types, channels,
// and databases. Users can check or uncheck nodes in these tree views to filter the elements displayed in the listViewElements.

// The listViewElements displays the filtered elements with their detailed information such as
// ID, long name, short name, element type, channel, database, location, and handle.

// There are four text boxes (textBoxElementType, textBoxChannel, textBoxDatabase, and textBoxElementName)
// that allow users to search for nodes in the tree views or elements in the list view by typing at least three characters and pressing Enter.

// When users search in the text boxes, the application finds matching nodes in the corresponding tree view, checks them, expands their parent nodes,
// and updates the list view with the new filter criteria.
// The buttonClear clears the text boxes, unchecks all nodes in the tree views, collapses them, and clears the list view.

// The buttonSend sends the selected element IDs from the list view to another application through a named pipe with a 3-second timeout.
// If the receiving application is not running, it shows a timeout error message.

// In summary, the ElementSearch application allows users to load, search, filter, and display elements based on different criteria.
// Users can also send selected element IDs to another application through a named pipe.

// Description of each function:

// The following functions work together to provide the functionality of searching, filtering, displaying elements,
// and sending selected element IDs to another application through a named pipe.

// ElementSearch_Load:
// This function is called when the form is loaded.It reads data from text files and initializes dictionaries and tree views with the data.

// ReadDataFromFile:
// A generic function to read data from a text file and return it as a list of strings.

// GetCheckedNodes:
// A recursive function that retrieves checked nodes from a TreeNodeCollection.
// If a node is checked and has no child nodes, it creates a new TreeNode with the full path and stores the original node in the Tag property.
// This function is used to collect checked nodes from treeViewElementType, treeViewChannel, and treeViewDatabase.

// UpdateListView:
// Updates the listViewElements based on the checked nodes in the tree views.
// It collects the checked nodes, filters the elements, and populates the list view with the filtered elements.

// FindAndCheckNodes:
// Searches for nodes in a tree view by their name and checks them if they match the provided string.

// FindNodesByName:
// A recursive function that searches for nodes in a TreeNode hierarchy by their name, checks them if they match the provided string, and expands their parent nodes.

// ClearTreeView:
// Unchecks all nodes in a tree view and collapses them.

// UncheckAndCollapseNodes:
// A recursive function that unchecks a node, collapses it, and repeats the process for all its child nodes.

// textBoxElementType_KeyDown:
// Handles the KeyDown event of the textBoxElementType.
// If the Enter key is pressed, it searches for nodes in treeViewElementType with a matching name and updates the list view.

// textBoxChannel_KeyDown:
// Handles the KeyDown event of the textBoxChannel.
// If the Enter key is pressed, it searches for nodes in treeViewChannel with a matching name and updates the list view.

// textBoxDatabase_KeyDown:
// Handles the KeyDown event of the textBoxDatabase.
// If the Enter key is pressed, it searches for nodes in treeViewDatabase with a matching name and updates the list view.

// textBoxElementName_KeyDown:
// Handles the KeyDown event of the textBoxElementName.
// If the Enter key is pressed, it searches for elements in the list view with matching long or short names,
// deselects any previously selected items, and selects the matching items.

// buttonClear_Click:
// Handles the Click event of the buttonClear.
// It clears the text boxes, unchecks and collapses all nodes in the tree views, and clears the list view.

// buttonSend_Click:
// Handles the Click event of the buttonSend.
// It sends the selected element IDs from the list view to another application through a named pipe.

// SendDataToPipe:
// Sends data to a named pipe. It tries to connect to the pipe and writes the data to it.
// If the receiving application is not running, it shows a timeout error message.


