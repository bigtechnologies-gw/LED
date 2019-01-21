using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

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