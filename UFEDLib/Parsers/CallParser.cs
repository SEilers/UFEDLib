using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using UFEDLib.Models;

namespace UFEDLib.Parsers
{
    public class CallParser
    {
        public static List<Call> Parse(string path)
        {
            try
            {
                List<Call> calls = new List<Call>();

                XDocument doc = XDocument.Load(path);
                var nsmgr = new XmlNamespaceManager(new NameTable());
                nsmgr.AddNamespace("a", "http://pa.cellebrite.com/report/2.0");
                //IEnumerable<XElement> contacts = doc.XPathSelectElements("//a:model[@type='Contact']", nsmgr);

                XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";
                IEnumerable<XElement> allCalls = doc.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "Call");

                foreach (XElement node in allCalls)
                {
                    Call call = new Call();

                    var source = node.XPathSelectElement("a:field[@name=\"Source\"]", nsmgr);
                    if (source != null)
                        call.Source = source.Value;

                    var direction = node.XPathSelectElement("a:field[@name=\"Direction\"]", nsmgr);
                    if (direction != null)
                        call.Direction = direction.Value;

                    var videoCall = node.XPathSelectElement("a:field[@name=\"VideoCall\"]", nsmgr);
                    if (videoCall != null)
                    {
                        bool vCall = false;

                        bool vCallSuccess = bool.TryParse((string)videoCall, out vCall);
                        call.VideoCall = vCall;
                    }


                    var duration = node.XPathSelectElement("a:field[@name=\"Duration\"]", nsmgr);
                    if (duration != null)
                        call.Duration = TimeSpan.Parse(duration.Value);

                    var timestamp = node.XPathSelectElement("a:field[@name=\"TimeStamp\"]", nsmgr);
                    if (timestamp != null)
                    {
                        DateTime dt;

                        bool success = DateTime.TryParse(timestamp.Value, out dt);
                        if (success)
                            call.TimeStamp = dt;
                    }


                    var parties = node.XPathSelectElement("a:multiModelField[@name=\"Parties\"]", nsmgr);

                    if (parties != null)
                    {
                        foreach (XElement partyNode in parties.Elements())
                        {
                            Party party = new Party();

                            var identifier = partyNode.XPathSelectElement("a:field[@name=\"Identifier\"]", nsmgr);
                            if (identifier != null)
                            {
                                party.Identifier = identifier.Value;
                            }

                            var role = partyNode.XPathSelectElement("a:field[@name=\"Role\"]", nsmgr);
                            if (role != null)
                            {
                                party.Role = role.Value;
                            }


                            var name = partyNode.XPathSelectElement("a:field[@name=\"Name\"]", nsmgr);
                            if (name != null)
                            {
                                party.Name = name.Value;
                            }


                            var isPhoneOwner = partyNode.XPathSelectElement("a:field[@name=\"isPhoneOwner\"]", nsmgr);
                            if (isPhoneOwner != null)
                            {
                                bool b_isPhoneOwner = false;
                                bool isPhoneOwnerSuccess = bool.TryParse((string)isPhoneOwner, out b_isPhoneOwner);
                                party.IsPhoneOwner = b_isPhoneOwner;
                            }

                            call.Parties.Add(party);
                        }
                    }

                    if (call.Parties.Count > 1)
                    {
                        Console.WriteLine("mehrer anrufer");
                    }

                    calls.Add(call);
                }

                //string csvText = "Source; Direction; VideoCall; Duration; TimeStamp; #Parties\n";
                //foreach (var call in calls)
                //{
                //    csvText += call.Source + ";" + call.Direction + ";" + call.VideoCall.ToString() + ";" + call.Duration + ";" + call.TimeStamp + ";" + call.Parties.Count.ToString() + "\n";
                //}

                //File.WriteAllText("call.csv", csvText);

                return calls;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }

}
