using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Versus.Droid.Adapters;
using Versus.Portable.Data;
using Versus.Portable.Entities;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Versus.Droid.Activities
{
    [Activity(Label = "Active Competitions")]
    public class CompetitionsActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.fragment_competitions;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var categoryName = Intent.GetStringExtra("categoryName") ?? "";
            IEnumerable<VsCompetition> competitions;

            if (categoryName == "")
            {
                Toast.MakeText(this, "Could not resolve category name", ToastLength.Short).Show();
                var all = await FirebaseManager.Instance.GetAllCompetitions();
                competitions = all.Values;
            }
            else
            {
                Toast.MakeText(this, "Found category " + categoryName, ToastLength.Short).Show();
                competitions = await FirebaseManager.Instance.GetCompetitions(categoryName);
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.fragment_competitions);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            //  ActionBar.Title = competitionName;
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = categoryName + " competitions";

            var competitionsListView = FindViewById<ListView>(Resource.Id.competitionsListView);

            competitionsListView.Adapter = new CompetitionListAdapter(this, competitions);

            competitionsListView.ItemClick += (sender, e) =>
            {
                var index = e.Position;

                var lv = sender as ListView;

                if (lv != null)
                {
                    var competitionListAdapter = lv.Adapter as CompetitionListAdapter;

                    if (competitionListAdapter != null)
                    {
                        var competition = competitionListAdapter.Competitions[index];
                        //  Toast.MakeText (this, competition.Name + " clicked", ToastLength.Short).Show ();

                        // Put the name of the selected category into the intent
                        var competitionActivity = new Intent(this, typeof(CompetitionPageActivity));
                        competitionActivity.PutExtra("competitionName", competition.Name);

                        // Start the competitions activity
                        StartActivity(competitionActivity);
                    }
                }
                else
                {
                    Toast.MakeText(this, "Item " + e.Position + " clicked", ToastLength.Short).Show();
                }
            };
        }
    }
}

