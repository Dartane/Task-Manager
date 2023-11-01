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
using TuskManaga;

using trying01;
using static trying01.TaskState;
using System.Runtime.Remoting.Channels;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Contexts;
using System.Collections;

namespace trying01
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBContainer db = new DBContainer();
        public static bool key = false;
        //Не передается переменная из окон авторизации - авторизируюсь в окне и должна передать переменную логина основной программе - но она теряется 
        List<TaskList> list = new List<TaskList>();
        
        public MainWindow()
        {
            InitializeComponent();
            /*
            MessageBox.Show(User + "Это юзер блять");
            if (User == null)
            {
                WinLogIn aw = new WinLogIn(); aw.Show();
                this.Hide();
            }
            */

            if (key == false)
            {
                WinLogIn aw = new WinLogIn();
                aw.Show();
                this.Hide();
            }
            if (key == true)
            {
                this.Show();
                string krang = UserInfo.Vivod();
                using (DBContainer db = new DBContainer())
                {
                    string s = UserInfo.Vivod();
                    var tasks = db.Tasks
                       .Select(t => new
                       {
                          Id = t.id_task, Name = t.name, Description = t.description, StartTime = t.time_start.ToString(), EndTime = t.time_finish.ToString(), Status = t.status, Login = t.login
                       }).ToList()
                        .Where(t => t.Login == s)
                        .ToList();

                    foreach (var task in tasks)
                    {
                        //MessageBox.Show(task.Name + task.Description + task.Status);
                        int key = 0;
                        TaskManage taskManage = new TaskManage(new PlanMod());
                        ITaskState currentState = new PlanMod();
                        foreach (var TaskList in list)
                        {
                            key++;
                            switch (TaskList.Ident)
                            {
                                case 0:
                                    taskManage = new TaskManage(new PlanMod());
                                    currentState = new PlanMod();
                                    break;
                                case 1:
                                    taskManage = new TaskManage(new ActiveMod());
                                    currentState = new ActiveMod();
                                    break;
                                case 2:
                                    taskManage = new TaskManage(new DisableMod());
                                    currentState = new DisableMod();
                                    break;
                                case 3:
                                    taskManage = new TaskManage(new EnableMod());
                                    currentState = new EnableMod();
                                    break;
                                case 4:
                                    taskManage = new TaskManage(new EndMod());
                                    currentState = new EndMod();
                                    break;

                            }


                        }
                        DateTime sdate = DateTime.Parse(task.StartTime.ToString());
                        DateTime edate = DateTime.Parse(task.EndTime.ToString());

                        list.Add(new TaskList()
                        {
                            Id = task.Id,
                            Name = task.Name,
                            Description = (string)task.Description,
                            StartTime = (string)sdate.ToString(),
                            EndTime = (string)edate.ToString(),
                            CurrentState = currentState,
                            State = taskManage,
                            Ident = (int)task.Status,
                            
                        });
                    }
                }
                
            }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskManage manage = new TaskManage(new PlanMod());
            ITaskState currentState = new PlanMod();


            TaskElEnter taskElEnter = new TaskElEnter();
            string taskname = " ";
            taskname += TB_01.Text;
            taskElEnter.NameCont(taskname);


            string taskdesc = " ";
            taskdesc += TB_02.Text;
            taskElEnter.DescriptionCont(taskdesc);


            string stasktime = " ";
            stasktime += TB_03.Text;
            taskElEnter.STimeCont(stasktime);

            string etasktime = " ";
            etasktime += TB_04.Text;
            taskElEnter.ETimeCont(etasktime);

            int kolvo = db.Tasks.Count();

            DateTime sdate = DateTime.Parse(stasktime);
            DateTime edate = DateTime.Parse(etasktime);
            Tasks tasc = new Tasks()
            {
                id_task = kolvo,
                name = taskname,
                description = taskdesc,
                time_start = sdate,
                time_finish = edate,
                status = 0,
                login = UserInfo.Vivod()
            };

            try

            {
                db.Tasks.Add(tasc);
                db.SaveChanges();
                MessageBox.Show("Задача создана");


                //a еще добавляем сюда глобальную переменную с логином 
            }

            catch
            {
                MessageBox.Show("Задача не создана");
            }


            list.Add(new TaskList
            {

                Name = taskname,
                Description = taskdesc,
                StartTime = stasktime,
                EndTime = etasktime,
                State = manage,
                CurrentState = currentState,
                Ident = 0
            });



            var taskBuilder = new TaskBuilder(taskElEnter, taskname, taskdesc, stasktime, etasktime);
            var taskBuilderDirector = new TaskBuilderDirector(taskBuilder);
            taskBuilderDirector.Build();

            var task = taskBuilder.GetTask();
            LB_01.Text += "Добавлена Задача\n" + task.ToString() + "\n";
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //cb1.Items.Clear();
            OKL.Items.Clear();
            int key = 0;
            foreach (var TaskList in list)
            {

                //LB_02.Text = "\n" + TaskList.Name;
                /*
                 LB_02.Text += "\n" + TaskList.Description;
                 LB_02.Text += "\n" + TaskList.Time;
                 switch (TaskList.Ident)
                 {
                     case 0:
                         LB_02.Text += "\n" + TaskList.State.Plan();
                         break;
                     case 1:
                         LB_02.Text += "\n" + TaskList.State.Enable();
                         break;
                     case 2:
                         LB_02.Text += "\n" + TaskList.State.Disable();
                         break;
                     case 3:
                         LB_02.Text += "\n" + TaskList.State.End();
                         break;
                     case 4:
                         LB_02.Text += "\n" + TaskList.State.Active();
                         break;

                 }
                 */
                //LB_02.Text += "\n" + TaskList.Ident.ToString();
                //Ll.Items.Add(LB_02.Text);
                //LB_02.Text = "";
                //LB_02.Text += "\n" + TaskList.CurrentState;
                //cb1.Items.Add(TaskList.State.ToString();
                key++;
                switch (TaskList.Ident)
                {
                    case 0:
                        //cb1.Items.Add(cb1.SelectedIndex + TaskList.Name + " " + TaskList.State.Plan());
                        OKL.Items.Add(String.Format("Number\t\t{0}\nName:\t\t{1}\nStart Time:\t{2}\nEnd Time:\t{3}\nState:\t\t{4}", key, TaskList.Name, TaskList.StartTime, TaskList.EndTime, TaskList.State.Plan()));
                        break;
                    case 1:
                        OKL.Items.Add(String.Format("Number\t\t{0}\nName:\t\t{1}\nStart Time:\t{2}\nEnd Time:\t{3}\nState:\t\t{4}", key, TaskList.Name, TaskList.StartTime, TaskList.EndTime, TaskList.State.Active()));
                        break;
                    case 2:
                        OKL.Items.Add(String.Format("Number\t\t{0}\nName:\t\t{1}\nStart Time:\t{2}\nEnd Time:\t{3}\nState:\t\t{4}", key, TaskList.Name, TaskList.StartTime, TaskList.EndTime, TaskList.State.Disable()));
                        break;
                    case 3:
                        OKL.Items.Add(String.Format("Number\t\t{0}\nName:\t\t{1}\nStart Time:\t{2}\nEnd Time:\t{3}\nState:\t\t{4}", key, TaskList.Name, TaskList.StartTime, TaskList.EndTime, TaskList.State.Enable()));
                        break;
                    case 4:
                        OKL.Items.Add(String.Format("Number\t\t{0}\nName:\t\t{1}\nStart Time:\t{2}\nEnd Time:\t{3}\nState:\t\t{4}", key, TaskList.Name, TaskList.StartTime, TaskList.EndTime, TaskList.State.End()));
                        break;

                }

                
            }
            if (OKL.Items.Count == 0) OKL.Items.Add("No Tasks!");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void OKL_SelectionChanged(object sender, RoutedEventArgs e)
        {

            LB_.Clear();
            if (e.OriginalSource is TextBox) OKL.SelectedItem = e.OriginalSource;
            int key = 0;
            int overkey = OKL.SelectedIndex + 1;
            foreach (var TaskList in list)
            {
                if (key == OKL.SelectedIndex)
                {
                    switch (TaskList.Ident)
                    {
                        case 0:
                            //cb1.Items.Add(cb1.SelectedIndex + TaskList.Name + " " + TaskList.State.Plan());
                            //LB_.Text = OKL.SelectedIndex+1 + " " + TaskList.Name + "\n" + TaskList.Description + "\n" + TaskList.Time + "\n" + TaskList.State.Plan();
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Plan());

                            break;
                        case 1:
                            //LB_.Text = OKL.SelectedIndex+1 + " " + TaskList.Name + "\n" + TaskList.Description + "\n" + TaskList.Time + "\n" + TaskList.State.Active();
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Active());
                            break;
                        case 2:
                            //LB_.Text = OKL.SelectedIndex+1 + " " + TaskList.Name + "\n" + TaskList.Description + "\n" + TaskList.Time + "\n" + TaskList.State.Disable();
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Disable());
                            break;
                        case 3:
                            //LB_.Text = OKL.SelectedIndex+1 + " " + TaskList.Name + "\n" + TaskList.Description + "\n" + TaskList.Time + "\n" + TaskList.State.Enable();
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Enable());
                            break;
                        case 4:
                            //LB_.Text = OKL.SelectedIndex+1 + " " + TaskList.Name + "\n" + TaskList.Description + "\n" + TaskList.Time + "\n" + TaskList.State.End();
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.End());
                            break;
                        default:
                            break;
                    }
                }
                key++;

            }
        }

        public void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource is TextBox) cb1.SelectedItem = e.OriginalSource;
            int key = 0;
            int overkey = OKL.SelectedIndex + 1;
            foreach (var TaskList in list)
            {
                if (key == OKL.SelectedIndex)
                {
                    switch (cb1.SelectedIndex)
                    {
                        case 0:
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Plan());
                            //cb1.Items.Add(cb1.SelectedIndex + TaskList.Name + " " + TaskList.State.Plan());
                            /*
                            list[key].State = new TaskManage(new PlanMod());
                            list[key].CurrentState = new PlanMod();
                            list[key].Ident = 0;
                            */
                            break;
                        case 1:
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Active());
                            if (TaskList.Ident == 0)
                            {
                                list[key].State = new TaskManage(new ActiveMod());
                                list[key].CurrentState = new ActiveMod();
                                list[key].Ident = 1;
                            }
                            break;
                        case 2:
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Disable());
                            if (TaskList.Ident == 1 || TaskList.Ident == 3)
                            {
                                list[key].State = new TaskManage(new DisableMod());
                                list[key].CurrentState = new DisableMod();
                                list[key].Ident = 2;
                            }
                            break;
                        case 3:
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.Enable());
                            if (TaskList.Ident == 2)
                            {
                                list[key].State = new TaskManage(new EnableMod());
                                list[key].CurrentState = new EnableMod();
                                list[key].Ident = 3;
                            }
                            break;
                        case 4:
                            LB_.Text = String.Format("Number\t\t{0}\nName:\t\t{1}\nDescription:\n{2}\nStart Time:\t\t{3}\nEnd Time:\t\t{4}\nState:\t\t{5} ", overkey, TaskList.Name, TaskList.Description, TaskList.StartTime, TaskList.EndTime, TaskList.State.End());
                            if (TaskList.Ident != 0)
                            {
                                list[key].State = new TaskManage(new EndMod());
                                list[key].CurrentState = new EndMod();
                                list[key].Ident = 4;
                            }
                            break;
                        default:

                            break;
                    }
                    /*
                    var query =
                        from ord in db.Tasks
                        where ord. == 11000
                        select ord;

                    // Execute the query, and change the column values
                    // you want to change.
                    foreach (Order ord in query)
                    {
                        ord.ShipName = "Mariner";
                        ord.ShipVia = 2;
                        // Insert any additional changes to column values.
                    }

                    // Submit the changes to the database.
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }
                    */
                    int s = TaskList.Id;
                    var tasks = db.Tasks
                           .Select(t => new
                           {
                               Id = t.id_task,
                               Name = t.name,
                               Description = t.description,
                               StartTime = t.time_start.ToString(),
                               EndTime = t.time_finish.ToString(),
                               Status = t.status,
                               Login = t.login
                           }).ToList()
                            .Where(t => t.Id == s)
                            .ToList();
                    //tasks.Status
                    //foreach (var stat in tasks)
                    //{
                    //    stat.Status = TaskList.Ident;
                    //}
                    var tusks = from t in tasks
                                where t.Id == s
                                select t;
                    (from p in db.Tasks
                     where p.id_task == s
                     select p).ToList()
                                        .ForEach(x => x.status = TaskList.Ident);
                    try
                    {
                        MessageBox.Show("Статус Задачи изменен!");
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("ТВою мать в кино водил");
                    }
                }
                key++;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*month.Items.Add(new ComboBoxItem());
            number.Items.Clear();
            if (month.SelectedIndex == 0 ||
                month.SelectedIndex == 2 ||
                month.SelectedIndex == 4 ||
                month.SelectedIndex == 6 ||
                month.SelectedIndex == 7 ||
                month.SelectedIndex == 9 ||
                month.SelectedIndex == 11)
            {
                for(int i = 1; i <= 31; i++)
                {
                    number.Items.Add(i.ToString());
                }
            }
            if (month.SelectedIndex == 3 ||
                month.SelectedIndex == 5 ||
                month.SelectedIndex == 8 ||
                month.SelectedIndex == 10)
            {
                for (int i = 1; i <= 30; i++)
                {
                    number.Items.Add(i.ToString());
                }
            }
            if (month.SelectedIndex == 1)
            {
                for (int i = 1; i <= 28; i++)
                {
                    number.Items.Add(i.ToString());
                }
            }
            */
        }
    }
}
