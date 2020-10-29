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


		TEST_METHOD(bad_ciff_magic)
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

	};
}
