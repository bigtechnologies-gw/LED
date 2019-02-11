
using System;
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
        // IDs of tabancas to be added to community
        private List<string> _IdTabancas = new List<string>();
        private string _mainTabancaId;

        private int _countAdd = 0;

        public string ID { get; } = Guid.NewGuid().ToString();

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
            // ReSharper disable once PossibleNullReferenceException
            IEnumerable<XElement> tabancas = rootSector.Element("Tabancas")
                .Elements("Tabanca")
                .Where(el =>
                    string.IsNullOrEmpty(el.Element("Name")?.Value) == false &&
                    string.IsNullOrEmpty(el.Element("ID")?.Value) == false &&
                    string.IsNullOrEmpty(el.Element("CommunityID")?.Value)).ToList();

            // TODO: handle when tabancas is null

            var listTabanca = tabancas.Select(el => new Tabanca
            {
                ID = el.Element("ID").Value,
                Name = el.Element("Name").Value,
            });

            var spinnerTabanca = FindViewById<Spinner>(Resource.Id.spinner1);
            spinnerTabanca.Adapter = new ArrayAdapter<Tabanca>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem,
               listTabanca.ToList());

            var buttonCreateCommunity = FindViewById<Button>(Resource.Id.buttonDoneCreateCommunity);

            buttonCreateCommunity.Click += (sender, e) =>
            {
                string communityName = FindViewById<TextInputEditText>(Resource.Id.textInputEditText1).Text;

                // validate communty name
                if (string.IsNullOrWhiteSpace(communityName))
                {
                    // notify user and return

                }

                var xSector = XmlUtils.GetRootSector();

                // community with same name already exit in current sector
                if (xSector.Element("Communities")?.Elements("Community").Any(c => c.Element("Name")?.Value.Equals(communityName, StringComparison.OrdinalIgnoreCase) == true) == true)
                {
                    // add community to xml file
                    var dBuilder = new AlertDialog.Builder(this, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dBuilder.SetTitle("Erro!");
                    dBuilder.SetMessage("A community with same name already exits!");
                    dBuilder.SetPositiveButton(Android.Resource.String.Yes, this);
                    dBuilder.SetNegativeButton(Android.Resource.String.No, this);
                    dBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                    dBuilder.Show();
                    return;
                }

                var el = new XElement("Community",
                    new XElement("ID", ID), // community id
                    new XElement("Name", communityName),
                    new XElement("MainID", _mainTabancaId ?? string.Empty),
                    new XElement("Tabancas",
                    _IdTabancas.Select(id => new XElement("ID", id)).ToList())
                    );

                //var xTabanca = XmlUtils.GetRootSector().Element("Tabancas").Elements("Tabanca")
                //    .First(t => t.Element("ID").Value.Equals(selTabanca.ID));

                //if (xTabanca.Element("CommunityID") == null)
                //{
                //    xTabanca.Add(new XElement("CommunityID", ID));
                //    XmlUtils.Save();
                //}


                // ReSharper disable once PossibleNullReferenceException
                xSector.Element("Communities").Add(el);

                XmlUtils.Save();

                // add community to xml file
                var dialogBuilder = new AlertDialog.Builder(this, Android.Resource.Style.ThemeMaterialDialogAlert);
                dialogBuilder.SetTitle("Comunidade criada!");
                dialogBuilder.SetMessage("A comunidade foi criada com sucesso");
                dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, this);
                dialogBuilder.SetNegativeButton(Android.Resource.String.No, this);
                dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                dialogBuilder.Show();

                Finish();
            };

            FindViewById<Button>(Resource.Id.buttonAddTabancaToCommunity).Click += (sender, e) =>
            {
                Spinner spinnerTabancas = FindViewById<Spinner>(Resource.Id.spinner1);
                var selTabanca = spinnerTabancas.SelectedItem.Cast<Tabanca>();

                _IdTabancas.Add(selTabanca.ID);

                var checkboxMain = FindViewById<CheckBox>(Resource.Id.checkBox1);
                if (checkboxMain.Checked)
                {
                    _mainTabancaId = selTabanca.ID;
                    // disable the main tabanca checkbox once one is added.
                    checkboxMain.Enabled = false;
                }

                var adapter = (ArrayAdapter)spinnerTabancas.Adapter;
                adapter.Remove(spinnerTabancas.SelectedItem);
                adapter.NotifyDataSetChanged();

                //spinnerTabancas.RemoveViewAt(spinnerTabancas.SelectedItemPosition);

                var dialogBuilder = new AlertDialog.Builder(this, Android.Resource.Style.ThemeMaterialDialogAlert);
                dialogBuilder.SetTitle("Adicionar tabanca");
                dialogBuilder.SetMessage($"Tabanca {selTabanca.Name} adicionada com sucesso");
                dialogBuilder.SetPositiveButton(Android.Resource.String.Yes, this);
                dialogBuilder.SetNegativeButton(Android.Resource.String.No, this);
                dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                dialogBuilder.Show();
                if (++_countAdd == 3)
                {
                    buttonCreateCommunity.Enabled = true;
                }
            };

        }


    }
}