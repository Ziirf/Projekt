using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 40);
            Console.SetBufferSize(150, 40);

            Frame FrameMain = new Frame(148, 39, 2, 1);
            FrameMain.AddVerticalDivider(2);
            FrameMain.AddVerticalDivider(34);
            FrameMain.AddHorizontalDivider(32, 2, 34);
            FrameMain.Print();


            Console.ReadKey();
        }
    }
}
