using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versus.Common.Base
{
    public interface IUser
    {
        bool UpdatePassword(string newPassword);
    }
}
