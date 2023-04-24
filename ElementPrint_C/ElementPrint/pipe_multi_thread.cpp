//#include <stdio.h>
//#include <windows.h>
//
//#define BUFFER_SIZE 256
//char rx_data_buffer[BUFFER_SIZE];
//
//HANDLE hMutex;
//BOOL bDataAvailable;
//
//DWORD WINAPI ReadThread(LPVOID lpParam)
//{
//    DWORD num_of_bytes_read;
//    HANDLE pipe = INVALID_HANDLE_VALUE;
//
//    while (true)
//    {
//        pipe = CreateNamedPipe(
//            L"\\\\.\\pipe\\Pipe_Element_ID", // Use the wide string literal
//            PIPE_ACCESS_INBOUND,
//            PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
//            PIPE_UNLIMITED_INSTANCES,
//            1024,
//            1024,
//            0,
//            NULL);
//
//        if (pipe == INVALID_HANDLE_VALUE)
//        {
//            printf("Error creating named pipe: %d\n", GetLastError());
//            return 1;
//        }
//
//        BOOL connected = ConnectNamedPipe(pipe, NULL);
//        if (connected == false)
//        {
//            printf("Error connecting to named pipe: %d\n", GetLastError());
//            CloseHandle(pipe);
//            return 1;
//        }
//
//        BOOL result = ReadFile(pipe, rx_data_buffer, sizeof(rx_data_buffer) - 1, &num_of_bytes_read, NULL);
//
//        if (result == false)
//        {
//            printf("Error reading from named pipe: %d\n", GetLastError());
//            CloseHandle(pipe);
//            return 1;
//        }
//
//        rx_data_buffer[num_of_bytes_read] = '\0';
//
//        WaitForSingleObject(hMutex, INFINITE);
//        strcpy_s(rx_data_buffer, BUFFER_SIZE, rx_data_buffer);
//        bDataAvailable = TRUE;
//        ReleaseMutex(hMutex);
//
//        FlushFileBuffers(pipe);
//        DisconnectNamedPipe(pipe);
//        CloseHandle(pipe);
//    }
//
//    return 0;
//}
//
//DWORD WINAPI PrintThread(LPVOID lpParam)
//{
//    while (true)
//    {
//        WaitForSingleObject(hMutex, INFINITE);
//        if (bDataAvailable)
//        {
//            printf("Received IDs: %s\n", rx_data_buffer);
//            bDataAvailable = FALSE;
//        }
//        ReleaseMutex(hMutex);
//
//        Sleep(100); // wait for a short period before checking again
//    }
//
//    return 0;
//}
//
//int main()
//{
//    hMutex = CreateMutex(NULL, FALSE, NULL);
//    if (hMutex == NULL)
//    {
//        printf("Error creating mutex: %d\n", GetLastError());
//        return 1;
//    }
//
//    bDataAvailable = FALSE;
//
//    HANDLE hReadThread = CreateThread(NULL, 0, ReadThread, NULL, 0, NULL);
//    if (hReadThread == NULL)
//    {
//        printf("Error creating read thread: %d\n", GetLastError());
//        CloseHandle(hMutex);
//        return 1;
//    }
//
//    HANDLE hPrintThread = CreateThread(NULL, 0, PrintThread, NULL, 0, NULL);
//    if (hPrintThread == NULL)
//    {
//        printf("Error creating print thread: %d\n", GetLastError());
//        CloseHandle(hMutex);
//        CloseHandle(hReadThread);
//        return 1;
//    }
//
//    WaitForSingleObject(hReadThread, INFINITE);
//    WaitForSingleObject(hPrintThread, INFINITE);
//
//    CloseHandle(hMutex);
//    CloseHandle(hReadThread);
//    CloseHandle(hPrintThread);
//
//    return 0;
//}