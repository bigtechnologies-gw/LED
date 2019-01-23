using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using EnaApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EnaApp
{
    [Activity(Label = "ModifyCommunityActivity")]
    public class ModifyCommunityActivity : Activity
    {
        public string CommunityID { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //string id = savedInstanceState.GetString("communityID");

            CommunityID = Intent.GetStringExtra("communityID");
            SetContentView(Resource.Layout.modify_community_layout);


        }
    }
}