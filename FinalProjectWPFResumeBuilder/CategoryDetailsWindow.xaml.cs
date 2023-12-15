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
    /// Interaction logic for CategoryDetailsWindow.xaml
    /// </summary>
    public partial class CategoryDetailsWindow : Window
    {
        Category category2;
        public CategoryDetailsWindow(Category category)
        {
            InitializeComponent();
            this.category2= category;

            //display the user
            CategoryNameTextBox.Text = category.CategoryName;
            DescriptionTextBox.Text = category.CategoryDescription;
            LocationTextBox.Text = category.Location;
            YOATextBox.Text = category.YOA.ToString();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            EditCategoryWindow updatecategory = new EditCategoryWindow(category2);
            updatecategory.ShowDialog();
            Close();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryDBHandler delete = CategoryDBHandler.Instance;
            delete.DeleteCategory(category2);
            Close();

        }
    }
}
