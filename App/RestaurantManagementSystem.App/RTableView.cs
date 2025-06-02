using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class RTableView : Form
    {
        private Label[] statusLabels;
        private Button[] notifyButtons;
        private Button[] reserveButtons;
        private Timer refreshTimer;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

        public RTableView()
        {
            InitializeComponent();

            statusLabels = new Label[] { t01statusLabel, t02statusLabel, t03statusLabel, t04statusLabel, t05statusLabel, t06statusLabel, t07statusLabel, t08statusLabel, t09statusLabel, t10statusLabel };
            notifyButtons = new Button[] { t01Btn, t02Btn, t03Btn, t04Btn, t05Btn, t06Btn, t07Btn, t08Btn, t09Btn, t10Btn };
            reserveButtons = new Button[] { t01rBtn, t02rBtn, t03rBtn, t04rBtn, t05rBtn, t06rBtn, t07rBtn, t08rBtn, t09rBtn, t10rBtn };

            for (int i = 0; i < notifyButtons.Length; i++)
                notifyButtons[i].Click += NotifyButton_Click;

            for (int i = 0; i < reserveButtons.Length; i++)
                reserveButtons[i].Click += ReserveButton_Click;

            refreshTimer = new Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshTableStatuses();
        }

        private void RefreshTableStatuses()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT ts.TableID, tst.StatusName, ts.StatusTypeID
                    FROM TableStatus ts
                    INNER JOIN TableStatusTypes tst ON ts.StatusTypeID = tst.StatusTypeID
                    ORDER BY ts.TableID ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                for (int i = 0; i < 10; i++)
                {
                    if (i < dt.Rows.Count)
                    {
                        int tableId = Convert.ToInt32(dt.Rows[i]["TableID"]);
                        string statusName = dt.Rows[i]["StatusName"].ToString();
                        int statusTypeId = Convert.ToInt32(dt.Rows[i]["StatusTypeID"]);

                        statusLabels[i].Text = statusName;

                        if (statusTypeId == 2)
                        {
                            statusLabels[i].ForeColor = Color.Red;
                        }
                        else
                        {
                            statusLabels[i].ForeColor = Color.White;
                        }

                        if (statusTypeId == 1 || statusTypeId == 2)
                        {
                            notifyButtons[i].Enabled = true;
                            notifyButtons[i].BackColor = Color.Lime;
                            notifyButtons[i].ForeColor = Color.Black;
                            notifyButtons[i].Text = "Notify the waiter.";
                        }
                        else
                        {
                            notifyButtons[i].Enabled = false;
                            notifyButtons[i].BackColor = Color.Gray;
                            notifyButtons[i].Text = "Occupied";
                        }

                        reserveButtons[i].Visible = statusTypeId == 1;
                    }
                }
            }
        }

        private void NotifyButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = Array.IndexOf(notifyButtons, clickedButton);
            if (index >= 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE TableStatus SET StatusTypeID = 3 WHERE TableID = @tableId AND (StatusTypeID = 1 OR StatusTypeID = 2)";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@tableId", index + 1);
                    cmd.ExecuteNonQuery();
                }
                RefreshTableStatuses();
            }
        }

        private void ReserveButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = Array.IndexOf(reserveButtons, clickedButton);
            if (index >= 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE TableStatus SET StatusTypeID = 2 WHERE TableID = @tableId AND StatusTypeID = 1";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@tableId", index + 1);
                    cmd.ExecuteNonQuery();
                }
                RefreshTableStatuses();
            }
        }
        private void RTableView_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var testForm = new TableStatusTestForm();
                testForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open TestStatusChanger: " + ex.Message);
            }
        }
    }
}
