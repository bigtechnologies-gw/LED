
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using EnaApp.Helpers;
using System.Collections.Generic;

namespace EnaApp
{
    [Activity(Label = "MainEntityActivity")]
    public class MainEntityActivity : Activity
    {
        private Spinner _spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_communityChooser);

            FindViewById<Button>(Resource.Id.buttonCreateCommunity).Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CreateCommunityActivity));
                StartActivity(intent);
            };

            FindViewById<Button>(Resource.Id.buttonCreateTabanca).Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CreateTabancaActivity));
                //GC.Collect()
                StartActivity(intent);
            };

            FindViewById<Button>(Resource.Id.buttonModifyCommunity).Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ModifyCommunityActivity));
                var c = _spinner.GetItemAtPosition(_spinner.SelectedItemPosition).Cast<Community>();
                intent.PutExtra("communityID", c.ID);
                StartActivity(intent);
            };


            // get list community and list and
            var listCommunity = new List<Community>()
            {
                new Community(){Name = "Community aa"},
                new Community(){Name = "Community bb"},
                new Community(){Name = "Community cc"},
                new Community(){Name = "Community dd"},
                new Community(){Name = "Community ee"},
                new Community(){Name = "Community ff"},
                new Community(){Name = "Community gg"},
            };


            _spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            _spinner.Adapter = new ArrayAdapter<Community>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, listCommunity);

        }
    }
}