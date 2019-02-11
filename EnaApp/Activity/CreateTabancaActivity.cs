
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using System.IO;
using System.Xml.Linq;

namespace EnaApp
{
    [Activity(Label = "CreateTabancaActivity")]
    public class CreateTabancaActivity : Activity
    {
        public static Tabanca NewTabanca { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.create_tabanca_layout);
            // Create your application here

            NewTabanca = Tabanca.Build();
        }

        public override View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            return base.OnCreateView(name, context, attrs);

            // by: Ivandro Ismael
            //return LayoutInflater.Inflate(Resource.Layout.create_tabanca_layout, null);
            //var layoutInflater = (LayoutInflater)context.GetSystemService(LayoutInflaterService);
            //layoutInflater.Inflate(Resource.Layout.create_tabanca_layout);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            // save data to file
            // get the data stored in AppContext in order to write/update the root
            // write the "NewTabanca data"
            //Android.OS.Environment

            //Java.IO.File file = FilesDir;

            // -------> 
            // close activity
            //Finish()

        }
    }
}