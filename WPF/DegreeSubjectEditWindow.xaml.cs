using SVLCmanage.DialogBox;
using System;
using System.Collections;
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

namespace SVLCmanage
{
    /// <summary>
    /// Interaction logic for DegreeSubjectEditWindow.xaml
    /// </summary>
    public partial class DegreeSubjectEditWindow : Window
    {
        public DegreeSubjectEditWindow()
        {
            InitializeComponent();
        }

        String StrIdSub;      
        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

        private void CleanData()
        {
            StrIdSub= "";
            txtSubId.Text = "";
            txtSubName.Text = "";
            txtSubEngName.Text = "";
            txtSubCredit.Text = "";
            txtSubHour.Text = "";
            comboBoxDegree11plus3plus3.SelectedIndex = -1;
            comboBoxDegree12plus3.SelectedIndex = -1;
            comboBoxDegree12plus4.SelectedIndex = -1;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Hide();
        }

        private void txtSubId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {

                if (txtSubId.Text.Trim() == "")
                {
                    return;
                }
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    var Isubject = from h in db.subjects                               
                                   where h.subject_id == txtSubId.Text
                                   select h;
                    DataContext = Isubject.ToList();


                    if (Isubject.Count() == 0)
                    {

                        NotFoundDialog frm = new NotFoundDialog();
                        frm.label1.Text = " ບໍ່ພົບວິຊານີ້ ";
                        frm.ShowDialog();
                        CleanData();
                    }
                    else
                    {
                        StrIdSub = (Isubject.FirstOrDefault().subject_id);                      
                        txtSubCredit.Text = (Isubject.FirstOrDefault().subject_credit).ToString();                       
                    }

                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtSubName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {

                if (txtSubName.Text.Trim() == "")
                {
                    return;
                }
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    var Isubject = from h in db.subjects                                  
                                   where (h.subject_name.Contains(txtSubName.Text))
                                   select h;
                    DataContext = Isubject.ToList();


                    if (Isubject.Count() == 0)
                    {
                        NotFoundDialog frm = new NotFoundDialog();
                        frm.label1.Text = " ບໍ່ພົບວິຊານີ້ ";
                        frm.ShowDialog();                       
                        CleanData();
                    }
                    else if (Isubject.Count() == 1)
                    {
                        StrIdSub = (Isubject.FirstOrDefault().subject_id);                      
                        txtSubCredit.Text = (Isubject.FirstOrDefault().subject_credit).ToString();                       
                    }

                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var Newsub = from sub in db.subjects

                               where sub.subject_id == StrIdSub

                               select sub;


                if (Newsub.Count() == 0)
                {
                    NotFoundDialog frm = new NotFoundDialog();
                    frm.label1.Text = " ບໍ່ພົບວິຊານີ້ ";
                    frm.ShowDialog();                  
                    CleanData();
                    return;

                }

                else
                {

                    foreach (subject sub in Newsub)
                    {

                        sub.subject_id = StrIdSub;
                        sub.subject_name = txtSubName.Text;
                        sub.subject_engname = txtSubEngName.Text;
                        sub.subject_credit = Convert.ToInt32(txtSubCredit.Text);
                        sub.subject_hour = txtSubHour.Text;
                        sub.degree11plus3plus3 = comboBoxDegree11plus3plus3.Text;
                        sub.degree12plus3 = comboBoxDegree12plus3.Text;
                        sub.degree12plus4 = comboBoxDegree12plus4.Text;

                    }

                    db.SubmitChanges();

                    SuccesfullDialog frm2 = new SuccesfullDialog();
                    frm2.ShowDialog();

                    CleanData();
                }


            }


            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
             
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object celldata = DataGrid.SelectedItem;
            Type typeDemo = celldata.GetType();
            string subID = (string)typeDemo.GetProperty("ລະຫັດວິຊາ").GetValue(celldata, null);

            var Isubject = from h in db.subjects
                           where h.subject_id == subID
                           select h;
            DataContext = Isubject.ToList();

            StrIdSub = (Isubject.FirstOrDefault().subject_id);      
            txtSubCredit.Text = (Isubject.FirstOrDefault().subject_credit).ToString();
            comboBoxDegree11plus3plus3.Text = (Isubject.FirstOrDefault().degree11plus3plus3);
            comboBoxDegree12plus3.Text = (Isubject.FirstOrDefault().degree12plus3);
            comboBoxDegree12plus4.Text = (Isubject.FirstOrDefault().degree12plus4);
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void comboBoxDegree_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                if (comboBoxDegree.Text == "ປະລິນຍາຕີ")
                {
                    DataGrid.ItemsSource = from h in db.subjects
                                   where h.degree12plus4 != "ບໍ່ມີໃນຫຼັກສູດ"
                                   select new
                                   {
                                       ລະຫັດວິຊາ = h.subject_id,
                                       ຊື່ວິຊາ = h.subject_name
                                   };                   
                }
                else if (comboBoxDegree.Text == "ເຊື່ອມຕໍ່")
                {
                    DataGrid.ItemsSource = from h in db.subjects
                                   where h.degree11plus3plus3 != "ບໍ່ມີໃນຫຼັກສູດ"
                                           select new
                                   {
                                       ລະຫັດວິຊາ = h.subject_id,
                                       ຊື່ວິຊາ = h.subject_name
                                   };                  
                }
                else if (comboBoxDegree.Text == "ຊັ້ນສູງ")
                {
                    DataGrid.ItemsSource = from h in db.subjects
                                   where h.degree12plus3 != "ບໍ່ມີໃນຫຼັກສູດ"
                                           select new
                                   {
                                       ລະຫັດວິຊາ = h.subject_id,
                                       ຊື່ວິຊາ = h.subject_name
                                   };                 
                }
                else
                {
                    DataGrid.ItemsSource = from h in db.subjects
                                   where h.degree12plus3 == "ບໍ່ມີໃນຫຼັກສູດ" 
                                   && h.degree11plus3plus3 == "ບໍ່ມີໃນຫຼັກສູດ" 
                                   && h.degree12plus4 == "ບໍ່ມີໃນຫຼັກສູດ" 
                                   select new
                                   {
                                       ລະຫັດວິຊາ = h.subject_id,
                                       ຊື່ວິຊາ = h.subject_name
                                   };
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
