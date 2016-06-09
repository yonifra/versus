using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Versus.Portable.Data;

namespace Versus.Droid.Activities
{
    [Activity (Label = "Categories", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected async override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var categoriesListView = FindViewById<ListView> (Resource.Id.categoriesListView);
            var categories = await FirebaseManager.Instance.GetAllCategories ();
            categoriesListView.Adapter = new CategoriesListAdapter (this, categories.Values);

            categoriesListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                var index = e.Position;

                var lv = (sender as ListView);

                if (lv != null) {
                    var category = (lv.Adapter as CategoriesListAdapter).Categories [index];
                    Toast.MakeText (this, category.Name + " clicked", ToastLength.Short).Show ();

                    // Put the name of the selected category into the intent
                    var competitionsActivity = new Intent (this, typeof (CompetitionsActivity));
                    competitionsActivity.PutExtra ("categoryName", category.Name);

                    // Start the competitions activity
                    StartActivity (competitionsActivity);
                } else {
                    Toast.MakeText (this, "Item " + e.Position + " clicked", ToastLength.Short).Show ();
                }
            };
        }
    }
}


