# Touch VS
**Touch interface for common functions of Visual Studio** 

[Download from Visual Studio Gallery](https://visualstudiogallery.msdn.microsoft.com/fb0207b6-ccd1-43b4-92ea-1dd0f3c58fe5)

![v1.0](https://github.com/CodeConnect/TouchVS/blob/master/screenshot.png?raw=true)

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

## See also
[Voice Commands extension](https://github.com/ligershark/VoiceExtension) ([VSIX](https://visualstudiogallery.msdn.microsoft.com/ce35c120-405a-435b-af2a-52ff24eb2c30)) - Source for command names and code that executes thme

![logo](https://github.com/CodeConnect/TouchVS/blob/master/src/CodeConnect.Touch/Resources/preview.png?raw=true)
