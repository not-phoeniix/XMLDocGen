using System.Xml;

namespace HTMLFromXML;

/// <summary>
/// Class representation of a class/struct/interface/etc. Holds a bunch of child DocElement's to display on page.
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

    /// <summary>
    /// List of all child DocElements's inside this container
    /// </summary>
    public List<DocElement> Elements { get; private set; } = new();

    private List<DocElement> Properties { get; set; } = new();
    private List<DocElement> Constructors { get; set; } = new();
    private List<DocElement> Methods { get; set; } = new();

    public void AddElement(DocElement element) {
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
        }

        Elements.Add(element);
    }

    public override string ToString() {
        // title string
        string html =
            $"<h2>{Name}</h2>\n" +
            $"<p>{Summary}</p>\n";

        // adds all constructors
        foreach (DocElement element in Constructors) {
           html += element.ToString() + "\n";
        }

        // adds all properties
        foreach (DocElement element in Properties) {
           html += element.ToString() + "\n";
        }

        // adds all
        foreach (DocElement element in Methods) {
           html += element.ToString() + "\n";
        }

        return html;
    }
}
