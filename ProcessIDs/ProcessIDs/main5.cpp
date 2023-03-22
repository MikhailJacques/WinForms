#include <Windows.h>
#include <stdio.h>

int main() 
{
    const char* sharedMemoryName = "MySharedMemory";
    int memorySize = 1024;

    HANDLE sharedMemory = OpenFileMappingA(FILE_MAP_READ, FALSE, sharedMemoryName);

    if (sharedMemory == NULL) 
    {
        printf("Error: Could not open shared memory\n");
        return 1;
    }

    void* memory = MapViewOfFile(sharedMemory, FILE_MAP_READ, 0, 0, memorySize);

    if (memory == NULL) 
    {
        printf("Error: Could not map shared memory\n");
        CloseHandle(sharedMemory);
        return 1;
    }

    printf("Data read from shared memory:\n %s", (char*)memory);

    UnmapViewOfFile(memory);
    CloseHandle(sharedMemory);
    return 0;
}
