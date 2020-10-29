
#include "caff.h"

unordered_map<type_index, errors> error_map;

void init_error_map() {
    error_map[type_index(typeid(invalid_id))] = EINVALID_ID;
    error_map[type_index(typeid(header_id_mismatch))] = EHEAD_ID_MISMATCH;
    error_map[type_index(typeid(header_size_mismatch))] = EHEAD_SIZE_MISMATCH;
    error_map[type_index(typeid(too_much_blocks))] = ETOO_MUCH_BLOCKS;
    error_map[type_index(typeid(bad_magic))] = EBAD_MAGIC;
    error_map[type_index(typeid(eof_in_caption))] = EOF_IN_CAPTION;
    error_map[type_index(typeid(long_ciff_header))] = ELONG_CIFF_HEAD;
    error_map[type_index(typeid(eof_in_tags))] = EOF_IN_TAGS;
    error_map[type_index(typeid(missing_tag_end))] = EMISSING_TAG_END;
    error_map[type_index(typeid(size_trunc))] = ESIZE_TRUNC;
    error_map[type_index(typeid(content_size_mismatch))] = ECONT_SIZE_MISMATCH;
    error_map[type_index(typeid(longer_content))] = ELONGER_CONTENT;
    error_map[type_index(typeid(out_open_fail))] = EOUT_OPEN_FAIL;
    error_map[type_index(typeid(block_size_mismatch))] = EBS_MISMATCH;
    error_map[type_index(typeid(ciff_header_size_mismatch))] = ECIFF_HEAD_SIZE_MISMATCH;
    error_map[type_index(typeid(out_open_fail))] = EOUT_OPEN_FAIL;
}


// Warning	C26429	Symbol 'argv' is never tested for nullness, it can be marked as not_null(f.23).Parser
//  argv cannot be null, initialized by the function calling main
// Warning	C26485	Expression 'argv': No array to pointer decay(bounds.3).Parser
//  argv is null terminated, sizeof will never be called on it & argc holds size anyway
#pragma warning(suppress: 26429 26485)
int main(int argc, const char *const *const argv)
//int main()
{

    init_error_map();

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
    

    if (out_dir.back() != '\\' && out_dir.back() != '/') {
        cerr << "trailing dir separator missing from output directory" << endl;
        return ENO_TRAILING_SEP;
    }

    const size_t last_slash = in_file.find_last_of("/\\");
    const size_t extension = in_file.find_last_of('.');

    string f_name = in_file.substr(last_slash + 1, extension - last_slash - 1);

    if (f_name.find('.') != string::npos) {
        cerr << "filename shall not contain a . character" << endl;
        return EDOT_FOUND;
    }

    string preview_path = out_dir + f_name + string("_preview.bmp");
    string metadata_path = out_dir + f_name + string(".json");
    

    //string in_file = "../ParserTestFiles/1.caff";
    ifstream f;
    f.open(in_file, ios::in | ios::binary);

    if(!f.is_open()) {
        cerr << in_file << " not found" << endl;
        return EIN_NOT_FOUND;
    }

    try {
        parse_caff_file(f, c.get());
    }
    catch ( const exception & e) {
        if (error_map.count(type_index(typeid(e))))
            ret = error_map.at(type_index(typeid(e)));
        else
            ret = UNKNOWN_ERROR;
    }

    f.close();

    
    if (ret != SUCCESS)
        return ret;

    try {
        c->dump_preview(preview_path);
        c->dump_metadata(metadata_path);
    }
    catch (exception& e) {
        if (error_map.count(type_index(typeid(e))))
            ret = error_map.at(type_index(typeid(e)));
        else
            ret = UNKNOWN_ERROR;
    }
    
    
    return ret;
}
