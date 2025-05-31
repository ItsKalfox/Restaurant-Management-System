using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class TableViewForm : Form
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";
        private readonly int currentWaiterId;

        private const int STATUS_EMPTY = 1;
        private const int STATUS_RESERVED = 2;
        private const int STATUS_EXPECTING_WAITER = 3;
        private const int STATUS_WAITING_FOR_MEAL = 4;
        private const int STATUS_DINING = 5;
        private const int STATUS_EXPECTING_BILL = 6;
        private const int STATUS_NEEDS_CLEANING = 7;

        private string[] attendingTables = new string[3];

        private Timer refreshTimer;

        public TableViewForm(int waiterId)
        {
            InitializeComponent();
            currentWaiterId = waiterId;

            t01Btn.Click += (s, e) => HandleTableButtonClick("T1");
            t02Btn.Click += (s, e) => HandleTableButtonClick("T2");
            t03Btn.Click += (s, e) => HandleTableButtonClick("T3");
            t04Btn.Click += (s, e) => HandleTableButtonClick("T4");
            t05Btn.Click += (s, e) => HandleTableButtonClick("T5");
            t06Btn.Click += (s, e) => HandleTableButtonClick("T6");
            t07Btn.Click += (s, e) => HandleTableButtonClick("T7");
            t08Btn.Click += (s, e) => HandleTableButtonClick("T8");
            t09Btn.Click += (s, e) => HandleTableButtonClick("T9");
            t10Btn.Click += (s, e) => HandleTableButtonClick("T10");

            this.Load += TableViewForm_Load;

            refreshTimer = new Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshAllTableStatuses();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            refreshTimer.Dispose();
            base.OnFormClosing(e);
        }

        private void TableViewForm_Load(object sender, EventArgs e)
        {
            RefreshAllTableStatuses();
        }

        private void RefreshAllTableStatuses()
        {
            ClearAttendingTables();

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new SqlCommand(@"
                    SELECT t.TableNumber, ts.StatusTypeID, tst.StatusName, ts.EmployeeID
                    FROM TableStatus ts
                    JOIN Tables t ON t.TableID = ts.TableID
                    JOIN TableStatusTypes tst ON tst.StatusTypeID = ts.StatusTypeID", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableNum = reader.GetString(0);
                        int statusId = reader.GetInt32(1);
                        string statusName = reader.GetString(2);
                        object empObj = reader[3];
                        int? employeeId = empObj != DBNull.Value ? (int?)empObj : null;

                        UpdateTableUI(tableNum, statusId, statusName, employeeId);
                    }
                }
            }

            UpdateAttendingLabels();
        }

        private void ClearAttendingTables()
        {
            attendingTables[0] = "";
            attendingTables[1] = "";
            attendingTables[2] = "";
        }

        private void UpdateTableUI(string tableNum, int statusId, string statusName, int? employeeId)
        {
            Label statusLabel = null;
            Button statusButton = null;

            if (tableNum == "T1") { statusLabel = t01statusLabel; statusButton = t01Btn; }
            else if (tableNum == "T2") { statusLabel = t02statusLabel; statusButton = t02Btn; }
            else if (tableNum == "T3") { statusLabel = t03statusLabel; statusButton = t03Btn; }
            else if (tableNum == "T4") { statusLabel = t04statusLabel; statusButton = t04Btn; }
            else if (tableNum == "T5") { statusLabel = t05statusLabel; statusButton = t05Btn; }
            else if (tableNum == "T6") { statusLabel = t06statusLabel; statusButton = t06Btn; }
            else if (tableNum == "T7") { statusLabel = t07statusLabel; statusButton = t07Btn; }
            else if (tableNum == "T8") { statusLabel = t08statusLabel; statusButton = t08Btn; }
            else if (tableNum == "T9") { statusLabel = t09statusLabel; statusButton = t09Btn; }
            else if (tableNum == "T10") { statusLabel = t10statusLabel; statusButton = t10Btn; }
            else
            {
                return;
            }

            statusLabel.Text = statusName;
            statusLabel.ForeColor = (statusId == STATUS_EXPECTING_WAITER) ? Color.Red : SystemColors.Control;

            statusButton.Enabled = false;
            statusButton.Text = "";
            statusButton.BackColor = SystemColors.ControlDark;

            if (statusId == STATUS_EXPECTING_WAITER)
            {
                statusButton.Enabled = true;
                statusButton.Text = "Confirm";
                statusButton.BackColor = Color.Red;
            }
            else if (statusId == STATUS_WAITING_FOR_MEAL && employeeId == currentWaiterId)
            {
                statusButton.Enabled = false;
                statusButton.Text = "Assigned";
                statusButton.BackColor = Color.Lime;

                AddAttendingTable(tableNum);
            }
            else if (statusId == STATUS_DINING && employeeId == currentWaiterId)
            {
                statusButton.Enabled = false;
                statusButton.Text = "Assigned";
                statusButton.BackColor = Color.Lime;

                AddAttendingTable(tableNum);
            }
            else if (statusId == STATUS_EXPECTING_BILL && employeeId == currentWaiterId)
            {
                statusButton.Enabled = false;
                statusButton.Text = "Assigned";
                statusButton.BackColor = Color.Lime;

                AddAttendingTable(tableNum);
            }
            else if (statusId == STATUS_NEEDS_CLEANING && employeeId == currentWaiterId)
            {
                statusButton.Enabled = true;
                statusButton.Text = "Done";
                statusButton.BackColor = Color.LightCoral;
            }
            else if (statusId == STATUS_EMPTY)
            {
                statusButton.Enabled = false;
                statusButton.Text = "";
                statusButton.BackColor = SystemColors.ControlDark;
            }
            else
            {
                statusButton.Enabled = false;
                statusButton.Text = "";
                statusButton.BackColor = SystemColors.ControlDark;
            }
        }

        private void AddAttendingTable(string tableNum)
        {
            for (int i = 0; i < attendingTables.Length; i++)
            {
                if (string.IsNullOrEmpty(attendingTables[i]))
                {
                    attendingTables[i] = tableNum;
                    break;
                }
            }
        }

        private void UpdateAttendingLabels()
        {
            attending1Label.Text = attendingTables[0];
            attending1Label.BackColor = string.IsNullOrEmpty(attendingTables[0]) ? SystemColors.ControlDark : Color.LightGreen;

            attending2Label.Text = attendingTables[1];
            attending2Label.BackColor = string.IsNullOrEmpty(attendingTables[1]) ? SystemColors.ControlDark : Color.LightGreen;

            attending3Label.Text = attendingTables[2];
            attending3Label.BackColor = string.IsNullOrEmpty(attendingTables[2]) ? SystemColors.ControlDark : Color.LightGreen;
        }

        private void HandleTableButtonClick(string tableNum)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        int tableId = -1;
                        int statusId = -1;
                        int? employeeId = null;

                        var getStatusCmd = new SqlCommand(@"
                            SELECT ts.TableID, ts.StatusTypeID, ts.EmployeeID
                            FROM TableStatus ts
                            JOIN Tables t ON t.TableID = ts.TableID
                            WHERE t.TableNumber = @tableNum", conn, tx);
                        getStatusCmd.Parameters.AddWithValue("@tableNum", tableNum);

                        using (var reader = getStatusCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tableId = reader.GetInt32(0);
                                statusId = reader.GetInt32(1);
                                employeeId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                            }
                            else
                            {
                                MessageBox.Show("Table not found in database.");
                                return;
                            }
                        }

                        if (statusId == STATUS_EXPECTING_WAITER)
                        {
                            var countCmd = new SqlCommand(@"
                                SELECT COUNT(*) 
                                FROM TableStatus 
                                WHERE EmployeeID = @eid AND StatusTypeID = @statusId", conn, tx);
                            countCmd.Parameters.AddWithValue("@eid", currentWaiterId);
                            countCmd.Parameters.AddWithValue("@statusId", STATUS_WAITING_FOR_MEAL);

                            int count = (int)countCmd.ExecuteScalar();
                            if (count >= 3)
                            {
                                MessageBox.Show("You are already attending 3 tables.");
                                tx.Rollback();
                                return;
                            }

                            var updateCmd = new SqlCommand(@"
                                UPDATE TableStatus 
                                SET StatusTypeID = @newStatus, EmployeeID = @eid 
                                WHERE TableID = @tid", conn, tx);
                            updateCmd.Parameters.AddWithValue("@newStatus", STATUS_WAITING_FOR_MEAL);
                            updateCmd.Parameters.AddWithValue("@eid", currentWaiterId);
                            updateCmd.Parameters.AddWithValue("@tid", tableId);

                            updateCmd.ExecuteNonQuery();
                        }
                        else if (statusId == STATUS_NEEDS_CLEANING && employeeId == currentWaiterId)
                        {
                            var updateCmd = new SqlCommand(@"
                                UPDATE TableStatus 
                                SET StatusTypeID = @emptyStatus, EmployeeID = NULL 
                                WHERE TableID = @tid", conn, tx);
                            updateCmd.Parameters.AddWithValue("@emptyStatus", STATUS_EMPTY);
                            updateCmd.Parameters.AddWithValue("@tid", tableId);

                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            tx.Rollback();
                            return;
                        }

                        tx.Commit();

                        RefreshAllTableStatuses();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating table status: " + ex.Message);
                        try { tx.Rollback(); } catch { }
                    }
                }
            }
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
