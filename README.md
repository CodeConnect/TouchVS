# Touch VS
**Touch interface for common functions of Visual Studio** ![logo](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/Resources/icon.png?raw=true)


## How to use it?

Install the VSIX from Visual Studio gallery or build the source
Launch the touch interface by double-tapping the code editor's surface.

## Contributions

Contributions are welcome.

[Please share](https://github.com/CodeConnect/TouchVS/issues) which commands you'd like to use in touch interface.

Relevant bits of code:
* [Available commands - CodeConnect.TouchVS.**Model.Commands**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/Model/Commands.cs)
* [Touch event - CodeConnect.TouchVS.**EditorExtensibility**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/EditorExtensibility.cs#L44)
* [Creating the UI - CodeConnect.TouchVS.**TouchControl**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/TouchControl.xaml.cs)
* [Theme and command execution - CodeConnect.TouchVS.**VisualStudioModule**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/VisualStudioModule.cs)

## How to build

It should work out of the box
