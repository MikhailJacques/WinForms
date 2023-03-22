//
//#define _CRT_SECURE_NO_DEPRECATE
//
//#include <Windows.h>
//#include <stdio.h>
//#include <stdlib.h>
//#include <string.h>
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
//int main() {
//    char* pipeName = read_pipe_name_from_file("D:\\Sandbox\\ElementSearchApp\\pipe_name.txt");
//
//    printf("Connecting to pipe: %s\n", pipeName);
//
//    wchar_t wPipeName[256];
//    MultiByteToWideChar(CP_UTF8, 0, pipeName, -1, wPipeName, 256);
//
//    HANDLE pipeHandle;
//    DWORD error;
//
//    while (1) {
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
//}
