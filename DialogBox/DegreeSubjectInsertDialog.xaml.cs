using SVLCmanage.DialogBox;
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
    /// Interaction logic for DegreeSubjectInsertWindow.xaml
    /// </summary>
    public partial class DegreeSubjectInsertDialog : Window
    {
        public DegreeSubjectInsertDialog()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

        private void CleanData()
        {
            txtSubCredit.Text = "";
            txtSubEngName.Text = "";
            txtSubHour.Text = "";
            txtSubID.Text = "";
            txtSubName.Text = "";

        }
      
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newsub = from sub in db.subjects
                               where sub.subject_id == txtSubID.Text 
                               select sub;


                if (newsub.Count() == 1)
                {
                    NotFoundDialog frm = new NotFoundDialog();
                    frm.label1.Text = " ວິຊານີ້ມີແລ້ວ ";
                    frm.ShowDialog();                   
                    CleanData();

                }
                else
                {

                    subject sub = new subject();
                    sub.subject_id = txtSubID.Text;
                    sub.subject_name = txtSubName.Text;
                    sub.subject_engname = txtSubEngName.Text;
                    sub.subject_credit = Convert.ToInt32(txtSubCredit.Text);
                    sub.subject_hour = txtSubHour.Text;
                    sub.degree11plus3plus3 = "ບໍ່ມີໃນຫຼັກສູດ";
                    sub.degree12plus3 = "ບໍ່ມີໃນຫຼັກສູດ";
                    sub.degree12plus4 = "ບໍ່ມີໃນຫຼັກສູດ";
                    db.subjects.InsertOnSubmit(sub);
                    db.SubmitChanges();

                    this.DialogResult = true;

                    CleanData();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSubID.Focus();
        }
       
    }
}
