namespace HTMLFromXML;

public class HTMLPage {
    /// <summary>
    /// Title of the HTML page
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Entire inner HTML string
    /// </summary>
    public string HTML { get; private set; } = "";

    /// <summary>
    /// Creates a new HTMLPage object
    /// </summary>
    /// <param name="title">Title of page</param>
    /// <param name="container">Container that contains XML document data (optional)</param>
    /// <param name="additionalHTML">Any additional HTML string to add after container stuff (optional)</param>
    public HTMLPage(string title, DocContainer? container, string? additionalHTML) {
        Title = title;

        // starting HTML
        HTML += (
            "<!DOCTYPE html>\n" +
            "<html lang=\"en\">\n" +
            "\n" +
            "<head>\n" +
                "\t<meta charset=\"UTF-8\">\n" +
                "\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                $"\t<title>{title}</title>\n" +
            "</head>\n" +
            "\n" +
            "<body>\n"
        );

        // body HTML
        if (container != null)
            HTML += container.ToString();

        if (additionalHTML != null)
            HTML += additionalHTML;

        // ending HTML
        HTML += (
            "</body>\n" +
            "\n" +
            "</html>\n"
        );
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
            for (int i = 0; i < HTML.Length; i++) {
                if (HTML[i] != '\n') {
                    // add character to line string
                    line += HTML[i];

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
