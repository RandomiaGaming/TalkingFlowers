#pragma once
#include "GameWindow.h"
#include <Windows.h>
#include <D2D1.h>
#pragma comment(lib, "D2D1.lib")

class GameRenderer {
public:
	GameRenderer(GameWindow* window, UINT32 bufferWidth = 256, UINT32 bufferHeight = 144);
	void BeginDraw();
	void Clear(D2D1_COLOR_F color);
	void DrawBitmap(ID2D1Bitmap* bitmap, INT32 x, INT32 y);
	void DrawBitmap(ID2D1Bitmap* bitmap, INT32 destinationX, INT32 destinationY, INT32 sourceX, INT32 sourceY);
	void EndDraw();
	~GameRenderer();

	GameWindow* GetWindow() const;
	ID2D1Factory* GetFactory() const; // Plz don't delete hehe.
	ID2D1HwndRenderTarget* GetWindowRenderTarget() const; // Plz don't delete hehe.
	UINT32 GetBufferWidth() const;
	UINT32 GetBufferHeight() const;

private:
	ID2D1Factory* _factory;
	ID2D1HwndRenderTarget* _windowRenderTarget;

	GameWindow* _window;
	UINT32 _bufferWidth;
	UINT32 _bufferHeight;
};