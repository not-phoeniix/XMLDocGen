<!DOCTYPE html>
<html lang="en">

<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>HTMLFromXML</title></head>

<body>
<h1>HTMLFromXML</h1><h2>HTMLFromXML.DocContainer</h2>
<p>Class representation of a class/struct/interface/etc. Holds a bunch of child DocElement's to display on page.</p>
<h3>Properties</h3>
<h4>HTMLFromXML.DocContainer.Name</h4>
<p><em>Summary:</em> Container name</p>

<h4>HTMLFromXML.DocContainer.Summary</h4>
<p><em>Summary:</em> Summary text of container</p>

<h4>HTMLFromXML.DocContainer.Elements</h4>
<p><em>Summary:</em> List of all child DocElements's inside this container</p>

<h3>Methods</h3>
<h4>HTMLFromXML.DocContainer.AddElement(HTMLFromXML.DocElement)</h4>
<p><em>Summary:</em> Adds an element to this container's html structure</p>
<p><em>param</em> element: Element to add</p>

<h4>HTMLFromXML.DocContainer.ToString</h4>
<p><em>Summary:</em> HTML representation of this container and all its elements</p>
<p><em>returns:</em> HTML string</p>

<br>

<h2>HTMLFromXML.DocElement</h2>
<p>Class representation of an individual "member" that isn't a class. Can hold things like methods, properties, constructors, etc.</p>
<h3>Constructors</h3>
<h4>HTMLFromXML.DocElement.#ctor(System.Xml.XmlNode)</h4>
<p><em>Summary:</em> Creates a new DocElement from the data in the parent XmlNode</p>
<p><em>param</em> node: Node to extract data from</p>

<h3>Properties</h3>
<h4>HTMLFromXML.DocElement.Name</h4>
<p><em>Summary:</em> Name of this element</p>

<h4>HTMLFromXML.DocElement.ContainerName</h4>
<p><em>Summary:</em> Name of containing class/struct/etc that this member is in</p>

<h4>HTMLFromXML.DocElement.Summary</h4>
<p><em>Summary:</em> Member summary</p>

<h4>HTMLFromXML.DocElement.Params</h4>
<p><em>Summary:</em> Dictionary of all parameters. Key == parameter name, value == parameter summary</p>

<h4>HTMLFromXML.DocElement.Returns</h4>
<p><em>Summary:</em> Member returns summary</p>

<h4>HTMLFromXML.DocElement.Type</h4>
<p><em>Summary:</em> Type of element, can be a method, property, constructor, etc</p>

<h3>Methods</h3>
<h4>HTMLFromXML.DocElement.GetType(System.String,System.String)</h4>
<p><em>Summary:</em> Determines type of element based on input string (one letter, P/M/T)</p>
<p><em>param</em> type: Single letter representation of element's type</p>
<p><em>param</em> name: Name following the element type</p>
<p><em>returns:</em> ElementType that corresponds to the inputted type string</p>

<h4>HTMLFromXML.DocElement.ToString</h4>
<p><em>Summary:</em> HTML representation of this current element</p>
<p><em>returns:</em> String of HTML for this current element</p>

<br>

<h2>HTMLFromXML.Program</h2>
<p>No container summary found :(</p>
<h3>Methods</h3>
<h4>HTMLFromXML.Program.PrintHelp</h4>
<p><em>Summary:</em> Prints the help message for running the command</p>

<h4>HTMLFromXML.Program.GetProjectName(System.Xml.XmlDocument)</h4>
<p><em>Summary:</em> Retrieves project name from the "name" element in an XML file</p>
<p><em>param</em> doc: XML Document to grab from</p>
<p><em>returns:</em> Name of project</p>

<h4>HTMLFromXML.Program.GenerateContainers(System.Xml.XmlDocument)</h4>
<p><em>Summary:</em> Generates a list of containers with all HTML-formatted information from inputted Xml doc</p>
<p><em>param</em> doc: Xml document to get data from</p>
<p><em>returns:</em> Generated list of containers</p>

<h4>HTMLFromXML.Program.WriteHTML(System.Xml.XmlDocument)</h4>
<p><em>Summary:</em> Writes converts and writes the inputted XML document to an HTML document</p>
<p><em>param</em> doc: Xml document to draw data from</p>

<br>

</body>

</html>
