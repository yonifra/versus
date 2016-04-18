using System;
using Versus.Common.Enums;

namespace Versus.Common.Base
{
    public interface IVsCompetition
    {
        DateTime GetEndingDate();
        bool UpdateVoteCount(int entityId, VoteType voteType, int userId);
        IVsEntity GetLeadingEntity();
    }
}
