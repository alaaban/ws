using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class restaurants : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlDataSource1.Insert();
            Label1.Text = "It has been added successfully!";
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";

        }
        catch(Exception ex)
        {
            Label1.Text = ex.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TextBox6.Text = hfLat.Value;
        TextBox5.Text = hfLon.Value;
 
    }
}