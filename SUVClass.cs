using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.App_Code
{
    public class SUVClass : AutomobileClass
    {
        private string _thirdRowSeating;
        private string _liftGate;
        private string _removableTop;

        public SUVClass ()
        {
            _thirdRowSeating = "";
            _liftGate = "";
            _removableTop = "";
        }

        public string ThirdRowSeating
        {
            get { return _thirdRowSeating; }
            set { _thirdRowSeating = value; }
        }

        public string LiftGate
        {
            get { return _liftGate; }
            set { _liftGate = value; }
        }

        public string RemovableTop
        {
            get { return _removableTop; }
            set { _removableTop = value; }
        }

    }
}
