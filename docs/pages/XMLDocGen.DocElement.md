# XMLDocGen.DocElement
An individual member of an object. Can hold things like methods, properties, constructors, etc.
## Constructorns
### XMLDocGen.DocElement.#ctor(System.Xml.XmlNode)
*Summary:* Creates a new DocElement from the data in the parent XmlNode
*param* node: Node to extract data from

## Properties
### XMLDocGen.DocElement.Name
*Summary:* Name of this element

### XMLDocGen.DocElement.ContainerName
*Summary:* Name of containing class/struct/etc that this member is in

### XMLDocGen.DocElement.Summary
*Summary:* Member summary

### XMLDocGen.DocElement.Type
*Summary:* Type of element, can be a method, property, constructor, etc

## Methods
### XMLDocGen.DocElement.GetType(System.String,System.String)
*Summary:* Determines type of element based on input string (one letter, P/M/T)
*param* type: Single letter representation of element's type
*param* name: Name following the element type
*returns:* ElementType that corresponds to the inputted type string

### XMLDocGen.DocElement.AsMarkdown
*Summary:* Markdown representation of this current element
*returns:* String of Markdown for this current element

### XMLDocGen.DocElement.AsHTML
*Summary:* HTML representation of this current element
*returns:* String of HTML for this current element

