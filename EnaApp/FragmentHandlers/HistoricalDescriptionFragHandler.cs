using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
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
            _view =  inflater.Inflate(Resource.Layout.historical_description, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);

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

            var tabanca = CreateTabancaActivity.NewTabanca;
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


             // TODO: !!!!
            //var spinner = new Spinner();

            //spinner.GetChildAt
            // store tabanca ID


        }
    }
}