using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    public partial class FoodCard : UserControl
    {
        public int MenuItemID { get; set; }

        public string ItemName
        {
            get => itemnameLabel.Text;
            set => itemnameLabel.Text = value;
        }

        public decimal Price
        {
            get => Convert.ToDecimal(priceLabel.Text.Replace("LKR ", ""));
            set => priceLabel.Text = $"LKR {value:F2}";
        }

        public decimal Rating
        {
            get => Convert.ToDecimal(ratingLabel.Text);
            set => ratingLabel.Text = value.ToString("0.0");
        }

        private string _imageKey;
        public string ImageKey
        {
            get => _imageKey;
            set
            {
                _imageKey = value;

                // Load image from Resources dynamically
                try
                {
                    object img = Properties.Resources.ResourceManager.GetObject(value);
                    if (img is Image image)
                    {
                        itempicPicbox.Image = image;
                    }
                    else
                    {
                        itempicPicbox.Image = Properties.Resources.default_food;
                    }
                }
                catch
                {
                    itempicPicbox.Image = Properties.Resources.default_food;
                }
            }
        }

        public event EventHandler AddToCartClicked;

        public FoodCard()
        {
            InitializeComponent();
            addBtn.Click += (s, e) => AddToCartClicked?.Invoke(this, EventArgs.Empty);
        }

        private void itempicPicbox_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
