using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Widget;
using Square.Picasso;
using Versus.Droid.Helpers;
using Versus.Portable.Data;
using Versus.Portable.Entities;

namespace Versus.Droid.Fragments
{
    public class CompetitionPageFragment : Android.Support.V4.App.Fragment
    {
        private VsEntity _selectedEntity;

        public CompetitionPageFragment ()
        {
            RetainInstance = true;
        }

        private void UpdateUiForSelectedEntity (TextView entityName, TextView entityDescription, TextView votingButton)
        {
            entityName.Text = _selectedEntity.Name;
            entityDescription.Text = _selectedEntity.Description;
            votingButton.Text = "VOTE FOR " + _selectedEntity.Name;
        }

        public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            base.OnCreateView (inflater, container, savedInstanceState);

            //var view = inflater.Inflate(Resource.Layout.fragment_categories, container, false);
            var view = inflater.Inflate (Resource.Layout.fragment_competitionDetails, null);
            PopulateDataAsync (view);

            return view;
        }

        async void PopulateDataAsync (Android.Views.View view)
        {
            var competition = await FirebaseManager.Instance.GetCompetition (Competition.Name);
            var parentView = View.FindViewById<FrameLayout> (Resource.Id.parentLayout);

            if (competition != null) {
                var e1ImageButton = view.FindViewById<ImageButton> (Resource.Id.leftEntityButton);
                var e2ImageButton = view.FindViewById<ImageButton> (Resource.Id.rightEntityButton);
                var entityName = view.FindViewById<AppCompatTextView> (Resource.Id.entityName);
                var votingButton = view.FindViewById<AppCompatButton> (Resource.Id.votingButton);
                var entityDescription = view.FindViewById<AppCompatTextView> (Resource.Id.entityDescription);
                FontsHelper.ApplyTypeface (view.Context.Assets, new List<TextView> { entityName, entityDescription });

                var entities = await FirebaseManager.Instance.GetAllEntities ();

                var entity1 = entities.Values.FirstOrDefault (entity => string.Equals (entity.Name, competition.CompetitorName1, StringComparison.CurrentCultureIgnoreCase));
                var entity2 = entities.Values.FirstOrDefault (entity => string.Equals (entity.Name, competition.CompetitorName2, StringComparison.CurrentCultureIgnoreCase));

                if (entity1 != null && entity2 != null) {
                    _selectedEntity = entity1;

                    // Load the image asynchonously
                    Picasso.With (view.Context).Load (entity1.ImageUrl).Into (e1ImageButton);
                    Picasso.With (view.Context).Load (entity2.ImageUrl).Into (e2ImageButton);
                    UpdateUiForSelectedEntity (entityName, entityDescription, votingButton);

                    votingButton.Click += (sender, args) => {
                        var index = _selectedEntity == entity1 ? 1 : 2;
                        FirebaseManager.Instance.UpdateVote (index, Competition.Name);
                        Snackbar.Make (parentView, "Casted a vote for " + _selectedEntity.Name, Snackbar.LengthLong).Show ();
                    };
                }

                e1ImageButton.Click += (sender, args) => {
                    _selectedEntity = entity1;
                    UpdateUiForSelectedEntity (entityName, entityDescription, votingButton);
                };

                e2ImageButton.Click += (sender, args) => {
                    _selectedEntity = entity2;
                    UpdateUiForSelectedEntity (entityName, entityDescription, votingButton);
                };
            }
        }

        public VsCompetition Competition { get; set; }
    }
}

