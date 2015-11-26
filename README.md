# Touch VS
**Touch interface for common functions of Visual Studio** ![logo](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/Resources/icon.png?raw=true)

## How to use it?

1. Install the VSIX from Visual Studio gallery or build the source
2. Launch the touch interface by double-tapping the code editor's surface.
3. Tap on the action you're interested in to invoke it. Tap outside of the control to hide it.

![gif](https://i.gyazo.com/c7008b663479f5fce1894c72fceb4f20.gif)

## Contributions

I saw a popular discussion on touch interface in Visual Studio and thought this would be a fun project. All contributions are welcome :)

[Please share](https://github.com/CodeConnect/TouchVS/issues) which commands you'd like to use in touch interface, or even better, submit a PR!

Relevant bits of code:
* [Available commands - CodeConnect.TouchVS.**Model.Commands**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/Model/Commands.cs)
* [Touch event - CodeConnect.TouchVS.**EditorExtensibility**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/EditorExtensibility.cs#L44)
* [Creating the UI - CodeConnect.TouchVS.**TouchControl**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/TouchControl.xaml.cs)
* [Theme and command execution - CodeConnect.TouchVS.**VisualStudioModule**](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/VisualStudioModule.cs)

