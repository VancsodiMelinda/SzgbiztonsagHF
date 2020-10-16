# Makefile for compiling the parser on Unix/Linux systems
# On Windows, import it as a Visual Studio solution

CC ?= g++

AFLCC = 

NAME = caff_parser

SRCS = Parser.cpp caff.cpp ciff.cpp

LDFLAGS = -lstdc++

all:
	$(CC) $(WARNS) -o $(NAME) -g $(SRCS) $(LDFLAGS)
clean:
	rm $(NAME)

afl-asan:
# ASAN needs around 17,5 preallocated memory on x64, so let's compile it to x86
	AFL_USE_ASAN=1 $(AFLCC) $(WARNS) -o $(NAME) -g $(SRCS) $(LDFLAGS)