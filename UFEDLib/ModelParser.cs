using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UFEDLib
{
    public class ModelParser
    {
        public static List<T> ParseXMLReport<T>(string xmlReportFile, IProgress<int> progress = null, bool debugAttributes = false) where T : ModelBase, IUfedModelParser<T>, new()
        {
            if(!File.Exists(xmlReportFile))
            {
                Console.WriteLine("File not found: " + xmlReportFile);
                return new List<T>();
            }

            // get file size
            long reportSize = new FileInfo(xmlReportFile).Length;

            var results = new List<T>();


            try
            {
                using (StreamReader sr = new StreamReader(xmlReportFile))
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
                                    string modelType = reader.GetAttribute("type");
                                    if (modelType == T.GetXmlModelType())
                                    {
                                        XElement element = XElement.Load(reader.ReadSubtree());
                                        results.Add(T.ParseModel(element, debugAttributes));
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
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing report.xml" +  ex.ToString());
            }
            finally
            {
                if (progress != null)
                    progress.Report(100);
            }

            return results;
        }

        public static List<T> ParseUfdr<T>(string ufdrFileName, IProgress<int> progress = null, bool debugAttributes = false) where T : ModelBase, IUfedModelParser<T>, new()
        {
            var results = new List<T>();

            if(!File.Exists(ufdrFileName))
            {
                Console.WriteLine("File not found: " + ufdrFileName);
                return results;
            }

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
                                            string modelType = reader.GetAttribute("type");

                                            if (modelType == T.GetXmlModelType())
                                            {
                                                XElement element = XElement.Load(reader.ReadSubtree());
                                                results.Add(T.ParseModel(element, debugAttributes));
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
                Console.WriteLine("Error parsing report.xml: " + ex.ToString());
            }
            finally
            {
                if (progress != null)
                    progress.Report(100);
            }

            return results;
        }
    
        
        
        public static List<string> ScanModels( string fileName, IProgress<int> progress = null)
        {
            List<string> models = new List<string>();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found: " + fileName);
                return models;
            }
            try
            {
                if( fileName.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    long reportSize = new FileInfo(fileName).Length;

                    try
                    {
                        using (StreamReader sr = new StreamReader(fileName))
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
                                reader.ReadToFollowing("decodedData");

                                while (reader.Read())
                                {
                                    try
                                    {
                                        if (reader.Depth == 3 && reader.Name == "model" && reader.IsStartElement())
                                        {
                                            string modelType = reader.GetAttribute("type");

                                            if (!models.Contains(modelType))
                                            {
                                                models.Add(modelType);
                                                //modelFound = true;
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

                                            if (modelFound && reader.Depth == 2 && reader.IsStartElement())
                                            {
                                                progress.Report(100);
                                                models.Sort();
                                                return models;
                                            }
                                        }
                                        else
                                        {
                                            if (modelFound && reader.Depth == 2 && reader.IsStartElement())
                                            {
                                                models.Sort();
                                                return models;
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
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error parsing report.xml: " +  ex.ToString());
                    }
                    finally
                    {
                        if (progress != null)
                            progress.Report(100);
                    }
                }
                else if (fileName.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {
                        long reportSize = 0;

                        using (ZipArchive zip = ZipFile.OpenRead(fileName))
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
                                        reader.ReadToFollowing("decodedData");

                                        while (reader.Read())
                                        {
                                            try
                                            {
                                                if (reader.Depth == 3 && reader.Name == "model" && reader.IsStartElement())
                                                {
                                                    string modelType = reader.GetAttribute("type");

                                                    if (!models.Contains(modelType))
                                                    {
                                                        models.Add(modelType);
                                                        //modelFound = true;
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
                                                        models.Sort();
                                                        return models;
                                                    }
                                                }
                                                else
                                                {
                                                    if (modelFound && reader.Depth == 2)
                                                    {
                                                        models.Sort();
                                                        return models;
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error parsing ufdr: " + ex.Message);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error parsing ufdr: " + ex.ToString());
                    }
                    finally
                    {
                        if (progress != null)
                            progress.Report(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing report: " + ex.ToString());
            }
            return models;
        }
    }
}
