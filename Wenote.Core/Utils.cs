using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenote.Core {
    public class Utils {
        public static string ComposeUrl(string url, string path) {
            if (url.EndsWith('/'))
                url = url[..^1];
            if (!path.StartsWith('/'))
                url = '/' + path;
            return url + path;
        }
    }
}
