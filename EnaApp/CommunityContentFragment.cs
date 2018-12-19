
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnaApp
{
    public class CommunityContentFragment : ListFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            // default layout-text-view: Android.Resource.Layout.SimpleListItemActivated1
            ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemActivated1,
                new[] {
                    "Home",
                    "Ponto de União",
                    "DESCRIÇÃO HISTORIAL DA COMUNIDADE",
                    "Economic",
                    "Base association/ONGs 2018",
                    "Ponters/Pointeiros",
                    "Types of housing construction predominant",
                    "Building construction"
                });
        }

        Android.Graphics.Color _defaultBackGround;

        View _preSelView = null;
        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            //v.SetBackgroundColor(Color.Blue);

            string selOption = ((TextView)v).Text;

            //ListView.SetItemChecked(position, true);
            ListView.SetItemChecked(position, true);
            //ListView.

            // reset the background color
            if (_preSelView != v)
                _preSelView?.SetBackgroundColor(_defaultBackGround); // reset predefined color.

            v.SetBackgroundColor(Android.Graphics.Color.ParseColor("#74c55f"));

            // TODO: have a builder class that will return us a fragmetn basing on the selOption

            var transaction = FragmentManager.BeginTransaction();

            //transaction.Replace(Resource.Id.KDWfra)

            switch (position)
            {
                case 0:
                    var localizationFragment = new LocalicationFragment();
                    // replace whatever content that is inside framelayout with the fragment content.
                    transaction.Replace(Resource.Id.frameLayout1, localizationFragment);
                    break;
                case 1:
                    var meetingPoint = new FragmentHandlers.MeetingPoint();
                    // replace whatever content that is inside framelayout with the fragment content.
                    transaction.Replace(Resource.Id.frameLayout1, meetingPoint);
                    break;
                case 2:
                    // replace whatever content that is inside framelayout with the fragment content.
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.HistoricalDescriptionFragHandler());
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }


            //transaction.AddToBackStack(null);
            transaction.SetTransition(FragmentTransit.FragmentFade);
            transaction.Commit();

            _preSelView = v;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //var listContent = new ArrayAdapter<string>(Activity, Resource.Layout.simple)

            return base.OnCreateView(inflater, container, savedInstanceState);

            //return inflater.Inflate(Resource.Layout.mainlayout, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            var defaultListBackgroundColor = Android.Graphics.Color.Rgb(37, 150, 166);

            ListView.SetBackgroundColor(defaultListBackgroundColor);
            _defaultBackGround = defaultListBackgroundColor;


            // TODO: Make sure correct item is selected from listview.


            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.frameLayout1, new LocalicationFragment());
            transaction.Commit();
        }
    }
}