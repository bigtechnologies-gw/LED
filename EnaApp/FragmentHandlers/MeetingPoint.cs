
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace EnaApp.FragmentHandlers
{
    public class MeetingPoint : Fragment
    {
        private View _view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            _view = inflater.Inflate(Resource.Layout.meetingpoint, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);

            // Restore view-data
            var tabanca = CreateTabancaActivity.NewTabanca;

            if (tabanca != null)
            {
                _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text = tabanca.DistanceToSection.ToString();
                _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText2).Text = tabanca.DistanceToSector.ToString();
                _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText3).Text = tabanca.DistanceToRegion.ToString();
                _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4).Text = tabanca.DistanceToBissau.ToString();
            }
            return _view;

        }

        public override void OnDetach()
        {
            base.OnDetach();
            AppContext appContext = MainActivity.AppContext;

            /*
            public decimal DistanceToSection { get; set; }
            public decimal DistanceToSector { get; set; }
            public decimal DistanceToRegion { get; set; }
            public decimal DistanceToBissau { get; set; }
            */
            var tabanca = CreateTabancaActivity.NewTabanca;
            tabanca.DistanceToSection = Convert.ToInt32(_view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text);
            tabanca.DistanceToSector = Convert.ToInt32(_view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText2).Text);
            tabanca.DistanceToRegion = Convert.ToInt32(_view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText3).Text);
            tabanca.DistanceToBissau = Convert.ToInt32(_view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4).Text);
        }

    }
}
