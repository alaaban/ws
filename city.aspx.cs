using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class city : System.Web.UI.Page
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

        }
        catch(Exception ex)
        {
            Label1.Text = ex.ToString();
        }
    }
}