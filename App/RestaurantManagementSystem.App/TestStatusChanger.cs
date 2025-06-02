using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class TableStatusTestForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

        public TableStatusTestForm()
        {
            InitializeComponent();
            LoadTableOptions();
            LoadStatusOptions();
        }

        private void LoadTableOptions()
        {
            for (int i = 1; i <= 10; i++)
            {
                tableComboBox.Items.Add($"T{i}");
            }
            tableComboBox.SelectedIndex = 0;
        }

        private void LoadStatusOptions()
        {
            statusComboBox.Items.AddRange(new string[]
            {
                "Empty",
                "Reserved",
                "Expecting Waiter",
                "Waiting for Meal",
                "Dining",
                "Expecting Bill",
                "Needs Cleaning"
            });
            statusComboBox.SelectedIndex = 0;
        }

        private void updateStatusButton_Click(object sender, EventArgs e)
        {
            string tableName = tableComboBox.SelectedItem.ToString();
            string statusName = statusComboBox.SelectedItem.ToString();

            int tableId = GetTableId(tableName);
            int statusTypeId = GetStatusTypeId(statusName);

            if (tableId > 0 && statusTypeId > 0)
            {
                UpdateStatus(tableId, statusTypeId);
                resultLabel.Text = $"✅ Updated {tableName} to '{statusName}'";
            }
            else
            {
                resultLabel.Text = "❌ Failed to update table.";
            }
        }

        private int GetTableId(string tableName)
        {
            int result = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TableID FROM Tables WHERE TableNumber = @num", conn))
                {
                    cmd.Parameters.AddWithValue("@num", tableName);
                    var obj = cmd.ExecuteScalar();
                    if (obj != null && int.TryParse(obj.ToString(), out int id))
                    {
                        result = id;
                    }
                }
            }
            return result;
        }

        private int GetStatusTypeId(string statusName)
        {
            int result = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT StatusTypeID FROM TableStatusTypes WHERE StatusName = @name", conn))
                {
                    cmd.Parameters.AddWithValue("@name", statusName);
                    var obj = cmd.ExecuteScalar();
                    if (obj != null && int.TryParse(obj.ToString(), out int id))
                    {
                        result = id;
                    }
                }
            }
            return result;
        }

        private void UpdateStatus(int tableId, int statusTypeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();

                try
                {
                    using (SqlCommand updateCmd = new SqlCommand(@"
                        UPDATE TableStatus
                        SET StatusTypeID = @statusTypeId, EmployeeID = CASE WHEN @statusTypeId = 1 THEN NULL ELSE EmployeeID END
                        WHERE TableID = @tableId", conn, tx))
                    {
                        updateCmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);
                        updateCmd.Parameters.AddWithValue("@tableId", tableId);
                        updateCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand logCmd = new SqlCommand(@"
                        INSERT INTO TableStatusLog (TableID, StatusTypeID)
                        VALUES (@tableId, @statusTypeId)", conn, tx))
                    {
                        logCmd.Parameters.AddWithValue("@tableId", tableId);
                        logCmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);
                        logCmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    MessageBox.Show("⚠️ Error updating status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TableStatusTestForm_Load(object sender, EventArgs e)
        {
        }

        private void resultLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
