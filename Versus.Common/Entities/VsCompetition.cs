using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Versus.Common.Base;
using Versus.Common.Enums;

namespace Versus.Common.Entities
{
    public class VsCompetition : IVsCompetition
    {
        public DateTime EndingDate { get; set; }
        public Dictionary<IVsEntity, int> Leaderboard { get; set; }

        public VsCompetition()
        {
            Leaderboard = new Dictionary<IVsEntity, int>();
        }

        public VsCompetition(IEnumerable<IVsEntity> competitors, DateTime endingDate)
        {
            // Initialize the leaderboard
            foreach (var c in competitors)
            {
                Leaderboard.Add(c, 0);
            }

            EndingDate = endingDate;
        }

        public DateTime GetEndingDate()
        {
            return EndingDate;
        }

        public bool UpdateVoteCount(int entityId, VoteType voteType, int userId)
        {
            if (Leaderboard.All(competitor => competitor.Key.GetId() != entityId))
            {
                return false;
            }

            // TODO: Check that the userId exist

            var item = Leaderboard.Keys.First(entity => entity.GetId() == entityId);

            switch (voteType)
            {
                case VoteType.VoteUp:
                    Leaderboard[item]++;
                    break;
                case VoteType.VoteDown:
                    Leaderboard[item]--;
                    break;
            }

            return true;
        }

        public IVsEntity GetLeadingEntity()
        {
            var currentHigh = Leaderboard.First().Value;
            var winner = Leaderboard.First().Key;

            foreach (var item in Leaderboard)
            {
                if (item.Value > currentHigh)
                {
                    winner = item.Key;
                }
                else if (item.Value == currentHigh && item.Key != winner)
                {
                    // we have a tie
                    return null;
                }
            }

            return winner;
        }
    }
}
