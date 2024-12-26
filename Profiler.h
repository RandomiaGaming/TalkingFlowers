#pragma once
#include <Windows.h>
#include <iostream>

class Profiler {
public:
	Profiler(LONGLONG logInterval);
	void Tick();
	~Profiler();

private:
	LONGLONG _logInterval;
	LONGLONG _sysClockFrequencyInt;
	DOUBLE _sysClockFrequency;
	LONGLONG _lastLogTime;
	LONGLONG _frameCount;
	BOOL _initComplete;
};