#pragma once
#include "GameRenderer.h"
#include <Windows.h>
#include <D2D1.h>
#pragma comment(lib, "D2D1.lib")

namespace AssetManager {
	struct Asset {
		UINT32 width;
		UINT32 height;
		const BYTE* buffer;
	};

	ID2D1Bitmap* LoadBitmap(const Asset* asset, GameRenderer* gameRenderer);
}