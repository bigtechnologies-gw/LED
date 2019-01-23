
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using EnaApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

            XElement rootSector = XmlUtils.GetRootSector();

            // only filter the tabancas that doesn't belong to a community already
            IEnumerable<XElement> tabancas = rootSector.Element("Tabancas")?
                .Elements("Tabanca")?.Where(el => string.IsNullOrEmpty(el.Element("CommunityID")?.Value));

            // TODO: handle when tabancas is null

            IEnumerable<Tabanca> listTabanca = tabancas.Select(el => new Tabanca
            {
                ID = el.Element("ID").Value,
                Name = el.Element("Name").Value,
            });

            Spinner _spinnerTabanca = FindViewById<Spinner>(Resource.Id.spinner1);
            _spinnerTabanca.Adapter = new ArrayAdapter<Tabanca>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem,
               listTabanca.ToList());

            FindViewById<Button>(Resource.Id.buttonDoneCreateCommunity).Click += (sender, e) =>
            {
                string communityName = FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text;

                // validate communty name
                if (string.IsNullOrWhiteSpace(communityName))
                {
                    // notify user and return

                }

                // add community to xml file
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
                // get select tabanca and store the it in community
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