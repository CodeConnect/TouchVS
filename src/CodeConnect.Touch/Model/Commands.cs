using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConnect.Touch.Model
{
    static public class Commands
    {
        static TouchBranchCommand entryPoint = new TouchBranchCommand("Entry point", new List<TouchCommand>()
            {
                new TouchBranchCommand("View", new List<TouchCommand>()
                {
                    new TouchVSCommand("Full screen", "View.FullScreen"),
                    new TouchVSCommand("Output", "View.Output"),
                    new TouchVSCommand("Error List", "View.ErrorList"),
                    new TouchVSCommand("Back", "View.NavigateBackward"),
                    new TouchVSCommand("Forward", "View.NavigateForward"),
                }),
                new TouchBranchCommand("Code", new List<TouchCommand>()
                {
                    new TouchVSCommand("References", "View.ShowReferences"),
                    new TouchVSCommand("Find", "Edit.FindInFiles"),

                }),
                new TouchBranchCommand("Build", new List<TouchCommand>()
                {
                    new TouchVSCommand("Build", "Build.BuildSolution"),
                    new TouchVSCommand("Rebuild", "Build.RebuildSolution"),
                    new TouchVSCommand("Deploy", "Build.DeploySolution"),
                    new TouchVSCommand("Clean", "Build.CleanSolution"),
                }),
                new TouchBranchCommand("File", new List<TouchCommand>()
                {
                    new TouchVSCommand("Save All", "File.SaveAll"),
                    new TouchVSCommand("Save", "File.SaveSelectedItem"),
                    new TouchVSCommand("Close", "File.Close"),
                    new TouchVSCommand("Close solution", "File.CloseSolution"),
                    new TouchVSCommand("Open", "File.OpenAll"),
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

        /// <summary>
        /// The list of commands available in VSTouch
        /// Visual Studio command names taken from https://raw.githubusercontent.com/ligershark/VoiceExtension/master/VoiceExtension/Resources/commands.txt
        /// </summary>
        public static TouchBranchCommand EntryPoint => entryPoint;
    }
}
