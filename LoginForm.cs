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
using System.Text.RegularExpressions;

namespace FinalProject
{ 
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            Match matchUser = Regex.Match(txtUserID.Text, "[^a-z0-9]", RegexOptions.IgnoreCase); // only allow user name to contain alpha-numeric characters
            Match matchPassword = Regex.Match(txtPassword.Text, "\\s", RegexOptions.IgnoreCase); // do not allow spaces
                if ((matchUser.Success == false) && (matchPassword.Success == false)) // if username only contains alpha-numeric characters and password does not contain spaces check login credentials against database
                {  
                    string cmdStr = "Select Count(*) From LoginsTable Where USERID = @USERID And PASSWORD = @Password";  // search query

                    SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C#\FinalProject\FinalProject\App_Data\UserLogins.mdf;Integrated Security=True"); // establish datasource pathway
                    SqlCommand command = new SqlCommand(cmdStr, connection); 
                    {
                        command.Parameters.AddWithValue("@USERID", txtUserID.Text); // userID parameter
                        command.Parameters.AddWithValue("@Password", txtPassword.Text); // password parameter

                        SqlDataAdapter sdAdapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sdAdapter.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1") // if username and password match
                        {
                            this.Hide(); // hide login form
                            frmMenu menu = new frmMenu(); // create new instance of frmMenu
                            menu.Closed += (s, args) => this.Close(); // closes hidden login form when menu form is closed
                            menu.ShowDialog(); // displays menu form
                        }
                        else // if username and password do not match
                        {
                            MessageBox.Show("Login failed:\nUserID and password do not match!", "American Motors", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else // user attempting to input characters that are not allowed
                {
                    MessageBox.Show("Login failed:\nInvalid entry for UserID and/or password!", "American Motors", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
