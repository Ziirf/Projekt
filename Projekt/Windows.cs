using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Windows
    {
        public static void MainWindow()
        {

        }

        public static void CustomerWindow()
        {
            Frame frameCustomer = new Frame(Program.frameDim);
            frameCustomer.AddVerticalDivider(2, 0, Program.frameDim[0]);
            frameCustomer.AddVerticalDivider(35, 0, Program.frameDim[0]);
            frameCustomer.AddHorizontalDivider(32, 2, 35);


        }
    }
}
