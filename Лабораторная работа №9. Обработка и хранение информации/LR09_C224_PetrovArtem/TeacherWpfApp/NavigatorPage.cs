using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TeacherWpfApp
{
    internal class NavigatorPage
    {
        public static Frame MainFrame { get; set; }
        public NavigatorPage()
        {
            MainFrame = new Frame();
        }
    }
}
