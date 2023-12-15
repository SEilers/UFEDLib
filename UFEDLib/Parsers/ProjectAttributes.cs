using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFEDLib
{
    [Serializable]
    public class ProjectAttributes
    {
        // Image File Attributes
        public String FileName { get; set; }

        public double FileSizeInBytes { get; set; }

        // UFED Project Attributes

        public String ProjectName { get; set; }

        public string ProjectId { get; set; }

        public String ReportVersion { get; set; }

        public int NodeCount { get; set; }

        public int ModelCount { get; set; }

        public String SourceExtractionsDeviceName { get; set; }

        public String SourceExtractionsFullName { get; set; }

        public List<Tuple<string, string>> CaseInformation { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> AdditionalFields { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> ExtractionData { get; set; } = new List<Tuple<string, string>>();

        public List<Tuple<string, string>> DeviceInfo { get; set; } = new List<Tuple<string, string>>();
    }
}
