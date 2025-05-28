using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button2.Height, 20, 20));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button3.Height, 20, 20));
            button4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button4.Height, 20, 20));

            Panel line = new Panel();
            line.Height = 2;
            line.Width = 900;
            line.Top = 100;
            line.Left = 250;
            line.BackColor = Color.FromArgb(0, 192, 0);
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
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TableViewForm TableViewForm = new TableViewForm();
            TableViewForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MenuForm MenuForm = new MenuForm();
            MenuForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalaryForm SalaryForm = new SalaryForm();
            SalaryForm.ShowDialog();
        }
    }
}
