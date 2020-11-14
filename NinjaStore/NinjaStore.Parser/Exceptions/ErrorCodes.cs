﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Parser.Exceptions
{
	// TODO Gergo: create exception for each error code

	public enum ErrorCodes
	{
		UNKNOWN_ERROR = -1,
		SUCCESS = 0,
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
		EMULTIPLE_CREDITS,
		EEMPTY_FRAME
	}
}
