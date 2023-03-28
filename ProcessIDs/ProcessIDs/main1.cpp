//#define _CRT_SECURE_NO_DEPRECATE
//
//#include <stdio.h>
//#include <stdlib.h>
//#include <string.h>
//#include <windows.h>
//
//#define BUFFER_SIZE 256
//
//char* read_pipe_name_from_file(const char* filename)
//{
//    FILE* file = fopen(filename, "r");
//    if (!file) {
//        perror("Error opening file");
//        return NULL;
//    }
//
//    char* buffer = (char*)malloc(BUFFER_SIZE);
//    if (!buffer) {
//        perror("Error allocating memory");
//        fclose(file);
//        return NULL;
//    }
//
//    if (!fgets(buffer, BUFFER_SIZE, file)) {
//        perror("Error reading file");
//        free(buffer);
//        fclose(file);
//        return NULL;
//    }
//
//    fclose(file);
//
//    // Remove trailing newline character
//    buffer[strcspn(buffer, "\n")] = '\0';
//
//    return buffer;
//}
//
//int main() 
//{
//    //char* pipeName = read_pipe_name_from_file("pipe_name.txt");
//    //char* pipeName = read_pipe_name_from_file("../ElementSearchApp/pipe_name.txt");
//    char* pipeName = read_pipe_name_from_file("D:\\Sandbox\\ElementSearchApp\\pipe_name.txt");
//
//    if (!pipeName) 
//    {
//        fprintf(stderr, "Error reading pipe name from file\n");
//        return 1;
//    }
//
//    HANDLE pipe = INVALID_HANDLE_VALUE;
//    char buffer[1024];
//    DWORD bytesRead;
//
//    while (1)
//    {
//        //pipe = CreateNamedPipe(
//        //    L"\\\\.\\pipe\\my_pipe", // Use the wide string literal
//        //    PIPE_ACCESS_INBOUND,
//        //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
//        //    PIPE_UNLIMITED_INSTANCES,
//        //    1024,
//        //    1024,
//        //    0,
//        //    NULL);
//
//        //pipe = CreateNamedPipe(
//        //    L"\\\\.\\pipe\\Local\\my_pipe", // Use the "Local" prefix
//        //    PIPE_ACCESS_INBOUND,
//        //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
//        //    PIPE_UNLIMITED_INSTANCES,
//        //    1024,
//        //    1024,
//        //    0,
//        //    NULL);
//
//        //pipe = CreateNamedPipe(
//        //    L"\\\\.\\pipe\\Global\\my_pipe", // Use the "Global" prefix
//        //    PIPE_ACCESS_INBOUND,
//        //    PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
//        //    PIPE_UNLIMITED_INSTANCES,
//        //    1024,
//        //    1024,
//        //    0,
//        //    NULL);
//
//        pipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\my_pipe"),
//            PIPE_ACCESS_DUPLEX,
//            PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_WAIT,
//            PIPE_UNLIMITED_INSTANCES,
//            4096,
//            4096,
//            NMPWAIT_USE_DEFAULT_WAIT,
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
//            return 1;
//        }
//
//        BOOL result = ReadFile(pipe, buffer, sizeof(buffer) - 1, &bytesRead, NULL);
//        if (!result)
//        {
//            printf("Error reading from named pipe: %d\n", GetLastError());
//            CloseHandle(pipe);
//            return 1;
//        }
//
//        buffer[bytesRead] = '\0';
//        printf("Received IDs: %s\n", buffer);
//
//        FlushFileBuffers(pipe);
//        DisconnectNamedPipe(pipe);
//        CloseHandle(pipe);
//    }
//
//    return 0;
//}