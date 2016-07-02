using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.App_Code
{
    public class TruckClass : AutomobileClass
    {
        private string _cabType;
        private double _bedLength;
        private string _towPackage;

        public TruckClass()
        {
            _cabType = "";
            _bedLength = 0;
            _towPackage = "";
        }

        public string CabType
        {
            get { return _cabType; }
            set { _cabType = value; }
        }

        public double BedLength
        {
            get { return _bedLength; }
            set { _bedLength = value; }
        }

        public string TowPackage
        {
            get { return _towPackage; }
            set { _towPackage = value; }
        }

    }
}
