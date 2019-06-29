using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    class Tarif
    {
        public string id{ get; set; }
        public String site_adi{ get; set; }

        public String  yemek { get; set; }
        public String url { get; set; }
        public String resim { get; set; }
        public String MALZEMELER { get; set; }
        public String HAZIRLANIS { get; set;}
        public List<String> malzemeler { get; set; }
        public List<String> hazirlanisi { get; set; }

        public Tarif()
        {
            malzemeler = new List<String>();
            hazirlanisi = new List<String>();
        }

        public override String ToString()
        {
            var stringVal = "";
            stringVal += "Url : " + url + "\n";
            stringVal += "Adi : " + yemek + "\n";
            stringVal += "Resim : " + resim + "\n";
            stringVal += "MALZEMELER : \n";
            for (var i = 0; i < malzemeler.Count; i++)
                stringVal += i + "- " + malzemeler[i] + "\n";
            stringVal += "HAZIRLANIŞI : \n";
            for (var i = 0; i < hazirlanisi.Count; i++)
                stringVal += i + "- " + hazirlanisi[i] + "\n";

            //yemekAdi + "\n" + imgSource + "\n" + malzemeListesi.ToString() + "\n" + nasilYapilir.ToString() + "\n*************"
            return stringVal;
        }
    }
}
