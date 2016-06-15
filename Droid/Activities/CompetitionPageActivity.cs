using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using Square.Picasso;
using Versus.Droid.Helpers;
using Versus.Portable.Data;
using Versus.Portable.Entities;

namespace Versus.Droid
{
    [Activity(Label = "Competition Page")]
    public class CompetitionPageActivity : Activity
    {
        private VsEntity _selectedEntity;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CompetitionPage);

            var competitionName = Intent.GetStringExtra("competitionName") ?? "";
            var competition = await FirebaseManager.Instance.GetCompetition(competitionName);

            if (competition != null)
            {
                //TODO: Do something with the competition
                var e1ImageButton = (ImageButton)FindViewById(Resource.Id.leftEntityButton);
                var e2ImageButton = (ImageButton)FindViewById(Resource.Id.rightEntityButton);
                var entityName = (TextView)FindViewById(Resource.Id.entityName);
                var votingButton = (Button)FindViewById(Resource.Id.votingButton);
                var entityDescription = (TextView)FindViewById(Resource.Id.entityDescription);
                FontsHelper.ApplyTypeface(Assets, new List<TextView> { entityName, entityDescription });

                var entities = await FirebaseManager.Instance.GetAllEntities();

                var entity1 = entities.Values.FirstOrDefault(entity => string.Equals(entity.Name, competition.CompetitorName1, StringComparison.CurrentCultureIgnoreCase));
                var entity2 = entities.Values.FirstOrDefault(entity => string.Equals(entity.Name, competition.CompetitorName2, StringComparison.CurrentCultureIgnoreCase));

                if (entity1 != null && entity2 != null)
                {
                    _selectedEntity = entity1;

                    // Load the image asynchonously
                    Picasso.With(this).Load(entity1.ImageUrl).Into(e1ImageButton);
                    Picasso.With(this).Load(entity2.ImageUrl).Into(e2ImageButton);
                    UpdateUiForSelectedEntity(entityName, entityDescription, votingButton);

                    votingButton.Click += (sender, args) =>
                    {
                        var index = _selectedEntity == entity1 ? 1 : 2;
                        FirebaseManager.Instance.UpdateVote(index, competitionName);
                        Toast.MakeText(this, "Casted a vote for " + _selectedEntity.Name, ToastLength.Short).Show();
                    };
                }

                e1ImageButton.Click += (sender, args) =>
                {
                    _selectedEntity = entity1;
                    UpdateUiForSelectedEntity(entityName, entityDescription, votingButton);
                };

                e2ImageButton.Click += (sender, args) =>
                {
                    _selectedEntity = entity2;
                    UpdateUiForSelectedEntity(entityName, entityDescription, votingButton);
                };
            }
        }

        private void UpdateUiForSelectedEntity(TextView entityName, TextView entityDescription, Button votingButton)
        {
            entityName.Text = _selectedEntity.Name;
            entityDescription.Text = _selectedEntity.Description;
            votingButton.Text = "VOTE FOR " + _selectedEntity.Name;
        }
    }
}

