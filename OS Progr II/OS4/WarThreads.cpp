//      ������� ������������ ���� Thread War
//      ����������� ������� "�����" � "������", ����� ���������� �����
//      ������� "������" ���������� �������
//      ���� 30 ������ ����� � ������ ���������������,  �� ���������
//      ���� ������ �� ������� ������� ����������
#include <windows.h>
#include <process.h>
#include <tchar.h>

#include <stdlib.h>
#include <stdint.h>
#include <time.h>
#include <stdio.h>

#define BULLET '*' // ������ ����
#define BARREL '|' // ������ �����
#define STACK_SIZE (64*1024) // ������ ����� ��� ������ ������

// ������� �������������
HANDLE screenlock;              // ���������� ������ ���������� ������ ���� �����
HANDLE bulletsem;               // ����� ���������� ������ ��� ���� ������
HANDLE startevt;                // ���� ���������� � �������� ������� "�����" ��� "������"
HANDLE conin, conout;           // ����������� �������
HANDLE mainthread;              // �������� ����� main
CRITICAL_SECTION gameover;

// ������ � ������ �������
int columns = 0;
int rows = 0;

// ���������� ��������� � ��������
LONG hit = 0;
LONG miss = 0;
LONG delayfactor = 7;           // ������ �������� ��� ��������� �����������

char badchar[] = "-\\|/";		// �������� ����������

// �������� ���������� ����� �� n0 �� n1
int random(int n0, int n1)
{
	if (n0 == 0 && n1 == 1)
		return rand() % 2;

	return rand() % (n1 - n0) + n0;
}

// ������� ������ ������� 
void cls()
{
	COORD org = { 0, 0 };
	DWORD res;
	FillConsoleOutputCharacter(conout, ' ', columns*rows,
		org, &res);
}

// ������� �� ����� ������ � ������� � � � 
void writeat(int x, int y, char c)
{
	// ����������� ����� �� ����� ��� ������ ��������
	WaitForSingleObject(screenlock, INFINITE);
	COORD pos = { x, y };
	DWORD res;
	WriteConsoleOutputCharacterA(conout, &c, 1, pos, &res);
	ReleaseMutex(screenlock);
}

// �������� ������� �� ������� (������� ����������� � ct) 
int getakey(int &ct)
{
	INPUT_RECORD input;
	DWORD res;
	while (true) {
		ReadConsoleInput(conin, &input, 1, &res);

		// ������������ ������ �������
		if (input.EventType != KEY_EVENT)
			continue;

		// ������������ ������� ���������� ������ 
		// ��� ���������� ������ �������
		if (!input.Event.KeyEvent.bKeyDown)
			continue;

		ct = input.Event.KeyEvent.wRepeatCount;
		return input.Event.KeyEvent.wVirtualKeyCode;
	}
}

// ��������� ���������� ^�, ^Break, � �.�. 
BOOL WINAPI ctrl(DWORD type)
{
	ExitProcess(0);
	return TRUE;                // ������������ ������� ����
}

// ���������� ������ � �������� ������� ������
int getat(int x, int y)
{
	char c;
	DWORD res;
	COORD org = { x, y };

	// ����������� ������ � ������� �� ��� ���, ���� ��������� �� ����� ���������
	WaitForSingleObject(screenlock, INFINITE);
	ReadConsoleOutputCharacterA(conout, &c, 1, org, &res);
	ReleaseMutex(screenlock);   // unlock
	return c;
}

// ���������� ���� � ��������� ���� � ��������� ������� ���������� ���� 
void score(void)
{
	TCHAR s[128];
	wsprintf(s, _T("Thread War! ���������: %3d, ��������: %3d"),
		hit, miss);
	SetConsoleTitle(s);

	if (miss >= 30) {
		EnterCriticalSection(&gameover);
		SuspendThread(mainthread);      // ���������� ������� �����
		MessageBox(NULL, _T("Game Over!"), _T("Thread War"),
			MB_OK | MB_SETFOREGROUND);
		ExitProcess(0);                 // �� ������� �� ����������� ������
	}

	if ((hit + miss) % 20 == 0)
		InterlockedDecrement(&delayfactor);     // ������ ���� ilock
}

// ��� ����� ���������� 
DWORD WINAPI badguy(LPVOID _y)
{
	int y = (long)_y;          // ��������� ���������� �
	int dir;
	int dly;
	int x;
	BOOL hitme = FALSE;

	// �������� � ���������� �����, ������ � ���������� ������
	x = y % 2 ? 0 : columns;

	// ���������� ����������� � ����������� �� ��������� �������
	dir = x ? -1 : 1;

	// ���� ��������� ��������� � �������� ������
	while (((dir == 1 && x != columns) ||
		(dir == -1 && x != 0)) && !hitme) {

		// �������� �� ��������� (����?) 
		if (getat(x, y) == BULLET)
			hitme = TRUE;

		// ����� ������� �� �����
		writeat(x, y, badchar[x % 4]);

		// ��� ���� �������� �� ���������
		if (getat(x, y) == BULLET)
			hitme = TRUE;

		// �������� �� ��������� ����� ���������
		// ���������� �������
		if (delayfactor < 3)
			dly = 3;
		else
			dly = delayfactor + 3;

		for (int i = 0; i < dly; i++) {
			Sleep(40);
			if (getat(x, y) == BULLET) {
				hitme = TRUE;
				break;
			}
		}

		writeat(x, y, ' ');

		// ��� ���� �������� �� ���������
		if (getat(x, y) == BULLET)
			hitme = TRUE;

		x += dir;
	}

	if (hitme)
	{
		// � ���������� ������!
		MessageBeep(-1);
		InterlockedIncrement(&hit);
	}
	else {
		// ��������� ������!
		InterlockedIncrement(&miss);
	}

	score();

	return 0;
}

// ���� ����� ���������� ��������� ������� �����������
DWORD WINAPI badguys(LPVOID)
{
	// ���� ������� � ������ ���� � ������� 15 ������
	WaitForSingleObject(startevt, 15000);

	// ������� ���������� �����
	// ������ 5 ������ ���������� ���� �������
	// ���������� � ������������ �� 1 �� 10
	while (true) {
		if (random(0, 100) < (hit + miss) / 25 + 20)
			// �� �������� ����������� �������������
			CreateThread(NULL, STACK_SIZE, badguy,
			(void *)(random(1, 10)), 0, NULL);
		Sleep(1000);            // ������ �������
	}
}

// ��� ����� ����
// ������ ���� - ��� ��������� �����
DWORD WINAPI bullet(LPVOID _xy_)
{
	int x = LOWORD(_xy_);
	int y = HIWORD(_xy_);

	if (getat(x, y) == '*')
		return 0;               // ����� ��� ���� ����

								// ���� ��������� 
								// ��������� �������
								// ���� ������� ����� 0, �������� �� ���������� 
	if (WaitForSingleObject(bulletsem, 0) == WAIT_TIMEOUT)
		return 0;

	while (--y >= 0) {
		writeat(x, y, BULLET);       // ���������� ����
		Sleep(100);
		writeat(x, y, ' ');       // ������� ����
	}

	// ������� ������ - �������� 1 � ��������
	ReleaseSemaphore(bulletsem, 1, NULL);

	return 0;
}

// �������� ���������
int main()
{
	// ��������� ���������� ����������
	conin = GetStdHandle(STD_INPUT_HANDLE);
	conout = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleCtrlHandler(ctrl, TRUE);
	SetConsoleMode(conin, ENABLE_WINDOW_INPUT);

	// ������ ������
	CONSOLE_CURSOR_INFO cinfo;
	cinfo.bVisible = false;
	SetConsoleCursorInfo(conout, &cinfo);

	// ������� �������� ���������� �������� ������
	DuplicateHandle(GetCurrentProcess(), GetCurrentThread(), GetCurrentProcess(),
		&mainthread, 0, FALSE, DUPLICATE_SAME_ACCESS);

	startevt = CreateEvent(NULL, TRUE, FALSE, NULL);
	screenlock = CreateMutex(NULL, FALSE, NULL);
	InitializeCriticalSection(&gameover);
	bulletsem = CreateSemaphore(NULL, 3, 3, NULL);

	CONSOLE_SCREEN_BUFFER_INFO info;        // ���������� � �������
	GetConsoleScreenBufferInfo(conout, &info);

	// ���������������� ����������� ���������� �� �����
	score();

	// ��������� ��������� ��������� �����
	srand((unsigned)time(NULL));
	cls();                      // �� ����� ���� �� ����� 

	// ��������� ��������� ������� �����
	columns = info.srWindow.Right - info.srWindow.Left + 1;
	rows = info.srWindow.Bottom - info.srWindow.Top + 1;
	int y = rows - 1;
	int x = columns / 2;

	// ��������� ����� badguys; ������ �� ������ �� ��� ���, 
	// ���� �� ���������� ������� ��� ������� 15 ������
	CreateThread(NULL, STACK_SIZE, badguys, NULL, 0, NULL);

	// �������� ���� ����
	while (true) {
		int c, ct;

		writeat(x, y, BARREL);     // ���������� ����� 
		c = getakey(ct);        // �������� ������

		switch (c) {
		case VK_SPACE:
			CreateThread(NULL, 64 * 1024, bullet, (void *)MAKELONG(x, y), 0, NULL);
			Sleep(100);     // ���� ���� ����� ������� �� ��������� ����������
			break;

		case VK_LEFT:          // ������� "�����!"
			SetEvent(startevt); // ����� badguys �������� 
			writeat(x, y, ' '); // ������ � ������ �����
			x = (x > ct) ? (x - ct) : 0;
			break;

		case VK_RIGHT:         // ������� "������!"; ������ �� ��
			SetEvent(startevt);
			writeat(x, y, ' ');
			x = (x + ct < columns - 1) ? (x + ct) : (columns - 1);
			break;
		}
	}
}
