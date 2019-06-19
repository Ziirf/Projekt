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

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public Car(int customerID, string vinNumber, string numberPlate, string carBrand, string carModel, string productionYear, int kmCount, string fuelType, DateTime createdDate)
        {
            CustomerID = customerID;
            VinNumber = vinNumber;
            NumberPlate = numberPlate;
            CarBrand = carBrand;
            CarModel = carModel;
            ProductionYear = productionYear;
            KmCount = kmCount;
            FuelType = fuelType;
            CreatedDate = createdDate;
        }
    }
}
