
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using EnaApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EnaApp
{
    [Activity(Label = "MainEntityActivity")]
    public class MainEntityActivity : Activity
    {
        private Spinner _spinner;
        private Button _buttonCreateCommunity;

        // Store the ID of all tabancas that belong to a community.
        private HashSet<string> _tabancasID => new HashSet<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_communityChooser);

            _buttonCreateCommunity = FindViewById<Button>(Resource.Id.buttonCreateCommunity);

            _buttonCreateCommunity.Click += (sender, e) =>
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
                intent = intent.PutExtra("communityID", c.ID);
                StartActivity(intent);
            };


            // get list community and list and
            //var listCommunity = new List<Community>()
            //{
            //    new Community(){Name = "Community aa"},
            //    new Community(){Name = "Community bb"},
            //    new Community(){Name = "Community cc"},
            //    new Community(){Name = "Community dd"},
            //    new Community(){Name = "Community ee"},
            //    new Community(){Name = "Community ff"},
            //    new Community(){Name = "Community gg"},
            //};


            //UpdateUI();
        }

        private void UpdateUI()
        {
            // ReSharper disable once PossibleNullReferenceException
            var countTabancaWithoutCommunity = XmlUtils.GetRootSector().Element("Tabancas")
                .Elements("Tabanca").Count(t => string.IsNullOrEmpty(t.Element("CommunityID")?.Value));

            _buttonCreateCommunity.Enabled = countTabancaWithoutCommunity >= 3;

            XElement xCommunities = XmlUtils.GetRootSector().Element("Communities");
            // ReSharper disable once PossibleNullReferenceException
            List<Community> communities = xCommunities.Elements("Community").Select(c => new Community()
            {
                // ReSharper disable once PossibleNullReferenceException
                ID = c.Element("ID").Value,
                Name = c.Element("Name")?.Value
            }).ToList();

            _spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            bool update = communities.Count > 0;
            _spinner.Enabled = update;
            FindViewById<Button>(Resource.Id.buttonModifyCommunity).Enabled = update;
            if (update)
            {
                _spinner.Adapter = new ArrayAdapter<Community>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem,
                    communities.ToList());
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateUI();
        }
    }
}