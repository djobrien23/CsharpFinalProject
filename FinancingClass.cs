using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class FinancingClass
    {
        private decimal _vehiclePrice;
        private decimal _downPayment;
        private decimal _tradeInValue;
        private decimal _salesTax;
        private decimal _interestRate;
        private int _numberOfMonths;
        private decimal _monthlyPayment;


        public FinancingClass()
        {
            _vehiclePrice = 0m;
            _downPayment = 0m;
            _tradeInValue = 0m;
            _salesTax = 0.0m;
            _interestRate = 0m;
            _numberOfMonths = 0;
            _monthlyPayment = 0m;
        }

        public FinancingClass(decimal vehiclePrice, decimal downPayment, decimal tradeInValue, 
            decimal salesTax, decimal interestRate, int numberOfMonths)
        {
            _vehiclePrice = vehiclePrice;
            _downPayment = downPayment;
            _tradeInValue = tradeInValue;
            _salesTax = salesTax;
            _interestRate = interestRate;
            _numberOfMonths = numberOfMonths;
        }
        
        public decimal VehiclePrice
        {
            get { return _vehiclePrice; }
            set { _vehiclePrice = value; }
        }

        public decimal DownPayment
        {
            get { return _downPayment; }
            set { _downPayment = value; }
        }

        public decimal TradeInValue
        {
            get { return _tradeInValue; }
            set { _tradeInValue = value; }
        }

        public decimal SalesTax
        {
            get { return _salesTax; }
            set { _salesTax = value; }
        }

        public decimal InterestRate
        {
            get { return _interestRate; }
            set { _interestRate = value; }
        }

        public int NumberOfMonths
        {
            get { return _numberOfMonths; }
            set { _numberOfMonths = value; }
        }

        public decimal MonthlyPayment
        {
            get { return _monthlyPayment; }
        }

        public void CalcMonthlyPayment()
        {
            //EMI = ( P × r × (1+r)^n ) / ((1+r)^n − 1)
            decimal p = _vehiclePrice - _downPayment - _tradeInValue;
            decimal r = _interestRate / 1200;
            int n = _numberOfMonths;
            decimal raisedPower = CalcPower(1 + r, n);
            
            _monthlyPayment = (p * r * raisedPower) / (raisedPower - 1);
        }

        public decimal CalcPower(decimal number, int power)
        {
            decimal raisedPower = 1m;

            for (int index = 0; index < power; index++)
            {
                raisedPower *= number;
            }
                return raisedPower;
        }


    }
}
