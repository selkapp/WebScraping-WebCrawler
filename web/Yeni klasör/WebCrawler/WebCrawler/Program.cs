using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WebCrawler.Crawlers;
using System.Threading.Tasks;
using System.Threading;

namespace WebCrawler
{
    class Program
    {
        static LinqToSQLCRUD crud;
        static int tarifBreak = 100;
        static void Main(string[] args)
        {
            crud = new LinqToSQLCRUD();
            List < Thread > threadList = new List<Thread>();
            threadList.Add(new Thread(new ThreadStart(yemekCrawlerWorker)));
            threadList.Add(new Thread(new ThreadStart(nefisYemekWorker)));
            threadList.Add(new Thread(new ThreadStart(lezzetWorker)));

            foreach (var thread in threadList)
            {
                thread.Start();
            }

            Console.Read();
        }


        public static void yemekCrawlerWorker()
        {
            new YemekCrawler(crud).extractURLs();
        }

        public static void nefisYemekWorker()
        {
            new NefisYemekCrawler(crud).extractURLs();
        }


        public static void lezzetWorker()
        {
            new LezzetCrawler(crud).extractURLs();
        }


    }
}
