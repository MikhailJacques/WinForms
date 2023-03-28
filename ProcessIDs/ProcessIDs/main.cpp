
#include <stdio.h>
#include <windows.h>

#define BUFFER_SIZE 256

int main()
{
    DWORD num_of_bytes_read;
    char rx_data_buffer[BUFFER_SIZE];
    HANDLE pipe = INVALID_HANDLE_VALUE;

    while (true)
    {
        pipe = CreateNamedPipe(
            L"\\\\.\\pipe\\Pipe_Element_ID", // Use the wide string literal
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
        printf("Received IDs: %s\n", rx_data_buffer);

        FlushFileBuffers(pipe);
        DisconnectNamedPipe(pipe);
        CloseHandle(pipe);
    }
  
    //pipe = CreateNamedPipe(
    //    L"\\\\.\\pipe\\MyPipe", // Use the wide string literal
    //    PIPE_ACCESS_INBOUND,
    //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
    //    PIPE_UNLIMITED_INSTANCES,
    //    1024,
    //    1024,
    //    0,
    //    NULL);

    //if (pipe == INVALID_HANDLE_VALUE)
    //{
    //    printf("Error creating named pipe: %d\n", GetLastError());
    //    return 1;
    //}

    //BOOL connected = ConnectNamedPipe(pipe, NULL);
    //if (connected == false)
    //{
    //    printf("Error connecting to named pipe: %d\n", GetLastError());
    //    CloseHandle(pipe);
    //    return 1;
    //}

    //while (true)
    //{
    //    BOOL result = ReadFile(pipe, rx_data_buffer, sizeof(rx_data_buffer) - 1, &num_of_bytes_read, NULL);

    //    if (result == false)
    //    {
    //        printf("Error reading from named pipe: %d\n", GetLastError());
    //        FlushFileBuffers(pipe);
    //        DisconnectNamedPipe(pipe);
    //        CloseHandle(pipe);
    //        return 1;
    //    }

    //    rx_data_buffer[num_of_bytes_read] = '\0';
    //    printf("Received IDs: %s\n", rx_data_buffer);
    //}

    //FlushFileBuffers(pipe);
    //DisconnectNamedPipe(pipe);
    //CloseHandle(pipe);

    return 0;
}