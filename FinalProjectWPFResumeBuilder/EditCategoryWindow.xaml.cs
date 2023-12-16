using FinalProjectResumeMaker;
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

namespace FinalProjectWPFResumeBuilder
{
    /// <summary>
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {
       Category category2;
        public EditCategoryWindow(Category category)
        {
            this.category2 = category;
            InitializeComponent();
            CategoryNameTextBox.Text = category.CategoryName;
            CategoryDescriptionTextBox.Text = category.CategoryDescription;
            LocationTextBox.Text = category.Location;
            YOATextBox.Text = category.YOA.ToString();



        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            category2.CategoryName = CategoryNameTextBox.Text;
            category2.CategoryDescription = CategoryDescriptionTextBox.Text;
            category2.Location = LocationTextBox.Text;
            category2.YOA = YOATextBox.Text;

            CategoryDBHandler db = CategoryDBHandler.Instance;
            db.UpdateCategory(category2);
            Close();
            UpdateDateTimeLabel();

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryDBHandler delete = CategoryDBHandler.Instance;
            delete.DeleteCategory(category2);
            Close();
            UpdateDateTimeLabel();
        }
        private void UpdateDateTimeLabel()
        {
            if (Application.Current.MainWindow is NewResumePage newResumePageWindow)
            {
                newResumePageWindow.DateTimeLabel.Content = "Last modified on: " + DateTime.Now.ToString("g");
            }
        }
    }
}
