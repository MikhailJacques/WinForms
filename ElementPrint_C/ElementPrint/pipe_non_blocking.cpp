//#include <stdio.h>
//#include <windows.h>
//
//#define BUFFER_SIZE 256
//char rx_data_buffer[BUFFER_SIZE];
//
//HANDLE hMutex;
//HANDLE hExitEvent;
//HANDLE hDataAvailableEvent;
//
//BOOL bDataAvailable;
//
//BOOL WINAPI CtrlHandler(DWORD fdwCtrlType)
//{
//    if (fdwCtrlType == CTRL_C_EVENT)
//    {
//        SetEvent(hExitEvent);
//        return TRUE;
//    }
//
//    return FALSE;
//}
//
//DWORD WINAPI ReadThread(LPVOID lpParam)
//{
//    DWORD num_of_bytes_read;
//    HANDLE pipe;
//
//    while (true)
//    {
//        pipe = CreateNamedPipe(
//            L"\\\\.\\pipe\\Pipe_Element_ID",
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
//        if (!connected)
//        {
//            printf("Error connecting to named pipe: %d\n", GetLastError());
//            CloseHandle(pipe);
//            continue; // Continue to the next iteration to create a new named pipe
//        }
//
//        BOOL result = ReadFile(pipe, rx_data_buffer, sizeof(rx_data_buffer) - 1, &num_of_bytes_read, NULL);
//
//        if (!result)
//        {
//            printf("Error reading from named pipe: %d\n", GetLastError());
//            DisconnectNamedPipe(pipe);
//            CloseHandle(pipe);
//            continue; // Continue to the next iteration to create a new named pipe
//        }
//
//        rx_data_buffer[num_of_bytes_read] = '\0';
//
//        WaitForSingleObject(hMutex, INFINITE);
//        strcpy_s(rx_data_buffer, BUFFER_SIZE, rx_data_buffer);
//        bDataAvailable = TRUE;
//        SetEvent(hDataAvailableEvent);
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
//
//
//DWORD WINAPI PrintThread(LPVOID lpParam)
//{
//    while (true)
//    {
//        DWORD waitResult = WaitForSingleObject(hDataAvailableEvent, INFINITE);
//        if (waitResult == WAIT_OBJECT_0)
//        {
//            WaitForSingleObject(hMutex, INFINITE);
//            if (bDataAvailable)
//            {
//                printf("Received IDs: %s\n", rx_data_buffer);
//                bDataAvailable = FALSE;
//                ResetEvent(hDataAvailableEvent);
//            }
//            ReleaseMutex(hMutex);
//        }
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
//    hDataAvailableEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
//    if (hDataAvailableEvent == NULL)
//    {
//        printf("Error creating data available event: %d\n", GetLastError());
//        CloseHandle(hMutex);
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
//        CloseHandle(hDataAvailableEvent);
//        return 1;
//    }
//
//    HANDLE hPrintThread = CreateThread(NULL, 0, PrintThread, NULL, 0, NULL);
//    if (hPrintThread == NULL)
//    {
//        printf("Error creating print thread: %d\n", GetLastError());
//        CloseHandle(hMutex);
//        CloseHandle(hDataAvailableEvent);
//        CloseHandle(hReadThread);
//        return 1;
//    }
//
//    hExitEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
//    if (hExitEvent == NULL)
//    {
//        printf("Error creating exit event: %d\n", GetLastError());
//        CloseHandle(hMutex);
//        CloseHandle(hDataAvailableEvent);
//        CloseHandle(hReadThread);
//        CloseHandle(hPrintThread);
//        return 1;
//    }
//
//    if (!SetConsoleCtrlHandler(CtrlHandler, TRUE))
//    {
//        printf("Error setting control handler: %d\n", GetLastError());
//        CloseHandle(hMutex);
//        CloseHandle(hDataAvailableEvent);
//        CloseHandle(hReadThread);
//        CloseHandle(hPrintThread);
//        CloseHandle(hExitEvent);
//        return 1;
//    }
//
//    WaitForSingleObject(hExitEvent, INFINITE);
//
//    TerminateThread(hReadThread, 0);
//    TerminateThread(hPrintThread, 0);
//    CloseHandle(hMutex);
//    CloseHandle(hDataAvailableEvent);
//    CloseHandle(hReadThread);
//    CloseHandle(hPrintThread);
//    CloseHandle(hExitEvent);
//
//    return 0;
//}