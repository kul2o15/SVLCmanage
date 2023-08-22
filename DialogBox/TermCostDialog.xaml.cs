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
    /// Interaction logic for TermCostDialog.xaml
    /// </summary>
    public partial class TermCostDialog : Window
    {
        public TermCostDialog()
        {
            InitializeComponent();
        }

        SouvilayDataClassesDataContext db = new SouvilayDataClassesDataContext();
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var TermCost = from tct in db.costs

                               where tct.index.ToString() == GlobalVariableClass.TempString4

                               select tct;
             
                    foreach (cost tct in TermCost)
                    {

                        tct.term1 = txtterm1.Text;
                        tct.term2 = txtterm2.Text;
                        tct.term3 = txtterm3.Text;
                        tct.term4 = txtterm4.Text;
                        tct.term5 = txtterm5.Text;
                        tct.term6 = txtterm6.Text;
                        tct.term7 = txtterm7.Text;
                        tct.term8 = txtterm8.Text;
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
