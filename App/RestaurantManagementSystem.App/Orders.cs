using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class Orders : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";
        private System.Windows.Forms.Timer refreshTimer;

        public Orders()
        {
            InitializeComponent();

            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            Panel line = new Panel();
            line.Height = 1;
            line.Top = 65;
            line.Left = 50;
            line.Width = this.ClientSize.Width - 90 - 10;
            line.BackColor = Color.Gray;
            line.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(line);

            LoadPendingOrders();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadPendingOrders();
        }

        private void LoadPendingOrders()
        {
            flowLayoutPanel1.Controls.Clear();

            string query = @"
                SELECT oi.OrderItemID, mi.Name, oi.Quantity
                FROM OrderItems oi
                INNER JOIN MenuItems mi ON oi.MenuItemID = mi.MenuItemID
                WHERE oi.OrderStatus = 'Pending'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int orderItemId = reader.GetInt32(0);
                            string itemName = reader.GetString(1);
                            int quantity = reader.GetInt32(2);

                            OrderDetails detailControl = new OrderDetails();
                            detailControl.SetData(orderItemId, itemName, quantity);
                            detailControl.OrderFinished += DetailControl_OrderFinished;

                            flowLayoutPanel1.Controls.Add(detailControl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load order items: " + ex.Message);
            }
        }

        private void DetailControl_OrderFinished(object sender, EventArgs e)
        {
            LoadPendingOrders();
        }

        private void Order_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
