
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Versus.Portable.Data;

namespace Versus.Droid
{
    [Activity (Label = "Competition Page")]
    public class CompetitionPageActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            var competitionName = Intent.GetStringExtra ("competitionName") ?? "";
            var competition = FirebaseManager.Instance.GetCompetition (competitionName);

            if (competition != null) {
                //TODO: Do something with the competition
            }
        }
    }
}

