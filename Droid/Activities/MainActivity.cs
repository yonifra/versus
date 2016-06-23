using Android.App;
using Android.OS;
using Versus.Droid.Fragments;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Versus.Droid.Activities
{
    [Activity (Label = "Versus", LaunchMode = Android.Content.PM.LaunchMode.SingleTop, MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.Main;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);

            //  ActionBar.Title = competitionName;
            SetSupportActionBar (toolbar);
            SupportActionBar.Title = "Versus";
        }

        private void ListItemClicked (int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position) {
            case 0:
                fragment = new CategoriesFragment ();
                break;
            case 1:
          //      fragment = new ByCategoryFragment ();
                break;
            case 2:
     //           fragment = new SearchFragment ();
                break;
            }

            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, fragment)
                .Commit ();
        }
    }
}


