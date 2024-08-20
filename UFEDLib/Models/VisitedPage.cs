using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class VisitedPage
    {
        #region fields
        public string LastVisited { get; set; }

        public string Source { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int VisitCount { get; set; }
        #endregion




        

        

        

        
    }
}
