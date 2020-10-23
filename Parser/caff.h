#pragma once

// header file defining the caff structure

#include "ciff.h"
#include "bitmap_image.hpp"

// block types

#define HEADER					0x1
#define CREDIT					0x2
#define ANIMATION				0x3

// sizeof(caff_header) != 20, probably because of constructors
#define caff_hdr_size			20

#define bmp_hdr_size			14
#define bmp_info_size			40

#define bmp_res_vert			400
#define bmp_res_hor				300

struct caff_header {
	char magic[4];
	uint64_t size;
	uint64_t num_anim;

public:
	caff_header(void) noexcept(true);
	caff_header(ifstream & f);
};


struct caff_creds {
	struct caff_data {
		uint16_t year;
		uint8_t month;
		uint8_t day;
		uint8_t hour;
		uint8_t minute;
	} date;
	uint64_t creator_len;
	string name;

public:
	caff_creds(void) noexcept(true);
	caff_creds(ifstream& f);
};

struct frame {
	uint64_t duration;
	unique_ptr<ciff> img;

public:
	frame(void) noexcept;
	frame(ifstream& f);
};

struct caff {
	caff_header head;
	bool have_creds;
	caff_creds creds;
	vector<frame> frames;

public:
	caff(void) noexcept(true);
	void dump_preview(string&) noexcept(false);
	void dump_metadata(string&);
};


void parse_caff_file(ifstream & f, caff * c) noexcept (false);


/*

###########################################
#                                         #
#   CRYSYS ANIMATION FILE FORMAT (CAFF)   #
#                                         #
###########################################

The CrySyS Animation File Format is a proprietary file format for animating
CIFF images. CAFF was intended to be a competitor to GIF. Unfortunately, the
development team could not finish the parser in time, thus, GIF
conquered the Internet.

The format contains a number of blocks in the following format:
 __ _________________________ _______________________________
|   |                        |                               |
|ID |         length         |          data  ...            |
|___|________________________|_______________________________|

	- ID: 1-byte number which identifies the type of the block:
		0x1 - header
		0x2 - credits
		0x3 - animation

	- Length: 8-byte-long integer giving the length of the block.

	- Data: This section is length bytes long and contain the data of the block.


#################
#               #
#  CAFF HEADER  #
#               #
#################


The first block of all CAFF files is the CAFF header. It contains the following
parts:
 ____________ ________________________ ________________________
|            |                        |                        |
|   magic    |       header_size      |        num_anim        |
|____________|________________________|________________________|

	- Magic: 4 ASCII character spelling 'CAFF'

	- Header size: 8-byte-long integer, its value is the size of the header
	(all fields included).

	- Number of animated CIFFs: 8-byte long integer, gives the number of CIFF
	animation blocks in the CAFF file.


##################
#                #
#  CAFF CREDITS  #
#                #
##################


The CAFF credits block specifies the creation date and time, as well as the
creator of the CAFF file.
 ______ ___ ___ ___ ___ ________________________ ________________
|      |   |   |   |   |                        |                |
|  YY  | M | D | h | m |      creator_len       |  creator  ...  |
|______|___|___|___|___|________________________|________________|

	- Creation date and time: the year, month, day, hour and minute of the CAFF
	file's creation:
		Y - year (2 bytes)
		M - month (1 byte)
		D - day (1 byte)
		h - hour (1 byte)
		m - minute (1 byte2

	- Length of creator: 8-byte-long integer, the length of the field
	specifying the creator.

	- Creator: Variable-length ASCII string, the creator of the CAFF file.


####################
#                  #
#  CAFF ANIMATION  #
#                  #
####################


The CAFF animation block contains a CIFF image to be animated. The block has
the following fields:
 ________________ ______________
|                |              |
|    duration    |   CIFF  ...  |
|________________|______________|

	- Duration: 8-byte-long integer, miliseconds for which the CIFF image must
	be displayed during animation.

	- CIFF: the image to be displayed in CIFF format.

*/