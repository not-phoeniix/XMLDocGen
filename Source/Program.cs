using System.Collections;
using System.Xml;

namespace HTMLFromXML;

public class Program {
    static void Main(string[] args) {
        if (args.Length < 2) {
            Console.WriteLine("Error with args...\n");
            PrintHelp();
            return;
        }

        string filepath = args[0];
        string outputPath = args[1];

        // create document object and load file, exit and print if file not found
        XmlDocument doc = new();
        try {
            doc.Load(filepath);
        } catch (Exception ex) {
            Console.WriteLine("Error loading Xml document! " + ex.Message);
            return;
        }

        // provide status updates lol
        Console.WriteLine($"Successfully loaded XML file at path \"{filepath}\"!");
        Console.WriteLine($"Project name: {GetProjectName(doc)}");

        WriteHTML(doc, outputPath);
    }

    /// <summary>
    /// Prints the help message for running the command
    /// </summary>
    static void PrintHelp() {
        Console.WriteLine(
            "HTMLFromXML - Generate an HTML page file from C# XML\n" +
            "\n" +
            "Usage: HTMLFromXML [inputFile] [outputPath]\n" +
                "\t[inputFile] - filepath of input C# XML project file\n" +
                "\t[outputPath] - directory to output all HTML files into\n"
        );
    }

    /// <summary>
    /// Retrieves project name from the "name" element in an XML file
    /// </summary>
    /// <param name="doc">XML Document to grab from</param>
    /// <returns>Name of project</returns>
    static string GetProjectName(XmlDocument doc) {
        return doc.GetElementsByTagName("name")[0].InnerText;
    }

    /// <summary>
    /// Generates a list of containers with all HTML-formatted information from inputted Xml doc
    /// </summary>
    /// <param name="doc">Xml document to get data from</param>
    /// <returns>Generated list of containers</returns>
    static List<DocContainer> GenerateContainers(XmlDocument doc) {
        List<DocContainer> containers = new();

        // gets all elements marked as a "member" and adds them iteratively
        XmlNodeList nodeList = doc.GetElementsByTagName("member");

        // adds all "member" nodes to containers
        foreach (XmlNode node in nodeList) {
            DocElement element = new(node);

            bool containerExists = false;
            foreach (DocContainer container in containers) {
                if (container.Name == element.ContainerName) {
                    containerExists = true;
                    container.AddElement(element);
                }
            }

            // creates a new container if it doesn't exist yet
            if (!containerExists) {
                DocContainer container = new();
                container.AddElement(element);
                containers.Add(container);
            }
        }

        return containers;
    }

    /// <summary>
    /// Writes converts and writes the inputted XML document to an HTML document
    /// </summary>
    /// <param name="doc">Xml document to draw data from</param>
    /// <param name="outputPath">Directory path to output HTML pages</param>
    static void WriteHTML(XmlDocument doc, string outputPath) {
        string pagesPath = outputPath + "pages/";

        // create directiories
        try {
            // creates directories
            FileInfo directory = new FileInfo(pagesPath);
            directory.Directory.Create();

        } catch (Exception ex) {
            // error printing
            Console.WriteLine("Error in creating directories! Error: " + ex.Message);
        }

        List<DocContainer> containers = GenerateContainers(doc);

        // create and write all type HTML files
        foreach (DocContainer container in containers) {
            string title = container.Name;
            HTMLPage page = new HTMLPage(title, container, null);
            page.WriteToFile(pagesPath + title + ".html");
        }

        // create main page

        string innerHTML = (
            "<h1>Main documentation page</h1>\n" +
            "<p>sub pages:</p>\n" +
            "<ul>\n"
        );

        foreach (DocContainer container in containers) {
            string relPath = "pages/" + container.Name + ".html";

            // adds a list item linking to each container page thingy
            innerHTML +=
                "\t<li>" +
                    $"<a href=\"{relPath}\">{container.Name}</a>" +
                "</li>\n";
        }

        innerHTML += "</ul>\n";

        HTMLPage mainPage = new HTMLPage("Main documentation page", null, innerHTML);
        mainPage.WriteToFile(outputPath + "index.html");
    }
}
