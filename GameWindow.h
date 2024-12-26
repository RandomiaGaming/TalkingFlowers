// KNOWN ISSUE: The loading cursor IDC_APPSTARTING which displays when starting debugging is a glitch in Visual Studio not
// an issue with EpsilonEngine. That's why the loading cursor persists even when you switch to unrelated apps like file explorer.
// If you launch an EpsilonEngine game from the .exe file this won't happen. Complain to Microsoft about it not me. LOL

#pragma once
#include <Windows.h>

class GameWindow {
public:
	GameWindow(const wchar_t* title = nullptr);
	void Show(int showCommand = -1);
	void ProcessMessage(); // Processes one message. If there are none returns instantly. Returns instantly if the window is closed.
	void ClearMessageQueue(); // Processes all messages in the queue then returns. Returns instantly if the window is closed.
	void RunMessagePump(); // Processes incoming messages until the window closes.
	~GameWindow();

	HWND GetHandle() const;
	const wchar_t* GetTitle() const;
	BOOL IsShowing() const;
	BOOL IsDestroyed() const;

private:
	HWND _handle;
	const wchar_t* _title;
	UINT32 _state; // 0 = Created, 1 = Showing, 2 = Destroyed

	static BOOL _doneGlobalInit;

	static constexpr const wchar_t* DefaultGameWindowTitle = L"Unnamed Game Window";
	static constexpr const wchar_t* GameWindowClassName = L"GameWindowClass";

	static LRESULT CALLBACK WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
};