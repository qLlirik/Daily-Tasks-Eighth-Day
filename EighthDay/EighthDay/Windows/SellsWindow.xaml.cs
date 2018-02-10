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
    /// Логика взаимодействия для SellsWindow.xaml
    /// </summary>
    public partial class SellsWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.db;

        public SellsWindow()
        {
            InitializeComponent();

            click_Search(null,null);

            cbxSearch.ItemsSource = db.GoodsStock.ToList();
            cbxSearch.DisplayMemberPath = "Goods.Name";
        }

        private void click_AddNewGoods(object sender, RoutedEventArgs e)
        {
            new SellWindow().Show();
            this.Close();
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void click_Search(object sender, RoutedEventArgs e)
        {
            var qwery = db.GoodsStock.Where(w => w.Goods.ID != null);
            if (cbxSearch.Text.Length != 0)
                qwery = qwery.Where(w => w.Goods.Name.Contains(cbxSearch.Text));
            lv.ItemsSource = qwery.ToList();
        }
    }
}
