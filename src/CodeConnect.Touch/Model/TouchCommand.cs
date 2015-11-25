using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConnect.Touch.Model
{
    public abstract class TouchCommand
    {
        public string DisplayName { get; protected set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
