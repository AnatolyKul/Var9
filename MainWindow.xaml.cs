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

namespace Var_9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static TradeEntities6 connection = new TradeEntities6();
        private string code = "";
        private const string chars = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789@#$%&";
        public MainWindow()
        {
            InitializeComponent();
            connection = Class2.GetKulEntities();
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890@?#";
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 6; i++)
            {
                int a = random.Next(0, chars.Length - 1);
                code += chars.Substring(a, 1);
            }

            Captha_L.Text = code;

#if DEBUG
            Login_TB.Text = "zpv1r94d5ous@gmail.com";
            Captha_TB.Text = code;
            Password_TB.Password = "2L6KZG";
#endif
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            string Login = Login_TB.Text; //проверка логина
            string Password = Password_TB.Password; // проверка пароля
           
            if ((Login.Length == 0) && (Password.Length == 0)) // если логин и пароль не ведены
            {
                MessageBox.Show("Все поля пустые!");
                return;
            }
            if (Login.Length == 0)
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (Password.Length == 0)
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (Captha_TB.Text != code)
            {
                MessageBox.Show("Неверный код проверки!!!");
                return;
            }

            else
            {



                var RUser1 = connection.User.Where(em => em.UserLogin == Login && em.UserPassword == Password).FirstOrDefault(); //проверка кто заходит
                if (RUser1 != null)
                {



                    switch (RUser1.UserRole)
                    {
                        case 1:
                            MessageBox.Show("Добро пожаловать, администратор  " + RUser1.UserFIO); //зашел админ
                            MainFrame.NavigationService.Navigate(Class1.administration);
                        
                            Auth_L.Visibility = Visibility.Collapsed;
                            Logo_IM.Visibility = Visibility.Collapsed;
                            Login_Lab.Visibility = Visibility.Collapsed;
                            Login_TB.Visibility = Visibility.Collapsed;
                            Password_Lab.Visibility = Visibility.Collapsed;
                            Password_TB.Visibility = Visibility.Collapsed;
                            Cap_Lab.Visibility = Visibility.Collapsed;
                            Captha_L.Visibility = Visibility.Collapsed;
                            Cap_L2.Visibility = Visibility.Collapsed;
                            Captha_TB.Visibility = Visibility.Collapsed;
                            Log.Visibility = Visibility.Collapsed;
                            Without_Log.Visibility = Visibility.Collapsed;
                            Main_Lab.Visibility = Visibility.Collapsed;

                            break;
                        case 2:
                            MessageBox.Show("Добро пожаловать, менеджер" + RUser1.UserFIO); //зашел менеджер
                            MainFrame.NavigationService.Navigate(Class1.manager);
            
                            Auth_L.Visibility = Visibility.Collapsed;
                            Logo_IM.Visibility = Visibility.Collapsed;
                            Login_Lab.Visibility = Visibility.Collapsed;
                            Login_TB.Visibility = Visibility.Collapsed;
                            Password_Lab.Visibility = Visibility.Collapsed;
                            Password_TB.Visibility = Visibility.Collapsed;
                            Cap_Lab.Visibility = Visibility.Collapsed;
                            Captha_L.Visibility = Visibility.Collapsed;
                            Cap_L2.Visibility = Visibility.Collapsed;
                            Captha_TB.Visibility = Visibility.Collapsed;
                            Log.Visibility = Visibility.Collapsed;
                            Without_Log.Visibility = Visibility.Collapsed;
                            Main_Lab.Visibility = Visibility.Collapsed;

                            break;
                        case 3:
                            MessageBox.Show("Добро пожаловать, " + RUser1.UserFIO); //зашел клиент
                            MainFrame.NavigationService.Navigate(Class1.client);
                            
                            Auth_L.Visibility = Visibility.Collapsed;
                            Logo_IM.Visibility = Visibility.Collapsed;
                            Login_Lab.Visibility = Visibility.Collapsed;
                            Login_TB.Visibility = Visibility.Collapsed;
                            Password_Lab.Visibility = Visibility.Collapsed;
                            Password_TB.Visibility = Visibility.Collapsed;
                            Cap_Lab.Visibility = Visibility.Collapsed;
                            Captha_L.Visibility = Visibility.Collapsed;
                            Cap_L2.Visibility = Visibility.Collapsed;
                            Captha_TB.Visibility = Visibility.Collapsed;
                            Log.Visibility = Visibility.Collapsed;
                            Without_Log.Visibility = Visibility.Collapsed;
                            Main_Lab.Visibility = Visibility.Collapsed;
                            break;

                    }


                }
                else
                {
                    MessageBox.Show("Введены некорректные данные!!!");
                }

            }
        }

            private void Without_Log_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добро пожаловать, гость");
            MainFrame.NavigationService.Navigate(Class1.client_not_auth);
            Auth_L.Visibility = Visibility.Collapsed;
            Logo_IM.Visibility = Visibility.Collapsed;
            Login_Lab.Visibility = Visibility.Collapsed;
            Login_TB.Visibility = Visibility.Collapsed;
            Password_Lab.Visibility = Visibility.Collapsed;
            Password_TB.Visibility = Visibility.Collapsed;
            Cap_Lab.Visibility = Visibility.Collapsed;
            Captha_L.Visibility = Visibility.Collapsed;
            Cap_L2.Visibility = Visibility.Collapsed;
            Captha_TB.Visibility = Visibility.Collapsed;
            Log.Visibility = Visibility.Collapsed;
            Without_Log.Visibility = Visibility.Collapsed;
            Main_Lab.Visibility = Visibility.Collapsed;
        }
    }
 }


