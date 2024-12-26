#include "GameWindow.h"

BOOL GameWindow::_doneGlobalInit;

LRESULT CALLBACK GameWindow::WndProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
	GameWindow* gameWindow = reinterpret_cast<GameWindow*>(GetWindowLongPtr(hwnd, GWLP_USERDATA));

	if (uMsg == WM_DESTROY) {
		gameWindow->_state = 2; // Destroyed
	}

	return DefWindowProc(hwnd, uMsg, wParam, lParam);
}

GameWindow::GameWindow(const wchar_t* title) {
	if (!_doneGlobalInit) {
		WNDCLASS wc = {};

		wc.style = CS_HREDRAW | CS_VREDRAW; // Redraw windows with this class on horizontal or vertical changes.
		wc.lpfnWndProc = WndProc; // Define the window event handler.
		wc.cbClsExtra = 0; // Allocate 0 extra bytes after the class declaration.
		wc.cbWndExtra = 0; // Allocate 0 extra bytes after windows of this class.
		wc.hInstance = GetModuleHandle(nullptr); // The WndProc function is in the current hInstance.
		wc.hIcon = nullptr; // Use the default icon.
		wc.hCursor = LoadCursor(nullptr, IDC_ARROW); // Load cursor with an HInstance of nullptr to load from system cursors.
		wc.hbrBackground = nullptr; // A background brush of null specifies user implamented background painting.
		wc.lpszMenuName = nullptr; // Windows of this class have no default menu.
		wc.lpszClassName = GameWindowClassName; // Set the window class name to DefaultD2DClass.

		::RegisterClass(&wc);

		_doneGlobalInit = true;
	}

	_state = 0;

	int primaryScreenWidth = GetSystemMetrics(SM_CXSCREEN); // Get the width of the primary display.
	int primaryScreenHeight = GetSystemMetrics(SM_CYSCREEN); // Get the height of the primary display.

	int width = primaryScreenWidth / 2;
	int height = primaryScreenHeight / 2;
	int x = width / 2;
	int y = height / 2;

	if (title == nullptr) {
		_title = DefaultGameWindowTitle;
	}
	else {
		_title = title;
	}

	_handle = CreateWindowEx(
		0, // Optional window styles
		GameWindowClassName, // Use the DefaultD2DClass which must be registerd in advance.
		_title, // Set the window title based on function input.
		WS_BORDER | WS_CAPTION | WS_MAXIMIZEBOX | WS_MINIMIZEBOX | WS_SIZEBOX | WS_SYSMENU, // Window style
		x, // Set the X position of our window to a quarter of the screen width.
		y, // Set the Y position of our window to a quarter of the screen height.
		width, // Set the width of our window to half of the screen width.
		height, // Set the height of our window to half of the screen height.
		nullptr, // Set the parent window to nullptr.
		nullptr, // Set the target menu to nullptr.
		GetModuleHandle(nullptr), // Set the instance handle to the instance handle for the current process.
		nullptr // Set additional data to nullptr.
	);

	SetWindowLongPtr(_handle, GWLP_USERDATA, reinterpret_cast<LONG_PTR>(this));
}
void GameWindow::Show(int showCommand) {
	if (showCommand < 0) {
		showCommand = SW_SHOWDEFAULT;
	}
	ShowWindow(_handle, showCommand);
}
void GameWindow::ProcessMessage() {
	if (IsDestroyed()) {
		return;
	}

	MSG msg = {};
	if (PeekMessage(&msg, _handle, 0, 0, PM_REMOVE)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}
void GameWindow::ClearMessageQueue() {
	if (IsDestroyed()) {
		return;
	}

	MSG msg = {};
	while (PeekMessage(&msg, _handle, 0, 0, PM_REMOVE)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}
void GameWindow::RunMessagePump() {
	MSG msg = {};
	while (GetMessage(&msg, _handle, 0, 0) && !IsDestroyed()) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
}
GameWindow::~GameWindow() {
	if (IsShowing()) {
		DestroyWindow(_handle);
	}

	_handle = nullptr;
	_title = nullptr;
	_state = 2;
}

HWND GameWindow::GetHandle() const {
	return _handle;
}
const wchar_t* GameWindow::GetTitle() const {
	return _title;
}
BOOL GameWindow::IsShowing() const {
	return _state == 1;
}
BOOL GameWindow::IsDestroyed() const {
	return _state == 2;
}