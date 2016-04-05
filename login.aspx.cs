using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["userloggedin"] = null;
        Session["idrest"] = null;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if ((TextBox1.Text == "") || (TextBox2.Text == ""))
        {
            Label1.Text = "username or password empty";
            return;
        }
        if ((TextBox1.Text == "admin") && (TextBox2.Text == "123"))
        {
            Session["userloggedin"] = "Admin";
            Response.Redirect("admin.aspx");
        }

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SELECT  *  FROM     restaurants WHERE        (username = @username) AND (password = @password) ");
                com.CommandType = CommandType.Text;
                com.Connection = con;
                com.Parameters.AddWithValue("@username", TextBox1.Text);
                com.Parameters.AddWithValue("@password", TextBox2.Text);
                using (SqlDataAdapter da = new SqlDataAdapter(com))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                         Session["idrest"] = dt.Rows[0][0].ToString();
                         Session["userloggedin"] = dt.Rows[0][4].ToString();
                         Response.Redirect("Default.aspx");                       
                    }
                    else
                    {
                        Label1.Text = "Username Or password incorrect";
                    }
                }
            }
        }
        catch
        {
            Label1.Text = "Error";
        }
    }


}