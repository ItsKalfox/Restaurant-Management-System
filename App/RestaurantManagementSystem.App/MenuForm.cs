using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class MenuForm : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\OneDrive - NSBM\DevProjects\Restaurant-Management-System\App\RestaurantManagementSystem.App\Database1.mdf"";Integrated Security=True";
        public MenuForm()
        {
            InitializeComponent();
            this.Load += MenuForm_Load;
            AddLineToPanel(cartPanel);
        }

        private void AddLineToPanel(Panel container)
        {
            Panel line = new Panel();
            line.Height = 1;
            line.Width = container.ClientSize.Width - 20;
            line.Top = 570;
            line.Left = 10;
            line.BackColor = Color.Gray;
            line.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Panel line1 = new Panel();
            line1.Height = 1;
            line1.Width = container.ClientSize.Width - 20;
            line1.Top = 73;
            line1.Left = 10;
            line1.BackColor = Color.Gray;
            line1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            container.Controls.Add(line);
            container.Controls.Add(line1);
        }


        private void MenuForm_Load(object sender, EventArgs e)
        {
            LoadMenuItems();
            LoadTables();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

        private void LoadMenuItems(string categoryFilter = "", string searchQuery = "")
        {
            flowLayoutPanel1.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM MenuItems WHERE 1=1";

                if (!string.IsNullOrEmpty(categoryFilter))
                    query += " AND Category = @Category";

                if (!string.IsNullOrEmpty(searchQuery))
                    query += " AND Name LIKE @Search";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(categoryFilter))
                    cmd.Parameters.AddWithValue("@Category", categoryFilter);
                if (!string.IsNullOrEmpty(searchQuery))
                    cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FoodCard card = new FoodCard();
                    card.ItemName = reader["Name"].ToString();
                    card.Price = Convert.ToDecimal(reader["Price"]);
                    card.Rating = Convert.ToDecimal(reader["Rating"]);
                    card.ImageKey = reader["ImagePath"].ToString();
                    card.MenuItemID = Convert.ToInt32(reader["MenuItemID"]);
                    card.AddToCartClicked += Card_AddToCartClicked;

                    flowLayoutPanel1.Controls.Add(card);
                }
            }
        }

        private class CartItem
        {
            public int MenuItemID;
            public string Name;
            public decimal Price;
        }

        private class TableItem
        {
            public int TableID { get; set; }
            public string TableNumber { get; set; }
            public override string ToString() => TableNumber;
        }

        private List<CartItem> cartItems = new List<CartItem>();

        private void Card_AddToCartClicked(object sender, EventArgs e)
        {
            var card = sender as FoodCard;

            if (cartItems.Count >= 8)
            {
                MessageBox.Show("Cart can contain a maximum of 8 items.");
                return;
            }

            if (cartItems.Any(ci => ci.MenuItemID == card.MenuItemID))
            {
                MessageBox.Show("Item already in cart.");
                return;
            }

            cartItems.Add(new CartItem
            {
                MenuItemID = card.MenuItemID,
                Name = card.ItemName,
                Price = card.Price
            });

            UpdateCartUI();
        }

        private void UpdateCartUI()
        {
            Label[] itemLabels = {
                cartitem01Label, cartitem02Label, cartitem03Label, cartitem04Label,
                cartitem05Label, cartitem06Label, cartitem07Label, cartitem08Label
            };
            Label[] itempriceLabels = {
                cartitemprice01Label, cartitemprice02Label, cartitemprice03Label, cartitemprice04Label,
                cartitemprice05Label, cartitemprice06Label, cartitemprice07Label, cartitemprice08Label
            };
            for (int i = 0; i < itemLabels.Length; i++)
            {
                if (i < cartItems.Count)
                {
                    itemLabels[i].Visible = true;
                    itempriceLabels[i].Visible = true;
                    itempriceLabels[i].AutoSize = false;
                    itempriceLabels[i].TextAlign = ContentAlignment.MiddleRight;
                    itempriceLabels[i].RightToLeft = RightToLeft.No;
                    itempriceLabels[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    itemLabels[i].Text = $"{cartItems[i].Name}";
                    itempriceLabels[i].Text = $"{cartItems[i].Price:F2}";
                }
                else
                {
                    itemLabels[i].Visible = false;
                    itempriceLabels[i].Visible = false;
                    itemLabels[i].Text = "";
                }
            }
            totalpriceLabel.Text = $"LKR {cartItems.Sum(item => item.Price):F2}";
        }

        private void LoadTables()
        {
            tableCombobox.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tables", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tableCombobox.Items.Add(new TableItem
                    {
                        TableID = Convert.ToInt32(reader["TableID"]),
                        TableNumber = reader["TableNumber"].ToString()
                    });
                }
            }
        }

        private void confirmBtn_Click_1(object sender, EventArgs e)
        {
            var selectedTable = tableCombobox.SelectedItem as TableItem;
            if (cartItems.Count == 0 || selectedTable == null)
            {
                MessageBox.Show("Select table and add items.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand insertOrder = new SqlCommand(
                        "INSERT INTO Orders (TableID) OUTPUT INSERTED.OrderID VALUES (@TableID)",
                        conn, transaction);
                    insertOrder.Parameters.AddWithValue("@TableID", selectedTable.TableID);
                    int orderID = (int)insertOrder.ExecuteScalar();

                    foreach (var item in cartItems)
                    {
                        SqlCommand insertItem = new SqlCommand(
                            "INSERT INTO OrderItems (OrderID, MenuItemID, Quantity) VALUES (@OrderID, @MenuItemID, 1)",
                            conn, transaction);
                        insertItem.Parameters.AddWithValue("@OrderID", orderID);
                        insertItem.Parameters.AddWithValue("@MenuItemID", item.MenuItemID);
                        insertItem.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Order confirmed!");
                    cartItems.Clear();
                    UpdateCartUI();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Failed to save order. Error: " + ex.Message);
                }
            }
        }

        private void clearBtn_Click_1(object sender, EventArgs e)
        {
            cartItems.Clear();
            UpdateCartUI();
        }

        private void searchTxtbox_TextChanged_1(object sender, EventArgs e)
        {
            LoadMenuItems(sortCombobox.Text, searchTxtbox.Text);
        }

        private void sortCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenuItems(sortCombobox.Text, searchTxtbox.Text);
        }

        private void totalLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cartitemprice01Label_Click(object sender, EventArgs e)
        {

        }
    }
}