using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.App_Code
{
    public class CarClass : AutomobileClass
    {
        private string _convertible;
        private string _groundEffects;
        private string _electric;
    
        public CarClass()
        {
            _convertible = "";
            _groundEffects = "";
            _electric = "";
        }

        public string Convertible
        {
            get { return _convertible; }
            set { _convertible = value; }
        }

        public string GroundEffects
        {
            get { return _groundEffects; }
            set { _groundEffects = value; }
        }

        public string Electric
        {
            get { return _electric; }
            set { _electric = value; }
        }

    }
}
