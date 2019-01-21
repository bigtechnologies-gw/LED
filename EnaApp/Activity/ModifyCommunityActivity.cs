using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using EnaApp.Helpers;
using System;
using System.Linq;
using System.Xml.Linq;

namespace EnaApp
{
    [Activity(Label = "ModifyCommunityActivity")]
    public class ModifyCommunityActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.modify_community_layout);

            //string id = savedInstanceState.GetString("communityID");

            string id = Intent.GetStringExtra("communityID");


            // get community name, tabanca principal
            // get all tabanca that belong to that community

            var xdocCom = XDocument.Load(Configs.CommunityFile);

            // regions -> region -> sectors -> sector -> 
            // -> Tabancas -> tabanca
            // -> Communities -> Community

            var selCommunity = xdocCom.Root.Elements("Community")
                .FirstOrDefault(el => el.Element("ID").Value.Equals(id, StringComparison.Ordinal));

            if (selCommunity == null)
            {
                return;
            }

            // tabanca that belogn to that 
            var tabancas = xdocCom.Root.Elements("Tabanca")
               .FirstOrDefault(el => el.Element("Name").Value.Equals(id, StringComparison.Ordinal));

            var listView = FindViewById<ExpandableListView>(Resource.Id.listMode);

        }
    }
}