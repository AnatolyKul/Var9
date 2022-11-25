using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Var_9
{

    

    public partial class Administration : Page
    {
        public static ObservableCollection<Product> Product { get; set; }

        public static ObservableCollection<Category> Category { get; set; }

        public static ObservableCollection<UnitProduct> UnitProduct { get; set; }

        public static TradeEntities6 connection = new TradeEntities6();

        private Edit_material edit_Material;

        
        public ObservableCollection<SortItem> SortItems { get; set; } = new ObservableCollection<SortItem>()
        {
            new SortItem() { DisplayName = "По возрастанию наименования", PropertyName = "ProductName", Ascending = true},
            new SortItem() { DisplayName = "По убыванию наименования", PropertyName = "ProductName", Ascending = false},
            new SortItem() { DisplayName = "По возрастанию остатка", PropertyName = "ProductQuantityInStock", Ascending = true},
            new SortItem() { DisplayName = "По убыванию остатка", PropertyName = "ProductQuantityInStock", Ascending = false}

        };

        public Administration()
        {
            InitializeComponent();
            Product = new ObservableCollection<Product>(connection.Product.ToList());
            Category = new ObservableCollection<Category>(connection.Category.ToList());
            UnitProduct = new ObservableCollection<UnitProduct>(connection.UnitProduct.ToList());
            Filter.ItemsSource = connection.Category.ToList();
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


        private void Joister_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (edit_Material != null) return;

            edit_Material = new Edit_material(Joister.SelectedItem as Product);
            edit_Material.Closed += Edit_material_Closed;
            edit_Material.ShowDialog();

        }

        private void Edit_material_Closed(object sender, EventArgs e)
        {
            edit_Material = null;

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            ViewPage(int.Parse(button.Content.ToString()));
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            new Add_material().Show();
        }
    }
}
