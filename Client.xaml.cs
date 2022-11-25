using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Var_9
{
   
    public partial class Client : Page
    {
        public ObservableCollection<Product> Product { get; set; }

        public ObservableCollection<Category> Category { get; set; }

        public static TradeEntities6 connection = new TradeEntities6();
        public ObservableCollection<SortItem> SortItems { get; set; } = new ObservableCollection<SortItem>()
        {
            new SortItem() { DisplayName = "По возрастанию наименования", PropertyName = "ProductName", Ascending = true},
            new SortItem() { DisplayName = "По убыванию наименования", PropertyName = "ProductName", Ascending = false},
            new SortItem() { DisplayName = "По возрастанию остатка", PropertyName = "ProductQuantityInStock", Ascending = true},
            new SortItem() { DisplayName = "По убыванию остатка", PropertyName = "ProductQuantityInStock", Ascending = false}

        };

        public Client()
        {
            InitializeComponent();
            Product = new ObservableCollection<Product>();
            Category = new ObservableCollection<Category>();
            Filter.ItemsSource = connection.Category.ToList();
            connection.Product.ToList().ForEach(material => Product.Add(material));
            DataContext = this;
        }

        private void SortByName(string property, bool asc = true)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Joister.ItemsSource);
            if (view == null) return;

            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(property, asc ? ListSortDirection.Ascending : ListSortDirection.Descending));
        }

        public void ViewPage(int page) // по нажатию на кнопку отрывается 15 товаров
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Joister.ItemsSource);
            if (view == null) return;

            int start = page * 10;
            int end = start + 10;
            int materilCounter = 0;
            view.Filter = new Predicate<object>(obj =>
            {
                bool isView = materilCounter >= start &&
                materilCounter < end;

                materilCounter++;
                return isView;
            });
        }
        private void FilterByName(string Category)
        {
            ICollectionView view_2 = CollectionViewSource.GetDefaultView(Joister.ItemsSource);
            if (view_2 == null) return;

            view_2.Filter = new Predicate<object>(obj =>
            {
                if (Category == "Все типы")
                    return true;
                return ((Product)obj).ProductCategory == Category;
            });

        }

        private void SearchBy(string substring)
        {
            ICollectionView view_3 = CollectionViewSource.GetDefaultView(Joister.ItemsSource);
            if (view_3 == null) return;
            view_3.Filter = new Predicate<object>(obj =>
            {
                return (((Product)obj).ProductName.ToLower().Contains(substring.ToLower()));

            });

        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortItem sortItem = Sort.SelectedItem as SortItem;
            SortByName(sortItem.PropertyName, sortItem.Ascending);

        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category category = Filter.SelectedItem as Category;
            FilterByName(category.NameCategory);
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBy(Search.Text);
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            ViewPage(int.Parse(button.Content.ToString()));
        }
    }
}
