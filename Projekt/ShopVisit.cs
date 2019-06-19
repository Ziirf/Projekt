using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class ShopVisit
    {
        private int visitID;
        public int VisitID
        {
            get { return visitID; }
            set { visitID = value; }
        }

        private DateTime dateTimeVisit;
        public DateTime DateTimeVisit
        {
            get { return dateTimeVisit; }
            set { dateTimeVisit = value; }
        }

        private string mechanic;
        public string Mechanic
        {
            get { return mechanic; }
            set { mechanic = value; }
        }

        private int vinNumber;
        public int VinNumber
        {
            get { return vinNumber; }
            set { vinNumber = value; }
        }

        private int kmCount;
        public int KmCount
        {
            get { return kmCount; }
            set { kmCount = value; }
        }

        private string issue;
        public string Issue
        {
            get { return issue; }
            set { issue = value; }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public ShopVisit(int visitID, DateTime dateTimeVisit, string mechanic, int vinNumber, int kmCount, string issue, string notes)
        {
            VisitID = visitID;
            DateTimeVisit = dateTimeVisit;
            Mechanic = mechanic;
            VinNumber = vinNumber;
            KmCount = kmCount;
            Issue = issue;
            Notes = notes;
        }
    }
}
