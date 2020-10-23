#include "ciff.h"

using namespace std;

// Warning	C26455	Default constructor may not throw.Declare it 'noexcept' (f.6).Parser
//	using allocator, so it may throw
#pragma warning(suppress: 26455)
ciff::ciff(void) {
	magic[0] = 0;
	magic[1] = 0;
	magic[2] = 0;
	magic[3] = 0;
	header_size = 0;
	content_size = 0;
	width = 0;
	height = 0;
	caption = string("");
	tags = vector<string>(0);
	pixels = vector<vector<RGB>>(0);
};

ciff::ciff(ifstream& f) {
	f.read(&magic[0], sizeof(char));
	f.read(&magic[1], sizeof(char));
	f.read(&magic[2], sizeof(char));
	f.read(&magic[3], sizeof(char));

	if (magic[0] != 'C' ||
		magic[1] != 'I' ||
		magic[2] != 'F' ||
		magic[3] != 'F')
		throw bad_magic("bad CIFF magic");

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&header_size, sizeof(header_size));

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&content_size, sizeof(content_size));

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&width, sizeof(width));

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&height, sizeof(height));
	
	if (content_size != width * height * RGB_count)
		throw content_size_mismatch("ciff content size mismatch");

	size_t bytes_read = hdr_size_min;
	char c = 0;

	// loop to get caption, search until \n
	while (true) {
		f.get(c);
		if (f.eof())
			throw eof_in_caption("eof in caption");

		bytes_read++;

		if (c != '\n')
			caption += c;
		else
			break;

		if (bytes_read >= header_size)
			throw long_ciff_header("ciff header longer then expected");
	}

	tags = vector<string>();
	tags.push_back(string(""));


	// loop to get tags, search until end of header
	while (true) {
		f.get(c);
		if (f.eof())
			throw eof_in_tags("found eof in ciff tags");

		bytes_read++;

		if (c != '\0')
			tags.back() += c;
		else
			tags.push_back(string(""));

		if (bytes_read >= header_size)
			break;


	}
	if (tags.back().compare("") != 0)
		throw missing_tag_end("last ciff tag not null-terminated");

	tags.pop_back();

	bytes_read = 0;

	pixels = vector<vector<RGB>>();
	for (auto i = 0; i != width; i++) {
		pixels.push_back(vector<RGB>());
		for (auto j = 0; j != height; j++) {
			pixels.back().push_back(RGB());
			f.read((char*)&pixels.back().back().R, sizeof(char));
			f.read((char*)&pixels.back().back().G, sizeof(char));
			f.read((char*)&pixels.back().back().B, sizeof(char));
			bytes_read += 3;
		}
	}

	if (bytes_read != content_size)
		throw content_size_mismatch("ciff content size mismatch");
}

RGB::RGB(void) noexcept {
	R = 0;
	G = 0;
	B = 0;
};
