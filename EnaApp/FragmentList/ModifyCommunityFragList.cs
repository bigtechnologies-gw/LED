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
using Android.Support.Design.Widget;

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
            //var listAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleExpandableListItem1, new[]
            //{
            //    "Tabanca 1",
            //    "Tabanca 2",
            //    "Tabanca 3",
            //    "Tabanca 4",
            //    "Tabanca 5",
            //    "Tabanca 6",
            //    "Tabanca 7",
            //    "Tabanca 8",
            //    "Tabanca 9",
            //    "Tabanca 10",
            //});
            //ListAdapter = listAdapter;

            var parentActivity = (ModifyCommunityActivity)Activity;


            //var propInfo = parentActivity.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals("CommunityID"));

            var curSector = XmlUtils.GetRootSector();

            // UNDONE: there must be atlest one community for this to work!
            //XElement selCommunity = curSector
            //    .Element("Communities")
            //    .Elements("Community")
            //    .FirstOrDefault(el => el.Element("ID")?.Value.Equals(parentActivity.CommunityID, StringComparison.OrdinalIgnoreCase) == true);


            // init UI

            // NOTE: Shouldn't be done here (done in activity that own this fragment)
            //Activity.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text =
            //    parentActivity.XCommunity.Element("Name").Value;


            // get all the tabancas that belong to current selected community
            IEnumerable<XElement> communityTabancas = curSector.Element("Tabancas")?.Elements("Tabanca")
               .Where(el => el.Element("CommunityID")?
                   .Value.Equals(parentActivity.CommunityID,
                       StringComparison.OrdinalIgnoreCase) == true);

            var tabancasX = curSector.Element("Tabancas").Elements("Tabanca");
            var parentActivityXCommunity = parentActivity.XCommunity;

            // NOTE: VERY BAD!
            //var communityTabancasGroupJoin = parentActivityXCommunity.Element("Tabancas")
            //    .Elements("ID")
            //    .GroupJoin(tabancasX, xId => xId.Value, xTb => xTb.Element("ID").Value,
            //        (xId, xTabancas) => new
            //        {
            //            CommunityID = xId.Parent.Parent.Element("ID").Value,
            //            CommunityName = xId.Parent.Parent.Element("Name").Value,
            //            Tabancas = xTabancas.ToList()
            //        }).First();

            // build a hashset for lookup
            var lookUpSet = parentActivityXCommunity.Element("Tabancas")
                .Elements("ID").Select(id => id.Value).ToHashSet();

            var listTabanca = tabancasX.Where(tb => lookUpSet.Contains(tb.Element("ID").Value)).Select(t => new Tabanca
                {ID = t.Element("ID").Value, Name = t.Element("Name").Value}).ToList();

            // note: used when working with group-join
            //var listTabanca = communityTabancasGroupJoin.Tabancas.Select(t => new Tabanca
            //{ ID = t.Element("ID").Value, Name = t.Element("Name").Value }).ToList();

            //List<Tabanca> listTabanca = communityTabancas?.Select(el => new Tabanca()
            //{
            //    ID = el.Element("ID").Value,
            //    Name = el.Element("Name").Value
            //}).ToList();

            // TODO: why Resource.Id.listMode?
            //var listView = View.FindViewById<ExpandableListView>(Resource.Id.listMode);

            //if (listTabanca.Count > 0)
            //{
            //    ListView.Adapter = new ArrayAdapter<Tabanca>(Activity, Android.Resource.Layout.SimpleListItem1, listTabanca);
            //}

            //ListView.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, new string[] { "Ivandro Ismael", "Gomes Jao" });
            ListAdapter = new ArrayAdapter<Tabanca>(Activity, Android.Resource.Layout.SimpleListItem1, listTabanca);

            parentActivity.PopulateSpinner(listTabanca);
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