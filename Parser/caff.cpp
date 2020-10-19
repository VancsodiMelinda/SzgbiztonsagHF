#include "caff.h"

using namespace std;

caff_header::caff_header(void) noexcept(true) {
	magic[0] = 0;
	magic[1] = 0;
	magic[2] = 0;
	magic[3] = 0;
	size = 0;
	num_anim = 0;
};

caff_header::caff_header(ifstream& f) {
	f.read(&magic[0], sizeof(char));
	f.read(&magic[1], sizeof(char));
	f.read(&magic[2], sizeof(char));
	f.read(&magic[3], sizeof(char));
	
	if (magic[0] != 'C' ||
		magic[1] != 'A' ||
		magic[2] != 'F' ||
		magic[3] != 'F')
		throw bad_magic();

	// Warning	C26493	Don't use C-style casts (type.4).	
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&size, sizeof(size));

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&num_anim, sizeof(num_anim));
}


caff_creds::caff_creds(void) noexcept(true) {
	date.year = 0;
	date.month = 0;
	date.day = 0;
	date.hour = 0;
	date.minute = 0;
	creator_len = 0;
	name = nullptr;
};

caff_creds::caff_creds(ifstream& f) {
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&date.year, sizeof(date.year));
	
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&date.month, sizeof(date.month));
	
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&date.day, sizeof(date.day));
	
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&date.hour, sizeof(date.hour));
	
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&date.minute, sizeof(date.minute));
	
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&creator_len, sizeof(creator_len));

	if (creator_len > uint_max)
		throw size_trunc();

	// Warning	C26409	Avoid calling newand delete explicitly, use std::make_unique<T> instead(r.11).Parser
	//	not calling new directly, using unique_ptr
	// Warning	C4244	'initializing': conversion from 'uint64_t' to 'unsigned int', possible loss of data	Parser
	//	we found no way yet to pass a value larger then UINT_MAX to allocation features, but we check and signal truncations
	#pragma warning(suppress: 26409 4244)
	name = unique_ptr<char[]>(new char[creator_len]);
	f.read(name.get(), creator_len);
};


frame::frame(void) noexcept {
	duration = 0;
	img = nullptr;
};

frame::frame(ifstream& f) {
	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&duration, sizeof(duration));

	img = make_unique<ciff>(f);
};


caff::caff(void) noexcept(true) {
	head = caff_header();
	creds = caff_creds();
	frames = vector<frame>();
};

void caff::dump_preview(void) {
	// TODO
};

void caff::dump_metadata(void) {
	// TODO
};


void parse_caff_file(ifstream &f, caff *c) noexcept (false)
{

	uint8_t id = 0;
	uint64_t length = 0;
	uint64_t num_anim_var = 0;

	bool have_cred = false;

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&id, sizeof(id));

	if (id != HEADER)
		throw header_id_mismatch();

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&length, sizeof(length));

	c->head = caff_header(f);

	// length is parsed from the block
	if(length != caff_hdr_size)
		throw header_size_mismatch();
	// header_size is parsed from the header itself
	if (c->head.size != length)
		throw header_size_mismatch();

	// should be while (!f.eof())
	while (f.eof()) {
		// should read new ID
		// should read length

		// exceeded number of data blocks specified in header & the creds
		// creds thought to be optional, location not specified, could be last block
		if (have_cred && c->head.num_anim == num_anim_var)
			throw too_much_blocks();

		switch (id)
		{
			case CREDIT:
				have_cred = true;
				c->creds = caff_creds(f);
				break;

			case ANIMATION:
				num_anim_var++;
				c->frames.push_back(frame(f));
				break;

			default:
				throw invalid_id();
		}

	}
}