using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    public class Report
    {
        public static List<Call> GetCalls(string path)
        {
            List<Call> result = new List<Call>();

            XmlReaderSettings settings = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(path, settings);

            while (reader.Read())
            {
                try
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "model" && reader.GetAttribute("type") == "Call")
                    {
                        XElement callNode = XElement.Load(reader.ReadSubtree());
                        Call call = CallParser.Parse(callNode);
                        result.Add(call);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            reader.Close();

            return result;
        }

        public static List<Chat> GetChats(string path)
        {
            List<Chat> result = new List<Chat>();

            XmlReaderSettings settings = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(path, settings);

            while (reader.Read())
            {
                try
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "model" && reader.GetAttribute("type") == "Chat")
                    {
                        XElement chatNode = XElement.Load(reader.ReadSubtree());
                        Chat chat = ChatParser.Parse(chatNode);
                        result.Add(chat);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            reader.Close();

            return result;
        }

        public static List<Contact> GetContacts(string path)
        {
            List<Contact> result = new List<Contact>();

            XmlReaderSettings settings = new XmlReaderSettings();
            XmlReader reader = XmlReader.Create(path, settings);

            while (reader.Read())
            {
                try
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "model" && reader.GetAttribute("type") == "Contact")
                    {
                        XElement contactNode = XElement.Load(reader.ReadSubtree());
                        Contact contact = ContactParser.Parse(contactNode);
                        result.Add(contact);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            reader.Close();

            return result;
        }
    }
}
