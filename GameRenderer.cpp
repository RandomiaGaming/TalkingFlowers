#include "GameRenderer.h"

GameRenderer::GameRenderer(GameWindow* window, UINT32 bufferWidth, UINT32 bufferHeight) {
	_window = window;
	_bufferWidth = bufferWidth;
	_bufferHeight = bufferHeight;

	D2D1CreateFactory(D2D1_FACTORY_TYPE_MULTI_THREADED, &_factory);

	int primaryScreenWidth = GetSystemMetrics(SM_CXSCREEN);
	int primaryScreenHeight = GetSystemMetrics(SM_CYSCREEN);

	_factory->CreateHwndRenderTarget(
		D2D1::RenderTargetProperties(),
		D2D1::HwndRenderTargetProperties(_window->GetHandle(), D2D1::SizeU(_bufferWidth, _bufferHeight), D2D1_PRESENT_OPTIONS_IMMEDIATELY),
		&_windowRenderTarget
	);
}
void GameRenderer::BeginDraw() {
	_windowRenderTarget->BeginDraw();
}
void GameRenderer::Clear(D2D1_COLOR_F color) {
	_windowRenderTarget->Clear(color);
}
void GameRenderer::DrawBitmap(ID2D1Bitmap* bitmap, INT32 x, INT32 y) {
	D2D1_SIZE_F size = bitmap->GetSize();
	float width = size.width;
	float height = size.height;

	_windowRenderTarget->DrawBitmap(
		bitmap,
		D2D1::RectF(x, y, x + width, y + height),
		1,
		D2D1_BITMAP_INTERPOLATION_MODE_NEAREST_NEIGHBOR,
		D2D1::RectF(0, 0, width, height)
	);
}
void GameRenderer::DrawBitmap(ID2D1Bitmap* bitmap, INT32 destinationX, INT32 destinationY, INT32 sourceX, INT32 sourceY) {
	D2D1_SIZE_F size = bitmap->GetSize();
	float width = size.width;
	float height = size.height;

	_windowRenderTarget->DrawBitmap(
		bitmap,
		D2D1::RectF(destinationX, destinationY, destinationX + width, destinationY + height),
		1.0f,
		D2D1_BITMAP_INTERPOLATION_MODE_NEAREST_NEIGHBOR,
		D2D1::RectF(sourceX, sourceY, sourceX + width, sourceY + height)
	);
}
void GameRenderer::EndDraw() {
	_windowRenderTarget->EndDraw();
}
GameRenderer::~GameRenderer() {
	_windowRenderTarget->Release();
	_factory->Release();

	_windowRenderTarget = nullptr;
	_factory = nullptr;
	_window = nullptr;
	_bufferWidth = 0;
	_bufferHeight = 0;
}

GameWindow* GameRenderer::GetWindow() const {
	return _window;
}
ID2D1Factory* GameRenderer::GetFactory() const {
	return _factory;
}
ID2D1HwndRenderTarget* GameRenderer::GetWindowRenderTarget() const {
	return _windowRenderTarget;
}
UINT32 GameRenderer::GetBufferWidth() const {
	return _bufferWidth;
}
UINT32 GameRenderer::GetBufferHeight() const {
	return _bufferHeight;
}