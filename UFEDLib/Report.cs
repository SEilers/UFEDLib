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
        public static List<Autofill> ParseAutofills(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if(filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if ( filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Autofill>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Autofill>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }
        public static List<Call> ParseCalls(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Call>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Call>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<Chat> ParseChats(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Chat>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Chat>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<Contact> ParseContacts(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Contact>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Contact>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<Cookie> ParseCookies(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Cookie>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Cookie>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<DictionaryWord> ParseDictionaryWords(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<DictionaryWord>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<DictionaryWord>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<EMail> ParseEMails(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<EMail>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<EMail>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<Journey> ParseJourneys(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Journey>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Journey>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<Location> ParseLocations(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Location>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Location>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<Password> ParsePasswords(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<Password>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<Password>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<SearchedItem> ParseSearchedItems(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<SearchedItem>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<SearchedItem>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<UserAccount> ParseUserAccounts(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<UserAccount>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<UserAccount>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<WebBookmark> ParseWebBookmarks(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<WebBookmark>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<WebBookmark>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static List<WirelessNetwork> ParseWirelessNetworks(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<WirelessNetwork>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<WirelessNetwork>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }
    }
}
