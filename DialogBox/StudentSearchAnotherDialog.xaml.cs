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

namespace SVLCmanage.DialogBox
{
    /// <summary>
    /// Interaction logic for StudentSearchAnotherDialog.xaml
    /// </summary>
    public partial class StudentSearchAnotherDialog : Window
    {
        public StudentSearchAnotherDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {          
            GlobalVariableClass.TempString2= comboBoxDegree.Text;
            GlobalVariableClass.TempString6 = comboBoxTime.Text;
            GlobalVariableClass.TempString3= txtYear1.Text;
            this.DialogResult = true;
        }

        private void txtYear1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                GlobalVariableClass.TempString2 = comboBoxDegree.Text;
                GlobalVariableClass.TempString6 = comboBoxTime.Text;
                GlobalVariableClass.TempString3 = txtYear1.Text;
                this.DialogResult = true;
            }
        }
    }
}
