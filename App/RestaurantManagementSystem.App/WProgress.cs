using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class WProgress : Form
    {
        private int employeeId;
        private Label[] nameLabels;
        private Label[] quantityLabels;
        private Label[] statusLabels;
        private Button[] serveButtons;
        private Timer refreshTimer;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

        public WProgress(int empId)
        {
            InitializeComponent();
            employeeId = empId;

            nameLabels = new Label[] { ordername1Label, ordername2Label, ordername3Label, ordername4Label, ordername5Label, ordername6Label, ordername7Label, ordername8Label };
            quantityLabels = new Label[] { orderquantity1Label, orderquantity2Label, orderquantity3Label, orderquantity4Label, orderquantity5Label, orderquantity6Label, orderquantity7Label, orderquantity8Label };
            statusLabels = new Label[] { orderstatus1Label, orderstatus2Label, orderstatus3Label, orderstatus4Label, orderstatus5Label, orderstatus6Label, orderstatus7Label, orderstatus8Label };
            serveButtons = new Button[] { served1Btn, served2Btn, served3Btn, served4Btn, served5Btn, served6Btn, served7Btn, served8Btn };

            foreach (var label in nameLabels.Concat(quantityLabels).Concat(statusLabels))
                label.Visible = false;
            foreach (var btn in serveButtons)
                btn.Visible = false;

            LoadAssignedTables();
            tablesComboBox.SelectedIndexChanged += TablesComboBox_SelectedIndexChanged;

            foreach (var btn in serveButtons)
                btn.Click += ServeButton_Click;

            refreshTimer = new Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            Panel line = new Panel();
            line.Height = 1;
            line.Top = 150;
            line.Left = 65;
            line.Width = this.ClientSize.Width - 120 - 10;
            line.BackColor = Color.Gray;
            line.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(line);
        }

        private void LoadAssignedTables()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT t.TableID, t.TableNumber
                    FROM TableStatus ts
                    INNER JOIN Tables t ON ts.TableID = t.TableID
                    WHERE ts.EmployeeID = @empId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@empId", employeeId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tableData = new DataTable();
                adapter.Fill(tableData);

                tablesComboBox.DisplayMember = "TableNumber";
                tablesComboBox.ValueMember = "TableID";
                tablesComboBox.DataSource = tableData;
            }
        }

        private void TablesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tablesComboBox.SelectedValue is int selectedTableId)
            {
                tablenameLabel.Text = $"Table: {tablesComboBox.Text}";
                LoadOrderData(selectedTableId);
            }
        }

        private void LoadOrderData(int tableId)
        {
            foreach (var btn in serveButtons)
            {
                btn.Visible = false;
                btn.Enabled = false;
                btn.Tag = null;
                btn.BackColor = Color.Gray;
            }

            foreach (var label in nameLabels.Concat(quantityLabels).Concat(statusLabels))
                label.Visible = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT TOP 8
                        oi.OrderItemID,
                        mi.Name,
                        oi.Quantity,
                        oi.OrderStatus
                    FROM Orders o
                    INNER JOIN OrderItems oi ON o.OrderID = oi.OrderID
                    INNER JOIN MenuItems mi ON oi.MenuItemID = mi.MenuItemID
                    WHERE o.TableID = @tableId AND oi.OrderStatus IN ('Pending', 'Ready to Pickup')
                    ORDER BY oi.OrderItemID ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tableId", tableId);
                SqlDataReader reader = cmd.ExecuteReader();

                int index = 0;
                while (reader.Read() && index < 8)
                {
                    int orderItemId = reader.GetInt32(0);
                    string itemName = reader.GetString(1);
                    int quantity = reader.GetInt32(2);
                    string status = reader.GetString(3);

                    nameLabels[index].Text = itemName;
                    quantityLabels[index].Text = $"{quantity}";
                    statusLabels[index].Text = status;

                    if (status == "Pending")
                    {
                        statusLabels[index].ForeColor = Color.DarkOrange;
                    }
                    else if (status == "Ready to Pickup")
                    {
                        statusLabels[index].ForeColor = Color.Firebrick;
                    }
                    else
                    {
                        statusLabels[index].ForeColor = SystemColors.ControlText;
                    }

                    nameLabels[index].Visible = true;
                    quantityLabels[index].Visible = true;
                    statusLabels[index].Visible = true;

                    if (index < serveButtons.Length)
                    {
                        serveButtons[index].Visible = true;

                        if (status == "Ready to Pickup")
                        {
                            serveButtons[index].Enabled = true;
                            serveButtons[index].BackColor = Color.Lime;
                            serveButtons[index].Tag = orderItemId;
                        }
                        else
                        {
                            serveButtons[index].Enabled = false;
                            serveButtons[index].BackColor = Color.Gray;
                            serveButtons[index].Tag = null;
                        }
                    }

                    index++;
                }
            }
        }

        private void ServeButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int orderItemId)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var update = "UPDATE OrderItems SET OrderStatus = 'Finished' WHERE OrderItemID = @id";
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.Parameters.AddWithValue("@id", orderItemId);
                    cmd.ExecuteNonQuery();
                }

                if (tablesComboBox.SelectedValue is int selectedTableId)
                    LoadOrderData(selectedTableId);
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (tablesComboBox.SelectedValue is int selectedTableId)
            {
                LoadOrderData(selectedTableId);
            }
        }

        private void WProgress_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
