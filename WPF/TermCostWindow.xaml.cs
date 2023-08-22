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
    /// Interaction logic for TermCostWindow.xaml
    /// </summary>
    public partial class TermCostWindow : Window
    {
        public TermCostWindow()
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
                frm.label1.Text = "ປ້ອນລະຫັດນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";           
                frm.ShowDialog();
               
                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                    GlobalVariableClass.IndexQuery = 1;
                    texttitle.Text = "ລະຫັດ " + GlobalVariableClass.TempString;
                    DataGrid.ItemsSource = from std in db.students
                                           join t in db.costs on std.student_id equals t.index
                                           where std.student_id.ToString() == GlobalVariableClass.TempString
                                           select new
                                           {
                                               ລະຫັດນັກສືກສາ = std.student_id,
                                               ຊື່ແລະນາມສະກຸນ = std.student_name,
                                               ປີ1ເທີມ1 = t.term1,
                                               ປີ1ເທີມ2 = t.term2,
                                               ປີ2ເທີມ3 = t.term3,
                                               ປີ2ເທີມ4 = t.term4,
                                               ປີ3ເທີມ5 = t.term5,
                                               ປີ3ເທີມ6 = t.term6,
                                               ປີ4ເທີມ7 = t.term7,
                                               ປີ4ເທີມ8 = t.term8,

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
                frm.label1.Text = "ປ້ອນ ຊື່ ແລະ ນາມສະກຸນ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString != "")
                {
                    GlobalVariableClass.IndexQuery = 2;
                    texttitle.Text = "ຊື່ ແລະ ນາມສະກຸນ " + GlobalVariableClass.TempString;
                    DataGrid.ItemsSource = from std in db.students
                                           join t in db.costs on std.student_id equals t.index
                                           where std.student_name == GlobalVariableClass.TempString
                                           select new
                                           {
                                               ລະຫັດນັກສືກສາ = std.student_id,
                                               ຊື່ແລະນາມສະກຸນ = std.student_name,
                                               ປີ1ເທີມ1 = t.term1,
                                               ປີ1ເທີມ2 = t.term2,
                                               ປີ2ເທີມ3 = t.term3,
                                               ປີ2ເທີມ4 = t.term4,
                                               ປີ3ເທີມ5 = t.term5,
                                               ປີ3ເທີມ6 = t.term6,
                                               ປີ4ເທີມ7 = t.term7,
                                               ປີ4ເທີມ8 = t.term8,

                                           };
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }           
        }

        private void BtnCost_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                GlobalVariableClass.IndexQuery = 3;
                texttitle.Text = "ຍັງບໍ່ຈ່າຍຄ່າເທີມ";
                var Istudent = from std in db.students
                               join t in db.costs on std.student_id equals t.index
                               where (t.term1 == null || t.term1 == "")
                               || (t.term2 == null || t.term2 == "")
                               || (t.term3 == null || t.term3 == "")
                               || (t.term4 == null || t.term4 == "")
                               || (t.term5 == null || t.term5 == "")
                               || (t.term6 == null || t.term6 == "")
                               || (t.term7 == null || t.term7 == "")
                               || (t.term8 == null || t.term8 == "")
                               select new
                               {
                                   ລະຫັດນັກສືກສາ = std.student_id,
                                   ຊື່ແລະນາມສະກຸນ = std.student_name,
                                   ປີ1ເທີມ1 = t.term1,
                                   ປີ1ເທີມ2 = t.term2,
                                   ປີ2ເທີມ3 = t.term3,
                                   ປີ2ເທີມ4 = t.term4,
                                   ປີ3ເທີມ5 = t.term5,
                                   ປີ3ເທີມ6 = t.term6,
                                   ປີ4ເທີມ7 = t.term7,
                                   ປີ4ເທີມ8 = t.term8,
                               };

                DataGrid.ItemsSource = Istudent.ToList();
                textcount.Text = "ຈຳນວນນັກສືກສາທີ່ຍັງບໍ່ຈ່າຍຄ່າເທີມ " + Istudent.Count().ToString() + " ຄົນ";

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
                frm.label1.Text = "ເລືອກ ຫຼັກສູດ ແລະ ສົກເຂົ້າສືກສາ ນັກສືກສາທີ່ຕ້ອງການຊອກຫາ";
                frm.ShowDialog();

                if (frm.DialogResult == true && GlobalVariableClass.TempString3 != "")
                {
                    GlobalVariableClass.IndexQuery = 4;
                    texttitle.Text = "ຫຼັກສູດ " + GlobalVariableClass.TempString2 + " " + GlobalVariableClass.TempString6 + " ສົກເຂົ້າສືກສາ " + GlobalVariableClass.TempString3;
                    DataGrid.ItemsSource = from std in db.students
                                           join t in db.costs on std.student_id equals t.index
                                           where std.student_degree == GlobalVariableClass.TempString2 && std.student_year == GlobalVariableClass.TempString3 && std.student_timestudy == GlobalVariableClass.TempString6
                                           select new
                                           {
                                               ລະຫັດນັກສືກສາ = std.student_id,
                                               ຊື່ແລະນາມສະກຸນ = std.student_name,
                                               ປີ1ເທີມ1 = t.term1,
                                               ປີ1ເທີມ2 = t.term2,
                                               ປີ2ເທີມ3 = t.term3,
                                               ປີ2ເທີມ4 = t.term4,
                                               ປີ3ເທີມ5 = t.term5,
                                               ປີ3ເທີມ6 = t.term6,
                                               ປີ4ເທີມ7 = t.term7,
                                               ປີ4ເທີມ8 = t.term8,

                                           };
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
                int stdID = (int)typeDemo.GetProperty("ລະຫັດນັກສືກສາ").GetValue(celldata, null);

                var Istudent = from h in db.costs
                               where h.index == stdID
                               select h;

                TermCostDialog frm = new TermCostDialog();             
                GlobalVariableClass.TempString4 = Istudent.FirstOrDefault().index.ToString();
                frm.txtterm1.Text = Istudent.FirstOrDefault().term1;
                frm.txtterm2.Text = Istudent.FirstOrDefault().term2;
                frm.txtterm3.Text = Istudent.FirstOrDefault().term3;
                frm.txtterm4.Text = Istudent.FirstOrDefault().term4;
                frm.txtterm5.Text = Istudent.FirstOrDefault().term5;
                frm.txtterm6.Text = Istudent.FirstOrDefault().term6;
                frm.txtterm7.Text = Istudent.FirstOrDefault().term7;
                frm.txtterm8.Text = Istudent.FirstOrDefault().term8;
                frm.ShowDialog();

                if (frm.DialogResult == true)
                {
                    #region 1
                    if (GlobalVariableClass.IndexQuery == 1)
                    {
                        DataGrid.ItemsSource = from std in db.students
                                               join t in db.costs on std.student_id equals t.index
                                               where std.student_id.ToString() == GlobalVariableClass.TempString
                                               select new
                                               {
                                                   ລະຫັດນັກສືກສາ = std.student_id,
                                                   ຊື່ແລະນາມສະກຸນ = std.student_name,
                                                   ປີ1ເທີມ1 = t.term1,
                                                   ປີ1ເທີມ2 = t.term2,
                                                   ປີ2ເທີມ3 = t.term3,
                                                   ປີ2ເທີມ4 = t.term4,
                                                   ປີ3ເທີມ5 = t.term5,
                                                   ປີ3ເທີມ6 = t.term6,
                                                   ປີ4ເທີມ7 = t.term7,
                                                   ປີ4ເທີມ8 = t.term8,

                                               };
                    }
                    #endregion

                    #region 2
                    else if (GlobalVariableClass.IndexQuery == 2)
                    {
                        DataGrid.ItemsSource = from std in db.students
                                               join t in db.costs on std.student_id equals t.index
                                               where std.student_name == GlobalVariableClass.TempString
                                               select new
                                               {
                                                   ລະຫັດນັກສືກສາ = std.student_id,
                                                   ຊື່ແລະນາມສະກຸນ = std.student_name,
                                                   ປີ1ເທີມ1 = t.term1,
                                                   ປີ1ເທີມ2 = t.term2,
                                                   ປີ2ເທີມ3 = t.term3,
                                                   ປີ2ເທີມ4 = t.term4,
                                                   ປີ3ເທີມ5 = t.term5,
                                                   ປີ3ເທີມ6 = t.term6,
                                                   ປີ4ເທີມ7 = t.term7,
                                                   ປີ4ເທີມ8 = t.term8,

                                               };
                    }
                    #endregion

                    #region 3
                    else if (GlobalVariableClass.IndexQuery == 3)
                    {
                        var Istudent2 = from std in db.students
                                       join t in db.costs on std.student_id equals t.index
                                       where (t.term1 == null || t.term1 == "")
                                       || (t.term2 == null || t.term2 == "")
                                       || (t.term3 == null || t.term3 == "")
                                       || (t.term4 == null || t.term4 == "")
                                       || (t.term5 == null || t.term5 == "")
                                       || (t.term6 == null || t.term6 == "")
                                       || (t.term7 == null || t.term7 == "")
                                       || (t.term8 == null || t.term8 == "")
                                        select new
                                       {
                                           ລະຫັດນັກສືກສາ = std.student_id,
                                           ຊື່ແລະນາມສະກຸນ = std.student_name,
                                           ປີ1ເທີມ1 = t.term1,
                                           ປີ1ເທີມ2 = t.term2,
                                           ປີ2ເທີມ3 = t.term3,
                                           ປີ2ເທີມ4 = t.term4,
                                           ປີ3ເທີມ5 = t.term5,
                                           ປີ3ເທີມ6 = t.term6,
                                           ປີ4ເທີມ7 = t.term7,
                                           ປີ4ເທີມ8 = t.term8,
                                       };

                        DataGrid.ItemsSource = Istudent2.ToList();
                        textcount.Text = "ຈຳນວນນັກສືກສາທີ່ຍັງບໍ່ຈ່າຍຄ່າເທີມ " + Istudent2.Count().ToString() + " ຄົນ";
                    }
                    #endregion

                    #region 4
                    else
                    {
                        DataGrid.ItemsSource = from std in db.students
                                               join t in db.costs on std.student_id equals t.index
                                               where std.student_degree == GlobalVariableClass.TempString2 && std.student_year == GlobalVariableClass.TempString3 && std.student_timestudy == GlobalVariableClass.TempString6
                                               select new
                                               {
                                                   ລະຫັດນັກສືກສາ = std.student_id,
                                                   ຊື່ແລະນາມສະກຸນ = std.student_name,
                                                   ປີ1ເທີມ1 = t.term1,
                                                   ປີ1ເທີມ2 = t.term2,
                                                   ປີ2ເທີມ3 = t.term3,
                                                   ປີ2ເທີມ4 = t.term4,
                                                   ປີ3ເທີມ5 = t.term5,
                                                   ປີ3ເທີມ6 = t.term6,
                                                   ປີ4ເທີມ7 = t.term7,
                                                   ປີ4ເທີມ8 = t.term8,

                                               };
                    }
                    #endregion

                    textcount.Text = "";
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
