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

        // testing to see how easy this'll be
        XmlNodeList tempList = doc.GetElementsByTagName("member");
        foreach (XmlNode memberNode in tempList) {
            string attribute = memberNode.Attributes[0].InnerText;
            string[] splitString = attribute.Split(":");
            string type = splitString[0];
            string name = splitString[1];

            // writes name header
            Console.WriteLine($"--- {name} ---");

            // determines type
            string typeString = "undetermined";
            switch (type) {
                case "P":
                    typeString = "Property";
                    break;

                case "M":
                    if (name.Contains("#ctor")) {
                        typeString = "Constructor";
                    } else {
                        typeString = "Method";
                    }

                    break;

                case "T":
                    typeString = "Class";
                    break;
            }
            Console.WriteLine($"Type: {typeString}");

            foreach (XmlNode child in memberNode.ChildNodes) {
                Console.WriteLine(child.InnerText.Trim());
            }

            Console.WriteLine();
        }

        // WriteHTML(doc);
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
                $"\t<title>{GetProjectName(doc)}</title>" +
            "</head>\n" +
            "\n" +
            "<body>\n"
        );

        html += $"<h1>{GetProjectName(doc)}</h1>";

        // add the body shit (ALL THE XML CONTENT)

        html += (
            "</body>\n" +
            "\n" +
            "</html>\n"
        );

        // write HTML to a file
        StreamWriter writer = null;
        try {
            // open writer
            writer = new StreamWriter("doc.html");
            List<string> htmlList = new();

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
