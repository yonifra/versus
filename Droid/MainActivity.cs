using Android.App;
using Android.OS;
using Android.Widget;
using Versus.Droid.Adapters;
using Versus.Portable.Data;

namespace Versus.Droid
{
    [Activity (Label = "Versus", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var competitionsListView = FindViewById<ListView> (Resource.Id.competitionsListView);
            var competitions = await FirebaseManager.Instance.GetAllCompetitions ();
            competitionsListView.Adapter = new CompetitionListAdapter (this, competitions.Values);
        }
    }
}


