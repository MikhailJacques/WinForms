// ProcessIDs.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <stdio.h>
#include <windows.h>
//#include <iostream>

//int main()
//{
//    std::cout << "Hello World!\n";
//}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file


#include <stdio.h>
#include <windows.h>

int main()
{
    HANDLE pipe = INVALID_HANDLE_VALUE;
    char buffer[1024];
    DWORD bytesRead;

    while (1)
    {
        pipe = CreateNamedPipe(
            L"\\\\.\\pipe\\MyPipe", // Use the wide string literal
            PIPE_ACCESS_INBOUND,
            PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
            PIPE_UNLIMITED_INSTANCES,
            1024,
            1024,
            0,
            NULL);

        //pipe = CreateNamedPipe(
        //    L"\\\\.\\pipe\\Local\\MyPipe", // Use the "Local" prefix
        //    PIPE_ACCESS_INBOUND,
        //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
        //    PIPE_UNLIMITED_INSTANCES,
        //    1024,
        //    1024,
        //    0,
        //    NULL);

        //pipe = CreateNamedPipe(
        //    L"\\\\.\\pipe\\Global\\MyPipe", // Use the "Global" prefix
        //    PIPE_ACCESS_INBOUND,
        //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
        //    PIPE_UNLIMITED_INSTANCES,
        //    1024,
        //    1024,
        //    0,
        //    NULL);

        //pipe = CreateNamedPipe(TEXT("\\\\.\\MyPipe\\my_pipe"),
        //    PIPE_ACCESS_DUPLEX,
        //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
        //    PIPE_UNLIMITED_INSTANCES,
        //    4096,
        //    4096,
        //    NMPWAIT_USE_DEFAULT_WAIT,
        //    NULL);

        if (pipe == INVALID_HANDLE_VALUE)
        {
            printf("Error creating named pipe: %d\n", GetLastError());
            return 1;
        }

        BOOL connected = ConnectNamedPipe(pipe, NULL);
        if (!connected)
        {
            printf("Error connecting to named pipe: %d\n", GetLastError());
            CloseHandle(pipe);
            return 1;
        }

        BOOL result = ReadFile(pipe, buffer, sizeof(buffer) - 1, &bytesRead, NULL);
        if (!result)
        {
            printf("Error reading from named pipe: %d\n", GetLastError());
            CloseHandle(pipe);
            return 1;
        }

        buffer[bytesRead] = '\0';
        printf("Received IDs: %s\n", buffer);

        FlushFileBuffers(pipe);
        DisconnectNamedPipe(pipe);
        CloseHandle(pipe);
    }

    return 0;
}
