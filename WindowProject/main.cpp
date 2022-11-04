#include <windows.h>
#include <tchar.h>

const TCHAR CLASS_NAME[] = _T("My Window");

HBRUSH hBrush;

LRESULT CALLBACK WindowUpdate(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);

int main(int argc, char const *argv[])
{
    int nCmdShow = SW_SHOW;

    HINSTANCE hThisInstance = GetModuleHandle(NULL);

    BOOL bMessageOk;
    MSG message;
    HWND windowHandle;
    WNDCLASS winClass = 
    {
        .lpfnWndProc = WindowUpdate,
        .hInstance = hThisInstance,
        .lpszClassName = CLASS_NAME
    };

    hBrush = CreateSolidBrush(RGB(255,255,255));


    return EXIT_SUCCESS;
}