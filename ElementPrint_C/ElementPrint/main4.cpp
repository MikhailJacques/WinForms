//
//#define _CRT_SECURE_NO_DEPRECATE
//#include <Windows.h>
//#include <stdio.h>
//#include <stdlib.h>
//#include <string.h>
//
//#define BUFFER_SIZE 256
//
//char* get_pipe_name_from_env() 
//{
//    DWORD length = GetEnvironmentVariableA("MY_PIPE_NAME", NULL, 0);
//    if (length == 0) {
//        printf("Error: Could not find environment variable\n");
//        exit(EXIT_FAILURE);
//    }
//
//    char* pipeName = (char*)malloc(length * sizeof(char));
//    GetEnvironmentVariableA("MY_PIPE_NAME", pipeName, length);
//    return pipeName;
//}
//
//int main() {
//    char* pipeName = get_pipe_name_from_env();
//
//    printf("Connecting to pipe: %s\n", pipeName);
//
//    wchar_t wPipeName[256];
//    MultiByteToWideChar(CP_UTF8, 0, pipeName, -1, wPipeName, 256);
//
//    HANDLE pipeHandle;
//    DWORD error;
//    
//    while (1) 
//    {
//        pipeHandle = CreateFile(
//            wPipeName,
//            GENERIC_READ | GENERIC_WRITE,
//            0,
//            NULL,
//            OPEN_EXISTING,
//            FILE_ATTRIBUTE_NORMAL,
//            NULL);
//    
//        if (pipeHandle != INVALID_HANDLE_VALUE) {
//            break;
//        }
//    
//        error = GetLastError();
//    
//        if (error != ERROR_FILE_NOT_FOUND) {
//            printf("Error: %lu\n", error);
//            exit(EXIT_FAILURE);
//        }
//    
//        Sleep(1000);  // Wait for 1 second before trying again
//    }
//    
//    printf("Connected to pipe\n");
//    
//    char buffer[256];
//    DWORD bytesRead;
//    
//    while (1) {
//        BOOL success = ReadFile(pipeHandle, buffer, sizeof(buffer), &bytesRead, NULL);
//    
//        if (!success || bytesRead == 0) {
//            printf("Pipe disconnected\n");
//            break;
//        }
//    
//        buffer[bytesRead] = '\0';
//        printf("Received: %s\n", buffer);
//    }
//    
//    CloseHandle(pipeHandle);
//    free(pipeName);
//    
//    return 0;
//
//    free(pipeName);
//    return 0;
//}
