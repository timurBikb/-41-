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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Бикбулатов41Размер
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        // для отображения количества записей
        int CountRecords;
        public ProductPage()
        {
            InitializeComponent();
            var currentData = Bikbulatov41Entities.GetContext().Product.ToList();
            // общее количество записей
            CountRecords = currentData.Count;

            ProductListView.ItemsSource = currentData;

            ComboType.SelectedIndex = 0;

            
        }

        private void UpdateServices()
        {
            // берем данные из бд
            var currentData = Bikbulatov41Entities.GetContext().Product.ToList();

            // прописываем фильтрацию
            if (ComboType.SelectedIndex == 0)
            {
                currentData = currentData.Where(p => (p.ProductDiscount >= 0 && p.ProductDiscount <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentData = currentData.Where(p => (p.ProductDiscount >= 0 && p.ProductDiscount < 3)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentData = currentData.Where(p => (p.ProductDiscount >= 10 && p.ProductDiscount < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentData = currentData.Where(p => (p.ProductDiscount >= 15 && p.ProductDiscount <= 100)).ToList();
            }

            // поиск по тексту
            currentData = currentData.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка по убыванию/возрастанию
            if (RButtonDown.IsChecked.Value)
            {
                currentData = currentData.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (RButtonUp.IsChecked.Value)
            {
                currentData = currentData.OrderBy(p => p.ProductCost).ToList();
            }

            // отображаем итоги поиска/фильтрации/сортировки
            ProductListView.ItemsSource = currentData;

            // количество элементов после фильтрации
            int CountCurrRecords = currentData.Count;
            CountBlock.Text = "кол-во " + CountCurrRecords.ToString() + " из " + CountRecords.ToString();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }
    }
}
