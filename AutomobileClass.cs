using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class AutomobileClass
    {
        private string _make;
        private string _model;
        private int _year;
        private string _vin;
        private string _color;
        private int _mileage;
        private int _engine;
        private string _transmission;
        private string _driveType;
        private int _doors;
        private decimal _price;

        public AutomobileClass()
        {
            _make = "";
            _model = "";
            _year = 0;
            _vin = "";
            _color = "";
            _mileage = 0;
            _engine = 0;
            _transmission = "";
            _driveType = "";
            _doors = 0;
            _price = 0m;
        }

        public AutomobileClass(string make, string model, int year, string vin, string color, int mileage, int engine, string transmission, string driveType, int doors, decimal price)
        {
            _make = make;
            _model = model;
            _year = year;
            _vin = vin;
            _color = color;
            _mileage = mileage;
            _engine = engine;
            _transmission = transmission;
            _driveType = driveType;
            _doors = doors;
            _price = price;
        }

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string VIN
        {
            get { return _vin; }
            set { _vin = value; }
        }
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public int Mileage
        {
            get { return _mileage; }
            set { _mileage = value; }
        }
        public int Engine
        {
            get { return _engine; }
            set { _engine = value; }
        }
        public string Transmission
        {
            get { return _transmission; }
            set { _transmission = value; }
        }
        public string DriveType
        {
            get { return _driveType; }
            set { _driveType = value; }
        }
        public int Doors
        {
            get { return _doors; }
            set { _doors = value; }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}
