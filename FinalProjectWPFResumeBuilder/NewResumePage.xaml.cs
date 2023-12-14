using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FinalProjectResumeMaker;

namespace FinalProjectWPFResumeBuilder
{
    /// <summary>
    /// Interaction logic for NewResumePage.xaml
    /// </summary>
    public partial class NewResumePage : Window
    {
        List <Category> category;
        
        public NewResumePage()
        {
            InitializeComponent();
            
        }
        private void AddData_Click(object sender, RoutedEventArgs e)
        {

        }
        private void EditData_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void DeleteData_Click(System.Object sender, RoutedEventArgs e)
        {
        }
        private void ExportToPDFButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToPDF.exportToPDF(category);
        }
    }
}
