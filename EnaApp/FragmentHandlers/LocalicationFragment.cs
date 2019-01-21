
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace EnaApp
{
    public class LocalicationFragment : Fragment
    {
        public View MyView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            MyView = inflater.Inflate(Resource.Layout.home, container, false);


            // restore user choices
            if (CreateTabancaActivity.NewTabanca != null)
            {
                MyView.FindViewById<EditText>(Resource.Id.textInputEditText1).Text = CreateTabancaActivity.NewTabanca.Name;

                if (CreateTabancaActivity.NewTabanca.Location != null)
                {
                    var viewGroup = (ViewGroup)MyView;
                    int totalChildren = ((ViewGroup)MyView).ChildCount;
                    for (int i = 0; i < totalChildren; i++)
                    {
                        var childView = viewGroup.GetChildAt(i);

                        if (childView is CheckBox childCheckbox) // pattern matching
                        {
                            var @value = childCheckbox.Text;
                            childCheckbox.Checked = CreateTabancaActivity.NewTabanca.Location.Contains(value);
                        }
                    }
                }
            }


            return MyView;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            CreateTabancaActivity.NewTabanca.Name = MyView.FindViewById<EditText>(Resource.Id.textInputEditText1).Text;

            var viewGroup = (ViewGroup)MyView;

            int totalChildren = ((ViewGroup)MyView).ChildCount;

            CreateTabancaActivity.NewTabanca.Location = new List<string>();

            for (int i = 0; i < totalChildren; i++)
            {
                var childView = viewGroup.GetChildAt(i);

                if (childView is CheckBox childCheckbox && childCheckbox.Checked) // pattern matching
                {
                    CreateTabancaActivity.NewTabanca.Location.Add(childCheckbox.Text);
                }
            }
            
            System.Diagnostics.Debug.WriteLine(CreateTabancaActivity.NewTabanca.Name);
        }
    }


}