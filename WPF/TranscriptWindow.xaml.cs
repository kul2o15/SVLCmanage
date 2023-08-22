using Microsoft.Reporting.WinForms;
using SVLCmanage.DialogBox;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for TranscriptWindow.xaml
    /// </summary>
    public partial class TranscriptWindow : Window
    {
        public TranscriptWindow()
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

        private void BtnId_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textcount.Text = "";
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນລະຫັດນັກສືກສາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                    texttitle.Text = "ລະຫັດ " + GlobalVariableClass.TempString;
                    dataGrid.ItemsSource = from std in db.students
                                           where std.student_id.ToString() == GlobalVariableClass.TempString && std.student_yearend != "ຍັງທັນບໍ່ຈົບ"
                                           select new
                                           {
                                               ລະຫັດນັກສືກສາ = std.student_id,
                                               ຊື່ = std.student_name,
                                               ເພດ = std.student_gender,
                                               ວັນເກິດ = std.student_birthday,
                                               ສະຖານທີ່ເກິດ = std.student_addressbirth,
                                               ຫຼັກສູດ = std.student_degree,
                                               ຈົບສົກ = std.student_yearend,
                                               ໝາຍເຫດ = std.student_info
                                           };
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textcount.Text = "";
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນຊື່ນັກສືກສາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                    texttitle.Text = "ຊື່ " + GlobalVariableClass.TempString;
                    dataGrid.ItemsSource = from std in db.students
                                           where std.student_name == GlobalVariableClass.TempString && std.student_yearend != "ຍັງທັນບໍ່ຈົບ"
                                           select new
                                           {
                                               ລະຫັດນັກສືກສາ = std.student_id,
                                               ຊື່ = std.student_name,
                                               ເພດ = std.student_gender,
                                               ວັນເກິດ = std.student_birthday,
                                               ສະຖານທີ່ເກິດ = std.student_addressbirth,
                                               ຫຼັກສູດ = std.student_degree,
                                               ຈົບສົກ = std.student_yearend,
                                               ໝາຍເຫດ = std.student_info
                                           };
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnOther_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textcount.Text = "";
                StudentSearchAnotherDialog frm = new StudentSearchAnotherDialog();
                frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
                {
                    texttitle.Text = "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " ສົກສືກສາ " + GlobalVariableClass.TempString3;
                    var Istudent = from std in db.students
                                   where std.student_degree == GlobalVariableClass.TempString2 && std.student_year == GlobalVariableClass.TempString3
                                   && std.student_timestudy == GlobalVariableClass.TempString6 && std.student_yearend != "ຍັງທັນບໍ່ຈົບ"
                                   select new
                                   {
                                       ລະຫັດນັກສືກສາ = std.student_id,
                                       ຊື່ = std.student_name,
                                       ເພດ = std.student_gender,
                                       ວັນເກິດ = std.student_birthday,
                                       ສະຖານທີ່ເກິດ = std.student_addressbirth,
                                       ຫຼັກສູດ = std.student_degree,
                                       ຈົບສົກ = std.student_yearend,
                                       ໝາຍເຫດ = std.student_info
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

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object celldata = dataGrid.SelectedItem;
                Type typeDemo = celldata.GetType();
                int stdID = (int)typeDemo.GetProperty("ລະຫັດນັກສືກສາ").GetValue(celldata, null);

                LanguageDialog frm2 = new LanguageDialog();
                frm2.label1.Text = "ເລືອກພາສາທີ່ຕ້ອງການ";
                frm2.ShowDialog();
                if (frm2.DialogResult == true)
                {
                    var IGrade = from h in db.grades
                                 join sub in db.subjects on h.subject_id equals sub.subject_id into newsub
                                 from nsub in newsub.DefaultIfEmpty()
                                 join std in db.students on h.student_id equals std.student_id.ToString() into newstd
                                 from nstd in newstd.DefaultIfEmpty()
                                 where h.student_id == stdID.ToString()
                                 select new
                                 {
                                     h.grade_grade,
                                     h.grade_average,
                                     nstd.student_name,
                                     nstd.student_engname,
                                     nstd.student_birthday,                             
                                     nstd.student_addressbirth,
                                     nstd.student_year,
                                     nstd.student_yearend,
                                     nsub.subject_name,
                                     nsub.subject_credit,
                                     nsub.degree12plus4,
                                     nsub.degree12plus3,
                                     nsub.degree11plus3plus3,
                                     nsub.subject_engname,
                                     nstd.student_id,
                                     AV = nsub.subject_credit * h.grade_average,
                                     nstd.student_degree,
                                     h.subject_tyear
                                 };

                    ReportDialog frm = new ReportDialog();
                    ReportDataSet.TranscriptDataTable table = new ReportDataSet.TranscriptDataTable();
                    DataTable dt = table;

                    string studentid = IGrade.FirstOrDefault().student_id.ToString();
                    string studentname = IGrade.FirstOrDefault().student_name;
                    string studentengname = IGrade.FirstOrDefault().student_engname;
                    string studentbirthday = IGrade.FirstOrDefault().student_birthday;                    
                    string studentyear = IGrade.FirstOrDefault().student_year;
                    string studentyearend = IGrade.FirstOrDefault().student_yearend;
                    string studentdegree = IGrade.FirstOrDefault().student_degree;

                    foreach (var data in IGrade.AsEnumerable())
                    {
                        table.Rows.Add(data.grade_grade, data.grade_average, data.student_addressbirth.Split(' ')[2],
                                       data.subject_name, data.subject_credit, data.degree12plus4, data.degree12plus3, data.degree11plus3plus3, data.subject_engname, data.AV,
                                       data.subject_tyear, data.student_yearend.Split('-')[1]);
                    }

                    frm.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.Transcript" + GlobalVariableClass.TempString5 + "Report.rdlc";
                    frm.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterId", studentid));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterName", studentname));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterEngname", studentengname));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterBirthday", studentbirthday));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterYear", studentyear));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterYearend", studentyearend));
                    frm.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameterDegree", studentdegree));
                    frm.reportViewer.RefreshReport();
                    frm.ShowDialog();
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

    }
}
