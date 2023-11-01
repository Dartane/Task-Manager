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

namespace trying01
{
    /// <summary>
    /// Логика взаимодействия для WinAutReg.xaml
    /// </summary>
    public partial class WinAutReg : Window
    {
        DBContainer db = new DBContainer();
        public WinAutReg()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == "" || password.Password == "")
            {
                MessageBox.Show("Пустые поля");
                return;
            }
            if (db.Users.Select(item => item.login).Contains(login.Text))
            {
                MessageBox.Show("Такой логин существует в системе");
                return;
            }

            Users newUser = new Users()
            {
                login = login.Text,
                password = password.Password
            };
                   
            try 
            
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались");

                MainWindow aw = new MainWindow();
                aw.Show();
                this.Close();
                

                //a еще добавляем сюда глобальную переменную с логином 
            } 

            catch 
            { 
                //lol
            }
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           WinLogIn aw = new WinLogIn(); aw.Show();
            this.Close();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
        }
    }
}
