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
		throw bad_magic("bad CAFF magic");

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
	name = string("");
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

	name = string("");
	for (int i = 0; i != creator_len; i++)
		name += f.get();

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
	have_creds = false;
	creds = caff_creds();
	frames = vector<frame>();
};


void caff::dump_preview(string & out_file) noexcept(false) {

	bitmap_image preview(frames[0].img.get()->width,frames[0].img.get()->height);

	// iterate through pixels & write RGB pixels (BMP uses the order BGR)
	for (int i = 0; i != frames[0].img.get()->pixels.size(); i++) {
		for (int j = 0; j != frames[0].img.get()->pixels[i].size(); j++) {
			preview.set_pixel(i, j,
				frames[0].img.get()->pixels[i][j].R,
				frames[0].img.get()->pixels[i][j].G,
				frames[0].img.get()->pixels[i][j].B);
		}
	}

	preview.save_image(out_file);

};


void caff::dump_metadata(string & out_file) {
	ofstream f;
	f.open(out_file);
	if (!f.is_open())
		throw out_open_fail("failed to open metadata file");

	f << "{" << endl;
	f << "\t" << "\"anim_count\": " << head.num_anim << "," << endl;
	if (have_creds) {
		f << "\t" << "\"date\": {" << endl;
		f << "\t\t" << "\"year\": " << creds.date.year << "," << endl;
		f << "\t\t" << "\"month\": " << static_cast<unsigned>(creds.date.month) << "," << endl;
		f << "\t\t" << "\"day\": " << static_cast<unsigned>(creds.date.day) << "," << endl;
		f << "\t\t" << "\"hour\": " << static_cast<unsigned>(creds.date.hour) << "," << endl;
		f << "\t\t" << "\"minute\": " << static_cast<unsigned>(creds.date.minute) << endl;
		f << "\t" << "}," << endl;
		f << "\t" << "\"creator\": \"" << creds.name << "\"," << endl;
	}

	f << "\t" << "\"frames\": [" << endl;
	for (int i = 0; i != frames.size(); i++) {
		f << "\t\t" << "{" << endl;
		f << "\t\t\t" << "\"duration\": " << frames[i].duration << "," << endl;
		f << "\t\t\t" << "\"image\": {" << endl;

		f << "\t\t\t\t" << "\"width\": " << frames[i].img.get()->width << "," << endl;
		f << "\t\t\t\t" << "\"height\": " << frames[i].img.get()->height << "," << endl;
		f << "\t\t\t\t" << "\"caption\": \"" << frames[i].img.get()->caption << "\"," << endl;

		f << "\t\t\t\t" << "\"tags\": [" << endl;
		for (int j = 0; j != frames[i].img.get()->tags.size(); j++) {
			f << "\t\t\t\t\t" << "\"" << frames[i].img.get()->tags[j] << "\"";
			if (j != frames[i].img.get()->tags.size() - 1)
				f << ",";
			f << endl;
		}
		f << "\t\t\t\t" << "]" << endl;

		f << "\t\t\t" << "}" << endl;

		if (i != frames.size() - 1)
			f << "\t\t" << "}," << endl;
		else 
			f << "\t\t" << "}" << endl;
	}
	f << "\t" << "]" << endl;

	f << "}" << endl;

};


void parse_caff_file(ifstream &f, caff *c) noexcept (false)
{

	uint8_t id = 0;
	uint64_t length = 0;
	uint64_t num_anim_var = 0;

	int tmp = 0;

	tmp = f.tellg();

	f >> id;

	tmp = f.tellg();

	if (id != HEADER)
		throw header_id_mismatch("caff header id not matching");

	// Warning	C26493	Don't use C-style casts (type.4).
	//	this is the most reliable way to read into variables, >> fails
	#pragma warning(suppress: 26493)
	f.read((char*)&length, sizeof(length));

	tmp = f.tellg();

	c->head = caff_header(f);

	tmp = f.tellg();

	// length is parsed from the block
	if(length != caff_hdr_size)
		throw header_size_mismatch("caff header size mismatch");
	// header_size is parsed from the header itself
	if (c->head.size != length)
		throw header_size_mismatch("caff header size mismatch");

	int cpos = 0;
	int npos = 0;

	while (!f.eof()) {

		// exceeded number of data blocks specified in header & the creds
		// creds thought to be optional, location not specified, could be last block
		if (c->have_creds && c->head.num_anim == num_anim_var)
			break;
//			throw too_much_blocks("number of animation blocks exceeded");

		f >> id;


		// Warning	C26493	Don't use C-style casts (type.4).
		//	this is the most reliable way to read into variables, >> fails
		#pragma warning(suppress: 26493)
		f.read((char*)&length, sizeof(length));


		switch (id)
		{
			case CREDIT:
				c->have_creds = true;

				cpos = f.tellg();
				c->creds = caff_creds(f);

				npos = f.tellg();
				if (length != npos - cpos)
					throw block_size_miscmatch("credits block too long");

				break;

			case ANIMATION:
				num_anim_var++;

				cpos = f.tellg();
				c->frames.push_back(frame(f));

				npos = f.tellg();
				if (length != npos - cpos)
					throw block_size_miscmatch("animation block too long");

				break;

			default:
				throw invalid_id("invalid block id");
		}

	}
}