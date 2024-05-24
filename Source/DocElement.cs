using System.Xml;

namespace XMLDocGen;

public enum ElementType {
    Property,
    Field,
    Constructor,
    Method,
    Type,
    Event
}

/// <summary>
/// An individual member of an object. Can hold things like methods, properties, constructors, etc.
/// </summary>
public class DocElement {
    private string name;
    private string summary;
    private string returns;
    private string containerName;
    private ElementType type;
    private Dictionary<string, string> parameters = new();

    /// <summary>
    /// Name of this element
    /// </summary>
    public string Name => name;

    /// <summary>
    /// Name of containing class/struct/etc that this member is in
    /// </summary>
    public string ContainerName => containerName;

    /// <summary>
    /// Member summary
    /// </summary>
    public string Summary => summary;

    /// <summary>
    /// Type of element, can be a method, property, constructor, etc
    /// </summary>
    public ElementType Type => type;

    /// <summary>
    /// Creates a new DocElement from the data in the parent XmlNode
    /// </summary>
    /// <param name="node">Node to extract data from</param>
    public DocElement(XmlNode node) {
        // gets the name/type attribute from XML
        string elementText = node.Attributes[0].InnerText;

        // splits into array
        string[] splitString = elementText.Split(":");
        name = splitString[1];
        type = GetType(splitString[0], name);

        // creates a "Namespace.ClassName" string
        string[] splitName = name.Split(".");
        containerName = $"{splitName[0]}.{splitName[1]}";

        XmlNodeList children = node.ChildNodes;

        // iterates thru all child nodes, updating values of this element
        foreach (XmlNode child in children) {
            // general summary
            if (child.Name == "summary") {
                summary = child.InnerText.Trim();
            }

            // parameters
            if (child.Name == "param") {
                string key = child.Attributes[0].InnerText.Trim();
                string value = child.InnerText.Trim();

                parameters.Add(key, value);
            }

            // return statement
            if (child.Name == "returns") {
                returns = child.InnerText.Trim();
            }
        }
    }

    /// <summary>
    /// Determines type of element based on input string (one letter, P/M/T)
    /// </summary>
    /// <param name="type">Single letter representation of element's type</param>
    /// <param name="name">Name following the element type</param>
    /// <returns>ElementType that corresponds to the inputted type string</returns>
    private ElementType GetType(string type, string name) {
        switch (type) {
            case "F":
                return ElementType.Field;

            case "P":
                return ElementType.Property;

            case "M":
                if (name.Contains("#ctor")) {
                    return ElementType.Constructor;
                } else {
                    return ElementType.Method;
                }

            case "T":
                return ElementType.Type;

            case "E":
                return ElementType.Event;
        }

        throw new Exception($"ERROR: ElementType {type} not recognized...");
    }

    /// <summary>
    /// Markdown representation of this current element
    /// </summary>
    /// <returns>String of Markdown for this current element</returns>
    public string AsMarkdown() {
        string content =
            $"### {Name}\n" +
            $"*Summary:* {Summary}\n";

        foreach (KeyValuePair<string, string> pair in parameters) {
            content += $"*param* {pair.Key}: {pair.Value}\n";
        }

        if (returns != null) {
            content += $"*returns:* {returns}\n";
        }

        return content;

    }

    /// <summary>
    /// HTML representation of this current element
    /// </summary>
    /// <returns>String of HTML for this current element</returns>
    public string AsHTML() {
        string content =
            $"<h3>{Name}</h3>\n" +
            $"<p><em>Summary:</em> {Summary}</p>\n";

        foreach (KeyValuePair<string, string> pair in parameters) {
            content += $"<p><em>param</em> {pair.Key}: {pair.Value}</p>\n";
        }

        if (returns != null) {
            content += $"<p><em>returns:</em> {returns}</p>\n";
        }

        return content;
    }
}
