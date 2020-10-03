using Contact.ContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        ContactClass c = new ContactClass();

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ConstactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted.");
                DataTable dt = c.Select();
                dataGridViewContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add New Contact. Try Again.");
            }


        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dataGridViewContactList.DataSource = dt;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            textBoxContactID.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNo.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(textBoxContactID.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ConstactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            bool success = c.Update(c);
            if (success==true)
            {
                MessageBox.Show("Contact has been Successfully Updated.");
                DataTable dt = c.Select();
                dataGridViewContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Update Contact. Try Again.");
            }
        }

        private void dataGridViewContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            textBoxContactID.Text = dataGridViewContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridViewContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridViewContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNo.Text = dataGridViewContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dataGridViewContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dataGridViewContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(textBoxContactID.Text);
            bool success = c.Delete(c);
            if (success==true)
            {
                MessageBox.Show("Contact successfully Deleted");
                DataTable dt = c.Select();
                dataGridViewContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact. Try again.");
            }


        }

        private static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+ "%'OR LastName LIKE '%" + keyword + "%' OR Address LIKE'%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridViewContactList.DataSource = dt;
        }
    }
}
