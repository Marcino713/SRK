#include "obslugaDirect2D.hpp"

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved) {
	switch (ul_reason_for_call) {
	case DLL_PROCESS_ATTACH:
		if (FabrykaDirect2D == NULL) {
			D2D1CreateFactory(D2D1_FACTORY_TYPE_SINGLE_THREADED, &FabrykaDirect2D);
		}

		if (FabrykaDirectWrite == NULL) {
			DWriteCreateFactory(DWRITE_FACTORY_TYPE_SHARED, __uuidof(FabrykaDirectWrite), reinterpret_cast<IUnknown**>(&FabrykaDirectWrite));
		}
		break;

	case DLL_PROCESS_DETACH:
		SafeRelease(&FabrykaDirect2D);
		SafeRelease(&FabrykaDirectWrite);
		break;
	}

	return TRUE;
}