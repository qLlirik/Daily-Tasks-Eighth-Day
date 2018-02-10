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
    /// Логика взаимодействия для AddNewGoodsWindow.xaml
    /// </summary>
    public partial class AddNewGoodsWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.db;

        public AddNewGoodsWindow()
        {
            InitializeComponent();

            cbxGoods.ItemsSource = db.Goods.ToList();
            cbxGoods.DisplayMemberPath = "Info";
            cbxGoods.SelectedIndex = 0;

            cbxProvider.ItemsSource = db.Provider.ToList();
            cbxProvider.DisplayMemberPath = "Name";
            cbxProvider.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new BuysWindow().Show();
            this.Close();
        }

        private void click_AddNewGood(object sender, RoutedEventArgs e)
        {
            new AddNewGoodWindow().Show();
            this.Close();
        }

        private void click_AddNewProvider(object sender, RoutedEventArgs e)
        {
            AddNewProviderAndClientWindow.wind = new AddNewGoodsWindow();
            new AddNewProviderAndClientWindow().Show();
            this.Close();
        }

        private void click_Ofrom(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Buy.Add(new DB.Buy
                {
                    Goods = (DB.Goods)cbxGoods.SelectedItem,
                    Provider = (DB.Provider)cbxProvider.SelectedItem,
                    Sign = chbxSign.IsChecked == true ? true : false,
                    GoodsInvoice = short.Parse(tbxGoodsInvoice.Text),
                    Date = DateTime.Now
                });

                var selgood = (DB.Goods)cbxGoods.SelectedItem;
                var good = db.GoodsStock.FirstOrDefault(w => w.GoodsID == selgood.ID);
                if (good == null)
                {
                    db.GoodsStock.Add(new DB.GoodsStock
                    {
                        Goods = (DB.Goods)cbxGoods.SelectedItem,
                        Count = double.Parse(tbxCount.Text)
                    });
                }
                else
                {
                    good.Count += int.Parse(tbxCount.Text);
                }
                db.SaveChanges();

                if (MessageBox.Show("Покупка оформлена! Желаете оформить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new AddNewGoodsWindow().Show();
                    this.Close();
                }
                else
                    click_Back(null, null);
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
