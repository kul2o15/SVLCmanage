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
    /// Interaction logic for GradeStudentDialog.xaml
    /// </summary>
    public partial class GradeStudentDialog : Window
    {
        public GradeStudentDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariableClass.TempString2 = comboBoxDegree.Text;
            GlobalVariableClass.TempString6 = comboBoxTime.Text;
            GlobalVariableClass.TempString3 = txtYear1.Text;
            GlobalVariableClass.TempString7 = comboBoxClassroom.Text;
            this.DialogResult = true;
        }
    }
}
