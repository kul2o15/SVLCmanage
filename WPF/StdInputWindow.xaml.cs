using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SVLCmanage.DialogBox;

namespace SVLCmanage
{
    /// <summary>
    /// Interaction logic for StdInputWindow.xaml
    /// </summary>
    public partial class StdInputWindow : Window
    {
        public StdInputWindow()
        {
            InitializeComponent();           
        }

        string EngMonth;
        Bitmap CBMP;
        string NameTitle,ENameTitle;
        SouvilayDataClassesDataContext  db = new SouvilayDataClassesDataContext();        

        private void CleanData()
        {            
            txtName.Text = "";
            txtEngname.Text = "";
            txtBvillage.Text = "";
            txtBdistric.Text = "";
            comboBoxBProvince.SelectedIndex = 0;
            comboBoxBDay.SelectedIndex = 0;
            comboBoxBMonth.SelectedIndex = 0;
            txtBYear.Text = "";
            radioButtonFemale.IsChecked = true;
            txtNvillage.Text = "";
            txtNdistric.Text = "";
            comboBoxNProvince.SelectedIndex = 0;
            txtTel1.Text = "";
            txtTel2.Text = "";
            comboBoxDegree.SelectedIndex = 0;
            comboBoxDepartment.SelectedIndex = 0;
            comboBoxMajor.SelectedIndex = 0;
            comboBoxTime.SelectedIndex = 0;
            txtYear1.Text = "";
            txtYear2.Text = "";
            txtNote.Text = "";
            image.Source = new BitmapImage(new Uri("/SVLCmanage;component/Image/DefaultImage.jpg", UriKind.Relative));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Hide();
        }
     
        private void BtnInsertPic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OpenDlg = new OpenFileDialog();
                OpenDlg.Title = "ເລືອກໄຟລຮູບພາບ";
                OpenDlg.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp";      
                OpenDlg.Multiselect = false;
                OpenDlg.FilterIndex = 0;
                if (OpenDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string picFileName = "";
                    picFileName = OpenDlg.FileName;
                    CBMP = new Bitmap(picFileName);
                    if (CBMP != null)
                    {
                        if (CBMP.Height < 185 || CBMP.Width < 135)
                        {

                            NotFoundDialog frm = new NotFoundDialog();
                            frm.label1.Text = " ໄຟລຮູບພາບທີ່ໃຊ້ ຕ້ອງມີຂະໜາດໃຫຍ່ກວ່າ 135 x 185 pixel ";
                            frm.ShowDialog();
                            return;
                        }
                        image.Source = new BitmapImage(new Uri(OpenDlg.FileName));
                    }
                    
                   
                }
            
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ໄຟລຮູບພາບທີ່ເລືອກ ບໍ່ສາມາດໃຊ້ງານໄດ້ ເນື່ອງຈາກ" + ex.Message, "ຂໍ້ຜິດພາດ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelPic_Click(object sender, RoutedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri("/SVLCmanage;component/Image/DefaultImage.jpg", UriKind.Relative));
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {          
           txtName.Focus();                
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //#region EngMont
                //switch (comboBoxBMonth.Text)
                //{
                //    case "ບໍ່ຮູ້":
                //        EngMonth = "Unkown";
                //        break;
                //    case "ມັງກອນ":
                //        EngMonth = "January";
                //        break;
                //    case "ກຸມພາ":
                //        EngMonth = "February";
                //        break;
                //    case "ມີນາ":
                //        EngMonth = "March";
                //        break;
                //    case "ເມສາ":
                //        EngMonth = "April";
                //        break;
                //    case "ພຶດສະພາ":
                //        EngMonth = "May";
                //        break;
                //    case "ມິຖຸນາ":
                //        EngMonth = "June";
                //        break;
                //    case "ກໍລະກົດ":
                //        EngMonth = "July";
                //        break;
                //    case "ສິງຫາ":
                //        EngMonth = "August";
                //        break;
                //    case "ກັນຍາ":
                //        EngMonth = "September";
                //        break;
                //    case "ຕຸລາ":
                //        EngMonth = "October";
                //        break;
                //    case "ພະຈິກ":
                //        EngMonth = "November";
                //        break;
                //    case "ທັນວາ":
                //        EngMonth = "December";
                //        break;                    
                //}
                //# endregion
              
                student std = new student();
                cost tc = new cost();               
                byte[] buffer;
                var bitmap = image.Source as BitmapSource;
                var encoder = new PngBitmapEncoder();

                if (radioButtonMale.IsChecked == true)
                {
                    std.student_gender = radioButtonMale.Content.ToString();
                    NameTitle = "ທ້າວ";
                    ENameTitle = "Mr.";
                }
                else
                {
                    std.student_gender = radioButtonFemale.Content.ToString();
                    NameTitle = "ນາງ";
                    ENameTitle = "Miss";
                }

                tc.student_name = NameTitle + " " + txtName.Text;
                std.student_name = NameTitle + " " + txtName.Text;
                std.student_engname = ENameTitle+" "+txtEngname.Text;
                std.student_addressbirth = "ບ້ານ"+txtBvillage.Text.Trim() +" ເມືອງ"+ txtBdistric.Text.Trim() + " ແຂວງ" + comboBoxBProvince.Text;              
                std.student_address = "ບ້ານ"+ txtNvillage.Text + " ເມືອງ" + txtNdistric.Text + " ແຂວງ" + comboBoxNProvince.Text;
                std.student_tel = txtTel1.Text +"  "+ txtTel2.Text;
                std.student_birthday = comboBoxBDay.Text + " " + comboBoxBMonth.Text + " " + txtBYear.Text;
                //std.student_engbirthday = comboBoxBDay.Text + " " + EngMonth + " " + txtBYear.Text;                              
                std.student_degree = comboBoxDegree.Text;
                std.student_department = comboBoxDepartment.Text;
                std.student_major = comboBoxMajor.Text;
                std.student_year = txtYear1.Text+"-"+txtYear2.Text;
                std.student_timestudy = comboBoxTime.Text;

                if (txtYear1end.Text =="" || txtYear2end.Text =="")
                {
                    std.student_yearend = "ຍັງບໍ່ຈົບ";
                }
                else
                {
                    std.student_yearend = txtYear1end.Text+"-"+txtYear2end.Text;
                }
               
                std.student_info = txtNote.Text;

                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    buffer = stream.ToArray();
                }
                std.student_pic = buffer;

                db.students.InsertOnSubmit(std);
                db.costs.InsertOnSubmit(tc);
                db.SubmitChanges();

                SuccesfullDialog frm2 = new SuccesfullDialog();               
                frm2.ShowDialog();
        
                CleanData();

                dataGrid.ItemsSource = (from h in db.students
                                        orderby h.student_id descending
                                        select new { ລະຫັດ = h.student_id, ຊື່ = h.student_name }).Take(10);

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
           
            try
            {
                txtName.Focus();
                dataGrid.ItemsSource = (from h in db.students
                                        orderby h.student_id descending
                                       select new { ລະຫັດ=h.student_id,ຊື່=h.student_name }).Take(10);
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
