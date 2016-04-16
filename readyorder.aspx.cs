using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.IO;


public partial class readyorder : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
    string idorderselected = "";
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
   
    void updatestate(string idorder)
    {
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "UPDATE       [order]  SET        state = 3 WHERE        (Idorder = @Idorder)";
                cmd.Parameters.AddWithValue("@Idorder", idorder);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }

   


    protected void Button1_Click(object sender, EventArgs e)
    {
        idorderselected = GridView1.SelectedRow.Cells[1].Text;
      
        if(idorderselected!="" )
        {
          
            updatestate(idorderselected);
            GridView1.DataBind();
        }
    }

   

}