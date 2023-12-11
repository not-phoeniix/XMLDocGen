# HTMLFromXML
Hiya! This is a small project I'm messing around with in my free time. I aim to use it for some of my games and other C# projects I create. It's all coded in C# using .NET 8.0.

## Usage
When building a C# project, make sure to have this element existing in your .csproj file:

```xml
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

Then you can use the outputted xml file in the bin folder as the only argument to this program. It'll spit out an HTML file in the running directory!

## Example
If ya wanna see an example of the generated HTML from THIS project, check out [doc.md](doc.md)! I just renamed the file extension from .html to .md so it could be previewed in GitHub, but you can also see the original HTML file at [doc.html](doc.html) (it should be the same ) :]
