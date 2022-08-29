using System;
using System.Collections.Generic;

namespace Frontend.Business.Settings
{
    public interface ISetting
    {
        int Id { get; }
        string UserName { get; set; }

        string UserLogin { get; set; }

        string Token { get; set; }

        string ApplicationName { get; set; }
        DateTime LastSync { get; set; }

        DateTime LastSolicitudesSync { get; set; }

    }
}
