
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using EnaApp.Helpers;
using System.Threading.Tasks;

namespace EnaApp
{
    public class CreateTabancaListFragment : ListFragment
    {
        private View _preSelView;
        private Color _defaultBackGround;

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
                    "Ponto de união",
                    "Descrição historial da tabanca",
                    "Governação local",
                    "Actividades económicas da tabanca",
                    "Economia",
                    "Ponteiros",
                    "Emprego",
                    "Serviços básicos (Água, energia e saneamento)",
                    "Acesso a comunidade",
                    "Comunicação",
                    "Concluir"
                });
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
                    // replace whatever content that is inside framelayout with the fragment content.
                    transaction.Replace(Resource.Id.frameLayout1, new LocalicationFragment());
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
                    // replace whatever content that is inside framelayout with the fragment content.
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.LocalGovFragHandler());
                    break;

                case 4:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.EconomicActivityFragHandler());
                    break;

                case 5:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.EconomicFragHandler());
                    break;

                case 6:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.PonteirosFragHandler());
                    break;

                case 7:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.EmpregoFragHandler());
                    break;
                case 8:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.ServicosBasicosFragHandler());
                    break;
                case 9:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.CommunityAccessFragHandler());
                    break;
                case 10:
                    transaction.Replace(Resource.Id.frameLayout1, new FragmentHandlers.CommunicationFragHandler());
                    break;
                case 11:

                    var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dialogBuilder.SetTitle("Tabanca criada!");
                    dialogBuilder.SetMessage("Tabanca foi criada com sucesso");

                    // TODO: Main activity has to implement 
                    //dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, Activity);
                    //dialogBuilder.SetNegativeButton(Android.Resource.String.No, Activity);

                    dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                    dialogBuilder.Show();

                    var xRootSector = XmlUtils.GetRootSector();
                    xRootSector.Element("Tabancas").Add(CreateTabancaActivity.NewTabanca.ToXElement());

                    // NOTE: ahaha...
                    //xRootSector.Save(Configs.DataFile);

                    XmlUtils.Save();


                    // save all data and close this Activity
                    //Activity.OnBackPressed();

                    //Thread.Sleep(1000 * 3);
                    Task.Delay(1000 * 3).ContinueWith(t =>
                    {
                        Activity.Finish();
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                    break;
            }


            //transaction.AddToBackStack(null);
            transaction.SetTransition(FragmentTransit.FragmentFade);
            transaction.Commit();

            _preSelView = v;

        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var defaultListBackgroundColor = Android.Graphics.Color.Rgb(37, 150, 166);

            ListView.SetBackgroundColor(defaultListBackgroundColor);
            _defaultBackGround = defaultListBackgroundColor;


            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.frameLayout1, new LocalicationFragment());
            transaction.Commit();

            //TODO: Make sure correct item is selected from listview.
            //ListView.PerformItemClick(null, 0, 0);
            //ListView.SetSelection(0);
        }

    }
}