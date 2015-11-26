using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConnect.Touch.Model
{
    /// <summary>
    /// The list of commands available in VSTouch
    /// Visual Studio command names taken from https://raw.githubusercontent.com/ligershark/VoiceExtension/master/VoiceExtension/Resources/commands.txt
    /// </summary>
    static public class Commands
    {
        static TouchBranchCommand entryPoint = new TouchBranchCommand("Entry point", new List<TouchCommand>()
            {
                new TouchBranchCommand("Build", new List<TouchCommand>()
                {
                    new TouchVSCommand("Debug", "Debug.Start"),
                    new TouchVSCommand("Build", "Build.BuildSolution"),
                    new TouchVSCommand("Rebuild", "Build.RebuildSolution"),
                    new TouchVSCommand("Clean", "Build.CleanSolution"),
                    new TouchVSCommand("Deploy", "Build.DeploySolution"),
                }),
                new TouchBranchCommand("Code", new List<TouchCommand>()
                {
                    new TouchVSCommand("F12", "Edit.GoToDefinition"),
                    new TouchVSCommand("Replace", "Edit.ReplaceinFiles"),
                    new TouchVSCommand("References", "Edit.FindAllReferences"),
                    new TouchVSCommand("Find", "Edit.FindInFiles"),

                }),
                new TouchBranchCommand("View", new List<TouchCommand>()
                {
                    new TouchVSCommand("Full screen", "View.FullScreen"),
                    new TouchVSCommand("Back", "View.NavigateBackward"),
                    new TouchVSCommand("Error List", "View.ErrorList"),
                    new TouchVSCommand("Output", "View.Output"),
                    new TouchVSCommand("Forward", "View.NavigateForward"),
                }),
                new TouchBranchCommand("File", new List<TouchCommand>()
                {
                    new TouchVSCommand("Save All", "File.SaveAll"),
                    new TouchVSCommand("Save", "File.SaveSelectedItems"),
                    new TouchVSCommand("Close", "File.Close"),
                    new TouchVSCommand("Close solution", "File.CloseSolution"),
                    new TouchVSCommand("Open", "File.OpenFile"),
                }),
                new TouchBranchCommand("Test", new List<TouchCommand>()
                {
                    new TouchVSCommand("Run All", "RunAllTestsInSolution"),
                    new TouchVSCommand("Debug All", "DebugAllTestsInSolution"),
                    new TouchVSCommand("Results", "View.TestResults"),
                    new TouchVSCommand("Test Explorer", "TestExplorer.ShowTestExplorer"),
                    new TouchVSCommand("Run failed", "TestExplorer.RunFailedTests"),
                })
            });

        public static TouchBranchCommand EntryPoint => entryPoint;
    }
}
