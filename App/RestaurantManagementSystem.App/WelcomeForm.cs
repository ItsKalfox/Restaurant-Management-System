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
        private Form activeDynamicForm = null;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        private readonly int employeeId;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,
           int nTopRect,
           int nRightRect,
           int nBottomRect,
           int nWidthEllipse,
           int nHeightEllipse
       );

        public WelcomeForm()
        {
            InitializeComponent();
            SetupUI();
        }

        public WelcomeForm(string fullName, string roleName, int roleId, int employeeId) : this()
        {
            usernameLable.Text = $"{fullName}";
            roleLable.Text = $"{roleName}";
            LoadButtonsForRole(roleId);
            this.employeeId = employeeId;
        }

        private void SetupUI()
        {
            navBtn1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn1.Width, navBtn1.Height, 20, 20));
            navBtn2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn2.Width, navBtn2.Height, 20, 20));
            navBtn3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn3.Width, navBtn3.Height, 20, 20));
            navBtn4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, navBtn4.Width, navBtn4.Height, 20, 20));
            lgBtn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, lgBtn.Width, lgBtn.Height, 20, 20));

            Panel line = new Panel();
            line.Height = 2;
            line.Top = 94;
            line.Left = 225;
            line.Width = this.ClientSize.Width - 250 - 10;
            line.BackColor = Color.FromArgb(0, 192, 0);
            line.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(line);

        }

        private void LoadButtonsForRole(int roleId)
        {
            List<Button> navButtons = new List<Button>
            {
                navBtn1, navBtn2, navBtn3, navBtn4, navBtn5, navBtn6, navBtn7
            };

            foreach (var btn in navButtons)
            {
                btn.Visible = false;
                btn.Text = "";
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

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

                            string formNameToShow = linkedForm;
                            btn.Click += (s, e) =>
                            {
                                try
                                {
                                    Type formType = Type.GetType("RestaurantManagementSystem.App." + formNameToShow);
                                    if (formType != null && typeof(Form).IsAssignableFrom(formType))
                                    {
                                        if (activeDynamicForm != null && !activeDynamicForm.IsDisposed)
                                        {
                                            activeDynamicForm.Close();
                                        }

                                        Form formInstance;
                                        if (formNameToShow == "TableViewForm")
                                        {
                                            formInstance = new TableViewForm(employeeId);
                                        }
                                        else
                                        {
                                            formInstance = (Form)Activator.CreateInstance(formType);
                                        }

                                        activeDynamicForm = formInstance;

                                        formInstance.StartPosition = FormStartPosition.Manual;
                                        formInstance.Location = new Point(this.Location.X + 200, this.Location.Y + 100);

                                        formInstance.Owner = this;

                                        formInstance.Show();

                                        this.LocationChanged += (sender2, e2) =>
                                        {
                                            if (activeDynamicForm != null && !activeDynamicForm.IsDisposed)
                                            {
                                                activeDynamicForm.Location = new Point(this.Location.X + 200, this.Location.Y + 100);
                                            }
                                        };
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Form '{formNameToShow}' not found or not a Form type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error loading form '{formNameToShow}': {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            };



                        }
                    }
                }
            }

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

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }
    }
}
