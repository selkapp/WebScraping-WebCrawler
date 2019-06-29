﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCrawler
{
    class YemekCrawler : ICrawler
    {
        static HtmlDocument htmlDoc = new HtmlDocument();

        static  string startUrl = "https://yemek.com";
        static string siteAdi = "Yemek.com";

        static WebRequest myWebRequest;
        static WebResponse myWebResponse;
        static Dictionary<String, bool> dict;
        LinqToSQLCRUD crud;
        List<String> urlList;
        int index;


        public YemekCrawler(LinqToSQLCRUD crud)
        {
            dict = new Dictionary<string, bool>();
            urlList = new List<String>();
            urlList.Add(getStartUrl());
            index = 0;
            this.crud = crud;
        }


        public String getStartUrl()
        {
            return startUrl;
        }
        public TARIF extractTarifFromHtml(string rstring, string url)
        {
            if (!url.Contains("/tarif/"))
                return null;

            var tarif = new TARIF();//new Tarif();
            tarif.URL = startUrl + url;
            tarif.SITE_ADI = siteAdi;
            htmlDoc.OptionFixNestedTags = true;
            try
            {
                htmlDoc.LoadHtml(rstring);
                //imgSource.ToList<HtmlNode>()[1].InnerHtml
                var currentTarif = htmlDoc.DocumentNode.Descendants("h1").ToList<HtmlNode>()[1];
                tarif.YEMEK_ADI = toTurkish(currentTarif.InnerHtml);
                tarif.RESIM = currentTarif.ParentNode.ParentNode.ParentNode.Descendants("img").ToList<HtmlNode>()[0].Attributes["src"].Value;
                tarif.HAZIRLANIS = "";
                tarif.MALZEMELER = "";
                tarif.KATEGORI = toTurkish(htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("_4sHUQUTiHjMXXArCyqTZk")).ToList<HtmlNode>()[0].InnerText);
                var list = htmlDoc.DocumentNode.Descendants().Where(x => x.HasClass("_3Z2MUIzzMNhESoosDGuUqN")).ToList<HtmlNode>();
                for (var i = 0; i < list.Count; i++)
                {
                    if (i == list.Count - 1)
                        tarif.HAZIRLANIS += appendString(tarif.HAZIRLANIS, list[i].Descendants("li").ToList<HtmlNode>().Select(x => x.InnerText).ToList<String>());
                    else
                        tarif.MALZEMELER = appendString(tarif.MALZEMELER, list[i].Descendants("li").ToList<HtmlNode>().Select(x => (x.InnerText.Substring(0,x.InnerText.Length-x.Descendants("div").ToList<HtmlNode>()[x.Descendants("div").ToList<HtmlNode>().Count-1].InnerText.Length) +" " + x.Descendants("div").ToList<HtmlNode>()[x.Descendants("div").ToList<HtmlNode>().Count - 1].InnerText)).ToList<String>());
                }
                Console.WriteLine(tarif.ToString());
                return tarif;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                Console.WriteLine("tarif çıkarılamadı : " + url);
                return null;
            }
        }


        public string toTurkish(string s)
        {
            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(s)).Trim().ToLower();
        }
        private string appendString(string val, List<string> enumerable)
        {
            for (var i = 0; i < enumerable.Count(); i++)
            {
                val += val == "" ? toTurkish(enumerable[i]) : "|" + toTurkish(enumerable[i]);
            }
            return val;
        }

        public string getUrl(string v)
        {
            return startUrl == v ? startUrl : startUrl + v;
        }


        public void extractURLs()
        {
            try
            {

                myWebRequest = WebRequest.Create(getUrl(urlList[index]));
                myWebResponse = myWebRequest.GetResponse();

                Stream responseStream = myWebResponse.GetResponseStream();

                StreamReader sreader = new StreamReader(responseStream);
                String Rstring = sreader.ReadToEnd();
                var tarif = extractTarifFromHtml(Rstring, urlList[index]);

                if (tarif != null && !crud.exists(tarif))
                {
                    crud.AddNewTarif(tarif);
                    Console.WriteLine(tarif.ToString());
                }


                GetNewLinks(Rstring, urlList);
                myWebResponse.Close();
            }
            catch (Exception ex)
            {
                Console.Write("unhandled exception " + ex.Message);
            }
            if (urlList.Count > index + 1)
            {
                index++;
               // if (index <= tarifBreak)
                    extractURLs();
            }
        }

        public void GetNewLinks(string content, List<String> urlList)
        {
            Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            foreach (var match in regexLink.Matches(content))
            {
                if (!dict.ContainsKey(match.ToString()))
                {
                    dict.Add(match.ToString(), true);
                    urlList.Add(match.ToString());
                }
            }
        }
    }
}
