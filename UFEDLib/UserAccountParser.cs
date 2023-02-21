using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;

namespace UFEDLib
{
    public class UserAccountParser
    {
        public static List<UserAccount> Parse(object reader_object)
        {
            var nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("a", "http://pa.cellebrite.com/report/2.0");
            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<UserAccount> userAccounts = new List<UserAccount>();

            XmlReader reader = (XmlReader)reader_object;

            while (reader.Read())
            {
                try
                {
                    if (reader.Depth == 3 && reader.Name == "model" && reader.GetAttribute("type") == "UserAccount" && reader.IsStartElement())
                    {
                        String? userAccountId = reader.GetAttribute("id");

                        XmlReader contactReader = reader.ReadSubtree();

                        UserAccount userAccount = new UserAccount();

                        if (userAccountId != null)
                        {
                            userAccount.Id = userAccountId;
                        }

                        XElement contactNode = XElement.Load(contactReader);

                        var contact_source = contactNode.XPathSelectElement("a:field[@name=\"Source\"]", nsmgr);
                        if (contact_source != null)
                            userAccount.Source = (String)contact_source.Value.Trim();

                        var contact_name = contactNode.XPathSelectElement("a:field[@name=\"Name\"]", nsmgr);
                        if (contact_name != null)
                            userAccount.Name = (String)contact_name.Value.Trim();

                        //var contact_group = contactNode.XPathSelectElement("a:field[@name=\"Username\"]", nsmgr);
                        //if (contact_group != null)
                        //    userAccount.Group = (String)contact_group.Value.Trim();

                        //var account = contactNode.XPathSelectElement("a:field[@name=\"Account\"]", nsmgr);
                        //if (account != null)
                        //    userAccount.Account = (String)account.Value.Trim();

                        userAccounts.Add(userAccount);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            reader.Close();

            return userAccounts;

        }
    }
}
