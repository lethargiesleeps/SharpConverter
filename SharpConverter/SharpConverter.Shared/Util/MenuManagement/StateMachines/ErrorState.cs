using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util.MenuManagement.StateMachines
{
    public enum ErrorState
    {
        None,
        Default,
        NSC,
        IEEE,
        Unicode,
        NullValue
    }
}
