using FinalProjectResumeMaker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectWPFResumeBuilder
{
    class ProfileDBHandler
    {
        static readonly string conString = ConfigurationManager.ConnectionStrings["ProfileDataBase"].ConnectionString;
        static readonly ProfileDBHandler instance = new ProfileDBHandler();
        //testing


        private ProfileDBHandler()
        {
            CreateTable();

            Profile newp1 = new Profile
            {
                FirstName = "Jude ",
                LastName = "Bellingham",
                City = "Madrid",
                Age = 20,
                Address = "45 almond road",
                Phone = "3647891029",
                Email = "Jude@Email"
            };
            Profile newp2 = new Profile
            {
                FirstName = "Cristiano",
                LastName = "Ronaldo",
                City = "Lisbon",
                Age = 38,
                Address = "467 buyers road",
                Phone = "3642334061",
                Email = "Christiano@Email"
            };


            Profile newp3 = new Profile
            {
                FirstName = "Masonount",
                LastName = "MountMason",
                City = "Manchester",
                Age = 24,
                Address = "129 wooden road",
                Phone = "9728401295",
                Email = "Masonount@Email"
            };


            Profile newp4 = new Profile
            {
                FirstName = "Vinicius ",
                LastName = "Junior",
                City = "Madrid",
                Age = 22,
                Address = "782 rocked road",
                Phone = "3649716740",
                Email = "Vinicius@Email"
            };

            AddProfile(newp1);
            AddProfile(newp2);
            AddProfile(newp3);
            AddProfile(newp4);
        }

        public static ProfileDBHandler Instance
        {
            get { return instance; }
        }

        public void CreateTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string drop = "drop table if exists PROFILES;";
                SQLiteCommand command1 = new SQLiteCommand(drop, con);
                command1.ExecuteNonQuery();

                string table = "Create table PROFILES (ID integer primary key," +
                    "FirstName text, " +
                    "LastName text, City text, Age integer, Address text, Phone text, Email text);";


                SQLiteCommand command2 = new SQLiteCommand(table, con);
                command2.ExecuteNonQuery();



            }
        }

        public int AddProfile(Profile profile)
        {
            int rows = 0;
            int newId = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                //create a parameterized query
                string query = "INSERT INTO PROFILES (FirstName, LastName, City, Age, Address, Phone, Email) VALUES(@FirstName, " +
                                "@LastName, @City, @Age, @Address, @Phone, @Email)";

                SQLiteCommand insertcom = new SQLiteCommand(query, con);

                //pass values to the querry parameters
                insertcom.Parameters.AddWithValue("@FirstName", profile.FirstName);
                insertcom.Parameters.AddWithValue("@LastName", profile.LastName);
                insertcom.Parameters.AddWithValue("@City", profile.City);
                insertcom.Parameters.AddWithValue("@Age", profile.Age);
                insertcom.Parameters.AddWithValue("@Address", profile.Address);
                insertcom.Parameters.AddWithValue("@Phone", profile.Phone);
                insertcom.Parameters.AddWithValue("@Email", profile.Email);

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

        public Profile GetProfile(int id)
        {
            Profile profile = new Profile();

            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                SQLiteCommand getcom = new SQLiteCommand("Select * from Profiles " +
                    "WHERE Id= @Id", con);
                getcom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getcom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            profile.Id = id2;
                        }
                        profile.FirstName = reader["FirstName"].ToString();
                        profile.LastName = reader["LastName"].ToString();
                        profile.City = reader["City"].ToString();
                        profile.Address = reader["Address"].ToString();
                        profile.Phone = reader["Phone"].ToString();
                        profile.Email = reader["Email"].ToString();


                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            profile.Age = age;
                        }
                    }
                }
            }
            return profile;
        }

        public int UpdateProfile(Profile profile)

        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {

                con.Open();
                string query = "UPDATE PROFILES SET FirstName= @FirstName, LastName= @LastName," +
                    "City = @City, Age= @Age WHERE Id= @Id";

                SQLiteCommand updatecom = new SQLiteCommand(query, con);
                updatecom.Parameters.AddWithValue("@Id", profile.Id);
                updatecom.Parameters.AddWithValue("@FirstName", profile.FirstName);
                updatecom.Parameters.AddWithValue("@LastName", profile.LastName);
                updatecom.Parameters.AddWithValue("@City", profile.City);
                updatecom.Parameters.AddWithValue("@Age", profile.Age);
                updatecom.Parameters.AddWithValue("@Address", profile.Address);
                updatecom.Parameters.AddWithValue("@Phone", profile.Phone);
                updatecom.Parameters.AddWithValue("@Email", profile.Email);

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

        public int DeleteProfile(Profile profile)
        {
            int row = 0;
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                string query = "DELETE FROM PROFILES WHERE id= @Id";
                SQLiteCommand deletecom = new SQLiteCommand(query, con);
                deletecom.Parameters.AddWithValue("@Id", profile.Id);
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

        public List<Profile> ReadAllProfile()
        {
            List<Profile> listProfiles = new List<Profile>();
            using (SQLiteConnection con = new SQLiteConnection(conString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("Select * from PROFILES", con);
                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create a Person Object
                        Profile profile = new Profile();
                        if (Int32.TryParse(reader["Id"].ToString(), out int id))
                        {
                            profile.Id = id;
                        }
                        profile.FirstName = reader["FirstName"].ToString();
                        profile.LastName = reader["LastName"].ToString();
                        profile.City = reader["City"].ToString();
                        profile.Address = reader["Address"].ToString();
                        profile.Phone = reader["Phone"].ToString();
                        profile.Email = reader["Email"].ToString();


                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            profile.Age = age;
                        }
                        listProfiles.Add(profile);


                    }
                }
            }
            return listProfiles;
        }
    }
}
