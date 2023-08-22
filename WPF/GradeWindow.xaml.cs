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

namespace SVLCmanage
{
    /// <summary>
    /// Interaction logic for GradeSearchWindow.xaml
    /// </summary>
    public partial class GradeWindow : Window
    {
        public GradeWindow()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
        }

        private void BtnOther_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textcount.Text = "";
                GradeStudentDialog frm = new GradeStudentDialog();
                frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
                {
                    textSubName.Text = "ວິຊາ:  " + GlobalVariableClass.TempString4 + System.Environment.NewLine +
                     "ຜູ່ປ້ອນຄະແນນ: " + GlobalVariableClass.UserAuth + System.Environment.NewLine +
                     "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " ສົກສືກສາ " + GlobalVariableClass.TempString3 +
                     " ຫ້ອງ " + GlobalVariableClass.TempString7;

                    var Istudent = from std in db.students

                                   where std.student_degree == GlobalVariableClass.TempString2
                                   && std.student_year == GlobalVariableClass.TempString3
                                   && std.student_timestudy == GlobalVariableClass.TempString6
                                   && std.student_classroom == GlobalVariableClass.TempString7                                 
                                   select new
                                   {
                                       ລະຫັດນັກສືກສາ = std.student_id,
                                       ຊື່ = std.student_name,
                                       ເພດ = std.student_gender                                     
                                   };




                    dataGrid.ItemsSource = Istudent;
                        textcount.Text = "ຈຳນວນ " + Istudent.Count().ToString() + " ຄົນ";
                                    
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTYear.Focus();
            textSubName.Text = "ວິຊາ:  "+ GlobalVariableClass.TempString4 + System.Environment.NewLine +
                "ຜູ່ປ້ອນຄະແນນ: " + GlobalVariableClass.UserAuth;          
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object celldata = dataGrid.SelectedItem;
                Type typeDemo = celldata.GetType();
                GlobalVariableClass.TempString5 = ((int)typeDemo.GetProperty("ລະຫັດນັກສືກສາ").GetValue(celldata, null)).ToString();
                GlobalVariableClass.TempString2 = txtTeacher.Text;
                GlobalVariableClass.TempString3 = txtTYear.Text;

                var newgrade = from grd in db.grades
                               where grd.student_id == GlobalVariableClass.TempString5
                               && grd.subject_id == GlobalVariableClass.TempString
                               select grd;

                GradeDialog frm = new GradeDialog();

                if (newgrade.Count() == 0)
                {
                   
                }
                else
                {                   
                    frm.txtTotal.Text = newgrade.FirstOrDefault().grade_total;
                    frm.comboBoxGrade.Text = newgrade.FirstOrDefault().grade_grade;
                }
              
                frm.ShowDialog();
              
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
