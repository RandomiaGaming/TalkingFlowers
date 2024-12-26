#include "Profiler.h"

Profiler::Profiler(LONGLONG logInterval) {
	_logInterval = logInterval;
	QueryPerformanceFrequency(reinterpret_cast<LARGE_INTEGER*>(&_sysClockFrequencyInt));
	_sysClockFrequency = static_cast<DOUBLE>(_sysClockFrequencyInt);
	QueryPerformanceCounter(reinterpret_cast<LARGE_INTEGER*>(&_lastLogTime));
	_frameCount = 0;
	_initComplete = false;
}
void Profiler::Tick() {
	if (!_initComplete) {
		LONGLONG timeNow;
		QueryPerformanceCounter(reinterpret_cast<LARGE_INTEGER*>(&timeNow));

		LONGLONG initTicks = timeNow - _lastLogTime;
		DOUBLE initSeconds = static_cast<DOUBLE>(initTicks) / _sysClockFrequency;

		std::cout << "PROFILER: Initialized completed in " << initSeconds << " seconds " << initTicks << " ticks." << std::endl;

		_initComplete = true;

		QueryPerformanceCounter(reinterpret_cast<LARGE_INTEGER*>(&_lastLogTime));
	}
	else if (_frameCount >= _logInterval) {
		LONGLONG timeNow;
		QueryPerformanceCounter(reinterpret_cast<LARGE_INTEGER*>(&timeNow));

		LONGLONG elapsedTicks = timeNow - _lastLogTime;
		LONGLONG TPF = elapsedTicks / _frameCount;
		LONGLONG FPS = (_frameCount * _sysClockFrequencyInt) / elapsedTicks;

		std::cout << "PROFILER: " << FPS << " FPS " << TPF << " TPF." << std::endl;

		QueryPerformanceCounter(reinterpret_cast<LARGE_INTEGER*>(&_lastLogTime));
		_frameCount = 0;
	}
	else {
		_frameCount++;
	}
}
Profiler::~Profiler() {
	_logInterval = 0;
	_sysClockFrequencyInt = 0;
	_sysClockFrequency = 0.0;
	_lastLogTime = 0;
	_frameCount = 0;
	_initComplete = false;
}