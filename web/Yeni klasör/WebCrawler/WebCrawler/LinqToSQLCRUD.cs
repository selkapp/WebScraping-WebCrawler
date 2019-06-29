using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace WebCrawler
{

    class LinqToSQLCRUD           ///firebase deneme
    {
        //DataClasses1DataContext db;
        //public LinqToSQLCRUD ()
        //{
        //    db = new DataClasses1DataContext(GetConnectionString());
        //}

        //public void AddNewTarif(TARIF newTarif)
        //{
        //    try
        //    {
        //        newTarif.ID = Guid.NewGuid().ToString();
        //        db.TARIFs.InsertOnSubmit(newTarif);
        //        db.SubmitChanges();
        //    }
        //    catch(Exception ex)
        //    {
        //        db.GetChangeSet().Updates.Clear();
        //        Console.Write("unhandled exception " + ex.Message);
        //    }
        //}
        //static private string GetConnectionString()
        //{
        //    return "Data Source=DESKTOP-JI4EDAT;Initial Catalog=YEMEK_TARIFI;Integrated Security=True";
        //}

        //internal bool exists(TARIF tarif)
        //{
        //    return db.TARIFs.FirstOrDefault(t=>t.YEMEK_ADI.Equals(tarif.YEMEK_ADI) && t.SITE_ADI.Equals(tarif.SITE_ADI)) != null;
        //}


        IFirebaseConfig config = new FirebaseConfig {

            AuthSecret = "kMtfKSMakTgEwl4Hw3mzA0P1Wk4kkz9uKPxpCWUN",
           BasePath="https://tarifdefterim-9af78.firebaseio.com/",

        };

        IFirebaseClient client;






    }
}
