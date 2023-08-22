using Microsoft.Reporting.WinForms;
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
    /// Interaction logic for DiaplomaWindow.xaml
    /// </summary>
    public partial class DiaplomaWindow : Window
    {
        public DiaplomaWindow()
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

        //private void BtnId_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        textcount.Text = "";
        //        StudentSearchDialog frm = new StudentSearchDialog();
        //        frm.label1.Text = "ປ້ອນລະຫັດນັກສືກສາ";
        //        frm.ShowDialog();

        //        if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
        //        {
        //            texttitle.Text = "ລະຫັດ " + GlobalVariableClass.TempString;
        //            dataGrid.ItemsSource = from std in db.students
        //                                   where std.student_id.ToString() == GlobalVariableClass.TempString && std.student_yearend != "ຍັງທັນບໍ່ຈົບ"
        //                                   select new
        //                                   {
        //                                       ລະຫັດນັກສືກສາ = std.student_id,
        //                                       ຊື່ = std.student_name,
        //                                       ເພດ = std.student_gender,
        //                                       ວັນເກິດ = std.student_birthday,
        //                                       ສະຖານທີ່ເກິດ = std.student_addressbirth,
        //                                       ຫຼັກສູດ = std.student_degree,                                             
        //                                       ສົກຈົບ = std.student_yearend,
        //                                       ໝາຍເຫດ = std.student_info
        //                                   };
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        //private void BtnName_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        textcount.Text = "";
        //        StudentSearchDialog frm = new StudentSearchDialog();
        //        frm.label1.Text = "ປ້ອນຊື່ນັກສືກສາ";
        //        frm.ShowDialog();

        //        if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
        //        {
        //            texttitle.Text = "ຊື່ " + GlobalVariableClass.TempString;
        //            dataGrid.ItemsSource = from std in db.students
        //                                   where std.student_name == GlobalVariableClass.TempString && std.student_yearend != "ຍັງທັນບໍ່ຈົບ"
        //                                   select new
        //                                   {
        //                                       ລະຫັດນັກສືກສາ = std.student_id,
        //                                       ຊື່ = std.student_name,
        //                                       ເພດ = std.student_gender,
        //                                       ວັນເກິດ = std.student_birthday,
        //                                       ສະຖານທີ່ເກິດ = std.student_addressbirth,
        //                                       ຫຼັກສູດ = std.student_degree,                                            
        //                                       ສົກຈົບ = std.student_yearend,
        //                                       ໝາຍເຫດ = std.student_info
        //                                   };
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        //private void BtnOther_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        textcount.Text = "";
        //        StudentSearchAnotherDialog frm = new StudentSearchAnotherDialog();
        //        frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ";
        //        frm.ShowDialog();

        //        if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
        //        {
        //            texttitle.Text = "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " "+ GlobalVariableClass.TempString6 + " ສົກເຂົ້າສືກສາ " + GlobalVariableClass.TempString3;
        //            var Istudent = from std in db.students
        //                           where std.student_degree == GlobalVariableClass.TempString2 && std.student_year == GlobalVariableClass.TempString3 && std.student_timestudy == GlobalVariableClass.TempString6 && std.student_yearend != "ຍັງທັນບໍ່ຈົບ"
        //                           select new
        //                           {
        //                               ລະຫັດນັກສືກສາ = std.student_id,
        //                               ຊື່ = std.student_name,
        //                               ເພດ = std.student_gender,
        //                               ວັນເກິດ = std.student_birthday,
        //                               ສະຖານທີ່ເກິດ = std.student_addressbirth,
        //                               ຫຼັກສູດ = std.student_degree,                                     
        //                               ສົກຈົບ = std.student_yearend,
        //                               ໝາຍເຫດ = std.student_info
        //                           };
        //            dataGrid.ItemsSource = Istudent;
        //            textcount.Text = "ຈຳນວນ " + Istudent.Count().ToString() + " ຄົນ";
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        //private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        object celldata = dataGrid.SelectedItem;
        //        Type typeDemo = celldata.GetType();
        //        int stdID = (int)typeDemo.GetProperty("ລະຫັດນັກສືກສາ").GetValue(celldata, null);
              
        //        LanguageDialog frm2 = new LanguageDialog();
        //        frm2.label1.Text = "ເລືອກພາສາທີ່ຕ້ອງການ";
        //        frm2.ShowDialog();
        //        if (frm2.DialogResult == true)
        //        {
        //            var Istudent = from h in db.students
        //                           where h.student_id == stdID
        //                           select h;

        //            ReportDialog frm = new ReportDialog();
        //            ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
        //            System.Data.DataTable dt = table;
                  
        //            foreach (student data in Istudent.AsEnumerable())
        //            {
        //                table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
        //                             data.student_birthday, data.student_engbirthday, data.student_addressbirth.Split(' ')[2], data.student_address, data.student_tel,
        //                             data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend.Split('-')[1], data.student_info, data.student_pic.ToArray());
        //            }
                    
        //            frm.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.Diaploma" + GlobalVariableClass.TempString5 + "Report.rdlc";
        //            frm.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));                
        //            frm.reportViewer.RefreshReport();
        //            frm.ShowDialog();
        //        }
               
        //    }

        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }

        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
        }
     
    }
}
