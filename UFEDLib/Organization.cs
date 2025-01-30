using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFEDLib.Models;

namespace UFEDLib
{
    [Serializable]
    public class Organization : ModelBase
    {
        #region fields
        /// <summary>
        /// Oragnization name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contact’s position in the organization
        /// </summary>
        public string Position { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion

    }
}
