using System;
using Versus.Portable.Entities;

namespace Versus.Droid
{
    public class CompetitionPageFragment : Android.Support.V4.App.Fragment
    {
        public CompetitionPageFragment ()
        {
        }

        public VsCompetition Competition { get; set; }
    }
}

