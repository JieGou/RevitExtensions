<p align="center">
    <picture>
        <source media="(prefers-color-scheme: dark)" width="750" srcset="https://github.com/Nice3point/RevitExtensions/assets/20504884/d605eb83-74a7-4a47-9db8-cb0daced374e">
        <img alt="RevitLookup" width="750" src="https://github.com/Nice3point/RevitExtensions/assets/20504884/a1772d7d-38d4-4a9b-9985-1d83b8cbea8d">
    </picture>
</p>

## Improve your experience with Revit API

[![Nuget](https://img.shields.io/nuget/v/Nice3point.Revit.Extensions?style=for-the-badge)](https://www.nuget.org/packages/Nice3point.Revit.Extensions)
[![Downloads](https://img.shields.io/nuget/dt/Nice3point.Revit.Extensions?style=for-the-badge)](https://www.nuget.org/packages/Nice3point.Revit.Extensions)
[![Last Commit](https://img.shields.io/github/last-commit/Nice3point/RevitExtensions/develop?style=for-the-badge)](https://github.com/Nice3point/RevitExtensions/commits/develop)

Extensions minimize the writing of repetitive code, add new methods not included in RevitAPI, and also help you write chained methods without worrying about API versioning:

```c#
new ElementId(123469)
    .ToElement<Door>()
    .Mirror()
    .FindParameter("Height")
    .AsDouble()
    .ToMillimeters()
    .Round()
```

Extensions include annotations to help ReShaper parse your code and report warnings when a method may return null or the value returned by the method is not used in your code.

## Installation

You can install Extensions as a [nuget package](https://www.nuget.org/packages/Nice3point.Revit.Extensions).

Packages are compiled for a specific version of Revit, to support different versions of libraries in one project, use RevitVersion property.

```text
<PackageReference Include="Nice3point.Revit.Extensions" Version="$(RevitVersion).*"/>
```

Package included by default in [Revit Templates](https://github.com/Nice3point/RevitTemplates).

## Table of contents

- [Element Extensions](#ElementExtensions)
- [ElementId Extensions](#ElementIdExtensions)
- [Geometry Extensions](#GeometryExtensions)
- [Ribbon Extensions](#RibbonExtensions)
- [ContextMenu Extensions](#ContextMenuExtensions)
- [Unit Extensions](#UnitExtensions)
- [Host Extensions](#HostExtensions)
- [Label Extensions](#LabelExtensions)
- [Solid Extensions](#SolidExtensions)
- [Schema Extensions](#SchemaExtensions)
- [Parameter Extensions](#ParameterExtensions)
- [Application Extensions](#ApplicationExtensions)
- [Collector Extensions](#CollectorExtensions)
- [Imperial Extensions](#ImperialExtensions)
- [System Extensions](#SystemExtensions)

### <a id="ElementExtensions">Element Extensions</a>

The **FindParameter()** method finds a parameter in an element.
For instances that do not have such a parameter, this method will find and return it at the element type.
This method combines all API methods for getting a parameter into one, such as `get_Parameter`, `LookupParameter`, `GetParameter`.

```c#
element.FindParameter(ParameterTypeId.AllModelUrl);
element.FindParameter(BuiltInParameter.ALL_MODEL_URL);
element.FindParameter("URL");
```

The **Copy()** method copies an element and places the copy at a location indicated by a given transformation.

```c#
element.Copy(1, 1, 0);
element.Copy(new XYZ(1, 1, 0));
```

The **Mirror()** method creates a mirrored copy of an element about a given plane.

```c#
element.Mirror(plane);
```

The **Move()** method moves the element by the specified vector.

```c#
element.Move(1, 1, 0);
element.Move(new XYZ(1, 1, 0));
```

The **Rotate()** method rotates an element about the given axis and angle.

```c#
element.Rotate(axis, angle);
```

The **CanBeMirrored()** method determines whether element can be mirrored.

```c#
element.CanBeMirrored();
```

The **Cast<T>()** method casts the element to the specified type. Handy for joining a method into a chain.

```c#
Wall wall = element.Cast<Wall>();
Floor floor = element.Cast<Floor>();
HostObject hostObject = element.Cast<HostObject>();
```

### <a id="ElementIdExtensions">ElementId Extensions</a>

The **ToElement()** method returns the element from the Id for a specified document and convert to a type if necessary.

```c#
Element element = elementId.ToElement(document);
Wall wall = elementId.ToElement<Wall>(document);
```

The **AreEquals()** method checks if an ID matches BuiltInСategory or BuiltInParameter.

```c#
categoryId.AreEquals(BuiltInCategory.OST_Walls);
parameterId.AreEquals(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);
```

### <a id="GeometryExtensions">Geometry Extensions</a>

The **Distance()** method returns distance between two lines. The lines are considered endless.

```c#
var line1 = Line.CreateBound(new XYZ(0,0,1), new XYZ(1,1,1));
var line2 = Line.CreateBound(new XYZ(1,2,2), new XYZ(1,2,2));
var distance = line1.Distance(line2);
```

The **JoinGeometry()** method creates clean joins between two elements that share a common face.

```c#
element1.JoinGeometry(element2);
```

The **UnjoinGeometry()** method removes a join between two elements.

```c#
element1.UnjoinGeometry(element2);
```

The **AreElementsJoined()** method determines whether two elements are joined.

```c#
var isJoined = element1.AreElementsJoined(element2);
```

The **GetJoinedElements()** method returns all elements joined to given element.

```c#
var elements = element1.GetJoinedElements();
```

The **SwitchJoinOrder()** method reverses the order in which two elements are joined.

```c#
element1.SwitchJoinOrder();
```

The **IsCuttingElementInJoin()** method determines whether the first of two joined elements is cutting the second element.

```c#
var isCutting = element1.IsCuttingElementInJoin(element2);
```

The **SetCoordinateX(), SetCoordinateY(), SetCoordinateZ()** methods creates an instance of a curve with a new coordinate.

```c#
var newLine = line.SetCoordinateX(1);
var newLine = line.SetCoordinateY(1);
var newLine = line.SetCoordinateZ(1);
var newArc = arc.SetCoordinateX(1);
var newArc = arc.SetCoordinateY(1);
var newArc = arc.SetCoordinateZ(1);
```

### <a id="RibbonExtensions">Ribbon Extensions</a>

The **CreatePanel()** method create a new panel in the default AddIn tab or the specified tab. If the panel exists on the ribbon, the method will return it.

```c#
application.CreatePanel("Panel name");
application.CreatePanel("Panel name", "Tab name");
```

The **AddPushButton()** method adds a PushButton to the ribbon.

```c#
panel.AddPushButton(typeof(Command), "Button text");
panel.AddPushButton<Command>("Button text");
pullDownButton.AddPushButton(typeof(Command), "Button text");
pullDownButton.AddPushButton<Command>("Button text");
```

The **AddPullDownButton()** method adds a PullDownButton to the ribbon.

```c#
panel.AddPullDownButton("Button name", "Button text");
```

The **AddSplitButton()** method adds a SplitButton to the ribbon.

```c#
panel.AddSplitButton("Button name", "Button text");
```

The **AddRadioButtonGroup()** method adds a RadioButtonGroup to the ribbon.

```c#
panel.AddRadioButtonGroup("Button name");
```

The **AddComboBox()** method adds a ComboBox to the ribbon.

```c#
panel.AddComboBox("Button name");
```

The **AddTextBox()** method adds a TextBox to the ribbon.

```c#
panel.AddTextBox("Button name");
```

The **SetImage()** method adds an image to the RibbonButton.

```c#
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png");
button.SetImage("http://example.com/RibbonIcon16.png");
button.SetImage("C:\Pictures\RibbonIcon16.png");
```

The **SetLargeImage()** method adds a large image to the RibbonButton.

```c#
button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png");
button.SetLargeImage("http://example.com/RibbonIcon32.png");
button.SetLargeImage("C:\Pictures\RibbonIcon32.png");
```

The **SetAvailabilityController()** method specifies the class that decides the availability of PushButton

```c#
pushButton.SetAvailabilityController<CommandController>();
```

### <a id="ContextMenuExtensions">ContextMenu Extensions</a>

The **ConfigureContextMenu()** method registers an action used to configure a Context menu.

```c#
application.ConfigureContextMenu(menu =>
{
    menu.AddMenuItem<Command>("Menu title");
    menu.AddMenuItem<Command>("Menu title")
        .SetAvailabilityController<Controller>()
        .SetToolTip("Description");
});
```

You can also specify your own context menu title. By default, Revit uses the Application name

```c#
application.ConfigureContextMenu("Title", menu =>
{
    menu.AddMenuItem<Command>("Menu title");
});
```

The **AddMenuItem()** method adds a menu item to the Context Menu.

```c#
menu.AddMenuItem<Command>("Menu title");
```

The **AddSeparator()** method adds a separator to the Context Menu.

```c#
menu.AddSeparator();
```

The **AddSubMenu()** method adds a sub menu to the Context Menu.

```c#
var subMenu = new ContextMenu();
subMenu.AddMenuItem<Command>("Menu title");
subMenu.AddMenuItem<Command>("Menu title");

menu.AddSubMenu("Sub menu title", subMenu);
```

The **SetAvailabilityController()** method specifies the class type that decides the availability of menu item.

```c#
menuItem.SetAvailabilityController<Controller>()
```

### <a id="UnitExtensions">Unit Extensions</a>

The **FromMillimeters()** method converts millimeters to internal Revit number format (feet).

```c#
double(69).FromMillimeters() => 0.226
```

The **ToMillimeters()** method converts a Revit internal format value (feet) to millimeters.

```c#
double(69).ToMillimeters() => 21031
```

The **FromMeters()** method converts meters to internal Revit number format (feet).

```c#
double(69).FromMeters() => 226.377
```

The **ToMeters()** method converts a Revit internal format value (feet) to meters.

```c#
double(69).ToMeters() => 21.031
```

The **FromInches()** method converts inches to internal Revit number format (feet).

```c#
double(69).FromInches() => 5.750
```

The **ToInches()** method converts a Revit internal format value (feet) to inches.

```c#
double(69).ToInches() => 827.999
```

The **FromDegrees()** method converts degrees to internal Revit number format (radians).

```c#
double(69).FromDegrees() => 1.204
```

The **ToDegrees()** method converts a Revit internal format value (radians) to degrees.

```c#
double(69).ToDegrees() => 3953
```

The **FromUnit(UnitTypeId)** method converts the specified unit type to internal Revit number format.

```c#
double(69).FromUnit(UnitTypeId.Celsius) => 342.15
```

The **ToUnit(UnitTypeId)** method converts a Revit internal format value to the specified unit type.

```c#
double(69).ToUnit(UnitTypeId.Celsius) => -204.15
```

The **FormatUnit()** method formats a number with units into a string.

```c#
document.FormatUnit(SpecTypeId.Length, 69, false) => 21031
document.FormatUnit(SpecTypeId.Length, 69, false, new FormatValueOptions {AppendUnitSymbol = true}) => 21031 mm
```

### <a id="HostExtensions">Host Extensions</a>

The **GetBottomFaces()** method returns the bottom faces for the host object.

```c#
floor.Cast<HostObject>().GetBottomFaces();
```

The **GetTopFaces()** method returns the top faces for the host object.

```c#
floor.Cast<HostObject>().GetTopFaces();
```

The **GetSideFaces()** method returns the major side faces for the host object.

```c#
wall.Cast<HostObject>().GetSideFaces(ShellLayerType.Interior);
```

### <a id="LabelExtensions">Label Extensions</a>

The **ToLabel()** method convert Enum to user-visible name.

```c#
BuiltInCategory.OST_Walls.ToLabel() => "Walls"
BuiltInParameter.WALL_TOP_OFFSET.ToLabel() => "Top Offset"
BuiltInParameter.WALL_TOP_OFFSET.ToLabel(LanguageType.Russian) => "Смещение сверху"
BuiltInParameterGroup.PG_LENGTH.ToLabel() => "Length"
DisplayUnitType.DUT_KILOWATTS.ToLabel() => "Kilowatts"
ParameterType.Length.ToLabel() => "Length"

DisciplineTypeId.Hvac.ToLabel() => "HVAC"
GroupTypeId.Geometry.ToLabel() => "Dimensions"
ParameterTypeId.DoorCost.ToLabel() => "Cost"
SpecTypeId.SheetLength.ToLabel() => "Sheet Length"
SymbolTypeId.Hour.ToLabel() => "h"
UnitTypeId.Hertz.ToLabel() => "Hertz"
```

The **ToDisciplineLabel()** method convert ForgeTypeId to user-visible name a discipline.

```c#
DisciplineTypeId.Hvac.ToDisciplineLabel() => "HVAC"
```

The **ToGroupLabel()** method convert ForgeTypeId to user-visible name for a built-in parameter group.

```c#
GroupTypeId.Geometry.ToGroupLabel() => "Dimensions"
```

The **ToParameterLabel()** method convert ForgeTypeId to user-visible name for a built-in parameter.

```c#
ParameterTypeId.DoorCost.ToParameterLabel() => "Cost"
```

The **ToSpecLabel()** method convert ForgeTypeId to user-visible name for a spec.

```c#
SpecTypeId.SheetLength.ToSpecLabel() => "Sheet Length"
```

The **ToSymbolLabel()** method convert ForgeTypeId to user-visible name for a symbol.

```c#
SymbolTypeId.Hour.ToSymbolLabel() => "h"
```

The **ToUnitLabel()** method convert ForgeTypeId to user-visible name for a unit.

```c#
UnitTypeId.Hertz.ToUnitLabel() => "Hertz"
```

### <a id="SolidExtensions">Solid Extensions</a>

The **Clone()** method creates a new Solid which is a copy of the input Solid.

```c#
solid.Clone();
```

The **CreateTransformed()** method creates a new Solid which is the transformation of the input Solid.

```c#
solid.CreateTransformed(Transform.CreateRotationAtPoint());
solid.CreateTransformed(Transform.CreateReflection());
```

The **SplitVolumes()** method splits a solid geometry into several solids.

```c#
solid.SplitVolumes();
```

The **IsValidForTessellation()** method tests if the input solid or shell is valid for tessellation.

```c#
solid.IsValidForTessellation();
```

The **TessellateSolidOrShell()** method facets (i.e., triangulates) a solid or an open shell.

```c#
solid.TessellateSolidOrShell();
```

The **FindAllEdgeEndPointsAtVertex()** method find all EdgeEndPoints at a vertex identified by the input EdgeEndPoint.

```c#
edgeEndPoint.FindAllEdgeEndPointsAtVertex();
```

### <a id="SchemaExtensions">Schema Extensions</a>

The **SaveEntity()** method stores data in the element. Existing data is overwritten.

```c#
document.ProjectInformation.SaveEntity(schema, "data", "schemaField");
door.SaveEntity(schema, "white", "doorColorField");
```

The **LoadEntity()** method retrieves the value stored in the schema from the element.

```c#
var data = document.ProjectInformation.LoadEntity<string>(schema, "schemaField");
var color = door.LoadEntity<string>(schema, "doorColorField");
```

### <a id="ParameterExtensions">Parameter Extensions</a>

The **AsBool()** method provides access to the boolean value within the parameter

```c#
bool value = element.FindParameter("IsClosed").AsBool();
```

The **AsColor()** method provides access to the Color within the parameter

```c#
Color value = element.FindParameter("Door color").AsColor();
```

The **AsElement()** method provides access to the Element within the parameter

```c#
Element value = element.FindParameter("Door material").AsElement();
Material value = element.FindParameter("Door material").AsElement<Material>();
```

The **Set()** method sets the parameter to a new value

```c#
parameter.Set(true);
parameter.Set(new Color(66, 69, 96);
```

### <a id="ApplicationExtensions">Application Extensions</a>

The **Show()** method opens a window and returns without waiting for the newly opened window to close.
Sets the owner of a child window. Applicable for modeless windows to be attached to Revit.

```c#
new RevitAddinView.Show(uiApplication.MainWindowHandle)
```

### <a id="CollectorExtensions">Collector Extensions</a>

This set of extensions encapsulates all the work of searching for elements in the Revit database.

The **GetElements()** a generic method which constructs a new FilteredElementCollector that will search and filter the set of elements in a document.
Filter criteria are not applied to the method.

```c#
document.GetElements().WhereElementIsViewIndependent().ToElements();
document.GetElements(elementIds).WhereElementIsViewIndependent.ToElements();
document.GetElements(viewId).ToElements();
```

The remaining methods contain a ready implementation of the collector, with filters applied:

```c#
document.GetInstances();
document.GetInstances(new ElementParameterFilter());
document.GetInstances(new []{elementParameterFilter, logicalFilter});

document.GetInstances(BuiltInCategory.OST_Walls);
document.GetInstances(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.GetInstances(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});    

document.EnumerateInstances();
document.EnumerateInstances(new ElementParameterFilter());
document.EnumerateInstances(new []{elementParameterFilter, logicalFilter});

document.EnumerateInstances(BuiltInCategory.OST_Walls);
document.EnumerateInstances(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateInstances(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.EnumerateInstances<Wall>();
document.EnumerateInstances<Wall>(new ElementParameterFilter());
document.EnumerateInstances<Wall>(new []{elementParameterFilter, logicalFilter});

document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls);
document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   
```

The same overloads exist for InstanceIds, Type, TypeIds:
```c#
document.GetTypes();
document.GetTypeIds();
document.GetInstanceIds();
document.EnumerateTypes();
document.EnumerateTypeIds();
document.EnumerateInstanceIds();
```

For instances, overloads are available with viewId. The collector will search and filter the visible elements in the view:
```c#
document.GetInstances(viewId);
document.GetInstanceIds(viewId);
document.EnumerateInstances(viewId);
document.EnumerateInstanceIds(viewId);
```

**Remarks**: `Get` methods are faster than `Enumerate` due to RevitApi internal optimizations. 
However, enumeration allows for more flexibility in finding elements.
Don't try to call ```GetInstances().Select().Tolist()``` instead of ```EnumerateInstances().Select().Tolist()```, you will degrade performance.

### <a id="ImperialExtensions">Imperial Extensions</a>

The **ToFraction()** method converts a number to Imperial fractional format.

```c#
int(1).ToFraction() => 1’-0〞
double(0.0123).ToFraction() => 0 5/32〞
double(15.125).ToFraction() => 15’-1 1/2〞
double(-25.222).ToFraction() => 25’-2 21/32〞
double(-25.222).ToFraction(4) => 25’-2 3/4〞
```

The **FromFraction()**, **TryFromFraction()** methods convert the textual representation of the Imperial system number to number.

```c#
string("").FromFraction() => double(0)
string(1 17/64〞).FromFraction() => double(0.105)
string(1’1.75).FromFraction() => double(1.145)
string(-69’-69〞).FromFraction() => double(-74.75)

string(-2’-1 15/64〞).TryFromFraction(out var value) => true
string("-").TryFromFraction(out var value) => true
string(".").TryFromFraction(out var value) => false
string("value").TryFromFraction(out var value) => false
string(null).TryFromFraction(out var value) => false
```

### <a id="SystemExtensions">System Extensions</a>

The **Round()** method rounds the value to the specified precision or 1e-9 precision specified in Revit Api.

```c#
double(6.56170000000000000000000001).Round() => 6.5617
double(6.56170000000000000000000001).Round(0) => 7
```

The **IsAlmostEqual()** method compares two numbers within specified precision or 1e-9 precision specified in Revit Api.

```c#
double(6.56170000000000000000000001).IsAlmostEqual(6.5617) => true
double(6.56170000000000000000000001).IsAlmostEqual(6.6, 1e-1) => true
```

The **IsNullOrEmpty()** method same as string.IsNullOrEmpty().

```c#
string("").IsNullOrEmpty() => true
string(null).IsNullOrEmpty() => true
```

The **IsNullOrWhiteSpace()** method same as string.IsNullOrWhiteSpace().

```c#
string(" ").IsNullOrWhiteSpace() => true
string(null).IsNullOrWhiteSpace() => true
```

The **AppendPath()** method combines 2 paths.

```c#
"C:\Folder".AppendPath("AddIn") => "C:\Folder\AddIn"
"C:\Folder".AppendPath("AddIn", "file.txt") => "C:\Folder\AddIn\file.txt"
```

The **Contains()** indicating whether a specified substring occurs within this string with `StringComparison` support.

```c#
"Revit extensions".Contains("Revit", StringComparison.OrdinalIgnoreCase) => true
"Revit extensions".Contains("revit", StringComparison.OrdinalIgnoreCase) => true
"Revit extensions".Contains("REVIT", StringComparison.OrdinalIgnoreCase) => true
"Revit extensions".Contains("invalid", StringComparison.OrdinalIgnoreCase) => false
```