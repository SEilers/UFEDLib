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
        public static List<Call> GetCalls(string ufdrFile)
        {
            return ModelParser.ParseUfdr<Call>(ufdrFile);
        }

        public static List<Chat> GetChats(string ufdrFile)
        {
            return ModelParser.ParseUfdr<Chat>(ufdrFile);
        }

        public static List<Contact> GetContacts(string ufdrFile)
        {
            return ModelParser.ParseUfdr<Contact>(ufdrFile);
        }

        public static List<EMail> GetEMails(string ufdrFile)
        {
            return ModelParser.ParseUfdr<EMail>(ufdrFile);
        }

        public static List<Journey> GetJourneys(string ufdrFile)
        {
            return ModelParser.ParseUfdr<Journey>(ufdrFile);
        }

        public static List<Location> GetLocations(string ufdrFile)
        {
            return ModelParser.ParseUfdr<Location>(ufdrFile);
        }

        public static List<Password> GetPasswords(string ufdrFile)
        {
            return ModelParser.ParseUfdr<Password>(ufdrFile);
        }

        public static List<SearchedItem> GetSearchedItems(string ufdrFile)
        {
            return ModelParser.ParseUfdr<SearchedItem>(ufdrFile);
        }

        public static List<UserAccount> GetUserAccounts(string ufdrFile)
        {
            return ModelParser.ParseUfdr<UserAccount>(ufdrFile);
        }

        public static List<WirelessNetwork> GetWirelessNetworks(string ufdrFile)
        {
            return ModelParser.ParseUfdr<WirelessNetwork>(ufdrFile);
        }
    }
}
