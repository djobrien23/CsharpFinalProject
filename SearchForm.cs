using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class frmSearch : Form
    {
        

        public frmSearch()
        {
            InitializeComponent();
        }

        private void btnReturnToMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {   // load vehicle manufactures in make drop down list box
            ddlMake.Items.Add("Chevrolet");
            ddlMake.Items.Add("Dodge");
            ddlMake.Items.Add("Ford");
            ddlMake.Items.Add("Jeep");

            for (int index = 2000; index <= 2016; index++) // load start and end year in respective drop down list box
            {
                ddlStartYear.Items.Add(index);
                ddlEndYear.Items.Add(index);
            }
            
        }

        private void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[][] vehicleArray = new string[4][]; // create jagged array assigned with models respective of make
            vehicleArray[0] = new string[]{"Camaro", "Corvette", "Silverado"};
            vehicleArray[1] = new string[] { "Charger", "Challenger", "Ram 1500", "Viper" };
            vehicleArray[2] = new string[] { "Explorer", "Fusion", "Mustang", "F150" };
            vehicleArray[3] = new string[] { "Grand Cherokee", "Wrangler"};

            ddlModel.Items.Clear();
            ddlModel.Text = "-Select-"; //initial text of model drop down telling user to select model

            for (int index = 0; index < vehicleArray[ddlMake.SelectedIndex].Length; index++) // load models in model drop down list based on selected index of make that correlates with array
            {
                ddlModel.Items.Add(vehicleArray[ddlMake.SelectedIndex][index]);
            }
           
        }

        private void SearchInventory() // method searches inventory based on user selection
        {
            
            this.lstSearchResults.Items.Clear(); // clear listbox so not to have duplicate date
            this.lstSearchResults.Items.Add("Stock#" + "\t" + "Year" + "\t" + "Make" + "\t" + "Model" + "\t" + "Color");
            // cmdStr contains the query statement
            // uses paramaters to help combat sql injections
            string cmdStr = "SELECT StockNumber, Year, Make, Model, Color, ImageFilePath FROM Inventory WHERE (Year Between @StartYear And @EndYear) And Make = @Make And Model = @Model ORDER BY Year";
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C#\FinalProject\FinalProject\App_Data\InventoryDatabase.mdf;Integrated Security=True");
            SqlCommand command = new SqlCommand(cmdStr, connection);
            {
                command.Parameters.AddWithValue("@Make", ddlMake.SelectedItem.ToString()); // make parameter
                command.Parameters.AddWithValue("@Model", ddlModel.SelectedItem.ToString()); // model parameter
                command.Parameters.AddWithValue("@StartYear", ddlStartYear.SelectedItem.ToString()); // startyear paramater
                command.Parameters.AddWithValue("@EndYear", ddlEndYear.SelectedItem.ToString()); // endyear paramater

                SqlDataAdapter sdAdapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sdAdapter.Fill(dt);
                foreach (DataRow dr in dt.Rows) // fill list box with each row that meets the search criteria
                {
                    this.lstSearchResults.Items.Add(dr[0].ToString() + "\t" + dr[1].ToString() + "\t" + dr[2].ToString() + "\t" + dr[3].ToString() + "\t" + dr[4].ToString());
                    //Columns: stocknumber, year, make, model, color respectively
                }
            }
        }

        private void btnViewVehicle_Click(object sender, EventArgs e)
        {
            // do get a substring of item in listbox to get stock number
            // send the stock number to new forum
            // query database with that stock number
            // load specs based on stock number
            if (lstSearchResults.SelectedIndex > 0) // check that user selected a vehicle from list to view
            {
                frmVehicleDetails vehicleDetailSheet = new frmVehicleDetails(); // create new instance of frmVehicleDetails
                string stockNo = lstSearchResults.SelectedItem.ToString().Substring(0, 4); // get vehicle stock number from selected item
                vehicleDetailSheet.LoadData(stockNo); // call LoadData method on vehiclDetailSheet form, to load data from inventory database using stockNo as search paramater
                vehicleDetailSheet.ShowDialog(); // display vehicleDetailSheet form
            }
            else
               MessageBox.Show("Please select vehicle from list you wish to view!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlMake.SelectedIndex >= 0) // check that make was selected
            {
                if (ddlModel.SelectedIndex >= 0) // check that model was selected
                {
                    if (ddlStartYear.SelectedIndex >= 0) // check that start year was selected
                    {
                        if (ddlEndYear.SelectedIndex >= 0) // check that end year was select
                        {
                            SearchInventory(); // user entered all search criteria, call method that searches inventory database
                            lstSearchResults.Focus(); // set focus to list box
                            lstSearchResults.SelectedIndex = 0; // set focus to first row of list box
                            if (lstSearchResults.Items.Count <= 1)
                                lstSearchResults.Items.Add("No vehicles found meeting search criteria!");
                        }
                        else
                            MessageBox.Show("Must select end year!");
                    }
                    else
                        MessageBox.Show("Must select start year!");
                }
                else
                    MessageBox.Show("Must select model!");
            }
            else
                MessageBox.Show("Must select make!");
        }

        private void ddlStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // prevent user from picking invalid range (an end year that is earlier than a start year)
            ddlEndYear.Items.Clear(); // clear endyear drop down listbox
            ddlEndYear.Text = "-Select-"; // set text property to select letting user know they need to make selection
            for (int index = (int)ddlStartYear.SelectedItem; index <= 2016; index++) // load endyear drop down listbox with year values >= start year
            {
                ddlEndYear.Items.Add(index); 
            }

        }
    }
}