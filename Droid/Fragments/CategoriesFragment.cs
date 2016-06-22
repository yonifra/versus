﻿using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Versus.Portable.Data;
using Versus.Portable.Entities;

namespace Versus.Droid
{
    public class CategoriesFragment : Android.Support.V4.App.Fragment
    {
        Dictionary<string, Category> _categories;

        public CategoriesFragment ()
        {
            RetainInstance = true;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView (inflater, container, savedInstanceState);

            var view = inflater.Inflate (Resource.Layout.fragment_categories, null);

            var categoriesListView = view.FindViewById<ListView> (Resource.Id.categoriesListView);

            if (_categories == null) {
                GetAllCategoriesAsync ();
            }

            categoriesListView.Adapter = new CategoriesListAdapter (Activity, _categories.Values);

            categoriesListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
                var index = e.Position;

                var lv = (sender as ListView);

                if (lv != null) {
                    var category = (lv.Adapter as CategoriesListAdapter).Categories [index];

                    // Put the name of the selected category into the intent
                    var competitionsActivity = new Intent (Activity, typeof (CompetitionsActivity));
                    competitionsActivity.PutExtra ("categoryName", category.Name);

                    // Start the competitions activity
                    StartActivity (competitionsActivity);
                } else {

#if DEBUG
                    Snackbar.Make (view, "Item " + e.Position + " clicked", Snackbar.LengthShort).Show ();
#endif
                }
            };

            return view;
        }

        private async void GetAllCategoriesAsync ()
        {
            _categories = await FirebaseManager.Instance.GetAllCategories ();
        }
    }
}

