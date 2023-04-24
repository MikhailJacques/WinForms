#include <stdio.h>
#include <windows.h>

#define BUFFER_SIZE 256

HANDLE hMutex;
char rx_data_buffer[BUFFER_SIZE];
BOOL bDataAvailable;
HANDLE hDataAvailableEvent;
HANDLE hExitEvent;

VOID WINAPI ReadCompletionRoutine(DWORD dwErrorCode, DWORD dwNumberOfBytesTransferred, LPOVERLAPPED lpOverlapped);
VOID WINAPI PrintCompletionRoutine(DWORD dwErrorCode, DWORD dwNumberOfBytesTransferred, LPOVERLAPPED lpOverlapped);

BOOL WINAPI CtrlHandler(DWORD fdwCtrlType)
{
    if (fdwCtrlType == CTRL_C_EVENT)
    {
        SetEvent(hExitEvent);
        return TRUE;
    }

    return FALSE;
}

DWORD WINAPI ReadThread(LPVOID lpParam)
{
    HANDLE pipe = CreateNamedPipe(
        L"\\\\.\\pipe\\Pipe_Element_ID",
        PIPE_ACCESS_INBOUND | FILE_FLAG_OVERLAPPED,
        PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
        PIPE_UNLIMITED_INSTANCES,
        1024,
        1024,
        0,
        NULL);

    if (pipe == INVALID_HANDLE_VALUE)
    {
        printf("Error creating named pipe: %d\n", GetLastError());
        return 1;
    }

    OVERLAPPED ol = { 0 };
    ol.hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

    while (true)
    {
        BOOL result = ConnectNamedPipe(pipe, &ol);
        if (!result && GetLastError() == ERROR_IO_PENDING)
        {
            DWORD bytesTransferred;
            result = GetOverlappedResult(pipe, &ol, &bytesTransferred, TRUE);
        }

        if (!result)
        {
            printf("Error connecting to named pipe: %d\n", GetLastError());
            CloseHandle(pipe);
            return 1;
        }

        result = ReadFileEx(pipe, rx_data_buffer, sizeof(rx_data_buffer) - 1, &ol, ReadCompletionRoutine);

        if (!result)
        {
            printf("Error reading from named pipe: %d\n", GetLastError());
            CloseHandle(pipe);
            return 1;
        }

        SleepEx(INFINITE, TRUE);

        DisconnectNamedPipe(pipe); // Add this line to disconnect the pipe after reading data
    }

    return 0;
}


VOID WINAPI ReadCompletionRoutine(DWORD dwErrorCode, DWORD dwNumberOfBytesTransferred, LPOVERLAPPED lpOverlapped)
{
    if (dwErrorCode == 0 && dwNumberOfBytesTransferred > 0)
    {
        rx_data_buffer[dwNumberOfBytesTransferred] = '\0';

        WaitForSingleObject(hMutex, INFINITE);
        strcpy_s(rx_data_buffer, BUFFER_SIZE, rx_data_buffer);
        bDataAvailable = TRUE;
        SetEvent(hDataAvailableEvent);
        ReleaseMutex(hMutex);
    }
}

DWORD WINAPI PrintThread(LPVOID lpParam)
{
    while (true)
    {
        DWORD waitResult = WaitForSingleObject(hDataAvailableEvent, INFINITE);
        if (waitResult == WAIT_OBJECT_0)
        {
            WaitForSingleObject(hMutex, INFINITE);
            if (bDataAvailable)
            {
                printf("Received IDs: %s\n", rx_data_buffer);
                bDataAvailable = FALSE;
                ResetEvent(hDataAvailableEvent);
            }
            ReleaseMutex(hMutex);
        }
    }

    return 0;
}

VOID CALLBACK PrintCompletionRoutine(
    DWORD dwErrorCode,
    DWORD dwNumberOfBytesTransfered,
    LPOVERLAPPED lpOverlapped)
{
    if (dwErrorCode == 0 && dwNumberOfBytesTransfered > 0)
    {
        WaitForSingleObject(hMutex, INFINITE);
        if (bDataAvailable)
        {
            printf("Received IDs: ");
            printf("%s\n", rx_data_buffer);
            bDataAvailable = FALSE;
            ResetEvent(hDataAvailableEvent);
        }
        ReleaseMutex(hMutex);
    }
    else
    {
        printf("Error in PrintCompletionRoutine: %d\n", dwErrorCode);
    }
}



int main()
{
    hMutex = CreateMutex(NULL, FALSE, NULL);
    if (hMutex == NULL)
    {
        printf("Error creating mutex: %d\n", GetLastError());
        return 1;
    }

    hDataAvailableEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
    if (hDataAvailableEvent == NULL)
    {
        printf("Error creating data available event: %d\n", GetLastError());
        CloseHandle(hMutex);
        return 1;
    }

    bDataAvailable = FALSE;

    HANDLE hReadThread = CreateThread(NULL, 0, ReadThread, NULL, 0, NULL);
    if (hReadThread == NULL)
    {
        printf("Error creating read thread: %d\n", GetLastError());
        CloseHandle(hMutex);
        CloseHandle(hDataAvailableEvent);
        return 1;
    }

    HANDLE hPrintThread = CreateThread(NULL, 0, PrintThread, NULL, 0, NULL);
    if (hPrintThread == NULL)
    {
        printf("Error creating print thread: %d\n", GetLastError());
        CloseHandle(hMutex);
        CloseHandle(hDataAvailableEvent);
        CloseHandle(hReadThread);
        return 1;
    }

    hExitEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
    if (hExitEvent == NULL)
    {
        printf("Error creating exit event: %d\n", GetLastError());
        CloseHandle(hMutex);
        CloseHandle(hDataAvailableEvent);
        CloseHandle(hReadThread);
        CloseHandle(hPrintThread);
        return 1;
    }

    if (!SetConsoleCtrlHandler(CtrlHandler, TRUE))
    {
        printf("Error setting control handler: %d\n", GetLastError());
        CloseHandle(hMutex);
        CloseHandle(hDataAvailableEvent);
        CloseHandle(hReadThread);
        CloseHandle(hPrintThread);
        CloseHandle(hExitEvent);
        return 1;
    }

    WaitForSingleObject(hExitEvent, INFINITE);

    TerminateThread(hReadThread, 0);
    TerminateThread(hPrintThread, 0);
    CloseHandle(hMutex);
    CloseHandle(hDataAvailableEvent);
    CloseHandle(hReadThread);
    CloseHandle(hPrintThread);
    CloseHandle(hExitEvent);

    return 0;
}
