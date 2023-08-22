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
    /// Interaction logic for SubjectDialog.xaml
    /// </summary>
    public partial class SubjectDialog : Window
    {
        public SubjectDialog()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();

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
                
                GlobalVariableClass.TempString = (Isubject.FirstOrDefault().subject_id);
                GlobalVariableClass.TempString4 = (Isubject.FirstOrDefault().subject_name);
                this.DialogResult = true;
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGrid.ItemsSource = from h in db.subjects
                                       where h.degree12plus4 != "ບໍ່ມີໃນຫຼັກສູດ" || h.degree11plus3plus3 != "ບໍ່ມີໃນຫຼັກສູດ"
                                       || h.degree12plus3 != "ບໍ່ມີໃນຫຼັກສູດ"
                                       select new
                                       {
                                           ລະຫັດວິຊາ = h.subject_id,
                                           ຊື່ວິຊາ = h.subject_name
                                       };
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show((string)("ເກິດຂໍ້ຜິດພາດ ເນື່ອງຈາກ " + ex.Message), "ຜົນການທຳງານ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
