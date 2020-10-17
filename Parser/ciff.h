#pragma once

// header file defining the ciff structure

#include <stdint.h>
#include <vector>
#include <iostream>
#include <fstream>
#include <exception>
#include <memory>

using namespace std;

#define hdr_size_min	(sizeof(char)*4 + sizeof(uint64_t)*4)


// uint max to check before truncating on conversion
// value obtained from docs.microfost.com

#define uint_max		4294967295

class bad_magic : public exception {};
class eof_in_caption : public exception {};
class long_ciff_header : public exception {};
class eof_in_tags : public exception {};
class missing_tag_end : public exception {};
class size_trunc : public exception {};
class content_size_mismatch : public exception {};
class longer_content : public exception {};


#define RGB_count		3

struct RGB {
	char R;
	char G;
	char B;

public:
	RGB(void) noexcept;
};

struct ciff {

	char magic[4];
	uint64_t header_size;
	uint64_t content_size;
	uint64_t width;
	uint64_t height;
	vector<char> caption;
	vector<vector<char>> tags;
	vector<vector<RGB>> pixels;

public:
	ciff(void);
	ciff(ifstream& f);
};

/*
#######################################
#                                     #
#   CRYSYS IMAGE FILE FORMAT (CIFF)   #
#                                     #
#######################################


The CrySyS Image File Format is a proprietary, uncompressed image format.
The format contains a header and the uncompressed pixels of the image.

Unfortunately, all libraries and modules implementing its parsing and
display have been lost to time and faulty backups.


#################
#               #
#  CIFF HEADER  #
#               #
#################


All CIFF files start with a header. The header contains 7 major parts:
 ____________ ________________________ ________________________
|            |                        |                        |
|    magic   |       header_size      |      content_size      |
|____________|________________________|________________________|
|                        |                        |            |
|         width          |         height         | caption... |
|________________________|________________________|____________|
|                                 |
|  tags (tag1 \0 tag2 \0 ... \0)  |
|_________________________________|

	- Magic: 4 ASCII character spelling 'CIFF'

	- Header size: 8-byte-long integer, its value is the size of the header
	(all fields included), i.e. the first header_size number of bytes in the
	file make up the whole header.

	- Content size: 8-byte long integer, its value is the size of the image
	pixels located at the end of the file. Its value must be width*heigth*3.

	- Width: 8-byte-long integer giving the width of the image. Its value can
	be zero, however, no pixels must be present in the file in that case.

	- Height: 8-byte-long integer giving the height of the image. Its value can
	be zero, however, no pixels must be present in the file in that case.

	- Caption: Variable-length ASCII encoded string ending with \n. It is the
	caption of the image. As \n is a special character for the file format, the
	caption cannot contains this character.

	- Tags: Variable number of variable-length ASCII encoded strings, each
	separated by \0 characters. The strings themselves must not be multiline.
	There must be a \0 character after the last tag as well.


##################
#                #
#  CIFF CONTENT  #
#                #
##################

 ________________
|                |
|  pixels  ...   |
|________________|

The header is followed by the actual pixels of the image in RGB format, with
each component taking up 1 byte. This part of the CIFF file must contain
exactly content_size number of bytes.

*/
