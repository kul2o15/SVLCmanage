using Microsoft.Reporting.WinForms;
using SVLCmanage.DialogBox;
using SVLCmanage.WPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVLCmanage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();
        int currentYear = DateTime.Now.Year;

        #region  ເມນູນັກສືກສາ

        private void NewstdMenuItem_Click(object sender, RoutedEventArgs e)
        {          
            if (GlobalVariableClass.LevelAuth == "3" || GlobalVariableClass.LevelAuth == "5")
            {
                StdInputWindow frm = new StdInputWindow();
                frm.Show();
                this.Hide();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
        }

        private void CoststdMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariableClass.LevelAuth =="4" || GlobalVariableClass.LevelAuth == "5") {
                TermCostWindow frm = new TermCostWindow();
                frm.Show();
                this.Hide();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
        }

        //private void DiplomastdMenuItem_Click(object sender, RoutedEventArgs e)
        //{
            
        //    if (GlobalVariableClass.LevelAuth != "1" && GlobalVariableClass.LevelAuth != "2")
        //    {
        //        DiaplomaWindow frm = new DiaplomaWindow();
        //        frm.Show();
        //        this.Hide();
        //    }
        //    else
        //    {
        //        NotFoundDialog frm3 = new NotFoundDialog();
        //        frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
        //        frm3.ShowDialog();
        //    }
        //}

        private void SearchstdMenuItem_Click(object sender, RoutedEventArgs e)
        {                     
                StdSearchWindow frm = new StdSearchWindow();
                frm.Show();
                this.Hide();           
        }

        private void StdSpeacialstaticMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StudentSearchAnotherDialog frm = new StudentSearchAnotherDialog();
                frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
                {

                    var Istudent = from std in db.students
                                   where std.student_degree == GlobalVariableClass.TempString2 && std.student_year == GlobalVariableClass.TempString3 && std.student_timestudy == GlobalVariableClass.TempString6
                                   select new
                                   {
                                       std.student_id,
                                       std.student_name,
                                       std.student_gender,
                                       std.student_birthday,
                                       std.student_addressbirth,
                                       std.student_degree,
                                       std.student_yearend,
                                       std.student_classroom,
                                       std.student_info
                                   };

                    if (Istudent.Count() == 0)
                    {

                        NotFoundDialog frm3 = new NotFoundDialog();
                        frm3.label1.Text = " ບໍ່ພົບນັກສືກສາ ";
                        frm3.ShowDialog();

                    }
                    else
                    {

                        ReportDialog frm2 = new ReportDialog();
                        ReportDataSet.studentsearchDataTable table = new ReportDataSet.studentsearchDataTable();
                        DataTable dt = table;

                        foreach (var data in Istudent.AsEnumerable())
                        {
                            table.Rows.Add(data.student_id, data.student_name, data.student_gender,
                             data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                             data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                        }

                        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StaticReport.rdlc";
                        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ນັກສືກສາຫຼັກສູດ " + GlobalVariableClass.TempString2 + " " + GlobalVariableClass.TempString6 + " ສົກເຂົ້າສືກສາ " + GlobalVariableClass.TempString3));                        
                        frm2.reportViewer.RefreshReport();
                        frm2.ShowDialog();
                    }

                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void StdAllstaticMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Istudent = from std in db.students
                               orderby std.student_degree ascending
                               select new
                               {
                                   std.student_id,
                                   std.student_name,
                                   std.student_gender,
                                   std.student_birthday,
                                   std.student_addressbirth,
                                   std.student_degree,
                                   std.student_yearend,
                                   std.student_classroom,
                                   std.student_info
                               };

                ReportDialog frm2 = new ReportDialog();
                ReportDataSet.studentsearchDataTable table = new ReportDataSet.studentsearchDataTable();
                DataTable dt = table;

                foreach (var data in Istudent.AsEnumerable())
                {
                    table.Rows.Add(data.student_id, data.student_name, data.student_gender,
                     data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                     data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                }

                frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StaticReport.rdlc";
                frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ນັກສືກສາທັງໝົດ"));                
                frm2.reportViewer.RefreshReport();
                frm2.ShowDialog();
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region ເມນຸເກຣດຄະແນນ

        private void ScoreGrdMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariableClass.LevelAuth != "2")
            {
                SubjectDialog frm = new SubjectDialog();
                frm.ShowDialog();

                if (frm.DialogResult == true)
                {
                    GradeWindow frm2 = new GradeWindow();
                    frm2.Show();
                    this.Hide();
                }
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
           

        }

        private void TranscriptGrdMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariableClass.LevelAuth != "2" && GlobalVariableClass.LevelAuth != "1")
            {
                TranscriptWindow frm = new TranscriptWindow();
                frm.Show();
                this.Hide();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }

        }

        private void SearchGrdMenuItem_Click(object sender, RoutedEventArgs e)
        {
            GradeSearchWindow frm3 = new GradeSearchWindow();
            frm3.Show();
            this.Hide();
        }

        #endregion

        #region ເມນູລາຍງານ
        private void StdNoteRpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນ ຈຸດພິເສດ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {

                    var Istudent = from h in db.students
                                   where h.student_info == GlobalVariableClass.TempString                                 
                                   select h;

                    if (Istudent.Count() == 0)
                    {

                        NotFoundDialog frm3 = new NotFoundDialog();
                        frm3.label1.Text = " ບໍ່ພົບນັກສືກສາ ";
                        frm3.ShowDialog();
                       

                    }
                    else
                    {

                        ReportDialog frm2 = new ReportDialog();

                        ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
                        DataTable dt = table;

                        foreach (student data in Istudent)
                        {
                            table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
                             data.student_birthday, data.student_addressbirth, data.student_address, data.student_tel,
                             data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend, data.student_info, data.student_pic.ToArray());
                        }

                        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
                        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ຈຸດພິເສດ " + GlobalVariableClass.TempString));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", ""));
                        frm2.reportViewer.RefreshReport();
                        frm2.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void StdSpeacialRpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StudentSearchAnotherDialog frm = new StudentSearchAnotherDialog();
                frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
                {
                   
                    var Istudent = from std in db.students
                                   where std.student_degree == GlobalVariableClass.TempString2 && std.student_year == GlobalVariableClass.TempString3 && std.student_timestudy == GlobalVariableClass.TempString6                                 
                                   select std;

                    if (Istudent.Count() == 0)
                    {

                        NotFoundDialog frm3 = new NotFoundDialog();
                        frm3.label1.Text = " ບໍ່ພົບນັກສືກສາ ";
                        frm3.ShowDialog();
                   
                    }
                    else
                    {

                        ReportDialog frm2 = new ReportDialog();
                        ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
                        DataTable dt = table;

                        foreach (student data in Istudent)
                        {
                            table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
                             data.student_birthday, data.student_addressbirth, data.student_address, data.student_tel,
                             data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend, data.student_info, data.student_pic.ToArray());
                        }

                        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
                        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ຫຼັກສູດ " + GlobalVariableClass.TempString2 +" "+ GlobalVariableClass.TempString6 + " ສົກເຂົ້າສືກສາ " + GlobalVariableClass.TempString3));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", ""));
                        frm2.reportViewer.RefreshReport();
                        frm2.ShowDialog();
                    }

                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void StdAllRpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {                              
                    var Istudent = from std in db.students                                   
                                   select std;

                        ReportDialog frm2 = new ReportDialog();
                        ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
                        DataTable dt = table;

                        foreach (student data in Istudent)
                        {
                            table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
                             data.student_birthday, data.student_addressbirth, data.student_address, data.student_tel,
                             data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend, data.student_info, data.student_pic.ToArray());
                        }

                        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
                        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ນັກສືກສາທັງໝົດ"));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", ""));
                        frm2.reportViewer.RefreshReport();
                        frm2.ShowDialog();                  
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void StdEndRpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນ ສົກສຳເລັດການສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {

                    var Istudent = from h in db.students
                                   where h.student_yearend == GlobalVariableClass.TempString
                                   select h;

                    if (Istudent.Count() == 0)
                    {

                        NotFoundDialog frm3 = new NotFoundDialog();
                        frm3.label1.Text = " ບໍ່ພົບນັກສືກສາ ";
                        frm3.ShowDialog();

                    }
                    else
                    {

                        ReportDialog frm2 = new ReportDialog();

                        ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
                        DataTable dt = table;

                        foreach (student data in Istudent.AsEnumerable())
                        {
                            table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
                             data.student_birthday, data.student_addressbirth, data.student_address, data.student_tel,
                             data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend.Split('-')[1], data.student_info, data.student_pic.ToArray());
                        }

                        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
                        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ທີ່ສຳເລັດການສືກສາ ໃນສົກຮຽນ " + GlobalVariableClass.TempString));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", "ປະລີນຍາຕີ"));
                        frm2.reportViewer.RefreshReport();
                        frm2.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private void end12plus4_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        StudentSearchDialog frm = new StudentSearchDialog();
        //        frm.label1.Text = "ປ້ອນ ສົກສຳເລັດການສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
        //        frm.ShowDialog();

            //        if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
            //        {

            //            var Istudent = from h in db.students
            //                           where h.student_yearend == GlobalVariableClass.TempString
            //                           select h;

            //            if (Istudent.Count() == 0)
            //            {

            //                NotFoundDialog frm3 = new NotFoundDialog();
            //                frm3.label1.Text = " ບໍ່ພົບນັກສືກສາ ";
            //                frm3.ShowDialog();

            //            }
            //            else
            //            {

            //                ReportDialog frm2 = new ReportDialog();

            //                ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
            //                DataTable dt = table;

            //                foreach (student data in Istudent.AsEnumerable())
            //                {
            //                    table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
            //                     data.student_birthday, data.student_addressbirth, data.student_address, data.student_tel,
            //                     data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend.Split('-')[1], data.student_info, data.student_pic.ToArray());
            //                }

            //                frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
            //                frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            //                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ທີ່ສຳເລັດການສືກສາ ໃນສົກຮຽນ " + GlobalVariableClass.TempString));
            //                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", "ປະລີນຍາຕີ"));                        
            //                frm2.reportViewer.RefreshReport();
            //                frm2.ShowDialog();
            //            }
            //        }
            //    }

            //    catch (Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}

            //private void end12plus3_Click(object sender, RoutedEventArgs e)
            //{
            //    try
            //    {
            //        StudentSearchDialog frm = new StudentSearchDialog();
            //        frm.label1.Text = "ປ້ອນ ສົກສຳເລັດການສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
            //        frm.ShowDialog();

            //        if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
            //        {

            //            var Istudent = from h in db.students
            //                           where h.student_yearend == GlobalVariableClass.TempString
            //                           select h;

            //            if (Istudent.Count() == 0)
            //            {

            //                NotFoundDialog frm3 = new NotFoundDialog();
            //                frm3.label1.Text = " ບໍ່ພົບນັກສືກສາ ";
            //                frm3.ShowDialog();

            //            }
            //            else
            //            {

            //                ReportDialog frm2 = new ReportDialog();

            //                ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
            //                DataTable dt = table;

            //                foreach (student data in Istudent.AsEnumerable())
            //                {
            //                    table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
            //                     data.student_birthday, data.student_addressbirth, data.student_address, data.student_tel,
            //                     data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend.Split('-')[1], data.student_info, data.student_pic.ToArray());
            //                }

            //                frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
            //                frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            //                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ທີ່ສຳເລັດການສືກສາ ໃນສົກຮຽນ " + GlobalVariableClass.TempString));
            //                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", "ຊັ້ນສູງ"));
            //                frm2.reportViewer.RefreshReport();
            //                frm2.ShowDialog();
            //            }
            //        }
            //    }

            //    catch (Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}

            //private void StdCostRpMenuItem_Click(object sender, RoutedEventArgs e)
            //{
            //    try
            //    {

            //        var Istudent = from std in db.students
            //                       join t in db.costs on std.student_id equals t.index
            //                       where (t.term1 == null || t.term1 == "")
            //                       || (t.term2 == null || t.term2 == "")
            //                       || (t.term3 == null || t.term3 == "")
            //                       || (t.term4 == null || t.term4 == "")
            //                       || (t.term5 == null || t.term5 == "")
            //                       || (t.term6 == null || t.term6 == "")
            //                       || (t.term7 == null || t.term7 == "")
            //                       || (t.term8 == null || t.term8 == "")
            //                       select std;

            //        ReportDialog frm2 = new ReportDialog();
            //        ReportDataSet.studentDataTable table = new ReportDataSet.studentDataTable();
            //        DataTable dt = table;

            //        foreach (student data in Istudent)
            //        {
            //            table.Rows.Add(data.student_id, data.student_name, data.student_engname, data.student_gender,
            //             data.student_birthday, data.student_engbirthday, data.student_addressbirth, data.student_address, data.student_tel,
            //             data.student_degree, data.student_department, data.student_major, data.student_timestudy, data.student_year, data.student_yearend, data.student_info, data.student_pic.ToArray());
            //        }

            //        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.StudentReport.rdlc";
            //        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            //        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ນັກສືກສາທີ່ຍັງບໍ່ຈາຍເທີມ"));
            //        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", ""));
            //        frm2.reportViewer.RefreshReport();
            //        frm2.ShowDialog();

            //    }

            //    catch (Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}

        private void MenuItem12plus4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Isubject = from sub in db.subjects                             
                               where sub.degree12plus4 != "ບໍ່ມີໃນຫຼັກສູດ"
                               select sub;

                ReportDialog frm2 = new ReportDialog();
                ReportDataSet.subjectDataTable table = new ReportDataSet.subjectDataTable();
                DataTable dt = table;

                foreach (subject data in Isubject)
                {
                    table.Rows.Add(data.subject_id,data.subject_name,data.subject_credit,data.subject_hour,data.degree12plus4);
                }

                frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.SubjectReport.rdlc";
                frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1","ປະລີນຍາຕີ"));
                frm2.reportViewer.RefreshReport();
                frm2.ShowDialog();
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void MenuItem11plus3plus3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Isubject = from sub in db.subjects
                               where sub.degree11plus3plus3 != "ບໍ່ມີໃນຫຼັກສູດ"
                               select sub;

                ReportDialog frm2 = new ReportDialog();
                ReportDataSet.subjectDataTable table = new ReportDataSet.subjectDataTable();
                DataTable dt = table;

                foreach (subject data in Isubject)
                {
                    table.Rows.Add(data.subject_id, data.subject_name, data.subject_credit, data.subject_hour, data.degree12plus4, data.degree11plus3plus3);
                }

                frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.SubjectReport.rdlc";            
                frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ເຊື່ອມຕໍ່"));
                frm2.reportViewer.RefreshReport();
                frm2.ShowDialog();
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void MenuItem12plus3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Isubject = from sub in db.subjects
                               where sub.degree12plus3 != "ບໍ່ມີໃນຫຼັກສູດ"
                               select sub;

                ReportDialog frm2 = new ReportDialog();
                ReportDataSet.subjectDataTable table = new ReportDataSet.subjectDataTable();
                DataTable dt = table;

                foreach (subject data in Isubject)
                {
                    table.Rows.Add(data.subject_id, data.subject_name, data.subject_credit, data.subject_hour, data.degree12plus4, data.degree11plus3plus3, data.degree12plus3);
                }

                frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.SubjectReport.rdlc";
                frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ຊັ້ນສູງ"));
                frm2.reportViewer.RefreshReport();
                frm2.ShowDialog();
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
          
        private void TableGradeRpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
                GradeStudentDialog frm = new GradeStudentDialog();
                frm.label1.Text = "ປ້ອນ ຫຼັກສູດ ສົກເຂົ້າສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
                {
                                    
                    var Istudent = from grd in db.grades
                                   join sub in db.subjects on grd.subject_id equals sub.subject_id
                                   join std in db.students on grd.student_id equals std.student_id.ToString()
                                   
                                   where std.student_degree == GlobalVariableClass.TempString2
                                   && std.student_year == GlobalVariableClass.TempString3
                                   && std.student_timestudy == GlobalVariableClass.TempString6
                                   && std.student_classroom == GlobalVariableClass.TempString7

                                   select new
                                   {
                                       std.student_name,
                                       sub.subject_name,
                                       sub.subject_credit,
                                       sub.degree12plus4,
                                       sub.degree11plus3plus3,
                                       sub.degree12plus3,
                                       grd.grade_grade,
                                       AV = sub.subject_credit * grd.grade_average
                                   };

                    if (Istudent.Count() == 0)
                    {

                        NotFoundDialog frm3 = new NotFoundDialog();
                        frm3.label1.Text = " ບໍ່ພົບຄະແນນ ";
                        frm3.ShowDialog();

                    }
                    else
                    {

                        ReportDialog frm2 = new ReportDialog();
                        ReportDataSet.TableGradeDataTable table = new ReportDataSet.TableGradeDataTable();
                        DataTable dt = table;

                        foreach (var data in Istudent)
                        {
                            table.Rows.Add(data.student_name, data.subject_name, data.subject_credit, data.degree12plus4,
                                data.degree11plus3plus3, data.degree12plus3, data.grade_grade, data.AV);

                        }

                        frm2.reportViewer.LocalReport.ReportEmbeddedResource = "SVLCmanage.Report.TableGradeReport.rdlc";
                        frm2.reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter1", "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " " + GlobalVariableClass.TempString6 + " ສົກເຂົ້າສືກສາ " + GlobalVariableClass.TempString3));
                        frm2.reportViewer.LocalReport.SetParameters(new ReportParameter("ReportParameter2", GlobalVariableClass.TempString2));
                        frm2.reportViewer.RefreshReport();
                        frm2.ShowDialog();
                    }
                                    
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region ກ່ຽວກັບລະບົບ
        private void DegreesubInsertMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariableClass.LevelAuth == "5" || GlobalVariableClass.LevelAuth == "3")
            {
                DegreeSubjectInsertDialog frm = new DegreeSubjectInsertDialog();
                frm.ShowDialog();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
                  
        }

        private void DegreesubEditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if (GlobalVariableClass.LevelAuth == "5" || GlobalVariableClass.LevelAuth == "3")
            {
                DegreeSubjectEditWindow frm = new DegreeSubjectEditWindow();
                frm.Show();
                this.Hide();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
        }

        private void ChangePassMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ChangePassDialog frm = new ChangePassDialog();
            frm.ShowDialog();           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LoginWindow frm = new LoginWindow();
            frm.Show();
            this.Hide();
        }

        private void LogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogDialog frm = new LogDialog();
            frm.ShowDialog();
        }

        private void NewUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if (GlobalVariableClass.LevelAuth == "5")
            {
                AddUserDialog frm = new AddUserDialog();
                frm.ShowDialog();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
        }

        private void DelUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
           
            if (GlobalVariableClass.LevelAuth == "5")
            {
                DelUserDialog frm = new DelUserDialog();
                frm.ShowDialog();
            }
            else
            {
                NotFoundDialog frm3 = new NotFoundDialog();
                frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                frm3.ShowDialog();
            }
        }

        #endregion

      
    }
}
