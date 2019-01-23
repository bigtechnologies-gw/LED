using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using EnaApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EnaApp
{
    public class ModifyCommunityFragList : ListFragment, IDialogInterfaceOnClickListener
    {
        private Color _defaultBackGround;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            var listAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleExpandableListItem1, new[]
            {
                "Tabanca 1",
                "Tabanca 2",
                "Tabanca 3",
                "Tabanca 4",
                "Tabanca 5",
                "Tabanca 6",
                "Tabanca 7",
                "Tabanca 8",
                "Tabanca 9",
                "Tabanca 10",
            });
            ListAdapter = listAdapter;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
            dialogBuilder.SetTitle("Modificar informacoes!");
            dialogBuilder.SetMessage($"Modificar informacoes da {((TextView)v).Text}");
            dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, this);
            dialogBuilder.SetNegativeButton(Android.Resource.String.No, this);
            dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
            dialogBuilder.Show();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var defaultListBackgroundColor = Android.Graphics.Color.Rgb(37, 150, 166);

            ListView.SetBackgroundColor(defaultListBackgroundColor);
            _defaultBackGround = defaultListBackgroundColor;

            var parentActivity = (ModifyCommunityActivity)Activity;

            if (Activity == null)
            {

            }
            else
            {
                var propInfo = parentActivity.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals("CommunityID"));
                Console.ReadLine();
            }

            // get community name, tabanca principal
            // get all tabanca that belong to that community

            var xdocData = XDocument.Load(Configs.DataFile);

            // regions -> region -> sectors -> sector -> 
            // -> Tabancas -> tabanca
            // -> Communities -> Community

            var appContext = MainActivity.AppContext;

            // note: XDocument.Root = Regions
            XElement xElSelSector = xdocData.Root
                .Elements("Region").First(el => el.Element("Name").Value.Equals(appContext.Region, StringComparison.OrdinalIgnoreCase))
                .Element("Sectors").Elements("Sector").FirstOrDefault(el => el.Element("Name").Value.Equals(appContext.Sector, StringComparison.OrdinalIgnoreCase));

            // UNDONE: there must be atlest one community for this to work!
            XElement selCommunity = xElSelSector
                .Element("Communities")
                .Elements("Community")
                .FirstOrDefault(el => el.Element("ID")?.Value.Equals(parentActivity.CommunityID, StringComparison.OrdinalIgnoreCase) == true);

            if (selCommunity == null)
            {
                return;
            }

            // get all the tabancas that belong to current selected community
            IEnumerable<XElement> communityTabancas = xElSelSector.Element("Tabancas").Elements("Tabanca")
               .Where(el => el.Element("CommunityID").Value.Equals(parentActivity.CommunityID, StringComparison.OrdinalIgnoreCase));

            List<Tabanca> listTabanca = communityTabancas.Select(el => new Tabanca(el)).ToList();

            // TODO: why Resource.Id.listMode?
            //var listView = View.FindViewById<ExpandableListView>(Resource.Id.listMode);
            ListView.Adapter = new ArrayAdapter<Tabanca>(Activity, Android.Resource.Layout.SimpleListItem1, listTabanca);

        }


        public void OnClick(IDialogInterface dialog, int which)
        {
            System.Diagnostics.Debug.WriteLine(which);
            switch (which)
            {
                // user "ok" action
                case -1:
                    var intent = new Intent(Activity, typeof(CreateTabancaActivity));
                    //GC.Collect()
                    StartActivity(intent);
                    break;
                // user cancel action
                case 2:
                    break;
            }
        }
    }
}