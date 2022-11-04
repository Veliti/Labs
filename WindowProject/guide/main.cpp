#include<stdio.h>
#include<stdlib.h>
#include<windows.h>
#include<tchar.h>
#include<cassert>

#define SIZE 1024

int main(){
	HANDLE f=CreateFile(_T("c:/cpp/OS2/lab2/file.txt"),(GENERIC_READ|GENERIC_WRITE),(FILE_SHARE_READ|FILE_SHARE_WRITE),NULL,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,NULL);
	HANDLE hMap=CreateFileMapping(f,NULL,PAGE_READWRITE,0,SIZE,_T("ohshit"));
	char*buf=(char*)MapViewOfFile(hMap,FILE_MAP_ALL_ACCESS,0,0,SIZE);
	bool run=true;
	while(run){
		int c=getchar();
		assert(getchar()=='\n');
		switch(c){
			case 'p':
				printf("%s\n",buf);
			break;
			case 'r':
				printf("enter string: ");
				fgets(buf,SIZE,stdin);
			break;
			case 'q':
				run=false;
			break;
			default:
				printf("unknown command\n");
		}
	}
	UnmapViewOfFile(buf);
	CloseHandle(hMap);
	CloseHandle(f);
return 0;
}