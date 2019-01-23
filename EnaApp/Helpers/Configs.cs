using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EnaApp.Helpers
{
    public static class Configs
    {
        public static string  DataFile { get; set; }

        static Configs()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            //CommunityFile = Path.Combine(path, "CommunityData.xml");
            //TabancaDataFile = Path.Combine(path, "TabancaData.xml");



            // regions -> region -> sectors -> sector -> 
            // -> Tabancas -> tabanca
            // -> Communities -> Community

            DataFile = Path.Combine(path, "DataFile.xml");
        }
    }
}