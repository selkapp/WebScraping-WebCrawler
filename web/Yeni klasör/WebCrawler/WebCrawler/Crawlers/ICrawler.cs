using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    interface ICrawler
    {
        TARIF extractTarifFromHtml(string rstring, string url);
        string getStartUrl();
        string getUrl(string v);
        void extractURLs();
        // string appendString(string val, List<string> enumerable);
    }
}
