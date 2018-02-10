using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EighthDay.Windows
{
    /// <summary>
    /// Логика взаимодействия для SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.db;

        public SellWindow()
        {
            InitializeComponent();

            cbxGoods.ItemsSource = db.GoodsStock.ToList();
            cbxGoods.DisplayMemberPath = "Goods.Info";
            cbxGoods.SelectedIndex = 0;

            cbxClient.ItemsSource = db.Clients.ToList();
            cbxClient.DisplayMemberPath = "Name";
            cbxClient.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new SellsWindow().Show();
            this.Close();
        }

        private void click_Ofrom(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((DB.GoodsStock)cbxGoods.SelectedItem).Count < int.Parse(tbxCount.Text))
                {
                    MessageBox.Show("На складе не хвататет товара!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }

                db.Sell.Add(new DB.Sell {
                    Goods = ((DB.GoodsStock)cbxGoods.SelectedItem).Goods,
                    Clients = (DB.Clients)cbxClient.SelectedItem,
                    CountNumber = short.Parse(tbxCountNumber.Text),
                    DateStarts = dpDateStart.SelectedDate.Value,
                    Count = double.Parse(tbxCount.Text),
                    Markup = decimal.Parse(tbxMarkup.Text),
                    Sum = (decimal.Parse(tbxCount.Text)) * (decimal.Parse(tbxMarkup.Text) + ((DB.GoodsStock)cbxGoods.SelectedItem).Goods.Price),
                    Cash = chbxCach.IsChecked == true ? true : false,
                    Worker = tbxWorker.Text,
                    Invoice = short.Parse(tbxInvoice.Text),
                    Seller = tbxSeller.Text,
                    Date = DateTime.Now
                });

                var good = (DB.GoodsStock)cbxGoods.SelectedItem;
                good.Count -= double.Parse(tbxCount.Text);
                if (good.Count == 0)
                    db.GoodsStock.Remove(good);
                db.SaveChanges();

                if (MessageBox.Show("Оформление продажи прошла успшено! Желаете оформить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new SellWindow().Show();
                    this.Close();
                }
                else
                    click_Back(null,null);
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void click_AddNewProvider(object sender, RoutedEventArgs e)
        {
            AddNewProviderAndClientWindow.wind = this;
            new AddNewProviderAndClientWindow().Show();
            this.Close();
        }

        private void keydown_cbxMarkup(object sender, KeyEventArgs e)
        {
            try
            {
                tbxSum.Text = (double.Parse(tbxCount.Text)) * Convert.ToDouble(decimal.Parse(tbxMarkup.Text) + ((DB.GoodsStock)cbxGoods.SelectedItem).Goods.Price) + "";
            }
            catch { }
        }

        private void keydown_tbxCount(object sender, KeyEventArgs e)
        {
            try
            {
                tbxSum.Text = (double.Parse(tbxCount.Text)) * Convert.ToDouble(decimal.Parse(tbxMarkup.Text) + ((DB.GoodsStock)cbxGoods.SelectedItem).Goods.Price) + "";
            }
            catch { }
        }
    }
}
