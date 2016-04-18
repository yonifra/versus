using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Versus.Common.Base
{
    public interface IVsEntity
    {
        string GetEntityName();
        List<IUser> GetFollowers();
        int GetId();
    }
}
