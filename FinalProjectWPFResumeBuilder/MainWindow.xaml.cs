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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProjectWPFResumeBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResumeInformationButton_Click(object sender, RoutedEventArgs e)
        {
            string message = "You can load or create your resume. You can also convert it to pdf.";
            string title = "Resume Builder Information";
            MessageBox.Show(message, title);
        }

        /*private void LoadResumeButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage loadPage = new LoadPage();
            this.Visibility = Visibility.Hidden;
            loadPage.Show();
        }*/

        private void NewResumeButton_Click(object sender, RoutedEventArgs e)
        {
            NewResumePage newPage = new NewResumePage();
            this.Visibility = Visibility.Hidden;
            newPage.Show();
        }
    }
}
