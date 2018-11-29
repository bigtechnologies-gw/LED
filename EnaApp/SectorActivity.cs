using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace EnaApp
{
    [Activity(Label = "SectorActivity")]
    public class SectorActivity : Activity
    {

        private readonly Dictionary<string, List<string>> _regionMap = new Dictionary<string, List<string>>
        {
            {"bafata",  new List<string> {"Bafata", "Bambadinca", "Cuntuboel","Galomaro", "Gamamundo", "Xitole"}},
            {"biombo",  new List<string> {  "Prabis", "Quinhamel", "Safim"}},
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_sector);

            var relativeLayout = FindViewById<RelativeLayout>(Resource.Id.relativeLayoutParent);
            var gridLayout = relativeLayout.FindViewById<GridLayout>(Resource.Id.gridLayoutSectors);
            // already defined in design-mode
            //gridLayout.ColumnCount = 4;

            //var layoutParams = new GridLayout.LayoutParams();
            //layoutParams.
            //gridLayout.LayoutParameters = layoutParams;
            // https://stackoverflow.com/a/38438187/2766753
            //var layoutparam = gridLayout.LayoutParameters;
            //var layout= new RelativeLayout.LayoutParams(25, 25);
            //layout.AddRule(LayoutRules.Above, )

            string region = Intent.GetStringExtra("Region");

            List<string> result;
            // todo: string needs to be normalize before checking in map (e.g: bafata has accent accute)

            if (region == null || _regionMap.TryGetValue(region, out result) == false)
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Error selection region!");
                builder.SetMessage("Region not found!");
                builder.Show();
                return;
            }
            // convert int to dp
            // https://stackoverflow.com/questions/40819194/how-to-convert-dp-to-px-in-xamarin-android
            //var dp = 100;
            //int pixel = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, );
            foreach (var sector in result)
            {
                var button = new Button(this)// , Android.Runtime.JniHandleOwnership)
                {
                    Text = sector,
                };
                button.TextSize = 24;
                button.SetWidth(500);
                button.TextAlignment = /*TextAlignment.Center | */TextAlignment.ResolvedDefault;
                //button.SetMinWidth(150) ;/*TypedValue.ApplyDimension(ComplexUnitType.Dip, 20, DisplayMetrics.)*/
                button.Click += (sender, e) =>
                {
                    var intent = new Intent(this, typeof(ChooseCommunity));
                    StartActivity(intent);
                };
                gridLayout.AddView(button);
                //this.AddContentView(button, Viewgou)
            }
            

            // returns => quinara/tombali....
            //string clickedRegion = Intent.GetStringExtra("Region");
            //var listSectors = _regionMap[clickedRegion];
            //var regions = (Regions)Intent.GetSerializableExtra("sectos");
            //buttonContext.Text = $"You clicked: { regions.First().Name}";
            // Create your application here
            //FindViewById<Button>(Resource.Id.buttonSector).Text = listSectors.First();
        }

        public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {
            return base.OnCreateView(parent, name, context, attrs);
            // NOTE: infinity loop! Why?
            //var inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            //return inflater.Inflate(Resource.Layout.activity_community, null);
        }

    }
}