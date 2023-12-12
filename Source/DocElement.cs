using System.Xml;

namespace HTMLFromXML;

public enum ElementType {
    Property,
    Constructor,
    Method,
    Type
}

/// <summary>
/// Class representation of an individual "member" that isn't a class. Can hold things like methods, properties, constructors, etc.
/// </summary>
public class DocElement {
    /// <summary>
    /// Name of this element
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Name of containing class/struct/etc that this member is in
    /// </summary>
    public string ContainerName { get; private set; }

    /// <summary>
    /// Member summary
    /// </summary>
    public string Summary { get; private set; }

    /// <summary>
    /// Dictionary of all parameters. Key == parameter name, value == parameter summary
    /// </summary>
    public Dictionary<string, string> Params { get; set; } = new();

    /// <summary>
    /// Member returns summary
    /// </summary>
    public string Returns { get; set; }

    /// <summary>
    /// Type of element, can be a method, property, constructor, etc
    /// </summary>
    public ElementType Type { get; private set; }

    /// <summary>
    /// Creates a new DocElement from the data in the parent XmlNode
    /// </summary>
    /// <param name="node">Node to extract data from</param>
    public DocElement(XmlNode node) {
        // gets the name/type attribute from XML
        string elementText = node.Attributes[0].InnerText;

        // splits into array
        string[] splitString = elementText.Split(":");
        Name = splitString[1];
        Type = GetType(splitString[0], Name);

        // creates a "Namespace.ClassName" string
        string[] splitName = Name.Split(".");
        ContainerName = $"{splitName[0]}.{splitName[1]}";

        XmlNodeList children = node.ChildNodes;

        // iterates thru all child nodes, updating values of this element
        foreach (XmlNode child in children) {
            // general summary
            if (child.Name == "summary") {
                Summary = child.InnerText.Trim();
            }

            // parameters
            if (child.Name == "param") {
                string key = child.Attributes[0].InnerText.Trim();
                string value = child.InnerText.Trim();

                Params.Add(key, value);
            }

            // return statement
            if (child.Name == "returns") {
                Returns = child.InnerText.Trim();
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
        }

        throw new Exception($"ERROR: ElementType {type} not recognized...");
    }

    /// <summary>
    /// HTML representation of this current element
    /// </summary>
    /// <returns>String of HTML for this current element</returns>
    public override string ToString() {
        string html = (
            $"<h3>{Name}</h3>\n" +
            $"<p><em>Summary:</em> {Summary}</p>\n"
        );

        foreach (KeyValuePair<string, string> pair in Params) {
            html += $"<p><em>param</em> {pair.Key}: {pair.Value}</p>\n";
        }

        if (Returns != null) {
            html += $"<p><em>returns:</em> {Returns}</p>\n";
        }

        return html;
    }
}
