using SVLCmanage.DialogBox;
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
using System.Windows.Shapes;

namespace SVLCmanage
{
    /// <summary>
    /// Interaction logic for StdSearchWindow.xaml
    /// </summary>
    public partial class StdSearchWindow : Window
    {
        public StdSearchWindow()
        {
            InitializeComponent();
        }

        int currentYear = DateTime.Now.Year;
       
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
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນລະຫັດນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();             
                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                    
                    GlobalVariableClass.IndexQuery = 1;
                    texttitle.Text = "ລະຫັດ " + GlobalVariableClass.TempString;
                    var istudent = from std in db.students
                                           where std.student_id.ToString() == GlobalVariableClass.TempString
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
                    ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();
                   
                    foreach (var data in istudent.AsEnumerable())
                    {
                        dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                         data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                         data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                    }

                    dataGrid.ItemsSource = dt.DefaultView;

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
                
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນ ຊື່ ແລະ ນາມສະກຸນ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                    
                    GlobalVariableClass.IndexQuery = 2;
                    texttitle.Text = "ຊື່ ແລະ ນາມສະກຸນ " + GlobalVariableClass.TempString;
                    var istudent = from std in db.students
                                           where std.student_name == GlobalVariableClass.TempString
                                           select new
                                           {
                                               std.student_id,
                                               std.student_name,
                                               std.student_gender,
                                               std.student_birthday,                                               
                                               std.student_addressbirth,
                                               std.student_degree,
                                               std.student_yearend,
                                               std.student_info
                                           };
                    ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                    foreach (var data in istudent.AsEnumerable())
                    {
                        dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                         data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                         data.student_degree, data.student_yearend, data.student_info);
                    }

                    dataGrid.ItemsSource = dt.DefaultView;
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                StudentSearchDialog frm = new StudentSearchDialog();
                frm.label1.Text = "ປ້ອນ ຈຸດພິເສດ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                   
                    GlobalVariableClass.IndexQuery = 3;
                    texttitle.Text = "ຈຸດພິເສດ " + GlobalVariableClass.TempString;
                    var Istudent = from std in db.students
                                   where std.student_info == GlobalVariableClass.TempString
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
                    ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                    foreach (var data in Istudent.AsEnumerable())
                    {
                        dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                         data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                         data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                    }

                    dataGrid.ItemsSource = dt.DefaultView;
                  
                    textcount.Text = "ນັກສືກສາຈຳນວນ " + Istudent.Count().ToString() + " ຄົນ" ;
                                     
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
               
                StudentSearchAnotherDialog frm = new StudentSearchAnotherDialog();
                frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true  && GlobalVariableClass.TempString3 != "")
                {
                    
                    GlobalVariableClass.IndexQuery = 4;
                    texttitle.Text = "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " " + GlobalVariableClass.TempString6 + " ສົກເຂົ້າສືກສາ " + GlobalVariableClass.TempString3;
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
                    ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                    foreach (var data in Istudent.AsEnumerable())
                    {
                        dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                         data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                         data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                    }

                    dataGrid.ItemsSource = dt.DefaultView;
                  
                    textcount.Text = "ນັກສືກສາຈຳນວນ " + Istudent.Count().ToString() + " ຄົນ";
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
                if (GlobalVariableClass.LevelAuth != "1")
                {

                    DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                    String stdID = (drv["ລະຫັດນັກສືກສາ"]).ToString();
                   
                    var Istudent = from h in db.students
                                   where h.student_id.ToString() == stdID
                                   select h;

                    #region Display
                    StdEditDialog frm = new StdEditDialog();
                    GlobalVariableClass.TempString4 = Istudent.FirstOrDefault().student_id.ToString();
                    frm.txtName.Text = Istudent.FirstOrDefault().student_name;
                    frm.txtEngname.Text = Istudent.FirstOrDefault().student_engname;
                    frm.txtBplace.Text = Istudent.FirstOrDefault().student_addressbirth;
                    frm.txtBirtday.Text = Istudent.FirstOrDefault().student_birthday;

                    if ((Istudent.FirstOrDefault().student_gender) == "ຊາຍ")
                    {
                        frm.radioButtonMale.IsChecked = true;
                    }
                    else
                    {
                        frm.radioButtonFemale.IsChecked = true;
                    }

                    if (Istudent.FirstOrDefault().student_pic != null)
                    {

                        byte[] b = (Istudent.FirstOrDefault().student_pic).ToArray();
                        MemoryStream memoryStream = new MemoryStream(b);
                        var imageSource = new BitmapImage();
                        imageSource.BeginInit();
                        imageSource.StreamSource = memoryStream;
                        imageSource.EndInit();
                        frm.image.Source = imageSource;
                    }

                    frm.txtNplace.Text = Istudent.FirstOrDefault().student_address;
                    frm.txtTel.Text = Istudent.FirstOrDefault().student_tel;
                    frm.comboBoxDegree.Text = Istudent.FirstOrDefault().student_degree;
                    frm.comboBoxDepartment.Text = Istudent.FirstOrDefault().student_department;
                    frm.comboBoxMajor.Text = Istudent.FirstOrDefault().student_major;
                    frm.comboBoxTime.Text = Istudent.FirstOrDefault().student_timestudy;
                    frm.txtYear.Text = Istudent.FirstOrDefault().student_year;
                    frm.comboBoxClassroom.Text = Istudent.FirstOrDefault().student_classroom;
                    frm.txtYearend.Text = Istudent.FirstOrDefault().student_yearend;
                    frm.txtNote.Text = Istudent.FirstOrDefault().student_info;
                    frm.ShowDialog();
                    #endregion

                    if (frm.DialogResult == true)
                    {
                        #region 1
                        if (GlobalVariableClass.IndexQuery == 1)
                        {
                            var istudent = from std in db.students
                                           where std.student_id.ToString() == GlobalVariableClass.TempString
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
                            ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                            foreach (var data in istudent.AsEnumerable())
                            {
                                dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                                 data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                                 data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                            }

                            dataGrid.ItemsSource = dt.DefaultView;
                        }
                        #endregion

                        #region 2
                        else if (GlobalVariableClass.IndexQuery == 2)
                        {
                            var istudent = from std in db.students
                                           where std.student_name == GlobalVariableClass.TempString
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
                            ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                            foreach (var data in istudent.AsEnumerable())
                            {
                                dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                                 data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                                 data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                            }

                            dataGrid.ItemsSource = dt.DefaultView;
                        }
                        #endregion

                        #region 3
                        else if (GlobalVariableClass.IndexQuery == 3)
                        {
                            var Istudent2 = from std in db.students
                                           where std.student_info == GlobalVariableClass.TempString
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
                            ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                            foreach (var data in Istudent2.AsEnumerable())
                            {
                                dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                                 data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                                 data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                            }

                            dataGrid.ItemsSource = dt.DefaultView;
                            
                            textcount.Text = "ນັກສືກສາຈຳນວນ " + Istudent2.Count().ToString() + " ຄົນ";
                        }
                        #endregion

                        #region 4
                        else
                        {
                            var Istudent2 = from std in db.students
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
                            ReportDataSet.studentsearchDataTable dt = new ReportDataSet.studentsearchDataTable();

                            foreach (var data in Istudent2.AsEnumerable())
                            {
                                dt.Rows.Add(data.student_id, data.student_name, data.student_gender,
                                 data.student_birthday, currentYear - Convert.ToInt32(data.student_birthday.Split(' ')[2]), data.student_addressbirth,
                                 data.student_degree, data.student_classroom, data.student_yearend, data.student_info);
                            }

                            dataGrid.ItemsSource = dt.DefaultView;
                           
                            textcount.Text = "ນັກສືກສາຈຳນວນ " + Istudent2.Count().ToString() + " ຄົນ";
                        }
                        #endregion
                    }
                }
                else
                {
                    NotFoundDialog frm3 = new NotFoundDialog();
                    frm3.label1.Text = " ທ່ານບໍ່ສາມາດໃຊ້ສ່ວນນີ້ຂອງລະບົບ ";
                    frm3.ShowDialog();
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
