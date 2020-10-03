using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.ContactClasses
{
    class ContactClass
    {
        //Getter Setter Properties
        //Acts as Data Carrier in Our Applicatipon

        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ConstactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            ///step1. Database Connection
            SqlConnection conn= new SqlConnection(myconnstrng);
            DataTable dt=new DataTable();
            try
            {
                //step2 writing SQL QUERY
                string sql = "SELECT * FROM tbl_contact";
                //creating cmd using sql and conn
                SqlCommand cmd= new SqlCommand(sql, conn);
                //creating SQL DataAdapter using zmd
                SqlDataAdapter adapter= new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        //inserting data into database
        public bool Insert(ContactClass c)
        {
            //creating a default type and setting its value to false
            bool isSuccess = false;
            
            //step 1 connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //step 2 create a slq query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //creating sql command using sql and conn
                SqlCommand cmd =new SqlCommand(sql, conn);
                //creating parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ConstactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //conection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfuly then the value of rows will be generate than zero else its value will be 0
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        //method to update in database from our application
        public bool Update(ContactClass c)
        {
            //create a default return type and set its default value to false 
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update in our database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //create sql commande
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ConstactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);

                //oen database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfuly then the value will be grearer than zero else its value will be 0

                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }

        //method to delete data from database

        public bool Delete(ContactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
    }
}
