using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVehicleSearch_Click(object sender, EventArgs e)
        {
            frmSearch searchForm = new frmSearch(); // create new instance of frmSearch
            searchForm.ShowDialog(); // display search form
        }

        private void btnModifyDatabase_Click(object sender, EventArgs e)
        {
            frmInventory inventoryForm = new frmInventory(); // create new instance of frmInventory
            inventoryForm.ShowDialog(); // display inventory form
        }

        private void btnCalculateLoan_Click(object sender, EventArgs e)
        {
            frmLoanCalculator loanForm = new frmLoanCalculator(); // create new instance of frmLoanCalculator
            loanForm.ShowDialog(); // display loan form
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrder orderForm = new frmOrder(); // create new instance of frmOrder
            orderForm.ShowDialog(); // display order form
        }
    }
}
