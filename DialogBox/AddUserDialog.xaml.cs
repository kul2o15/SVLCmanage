using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SVLCmanage.DialogBox
{
    /// <summary>
    /// Interaction logic for AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public AddUserDialog()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string lv="";
                switch (comboBoxAuth.Text)
                {
                    case "ອາຈານ":
                        lv ="1";
                        break;
                    case "ຫ້ອງບໍລິຫານ":
                        lv="2";
                        break;
                    case "ຫ້ອງວິຊາການ":
                        lv="3";
                        break;                   
                }

                var Iuser = from h in db.auths
                                where h.user_id == txtUser.Text 
                                select h;

                    if (Iuser.Count() == 1)
                    {
                        NotFoundDialog frm = new NotFoundDialog();
                        frm.label1.Text = " ຊື່ຜູ່ໃຊ້ມີແລ້ວ ";
                        frm.ShowDialog();
                        Pass.Password = null;
                    }
                    else 
                    {
                        auth ulog = new auth();
                        ulog.user_id = txtUser.Text;
                        ulog.user_auth = lv;

                    if (checkBox.IsChecked == true)
                    {
                        ulog.user_pass = txtPass.Text;
                    }
                    else
                    {
                        ulog.user_pass = Pass.Password;
                    }
                        
                        db.auths.InsertOnSubmit(ulog);
                        db.SubmitChanges();                      
                    }

                this.DialogResult = true;
                
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
