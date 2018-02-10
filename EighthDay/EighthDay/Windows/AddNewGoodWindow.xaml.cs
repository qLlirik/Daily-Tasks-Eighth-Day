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
    /// Логика взаимодействия для AddNewGoodWindow.xaml
    /// </summary>
    public partial class AddNewGoodWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.db;

        public AddNewGoodWindow()
        {
            InitializeComponent();
            dpBegin.SelectedDate = DateTime.Now;
            dpEnd.SelectedDate = DateTime.Now;

            cbxBatch.ItemsSource = db.Batches.ToList();
            cbxBatch.DisplayMemberPath = "Name";
            cbxBatch.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new AddNewGoodsWindow().Show();
            this.Close();
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                double ID = 1;
                try
                {
                    ID = db.Goods.ToList().Last().ID;
                }
                catch { }

                db.Goods.Add(new DB.Goods
                {
                    ID = ID + 1,
                    Name = tbxName.Text,
                    International = tbxInternational.Text,
                    Begin = dpBegin.SelectedDate.Value,
                    End = dpEnd.SelectedDate.Value,
                    Yes = chbxYes.IsChecked == true ? true : false,
                    RF = tbxRF.Text,
                    Producer = tbxProducer.Text,
                    Instructions = tbxInstructions.Text,
                    Batches = (DB.Batches)cbxBatch.SelectedItem,
                    Price = decimal.Parse(tbxPrice.Text)
                });
                db.SaveChanges();

                if (MessageBox.Show("Новый товар добавлен! Желаете добавить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new AddNewGoodWindow().Show();
                    this.Close();
                }
                else
                    click_Back(null,null);
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
