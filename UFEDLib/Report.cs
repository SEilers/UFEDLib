using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UFEDLib
{
    public class Report
    {
        private static readonly List<string> SupportedModels = new List<string> 
        {
            "ActivitySensorData",
            "ApplicationUsage",
            "AppsUsageLog",
            "Autofill",
            "CalendarEntry",
            "Call",
            "CellTower",
            "Chat",
            "Contact",
            "Cookie",
            "CreditCard",
            "DeviceConnectivity",
            "DeviceEvent",
            "DeviceInfoEntry",
            "DictionaryWord",
            "Email",
            "FileDownload",
            "FileUpload",
            "FinancialAccount",
            "InstalledApplication",
            "InstantMessage",
            "Journey",
            "LogEntry",
            "Location",
            "MobileCard",
            "NetworkUsage",
            "Note",
            "Notification",
            "Password",
            "PoweringEvent",
            "PublicTransportationTicket",
            "RecognizedDevice",
            "Recording",
            "SearchedItem",
            "SIMData",
            "SocialMediaActivity",
            "TransferOfFunds",
            "User",
            "UserAccount",
            "VisitedPage",
            "Voicemail",
            "WebBookmark",
            "WirelessNetwork"
        };

        public static List<string> GetModels(string fileName)
        {
            List<string> foundModels = ModelParser.ScanModels(fileName);
            return foundModels;
        }

        public static List<String> GetSupportedModels()
        {
            return SupportedModels;
        }

        public static List<string> GetUnsupportedModels(string fileName)
        {
            List<string> unsupportedModels = new List<string>();

            List<string> foundModels = ModelParser.ScanModels(fileName);
            List<string> supportedModels = Report.GetSupportedModels();

            foreach (string model in foundModels)
            {
                if (!supportedModels.Contains(model))
                {
                    unsupportedModels.Add(model);
                }
            }

            return unsupportedModels;
        }

        public static List<T> ParseData<T>(string filename, IProgress<int> progress = null, bool debugAttributes = false) where T : ModelBase, IUfedModelParser<T>, new()
        {
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (filename.EndsWith("ufdr", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseUfdr<T>(filename, progress, debugAttributes);
            }
            else if (filename.EndsWith("xml", StringComparison.InvariantCultureIgnoreCase))
            {
                return ModelParser.ParseXMLReport<T>(filename, progress, debugAttributes);
            }
            else
            {
                throw new ArgumentException("Unsupported file format: " + filename);
            }
        }

        public static ProjectAttributes ParseProjectAttributes( string filename, IProgress<int> progress = null)
        {
            return ProjectAttributes.Parse(filename);
        }

        public static List<ActivitySensorData> ParseActivitySensorData(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<ActivitySensorData>(filename, progress, debugAttributes);
        }

        public static List<ApplicationUsage> ParseApplicationUsages(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<ApplicationUsage>(filename, progress, debugAttributes);
        }

        public static List<AppsUsageLog> ParseAppsUsageLogs(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<AppsUsageLog>(filename, progress, debugAttributes);
        }

        public static List<Autofill> ParseAutofills(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Autofill>(filename, progress, debugAttributes);
        }

        public static List<CalendarEntry> ParseCalendarEntry(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<CalendarEntry>(filename, progress, debugAttributes);
        }

        public static List<Call> ParseCalls(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Call>(filename, progress, debugAttributes);
        }

        public static List<CellTower> ParsCellTowers(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<CellTower>(filename, progress, debugAttributes);
        }

        public static List<Chat> ParseChats(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Chat>(filename, progress, debugAttributes);
        }

        public static List<Contact> ParseContacts(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Contact>(filename, progress, debugAttributes);
        }
        public static List<Cookie> ParseCookies(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Cookie>(filename, progress, debugAttributes);
        }

        public static List<CreditCard> ParseCreditCards(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<CreditCard>(filename, progress, debugAttributes);
        }

        public static List<DeviceConnectivity> ParseDeviceConnectivity(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<DeviceConnectivity>(filename, progress, debugAttributes);
        }

        public static List<DeviceEvent> ParseDeviceEvent(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<DeviceEvent>(filename, progress, debugAttributes);
        }

        public static List<DeviceInfoEntry> ParseDeviceInfoEntry(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<DeviceInfoEntry>(filename, progress, debugAttributes);
        }

        public static List<DictionaryWord> ParseDictionaryWords(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<DictionaryWord>(filename, progress, debugAttributes);
        }

        public static List<Email> ParseEmails(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Email>(filename, progress, debugAttributes);
        }

        public static List<FileDownload> ParseFileDownloads(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<FileDownload>(filename, progress, debugAttributes);
        }

        public static List<FileUpload> ParseFileUploads(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<FileUpload>(filename, progress, debugAttributes);
        }

        public static List<FinancialAccount> ParseFinancialAccounts(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<FinancialAccount>(filename, progress, debugAttributes);
        }

        public static List<InstalledApplication> ParseInstalledApplications(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<InstalledApplication>(filename, progress, debugAttributes);
        }

        public static List<InstantMessage> ParseInstantMessages(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<InstantMessage>(filename, progress, debugAttributes);
        }

        public static List<Journey> ParseJourneys(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Journey>(filename, progress, debugAttributes);
        }

        public static List<LogEntry> ParseLogEntries(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<LogEntry>(filename, progress, debugAttributes);
        }

        public static List<Location> ParseLocations(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Location>(filename, progress, debugAttributes);
        }

        public static List<MobileCard> ParseMobileCards(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<MobileCard>(filename, progress, debugAttributes);
        }
        public static List<NetworkUsage> ParseNetworkUsage(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<NetworkUsage>(filename, progress, debugAttributes);
        }

        public static List<Note> ParseNotes(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Note>(filename, progress, debugAttributes);
        }
        
        public static List<Notification> ParseNotifications(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Notification>(filename, progress, debugAttributes);
        }

        public static List<Password> ParsePasswords(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Password>(filename, progress, debugAttributes);
        }

        public static List<PoweringEvent> ParsePoweringEvents(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<PoweringEvent>(filename, progress, debugAttributes);
        }

        public static List<PublicTransportationTicket> ParsePublicTransportationTickets(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<PublicTransportationTicket>(filename, progress, debugAttributes);
        }

        public static List<RecognizedDevice> ParseRecognizedDevices(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<RecognizedDevice>(filename, progress, debugAttributes);
        }
        public static List<Recording> ParseRecordings(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Recording>(filename, progress, debugAttributes);
        }

        public static List<SearchedItem> ParseSearchedItems(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<SearchedItem>(filename, progress, debugAttributes);
        }

        public static List<SIMData> ParseSIMData(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<SIMData>(filename, progress, debugAttributes);
        }

        public static List<SocialMediaActivity> ParseSocialMediaActivities(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<SocialMediaActivity>(filename, progress, debugAttributes);
        }

        public static List<TransferOfFunds> ParseTransferOfFunds(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<TransferOfFunds>(filename, progress, debugAttributes);
        }

        public static List<User> ParseUsers(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<User>(filename, progress, debugAttributes);
        }

        public static List<UserAccount> ParseUserAccounts(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<UserAccount>(filename, progress, debugAttributes);
        }

        public static List<VisitedPage> ParseVisitedPages(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<VisitedPage>(filename, progress, debugAttributes);
        }

        public static List<Voicemail> ParseVoicemails(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<Voicemail>(filename, progress, debugAttributes);
        }

        public static List<WebBookmark> ParseWebBookmarks(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<WebBookmark>(filename, progress, debugAttributes);
        }

        public static List<WirelessNetwork> ParseWirelessNetworks(string filename, IProgress<int> progress = null, bool debugAttributes = false)
        {
            return ParseData<WirelessNetwork>(filename, progress, debugAttributes);
        }
    }
}
