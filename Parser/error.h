#pragma once

#include <exception>
#include <unordered_map>
#include <typeindex>
#include <typeinfo>

using namespace std;

// Exceptions

class invalid_id : public runtime_error { public: invalid_id(const char* w) : runtime_error(w) {} };
class header_id_mismatch : public runtime_error { public: header_id_mismatch(const char* w) : runtime_error(w) {} };
class header_size_mismatch : public runtime_error { public: header_size_mismatch(const char* w) : runtime_error(w) {} };
class too_much_blocks : public runtime_error { public: too_much_blocks(const char* w) : runtime_error(w) {} };
class bad_magic : public runtime_error { public: bad_magic(const char* w) : runtime_error(w) {} };
class eof_in_caption : public runtime_error { public: eof_in_caption(const char* w) : runtime_error(w) {} };
class long_ciff_header : public runtime_error { public: long_ciff_header(const char* w) : runtime_error(w) {} };
class eof_in_tags : public runtime_error { public: eof_in_tags(const char* w) : runtime_error(w) {} };
class missing_tag_end : public runtime_error { public: missing_tag_end(const char* w) : runtime_error(w) {} };
class size_trunc : public runtime_error { public: size_trunc(const char* w) : runtime_error(w) {} };
class content_size_mismatch : public runtime_error { public: content_size_mismatch(const char* w) : runtime_error(w) {} };
class longer_content : public runtime_error { public: longer_content(const char* w) : runtime_error(w) {} };
class out_open_fail : public runtime_error { public: out_open_fail(const char* w) : runtime_error(w) {} };
class block_size_mismatch : public runtime_error { public: block_size_mismatch(const char* w) : runtime_error(w) {} };
class ciff_header_size_mismatch : public runtime_error { public: ciff_header_size_mismatch(const char* w) : runtime_error(w) {} };
class multiple_credits : public runtime_error { public: multiple_credits(const char* w) : runtime_error(w) {} };


// return codes

enum errors {
	UNKNOWN_ERROR = -1,
	SUCCESS	= 0,
	EARGC_MISMATCH,
	ENO_TRAILING_SEP,
	EDOT_FOUND,
	EIN_NOT_FOUND,
	EINVALID_ID,
	EBAD_MAGIC,
	EHEAD_ID_MISMATCH,
	EHEAD_SIZE_MISMATCH,
	ETOO_MUCH_BLOCKS,
	EOF_IN_CAPTION,
	ELONG_CIFF_HEAD,
	EOF_IN_TAGS,
	EMISSING_TAG_END,
	ESIZE_TRUNC,
	ECONT_SIZE_MISMATCH,
	ELONGER_CONTENT,
	EOUT_OPEN_FAIL,
	EBS_MISMATCH,
	ECIFF_HEAD_SIZE_MISMATCH,
	EMULTIPLE_CREDITS
};
