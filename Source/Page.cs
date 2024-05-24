namespace XMLDocGen;

public class Page {
    /// <summary>
    /// String that contains all content for entire page
    /// </summary>
    private string content = "";

    /// <summary>
    /// Creates a new Page object
    /// </summary>
    /// <param name="filename">File name of this page, extension not included</param>
    /// <param name="container">Container that contains XML document data (optional)</param>
    /// <param name="additionalContent">Any additional HTML string to add after container stuff (optional)</param>
    public Page(string filename, bool asMarkdown, DocContainer container = null, string additionalContent = "") {
        // starting HTML
        if (!asMarkdown) {
            content +=
                "<!DOCTYPE html>\n" +
                "<html lang=\"en\">\n" +
                "\n" +
                "<head>\n" +
                    "\t<meta charset=\"UTF-8\">\n" +
                    "\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                    $"\t<title>{filename}</title>\n" +
                "</head>\n" +
                "\n" +
                "<body>\n";
        }

        // body HTML
        if (container != null) {
            content += asMarkdown ?
                container.AsMarkdown() :
                container.AsHTML();
        }

        // additional content
        content += additionalContent;

        // ending HTML
        if (!asMarkdown) {
            content +=
                "</body>\n" +
                "\n" +
                "</html>\n";
        }
    }

    /// <summary>
    /// Writes the HTMLPage's html content to a file
    /// </summary>
    /// <param name="filepath">Filepath of new HTML file</param>
    public void WriteToFile(string filepath) {
        StreamWriter writer = null;

        try {
            writer = new StreamWriter(filepath);

            List<string> htmlList = new();

            // fill htmlList with lines of HTML from big string
            string line = "";
            for (int i = 0; i < content.Length; i++) {
                if (content[i] != '\n') {
                    // add character to line string
                    line += content[i];

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

            Console.WriteLine($"Successfully wrote HTML file at filepath \"{filepath}\"!");

        } catch (Exception ex) {
            // error printing
            Console.WriteLine("Error in writing HTML! Error: " + ex.Message);

        } finally {
            // close writer object if it's not null
            //   (flushing stream buffer and creating file)
            writer?.Close();
        }
    }
}
