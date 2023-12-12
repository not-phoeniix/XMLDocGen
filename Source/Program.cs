using System.Collections;
using System.Xml;

namespace HTMLFromXML;

public class Program {
    static void Main(string[] args) {
        if (args.Length == 0) {
            Console.WriteLine("ERROR: No filepath provided...\n");
            PrintHelp();
            return;
        }

        string filepath = args[0];

        // create document object and load file, exit and print if file not found
        XmlDocument doc = new();
        try {
            doc.Load(filepath);
        } catch (Exception ex) {
            Console.WriteLine("Error loading file! " + ex.Message);
            return;
        }

        // provide status updates lol
        Console.WriteLine($"Successfully loaded XML file at path \"{filepath}\"!");
        Console.WriteLine($"Project name: {GetProjectName(doc)}");

        WriteHTML(doc);
    }

    /// <summary>
    /// Prints the help message for running the command
    /// </summary>
    static void PrintHelp() {
        Console.WriteLine(
            "HTMLFromXML - Generate an HTML page file from C# XML\n" +
            "\n" +
            "Usage:\n" +
            "HTMLFromXML [filepath]\n" +
            "(where filepath is the path to the xml file to be converted)"
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
    static void WriteHTML(XmlDocument doc) {
        string html = "";

        // top HTML
        html += (
            "<!DOCTYPE html>\n" +
            "<html lang=\"en\">\n" +
            "\n" +
            "<head>\n" +
                "\t<meta charset=\"UTF-8\">\n" +
                "\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                $"\t<title>{GetProjectName(doc)}</title>\n" +
            "</head>\n" +
            "\n" +
            "<body>\n"
        );

        // body HTML
        html += $"<h1>{GetProjectName(doc)}</h1>";
        List<DocContainer> containers = GenerateContainers(doc);
        foreach (DocContainer container in containers) {
            html += container.ToString();
            html += "<br>\n\n";
        }

        // ending HTML
        html += (
            "</body>\n" +
            "\n" +
            "</html>\n"
        );

        string outputFolder = "bin/output/";

        // write HTML to a file
        StreamWriter writer = null;
        try {
            // creates directories
            FileInfo directory = new FileInfo(outputFolder + "pages/");
            directory.Directory.Create();

            // open writer
            writer = new StreamWriter(outputFolder + "doc.html");
            List<string> htmlList = new();

            // fill htmlList with lines of HTML from big string
            string line = "";
            for (int i = 0; i < html.Length; i++) {
                if (html[i] != '\n') {
                    // add character to line string
                    line += html[i];

                } else {
                    // add line and reset string
                    htmlList.Add(line);
                    line = "";
                }
            }

            // write the lines to the file buffer
            foreach (string s in htmlList) {
                writer.WriteLine(s);
            }

            Console.WriteLine("Successfully wrote HTML file!!");

        } catch (Exception ex) {
            // error printing
            Console.WriteLine("Error in writing HTML! Error: " + ex.Message);

        } finally {
            // close writer if it's not null
            //   (flushing stream and creating file)
            writer?.Close();
        }
    }
}
