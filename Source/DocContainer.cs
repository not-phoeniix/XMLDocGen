namespace XMLDocGen;

/// <summary>
/// An object, like a class/struct/interface/etc. Holds a bunch of child DocElement's to display on page.
/// </summary>
public class DocContainer {
    /// <summary>
    /// Container name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Summary text of container
    /// </summary>
    public string Summary { get; private set; }

    private List<DocElement> elements = new();

    private List<DocElement> fields = new();
    private List<DocElement> properties = new();
    private List<DocElement> constructors = new();
    private List<DocElement> methods = new();
    private List<DocElement> events = new();

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
                properties.Add(element);
                break;

            case ElementType.Constructor:
                constructors.Add(element);
                break;

            case ElementType.Method:
                methods.Add(element);
                break;

            case ElementType.Field:
                fields.Add(element);
                break;

            case ElementType.Event:
                events.Add(element);
                break;
        }

        elements.Add(element);
    }

    /// <summary>
    /// Markdown representation of this current container
    /// </summary>
    /// <returns>String of Markdown for this current container</returns>
    public string AsMarkdown() {
        // title string
        string content = $"# {Name}\n";

        // sets summary, notifies if no summary exists
        content += Summary == null ?
            "No container summary found :(\n" :
            $"{Summary}\n";

        // adds all constructors if there are any
        if (constructors.Count != 0) {
            content += "## Constructors\n";
            foreach (DocElement element in constructors) {
                content += element.AsMarkdown() + "\n";
            }
        }

        // adds all fields if there are any
        if (fields.Count != 0) {
            content += "## Fields\n";
            foreach (DocElement element in fields) {
                content += element.AsMarkdown() + "\n";
            }
        }

        // adds all properties if there are any
        if (properties.Count != 0) {
            content += "## Properties\n";
            foreach (DocElement element in properties) {
                content += element.AsMarkdown() + "\n";
            }
        }

        // adds all events if there are any
        if (events.Count != 0) {
            content += "## Events\n";
            foreach (DocElement element in events) {
                content += element.AsMarkdown() + "\n";
            }
        }

        // adds all methods if there are any
        if (methods.Count != 0) {
            content += "## Methods\n";
            foreach (DocElement element in methods) {
                content += element.AsMarkdown() + "\n";
            }
        }

        string mainName = "main.md";
        content += $"[Back to main page](../{mainName})";

        return content;
    }

    /// <summary>
    /// HTML representation of this current container
    /// </summary>
    /// <returns>String of HTML for this current container</returns>
    public string AsHTML() {
        // title string
        string content = $"<h1>{Name}</h1>\n";

        // sets summary, notifies if no summary exists
        content += Summary == null ?
            "<p>No container summary found :(</p>\n" :
            $"<p>{Summary}</p>\n";

        // adds all constructors if there are any
        if (constructors.Count != 0) {
            content += "<h2>Constructors</h2>\n";
            foreach (DocElement element in constructors) {
                content += element.AsHTML() + "\n";
            }
        }

        // adds all fields if there are any
        if (fields.Count != 0) {
            content += "<h2>Fields</h2>\n";
            foreach (DocElement element in fields) {
                content += element.AsHTML() + "\n";
            }
        }

        // adds all properties if there are any
        if (properties.Count != 0) {
            content += "<h2>Properties</h2>\n";
            foreach (DocElement element in properties) {
                content += element.AsHTML() + "\n";
            }
        }

        // adds all events if there are any
        if (events.Count != 0) {
            content += "<h2>Events</h2>\n";
            foreach (DocElement element in events) {
                content += element.AsHTML() + "\n";
            }
        }

        // adds all methods if there are any
        if (methods.Count != 0) {
            content += "<h2>Methods</h2>\n";
            foreach (DocElement element in methods) {
                content += element.AsHTML() + "\n";
            }
        }

        string mainName = "main.md";
        content += $"<footer><a href=\"../{mainName}\">Back to main page</a></footer>";

        return content;
    }
}
