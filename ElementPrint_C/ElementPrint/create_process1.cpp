#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <tchar.h>
#include <wchar.h>

int start_csharp_executable(const wchar_t* csharp_exe_path);

int main() 
{
    const wchar_t* csharp_exe_path = L"ElementSearch.exe";
    if (start_csharp_executable(csharp_exe_path) == 0) 
    {
        printf("C# executable started successfully.\n");
    }
    else 
    {
        printf("Failed to start C# executable.\n");
    }

    return 0;
}

int start_csharp_executable(const wchar_t* csharp_exe_path)
{
    STARTUPINFOW si;
    PROCESS_INFORMATION pi;

    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    ZeroMemory(&pi, sizeof(pi));

    size_t path_len = wcslen(csharp_exe_path);
    wchar_t* mutable_path = (wchar_t*)malloc((path_len + 1) * sizeof(wchar_t));
    if (!mutable_path)
    {
        printf("Failed to allocate memory for the path buffer.\n");
        return -1;
    }

    wcscpy_s(mutable_path, path_len + 1, csharp_exe_path);

    if (!CreateProcessW(
        NULL,               // No module name (use command line)
        mutable_path,       // Command line
        NULL,               // Process handle not inheritable
        NULL,               // Thread handle not inheritable
        FALSE,              // Set handle inheritance to FALSE
        0,                  // No creation flags
        NULL,               // Use parent's environment block
        NULL,               // Use parent's starting directory 
        &si,                // Pointer to STARTUPINFO structure
        &pi)                // Pointer to PROCESS_INFORMATION structure
        )
    {
        DWORD error_code = GetLastError();
        printf("CreateProcess failed (%d). Error code: %lu\n", error_code, error_code);
        free(mutable_path);
        return -1;
    }

    free(mutable_path);

    // Wait until child process exits
    WaitForSingleObject(pi.hProcess, INFINITE);

    // Wait for the child process to exit or time out after 10 seconds
    //DWORD wait_result = WaitForSingleObject(pi.hProcess, 10000);

    //if (wait_result == WAIT_TIMEOUT) 
    //{
    //    printf("Waiting for the C# executable timed out.\n");
    //}

    // Close process and thread handles
    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);

    return 0;
}