using System;
using System.Collections.Generic;
using System.Configuration;

using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectResumeMaker
{
    class CategoryDBHandler
    {
        static readonly string conString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        static readonly CategoryDBHandler instance = new CategoryDBHandler();
        //testing


        private CategoryDBHandler()
        {
            CreateTable();

            Category newCat1 = new Category
            {
                CategoryName = "Education",
                CategoryDescription = "Masters Degree",
                Location = "Madrid",
                YOA = "2014"
            };
            Category newCat2 = new Category
            {
                CategoryName = "Certification",
                CategoryDescription = "ComptiA+",
                Location = "Madrid",
                YOA = "2014"
            };


            Category newCat3 = new Category
            {
                CategoryName = "Internship",
                CategoryDescription = "Google",
                Location = "Madrid",
                YOA = "2014"
            };


            Category newCat4 = new Category
            {
                CategoryName = "Job",
                CategoryDescription = "New Grad google",
                Location = "Madrid",
                YOA = "2015"
            };

            AddCategory(newCat1);
            AddCategory(newCat2);
            AddCategory(newCat3);
            AddCategory(newCat4);
        }

        public static CategoryDBHandler Instance
        {
            get { return instance; }
        }

        public void CreateTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string drop = "drop table if exists CATEGORIES;";
                SQLiteCommand command1 = new SQLiteCommand(drop, con);
                command1.ExecuteNonQuery();

                string table = "Create table CATEGORIES (ID integer primary key," +
                    "CategoryName text, " +
                    "CategoryDescription text, Location text, YOA text);";


                SQLiteCommand command2 = new SQLiteCommand(table, con);
                command2.ExecuteNonQuery();



            }
        }

        public int AddCategory(Category category)
        {
            int rows = 0;
            int newId = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                //create a parameterized query
                string query = "INSERT INTO CATEGORIES (CategoryName, CategoryDescription, Location, YOA) VALUES(@CategoryName, " +
                                "@CategoryDescritpion, @Location, @YOA)";

                SQLiteCommand insertcom = new SQLiteCommand(query, con);

                //pass values to the querry parameters
                insertcom.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                insertcom.Parameters.AddWithValue("@CategoryDescription", category.CategoryDescription);
                insertcom.Parameters.AddWithValue("@Location", category.Location);
                insertcom.Parameters.AddWithValue("@YOA", category.YOA);

                try
                {
                    rows = insertcom.ExecuteNonQuery();
                    //let get the rowid inserted
                    insertcom.CommandText = " select last_insert_rowid()";
                    Int64 LastRowID64 = Convert.ToInt64(insertcom.ExecuteScalar());
                    //grab the bottom 32-bits as the unique id of the row
                    newId = Convert.ToInt32(LastRowID64);
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("error generated. Details: " + e.ToString());
                }
            }
            return newId;
        }

        public Category GetCategory(int id)
        {
            Category category = new Category();

            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                SQLiteCommand getcom = new SQLiteCommand("Select * from Categories " +
                    "WHERE Id= @Id", con);
                getcom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getcom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            category.Id = id2;
                        }
                        category.CategoryName = reader["CategoryName"].ToString();
                        // Fix the typo in the next line from "CategoryDescritpion" to "CategoryDescription"
                        category.CategoryDescription = reader["CategoryDescription"].ToString();
                        category.Location = reader["Location"].ToString();
                        category.YOA = reader["YOA"].ToString();
                    }
                }
            }
            return category;
        }

        public int UpdateCategory(Category category)
        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string query = "UPDATE CATEGORIES SET CategoryName= @CategoryName, Location= @Location," +
                    "YOA = @YOA WHERE Id = @Id";

                SQLiteCommand updatecom = new SQLiteCommand(query, con);
                updatecom.Parameters.AddWithValue("@Id", category.Id);
                updatecom.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                // Fix the typo in the next line from "CategoryDescritpion" to "CategoryDescription"
                updatecom.Parameters.AddWithValue("@CategoryDescription", category.CategoryDescription);
                updatecom.Parameters.AddWithValue("@Location", category.Location);
                updatecom.Parameters.AddWithValue("@YOA", category.YOA);

                try
                {
                    row = updatecom.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("error generated. Details: " + e.ToString());
                }
            }
            return row;
        }


        public int DeleteCategory(Category category)
        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string query = "DELETE FROM CATEGORIES WHERE id= @Id";
                SQLiteCommand deletecom = new SQLiteCommand(query, con);
                deletecom.Parameters.AddWithValue("@Id", category.Id);
                try
                {
                    row = deletecom.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("Error geenrated detials:" + e.ToString());
                }
                return row;
            }

        }

        public List<Category> ReadAllCategory()
        {
            List<Category> listCategories = new List<Category>();
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("Select * from CATEGORIES", con);
                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create a Person Object
                        Category category = new Category();
                        if (Int32.TryParse(reader["Id"].ToString(), out int id))
                        {
                            category.Id = id;
                        }
                        category.CategoryName = reader["CategoryName"].ToString();
                        category.CategoryDescription = reader["CategoryDescription"].ToString();
                        category.Location = reader["Location"].ToString();
                        category.YOA = reader["YOA"].ToString();

                        listCategories.Add(category);

                    }
                }
            }
            return listCategories;
        }
    }
}

