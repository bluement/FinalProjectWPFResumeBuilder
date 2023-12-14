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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResumeInformationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome to the resume builder. Click on 'New Resume' to create a new template resume.");
        }

        private void NewResumeButton_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the NewResumePage window
            NewResumePage newResumePageWindow = new NewResumePage();
            this.Visibility = Visibility.Collapsed;
            newResumePageWindow.Show();
        }
    }
}

