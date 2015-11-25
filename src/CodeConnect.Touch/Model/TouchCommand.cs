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

        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherTouchCommand = obj as TouchCommand;
            if (obj == null)
            {
                return false;
            }
            return this.DisplayName == otherTouchCommand.DisplayName;
        }
    }
}
