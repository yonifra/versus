using System.Collections.Generic;
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
    public class CompetitionsFragment : Android.Support.V4.App.Fragment
    {
        private const string LOG_TAG = "COMPETITIONS_FRAGMENT";
        private IEnumerable<VsCompetition> _competitions;
        private View _view;

        public CompetitionsFragment()
        {
            RetainInstance = true;
            _competitions = new List<VsCompetition>();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            _view = inflater.Inflate(Resource.Layout.fragment_competitions, null);

            LoadDataToGridAsync(_view);

            return _view;
        }

        private async void LoadDataToGridAsync(View view)
        {
            await GetAllCategoriesAsync(SelectedCategory);

            var competitionsListView = view.FindViewById<ListView>(Resource.Id.competitionsListView);
            competitionsListView.Adapter = new CompetitionListAdapter(Activity, _competitions);

            competitionsListView.ItemClick += (sender, e) =>
            {
                var index = e.Position;

                var lv = (sender as ListView);

                if (lv != null)
                {
                    var competitionListAdapter = lv.Adapter as CompetitionListAdapter;
                    var competition = competitionListAdapter?.Competitions[index];

                    if (competition != null)
                    {
                        // Put the name of the selected category into the intent
                        var fragment = new CompetitionPageFragment { Competition = competition };

                        Activity.SupportFragmentManager.BeginTransaction()
                            .Replace(Resource.Id.content_frame, fragment)
                            .Commit();
                    }
                }
                else
                {
                    Snackbar.Make(_view, "Item " + e.Position + " clicked", Snackbar.LengthShort).Show();
                }
            };
        }

        private async Task GetAllCategoriesAsync(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                Log.Debug(LOG_TAG, "Could not resolve category name");
                var all = await FirebaseManager.Instance.GetAllCompetitions();
                _competitions = all.Values;
            }
            else
            {
                Log.Debug(LOG_TAG, "Found category " + categoryName);
                _competitions = await FirebaseManager.Instance.GetCompetitions(categoryName);
            }
        }

        public string SelectedCategory { get; set; }
    }
}

