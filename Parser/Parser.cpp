
#include "caff.h"


// Warning	C26429	Symbol 'argv' is never tested for nullness, it can be marked as not_null(f.23).Parser
//  argv cannot be null, initialized by the function calling main
// Warning	C26485	Expression 'argv': No array to pointer decay(bounds.3).Parser
//  argv is null terminated, sizeof will never be called on it & argc holds size anyway
#pragma warning(suppress: 26429 26485)
int main(int argc, const char *const *const argv)
//int main()
{
    
    int ret = SUCCESS;

    unique_ptr<caff> c = make_unique<caff>();
    
    if (argc != 3) {
        cerr << "Usage:" << endl;
        cerr << *argv << " <input file> <output dir>" << endl;
        return EARGC_MISMATCH;
    }

    // Warning	C26481	Don't use pointer arithmetic. Use span instead (bounds.1).
    //  std::span should be used (C++20) only, but bound checking is correct
    #pragma warning(suppress: 26481)
    string in_file(argv[1]);
    
    // Warning	C26481	Don't use pointer arithmetic. Use span instead (bounds.1).
    //  std::span should be used (C++20) only, but bound checking is correct
    #pragma warning(suppress: 26481)
    string out_dir(argv[2]);
    

    //string in_file = "../ParserTestFiles/1.caff";
    ifstream f;
    f.open(in_file, ios::in | ios::binary);

    if (!f.is_open()) {
        cerr << in_file << " not found" << endl;
        return EIN_NOT_FOUND;
    }

    try {
        parse_caff_file(f, c.get());
    }
    catch ( const exception & ) {
        try {
            throw;
        } catch(const invalid_id & ) {
            ret = EINVALID_ID;
        }
        catch (const bad_magic&) {
            ret = EBAD_MAGIC;
        }
        catch (const header_id_mismatch&) {
            ret = EHEAD_ID_MISMATCH;
        }
        catch (const header_size_mismatch&) {
            ret = EHEAD_SIZE_MISMATCH;
        }
        catch (const too_much_blocks&) {
            ret = ETOO_MUCH_BLOCKS;
        }
        catch (const eof_in_caption&) {
            ret = EOF_IN_CAPTION;
        }
        catch (const long_ciff_header&) {
            ret = ELONG_CIFF_HEAD;
        }
        catch (const eof_in_tags&) {
            ret = EOF_IN_TAGS;
        }
        catch (const missing_tag_end&) {
            ret = EMISSING_TAG_END;
        }
        catch (const size_trunc&) {
            ret = ESIZE_TRUNC;
        }
        catch (const content_size_mismatch&) {
            ret = ECONT_SIZE_MISMATCH;
        }
        catch (const longer_content&) {
            ret = ELONGER_CONTENT;
        }
        catch (const exception&) {
            ret = UNKNOWN_ERROR;
        }
    }

    f.close();

    if (ret != SUCCESS)
        return ret;



    try {
        c->dump_preview();
        c->dump_metadata();
    }
    catch (exception&) {
        return UNKNOWN_ERROR;
    }

//          write caff.frames[0].pixels to <name>_preview.bmp
//          write caff as json to <name>.json

    
    return SUCCESS;
}
