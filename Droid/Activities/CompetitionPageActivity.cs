
using System;
using System.Linq;
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
                var e1Button = (Button)FindViewById (Resource.Id.entity1Button);
                var e2Button = (Button)FindViewById (Resource.Id.entity2Button);
                var e1Desc = (TextView)FindViewById (Resource.Id.entity1Description);
                var e2Desc = (TextView)FindViewById (Resource.Id.entity2Description);

                var entities = await FirebaseManager.Instance.GetAllEntities ();

                var entity1 = entities.Values.FirstOrDefault (entity => entity.Name.ToLower () == competition.CompetitorName1.ToLower());
                var entity2 = entities.Values.FirstOrDefault (entity => entity.Name.ToLower () == competition.CompetitorName2.ToLower());


                if (entity1 != null && entity2 != null) {

                    // Load the image asynchonously
                    Picasso.With (this).Load (entity1.ImageUrl).Into (e1Image);
                    Picasso.With (this).Load (entity2.ImageUrl).Into (e2Image);
                    e1Name.Text = entity1.Name;
                    e2Name.Text = entity2.Name;
                    e1Desc.Text = entity1.Description;
                    e2Desc.Text = entity2.Description;

                    e1Button.Click += (object sender, EventArgs e) => {
                        FirebaseManager.Instance.UpdateVote (1, competitionName);
                    };

                    e2Button.Click += (object sender, EventArgs e) => {
                        FirebaseManager.Instance.UpdateVote (2, competitionName);
                };
                }

            }
        }
    }
}

