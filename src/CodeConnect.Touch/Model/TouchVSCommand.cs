using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConnect.Touch.Model
{
    public class TouchVSCommand : TouchCommand
    {
        public string VsCommandName { get; private set; }
        public string VsCommandParams { get; private set; }

        public TouchVSCommand(string displayName, string commandName, string commandParams = "")
        {
            DisplayName = displayName;
            VsCommandName = commandName;
            VsCommandParams = commandParams;
        }
    }
}
