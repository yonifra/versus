
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Versus.Droid.Adapters;
using Versus.Portable.Data;
using Versus.Portable.Entities;

namespace Versus.Droid
{
    [Activity (Label = "Active Competitions")]
    public class CompetitionsActivity : Activity
    {
        protected async override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            var categoryName = Intent.GetStringExtra ("categoryName") ?? "";
            IEnumerable<VsCompetition> competitions;

            if (categoryName == "") {
                Toast.MakeText (this, "Could not resolve category name", ToastLength.Short).Show ();
                var all = await FirebaseManager.Instance.GetAllCompetitions ();
                competitions = all.Values;
            } else {
                Toast.MakeText (this, "Found category " + categoryName, ToastLength.Short).Show ();
                competitions = await FirebaseManager.Instance.GetCompetitions (categoryName);
            }

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Competitions);

            var competitionsListView = FindViewById<ListView> (Resource.Id.competitionsListView);

            competitionsListView.Adapter = new CompetitionListAdapter (this, competitions);

            competitionsListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                var index = e.Position;

                var lv = (sender as ListView);

                if (lv != null) {
                    var competition = (lv.Adapter as CompetitionListAdapter).Competitions [index];
                    Toast.MakeText (this, competition.Name + " clicked", ToastLength.Short).Show ();
                    // TODO: Start an activity with the selected competition.
                } else {
                    Toast.MakeText (this, "Item " + e.Position + " clicked", ToastLength.Short).Show ();
                }
            };
        }
    }
}

