﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Versus.Droid.Fragments;

namespace Versus.Droid.Activities
{
    [Activity(Label = "Versus", LaunchMode = LaunchMode.SingleTop, MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : BaseActivity
    {
        private NavigationView _navigationView;
        private DrawerLayout _drawerLayout;
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private Android.Widget.ShareActionProvider _shareActionProvider;

        protected override int LayoutResource => Resource.Layout.Main;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar> (Resource.Id.toolbar);


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

        public override bool OnPrepareOptionsMenu (Android.Views.IMenu menu)
        {
            menu.Clear ();

            // Check if the toolbar is initialized, and if so, inflate the menu onto it
            if (_toolbar != null) {
                MenuInflater.Inflate (Resource.Menu.menu, menu);

                // Locate MenuItem with ShareActionProvider
                var item = menu.FindItem (Resource.Id.menu_item_share);

                // Fetch and store ShareActionProvider

                //TODO: Fix this to get the action provider for the share!
               // _shareActionProvider = (Android.Widget.ShareActionProvider)item.ActionProvider;
            }

            return base.OnPrepareOptionsMenu (menu);
        }

        // Call to update the share intent
        private void setShareIntent (Intent shareIntent)
        {
            if (_shareActionProvider != null) {
                _shareActionProvider.SetShareIntent (shareIntent);
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
                case Resource.Id.nav_logoutLogin:
                    fragment = new LoginFragment ();
                    break;
                default:
                    fragment = new CategoriesFragment();
                    break;
            }

            // Make the actual change of fragments
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}


