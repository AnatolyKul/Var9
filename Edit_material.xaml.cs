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
using Microsoft.Win32;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Var_9
{
    /// <summary>
    /// Логика взаимодействия для Edit_material.xaml
    /// </summary>
    public partial class Edit_material : Window
    {

        public Product Product { get; set; }
        
        public Edit_material(Product product)
        {
            InitializeComponent();
            Product = product;

            C_B_MaterialType.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = Administration.Category

            });

            C_B_Unit.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Source = Administration.UnitProduct

            });

            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
             var results = Administration.connection.SaveChanges();
                if (results == 0)
                {
                    MessageBox.Show("Не измененно");
                }
                else
                {
                    MessageBox.Show("Изменилось");
                }
           
            
           
        }

        private void Add_Image_Click(object sender, RoutedEventArgs e)
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
