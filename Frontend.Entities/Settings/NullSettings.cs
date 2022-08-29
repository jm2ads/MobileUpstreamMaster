using System;
using Frontend.Commons.Commons;

namespace Frontend.Business.Settings
{
    public class NullSettings : ISetting
    {
        public int Id { get { return 0; } }

        public string UserName { get { return String.Empty; } set { } }

        public string UserLogin { get { return String.Empty; } set { } }

        public string Token { get { return String.Empty; } set { } }

        public string ApplicationName { get { return ApplicationConstants.ApplicationName; } set { } }

        public DateTime LastSync { get { return new DateTime(2017, 01, 01); } set { } }

        public DateTime LastSolicitudesSync { get { return new DateTime(2017, 01, 01); } set { } }
    }
}
