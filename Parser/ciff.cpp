#include "ciff.h"

using namespace std;

// Warning	C26455	Default constructor may not throw.Declare it 'noexcept' (f.6).Parser	C : \Users\roli\Source\Repos\SzgbiztonsagHF\Parser\ciff.cpp	5
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
	caption = vector<char>(0);
	tags = vector<vector<char>>(0);
	pixels = vector<vector<RGB>>(0);
};

ciff::ciff(ifstream& f) {
	f.read(&magic[0], sizeof(char));
	f.read(&magic[1], sizeof(char));
	f.read(&magic[2], sizeof(char));
	f.read(&magic[3], sizeof(char));

	// Warning	C26493	Don't use C-style casts (type.4).	Parser	C:\Users\roli\Source\Repos\SzgbiztonsagHF\Parser\caff.cpp	20	
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&header_size, sizeof(header_size));

	// Warning	C26493	Don't use C-style casts (type.4).	Parser	C:\Users\roli\Source\Repos\SzgbiztonsagHF\Parser\caff.cpp	20	
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&content_size, sizeof(content_size));

	// Warning	C26493	Don't use C-style casts (type.4).	Parser	C:\Users\roli\Source\Repos\SzgbiztonsagHF\Parser\caff.cpp	20	
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&width, sizeof(width));

	// Warning	C26493	Don't use C-style casts (type.4).	Parser	C:\Users\roli\Source\Repos\SzgbiztonsagHF\Parser\caff.cpp	20	
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&height, sizeof(height));
	
	if (content_size != width * height * RGB_count)
		throw content_size_mismatch();

	size_t bytes_read = hdr_size_min;
	char c = 0;

	// loop to get caption, search until \n
	while (true) {
		f.get(c);
		if (f.eof())
			throw eof_in_caption();

		bytes_read++;
		if (bytes_read >= header_size)
			throw long_ciff_header();

		if (c != '\n')
			caption.push_back(c);
		else {
			caption.push_back('\0');
			break;
		}
	}

	// loop to get tags, search until end of header
	while (true) {
		f.get(c);
		if (f.eof())
			throw eof_in_tags();

		bytes_read++;
		if (bytes_read >= header_size)
			break;

		if (c != '\0')
			tags.back().push_back(c);
		else {
			tags.back().push_back('\0');
			tags.push_back(vector<char>());
		}

	}

	if (tags.back().back() != '\0')
		throw missing_tag_end();

	if (content_size > uint_max)
		throw size_trunc();

	pixels = vector<vector<RGB>>();
	for (auto i = 0; i != width; i++) {
		pixels.push_back(vector<RGB>());
		for (auto j = 0; j != height; j++) {
			pixels.back().push_back(RGB());
			f.read(&pixels.back().back().R, sizeof(char));
			f.read(&pixels.back().back().G, sizeof(char));
			f.read(&pixels.back().back().B, sizeof(char));
		}
	}

	if (f.peek() != EOF)
		throw longer_content();

}

RGB::RGB(void) noexcept {
	R = 0;
	G = 0;
	B = 0;
};
