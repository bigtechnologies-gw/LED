
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnaApp.FragmentHandlers
{
    public class HistoricalDescriptionFragHandler : Fragment
    {
        private View _view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            // RadioGroup radioGroup = (RadioGroup)Activity.FindViewById(Resource.Id.radioGroup1);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            _view = inflater.Inflate(Resource.Layout.historical_description, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);

            //

            var newTabanca = CreateTabancaActivity.NewTabanca;

            if (newTabanca != null)
            {

                // node do fundador da tabanca
                _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditTextFounderName).Text = newTabanca.FounderName ?? string.Empty;

                // periodo de fundacao da tabanca em ano
                var radioGroupFoundation = _view.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
                int countRadioChild = radioGroupFoundation.ChildCount;
                for (int i = 0; i < countRadioChild; i++)
                {
                    RadioButton radioButton = (RadioButton)radioGroupFoundation.GetChildAt(i);

                    if (radioButton.Text.Equals(newTabanca.TabancaCreation, System.StringComparison.OrdinalIgnoreCase))
                    {
                        radioButton.Checked = true;
                        break;
                    }
                }

                // tabanca de origem
                _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4).Text = newTabanca.NativeTabanca ?? string.Empty;
                var spinner = _view.FindViewById<Spinner>(Resource.Id.spinner2);

                int count = spinner.Adapter.Count;
                for (int i = 0; i < count; i++)
                {

                    var view = spinner.GetChildAt(0);

                    //if (view is TextView)
                    //{

                    //}
                    //if (view is EditText)
                    //{

                    //}


                    string item = (string)spinner.Adapter.GetItem(i);
                    if (item.Equals(newTabanca.FounderEthnic, System.StringComparison.OrdinalIgnoreCase))
                    {
                        spinner.SetSelection(i);
                        break;
                    }
                }
            }

            return _view;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }

        public override void OnViewStateRestored(Bundle savedInstanceState)
        {
            base.OnViewStateRestored(savedInstanceState);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            Tabanca tabanca = CreateTabancaActivity.NewTabanca;
            tabanca.FounderName = _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditTextFounderName).Text;

            var radioGroup = (RadioGroup)_view.FindViewById(Resource.Id.radioGroup1);

            int count = radioGroup.ChildCount;

            for (int i = 0; i < count; i++)
            {
                RadioButton radioButton = (RadioButton)radioGroup.GetChildAt(i);

                if (radioButton.Checked)
                {
                    tabanca.TabancaCreation = radioButton.Text;
                    break;
                }
            }

            tabanca.NativeTabanca = View.FindViewById<TextInputEditText>(Resource.Id.textInputEditText4).Text;
            var view = _view.FindViewById<Spinner>(Resource.Id.spinner2).SelectedView;

            tabanca.FounderEthnic = ((TextView)view).Text;
            // TODO: !!!!
            //var spinner = new Spinner();

            //spinner.GetChildAt
            // store tabanca ID
        }
    }
}