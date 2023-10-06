using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using UFEDLib.Models;

namespace UFEDLib
{
    public class ChatParser
    {
        public static List<Chat> Parse(object reader_object)
        {
            var nsmgr = new XmlNamespaceManager(new NameTable());
            nsmgr.AddNamespace("a", "http://pa.cellebrite.com/report/2.0");

            XNamespace xNamespace = "http://pa.cellebrite.com/report/2.0";

            List<Chat> chats = new List<Chat>();
            int last_percent = 0;

            XmlReader reader = (XmlReader)reader_object;

            while (reader.Read())
            {
                try
                {
                    if (reader.Depth == 3 && reader.Name == "model" && reader.GetAttribute("type") == "Chat" && reader.IsStartElement())
                    {
                        String? chatid = reader.GetAttribute("id");

                        XmlReader chatReader = reader.ReadSubtree();

                        Chat chat = new Chat();
                        if (chatid != null)
                        {
                            chat.Id = chatid;
                        }

                        XElement chatNode = XElement.Load(chatReader);

                        var chat_source = chatNode.XPathSelectElement("a:field[@name=\"Source\"]", nsmgr);
                        if (chat_source != null)
                            chat.Source = (String)chat_source.Value.Trim();

                        var startTime = chatNode.XPathSelectElement("a:field[@name=\"StartTime\"]", nsmgr);
                        if (startTime != null)
                        {
                            DateTime dt;

                            bool success = DateTime.TryParse(startTime.Value.Trim(), out dt);
                            if (success)
                                chat.StartTime = dt;
                        }

                        var lastActivity = chatNode.XPathSelectElement("a:field[@name=\"LastActivity\"]", nsmgr);
                        if (lastActivity != null)
                        {
                            DateTime dt;

                            bool success = DateTime.TryParse(lastActivity.Value.Trim(), out dt);
                            if (success)
                                chat.LastActivity = dt;
                        }

                        var account = chatNode.XPathSelectElement("a:field[@name=\"Account\"]", nsmgr);
                        if (account != null)
                            chat.Account = (String)account.Value.Trim();

                        //IEnumerable<XElement> participants = chatNode.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "Party");
                        IEnumerable<XElement> participants = chatNode.XPathSelectElements("a:multiModelField[@name=\"Participants\"]/a:model[@type=\"Party\"]", nsmgr);

                        foreach (XElement participant in participants)
                        {
                            Party chatParty = new Party();

                            var from_identifier = participant.XPathSelectElement("a:field[@name=\"Identifier\"]", nsmgr);
                            if (from_identifier != null)
                                chatParty.Identifier = from_identifier.Value.Trim();

                            var from_name = participant.XPathSelectElement("a:field[@name=\"Name\"]", nsmgr);
                            if (from_name != null)
                                chatParty.Name = from_name.Value.Trim(); ;

                            var from_isPhoneOwner = participant.XPathSelectElement("a:field[@name=\"IsPhoneOwner\"]", nsmgr);
                            if (from_isPhoneOwner != null)
                                chatParty.IsPhoneOwner = bool.Parse(from_isPhoneOwner.Value.Trim());

                            chat.Participants.Add(chatParty);
                        }

                        //IEnumerable<XElement> photos = chatNode.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "ContactPhoto");


                        IEnumerable<XElement> instantMessages = chatNode.Descendants(xNamespace + "model").Where(x => x.Attribute("type").Value == "InstantMessage");

                        foreach (XElement node in instantMessages)
                        {
                            String? messageid = (String)node.Attribute("id");

                            InstantMessage msg = new InstantMessage();

                            if (messageid != null)
                                msg.Id = messageid;

                            if (chatid != null)
                                msg.ChatId = chatid;

                            var source = node.XPathSelectElement("a:field[@name=\"Source\"]", nsmgr);
                            if (source != null)
                                msg.Source = (String)source.Value.Trim();

                            var body = node.XPathSelectElement("a:field[@name=\"Body\"]", nsmgr);
                            if (body != null)
                                msg.Body = (String)body.Value.Trim();

                            var from = node.XPathSelectElement("a:modelField[@name=\"From\"]", nsmgr);
                            if (from != null)
                            {
                                Party party = new Party();

                                var pty = from.XPathSelectElement("a:model[@type=\"Party\"]", nsmgr);

                                if (pty != null)
                                {
                                    var from_identifier = pty.XPathSelectElement("a:field[@name=\"Identifier\"]", nsmgr);
                                    if (from_identifier != null)
                                        party.Identifier = from_identifier.Value.Trim();

                                    var from_role = pty.XPathSelectElement("a:field[@name=\"Role\"]", nsmgr);
                                    if (from_role != null)
                                        party.Role = from_role.Value.Trim();

                                    var from_name = pty.XPathSelectElement("a:field[@name=\"Name\"]", nsmgr);
                                    if (from_name != null)
                                        party.Name = from_name.Value.Trim(); ;

                                    var from_isPhoneOwner = pty.XPathSelectElement("a:field[@name=\"IsPhoneOwner\"]", nsmgr);
                                    if (from_isPhoneOwner != null)
                                        party.IsPhoneOwner = bool.Parse(from_isPhoneOwner.Value.Trim());

                                    msg.From = party;

                                }
                            }

                            var parties = node.XPathSelectElement("a:multiModelField[@name=\"To\"]", nsmgr);

                            if (parties != null)
                            {
                                foreach (XElement partyNode in parties.Elements())
                                {
                                    Party party = new Party();

                                    var p_identifier = partyNode.XPathSelectElement("a:field[@name=\"Identifier\"]", nsmgr);
                                    if (p_identifier != null)
                                        party.Identifier = p_identifier.Value.Trim();

                                    var role = partyNode.XPathSelectElement("a:field[@name=\"Role\"]", nsmgr);
                                    if (role != null)
                                        party.Role = role.Value.Trim();

                                    var name = partyNode.XPathSelectElement("a:field[@name=\"Name\"]", nsmgr);
                                    if (name != null)
                                        party.Name = name.Value.Trim();

                                    var isPhoneOwner = partyNode.XPathSelectElement("a:field[@name=\"IsPhoneOwner\"]", nsmgr);
                                    if (isPhoneOwner != null)
                                        party.IsPhoneOwner = bool.Parse(isPhoneOwner.Value.Trim());

                                    msg.To.Add(party);
                                }
                            }

                            var identifier = node.XPathSelectElement("a:field[@name=\"Identifier\"]", nsmgr);
                            if (identifier != null)
                                msg.Identifier = (String)(identifier.Value.Trim());

                            var timestamp = node.XPathSelectElement("a:field[@name=\"TimeStamp\"]", nsmgr);
                            if (timestamp != null)
                            {
                                //msg.TimeStamp = DateTime.Parse(timestamp.Value);

                                DateTime dt;

                                bool success = DateTime.TryParse(timestamp.Value.Trim(), out dt);
                                if (success)
                                    msg.TimeStamp = dt;
                            }

                            var status = node.XPathSelectElement("a:field[@name=\"Status\"]", nsmgr);
                            if (status != null)
                                msg.Status = (String)(status.Value.Trim());

                            var type = node.XPathSelectElement("a:field[@name=\"Type\"]", nsmgr);
                            if (type != null)
                                msg.Type = (String)(type.Value.Trim());

                            var sourceapplication = node.XPathSelectElement("a:field[@name=\"SourceApplication\"]", nsmgr);
                            if (sourceapplication != null)
                                msg.SourceApplication = (String)(sourceapplication.Value.Trim());

                            var attachment = node.XPathSelectElement("a:multiModelField[@name=\"Attachments\"]/a:model[@type=\"Attachment\"]", nsmgr);

                            if (attachment != null)
                            {
                                var filename = attachment.XPathSelectElement("a:field[@name=\"Filename\"]", nsmgr);

                                if (filename != null)
                                    msg.AttachentFileName = (String)(filename.Value.Trim());

                                var attachmentType = attachment.XPathSelectElement("a:field[@name=\"ContentType\"]", nsmgr);

                                if (attachmentType != null)
                                    msg.AttachmentType = (String)(attachmentType.Value.Trim());
                            }

                            var targetid = node.XPathSelectElement("a:targetid[@ismodel=\"true\"]", nsmgr);

                            if (targetid != null)
                            {
                                msg.JumpTargetId = (String)targetid.Value.Trim();
                            }

                            chat.Messages.Add(msg);

                        }

                        chats.Add(chat);
                        int numProcessedChats = chats.Count;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            
            
            reader.Close();

            return chats;

        }
    }
}
