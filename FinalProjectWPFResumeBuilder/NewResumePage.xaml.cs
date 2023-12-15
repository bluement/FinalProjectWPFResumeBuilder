using System;
using System.Collections.Generic;
using System.Diagnostics;
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
   
    public partial class NewResumePage : Window
    {
        CategoryDBHandler db = CategoryDBHandler.Instance;
        List <Category> category;
        
        
        public NewResumePage()
        {
            InitializeComponent();
            LoadCategories();
            RefrestAllCategoriesList();

        }
        public void RefrestAllCategoriesList()
        {
            AllCategoriesDataGrid.ItemsSource = null;
            category = db.ReadAllCategory();
            AllCategoriesDataGrid.ItemsSource = category;
        }
        private void AddNewCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow();
            addCategoryWindow.ShowDialog();
            RefrestAllCategoriesList();
        }
        private void ExportToPDFButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToPDF.exportToPDF(category);
        }
        private void LoadCategories()
        {
            var allCategories = db.ReadAllCategory();
            AllCategoriesDataGrid.ItemsSource = allCategories.ToList();
            /*try
            {
                var allCategories = db.ReadAllCategory();
                if (allCategories == null || !allCategories.Any())
                {
                    Debug.WriteLine("No categories found or ReadAllCategory returned null.");
                    return;
                }

                AllCategoriesDataGrid.ItemsSource = allCategories.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading categories: {ex.Message}");
            }*/
        }
        private void AllCategoriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllCategoriesDataGrid.SelectedItem is Category selectedCategory)
            {
                var editWindow = new EditCategoryWindow(selectedCategory);
                if(editWindow.ShowDialog() == true)
                {
                    LoadCategories();
                }
            }

        }
    }
}
