#include "pch.h"
#include "CppUnitTest.h"

#include "../Parser/caff.h"


using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace ParserTests
{
	TEST_CLASS(ParserTests)
	{
	public:
		
		TEST_METHOD(test_caff1)
		{
			char* in_file = "1.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			try {
				parse_caff_file(f, c.get());
			}
			catch (const exception& e) {
				size_t ret = 0;

				const char* what = typeid(e).name();
				const size_t s = strlen(what) + 1;
				wchar_t* wc = new wchar_t[s];
				mbstowcs_s(&ret, wc, s, what, s - 1);
				Assert::Fail(wc);
			}
		}

		TEST_METHOD(test_caff2)
		{
			char* in_file = "2.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			try {
				parse_caff_file(f, c.get());
			}
			catch (const exception& e) {
				size_t ret = 0;

				const char* what = typeid(e).name();
				const size_t s = strlen(what) + 1;
				wchar_t* wc = new wchar_t[s];
				mbstowcs_s(&ret, wc, s, what, s - 1);
				Assert::Fail(wc);
			}
		}

		TEST_METHOD(test_caff3)
		{
			char* in_file = "3.caff";
			unique_ptr<caff> c = make_unique<caff>();
			ifstream f;
			f.open(in_file, ios::in | ios::binary);

			if (!f.is_open()) {
				Assert::Fail(L"file not found");
			}

			try {
				parse_caff_file(f, c.get());
			}
			catch (const exception& e) {
				size_t ret = 0;

				const char* what = typeid(e).name();
				const size_t s = strlen(what) + 1;
				wchar_t* wc = new wchar_t[s];
				mbstowcs_s(&ret, wc, s, what, s - 1);
				Assert::Fail(wc);
			}
		}

	};
}
