using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace FinalProject
{
    public partial class frmOrder : Form
    {
        App_Code.CarClass newCar;
        App_Code.SUVClass newSUV;
        App_Code.TruckClass newTruck;

        public static List<string> vehicles = new List<string>();

        bool inputValid;

        public frmOrder()
        {
            InitializeComponent();
        }

        public bool ValidateMakeAndColorInput(string userInput, bool inputValid) // validates car make input
        {
            Match match = Regex.Match(userInput, "^([a-z]+( [a-z]+)?)$", RegexOptions.IgnoreCase);
            if (match.Success == true) // if string starts and contains only letters,  and optionally can be followed by a space and letters
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateModel(string userInput, bool inputValid) // validates input for car model and license plate
        {
            Match match = Regex.Match(userInput, "^([a-z0-9]+( [a-z0-9]+)?)$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to start with letter or number only, and optionally can be followed by a space and letters or numbers
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateYearInput(string userInput, bool inputValid) // validates input for car model and license plate
        {
            Match match = Regex.Match(userInput, "^\\d{4}$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to start with letter or number only, and optionally can be followed by a space and letters or numbers
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateEngineAndDoorInput(string userInput, bool inputValid) // validates input for car model and license plate
        {
            Match match = Regex.Match(userInput, "^\\d{1}$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows single number
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateTransmissionInput(string userInput, bool inputValid) // validates input for car model and license plate
        {
            Match match = Regex.Match(userInput, "^Automatic$|^Manual$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to be automatic or manual only
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateDriveTypeInput(string userInput, bool inputValid) // validates input for car model and license plate
        {
            Match match = Regex.Match(userInput, "(^[4ARF]WD)$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to be automatic or manual only
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidatePriceInput(string userInput, bool inputValid) // validates input for car model and license plate
        {
            Match match = Regex.Match(userInput, "^(\\d{1,5}\\.\\d{2,4})$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows price 1-4 digits follow by a . and 2-4 digits
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        private bool GetVehicleInfo(ref string make, ref string model, ref int year, ref string color, ref int engine, ref string transmission, ref string driveType, ref int doors, ref decimal price)
        {
            inputValid = false;

            inputValid = ValidateMakeAndColorInput(txtMake.Text, inputValid); // call for validation of make; letters and space only
            if (inputValid == true)
            {
                make = txtMake.Text;
                inputValid = ValidateModel(txtModel.Text, inputValid); // call for validation of model; letters, number, space
                if (inputValid == true)
                {   // if model entry is valid assign txtModel.Text to newCar.Model, and proceed to next validation
                    model = txtModel.Text;
                    inputValid = ValidateYearInput(txtYear.Text, inputValid); // call for validation of Year; 4 digits only
                    if (inputValid == true)
                    {
                        year = int.Parse(txtYear.Text);
                        inputValid = ValidateMakeAndColorInput(txtColor.Text, inputValid); // call for validation of color; letters and space only
                        if (inputValid == true)
                        {
                            color = txtColor.Text;
                            inputValid = ValidateEngineAndDoorInput(txtEngine.Text, inputValid); // call for validation of egine; single digit only
                            if (inputValid == true)
                            {
                                engine = int.Parse(txtEngine.Text);
                                inputValid = ValidateTransmissionInput(txtTransmission.Text, inputValid); // call for validation of transmission; Automatic or Manual
                                if (inputValid == true)
                                {
                                    transmission = txtTransmission.Text;
                                    inputValid = ValidateDriveTypeInput(txtDriveType.Text, inputValid); // call for validation of drivetype; 3 chars letters or number only
                                    if (inputValid == true)
                                    {
                                        driveType = txtDriveType.Text;
                                        inputValid = ValidateEngineAndDoorInput(txtDoors.Text, inputValid); // call for validation of doors; single digit only
                                        if (inputValid == true)
                                        {
                                            doors = int.Parse(txtDoors.Text);
                                            inputValid = ValidatePriceInput(txtPrice.Text, inputValid); // call for validation of price; #####.#### \d{5}\.\d{0,4}
                                            if (inputValid == true)
                                            {
                                                price = decimal.Parse(txtPrice.Text);
                                            }
                                            else
                                                MessageBox.Show("Invalid Price! 1-5 numbers before . and 2-4 numbers following it.");
                                        }
                                        else
                                            MessageBox.Show("Invalid Doors! Only one interger value.");
                                    }
                                    else
                                        MessageBox.Show("Invalid DriveType! 4WD, AWD, RWD, FWD.");
                                }
                                else
                                    MessageBox.Show("Invalid Transmission! Automatic or Manual.");
                            }
                            else
                                MessageBox.Show("Invalid Engine! Only one interger value.");
                        }
                        else
                            MessageBox.Show("Invalid Color! Letters only.");
                    }
                    else
                        MessageBox.Show("Invalid Year! Format: 2016.");
                }
                else
                    MessageBox.Show("Invalid Model! Letters and numbers only.");
            }
            else
                MessageBox.Show("Invalid Make! Letters only."); 
            
            return inputValid;
        }

        public bool ValidateChildInfo(string userInput, bool inputValid) // validates input for car/suv/truck info
        {
            Match match = Regex.Match(userInput, "^Yes$|^No$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to be automatic or manual only
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateLiftGateInfo(string userInput, bool inputValid) // validates input suv liftgate  info
        {
            Match match = Regex.Match(userInput, "^No$|^Power$|^Manual$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to be no, power, or manual only
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateRemovableTopInfo(string userInput, bool inputValid) // validates input suv removable top info
        {
            Match match = Regex.Match(userInput, "^No$|^SoftTop$|^HardTop$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to be no, softop, hardtop
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        public bool ValidateCabTypeInfo(string userInput, bool inputValid) // validates input truck cabtype info
        {
            Match match = Regex.Match(userInput, "^Regular$|^Extended$|^Crew$|^Quad$|^Mega$", RegexOptions.IgnoreCase);
            if (match.Success == true) // allows string to be regluar, extended, crew, quad, or mega
            {
                if (userInput.Length != 0) // check that text box is not empty
                    inputValid = true; // if both test are passed boolean true
                else
                    inputValid = false; // if string empty boolean false
            }
            else
                inputValid = false; // expression not matched boolean false

            return inputValid; // return boolean variable
        }

        private bool GetCarInfo(ref string convertible, ref string groundEffects, ref string electric) // get car information
        {
            inputValid = false;
            inputValid = ValidateChildInfo(txtConvertible.Text, inputValid); // validate convertible entry
            if (inputValid == true)
            {
                convertible = txtConvertible.Text;
                inputValid = ValidateChildInfo(txtGroundEffects.Text, inputValid); // validate groundeffects entry
                if (inputValid == true)
                {
                    groundEffects = txtGroundEffects.Text;
                    inputValid = ValidateChildInfo(txtElectric.Text, inputValid); // validate electric entry
                    if (inputValid == true)
                    {
                        electric = txtElectric.Text;
                    }
                    else
                        MessageBox.Show("Invalid Electric Entry! Yes or No.");
                }
                else
                    MessageBox.Show("Invalid GroundEffects Entry! Yes or No.");
            }
            else
                MessageBox.Show("Invalid Convertible Entry! Yes or No.");

            return inputValid; // return bool value
        }

        private bool GetSUVInfo(ref string thirdRow, ref string liftGate, ref string removableTop) // get suv information
        {
            inputValid = false;
            inputValid = ValidateChildInfo(txtThirdRowSeating.Text, inputValid);
            if(inputValid == true)
            {
                thirdRow = txtThirdRowSeating.Text;
                inputValid = ValidateLiftGateInfo(txtLiftGate.Text, inputValid);
                if(inputValid == true)
                {
                    liftGate = txtLiftGate.Text;
                    inputValid = ValidateRemovableTopInfo(txtRemovableTop.Text, inputValid);
                    if (inputValid == true)
                    {
                        removableTop = txtRemovableTop.Text;
                    }
                    else
                        MessageBox.Show("Invalid RemovableTop! No, SoftTop, or HardTop.");
                }
                else
                    MessageBox.Show("Invalid LiftGate! No, Power, or Manual.");
            }
            else
                MessageBox.Show("Invalid ThirdRowSeating! Yes or No.");

            return inputValid;
        }

        private bool GetTruckInfo(ref string cabType, ref double bedLength, ref string towPackage) // get truck information
        {
            inputValid = false;
            inputValid = ValidateCabTypeInfo(txtCabType.Text, inputValid); // check if cabtype is valid entry
            if (inputValid == true)
            {
                cabType = txtCabType.Text;
                if (double.TryParse(txtBedLength.Text, out bedLength)) // check if bedlength is a double value
                {
                    inputValid = ValidateChildInfo(txtTowPackage.Text, inputValid); // check if towpackage is valid entry
                    if (inputValid == true)
                    {
                        towPackage = txtTowPackage.Text;
                    }
                    else
                        MessageBox.Show("Invalid TowPacakge! Yes or No.");
                }
                else
                {
                    inputValid = false;
                    MessageBox.Show("Invalid Bedlength! Double value only.");
                }
            }
            else
                MessageBox.Show("Invalid Cabtype! Standard, Extended, Crew, Mega, or Quad.");

            return inputValid;
        }

        private void MakeReadyForNewVehicle() // Make form ready for new vehicle entry
        {
            cbxVehicleType.Focus(); // set focus to vehicle type drop down list
            cbxVehicleType.Text = "-Select-";
            cbxVehicleType.SelectAll();
            // clear textboxes
            txtBedLength.Clear();
            txtCabType.Clear();
            txtColor.Clear();
            txtConvertible.Clear();
            txtDoors.Clear();
            txtDriveType.Clear();
            txtElectric.Clear();
            txtEngine.Clear();
            txtGroundEffects.Clear();
            txtLiftGate.Clear();
            txtMake.Clear();
            txtModel.Clear();
            txtPrice.Clear();
            txtRemovableTop.Clear();
            txtThirdRowSeating.Clear();
            txtTowPackage.Clear();
            txtTransmission.Clear();
            txtYear.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddVehicle_Click(object sender, EventArgs e)
        {
            // intialize variables for testing
            string make = "", model = "", color = "", transmission = "", driveType = "";
            int year = 0, engine = 00, doors = 00;
            decimal price = 0m;
            bool inputValid = false;

            if (cbxVehicleType.SelectedIndex == 0) // if user selected car from combobox
            {
                string convertible = "", groundEffects = "", electric = "";
                // check if user input is valid and get user input
                inputValid = GetVehicleInfo(ref make, ref model, ref year, ref color, ref engine, ref transmission, ref driveType, ref doors, ref price);
                if (inputValid == true)
                {
                    inputValid = GetCarInfo(ref convertible, ref groundEffects, ref electric);
                    if (inputValid == true) // if user entered data is valid
                    {
                        newCar = new App_Code.CarClass(); // create new instance of CarClass
                        // assign values to properites
                        newCar.Make = make;
                        newCar.Model = model;
                        newCar.Year = year;
                        newCar.Color = color;
                        newCar.Engine = engine;
                        newCar.Transmission = transmission;
                        newCar.DriveType = driveType;
                        newCar.Doors = doors;
                        newCar.Price = price;
                        newCar.Convertible = convertible;
                        newCar.GroundEffects = groundEffects;
                        newCar.Electric = electric;
                        // add car/automobile properties to vehicles list 
                        vehicles.Add("Make: " + newCar.Make + " Model: " + newCar.Model + " Year: " +
                            newCar.Year.ToString() + " Color: " + newCar.Color + " Engine: " + newCar.Engine.ToString() +
                            " Transmission: " + newCar.Transmission + "Drive Type: " + newCar.DriveType + " Doors: " +
                            +newCar.Doors + " Price: " + newCar.Price.ToString() + " Convertible: " + newCar.Convertible + " GroundEffects: " +
                            newCar.GroundEffects + " Electric: " + newCar.Electric);

                        MakeReadyForNewVehicle(); // makes form ready for new entry
                    }
                }
            }
            else if (cbxVehicleType.SelectedIndex == 1) // if user selected suv from combobox
            {
                string thirdRow = "", liftGate = "", removableTop = "";
                // check if user input is valid and get user input
                inputValid = GetVehicleInfo(ref make, ref model, ref year, ref color, ref engine, ref transmission, ref driveType, ref doors, ref price);
                if (inputValid == true)
                {
                    inputValid = GetSUVInfo(ref thirdRow, ref liftGate, ref removableTop);
                    if (inputValid == true) // if user entered data is valid
                    {
                        newSUV = new App_Code.SUVClass();
                        // assign values to properites
                        newSUV.Make = make;
                        newSUV.Model = model;
                        newSUV.Year = year;
                        newSUV.Color = color;
                        newSUV.Engine = engine;
                        newSUV.Transmission = transmission;
                        newSUV.DriveType = driveType;
                        newSUV.Doors = doors;
                        newSUV.Price = price;
                        newSUV.ThirdRowSeating = thirdRow;
                        newSUV.LiftGate = liftGate;
                        newSUV.RemovableTop = removableTop;
                        // add suv/automobile properties to vehicles list
                        vehicles.Add("Make: " + newSUV.Make + " Model: " + newSUV.Model + " Year: " +
                            newSUV.Year.ToString() + " Color: " + newSUV.Color + " Engine: " + newSUV.Engine.ToString() +
                            " Transmission: " + newSUV.Transmission + "Drive Type: " + newSUV.DriveType + " Doors: " +
                            +newSUV.Doors + " Price: " + newSUV.Price.ToString() + " ThirdRowSeating: " + newSUV.ThirdRowSeating + " LiftGate: " +
                            newSUV.LiftGate + " RemovableTop: " + newSUV.RemovableTop);

                        MakeReadyForNewVehicle(); // makes form ready for new entry
                    }
                }
            }

            else if (cbxVehicleType.SelectedIndex == 2) // if user selected truck from combobox
            {
                string cabType = "", towPackage = "";
                double bedLength = 0;
                // check if user input is valid and get user input
                inputValid = GetVehicleInfo(ref make, ref model, ref year, ref color, ref engine, ref transmission, ref driveType, ref doors, ref price);
                if (inputValid == true)
                {
                    inputValid = GetTruckInfo(ref cabType, ref bedLength, ref towPackage);
                    if (inputValid == true) // if user entered data is valid
                    {
                        newTruck = new App_Code.TruckClass();
                        // assign values to properites
                        newTruck.Make = make;
                        newTruck.Model = model;
                        newTruck.Year = year;
                        newTruck.Color = color;
                        newTruck.Engine = engine;
                        newTruck.Transmission = transmission;
                        newTruck.DriveType = driveType;
                        newTruck.Doors = doors;
                        newTruck.Price = price;
                        newTruck.CabType = cabType;
                        newTruck.BedLength = bedLength;
                        newTruck.TowPackage = towPackage;
                        // add truck/automobile properties to vehicles list
                        vehicles.Add("Make: " + newTruck.Make + " Model: " + newTruck.Model + " Year: " +
                            newTruck.Year.ToString() + " Color: " + newTruck.Color + " Engine: " + newTruck.Engine.ToString() +
                            " Transmission: " + newTruck.Transmission + "Drive Type: " + newTruck.DriveType + " Doors: " +
                            +newTruck.Doors + " Price: " + newTruck.Price.ToString() + " CabType: " + newTruck.CabType + " BedLength: " +
                            newTruck.BedLength.ToString() + " TowPackage: " + newTruck.TowPackage);

                        MakeReadyForNewVehicle(); // makes form ready for new entry
                    }
                }
            }

            lstVehicleOrder.Items.Clear(); // clear listbox so values aren't repeated
            foreach (string vehicle in vehicles) // for each vehicle in vehicles list add to vehicleorder listbox
            {
                lstVehicleOrder.Items.Add(vehicle);
            }


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // save list to file
            try
            {
                StreamWriter outputFile; //creates reference to file
                outputFile = File.AppendText("vehicleorderlist.text"); //will create new or overwrite file

                if (vehicles.Count > 0)
                {
                    foreach (string vehicle in vehicles)
                    {
                        outputFile.WriteLine(vehicle);
                    }
                }

                outputFile.Close(); // closes file
                MessageBox.Show("Vehicle order list submitted!");
            }
            catch (Exception myError)
            {
                MessageBox.Show(myError.Message);
            }
        }

        private void cbxVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // display appropriate vehicle attribute group box based on vehicle type select
            if (cbxVehicleType.SelectedIndex == 0) // if car selected
            {
                gbxCar.Visible = true; // make car groupbox visbile
                gbxSUV.Visible = false; // make suv groupbox invisbile
                gbxTruck.Visible = false; // make truck groupbox invisbile
            }
            else if (cbxVehicleType.SelectedIndex == 1)
            {
                gbxSUV.Visible = true; // make suv groupbox visbile
                gbxCar.Visible = false; // make car groupbox invisbile
                gbxTruck.Visible = false; // make truck groupbox invisbile
            }
            else if (cbxVehicleType.SelectedIndex == 2)
            {
                gbxTruck.Visible = true; // make suv groupbox visbile
                gbxCar.Visible = false; // make car groupbox invisbile
                gbxSUV.Visible = false; // make suv groupbox invisbile
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstVehicleOrder.SelectedIndex > -1) // check if vehicle in list has been selected
            {
                vehicles.Remove(lstVehicleOrder.SelectedItem.ToString()); // remove from vehicles list
                lstVehicleOrder.Items.Remove(lstVehicleOrder.SelectedItem); // remove from vehicle order listbox
            }
            else // no vehicle selected
                MessageBox.Show("Must select vehicle you wish to remove before clicking remove!");
        }
    }
}
