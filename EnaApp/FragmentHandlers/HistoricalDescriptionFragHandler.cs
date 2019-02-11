
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using EnaApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace EnaApp.FragmentHandlers
{
    public class HistoricalDescriptionFragHandler : Fragment, IDataPersistent
    {
        private View _view;
        private int _countMainEthnic;

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

            _view.FindViewById<CheckBox>(Resource.Id.checkBoxReasonFoundation).CheckedChange += (sender, e) =>
            {
                _view.FindViewById<EditText>(Resource.Id.editTextFoundingReasonOthers).Enabled = e.IsChecked;
            };
            _view.FindViewById<CheckBox>(Resource.Id.checkBoxCausesEthnic).CheckedChange += (sender, e) =>
            {
                _view.FindViewById<EditText>(Resource.Id.textInputEditTextEthnicsInTabanca).Enabled = e.IsChecked;
            };
            _view.FindViewById<CheckBox>(Resource.Id.checkBoxCausesCommunity).CheckedChange += (sender, e) =>
            {
                _view.FindViewById<EditText>(Resource.Id.textInputEditTextNearCommunity).Enabled = e.IsChecked;
            };

            var buttonAddEthnic = _view.FindViewById<Button>(Resource.Id.buttonAddEtnia);
            buttonAddEthnic.Click += (sender, e) =>
            {
                var newTabanca = CreateTabancaActivity.NewTabanca;
                _countMainEthnic++;

                if (_countMainEthnic == 3)
                {
                    buttonAddEthnic.Enabled = false;
                }

                newTabanca.MainEthnicGroupsInTabanca = newTabanca.MainEthnicGroupsInTabanca ?? new List<string>();
                var spinner = View.FindViewById<Spinner>(Resource.Id.spinner1);
                int index = spinner.SelectedItemPosition;
                string item = (string)spinner.Adapter.GetItem(index);
                if (newTabanca.MainEthnicGroupsInTabanca.Contains(item) == false)
                {
                    newTabanca.MainEthnicGroupsInTabanca.Add(item);
                    // display messsage new item added
                    var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dialogBuilder.SetTitle("Etnia adicionada!");
                    dialogBuilder.SetMessage("Etnia adicionada com sucesso!");
                    dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                    dialogBuilder.Show();
                }
                else
                {
                    // display message item already added
                    var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dialogBuilder.SetTitle("Informacao");
                    dialogBuilder.SetMessage("A etnia ja foi adicionada!");
                    dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                    dialogBuilder.Show();
                }
            };

            _view.FindViewById<Button>(Resource.Id.buttonAddChief).Click += (sender, e) =>
            {
                var newTabanca = CreateTabancaActivity.NewTabanca;

                newTabanca.CurrentTabancaChiefs = newTabanca.CurrentTabancaChiefs ?? new List<string>();
                var inputText = View.FindViewById<TextInputEditText>(Resource.Id.textInputEditTextCurrentChiefs);

                // invalid name
                if (string.IsNullOrWhiteSpace(inputText.Text))
                {
                    // todo: more checks
                    // display messsage new item added
                    var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dialogBuilder.SetTitle("Invalido!");
                    dialogBuilder.SetMessage("Nome invalido!");
                    dialogBuilder.SetIcon(Android.Resource.Drawable.StatNotifyError);
                    dialogBuilder.Show();
                    return;
                }

                if (newTabanca.CurrentTabancaChiefs.Contains(inputText.Text) == false)
                {
                    newTabanca.CurrentTabancaChiefs.Add(inputText.Text);
                    // display messsage new item added
                    var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dialogBuilder.SetTitle("Adicionar chefe!");
                    dialogBuilder.SetMessage("Chefe da tabanca adicionada com sucesso!");
                    dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                    dialogBuilder.Show();
                }
                else
                {
                    // display message item already added
                    var dialogBuilder = new AlertDialog.Builder(Activity, Android.Resource.Style.ThemeMaterialDialogAlert);
                    dialogBuilder.SetTitle("Informacao");
                    dialogBuilder.SetMessage("Chefe da tabanca com o mesmo nome ja existe!");
                    dialogBuilder.SetIcon(Android.Resource.Drawable.IcDialogInfo);
                    dialogBuilder.Show();
                }
            };

            LoadState();

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
            SaveState();

        }

        public void LoadState()
        {

            var newTabanca = CreateTabancaActivity.NewTabanca;

            if (newTabanca == null)
            {
                return;
            }

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
                //var view = spinner.GetChildAt(0); 
                string item = (string)spinner.Adapter.GetItem(i);
                if (item.Equals(newTabanca.FounderEthnic, System.StringComparison.OrdinalIgnoreCase))
                {
                    spinner.SetSelection(i);
                    break;
                }
            }

            // FOUNDING REASONS
            if (newTabanca.FoundingReasons != null)
            {
                var gridLayoutFoundingReason = _view.FindViewById<GridLayout>(Resource.Id.gridLayout1);
                for (int i = 0; i < gridLayoutFoundingReason.ChildCount; i++)
                {
                    View viewIdx = gridLayoutFoundingReason.GetChildAt(i);

                    if (viewIdx is CheckBox checkbox)
                    {
                        if (newTabanca.FoundingReasons.Contains(checkbox.Text))
                        {
                            checkbox.Checked = true;
                        }
                        else if (checkbox.Text.Equals("outros", System.StringComparison.OrdinalIgnoreCase))
                        {
                            //check the other-textbox and put the text in in input-text after textbox
                            checkbox.Checked = true;
                            _view.FindViewById<EditText>(Resource.Id.editTextFoundingReasonOthers).Text = newTabanca.FoudingReasongOthers;
                        }
                    }
                }
            }

            // PAST ISSUES RESTORE STATE
            if (newTabanca.PastIssues != null)
            {
                var gridlayoutPassIssues = _view.FindViewById<GridLayout>(Resource.Id.gridLayout2);
                for (int i = 0; i < gridlayoutPassIssues.ChildCount; i++)
                {
                    View viewIdx = gridlayoutPassIssues.GetChildAt(i);

                    if (viewIdx.GetType().IsInstanceOfType(typeof(CheckBox)))
                    {

                    }

                    if (viewIdx is CheckBox checkbox)
                    {
                        if (newTabanca.PastIssues.Contains(checkbox.Text))
                        {
                            checkbox.Checked = true;
                        }
                        //tabanca.PastIssues = tabanca.PastIssues ?? new List<string>();
                        //tabanca.FoundingReasons.Add(checkbox.Text);
                    }

                }
            }

            // CURRENT ISSUES RESTORE STATE
            if (newTabanca.CurrentIssues != null)
            {
                var gridlayoutCurrentIssues = _view.FindViewById<GridLayout>(Resource.Id.gridLayout3);
                for (int i = 0; i < gridlayoutCurrentIssues.ChildCount; i++)
                {
                    var viewIdx = gridlayoutCurrentIssues.GetChildAt(i);
                    if (viewIdx is CheckBox checkbox)
                    {
                        if (newTabanca.CurrentIssues.Contains(checkbox.Text))
                        {
                            checkbox.Checked = true;
                        }
                    }
                }
            }

            // principais etnicas da comunidade..
            // no need?

            // relacoes entre as etnias...
            if (newTabanca.RelationBetweenEthnicGroups != null)
            {
                var gridLayoutRelationBetweenEthnics = _view.FindViewById<GridLayout>(Resource.Id.gridLayoutRelationBetweenEthnics);
                for (int i = 0; i < gridLayoutRelationBetweenEthnics.ChildCount; i++)
                {
                    var viewIdx = gridLayoutRelationBetweenEthnics.GetChildAt(i);
                    if (viewIdx is CheckBox checkbox)
                    {
                        if (newTabanca.RelationBetweenEthnicGroups.Contains(checkbox.Text))
                        {
                            checkbox.Checked = true;
                        }
                        else
                        {
                            //RelationBetweenEthnicGroups

                            //    foreach
                            var textCause = newTabanca.RelationBetweenEthnicGroups
                                .Where(el => !(el.Equals("Boas", System.StringComparison.OrdinalIgnoreCase) ||
                                el.Equals("Conflituosas", System.StringComparison.OrdinalIgnoreCase) ||
                                el.Equals("Neutras", System.StringComparison.OrdinalIgnoreCase))).FirstOrDefault();

                            if (textCause != null)
                            {
                                checkbox.Checked = true;
                                // put the value in input-text
                                _view.FindViewById<TextInputEditText>
                                     (Resource.Id.textInputEditTextEthnicsInTabanca).Text = textCause;
                            }
                        }
                    }
                } // for

            } // if


            //relacoes com as comunidades vizinhas..
            if (newTabanca.RelationBetweenNearCommunities != null)
            {
                var gridLayoutRelationBetweenNearCommunities = _view.FindViewById<GridLayout>(Resource.Id.gridLayoutRelationBetweenNearCommunities);
                for (int i = 0; i < gridLayoutRelationBetweenNearCommunities.ChildCount; i++)
                {
                    var viewIdx = gridLayoutRelationBetweenNearCommunities.GetChildAt(i);
                    if (viewIdx is CheckBox checkbox)
                    {
                        if (newTabanca.RelationBetweenNearCommunities.Contains(checkbox.Text))
                        {
                            checkbox.Checked = true;
                        }
                        else
                        {
                            //RelationBetweenEthnicGroups

                            //    foreach
                            var textCause = newTabanca.RelationBetweenNearCommunities
                                .Where(el => !(el.Equals("Boas", System.StringComparison.OrdinalIgnoreCase) ||
                                el.Equals("Conflituosas", System.StringComparison.OrdinalIgnoreCase) ||
                                el.Equals("Neutras", System.StringComparison.OrdinalIgnoreCase))).FirstOrDefault();

                            if (textCause != null)
                            {
                                checkbox.Checked = true;
                                // put the value in input-text
                                _view.FindViewById<TextInputEditText>
                                     (Resource.Id.textInputEditTextNearCommunity).Text = textCause;
                            }
                        }
                    }
                }

                // actuais chefes da tabancas
                // display a list ? no need
            }

        }

        public void SaveState()
        {
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
            tabanca.FounderEthnic = ((TextView)_view.FindViewById<Spinner>(Resource.Id.spinner2).SelectedView).Text;

            // get gridLayout1
            // get all child controls

            // for those that are checkbox history the value in tabanca list (for option others only history if it's checked"

            // FOUNDING REASONS
            var gridLayout = _view.FindViewById<GridLayout>(Resource.Id.gridLayout1);
            for (int i = 0; i < gridLayout.ChildCount; i++)
            {
                var viewIdx = gridLayout.GetChildAt(i);

                if (viewIdx is CheckBox checkbox && checkbox.Checked)
                {
                    if (checkbox.Text.Equals("outros", System.StringComparison.OrdinalIgnoreCase))
                    {
                        tabanca.FoudingReasongOthers = _view.FindViewById<EditText>(Resource.Id.editTextFoundingReasonOthers).Text;
                    }
                    else
                    {
                        tabanca.FoundingReasons = tabanca.FoundingReasons ?? new List<string>();

                        // check if it's already to avoid duplicating elements
                        if (tabanca.FoundingReasons.Contains(checkbox.Text) == false)
                        {
                            tabanca.FoundingReasons.Add(checkbox.Text);
                        }
                    }
                }

            }

            tabanca.PastIssues = tabanca.PastIssues ?? new List<string>();
            // PAST ISSUES
            var gridlayoutPassIssues = _view.FindViewById<GridLayout>(Resource.Id.gridLayout2);
            for (int i = 0; i < gridlayoutPassIssues.ChildCount; i++)
            {
                var viewIdx = gridlayoutPassIssues.GetChildAt(i);
                if (viewIdx is CheckBox checkbox)
                {
                    if (checkbox.Checked)
                    {
                        if (tabanca.PastIssues.Contains(checkbox.Text) == false)
                        {
                            tabanca.PastIssues.Add(checkbox.Text);
                        }
                    }
                    else
                    {
                        tabanca.PastIssues.Remove(checkbox.Text);
                    }
                }

            }

            tabanca.CurrentIssues = tabanca.CurrentIssues ?? new List<string>();
            // CURRENT ISSUES
            var gridlayoutCurrentIssues = _view.FindViewById<GridLayout>(Resource.Id.gridLayout3);
            for (int i = 0; i < gridlayoutCurrentIssues.ChildCount; i++)
            {
                var viewIdx = gridlayoutCurrentIssues.GetChildAt(i);
                if (viewIdx is CheckBox checkbox)
                {
                    if (checkbox.Checked)
                    {
                        if (tabanca.CurrentIssues.Contains(checkbox.Text) == false)
                        {
                            tabanca.CurrentIssues.Add(checkbox.Text);
                        }
                    }
                    else
                    {
                        tabanca.CurrentIssues.Remove(checkbox.Text);
                    }
                }

            }

            // HANDLED
            //tabanca.MainEthnicGroupsInTabanca = tabanca.MainEthnicGroupsInTabanca ?? new List<string>();
            //tabanca.MainEthnicGroupsInTabanca

            // RELACOES ENTRE AS ETNIAS RESIDENTES NA TABANCA
            tabanca.RelationBetweenEthnicGroups = tabanca.RelationBetweenEthnicGroups ?? new List<string>();
            var gridlayoutRelatoinsBetweenEtnics = _view.FindViewById<GridLayout>(Resource.Id.gridLayoutRelationBetweenEthnics);
            for (int i = 0; i < gridlayoutRelatoinsBetweenEtnics.ChildCount; i++)
            {
                var viewIdx = gridlayoutRelatoinsBetweenEtnics.GetChildAt(i);
                if (viewIdx is CheckBox checkbox)
                {
                    if (checkbox.Checked)
                    {
                        if (tabanca.RelationBetweenEthnicGroups.Contains(checkbox.Text) == false && checkbox.Text.Equals("Se, quais são as causas", System.StringComparison.OrdinalIgnoreCase) == false)
                        {
                            tabanca.RelationBetweenEthnicGroups.Add(checkbox.Text);
                        }
                        else if (checkbox.Text.Equals("Se, quais são as causas", System.StringComparison.OrdinalIgnoreCase))
                        {
                            string textFromInputBox = "";
                            var textInput = _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditTextEthnicsInTabanca);

                            textFromInputBox = textInput.Text;

                            // only add if doesn't already exits!
                            if (tabanca.RelationBetweenEthnicGroups.Contains(textFromInputBox) == false)
                            {
                                tabanca.RelationBetweenEthnicGroups.Add(textFromInputBox);
                            }
                        }
                    }
                    else
                    {
                        if (checkbox.Text.Equals("Se, quais são as causas", System.StringComparison.OrdinalIgnoreCase) == false)
                        {
                            tabanca.RelationBetweenEthnicGroups.Remove(checkbox.Text);
                        }
                        else
                        {
                            // remove anything in tabanca.RelationBetweenEthnicGroups
                            // that is not "Boas" "Conflituosas", "Neutras"
                            for (int k = tabanca.RelationBetweenEthnicGroups.Count - 1; k >= 0; k--)
                            {
                                string valueAtIndex = tabanca.RelationBetweenEthnicGroups[k];
                                if ("Boas,Conflituosas,Neutras".IndexOf(valueAtIndex, System.StringComparison.OrdinalIgnoreCase) < 0)
                                {
                                    tabanca.RelationBetweenEthnicGroups.RemoveAt(i);
                                }
                            }
                        }
                    }
                }

            }

            // RELECOES COM AS COMUNIDADES VIZINHAS
            tabanca.RelationBetweenNearCommunities = tabanca.RelationBetweenNearCommunities ?? new List<string>();
            var gridLayoutRelationBetweenNearCommunities = _view.FindViewById<GridLayout>(Resource.Id.gridLayoutRelationBetweenNearCommunities);
            for (int i = 0; i < gridLayoutRelationBetweenNearCommunities.ChildCount; i++)
            {
                var viewIdx = gridLayoutRelationBetweenNearCommunities.GetChildAt(i);
                if (viewIdx is CheckBox checkbox)
                {
                    if (checkbox.Checked)
                    {
                        if (tabanca.RelationBetweenNearCommunities.Contains(checkbox.Text) == false && checkbox.Text.Equals("Se, quais são as causas", System.StringComparison.OrdinalIgnoreCase) == false)
                        {
                            tabanca.RelationBetweenNearCommunities.Add(checkbox.Text);
                        }
                        else if (checkbox.Text.Equals("Se, quais são as causas", System.StringComparison.OrdinalIgnoreCase))
                        {
                            string textFromInputBox = "";
                            var textInput = _view.FindViewById<TextInputEditText>(Resource.Id.textInputEditTextNearCommunity);

                            textFromInputBox = textInput.Text;

                            // only add if doesn't already exits!
                            if (tabanca.RelationBetweenNearCommunities.Contains(textFromInputBox) == false)
                            {
                                tabanca.RelationBetweenNearCommunities.Add(textFromInputBox);
                            }
                        }
                    }
                    else
                    {
                        if (checkbox.Text.Equals("Se, quais são as causas", System.StringComparison.OrdinalIgnoreCase) == false)
                        {
                            tabanca.RelationBetweenEthnicGroups.Remove(checkbox.Text);
                        }
                        else
                        {
                            // remove anything in tabanca.RelationBetweenEthnicGroups
                            // that is not "Boas" "Conflituosas", "Neutras"
                            for (int k = tabanca.RelationBetweenNearCommunities.Count - 1; k >= 0; k--)
                            {
                                string valueAtIndex = tabanca.RelationBetweenNearCommunities[k];
                                if ("Boas,Conflituosas,Neutras".IndexOf(valueAtIndex, System.StringComparison.OrdinalIgnoreCase) < 0)
                                {
                                    tabanca.RelationBetweenNearCommunities.RemoveAt(i);
                                }
                            }
                        }
                    }
                }

            }

            // actuais chefes da tabanca:
            // NOTE: SHOULD BE HANDLED WHEN THE BUTTON IS CLICKED.
        }
    }
}