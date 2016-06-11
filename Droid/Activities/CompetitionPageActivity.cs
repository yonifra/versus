
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Square.Picasso;
using Versus.Portable.Data;

namespace Versus.Droid
{
    [Activity (Label = "Competition Page")]
    public class CompetitionPageActivity : Activity
    {
        protected async override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.CompetitionPage);

            var competitionName = Intent.GetStringExtra ("competitionName") ?? "";
            var competition = await FirebaseManager.Instance.GetCompetition (competitionName);

            if (competition != null) {
                //TODO: Do something with the competition

                var e1Image = (ImageView)FindViewById (Resource.Id.entity1ImageView);
                var e1Name = (TextView)FindViewById (Resource.Id.entity1Name);
                var e2Image = (ImageView)FindViewById (Resource.Id.entity2ImageView);
                var e2Name = (TextView)FindViewById (Resource.Id.entity2Name);


                // Load the image asynchonously
                Picasso.With (this).Load (competition.BackdropUrl).Into (e1Image);
                Picasso.With (this).Load (competition.BackdropUrl).Into (e2Image);
                e1Name.Text = "Entity 1";
                e2Name.Text = "Entity 2";
            }
        }
    }
}

