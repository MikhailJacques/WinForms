//
//#define _CRT_SECURE_NO_DEPRECATE
//#include <stdio.h>
//#include <stdlib.h>
//#include <string.h>
//#include <windows.h>
//
////char* read_pipe_name_from_file(const char* filename) 
////{
////    FILE* file = fopen(filename, "r");
////    if (file == NULL) {
////        perror("Error opening file");
////        exit(EXIT_FAILURE);
////    }
////
////    char* pipeName = malloc(256);
////    if (fgets(pipeName, 256, file) == NULL) {
////        perror("Error reading from file");
////        exit(EXIT_FAILURE);
////    }
////
////    fclose(file);
////    return pipeName;
////}
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
//    char* pipeName = read_pipe_name_from_file("D:\\Sandbox\\ElementSearchApp\\pipe_name.txt");
//
//    printf("Connecting to pipe: %s\n", pipeName);
//
//    wchar_t wPipeName[256];
//    
//    MultiByteToWideChar(CP_UTF8, 0, pipeName, -1, wPipeName, 256);
//
//    HANDLE pipeHandle = CreateFile(
//        wPipeName,
//        GENERIC_READ | GENERIC_WRITE,
//        0,
//        NULL,
//        OPEN_EXISTING,
//        FILE_ATTRIBUTE_NORMAL,
//        NULL);
//
//    if (pipeHandle == INVALID_HANDLE_VALUE) {
//        printf("Error: %lu\n", GetLastError());
//        exit(EXIT_FAILURE);
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
//}
