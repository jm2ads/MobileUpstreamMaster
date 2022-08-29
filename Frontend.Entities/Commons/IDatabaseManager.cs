using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Commons
{
    public interface IDatabaseManager
    {
        void InitDB();
        Task ResetDB();
        Task ResetDB(IList<Type> typesToResert);
    }
}
