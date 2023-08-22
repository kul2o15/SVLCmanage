using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace SVLCmanage.DialogBox
{
    /// <summary>
    /// Interaction logic for StdEditDialog.xaml
    /// </summary>
    public partial class StdEditDialog : Window
    {
        public StdEditDialog()
        {
            InitializeComponent();
        }
   
        string picFileName = "";
        Bitmap CBMP;       
        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();
        
        private void BtnInsertPic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OpenDlg = new OpenFileDialog();
                OpenDlg.Title = "ເລືອກໄຟລຮູບພາບ";
                OpenDlg.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp";
                OpenDlg.FileName = "";
                picFileName = "";
                OpenDlg.Multiselect = false;
                OpenDlg.FilterIndex = 0;
                if (OpenDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
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
            
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                byte[] CurrentImage;
                var newstd = from std in db.students
                             where std.student_id.ToString() == GlobalVariableClass.TempString4
                             select std;
             
                foreach (student std in newstd)
                {

                    std.student_name = txtName.Text;
                    std.student_engname = txtEngname.Text;
                    std.student_addressbirth = txtBplace.Text;
                    std.student_address = txtNplace.Text;
                    std.student_tel = txtTel.Text;
                    std.student_birthday = txtBirtday.Text;                   

                    if (radioButtonMale.IsChecked == true)
                    {
                        std.student_gender = radioButtonMale.Content.ToString();
                    }
                    else
                    {
                        std.student_gender = radioButtonFemale.Content.ToString();
                    }

                    std.student_degree = comboBoxDegree.Text;
                    std.student_department = comboBoxDepartment.Text;
                    std.student_major = comboBoxMajor.Text;
                    std.student_year = txtYear.Text;
                    std.student_yearend = txtYearend.Text;
                    std.student_timestudy = comboBoxTime.Text;
                    std.student_info = txtNote.Text;
                    std.student_classroom = comboBoxClassroom.Text;

                    if (image.Source != null)
                    {

                        if (picFileName != "")
                        {
                            var fs = new FileStream(picFileName, FileMode.Open, FileAccess.Read);
                            CurrentImage = new byte[Convert.ToInt32(fs.Length) + 1];
                            fs.Read(CurrentImage, 0, Convert.ToInt32(fs.Length - 1));
                            fs.Close();
                            std.student_pic = CurrentImage;
                        }

                    }
                                    
                }
               
                db.SubmitChanges();
                this.DialogResult = true;

            }


            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }
    }
}
