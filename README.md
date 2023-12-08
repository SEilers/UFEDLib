# UFEDLib
A C# library for mapping and parsing the ufed data model. 

Using XMLReader for performance and XElement at the lower level.

Exporting to other formats (Databases, Excel, CSV, JSON...) is not a part of this library. 

## Pre-Release Status(!)
This library is in pre-release status. Meaning it is under heavy development. 
I am opening this repostitory at this early stage to give the forensic community a chance to comment.
If you would like to participate in the development feel free to contact me.

First release will be when following items (modelTypes) are feature complete:
- Call
- Chat
- Contact

## Usage
This is assuming you already have unzipped the "report.xml" file from the ufdr image.
Then you can parse the chats of the report in your application like follows:

```
public List<Chat> ParseChats(string xmlFileName)
{
    List<Chat> result = new List<Chat>();
    
    XmlReaderSettings settings = new XmlReaderSettings();
    XmlReader reader = XmlReader.Create(xmlFileName, settings);

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
        catch(Exception ex )
        {
            Console.WriteLine(ex.Message);
        }
     }
    return result;
}
```





