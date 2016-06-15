using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using Versus.Droid.Helpers;
using Versus.Portable.Entities;

namespace Versus.Droid.Adapters
{
    class CompetitionAdapterWrapper : Java.Lang.Object
    {
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public ImageView Backdrop { get; set; }
    }

    class CompetitionListAdapter : BaseAdapter
    {
        private readonly Activity _context;
        private readonly IEnumerable<VsCompetition> _competitions;

        public CompetitionListAdapter(Activity context, IEnumerable<VsCompetition> competitions)
        {
            _context = context;
            _competitions = competitions;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (position < 0)
                return null;

            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CompetitionListItemLayout, parent, false);

            if (view == null)
                return null;

            var wrapper = view.Tag as CompetitionAdapterWrapper;
            if (wrapper == null)
            {
                wrapper = new CompetitionAdapterWrapper
                {
                    Name = view.FindViewById<TextView>(Resource.Id.compNameTextView),
                    Description = view.FindViewById<TextView>(Resource.Id.compDescriptionTextView),
                    Backdrop = view.FindViewById<ImageView>(Resource.Id.compBackdropImageView)
                };
                view.Tag = wrapper;
            }

            var competition = _competitions.ElementAt(position);

            wrapper.Backdrop.SetBackgroundResource(Android.Resource.Color.Transparent);
            wrapper.Name.Text = competition.Name;
            wrapper.Description.Text = competition.Description;

            FontsHelper.ApplyTypeface(_context.Assets, new List<TextView> { wrapper.Name, wrapper.Description });

            // Load the image asynchonously
            Picasso.With(_context).Load(competition.BackdropUrl).Into(wrapper.Backdrop);

            return view;
        }

        public override int Count => _competitions.Count();

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public List<VsCompetition> Competitions => _competitions.ToList();

        public override long GetItemId(int position)
        {
            return position;
        }

        public override bool HasStableIds => true;
    }
}

