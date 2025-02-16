# UFEDLib
A C# library for parsing the ufed data model from ufdr files and mapping the models to c# objects. 
Exporting to other formats (Databases, Excel, CSV, JSON...) is not a part of this library. 

## Supported Models

- ActivitySensorData
- ApplicationUsage
- AppsUsageLog
- Autofill
- CalendarEntry
- Call
- CellTower
- Chat
- Contact
- Cookie
- CreditCard
- DeviceConnectivity
- DeviceEvent
- DeviceInfoEntry
- DictionaryWord
- Email
- FileDownload
- FileUpload
- FinancialAccount
- InstalledApplication
- InstantMessage
- Journey
- LogEntry
- Location
- MobileCard
- NetworkUsage
- Note
- Notification
- Password
- PoweringEvent
- PublicTransportationTicket
- RecognizedDevice
- Recording
- SearchedItem
- SIMData
- SocialMediaActivity
- TransferOfFunds
- User
- UserAccount
- VisitedPage
- Voicemail
- WebBookmark
- WirelessNetwork

## Usage
Main interface for the usage is the Report Class, which has static functions to parse the models above.
For exmamle:

```
List<Location> locations = Report.ParseLocations(fileName);
```

The file can be a ufdr file or a report.xml file you already extracted from the report container.

### Example Console App
This short example parses the location models of the ufdr report and exports the timestamp, longitude and latitude to a CSV file.

```
using UFEDLib;
using System.Globalization;
using System.IO;

string path = @"C:\Path\To\Your\File\Phone.ufdr";

var locations = Report.ParseLocations(path);

Console.WriteLine("Locations: " + locations.Count);

string fileName = Path.GetFileNameWithoutExtension(path);
string newFileName = fileName + "_locations.csv";

using (var writer = new StreamWriter(newFileName))
{
    writer.WriteLine("TimeStamp;Longitude;Latitude");

    foreach (var location in locations)
    {
        double? longitude = location?.Position?.Longitude;
        double? latitude = location?.Position?.Latitude;

        if (longitude == null || latitude == null)
        {
            continue;
        }

        writer.WriteLine(
            $"{location.TimeStamp};" +
            $"{longitude.Value.ToString(CultureInfo.InvariantCulture)};" +
            $"{latitude.Value.ToString(CultureInfo.InvariantCulture)}");
    }
}
```





