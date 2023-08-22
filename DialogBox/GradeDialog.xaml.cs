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
    /// Interaction logic for GradeDialog.xaml
    /// </summary>
    public partial class GradeDialog : Window
    {
        public GradeDialog()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Double GradeAv = 0;
                switch (comboBoxGrade.Text)
                {
                    case "A":
                        GradeAv = 4;
                        break;
                    case "B+":
                        GradeAv = 3.5;
                        break;
                    case "B":
                        GradeAv = 3;
                        break;
                    case "C+":
                        GradeAv = 2.5;
                        break;
                    case "C":
                        GradeAv = 2;
                        break;
                    case "D+":
                        GradeAv = 1.5;
                        break;
                    case "D":
                        GradeAv = 1;
                        break;
                    case "F":
                        GradeAv = 0;
                        break;
                    case "I":
                        GradeAv = 0;
                        break;
                    case "W":
                        GradeAv = 0;
                        break;
                }

                var newgrade = from grd in db.grades
                               where grd.student_id == GlobalVariableClass.TempString5
                               && grd.subject_id == GlobalVariableClass.TempString
                               select grd;


                if (newgrade.Count() == 0)
                {

                    grade grd = new grade();
                    grd.student_id = GlobalVariableClass.TempString5;
                    grd.subject_id = GlobalVariableClass.TempString;
                    grd.grade_total = txtTotal.Text;
                    grd.grade_grade = comboBoxGrade.Text;
                    grd.teacher_name = GlobalVariableClass.TempString2;
                    grd.grade_average = Convert.ToDecimal(GradeAv);
                    grd.subject_tyear = GlobalVariableClass.TempString3;
                    grd.grade_typer = GlobalVariableClass.UserAuth;
                    db.grades.InsertOnSubmit(grd);
                    db.SubmitChanges();
                  
                }
                else
                {


                    foreach (grade grd in newgrade)
                    {
                        grd.grade_typer = GlobalVariableClass.UserAuth;
                        grd.grade_total = txtTotal.Text;
                        grd.subject_tyear = GlobalVariableClass.TempString3;
                        grd.grade_grade = comboBoxGrade.Text;
                        grd.grade_average = Convert.ToDecimal(GradeAv);
                    }

                    db.SubmitChanges();
                                     
                }
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
                txtTotal.Focus();                        
        }
    }
}
