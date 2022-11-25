using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Var_9
{
    /// <summary>
    /// Логика взаимодействия для Add_material.xaml
    /// </summary>
    public partial class Add_material : Window
    {

        public Product Product { get; set; }

        
        public Add_material()
        {
            InitializeComponent();
            Product = new Product();
            SP_Add.GetBindingExpression(DataContextProperty).UpdateTarget();
            LoadCategory();
            LoadUnit();

            DataContext = this;
        }

        private void LoadCategory()
        {
            var pos = Administration.connection.Category.ToList();
            foreach (var p in pos)
            {

                C_B_MaterialType.Items.Add(p.NameCategory);
            }
            
        }

        private void LoadUnit()
        {
            var pos = Administration.connection.UnitProduct.ToList();
            foreach (var p in pos)
            {

                C_B_Unit.Items.Add(p.NameUnit);
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Administration.connection.Product.Add(Product);
            if (Administration.connection.SaveChanges() == 1)
            {
                Administration.Product.Add(Product);
                Product = new Product();
                SP_Add.GetBindingExpression(DataContextProperty).UpdateTarget();
                MessageBox.Show("Товар добавлен");
                
            }
        }

        private void Add_Image_2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                Product.ProductPhoto = "" + openFileDialog.FileName;

            }
        }

       
    }

      
    
}
