using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EnaApp
{
    [Activity(Label = "ChooseCommunity")]
    public class ChooseCommunity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_communityChooser);

            var button = FindViewById<Button>(Resource.Id.buttonChooseCommunity);
            button.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CommunityActivity));
                StartActivity(intent);
            };
        }
    }
}