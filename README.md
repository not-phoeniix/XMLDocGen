# HTMLFromXML
Hiya! This is a small project I'm messing around with in my free time. I aim to use it for some of my games and other C# projects I create. It's all coded in C# using .NET 8.0.

## Usage
Command usage:

```
HTMLFromXML - Generate an HTML page file from C# XML

Usage: HTMLFromXML [inputFile] [outputPath]
    [inputFile] - filepath of input C# XML project file
    [outputPath] - directory to output all HTML files into
```

**Note:** When building a C# project, make sure to have this element in your .csproj file:

```xml
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

Then you can use the outputted xml file in the bin folder (after building) as the first argument to this program. Then type in the desired output directory as the second argument, and it'll spit out a bunch of generated HTML files in your specified directory!

Usage example: `dotnet run "bin/Debug/net8.0/HTMLFromXML.xml" "../docs/"`. Please note that this example is running in debug mode from the console, and is executed from the project's Source/ folder.

## Example
If you wanna see an example of the generated HTML from THIS project, check out [index.html](docs/index.html)! It's the "main page" of the collected documentation pages, and has links to all the sub-pages in the generated documentation. The original XML file has also been included in this project (at [HTMLFromXML.xml](docs/HTMLFromXML.xml)) if you'd like to see where the HTML was generated from :]
