using SVLCmanage.DialogBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SVLCmanage
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();
        string templog = "";

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private void passwordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (Pass.Password == "" || txtUser.Text == "")
                {
                    return;
                }
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    var Iuser = from h in db.auths
                                where h.user_id == txtUser.Text && h.user_pass == Pass.Password
                                select new
                                {
                                    h.user_id,
                                    h.user_auth,
                                };

                    if (Iuser.Count() == 0)
                    {
                        NotFoundDialog frm = new NotFoundDialog();
                        frm.label1.Text = " ຊື່ຜູ່ໃຊ້ ຫຼີ ລະຫັດບໍຖືກຕ້ອງ ";
                        frm.ShowDialog();
                        Pass.Password = null;
                    }
                    else if (Iuser.Count() == 1)
                    {                       
                        MainWindow frm = new MainWindow();
                        frm.Show();

                        login log = new login();
                        log.username_ip = Iuser.FirstOrDefault().user_id + templog;
                        log.logintime = DateTime.Now.ToString();
                        db.logins.InsertOnSubmit(log);
                        db.SubmitChanges();

                        GlobalVariableClass.LevelAuth = Iuser.FirstOrDefault().user_auth;
                        GlobalVariableClass.UserAuth = Iuser.FirstOrDefault().user_id;

                        this.Hide();
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPass_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (Pass.Password == "" || txtUser.Text == "")
                {
                    return;
                }
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    var Iuser = from h in db.auths
                                where h.user_id == txtUser.Text && h.user_pass == Pass.Password
                                select h;

                    if (Iuser.Count() == 0)
                    {
                        NotFoundDialog frm = new NotFoundDialog();
                        frm.label1.Text = " ຊື່ຜູ່ໃຊ້ ຫຼີ ລະຫັດບໍຖືກຕ້ອງ ";
                        frm.ShowDialog();
                        Pass.Password = null;
                    }
                    else if (Iuser.Count() == 1)
                    {
                        login log = new login();
                        log.username_ip = Iuser.FirstOrDefault().user_id + templog;
                        log.logintime = DateTime.Now.ToString();
                        db.logins.InsertOnSubmit(log);
                        db.SubmitChanges();

                        GlobalVariableClass.LevelAuth = Iuser.FirstOrDefault().user_auth;
                        GlobalVariableClass.UserAuth = Iuser.FirstOrDefault().user_id;
                      
                        MainWindow frm = new MainWindow();
                        frm.Show();
                        this.Hide();

                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            Pass.Visibility = System.Windows.Visibility.Hidden;
            txtPass.Visibility = System.Windows.Visibility.Visible;
            txtPass.Text = Pass.Password;            
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Pass.Visibility = System.Windows.Visibility.Visible;
            txtPass.Visibility = System.Windows.Visibility.Hidden;
            Pass.Password = txtPass.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();
            templog = "  " + Dns.GetHostName() + " " + GetLocalIPAddress();
        }
    }
}
