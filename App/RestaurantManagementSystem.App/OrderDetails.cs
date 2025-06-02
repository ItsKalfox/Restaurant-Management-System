using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class OrderDetails : UserControl
    {
        private int orderItemId;
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";

        public event EventHandler OrderFinished;

        public OrderDetails()
        {
            InitializeComponent();

            doneBtn.Click += DoneBtn_Click_1;
        }

        public void SetData(int orderItemId, string itemName, int quantity)
        {
            this.orderItemId = orderItemId;
            orderitemLabel.Text = itemName;
            orderquantityLabel.Text = quantity.ToString();
        }

        private void DoneBtn_Click_1(object sender, EventArgs e)
        {
            // Update OrderStatus to "Finished" in DB
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string updateQuery = "UPDATE OrderItems SET OrderStatus = 'Finished' WHERE OrderItemID = @orderItemId";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderItemId", orderItemId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Raise event to notify Orders form to refresh
                OrderFinished?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update order status: " + ex.Message);
            }
        }
    }
}
