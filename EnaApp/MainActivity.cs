using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using EnaApp.Entities;
using System;
using System.Collections.Generic;
//using static Android.Manifest;

//using Android.Graphics;

namespace EnaApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public readonly List<Region> RegionList;

        public MainActivity()
        {
            // https://www.viralandroid.com/2016/04/android-gridview-with-image-and-text.html
            //***this.LayoutInflater.Context.GetSystemService(Context.LayoutInflaterService);

            RegionList = new List<Region>()
            {
                new Region("Bafata", new List<string>
                {
                    "Bafata", "Bambadinca", "Cuntuboel","Galomaro", "Gamamundo", "Xitole"
                }),
                new Region("Biombo", new List<string>
                {
                     "Prabis", "Quinhamel", "Safim"
                }),
                new Region("Bolama", new List<string>
                {
                    "Bolama", "Bobaque", "Caravela", "Uno"
                }),
                new Region("Cacheu", new List<string>
                {
                   "Bigene", "Bula", "Cacheu", "Caio", "Canchungo", "Sao Domingos"
                }),
                new Region("Gabu", new List<string>
                {
                    "Boe", "Gabu", "Piche", "Pirada", "Sonaco"
                }),
                new Region("Oio", new List<string>
                {
                    "Bissora", "Farim", "Mansaba", "Mansoa", "Nhacra"
                }),
                new Region("Quinta", new List<string>
                {
                    "Buba", "Empada", "Fulacunda", "Tite"
                }),
                new Region("Tombali", new List<string>
                {
                    "Bedanda", "Cacine", "Catio", "Quebo", "Komo"
                }),
            };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            FindViewById<Button>(Resource.Id.buttonBafata).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonBiombo).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonBolama).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonCacheu).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonQuinara).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonTombali).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonBiombo).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonGabu).Click += ButtonRegionClickHandler;
            FindViewById<Button>(Resource.Id.buttonOio).Click += ButtonRegionClickHandler;

            //var spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            //spinner.SetBackgroundColor(Android.Graphics.Color.Blue);

            //spinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1,
            //    new string[]{
            //    "Community A",
            //    "Community B",
            //    "Community C",
            //});

            //var button = FindViewById<Button>(Resource.Id.buttonBafata);
            // button.Click += Button_Click;
            // FindViewById<Button>(Resource.Id.buttonBiombo).Click += MainActivity_Click;

            // this.CheckSelfPermission(Manifest.Permission.Camera) == Android.Content.PM.Permission.Granted;
            // Android.Resource.Id.Content
            // this.CheckSelfPermission(Manifest.Permission.Camera) == Android.Content.PM.Permission.
        }

        private void ButtonRegionClickHandler(object obj, EventArgs e)
        {
            Button clickedButton = (Button)obj;
            var intent = new Intent(this, typeof(SectorActivity));
            intent.PutExtra("Region", (string)clickedButton.Tag);
            StartActivity(intent);
            //Window.AddFlags(Android.Views.WindowManagerFlags.AllowLockWhileScreenOn);
        }

    }
}

/*
 * https://forums.xamarin.com/discussion/comment/247920/#Comment_247920
 * I have found the solution.
LinearLayout.LayoutParams layoutparams = new LinearLayout.LayoutParams(int width, int height);
view.FindViewById(Resource.Id.ImageTextA).LayoutParameters = layoutparams;
 */
