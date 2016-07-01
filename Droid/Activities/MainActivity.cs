using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Versus.Droid.Fragments;

namespace Versus.Droid.Activities
{
    [Activity(Label = "Versus", LaunchMode = LaunchMode.SingleTop, MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : BaseActivity
    {
        private NavigationView _navigationView;
        private DrawerLayout _drawerLayout;

        protected override int LayoutResource => Resource.Layout.Main;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);

            _navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                ListItemClicked (e.MenuItem.ItemId);
            };

            //if first time you will want to go ahead and click first item.
            if (bundle == null)
            {
                ListItemClicked(0);
            }
        }

        private void ListItemClicked(int itemId, bool shouldCloseDrawer = true)
        {
            Android.Support.V4.App.Fragment fragment;

            // Close the drawer if item was clicked (configurable)
            if (_drawerLayout != null && shouldCloseDrawer) {
                _drawerLayout.CloseDrawers ();
            }

            // Depending on the item ID that was selected in the drawer,
            // replace the fragment with the corresponding fragment
            switch (itemId)
            {
                case Resource.Id.nav_home:
                    fragment = new CategoriesFragment();
                    break;
            case Resource.Id.nav_about:
                fragment = new AboutFragment ();
                break;
                default:
                    fragment = new CategoriesFragment();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}


