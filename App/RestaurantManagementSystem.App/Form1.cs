using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace RestaurantManagementSystem.App
{
    public partial class Form1: Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void usernameTxtbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void showpassCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            bool show = showpassCheckbox.Checked;
            passwordTxtbox.UseSystemPasswordChar = !show;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                usernameerrorLabel.Visible = false;
                passworderrorLabel.Visible = false;

                bool hasError = false;

                if (string.IsNullOrWhiteSpace(usernameTxtbox.Text))
                {
                    usernameerrorLabel.Text = "* Username is required!";
                    usernameerrorLabel.Visible = true;
                    hasError = true;
                }

                if (string.IsNullOrWhiteSpace(passwordTxtbox.Text))
                {
                    passworderrorLabel.Text = "* Password is required!";
                    passworderrorLabel.Visible = true;
                    hasError = true;
                }

                if (hasError)
                    return;

                string inputUsername = usernameTxtbox.Text;
                string inputPassword = passwordTxtbox.Text;

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\netys\OneDrive - NSBM\Y1 S3\C# project\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // First fetch everything in one go
                    string query = @"SELECT 
                                        l.PasswordHash, 
                                        e.FirstName, 
                                        e.LastName, 
                                        r.RoleName, 
                                        r.RoleID
                                    FROM logins l
                                    JOIN employees e ON l.EmployeeID = e.EmployeeID
                                    JOIN roles r ON l.RoleID = r.RoleID
                                    WHERE l.Username = @username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", inputUsername);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPasswordHash = reader["PasswordHash"].ToString();

                                if (storedPasswordHash == inputPassword)
                                {
                                    string fullName = $"{reader["FirstName"]} {reader["LastName"]}";
                                    string roleName = reader["RoleName"].ToString();
                                    int roleId = Convert.ToInt32(reader["RoleID"]);

                                    WelcomeForm welcomeForm = new WelcomeForm(fullName, roleName, roleId);
                                    welcomeForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    passworderrorLabel.Text = "* Password is incorrect!";
                                    passworderrorLabel.Visible = true;
                                }
                            }
                            else
                            {
                                usernameerrorLabel.Text = "* Username not found!";
                                usernameerrorLabel.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}