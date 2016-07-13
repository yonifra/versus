using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Versus.Droid.Adapters;
using Versus.Portable.Data;
using Versus.Portable.Entities;

namespace Versus.Droid.Fragments
{
    public class TrendingCompetitionsFragment : Android.Support.V4.App.Fragment
    {
        private const string LOG_TAG = "TRENDING_COMPETITIONS_FRAGMENT";
        private IEnumerable<VsCompetition> _trendingCompetitions;
        private View _view;

        public TrendingCompetitionsFragment ()
        {
            RetainInstance = true;
            _trendingCompetitions = new List<VsCompetition> ();
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            _view = inflater.Inflate (Resource.Layout.trending_fragment, null);

            LoadDataToGridAsync (_view);

            return _view;
        }

        private async void LoadDataToGridAsync (View view)
        {
            await GetTrendingCompetitionsAsync ();

            if (_trendingCompetitions != null && _trendingCompetitions.Any ())
            {
                var textView = view.FindViewById<TextView> (Resource.Id.noTrendingCompetitionsMessage);
                textView.Visibility = ViewStates.Invisible;
            }
            else 
            {
                Snackbar.Make (_view, "No trending competitions found", Snackbar.LengthShort).Show ();
                return;
            }

            var competitionsListView = view.FindViewById<ListView> (Resource.Id.trendingCompetitionsListView);
            competitionsListView.Visibility = ViewStates.Visible;
            competitionsListView.Adapter = new CompetitionListAdapter (Activity, _trendingCompetitions);

            competitionsListView.ItemClick += (sender, e) =>
            {
                var index = e.Position;

                var lv = (sender as ListView);

                if (lv != null)
                {
                    var competitionListAdapter = lv.Adapter as CompetitionListAdapter;
                    var competition = competitionListAdapter?.Competitions [index];

                    if (competition != null)
                    {
                        // Put the name of the selected category into the intent
                        var fragment = new CompetitionPageFragment { Competition = competition };

                        Activity.SupportFragmentManager.BeginTransaction ()
                            .Replace (Resource.Id.content_frame, fragment)
                            .AddToBackStack (fragment.Tag)
                            .Commit ();
                    }
                }
                else
                {
                    Snackbar.Make (_view, "Item " + e.Position + " clicked", Snackbar.LengthShort).Show ();
                }
            };
        }

        private async Task GetTrendingCompetitionsAsync ()
        {
                Log.Debug (LOG_TAG, "Fetching trending competitions");
            var trending = await FirebaseManager.Instance.GetTrendingCompetitions ();

            _trendingCompetitions = trending.Values;
        }
    }
}

