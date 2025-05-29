using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class WelcomeForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );

        public WelcomeForm()
        {
            InitializeComponent();
            SetupUI();
        }

        public WelcomeForm(string fullName, string roleName, int roleId) : this()  // calls parameterless constructor first
        {
            usernameLable.Text = $"{fullName}";
            roleLable.Text = $"{roleName}";
            LoadButtonsForRole(roleId);
        }

        private void LoadButtonsForRole(int roleId)
        {
            // List of your nav buttons
            List<Button> navButtons = new List<Button>
            {
                navBtn1, navBtn2, navBtn3, navBtn4, navBtn5, navBtn6, navBtn7
            };

            // Hide all buttons initially
            foreach (var btn in navButtons)
            {
                btn.Visible = false;
                btn.Text = "";
                //btn.Click -= NavButton_Click; // Remove old event handlers
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\netys\OneDrive - NSBM\Y1 S3\C# project\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ButtonName, LinkedFormName FROM RoleButtons WHERE RoleID = @roleId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@roleId", roleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read() && index < navButtons.Count)
                        {
                            string buttonName = reader["ButtonName"].ToString();
                            string linkedForm = reader["LinkedFormName"].ToString();

                            Button btn = navButtons[index++];
                            btn.Text = buttonName;
                            btn.Visible = true;

                            // Create a local copy of the variable for closure
                            string formNameToShow = linkedForm;
                            btn.Click += (s, e) =>
                            {
                                MessageBox.Show($"Would open form: {formNameToShow}", "Linked Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                        }
                    }
                }
            }
        }


        private void SetupUI()
        {
            navBtn1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn1.Width, navBtn1.Height, 20, 20));
            navBtn2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn1.Width, navBtn2.Height, 20, 20));
            navBtn3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn1.Width, navBtn3.Height, 20, 20));
            navBtn4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn1.Width, navBtn4.Height, 20, 20));

            Panel line = new Panel();
            line.Height = 2;
            line.Top = 100;
            line.Left = 250;
            line.Width = this.ClientSize.Width - 250 - 20;
            line.BackColor = Color.FromArgb(0, 192, 0);
            line.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(line);

        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {

        }

        private void minmizebtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void AdjustPanelWidths()
        {
            int third = this.ClientSize.Width / 5;
            panel1.Width = third;
            panel2.Width = third;
            panel3.Width = third;
            panel4.Width = third;
            // panel3 will fill the rest
        }

        private void fullsrcbtn_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            maximizeBtn.Visible = false;
            restoreBtn.Visible = true;
        }

        private void restoreBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            restoreBtn.Visible = false;
            maximizeBtn.Visible = true;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
