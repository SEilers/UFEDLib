using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    internal class ApplicationUsage
    {
        #region fields

        public int ActivationCount { get; set; }

        public TimeSpan ActiveTime { get; set; }

        public TimeSpan BackgroundTime { get; set; }

        public DateTime Date { get; set; }

        public DateTime LastLaunch { get; set; }

        public TimeSpan LastUsageDuration { get; set; }

        public int LaunchCount { get; set; }

        public String Name { get; set; }

        #endregion
    }
}
