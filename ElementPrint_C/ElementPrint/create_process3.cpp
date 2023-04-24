//#include <stdio.h>
//#include <stdlib.h>
//#include <windows.h>
//#include <tchar.h>
//#include <wchar.h>
//
//void _tmain(int argc, TCHAR* argv[])
//{
//    STARTUPINFO si;
//    PROCESS_INFORMATION pi;
//
//    ZeroMemory(&si, sizeof(si));
//    si.cb = sizeof(si);
//    ZeroMemory(&pi, sizeof(pi));
//
//    const wchar_t* csharp_exe_path = L"ElementSearch.exe";
//
//    size_t path_len = wcslen(csharp_exe_path);
//    wchar_t* mutable_path = (wchar_t*)malloc((path_len + 1) * sizeof(wchar_t));
//
//    if (!mutable_path)
//    {
//        printf("Failed to allocate memory for the path buffer.\n");
//        return;
//    }
//
//    // Copy the contents of csharp_exe_path into mutable_path
//    wcscpy_s(mutable_path, path_len + 1, csharp_exe_path);
//
//    // Start the child process. 
//    if (!CreateProcess(NULL,   // No module name (use command line)
//        mutable_path,        // 
//        NULL,           // Process handle not inheritable
//        NULL,           // Thread handle not inheritable
//        FALSE,          // Set handle inheritance to FALSE
//        0,              // No creation flags
//        NULL,           // Use parent's environment block
//        NULL,           // Use parent's starting directory 
//        &si,            // Pointer to STARTUPINFO structure
//        &pi)           // Pointer to PROCESS_INFORMATION structure
//        )
//    {
//        printf("CreateProcess failed (%d).\n", GetLastError());
//        return;
//    }
//
//    printf("C# WinForms application started. Process ID: %lu\n", pi.dwProcessId);
//
//    // Wait until child process exits.
//    WaitForSingleObject(pi.hProcess, INFINITE);
//
//    DWORD exit_code;
//    GetExitCodeProcess(pi.hProcess, &exit_code);
//    printf("C# WinForms application exited with code %lu.\n", exit_code);
//
//    // Close process and thread handles. 
//    CloseHandle(pi.hProcess);
//    CloseHandle(pi.hThread);
//}
