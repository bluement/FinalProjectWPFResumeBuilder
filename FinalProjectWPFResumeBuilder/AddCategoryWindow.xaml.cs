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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
     
        public AddCategoryWindow()
        {
            
            InitializeComponent();
            
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Category newCategory = new Category();
            newCategory.CategoryName = CategoryNameTextBox.Text;
            newCategory.CategoryDescription = CategoryDescriptionTextBox.Text;
            newCategory.Location = LocationTextBox.Text;
            newCategory.YOA = YOATextBox.Text;

            CategoryDBHandler db = CategoryDBHandler.Instance;
            db.AddCategory(newCategory);
            Close();
        }
    }
}
