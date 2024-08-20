using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib.Models
{
    [Serializable]
    public class Password
    {
        #region fields
        public string AccessGroup { get; set; }
        public string Account { get; set; }
        // The password itself
        public string Data { get; set; }
        public string GenericAttribute { get; set; }
        public string Label { get; set; }
        public string Server { get; set; }
        public string Service { get; set; }

        // enum with the following values: "Default", "Key", "Secret", "Token"
        public string Type { get; set; }
        #endregion

        #region models
        #endregion

        #region multiModels
        #endregion
    }
}
