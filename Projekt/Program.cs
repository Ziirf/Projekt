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
            int[] winSize = { 190, 40 };
            int[] frameDim = { 190, 40, 0, 1 };

            SQL.ReadCustomerToObj();
            SQL.ReadCarToObj();

            //Console.SetWindowSize(190, 40);
            //Console.SetBufferSize(190, 40);
            //Console.WindowLeft = 0;

            //Frame FrameMain = new Frame(190, 39, 0, 1);
            //FrameMain.AddVerticalDivider(2, 0, 190);
            //FrameMain.AddVerticalDivider(35, 0, 190);
            //FrameMain.AddHorizontalDivider(32, 2, 35);
            //FrameMain.Print();
            //FrameMain.CustomerOverview(33, 3, new[] { 1, 10, 30, 50, 70, 90, 110 });

            Console.SetWindowSize(winSize[0], winSize[1]);
            Console.SetBufferSize(winSize[0], winSize[1]);
            Console.WindowLeft = 0;

            Frame FrameMain = new Frame(frameDim);
            FrameMain.AddVerticalDivider(1, 0, frameDim[0]);
            FrameMain.AddVerticalDivider(35, 0, frameDim[0]);
            FrameMain.AddHorizontalDivider(32, 1, 35);
            FrameMain.Print();
            //FrameMain.CustomerOverview(33, 3, new[] { 1, 10, 30, 50, 70, 90 ,110, 130 });
            //FrameMain.CarOverview(33, 3, new[] { 1, 10, 30, 50, 70, 90, 110, 130 });


            Console.ReadKey();
        }
    }
}
