using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeConnect.Touch
{
    /// <summary>
    /// Interaction logic for TouchControl.xaml
    /// </summary>
    public partial class TouchControl : UserControl
    {
        const double FULL_CIRCLE = 360d;
        const double INNER_RADIUS = 50d;
        const double OUTER_RADIUS = 180d;
        public const double DIAMETER = 2 * OUTER_RADIUS;

        public TouchControl()
        {
            InitializeComponent();
        }
    }
}
