avr-gcc -mmcu=atmega8 -Wall -O3 -c ${1}.c -o bin/${1}.o
avr-gcc -mmcu=atmega8 -Wall -O3 -c obslugaUart.c -o bin/obslugaUart.o
avr-gcc -mmcu=atmega8 -Wall -O3 -c obslugaKonfiguracji.c -o bin/obslugaKonfiguracji.o
avr-gcc -mmcu=atmega8 -o bin/${1}.elf bin/${1}.o bin/obslugaUart.o bin/obslugaKonfiguracji.o
avr-size bin/${1}.elf
avr-objcopy -O ihex bin/${1}.elf bin/${1}.hex
