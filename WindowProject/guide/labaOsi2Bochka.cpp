#include <iostream>
#include <windows.h>
#include <tchar.h>

LRESULT CALLBACK ProcessWindowProcCallback(
	HWND hWnd,		//window handle
	UINT msg,		//event code (like mouse movement, keyboard keys etc)
	WPARAM wParam,	//params are information about event (like mouse pos, what key is pressed)
	LPARAM lParam) 
{
	switch (msg)
	{
		//if we got destroy message (when we close window)
	case WM_DESTROY:
		//Push to system message that our window is closed
		PostQuitMessage(0);
		return 0;
	}
	//call base callback function
	return DefWindowProc(hWnd, msg, wParam, lParam);
}

//because our app will use different char types we will let
//compiler to choose what to use(like UTF-8, UTF-16, UNICODE, etc)
const TCHAR className[] = _T("MyApp111");

int main()
{
	WNDCLASS winC = { 0 };
	
	//Returns handle to current proccess. Handle is a indexer to system resource
	//that windows uses to manage resources.
	winC.hInstance = GetModuleHandle(NULL);
	
	//unique application name that also will be used for application indexing for windows
	winC.lpszClassName = className;
	
	//this is callback on window events. Default one is DefWindowProc, but we can decorate it with whatever we need
	winC.lpfnWndProc = ProcessWindowProcCallback;
	
	//register window. Returns non-zero value if registered successfully.
	if (!RegisterClass(&winC)) {
		std::cout << "Error: "<<GetLastError() << std::endl;
	}

	//creates window as windows resource
	auto window = CreateWindow(
		className, //app name
		_T("My window!"), // window name
		WS_OVERLAPPEDWINDOW, // windos tyle
		100, 100, // window pos
		500, 500, //window size
		HWND_DESKTOP, // window parent. Can be any handle by default. Let it be parent from desktop
		NULL, //menu. Let it be null for null
		winC.hInstance, // handle of execution module
		NULL // other params that we don't care
	);

	//We tell system to show window. This method also can be used to
	//hide window, how it with animation, etc
	ShowWindow(window, SW_SHOW);


	BOOL returnCode;	
	MSG msg;	
	//GetMessage returns returnCode that indicates whatever we
	//get some error (returnCode<0), or when we need to shut down (returnCode==0),
	//or when we fine and don't have any errors(returnCode>0)
	while ((returnCode = GetMessage(&msg, window, 0,0)) != 0) {
		if (returnCode<0) {
			auto err = GetLastError();
			if (err == 1400) break;
			std::cout << __LINE__ << " Error: " << GetLastError() << std::endl;
			break;
		}
		//We compute message into useful information
		TranslateMessage(&msg);
		//Will call our message into winC.lpfnWndProc to handle message
		DispatchMessage(&msg);
	}

	// Destroy window when we done
	DestroyWindow(window);

	//clear window resources from system by application name and
	//handle to it
	UnregisterClass(className, winC.hInstance);
}

