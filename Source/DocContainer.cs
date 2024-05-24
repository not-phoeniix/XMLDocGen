using System.Xml;

namespace XMLDocGen;

/// <summary>
/// Class representation of a class/struct/interface/etc. Holds a bunch of child DocElement's to display on page.
/// </summary>
public class DocContainer {
    /// <summary>
    /// Container name
    /// </summary>
    public string Name { get; private set; } = null;

    /// <summary>
    /// Summary text of container
    /// </summary>
    public string Summary { get; private set; } = null;

    /// <summary>
    /// Whether or not this container represents a markdown file
    /// </summary>
    public bool IsMarkdown { get; set; } = false;

    /// <summary>
    /// List of all child DocElements's inside this container
    /// </summary>
    public List<DocElement> Elements { get; private set; } = new();

    private List<DocElement> Fields { get; set; } = new();
    private List<DocElement> Properties { get; set; } = new();
    private List<DocElement> Constructors { get; set; } = new();
    private List<DocElement> Methods { get; set; } = new();
    private List<DocElement> Events { get; set; } = new();

    /// <summary>
    /// Adds an element to this container's html structure
    /// </summary>
    /// <param name="element">Element to add</param>
    public void AddElement(DocElement element) {
        // sets container name to element's container name if null
        Name ??= element.ContainerName;

        switch (element.Type) {
            case ElementType.Type:
                Name = element.Name;
                Summary = element.Summary;
                break;

            case ElementType.Property:
                Properties.Add(element);
                break;

            case ElementType.Constructor:
                Constructors.Add(element);
                break;

            case ElementType.Method:
                Methods.Add(element);
                break;

            case ElementType.Field:
                Fields.Add(element);
                break;

            case ElementType.Event:
                Events.Add(element);
                break;
        }

        Elements.Add(element);
    }

    /// <summary>
    /// HTML representation of this container and all its elements
    /// </summary>
    /// <returns>HTML string</returns>
    public override string ToString() {
        // title string
        string html = $"<h1>{Name}</h1>\n";

        // sets summary, notifies if no summary exists
        html += Summary == null ?
            "<p>No container summary found :(</p>\n" :
            $"<p>{Summary}</p>\n";

        // adds all constructors if there are any
        if (Constructors.Count != 0) {
            html += "<h2>Constructors</h2>\n";
            foreach (DocElement element in Constructors) {
                html += element.ToString() + "\n";
            }

        }

        if (Fields.Count != 0) {
            html += "<h2>Fields</h2>\n";
            foreach (DocElement element in Fields) {
                html += element.ToString() + "\n";
            }
        }

        // adds all properties if there are any
        if (Properties.Count != 0) {
            html += "<h2>Properties</h2>\n";
            foreach (DocElement element in Properties) {
                html += element.ToString() + "\n";
            }
        }

        if (Events.Count != 0) {
            html += "<h2>Events</h2>\n";
            foreach (DocElement element in Events) {
                html += element.ToString() + "\n";
            }
        }

        // adds all methods if there are any
        if (Methods.Count != 0) {
            html += "<h2>Methods</h2>\n";
            foreach (DocElement element in Methods) {
                html += element.ToString() + "\n";
            }
        }

        string mainName = IsMarkdown ? "main.md" : "index.html";
        html += $"<footer><a href=\"../{mainName}\">Back to main page</a></footer>";

        return html;
    }
}
