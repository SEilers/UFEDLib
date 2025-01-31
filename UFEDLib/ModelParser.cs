using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace UFEDLib
{
    public class ModelParser
    {
        public static List<T> ParseUfdr<T>(string ufdrFileName, IProgress<int> progress = null) where T : ModelBase, IUfedModelParser<T>, new()
        {
            var results = new List<T>();

            try
            {
                long reportSize = 0;

                using (ZipArchive zip = ZipFile.OpenRead(ufdrFileName))
                {
                    var report = zip.GetEntry("report.xml");

                    if (report == null)
                    {
                        Console.WriteLine("report.xml not found in the ufdr file");
                        return null;
                    }

                    reportSize = report.Length;

                    using (Stream reportStream = report.Open())
                    {
                        using (StreamReader sr = new StreamReader(reportStream))
                        {
                            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
                            {
                                CheckCharacters = false
                            };

                            long currentPosition = 0;
                            int lastPercent = 0;
                            bool modelFound = false;

                            using (XmlReader reader = XmlReader.Create(sr, xmlReaderSettings))
                            {
                                while (reader.Read())
                                {
                                    try
                                    {
                                        if (reader.Depth == 3 && reader.Name == "model" && reader.IsStartElement())
                                        {
                                            String modelType = reader.GetAttribute("type");

                                            if (modelType == T.GetXmlModelType())
                                            {
                                                XElement element = XElement.Load(reader.ReadSubtree());
                                                results.Add(T.ParseModel(element));
                                                modelFound = true;
                                            }
                                        }

                                        if (progress != null)
                                        {
                                            if (reportSize > 0)
                                            {
                                                currentPosition = sr.BaseStream.Position;
                                                int percent = (int)((double)currentPosition / reportSize * 100);
                                                if (percent > lastPercent)
                                                {
                                                    lastPercent = percent;
                                                    progress.Report(percent);
                                                }
                                            }

                                            if (modelFound && reader.Depth == 2)
                                            {
                                                progress.Report(100);
                                                return results;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error parsing report.xml: " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return results;
            }

            return results;
        }
    }
}
