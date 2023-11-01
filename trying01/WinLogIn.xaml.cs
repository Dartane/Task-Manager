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
using static trying01.UserInfo;

namespace trying01
{
    /// <summary>
    /// Логика взаимодействия для WinLogIn.xaml
    /// </summary>
    public partial class WinLogIn : Window
    {
        DBContainer db = new DBContainer();
        
        public WinLogIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            WinAutReg aw = new WinAutReg(); 
            aw.Show();
            this.Close();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {


            if (login.Text == "" || password.Password == "") 
            {
                MessageBox.Show("Ошибка пустые поля");
                return;
            }


            if (db.Users.Select(item => item.login + " " + item.password).Contains(login.Text + " " + password.Password))
            {
                MessageBox.Show("Вы авторизированы");
                UserInfo userinfo = new UserInfo(login.Text);

                MainWindow.key = true;
                MainWindow aw = new MainWindow();
                aw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка логина/пароля");
                return;
            }

            //aw.User = login.Text;


            
            //MessageBox.Show(UserInfo.Vivod());
            //a еще добавляем сюда глобальную переменную с логином

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    
    }
}
