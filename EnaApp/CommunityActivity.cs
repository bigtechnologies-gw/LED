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

namespace EnaApp
{
    [Activity(Label = "community_activity")]
    public class CommunityActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_community);
            // Create your application here

            var inputText = FindViewById<TextInputEditText>(Resource.Id.textInputEditTextCommunityName);
            var inputText2 = FindViewById<TextInputEditText>(Resource.Id.textInputEditText2);
            var inputText3 = FindViewById<TextInputEditText>(Resource.Id.textInputEditText4);
            //inputText.Touch += (sender, e) =>
            //{
            //    inputText.SelectAll();
            //};
            // select all text when input-text is selecte
            inputText.SetSelectAllOnFocus(true);
            inputText2.SetSelectAllOnFocus(true);
            inputText3.SetSelectAllOnFocus(true);

            // @+id/textInputEditTextCommunityName
            if (inputText.Clickable)
            {
                System.Diagnostics.Debug.WriteLine("IS CLICKABLE!");
                // write the output log
                inputText.Click += (sender, e) =>
                {
                    System.Diagnostics.Debug.WriteLine("BUTTON CLICKED!");
                    System.Diagnostics.Debug.WriteLine($"Log output: {e.ToString()}");
                    Log.WriteLine(LogPriority.Debug, "none", $"Log output : {e.ToString()}");
                };
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("is not clickable!");
            }
            //inputText.Click += (sender, e) =>
            //{
            //    inputText.selectall
            //};
        }
    }
}