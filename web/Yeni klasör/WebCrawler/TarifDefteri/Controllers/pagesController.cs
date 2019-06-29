using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TarifDefteri.Models;

namespace TarifDefteri.Controllers
{
    public class pagesController : Controller
    {
        // GET: pages
        public ActionResult searchEnginePage()
        {
            return View();
        }

        public ActionResult searchResultPage(string search, string page,string type)
        {
            if ((search != null ? search : "") != "")
            {
                DateTime start = System.DateTime.Now;

                int sayfa = 1;
                try { sayfa = Convert.ToInt32((page != null ? page : "")); }
                catch { sayfa = 1; }
                if (sayfa < 1) { sayfa = 1; }

                List<string> anahtar_kelimeler = new List<string>();
                string _search = search.Replace("-", " ")
                    .Replace(",", " ")
                    .Replace(":", " ")
                    .Replace("+", " ")
                    .Replace("_", " ")
                    .Replace("/", " ")
                    .Replace("(", " ")
                    .Replace(")", " ")
                    .Replace("&", " ")
                    .Replace("*", " ");
                foreach (var item in _search.Split(' ')) anahtar_kelimeler.Add(item);

                int search_type;

                if (type == "tumu") search_type = 0;
                else if (type == "kategori") search_type = 1;
                else if (type == "site") search_type = 2;
                else { search_type = 0; type = "tumu"; }

                YemekTarifiEntities db = new YemekTarifiEntities();
                var results = db.sp_getSearchResults(search, search_type).ToList();
                int total_result = results.Count;
                int page_count = total_result / 10;
                if (results.Count % 10 != 0) page_count = page_count + 1;
                if (page_count < sayfa) sayfa = page_count;

                results = results.OrderByDescending(c => c.RANK).Skip((sayfa - 1) * 10).Take(10).ToList();
                
                DateTime end = System.DateTime.Now;

                ViewBag.total_result = total_result;
                ViewBag.searchResults = SearchResult.GetSearchResults(results, anahtar_kelimeler).ToList();
                ViewBag.search = search;
                ViewBag.page = sayfa;
                ViewBag.page_count = page_count;
                ViewBag.page_duration = (end - start).TotalSeconds;
                ViewBag.type = type;


                return View();
            }
            else
            {
                return RedirectToAction("searchEnginePage", "pages");
            }
        }

        public ActionResult searchResultDetailPage(string id, string search, string type)
        {
            using (YemekTarifiEntities db = new YemekTarifiEntities())
            {
                TARIF tarif = null;
                tarif = db.TARIF.FirstOrDefault(c => c.ID == id); 
                if(tarif!=null)
                {
                    if (type != "tumu" && type != "kategori" && type != "site") type = "tumu";

                    ViewBag.tarif = tarif;
                    ViewBag.type = type;
                    ViewBag.search = search;

                    return View();
                }
                else
                {
                    return RedirectToAction("searchEnginePage", "pages");
                }

            }
        }

    }
}