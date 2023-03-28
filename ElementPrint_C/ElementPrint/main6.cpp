//#include <Windows.h>
//#include <stdio.h>
//
//int main()
//{
//    HANDLE dataReadyEvent = OpenEvent(EVENT_MODIFY_STATE | SYNCHRONIZE, FALSE, L"DataReadyEvent");
//    HANDLE dataReadEvent = OpenEvent(EVENT_MODIFY_STATE | SYNCHRONIZE, FALSE, L"DataReadEvent");
//
//    if (!dataReadyEvent || !dataReadEvent)
//    {
//        printf("Error opening events: %d\n", GetLastError());
//        return 1;
//    }
//
//    // Wait for the server to signal that data is ready to be read.
//    WaitForSingleObject(dataReadyEvent, INFINITE);
//
//    HANDLE sharedMemory = OpenFileMapping(FILE_MAP_READ, FALSE, L"MySharedMemory");
//
//    if (!sharedMemory)
//    {
//        printf("Error opening shared memory: %d\n", GetLastError());
//        return 1;
//    }
//
//    char* buffer = (char*)MapViewOfFile(sharedMemory, FILE_MAP_READ, 0, 0, 1024);
//
//    if (!buffer)
//    {
//        printf("Error mapping shared memory: %d\n", GetLastError());
//        return 1;
//    }
//
//    printf("Data read from shared memory:\n%s\n", buffer);
//
//    UnmapViewOfFile(buffer);
//    CloseHandle(sharedMemory);
//
//    // Signal the server that we've finished reading the data.
//    SetEvent(dataReadEvent);
//
//    CloseHandle(dataReadEvent);
//    CloseHandle(dataReadyEvent);
//
//    return 0;
//}