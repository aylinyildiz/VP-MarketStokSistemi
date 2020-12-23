﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VP_MarketStokSistemi
{
    public partial class UpdateWholesalers : Form
    {
        public UpdateWholesalers()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;initial catalog=northwind;");
        DataSet daset = new DataSet();
       
        private void UpdateWholesalers_Load(object sender, EventArgs e)
        {
            ListWholesalers();
        }
        private void ListWholesalers()
        {
            connect.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select ID, CompanyName, ContactName, Address, City, Phone, Fax from Wholesalers", connect);
            adtr.Fill(daset, "Wholesalers");
            dgwWholesalers.DataSource = daset.Tables["Wholesalers"];
            connect.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("Update Wholesalers set CompanyName=@CompanyName,ContactName=@ContactName,Address=@Address,Phone=@Phone,Fax=@Fax where ID = '" + Convert.ToInt32(dgwWholesalers.CurrentRow.Cells[0].Value) + "'", connect);
            command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
            command.Parameters.AddWithValue("@ContactName", txtContactName.Text);
            command.Parameters.AddWithValue("@Address", txtAddress.Text);
            command.Parameters.AddWithValue("@City", txtCity.Text);
            command.Parameters.AddWithValue("@Phone", txtPhone.Text);
            command.Parameters.AddWithValue("@Fax", txtFax.Text);
            command.ExecuteNonQuery();
            connect.Close();
            ListWholesalers();       
            daset.Tables["Wholesalers"].Clear();
            MessageBox.Show("Wholesaler updated!");
         
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void dgwWholesalers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgwWholesalers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCompanyName.Text = dgwWholesalers.CurrentRow.Cells["CompanyName"].Value.ToString();
            txtContactName.Text = dgwWholesalers.CurrentRow.Cells["ContactName"].Value.ToString();
            txtAddress.Text = dgwWholesalers.CurrentRow.Cells["Address"].Value.ToString();
            txtCity.Text = dgwWholesalers.CurrentRow.Cells["City"].Value.ToString();
            txtPhone.Text = dgwWholesalers.CurrentRow.Cells["Phone"].Value.ToString();
            txtFax.Text = dgwWholesalers.CurrentRow.Cells["Fax"].Value.ToString();

        }
    }
}
