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
        private int employeeId;
        public MenuForm(int empId)
        {
            InitializeComponent();
            employeeId = empId;
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
            WireQuantityButtons();
            UpdateConfirmButtonState();
        }

        private void WireQuantityButtons()
        {
            for (int i = 1; i <= 8; i++)
            {
                var addBtn = this.Controls.Find($"cartitem{i:00}qaBtn", true).FirstOrDefault() as Button;
                var removeBtn = this.Controls.Find($"cartitem{i:00}qrBtn", true).FirstOrDefault() as Button;

                if (addBtn != null)
                    addBtn.Click += QuantityAdd_Click;

                if (removeBtn != null)
                    removeBtn.Click += QuantityRemove_Click;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

        private void LoadMenuItems(string categoryFilter = "", string searchQuery = "")
        {
            flowLayoutPanel1.Controls.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM MenuItems WHERE 1=1";

                if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All")
                    query += " AND Category = @Category";

                if (!string.IsNullOrEmpty(searchQuery))
                    query += " AND Name LIKE @Search";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All")
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
            public int Quantity;
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
            var existingItem = cartItems.FirstOrDefault(ci => ci.MenuItemID == card.MenuItemID);

            if (existingItem != null)
            {
                if (existingItem.Quantity >= 10)
                {
                    MessageBox.Show("Maximum quantity for this item is 10.");
                    return;
                }
                existingItem.Quantity++;
            }
            else
            {
                if (cartItems.Count >= 8)
                {
                    MessageBox.Show("Cart can contain a maximum of 8 different items.");
                    return;
                }

                cartItems.Add(new CartItem
                {
                    MenuItemID = card.MenuItemID,
                    Name = card.ItemName,
                    Price = card.Price,
                    Quantity = 1
                });
            }

            UpdateCartUI();
        }


        private void UpdateCartUI()
        {
            Label[] itemLabels = {
                cartitem01Label, cartitem02Label, cartitem03Label, cartitem04Label,
                cartitem05Label, cartitem06Label, cartitem07Label, cartitem08Label
            };
            Label[] itemPriceLabels = {
                cartitemprice01Label, cartitemprice02Label, cartitemprice03Label, cartitemprice04Label,
                cartitemprice05Label, cartitemprice06Label, cartitemprice07Label, cartitemprice08Label
            };
            Label[] quantityLabels = {
                cartitem01qLabel, cartitem02qLabel, cartitem03qLabel, cartitem04qLabel,
                cartitem05qLabel, cartitem06qLabel, cartitem07qLabel, cartitem08qLabel
            };
            Button[] removeButtons = {
                cartitem01qrBtn, cartitem02qrBtn, cartitem03qrBtn, cartitem04qrBtn,
                cartitem05qrBtn, cartitem06qrBtn, cartitem07qrBtn, cartitem08qrBtn
            };
            Button[] addButtons = {
                cartitem01qaBtn, cartitem02qaBtn, cartitem03qaBtn, cartitem04qaBtn,
                cartitem05qaBtn, cartitem06qaBtn, cartitem07qaBtn, cartitem08qaBtn
            };

            for (int i = 0; i < itemLabels.Length; i++)
            {
                if (i < cartItems.Count)
                {
                    var item = cartItems[i];
                    itemLabels[i].Visible = true;
                    itemPriceLabels[i].Visible = true;
                    quantityLabels[i].Visible = true;
                    removeButtons[i].Visible = true;
                    addButtons[i].Visible = true;

                    itemLabels[i].Text = item.Name;
                    itemPriceLabels[i].Text = $"LKR {item.Price * item.Quantity:F2}";
                    quantityLabels[i].Text = $"{item.Quantity}";
                }
                else
                {
                    itemLabels[i].Visible = false;
                    itemPriceLabels[i].Visible = false;
                    quantityLabels[i].Visible = false;
                    removeButtons[i].Visible = false;
                    addButtons[i].Visible = false;

                    itemLabels[i].Text = "";
                    itemPriceLabels[i].Text = "";
                    quantityLabels[i].Text = "";
                }
            }

            totalpriceLabel.Text = $"LKR {cartItems.Sum(item => item.Price * item.Quantity):F2}";

            UpdateConfirmButtonState();
        }


        private void LoadTables()
        {
            tableCombobox.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT T.TableID, T.TableNumber
            FROM TableStatus TS
            INNER JOIN Tables T ON TS.TableID = T.TableID
            WHERE TS.EmployeeID = @empId
              AND TS.StatusTypeID <> (
                  SELECT StatusTypeID FROM TableStatusTypes WHERE StatusName = 'Empty'
              )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@empId", employeeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
            }
        }

        private void UpdateConfirmButtonState()
        {
            bool hasTable = tableCombobox.SelectedItem != null;
            bool hasItems = cartItems.Count > 0;

            confirmBtn.Enabled = hasTable && hasItems;
            confirmBtn.BackColor = confirmBtn.Enabled ? Color.Lime : Color.DimGray;
        }



        private void confirmBtn_Click_1(object sender, EventArgs e)
        {
            var selectedTable = tableCombobox.SelectedItem as TableItem;
            if (!confirmBtn.Enabled) return;

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
                            "INSERT INTO OrderItems (OrderID, MenuItemID, Quantity) VALUES (@OrderID, @MenuItemID, @Quantity)",
                            conn, transaction);
                            insertItem.Parameters.AddWithValue("@OrderID", orderID);
                            insertItem.Parameters.AddWithValue("@MenuItemID", item.MenuItemID);
                            insertItem.Parameters.AddWithValue("@Quantity", item.Quantity);
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

        private void QuantityAdd_Click(object sender, EventArgs e)
        {
            int index = GetCartIndexFromButton(sender);
            if (index < 0 || index >= cartItems.Count) return;

            if (cartItems[index].Quantity < 10)
                cartItems[index].Quantity++;

            UpdateCartUI();
        }

        private void QuantityRemove_Click(object sender, EventArgs e)
        {
            int index = GetCartIndexFromButton(sender);
            if (index < 0 || index >= cartItems.Count) return;

            cartItems[index].Quantity--;
            if (cartItems[index].Quantity <= 0)
                cartItems.RemoveAt(index);

            UpdateCartUI();
        }

        private int GetCartIndexFromButton(object sender)
        {
            string btnName = ((Button)sender).Name;
            string numberStr = btnName.Substring(8, 2);
            if (int.TryParse(numberStr, out int index))
                return index - 1;
            return -1;
        }

        private void tableCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConfirmButtonState();
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

        private void cartitem01qrBtn_Click(object sender, EventArgs e)
        {

        }

        private void cartitem03qrBtn_Click(object sender, EventArgs e)
        {

        }
    }
}