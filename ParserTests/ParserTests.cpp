#include "pch.h"
#include "CppUnitTest.h"

#include "../Parser/caff.h"
#include "../Parser/ciff.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace ParserTests
{
	TEST_CLASS(ParserTests)
	{
	public:
		
		TEST_METHOD(caff1_parse)
		{
			char* in_file = "1.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			parse_caff_file(f, c.get());
			c->dump_preview(string("1_preview.bmp"));
			c->dump_metadata(string("1_meta.json"));

		}

		TEST_METHOD(caff2_parse)
		{
			char* in_file = "2.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			parse_caff_file(f, c.get());
			c->dump_preview(string("2_preview.bmp"));
			
		}

		TEST_METHOD(caff3_parse)
		{
			char* in_file = "3.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try {
				parse_caff_file(f, c.get());
				c->dump_preview(string("3_preview.bmp"));
			}
			catch (const invalid_id& ) {
				exceptionThrown = true;
			}

			if (!exceptionThrown) {
				Assert::Fail(L"expected to throw invalid_id exception");
			}

		}

		TEST_METHOD(small_parse)
		{
			char* in_file = "small_3.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			parse_caff_file(f, c.get());
			c->dump_preview(string("small_preview.bmp"));

		}

	};

	TEST_CLASS(CaffTests)
	{
	public:
		TEST_METHOD(bad_caff_magic)
		{
			char* in_file = "bad_caff_magic.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (bad_magic&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw bad_magic exception");
			}
		}

		TEST_METHOD(bad_caff_header_id)
		{
			char* in_file = "bad_caff_header_id.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (header_id_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw header_id_mismatch exception");
			}
		}

		TEST_METHOD(bad_caff_credits_id)
		{
			char* in_file = "bad_caff_credits_id.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (invalid_id&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw invalid_id exception");
			}
		}

		TEST_METHOD(bad_caff_animation_id)
		{
			char* in_file = "bad_caff_animation_id.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (invalid_id&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw invalid_id exception");
			}
		}

		TEST_METHOD(bad_caff_header_length)
		{
			char* in_file = "bad_caff_header_length.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (header_size_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw header_size_mismatch exception");
			}
		}

		TEST_METHOD(bad_caff_credits_length)
		{
			char* in_file = "bad_caff_credits_length.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (block_size_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw block_size_mismatch exception");
			}
		}

		TEST_METHOD(bad_caff_header_num_anim)
		{
			char* in_file = "bad_caff_header_num_anim.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (too_much_blocks&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw too_much_blocks exception");
			}
		}

		TEST_METHOD(bad_caff_animation_length)
		{
			char* in_file = "bad_caff_animation_length.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (block_size_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw block_size_mismatch exception");
			}
		}

		/*
		TEST_METHOD(bad_caff_multiple_caff_credits)
		{
			char* in_file = "bad_caff_multiple_caff_credits.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			try
			{
				parse_caff_file(f, c.get());
				std::cout << "NO exception was thrown";
				Assert::Fail(L"was expecting an exception");
			}
			catch (...){}
		}
		*/
	};

	TEST_CLASS(CiffTests)
	{
		TEST_METHOD(bad_ciff_magic)
		{
			char* in_file = "bad_ciff_magic.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (bad_magic&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw bad_magic exception");
			}
		}

		TEST_METHOD(bad_ciff_magic2)
		{
			char* in_file = "bad_ciff_magic2.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (bad_magic&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw bad_magic exception");
			}
		}

		TEST_METHOD(bad_ciff_content_size)
		{
			char* in_file = "bad_ciff_content_size.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (content_size_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw content_size_mismatch exception");
			}
		}

		TEST_METHOD(bad_ciff_width)
		{
			char* in_file = "bad_ciff_width.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (content_size_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw content_size_mismatch exception");
			}
		}

		TEST_METHOD(bad_ciff_height)
		{
			char* in_file = "bad_ciff_height.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (content_size_mismatch&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw content_size_mismatch exception");
			}
		}

		TEST_METHOD(bad_ciff_caption)
		{
			char* in_file = "bad_ciff_caption.caff";  // no \n
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (long_ciff_header&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw long_ciff_header exception");
			}
		}

		// ciff header is smaller than header_size
		/*
		TEST_METHOD(bad_ciff_caption2)
		{
			char* in_file = "bad_ciff_caption2.caff";  // no \n
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (long_ciff_header&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw long_ciff_header exception");
			}
		}
		*/

		TEST_METHOD(bad_ciff_tags)
		{
			char* in_file = "bad_ciff_tags.caff";  // no \n
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			bool exceptionThrown = false;

			try
			{
				parse_caff_file(f, c.get());
			}
			catch (missing_tag_end&) // special exception type
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
			{
				Assert::Fail(L"expected to throw missing_tag_end exception");
			}
		}

		/*
		TEST_METHOD(bad_pixel_data)
		{
			char* in_file = "bad_pixel_data.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			try
			{
				parse_caff_file(f, c.get());
				//Assert::Fail(L"was expecting an exception");
			}
			catch (content_size_mismatch&)
			{
				Assert::Fail(L"good excepton was throw");
			}
		}
		*/
		
	};
}
