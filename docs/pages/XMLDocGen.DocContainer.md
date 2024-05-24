# XMLDocGen.DocContainer
An object, like a class/struct/interface/etc. Holds a bunch of child DocElement's to display on page.
## Properties
### XMLDocGen.DocContainer.Name
*Summary:* Container name

### XMLDocGen.DocContainer.Summary
*Summary:* Summary text of container

## Methods
### XMLDocGen.DocContainer.AddElement(XMLDocGen.DocElement)
*Summary:* Adds an element to this container's html structure
*param* element: Element to add

### XMLDocGen.DocContainer.AsMarkdown
*Summary:* Markdown representation of this current container
*returns:* String of Markdown for this current container

### XMLDocGen.DocContainer.AsHTML
*Summary:* HTML representation of this current container
*returns:* String of HTML for this current container

