
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Linq;

namespace EnaApp
{
    [Activity(Label = "CreateCommunityActivity")]
    public class CreateCommunityActivity : Activity, IDialogInterfaceOnClickListener
    {
        public void OnClick(IDialogInterface dialog, int which)
        {
            //throw new System.NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.activity_community);

            SetContentView(Resource.Layout.create_communityLayout);

            // Create your application here

            //var inputText = FindViewById<TextInputEditText>(Resource.Id.textInputEditTextCommunityName);
            //var inputText2 = FindViewById<TextInputEditText>(Resource.Id.textInputEditText2);
            //var inputText3 = FindViewById<TextInputEditText>(Resource.Id.textInputEditText4);


            // # commnted
            //inputText.Touch += (sender, e) =>
            //{
            //    inputText.SelectAll();
            //};

            // select all text when input-text is selecte
            //inputText.SetSelectAllOnFocus(true);
            //inputText2.SetSelectAllOnFocus(true);
            //inputText3.SetSelectAllOnFocus(true);

            // @+id/textInputEditTextCommunityName
            //if (inputText.Clickable)
            //{
            //    System.Diagnostics.Debug.WriteLine("IS CLICKABLE!");
            //    // write the output log
            //    inputText.Click += (sender, e) =>
            //    {
            //        System.Diagnostics.Debug.WriteLine("BUTTON CLICKED!");
            //        System.Diagnostics.Debug.WriteLine($"Log output: {e.ToString()}");
            //        Log.WriteLine(LogPriority.Debug, "none", $"Log output : {e.ToString()}");
            //    };
            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine("is not clickable!");
            //}
            //inputText.Click += (sender, e) =>
            //{
            //    inputText.selectall
            //};

            Spinner _spinnerTabanca = FindViewById<Spinner>(Resource.Id.spinner1);
            _spinnerTabanca.Adapter = new ArrayAdapter<Tabanca>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem,
                MainActivity.AppContext.Communities.First().Tabancas);

            FindViewById<Button>(Resource.Id.buttonDoneCreateCommunity).Click += (sender, e) =>
            {
                var dialogBuilder = new AlertDialog.Builder(this, Android.Resource.Style.ThemeMaterialDialogAlert);
                dialogBuilder.SetTitle("Comunidade criada!");
                dialogBuilder.SetMessage("A comunidade foi criada com sucesso");
                dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, this);
                dialogBuilder.SetNegativeButton(Android.Resource.String.No, this);
                dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                dialogBuilder.Show();
            };

            FindViewById<Button>(Resource.Id.buttonAddTabancaToCommunity).Click += (sender, e) =>
            {
                MainActivity.AppContext.Communities.First().Tabancas.Add(new Tabanca { Name = "New tabanca" });

                Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
                string tabanca = spinner.SelectedItem.ToString();

                var dialogBuilder = new AlertDialog.Builder(this, Android.Resource.Style.ThemeMaterialDialogAlert);
                dialogBuilder.SetTitle("Adicionar tabanca");
                dialogBuilder.SetMessage($"Tabanca {tabanca} adicionada com sucesso");
                dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, this);
                dialogBuilder.SetNegativeButton(Android.Resource.String.No, this);
                dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                dialogBuilder.Show();
                
            };
        }
    }
}