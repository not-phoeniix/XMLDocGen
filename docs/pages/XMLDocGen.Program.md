# XMLDocGen.Program
No container summary found :(
## Methods
### XMLDocGen.Program.PrintHelp
*Summary:* Prints the help message for running the command

### XMLDocGen.Program.GetProjectName(System.Xml.XmlDocument)
*Summary:* Retrieves project name from the "name" element in an XML file
*param* doc: XML Document to grab from
*returns:* Name of project

### XMLDocGen.Program.GenerateContainers(System.Xml.XmlDocument)
*Summary:* Generates a list of containers with all HTML-formatted information from inputted Xml doc
*param* doc: Xml document to get data from
*returns:* Generated list of containers

### XMLDocGen.Program.WriteHTML(System.Xml.XmlDocument,System.String)
*Summary:* Writes converts and writes the inputted XML document to an HTML document
*param* doc: Xml document to draw data from
*param* outputPath: Directory path to output HTML pages

