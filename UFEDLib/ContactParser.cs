using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics.Contracts;
using System.Xml.XPath;

namespace UFEDLib
{
    public class ContactParser
    {
        public static List<Contact> Parse(object reader_object)
        {
            var nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("a", "http://pa.cellebrite.com/report/2.0");
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<Contact> contacts = new List<Contact>();
      
            XmlReader reader = (XmlReader)reader_object;

            while (reader.Read())
            {
                try
                {
                    if (reader.Depth == 3 && reader.Name == "model" && reader.GetAttribute("type") == "Contact" && reader.IsStartElement())
                    {
                        String? contactid = reader.GetAttribute("id");

                        XmlReader contactReader = reader.ReadSubtree();

                        Contact contact = new Contact();

                        if (contactid != null)
                        {
                            contact.Id = contactid;
                        }

                        XElement contactNode = XElement.Load(contactReader);

                        var contact_source = contactNode.XPathSelectElement("a:field[@name=\"Source\"]", nsmgr);
                        if (contact_source != null)
                            contact.Source = (String)contact_source.Value.Trim();

                        var contact_name = contactNode.XPathSelectElement("a:field[@name=\"Name\"]", nsmgr);
                        if (contact_name != null)
                            contact.Name = (String)contact_name.Value.Trim();

                        var contact_group = contactNode.XPathSelectElement("a:field[@name=\"Group\"]", nsmgr);
                        if (contact_group != null)
                            contact.Group = (String)contact_group.Value.Trim();

                        var account = contactNode.XPathSelectElement("a:field[@name=\"Account\"]", nsmgr);
                        if (account != null)
                            contact.Account = (String)account.Value.Trim();

                        contacts.Add(contact);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            reader.Close();

            return contacts;

        }
    }
}
