#include <stdio.h>
#include <windows.h>

#define BUFFER_SIZE 256
#define WM_RX_DATA_AVAILABLE (WM_USER + 1)

char rx_data_buffer[BUFFER_SIZE];
HWND hWnd;

HANDLE hMutex;
BOOL bDataAvailable;

DWORD WINAPI ReadThread(LPVOID lpParam)
{
    DWORD num_of_bytes_read;
    HANDLE pipe = INVALID_HANDLE_VALUE;

    while (true)
    {
        pipe = CreateNamedPipe(
            L"\\\\.\\pipe\\Pipe_Element_ID",
            PIPE_ACCESS_INBOUND,
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

        BOOL connected = ConnectNamedPipe(pipe, NULL);
        if (connected == false)
        {
            printf("Error connecting to named pipe: %d\n", GetLastError());
            CloseHandle(pipe);
            return 1;
        }

        BOOL result = ReadFile(pipe, rx_data_buffer, sizeof(rx_data_buffer) - 1, &num_of_bytes_read, NULL);

        if (result == false)
        {
            printf("Error reading from named pipe: %d\n", GetLastError());
            CloseHandle(pipe);
            return 1;
        }

        rx_data_buffer[num_of_bytes_read] = '\0';

        WaitForSingleObject(hMutex, INFINITE);
        strcpy_s(rx_data_buffer, BUFFER_SIZE, rx_data_buffer);
        bDataAvailable = TRUE;
        ReleaseMutex(hMutex);

        PostMessage(hWnd, WM_RX_DATA_AVAILABLE, 0, 0);

        FlushFileBuffers(pipe);
        DisconnectNamedPipe(pipe);
        CloseHandle(pipe);
    }

    return 0;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_RX_DATA_AVAILABLE:
        WaitForSingleObject(hMutex, INFINITE);
        if (bDataAvailable)
        {
            printf("Received IDs: %s\n", rx_data_buffer);
            bDataAvailable = FALSE;
        }
        ReleaseMutex(hMutex);
        break;

        // ... other case statements for handling other messages ...

    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }

    return 0;
}

int main()
{
    // ... initialize hWnd variable with the handle of your main window ...

    hMutex = CreateMutex(NULL, FALSE, NULL);
    if (hMutex == NULL)
    {
        printf("Error creating mutex: %d\n", GetLastError());
        return 1;
    }

    bDataAvailable = FALSE;

    HANDLE hReadThread = CreateThread(NULL, 0, ReadThread, NULL, 0, NULL);
    if (hReadThread == NULL)
    {
        printf("Error creating read thread: %d\n", GetLastError());
        CloseHandle(hMutex);
        return 1;
    }

    // ... add standard message loop for the main UI thread ...

    WaitForSingleObject(hReadThread, INFINITE);

    CloseHandle(hMutex);
    CloseHandle(hReadThread);

    return 0;
}