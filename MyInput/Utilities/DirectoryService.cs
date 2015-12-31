using System;
using System.Collections.Generic;
using System.Text;

namespace MyInput.Utilities
{
    class DirectoryService
    {
        public static string RetrieveFileName(string path,bool stripeExt)
        {
            path = path.Substring(path.LastIndexOf("\\")+1);
            if(stripeExt) path = path.Substring(0, path.LastIndexOf("."));
            return path;
        }
    }
}
