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
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class frmVehicleDetails : Form
    {
        public static decimal price = 0m; // variable to hold price from database so it can be converted to appropriate string format
                                          // and be passed to loan calculation form
        public frmVehicleDetails()
        {
            InitializeComponent();
        }

        internal void LoadData(string stockNo) // method creates new table loading it with information from inventory database
        {                                      // using stockNo from vehicle search form 
            string cmdStr = "Select * From Inventory Where StockNumber = @StockNo";
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C#\FinalProject\FinalProject\App_Data\InventoryDatabase.mdf;Integrated Security=True");
            SqlCommand command = new SqlCommand(cmdStr, connection);
            {
                command.Parameters.AddWithValue("@StockNo", stockNo);

                SqlDataAdapter sdAdapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sdAdapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    this.picVehicle.ImageLocation = dr[12].ToString(); // vehicle imagae
                    lblVehicle.Text = dr[4].ToString() + " " + dr[3].ToString() + " " + dr[2].ToString(); // year make model
                    price = decimal.Parse(dr[11].ToString()); // convert price to decimal format
                    lblPrice.Text = price.ToString("c2"); // format price to currency 2 decimal
                    lblStockNumber.Text = dr[0].ToString(); // stock number (id number)
                    lblColor.Text = dr[4].ToString(); // vehicle color
                    lblVIN.Text = dr[1].ToString(); // vehicle VIN
                    lblMileage.Text = dr[6].ToString(); // vehicle mileage
                    lblEngine.Text = dr[7].ToString() + " cyl"; // vehicle engine size
                    lblDoors.Text = dr[10].ToString(); // number of doors
                    lblTransmission.Text = dr[8].ToString(); // transmission type
                    lblDriveType.Text = dr[9].ToString(); // drive type
                    if (dr[13].ToString() != "") // determine if vehicle contains car class info, if so fill labels accordingly
                    {
                        lblVechOption1.Text = "Convertible:"; // is vehicle convertible?
                        lblVechOption1Answer.Text = dr[13].ToString();

                        lblVechOption2.Text = "GroundEffects:"; // does it have ground effects?
                        lblVechOption2Answer.Text = dr[14].ToString();

                        lblVechOption3.Text = "Electric:"; // electric motor?
                        lblVechOption3Answer.Text = dr[15].ToString();
                    }
                    else if (dr[16].ToString() != "") // determine if vehicle contains suv class info, if so fill labels accordingly
                    {
                        lblVechOption1.Text = "3 Row Seating:"; // third row of seats?
                        lblVechOption1Answer.Text = dr[16].ToString();

                        lblVechOption2.Text = "Lift-gate:"; // manual or automatic transmission?
                        lblVechOption2Answer.Text = dr[17].ToString();

                        lblVechOption3.Text = "Removable Top:"; // does top come off, if so soft or hard top
                        lblVechOption3Answer.Text = dr[18].ToString();
                    }
                    else if (dr[19].ToString() != "") // determine if vehicle contains truck class info, if so fill labels accordingly
                    {
                        lblVechOption1.Text = "Cab Type:"; // standard, crew cab, mega, extended, quad?
                        lblVechOption1Answer.Text = dr[19].ToString();

                        lblVechOption2.Text = "Bed Length:"; // length of bed in feet
                        lblVechOption2Answer.Text = dr[20].ToString();

                        lblVechOption3.Text = "Tow Package:"; // does it have tow package?
                        lblVechOption3Answer.Text = dr[21].ToString();
                    }
                }
            }
        }

        private void btnCalcLoan_Click(object sender, EventArgs e)
        {
            frmLoanCalculator loanForm = new frmLoanCalculator(price.ToString("n2")); // create new instance of frmLoanCalculator and pass it vehicle price
            loanForm.ShowDialog(); // display the loan form
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {   // takes a screen shot to print form
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height - 100));
            e.Graphics.DrawImage((Image)bmp, x, y);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            PrintPreviewDialog test = new PrintPreviewDialog();
            doc.PrintPage += this.Doc_PrintPage;
            doc.PrinterSettings.DefaultPageSettings.Landscape = true;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;

            test.Document = doc;
            test.ShowDialog();
        }
    }
}
