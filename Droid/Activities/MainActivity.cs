using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Versus.Droid.Fragments;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Versus.Droid.Activities
{
    [Activity (Label = "Versus", LaunchMode = Android.Content.PM.LaunchMode.SingleTop, MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : BaseActivity
    {
      //  DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override int LayoutResource => Resource.Layout.Main;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            //     SetContentView (Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);

            //  ActionBar.Title = competitionName;
            SetSupportActionBar (toolbar);
            SupportActionBar.Title = "Versus";

            navigationView = FindViewById<NavigationView> (Resource.Id.nav_view);
            navigationView.NavigationItemSelected += (sender, e) => {
                e.MenuItem.SetChecked (true);

                switch (e.MenuItem.ItemId) {
                case Resource.Id.nav_home:
                    ListItemClicked (0);
                    break;
                case Resource.Id.nav_search:
                    ListItemClicked (1);
                    break;
                case Resource.Id.nav_profile:
                    ListItemClicked (2);
                    break;
                }
            };

            //if first time you will want to go ahead and click first item.
            if (bundle == null) {
                ListItemClicked (0);
            }
        }

        private void ListItemClicked (int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position) {
            case 0:
                fragment = new CategoriesFragment ();
                break;
            default:
                fragment = new CategoriesFragment ();
                break;
            }

            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content_frame, fragment)
                .Commit ();
        }
    }
}


