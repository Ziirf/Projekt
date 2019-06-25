using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    interface IObjects
    {
        string StringFormat { get; set; }
        void Read(Frame frame, int left, int top);
        void Update(Frame frame, int left, int top);
    }
}
