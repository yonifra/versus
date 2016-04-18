using System.Collections.Generic;

namespace Versus.Common.Base
{
    public interface IVsEntity
    {
        string GetEntityName();
        List<IUser> GetFollowers();
        int GetId();
    }
}
