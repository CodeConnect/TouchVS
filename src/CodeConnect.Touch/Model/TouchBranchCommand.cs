using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConnect.Touch.Model
{
    public class TouchBranchCommand : TouchCommand
    {
        public IEnumerable<TouchCommand> Commands { get; private set; }

        public TouchBranchCommand(string displayName, IEnumerable<TouchCommand> commands)
        {
            DisplayName = displayName;
            Commands = commands;
        }
    }
}
