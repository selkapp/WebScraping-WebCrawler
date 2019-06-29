using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TarifDefteri.Models
{
    public class SearchResult
    {
        public string ID { get; private set; }
        public int? RANK { get; private set; }
        public string RESIM { get; private set; }
        public string SITE_ADI { get; private set; }
        public string URL { get; private set; }
        public string YEMEK_ADI { get; private set; }
        public string OZET { get; private set; }
        public string KATEGORI { get; private set; }

        public static List<SearchResult> GetSearchResults(List<sp_getSearchResults_Result> result, List<string> anahtar_kelimeler)
        {
            List<SearchResult> return_val = new List<SearchResult>();
            string OZET = "";
            int index = -1 , _index = -1;
            foreach (var item in result)
            {
                OZET = (item.OZET != null ? item.OZET : "").Replace("|", " ");
                try
                {

                    foreach (var ak in anahtar_kelimeler.OrderByDescending(c => c).ToList())
                    {
                        _index = OZET.IndexOf(ak);
                        if (_index != -1 && _index > (400 - ak.Length - 7) && (index == -1 || index > _index))
                        {
                            if (index == -1) index = _index;
                            else index = Math.Min(index, _index);
                        }
                    }
                    if (index > 0)
                    {
                        string _OZET = OZET.Substring(0, index);

                        if (_OZET.IndexOf(".") != -1)
                        {
                            index = _OZET.LastIndexOf(".") + 1;
                        }
                        else if (_OZET.IndexOf(" ") != -1)
                        {
                            index = _OZET.LastIndexOf(" ") + 1;
                        }

                        OZET = OZET.Substring(index, OZET.Length - index);
                    }

                    OZET = (OZET.Length > 400 ? OZET.Substring(0, 400) : OZET);
                }
                catch { }
                
                foreach (var ak in anahtar_kelimeler.OrderByDescending(c => c).ToList())
                {
                    if (anahtar_kelimeler.Find(c => c != ak && c.Contains(ak)) == null)
                    {
                        OZET = OZET.Replace(ak, "<b>" + ak + "</b>");
                    }
                }
                return_val.Add(new SearchResult
                {
                    ID = item.ID,
                    RANK = item.RANK,
                    RESIM = item.RESIM,
                    SITE_ADI = item.SITE_ADI,
                    URL = item.URL,
                    YEMEK_ADI = item.YEMEK_ADI,
                    KATEGORI = item.KATEGORI,
                    OZET = OZET
                });
            }
            return return_val;
        }
    }
}