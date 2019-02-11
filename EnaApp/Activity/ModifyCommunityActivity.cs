using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using EnaApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Android.Support.Design.Widget;

namespace EnaApp
{
    [Activity(Label = "ModifyCommunityActivity")]
    public class ModifyCommunityActivity : Activity
    {
        public string CommunityID { get; set; }

        public XElement XCommunity { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // NOTE: THIS HAS TO BE HERE CAUSE THERE IS FRAGMENTLIST ASSOCIETED WITH THIS ACTIVITY
            // THAT WILL DEPEND ON COMMUNITY ID TO LOAD
            CommunityID = Intent.GetStringExtra("communityID");
            if (savedInstanceState != null)
            {
                CommunityID = savedInstanceState.GetString("communityID");
            }

            CommunityID = CommunityID ?? Intent.GetStringExtra("communityID");
            XCommunity = XmlUtils.GetRootSector()
                .Element("Communities")
                ?.Elements("Community")
                .FirstOrDefault(el => el.Element("ID")?.Value.Equals(CommunityID, StringComparison.OrdinalIgnoreCase) == true);

            // NOTE: LOAD VIEW WITH WILL CAUSE FRAGMETNLIST TO LOAD WHICH 
            // HAS HIGH DEPENDENCY IN "CommunityID"
            SetContentView(Resource.Layout.modify_community_layout);
            // Create your application here

            var buttonUpdateCommunity = FindViewById<Button>(Resource.Id.button1);

            var textInput = FindViewById<TextInputEditText>(Resource.Id.textInputEditText1);

            buttonUpdateCommunity.Click += (sender, args) =>
            {
                string newName = textInput.Text;

                if (string.IsNullOrWhiteSpace(newName))
                {
                    return;
                }

                XCommunity.Element("Name").Value = newName;
                XmlUtils.Save();
                
                var dialogBuilder = new AlertDialog.Builder(this, Android.Resource.Style.ThemeMaterialDialogAlert);
                dialogBuilder.SetTitle("Actualizada!");
                dialogBuilder.SetMessage("Informacoes actualizadas com sucesso");

                // TODO: Main activity has to implement 
                //dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, Activity);
                //dialogBuilder.SetNegativeButton(Android.Resource.String.No, Activity);
                dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                dialogBuilder.Show();
            };

            var communityName = XCommunity.Element("Name")?.Value;
            FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text = communityName;
        }

        public void PopulateSpinner(IList<Tabanca> tabancas)
        {
            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.Adapter = new ArrayAdapter<Tabanca>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, tabancas);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState?.PutString("communityID", CommunityID);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            if (savedInstanceState != null)
            {
                CommunityID = savedInstanceState.GetString("communityID");
            }
        }


        protected override void OnResume()
        {
            base.OnResume();
            //CommunityID = CommunityID ?? Intent.GetStringExtra("communityID");
            //XCommunity = XmlUtils.GetRootSector()
            //    .Element("Communities")
            //    ?.Elements("Community")
            //    .FirstOrDefault(el => el.Element("ID")?.Value.Equals(CommunityID, StringComparison.OrdinalIgnoreCase) == true);

            //var communityName = XCommunity.Element("Name")?.Value;

            //var view = FindViewById<TextInputEditText>(Resource.Id.textInputEditText1);

            //if (view == null)
            //{
                
            //}
        }
    }
}