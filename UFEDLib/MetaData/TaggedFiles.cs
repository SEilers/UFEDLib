using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace UFEDLib.MetaData
{
    public class TaggedFiles
    {
        public static List<TaggedFile> Parse(string fileName)
        {
            if (fileName.EndsWith(".ufdr", StringComparison.OrdinalIgnoreCase))
            {
                using (var zip = ZipFile.OpenRead(fileName))
                {
                    var report = zip.GetEntry("report.xml");

                    if (report == null)
                        throw new FileNotFoundException(
                            "report.xml not found in the ufdr file");

                    using (var reportStream = report.Open())
                    {
                        return ParseTaggedFiles(reportStream);
                    }
                }
            }

            if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                using (var fs = File.OpenRead(fileName))
                {
                    return ParseTaggedFiles(fs);
                }
            }

            throw new NotSupportedException(
                "Unsupported file type: " + fileName);
        }

        private static List<TaggedFile> ParseTaggedFiles(Stream stream)
        {
            var taggedFiles = new List<TaggedFile>();

            var settings = new XmlReaderSettings
            {
                CheckCharacters = false,
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            using (var reader = XmlReader.Create(stream, settings))
            {
                TaggedFile currentFile = null;
                string currentMetadataSection = null;

                while (reader.Read())
                {
                    // start element
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        // -----------------------------------------------------
                        // <file ...>
                        // -----------------------------------------------------
                        if (reader.Name == "file")
                        {
                            currentFile = ParseFileAttributes(reader);

                            taggedFiles.Add(currentFile);

                            currentMetadataSection = null;

                            continue;
                        }

                        if (currentFile == null)
                            continue;

                        // -----------------------------------------------------
                        // <accessInfo>
                        // -----------------------------------------------------
                        if (reader.Name == "accessInfo")
                        {
                            ParseAccessInfo(reader, currentFile);
                            continue;
                        }

                        // -----------------------------------------------------
                        // <metadata section="File">
                        // <metadata section="Metadata">
                        // -----------------------------------------------------
                        if (reader.Name == "metadata")
                        {
                            currentMetadataSection =
                                reader.GetAttribute("section");

                            continue;
                        }

                        // -----------------------------------------------------
                        // <item name="..." ...>
                        // -----------------------------------------------------
                        if (reader.Name == "item" &&
                            !string.IsNullOrEmpty(currentMetadataSection))
                        {
                            ParseMetadataItem(
                                reader,
                                currentFile,
                                currentMetadataSection);

                            continue;
                        }
                    }

                    // end element
                    if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "metadata")
                        {
                            currentMetadataSection = null;
                        }
                    }
                }
            }

            return taggedFiles;
        }

        // file attributes

        private static TaggedFile ParseFileAttributes(XmlReader reader)
        {
            var taggedFile = new TaggedFile
            {
                fs = reader.GetAttribute("fs") ?? "",
                fsid = reader.GetAttribute("fsid") ?? "",
                path = reader.GetAttribute("path") ?? "",
                name = reader.GetAttribute("name") ?? "",
                id = reader.GetAttribute("id") ?? "",
                deleted = reader.GetAttribute("deleted") ?? "",
                embedded = reader.GetAttribute("embedded") ?? "",
                isrelated = reader.GetAttribute("isrelated") ?? "",
                source_index = reader.GetAttribute("source_index") ?? ""
            };

            if (long.TryParse(
                reader.GetAttribute("size"),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var size))
            {
                taggedFile.size = size;
            }

            if (int.TryParse(
                reader.GetAttribute("extractionId"),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var extractionId))
            {
                taggedFile.extractionId = extractionId;
            }

            return taggedFile;
        }

        // access info
       
        private static void ParseAccessInfo(
            XmlReader reader,
            TaggedFile taggedFile)
        {
            if (reader.IsEmptyElement)
                return;

            using (var subtree = reader.ReadSubtree())
            {
                while (subtree.Read())
                {
                    if (subtree.NodeType != XmlNodeType.Element ||
                        subtree.Name != "timestamp")
                    {
                        continue;
                    }

                    var name = subtree.GetAttribute("name");

                    var formattedTimestamp =
                        subtree.GetAttribute("formattedTimestamp");

                    if (string.IsNullOrWhiteSpace(name) ||
                        string.IsNullOrWhiteSpace(formattedTimestamp))
                    {
                        continue;
                    }

                    var dateTime =
                        ParseTimestamp(formattedTimestamp);

                    switch (name)
                    {
                        case "CreationTime":
                            taggedFile.CreationTime = dateTime;
                            break;

                        case "ModifyTime":
                            taggedFile.ModifyTime = dateTime;
                            break;

                        case "AccessTime":
                            taggedFile.AccessTime = dateTime;
                            break;

                        case "ChangeTime":
                            taggedFile.ChangeTime = dateTime;
                            break;
                    }
                }
            }
        }

        // file / metadata item
  
        private static void ParseMetadataItem(
            XmlReader reader,
            TaggedFile taggedFile,
            string section)
        {
            var name = reader.GetAttribute("name");

            if (string.IsNullOrWhiteSpace(name))
                return;

            string value;

            var attributeValue = reader.GetAttribute("value");

            if (attributeValue != null)
            {
                value = attributeValue;
            }
            else if (reader.IsEmptyElement)
            {
                value = "";
            }
            else
            {
                value = reader.ReadElementContentAsString();
            }

            value ??= "";

            switch (section)
            {
                case "File":
                    taggedFile.File[name] = value;
                    break;

                case "Metadata":
                    taggedFile.Metadata[name] = value;
                    break;
            }
        }

        // Timestamp parsing

        private static DateTime ParseTimestamp(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DateTime.MinValue;

            if (DateTime.TryParse(
                value,
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind,
                out var result))
            {
                return result;
            }

            return DateTime.MinValue;
        }
    }

    public class TaggedFile
    {
        // File attributes

        public string fs { get; set; } = "";

        public string fsid { get; set; } = "";

        public string path { get; set; } = "";

        public string name { get; set; } = "";

        public long size { get; set; } = 0;

        public string id { get; set; } = "";

        public int extractionId { get; set; } = 0;

        public string deleted { get; set; } = "";

        public string embedded { get; set; } = "";

        public string isrelated { get; set; } = "";

        public string source_index { get; set; } = "";

        // Access Infos
     
        public DateTime CreationTime { get; set; } = DateTime.MinValue;

        public DateTime ModifyTime { get; set; } = DateTime.MinValue;

        public DateTime AccessTime { get; set; } = DateTime.MinValue;

        public DateTime ChangeTime { get; set; } = DateTime.MinValue;

        // <metadata section="File">

        public Dictionary<string, string> File { get; set; }
            = new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase);

        // <metadata section="Metadata">

        public Dictionary<string, string> Metadata { get; set; }
            = new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase);
    }
}