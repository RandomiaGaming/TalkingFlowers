#include "EpsilonEngine.h"
#include "TalkingFlowerImage.h";
#include <thread>
#include <chrono>

int main(int argc, char** argv) {
	Profiler profiler = Profiler(1000);

	GameWindow window = GameWindow(L"");

	GameRenderer renderer = GameRenderer(&window);

	ID2D1Bitmap* testImage = AssetManager::LoadBitmap(&TalkingFlowerImage, &renderer);

	window.Show();

	while (true) {
		window.ClearMessageQueue();

		renderer.BeginDraw();
		renderer.Clear(D2D1::ColorF(D2D1::ColorF::CornflowerBlue));

		renderer.DrawBitmap(testImage, 0, 0);
		
		renderer.EndDraw();

		profiler.Tick();
	}

	return 0;
}