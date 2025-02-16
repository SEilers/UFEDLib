# UFEDLib
UFEDLib is a C# library designed to parse the UFED data model from UFDR files and map the extracted data into C# objects.
This library is intended for forensic analysis, allowing developers to extract structured data from UFDR reports.

> [!NOTE]  
> UFEDLib __does not__ support exporting data to other formats (e.g., databases, Excel, CSV, JSON). Its primary focus is __parsing and mapping__ UFDR data models.

## Installation
You can install UFEDLib via NuGet:
```sh
dotnet add package ufedlibdotnet
```
Or manually reference the compiled DLL in your project.

## Supported Models
UFEDLib currently supports parsing the following data models:
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
The main interface for working with UFEDLib is the Report class, which provides static functions to parse supported data models.

### Basic Example: Parsing Locations

```csharp
using UFEDLib;
using System.Collections.Generic;

// Parse locations from a UFDR file
List<Location> locations = Report.ParseLocations("report.ufdr");

Console.WriteLine($"Parsed {locations.Count} locations.");
```
The input file can be either:
- A UFDR file (e.g., report.ufdr)
- An extracted report.xml file from a UFDR report container

### Example: Exporting Location Data to CSV
This example extracts location models from a UFDR file and writes their timestamp, longitude, and latitude to a CSV file.

```csharp
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


## License
This project is licensed under the MIT License.




