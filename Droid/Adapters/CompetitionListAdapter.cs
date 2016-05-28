using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace Versus.Droid
{
    internal class CompetitionAdapterWrapper : Java.Lang.Object
    {
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public ImageView Backdrop { get; set; }
    }

    class CompetitionListAdapter : BaseAdapter
    {
        private readonly Activity context;
        private readonly IEnumerable<VsCompetition> _competitions;

        public CompetitionListAdapter (Activity context, IEnumerable<VsCompetition> competitions)
        {
            this.context = context;
            this._competitions = competitions;
        }

        public override View GetView (int position, View convertView, ViewGroup parent)
        {
            if (position < 0)
                return null;

            var view = (convertView
                       ?? context.LayoutInflater.Inflate (
                    Resource.Layout.CompetitionListItemLayout, parent, false)
                       );

            if (view == null)
                return null;

            var wrapper = view.Tag as CompetitionAdapterWrapper;
            if (wrapper == null) {
                wrapper = new CompetitionAdapterWrapper {
                    Title = view.FindViewById<TextView> (Resource.Id.item_title),
                    Art = view.FindViewById<ImageView> (Resource.Id.item_image)
                };
                view.Tag = wrapper;
            }

            var movie = _competitions.ElementAt (position);

            wrapper.Title.SetBackgroundResource (Android.Resource.Color.Transparent);
            wrapper.Title.Text = movie.Title;

            // Load the image asynchonously
            Picasso.With (context).Load (movie.PosterUrl).Into (wrapper.Art);

            return view;
        }

        public override int Count {
            get { return _competitions.Count (); }
        }

        public override Java.Lang.Object GetItem (int position)
        {
            return position;
        }

        public override long GetItemId (int position)
        {
            return position;
        }

        public override bool HasStableIds {
            get {
                return true;
            }
        }
    }
}

