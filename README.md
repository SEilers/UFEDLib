# UFEDLib
A C# library for parsing UFED XML reports and working with its data. 
Using XMLReader for performance and XElement at the lower level.

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
                String? chatid = reader.GetAttribute("id");
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





