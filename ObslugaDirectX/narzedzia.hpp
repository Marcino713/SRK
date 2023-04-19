#pragma once
#ifndef NARZEDZIA_H
#define NARZEDZIA_H

template<class Interface>
inline void WyczyscOrazZeruj(Interface** obiekt) {
	if (*obiekt != NULL) {
		(*obiekt)->Release();
		(*obiekt) = NULL;
	}
}

#endif