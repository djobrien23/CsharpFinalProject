using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class frmLoanCalculator : Form
    {
        FinancingClass newLoanEst;

        public frmLoanCalculator()
        {
            InitializeComponent();
        }

        public frmLoanCalculator(string price) // load form with vehicle price from vehicle specsheet form
        {
            InitializeComponent();
            txtVehiclePrice.Text = price; // assign price to vehicle price text property
        }

        public void HideErrorLabels() // method to hide error labels when clicking calculate, to recheck entries
        {
            lblErrorPrice.Visible = false;
            lblErrorDownPayment.Visible = false;
            lblErrorTradeIn.Visible = false;
            lblErrorTax.Visible = false;
            lblErrorInterest.Visible = false;
            lblErrorTerms.Visible = false;
        }

        public void GetLoanInput(ref bool isValid)
        {
            decimal decimalValue; // variable to store tryparse of decimal values
            int intValue; // variable to store tryparse of integer values

            if (decimal.TryParse(txtVehiclePrice.Text, out decimalValue)) // if vehicle price is valid decimal entry
                newLoanEst.VehiclePrice = decimalValue; // assign to decimal value to VehiclePrice property
            else
            {
                lblErrorPrice.Visible = true; // show error message for invalid price
                txtVehiclePrice.Focus(); // set focus to textbox
                txtVehiclePrice.SelectAll(); 
                isValid = false; //
            }

            if (decimal.TryParse(txtDownPayment.Text, out decimalValue)) // if downpayment is valid decimal entry
                newLoanEst.DownPayment = decimalValue; // assign to decimal value to downpayment property
            else
            {
                lblErrorDownPayment.Visible = true; // show error message for downpayment entry
                txtDownPayment.Focus(); // set focus to textbox
                txtDownPayment.SelectAll();
                isValid = false;
            }

            if (decimal.TryParse(txtTradeInValue.Text, out decimalValue)) // if tradeinvalue is valid decimal entry
                newLoanEst.TradeInValue = decimalValue; // assign to decimal value to tradeinvalue property
            else
            {
                lblErrorTradeIn.Visible = true; // show error message for tradein entry
                txtTradeInValue.Focus(); // set focus to textbox
                txtTradeInValue.SelectAll();
                isValid = false;
            }

            if (decimal.TryParse(txtSalesTax.Text, out decimalValue)) // if salestax is valid decimal entry
                newLoanEst.SalesTax = decimalValue; // assign to decimal value to salestax property
            else
            {
                lblErrorTax.Visible = true; // show error message for tax entry
                txtSalesTax.Focus(); // set focus to textbox
                txtSalesTax.SelectAll();
                isValid = false;
            }

            if (decimal.TryParse(txtInterestRate.Text, out decimalValue)) // if interestrate is valid decimal entry
                newLoanEst.InterestRate = decimalValue;  // assign to decimal value to interestrate property
            else
            {
                lblErrorInterest.Visible = true; // show error message for interestrate entry
                txtInterestRate.Focus(); // set focus to textbox
                txtInterestRate.SelectAll();
                isValid = false;
            }

            if (int.TryParse(txtMonths.Text, out intValue)) // if terms is valid integer entry
                newLoanEst.NumberOfMonths = intValue;  // assign to integer value to numberofmonths property
            else
            {
                lblErrorTerms.Visible = true; // show error message for interestrate entry
                txtMonths.Focus(); // set focus to textbox
                txtMonths.SelectAll();
                isValid = false;
            }
        }

        private void btnCloseCalculator_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            HideErrorLabels(); // call method to hide error labels, so only new error messages will display if there are any
            bool isValid = true; // bool value to store true or false depending on if user enters valid data
            newLoanEst = new FinancingClass(); // new instnace of Financing Class
            GetLoanInput(ref isValid); // Get/validate user input
            if (isValid == true) // if user entries are valid
            {
                if (newLoanEst.VehiclePrice - newLoanEst.DownPayment - newLoanEst.TradeInValue >= 0) //check that vehicle is greater than the sume of the downpayment and tradein
                {
                    newLoanEst.CalcMonthlyPayment(); // call method of financing class to calculate monthly payment
                    lblMonthlyPayment.Text = newLoanEst.MonthlyPayment.ToString("n2"); // display monthly payment in lblmonthlypayment.
                }
                else
                    MessageBox.Show("Cost of vehicle can not be less than down payment and trade-in!");
            }
            
        }

        private void frmLoanCalculator_Load(object sender, EventArgs e)
        {
            const decimal INTEREST_RATE = 0.06m; // interest rate of state cannot be changed by regular user
            txtSalesTax.Text = INTEREST_RATE.ToString(); //display interest rate on form
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {   // takes a screen shot to print form
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height-100));
            e.Graphics.DrawImage((Image)bmp, x, y);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Today.ToShortDateString(); // show current date on quote form
            lblQuote.Visible = true; // display quote text
            lblDate.Visible = true; // display date

            PrintDocument doc = new PrintDocument();
            PrintPreviewDialog test = new PrintPreviewDialog();
            doc.PrintPage += this.Doc_PrintPage;
            doc.PrinterSettings.DefaultPageSettings.Landscape = true;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            
            test.Document = doc;
            test.ShowDialog();

            lblQuote.Visible = false;  // after print hide quote text
            lblDate.Visible = false; // after print hide date
        }
    }
}
