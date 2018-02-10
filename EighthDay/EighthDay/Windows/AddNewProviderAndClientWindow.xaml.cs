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
    /// Логика взаимодействия для AddNewProviderAndClientWindow.xaml
    /// </summary>
    public partial class AddNewProviderAndClientWindow : Window
    {
        static public Window wind;
        DB.Entity db = HelpClasses.StaticClass.db;

        public AddNewProviderAndClientWindow()
        {
            InitializeComponent();
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            if (wind.Tag + "" == new AddNewGoodsWindow().Tag + "")
                new AddNewGoodsWindow().Show();
            else
                new SellWindow().Show();
            this.Close();
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            if ((tbxName.Text.Length == 0) && (tbxAddress.Text.Length == 0) && (tbxPhone.Text.Length == 0) && (tbxINN.Text.Length == 0))
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            try
            {
                if (wind.Tag + "" == "1")
                {
                    db.Provider.Add(new DB.Provider {
                        Name = tbxName.Text,
                        Address = tbxAddress.Text,
                        Phone = tbxPhone.Text,
                        INN = tbxINN.Text
                    });
                    db.SaveChanges();

                    MessageBox.Show("Добавление нового продавца прошла успешно!", "Perfcet", MessageBoxButton.OK, MessageBoxImage.Information);
                    click_Back(null, null);
                }
                else
                {
                    db.Clients.Add(new DB.Clients
                    {
                        Name = tbxName.Text,
                        Address = tbxAddress.Text,
                        Phone = tbxPhone.Text,
                        INN = tbxINN.Text
                    });
                    db.SaveChanges();

                    MessageBox.Show("Добавление нового покупателя прошла успешно!", "Perfcet", MessageBoxButton.OK, MessageBoxImage.Information);
                    click_Back(null, null);
                }
            }
            catch
            {
                MessageBox.Show("Неизвестная ошибка!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
