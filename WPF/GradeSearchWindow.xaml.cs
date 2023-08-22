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

namespace SVLCmanage.WPF
{
    /// <summary>
    /// Interaction logic for GradeSearchWindow.xaml
    /// </summary>
    public partial class GradeSearchWindow : Window
    {
        public GradeSearchWindow()
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
                     "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " ສົກສືກສາ " + GlobalVariableClass.TempString3+
                     " ຫ້ອງ " + GlobalVariableClass.TempString7;

                    var Istudent = from h in db.grades
                                   join sub in db.subjects on h.subject_id equals sub.subject_id into newsub
                                   from nsub in newsub.DefaultIfEmpty()
                                   join std in db.students on h.student_id equals std.student_id.ToString() into newstd
                                   from nstd in newstd.DefaultIfEmpty()
                                   where nstd.student_degree == GlobalVariableClass.TempString2
                                   && nstd.student_year == GlobalVariableClass.TempString3
                                   && nstd.student_timestudy == GlobalVariableClass.TempString6
                                   && nstd.student_classroom == GlobalVariableClass.TempString7
                                   && (nsub.subject_name == GlobalVariableClass.TempString || nsub.subject_id == GlobalVariableClass.TempString)
                                   select new
                                   {                                       
                                       ຊື່ = nstd.student_name,
                                       ເພດ = nstd.student_gender,
                                       ຄະແນນ = h.grade_total,
                                       ເກຣດ = h.grade_grade,
                                       ອາຈານ = h.teacher_name,
                                       ສົກປີ = h.subject_tyear,
                                       ຜູ້ເກັບຄະແນນ = h.grade_typer
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

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
        }

        private void BtnSub_Click(object sender, RoutedEventArgs e)
        {
            SubjectDialog frm = new SubjectDialog();
            frm.ShowDialog();
            textSubName.Text = "ວິຊາ:  " + GlobalVariableClass.TempString4;
        }
    }
}
