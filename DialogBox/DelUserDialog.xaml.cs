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
    /// Interaction logic for DelUserDialog.xaml
    /// </summary>
    public partial class DelUserDialog : Window
    {
        public DelUserDialog()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                auth ulog = new auth();
                ulog.user_id = comboBoxUser.Text;              
                db.auths.DeleteOnSubmit(ulog);
                db.SubmitChanges();
                this.DialogResult = true;
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                comboBoxUser.ItemsSource = from h in db.auths 
                                           where h.user_auth !="4" && h.user_auth != "5"
                                           select h.user_id ;
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
