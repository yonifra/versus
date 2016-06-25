using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Versus.Droid.Activities;
using Versus.Droid.Adapters;
using Versus.Portable.Data;
using Versus.Portable.Entities;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Versus.Droid
{
    [Activity (Label = "Active Competitions")]
    public class CompetitionsActivity : BaseActivity
    {
        protected override int LayoutResource {
            get {
                return Resource.Layout.fragment_competitions;
            }
        }

        protected async override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
        }
    }
}

