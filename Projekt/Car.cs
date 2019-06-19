using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Car
    {
        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private string vinNumber;
        public string VinNumber
        {
            get { return vinNumber; }
            set { vinNumber = value; }
        }

        private string numberPlate;
        public string NumberPlate
        {
            get { return numberPlate; }
            set { numberPlate = value; }
        }

        private string carBrand;
        public string CarBrand
        {
            get { return carBrand; }
            set { carBrand = value; }
        }

        private string carModel;
        public string CarModel
        {
            get { return carModel; }
            set { carModel = value; }
        }

        private string productionYear;
        public string ProductionYear
        {
            get { return productionYear; }
            set { productionYear = value; }
        }

        private int kmCount;
        public int KmCount
        {
            get { return kmCount; }
            set { kmCount = value; }
        }

        private string fuelType;
        public string FuelType
        {
            get { return fuelType; }
            set { fuelType = value; }
        }

    }
}
