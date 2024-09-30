using LearnSchoolAppDemo.DB;
using LearnSchoolAppDemo.MyClasses;
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

namespace LearnSchoolAppDemo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Service> AllServices = new List<Service>();
        private static List<Service> FilterServices = new List<Service>();
        public MainWindow()
        {
            InitializeComponent();
            AllServices = App.db.Service.ToList();
            DiscountTypeCb.ItemsSource = new List<DiscountType>()
            {
                new DiscountType("Все", 0, 100),
                new DiscountType("от 0 до 5%", 0, 5),
                new DiscountType("от 5% до 15%", 5, 15),
                new DiscountType("от 15% до 30%", 15, 30),
                new DiscountType("от 30% до 70%", 30, 70),
                new DiscountType("от 70% до 100%", 70, 100)
            };
            DiscountTypeCb.DisplayMemberPath = "Name";
            DiscountTypeCb.SelectedIndex = 0;
            CostSortCb.SelectedIndex = 0;
        }
        private void Filtration()
        {
            FilterServices = AllServices;
            if (CostSortCb.SelectedIndex == 0)
            {
                FilterServices = FilterServices.OrderBy(x => x.CostWithDiscount).ToList();
            }
            else
            {
                FilterServices = FilterServices.OrderByDescending(x => x.CostWithDiscount).ToList();
            }
            DiscountType discountType = (DiscountType)DiscountTypeCb.SelectedItem;
            FilterServices = FilterServices.Where(x => x.Discount >= discountType.Min && x.Discount < discountType.Max).ToList();
            if (!string.IsNullOrWhiteSpace(SearchTb.Text))
            {
                string searchText  = SearchTb.Text.Trim().ToLower();
                FilterServices = FilterServices.Where(x => x.Title.Trim().ToLower().Contains(searchText)).ToList();
            }
            ServiceList.ItemsSource = FilterServices;
            DataCountTb.Text = $"{FilterServices.Count} из {AllServices.Count}";
        }

        private void CostSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }
        private void DiscountTypeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtration();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }
    }
}
